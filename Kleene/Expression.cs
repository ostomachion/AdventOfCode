using Kleene.Models;

namespace Kleene;

public abstract class Expression
{
    public const int ToStringLength = 120;

    private static Expression Concat(params Expression[] children)
    {
        return children.Length switch
        {
            0 => Pass(),
            1 => children[0],
            _ => new ConcatExpression(children)
        };
    }

    private static Expression Alt(params Expression[] children)
    {
        return children.Length switch
        {
            0 => Fail(),
            1 => children[0],
            _ => new AltExpression(children)
        };
    }

    private static PassExpression Pass() => new();
    private static FailExpression Fail() => new();

    private static FunctionExpression Fun(string name, params Expression[] children) => new(name, Concat(children));
    private static CallExpression Call(string name, CaptureName? captureName = null) => new(name, captureName);
    private static CallExpression WS => Call("ws");
    private static RatchetExpression R => new();
    private static CharacterClassExpression D => new(new("0123456789", false));
    private static CharacterClassExpression S => new(new(" \t\n", false));
    private static CharacterClassExpression A => new(new("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", false));
    private static CharacterClassExpression W => new(new("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", false));

    private static RepExpression Star(params Expression[] children) => new(Concat(children), null, new RepCount(0, RepCount.Unbounded));
    private static RepExpression Plus(params Expression[] children) => new(Concat(children), null, new RepCount(1, RepCount.Unbounded));
    private static OptExpression Opt(params Expression[] children) => new(Concat(children));
    private static RepExpression Sep(RepExpression expression, Expression sep) => new(expression.Expression, sep, expression.Count, expression.Order);
    private static ScopeExpression Scope(string name, params Expression[] children) => new(name, Concat(children));
    private static CaptureExpression Cap(string name, params Expression[] children) => new(name, Concat(children));
    private static TransformExpression Trans(Expression input, Expression output) => new(input, output);
    private static SubExpression Sub(TextValueExpression input, params Expression[] children) => new(input, Concat(children));
    private static BackreferenceExpression Ref(string name) => new(name);
    private static AssignmentExpression Assign(string name, TextValueExpression value) => new(name, value);
    private static RenameExpression Rename(string name, string newName) => new(name, newName);
    private static TextExpression Text(string value) => new(value);
    private static CharacterClassExpression CC(string chars) => new(new(chars, false));
    private static CharacterClassExpression CCN(string chars) => new(new(chars, true));

    private static UsingExpression Use(string name) => new(name);
    private static TypeAssignmentExpression Type<T>(params TypeAssignmentProperty[] props) => new(typeof(T).FullName!, props);

    public static readonly Expression Meta = Concat(
        Use("Kleene.Models"),

        Fun("root", WS, Call("expression", "value"), WS, R),

        Fun("expression", Alt(Call("bullet-alt", "value"), Call("trans", "value"))),

        Fun("bullet-alt",
            Sep(Plus(Text("-"), WS, Call("trans", "Expressions")), WS), R,
            Type<AltExpressionModel>()
        ),

        Fun("trans",
            Call("alt", "value"), R,
            Opt(WS, Text("/"), WS, Call("alt", "Output"), Rename("value", "Input"), Type<TransformExpressionModel>()), R
        ),

        Fun("alt",
            Sep(Plus(Call("concat", "value")), Concat(WS, Text("|"), WS, Cap("alt", Type<AltExpressionModel>()))), R,
            Opt(Sub(Ref("alt")), Rename("value", "Expressions"))
        ),

        // TODO: Use <postfix> for items once left-recursion is implemented.

        Fun("concat",
            Sep(Plus(Call("capture", "value")), Concat(WS, Cap("concat", Type<ConcatExpressionModel>()))), R,
            Opt(Sub(Ref("concat")), Rename("value", "Expressions"))
        ),

        Fun("capture", Alt(
            Concat(
                Call("quant", "value"), R,
                Opt(Text(":"), Call("dotted-name", "Name"), Rename("value", "Expression"), Type<CaptureExpressionModel>()), R
            )
        )),

        Fun("quant",
            Call("req", "value"), R,
            Opt(
                Type<RepExpressionModel>(),
                Alt(
                    Text("*"),
                    Cap("Count.Min", Trans(Text("+"), Text("1"))),
                    Concat(Text("^"), Cap("Count.Min", Plus(D)), Text("+")),
                    Concat(Text("^"), Cap("Count.Min", Plus(D)), Text("-"), Cap("Count.Max", Plus(D))),
                    Concat(Text("^"), Cap("Count.Max", Cap("Count.Min", Plus(D)))),

                    Concat(Text("?"), Type<OptExpressionModel>())
                ), R,
                Opt(Cap("Eval", Trans(Text("?"), Text("Lazy")))), R,
                Opt(WS, Text("%"), WS, Call("capture", "Separator")), R,
                Rename("value", "Expression")
            ), R
        ),

        Fun("req",
            Call("item", "value"), R,
            Opt(Text("!"), Rename("value", "Expression"), Type<ReqExpressionModel>()), R
        ),

        Fun("item", Alt(
            Call("assignment"),
            Call("subexpression"),
            Call("scope"),
            Call("atomic"),
            Call("group"),
            Call("backreference"),
            Call("rename"),
            Call("using"),
            Call("type-set"),
            Call("function"),
            Call("call"),
            Call("char-class"),
            Call("anchor"),
            Call("ratchet"),
            Call("special"),
            Call("text")
        )),

        Fun("assignment",
            Text("(:"), WS, Call("dotted-name", "Name"), WS, Text("="), WS, Call("static", "Value"), WS, Text(")"), R,
            Type<AssignmentExpressionModel>()
        ),

        Fun("subexpression",
            Text("(?"),
            WS, Call("static", "Input"), WS, R,
            Opt(Call("expression", "Expression"), WS),
            Text(")"), R,
            Type<SubExpressionModel>()
        ),

        Fun("scope",
            Text("(="),
            WS, Call("dotted-name", "Name"), WS, R,
            Opt(Call("expression", "Expression"), WS),
            Text(")"), R,
            Type<ScopeExpressionModel>()
        ),

        Fun("atomic",
            Text("(>"),
            WS, Call("expression", "Expression"), WS,
            Text(")"), R,
            Type<AtomicExpressionModel>()
        ),

        Fun("group",
            Text("("), WS,
            Call("expression", "Expressions"),
            WS, Text(")"), R,
            Type<ConcatExpressionModel>() // TODO: Optimize this.
        ),

        Fun("backreference",
            Call("dotted-capture-name", "Name"), Type<BackreferenceExpressionModel>(), R
        ),

        Fun("rename",
            Text("@/"), Call("dotted-name", "Name"),
            Text("/"), Call("name", "NewName"), R,
            Type<RenameExpressionModel>()
        ),

        Fun("using",
            Text(":::"), Call("dotnet-namespace-name", "NamespaceName"), Type<UsingExpressionModel>(), R
        ),

        Fun("type-set",
            Text("::"), Call("dotnet-type-name", "TypeName"), R,
            Opt(
                WS, Text("{"), WS,
                Sep(Star(Cap("Properties", Call("dotnet-name", "Name"), WS, Text("="), WS, Call("static", "Value"))), Concat(Text(","), WS)),
                WS, Text("}")
            ), R,
            Type<TypeAssignmentExpressionModel>()
        ),

        Fun("function",
            Text("<"), Call("name", "Name"), Text(">"), WS, Text("{"), WS, Call("expression", "Expression"), WS, Text("}"), R,
            Type<FunctionExpressionModel>()
        ),

        Fun("call",
            Text("<"), Call("name", "Name"), Text(">"), R,
            Opt(Text(":"), Call("dotted-name", "CaptureName")), R,
            Type<CallExpressionModel>()
        ),

        Fun("char-class", Alt(Call("predefined-char-class"), Call("literal-char-class"))),

        Fun("predefined-char-class", Alt(
            Call("positive-predefined-char-class"),
            Call("negative-predefined-char-class")
        )),

        Fun("positive-predefined-char-class",
            Cap("CharacterClass", Cap("Characters", Call("positive-predefined-char-class-chars")), R),
            Type<CharacterClassExpressionModel>()
        ),

        Fun("positive-predefined-char-class-chars",
            Alt(
                Trans(Text("\\\\"), Text("\\")),
                Trans(Text("\\n"), Text("\n")),
                Trans(Text("\\t"), Text("\t")),
                Trans(Text("\\h"), Text(" \t")),
                Trans(Text("\\s"), Text(" \t\n")),
                Trans(Text("\\d"), Text("0123456789")),
                Trans(Text("\\u"), Text("ABCDEFGHIJKLMNOPQRSTUVWXYZ")),
                Trans(Text("\\l"), Text("abcdefghijklmnopqrstuvwxyz")),
                Trans(Text("\\a"), Text("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")),
                Trans(Text("\\w"), Text("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"))
            ), R
        ),

        Fun("negative-predefined-char-class",
            Cap("CharacterClass", Cap("Characters", Alt(
                Trans(Text("."), Text("")),
                Trans(Text("\\N"), Text("\n")),
                Trans(Text("\\T"), Text("\t")),
                Trans(Text("\\H"), Text(" \t")),
                Trans(Text("\\S"), Text(" \t\n")),
                Trans(Text("\\D"), Text("0123456789")),
                Trans(Text("\\U"), Text("ABCDEFGHIJKLMNOPQRSTUVWXYZ")),
                Trans(Text("\\L"), Text("abcdefghijklmnopqrstuvwxyz")),
                Trans(Text("\\A"), Text("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")),
                Trans(Text("\\W"), Text("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"))
            )), R,
            Cap("Negated")),
            Type<CharacterClassExpressionModel>()
        ),

        Fun("literal-char-class",
            Cap("CharacterClass",
                Text("["),
                Opt(Cap("Negated", Text("^"))), R,
                Cap("Characters", Plus(Alt(
                    Call("char-escape"),
                    Call("positive-predefined-char-class-chars"),
                    Plus(CCN("[]\\"))
                ))),
                Text("]"), R
            ),
            Type<CharacterClassExpressionModel>()
        ),

        Fun("anchor", Alt(
            Call("predefined-anchor"), Call("literal-anchor")
        )),

        Fun("predefined-anchor",
            Cap("Anchor", Alt(
                Concat(Text("^^"), Assign("Type", "Start"), Sub("\\N", Call("char-class", "CharacterClass"))),
                Concat(Text("$$"), Assign("Type", "End"), Sub("\\N", Call("char-class", "CharacterClass"))),
                Concat(Text("^"), Assign("Type", "Start"), Sub(".", Call("char-class", "CharacterClass"))),
                Concat(Text("$$"), Assign("Type", "End"), Sub(".", Call("char-class", "CharacterClass")))
            )), R,
            Type<AnchorExpressionModel>()
        ),

        Fun("literal-anchor",
            Cap("Anchor", 
                Cap("start", CC("<>")),
                Opt(Cap("Negated", Text("!"))),
                Alt(
                    Call("char-class", "CharacterClass"),
                    Sub("\\w", Call("char-class", "CharacterClass")) // Default char-class to \w
                ),
                Cap("end", CC("<>")),
                Alt(
                    Concat(Sub(Ref("start"), Text("<")), Sub(Ref("end"), Text("<")), Assign("Type", "Start")),
                    Concat(Sub(Ref("start"), Text(">")), Sub(Ref("end"), Text(">")), Assign("Type", "End")),
                    Concat(Sub(Ref("start"), Text("<")), Sub(Ref("end"), Text(">")), Assign("Type", "Outer")),
                    Concat(Sub(Ref("start"), Text(">")), Sub(Ref("end"), Text("<")), Assign("Type", "Inner"))
                ), R
            ),
            Type<AnchorExpressionModel>()
        ),

        Fun("ratchet", Text(";"), Type<RatchetExpressionModel>()),

        Fun("special", Alt(
            Call("pass"),
            Call("fail")
        )),

        Fun("pass", Text("?"), Type<PassExpressionModel>()),

        Fun("fail", Text("!"), Type<FailExpressionModel>()),

        Fun("text", Alt(Call("string"), Call("literal"))),

        Fun("string",
            Trans(Text("'"), Concat()), Star(Alt(Plus(CCN("'[]")), Call("char-escape"))), Trans(Text("'"), Concat()), R,
            Type<TextExpressionModel>()
        ),

        Fun("literal",
            Plus(CC("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_")),
            Type<TextExpressionModel>()
        ),

        Fun("char-escape",
            Trans(Text("["), Concat()),
            Alt(
                Text("'"),
                Trans(Text("<"), Text("[")),
                Trans(Text(">"), Text("]")),
                Trans(Text("n"), Text("\n")),
                Trans(Text("t"), Text("\t"))
                // TODO: Unicode escapes.
            ),
            Trans(Text("]"), Concat()), R
        ),

        Fun("static", Alt(Call("backreference"), Call("text"))),

        Fun("ws", Star(Alt(Plus(S), Call("comment"))), R),

        Fun("comment", Alt(Call("block-comment"), Call("line-comment")), R),

        Fun("block-comment", Text("#{"), Star(CCN("}")), Text("}"), R),

        Fun("line-comment", Text("#"), Star(CCN("\n")), R),

        Fun("name", Sep(Plus(A, Star(W)), CC("-_")), R),

        Fun("capture-name", Text("@"), Call("name")),

        Fun("dotted-name", Sep(Plus(Call("name")), Text(".")), R),

        Fun("dotted-capture-name", Text("@"), Call("dotted-name")),

        Fun("dotnet-namespace-name",
            Sep(
                Plus(Call("dotnet-name")),
                Text(".")
            )
        ),

        Fun("dotnet-type-name",
            Opt(Call("dotnet-namespace-name"), Text(".")),
            Call("dotnet-name"),
            Opt(
                Text("<"),
                Sep(Plus(Call("dotnet-type-name")), Concat(Text(","), WS)),
                Text(">")
            ), R
        ),

        Fun("dotnet-name",
            CC("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_"),
                Star(CC("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_")), R
        ),

        Call("root")
    );

    public string? RunFull(string input, out CaptureTree? captureTree)
    {
        var context = new ExpressionContext(input);
        if (Run(context).FirstOrDefault(_ => context.Local.IsAtEnd) is ExpressionResult result)
        {
            captureTree = context.CaptureTree;
            captureTree.Close(CaptureTree.RootCaptureName, result);
            return result.Output;
        }
        else
        {
            captureTree = null;
            return null;
        }
    }

    public string? Transform(string input) => RunFull(input, out _);

    public T? Parse<T>(string input)
    {
        if (RunFull(input, out var captureTree) is null)
            throw new Exception("Could not parse value.");
        
        return captureTree!.Root.Parse<T>();
    }

    public abstract IEnumerable<ExpressionResult> RunInternal(ExpressionContext context);

    public IEnumerable<ExpressionResult> Run(ExpressionContext context)
    {
        if (context.Ratchet)
            yield break;

        foreach (var result in RunInternal(context))
        {
            if (!context.Ratchet)
                yield return result;
        }
    }

    public static Expression Parse(string pattern) => Meta.Parse<IModel<Expression>>(pattern)?.Convert() ?? throw new Exception("Could not parse pattern.");
}
