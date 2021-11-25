namespace Kleene;

public abstract class Expression
{
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
    private static TextExpression Text(string value) => new(value);
    private static CharacterClassExpression CC(string chars) => new(new(chars, false));
    private static CharacterClassExpression CCN(string chars) => new(new(chars, true));

    private static TypeAssignmentExpression Type<T>(params TypeAssignmentProperty[] props) => new(typeof(T).Name, props);

    public static readonly Expression Meta = Concat(
        Fun("root", WS, Call("expression", "value"), WS, R),

        Fun("expression", Alt(Call("bullet-alt", "value"), Call("trans", "value"))),

        Fun("bullet-alt",
            Plus(Text("-"), Call("trans", "Items")), R,
            Type<AltExpression>()
        ),

        Fun("trans",
            Call("alt", "Input"), R,
            Opt(WS, Text("/"), WS, Call("alt", "Output"), Type<TransformExpression>()), R
        ),

        Fun("alt",
            Sep(Plus(Call("concat", "Items")), Concat(WS, Text("|"), WS, Type<AltExpression>())), R
        ),

        // TODO: Use <postfix> for items once left-recursion is implemented.

        Fun("concat",
            Sep(Plus(Call("capture", "Items")), Concat(WS, Type<ConcatExpression>())), R
        ),

        Fun("capture", Alt(
            Concat(
                // Special case. Captures on calls have to be handled by the call expression itself.
                Call("call", "expression"),
                Scope("expression",
                    Opt(Text(":"), Call("dotted-name", "CaptureName")), R,
                    Type<CallExpression>()
                )
            ),
            Concat(
                Call("quant", "Expression"), R,
                Opt(Text(":"), Call("dotted-name", "Name"), Type<CaptureExpression>()), R
            )
        )),

        Fun("quant",
            Call("req", "Expression"), R,
            Opt(
                Type<RepExpression>(),
                Alt(
                    Text("*"),
                    Cap("Min", Trans(Text("+"), Text("1"))),
                    Concat(Text("^"), Cap("Min", Plus(D)), Text("+")),
                    Concat(Text("^"), Cap("Min", Plus(D)), Text("-"), Cap("Max", Plus(D))),
                    Concat(Text("^"), Cap("Max", Cap("Min", Plus(D))))
                ), R,
                Opt(Cap("Eval", Trans(Text("?"), Text("Lazy")))), R,
                Opt(WS, Text("%"), WS, Call("capture", "Separator")), R
            ), R
        ),

        Fun("req",
            Call("item", "Expression"), R,
            Opt(Text("!"), Type<ReqExpression>()), R
        ),

        Fun("item", Alt(
            Call("assignment"),
            Call("subexpression"),
            Call("scope"),
            Call("atomic"),
            Call("group"),
            Call("backreference"),
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
            Text("(:"), Call("dotted-name", "Name"), WS, Text("="), WS, Call("static", "Value"), Text(")"), R,
            Type<AssignmentExpression>()
        ),

        Fun("subexpression",
            Text("(?"),
            WS, Call("static", "Input"), WS, R,
            Opt(Call("expression", "Expression"), WS),
            Text(")"), R,
            Type<SubExpression>()
        ),

        Fun("scope",
            Text("(="),
            WS, Call("dotted-name", "Name"), WS, R,
            Opt(Call("expression", "Expression"), WS),
            Text(")"), R,
            Type<ScopeExpression>()
        ),

        Fun("atomic",
            Text("(>"),
            WS, Call("expression", "Expression"), WS,
            Text(")"), R,
            Type<AtomicExpression>()
        ),

        Fun("group",
            Text("("), WS, Call("expression", "Expression"), WS, Text(")"), R
        ),

        Fun("backreference",
            Call("dotted-capture-name", "Name"), Type<BackreferenceExpression>(), R
        ),

        Fun("type-set",
            Text("::"), Call("type-prop-name", "Name"), R,
            Opt(
                WS, Text("{"), WS,
                Sep(Star(Cap("Properties", Call("type-prop-name", "Name"), WS, Text("="), WS, Call("static", "Value"))), Concat(Text(","), WS)),
                WS, Text("}")
            ), R
        ),

        Fun("function",
            Text("<"), Call("name", "Name"), Text(">"), WS, Text("{"), WS, Call("expression", "Expression"), WS, Text("}"), R,
            Type<FunctionExpression>()
        ),

        Fun("call",
            Text("<"), Call("name", "Name"), Text(">"), R,
            Type<CallExpression>()
        ),

        Fun("char-class", Alt(Call("predefined-char-class"), Call("literal-char-class"))),

        Fun("predefined-char-class", Alt(
            Call("positive-predefined-char-class"),
            Call("negative-predefined-char-class")
        )),

        Fun("positive-predefined-char-class",
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
            ), R,
            Type<CharacterClassExpression>()
        ),

        Fun("negative-predefined-char-class",
            Alt(
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
            ), R,
            Cap("Negated"),
            Type<CharacterClassExpression>()
        ),

        Fun("literal-char-class",
            Text("["),
            Opt(Cap("Negated", Text("^"))),
            Alt(
                Call("character-escape"),
                Call("positive-predefined-char-class"),
                Plus(CCN("<>\\"))
            ),
            Text("]"), R
        ),

        Fun("anchor", Alt(
            Call("predefined-anchor"), Call("literal-anchor")
        )),

        Fun("predefined-anchor",
            Cap("start", CC("<>")),
            Opt(Cap("Negated", Text("!"))),
            Alt(
                Call("char-class", "CharClass"),
                Sub("\\w", Call("char-class", "CharClass")) // Default char-class to \w
            ),
            Cap("end", CC("<>")),
            Alt(
                Concat(Sub(Ref("start"), Text("<")), Sub(Ref("end"), Text("<")), Assign("Type", "Start")),
                Concat(Sub(Ref("start"), Text(">")), Sub(Ref("end"), Text(">")), Assign("Type", "End")),
                Concat(Sub(Ref("start"), Text("<")), Sub(Ref("end"), Text(">")), Assign("Type", "Outer")),
                Concat(Sub(Ref("start"), Text(">")), Sub(Ref("end"), Text("<")), Assign("Type", "Inner"))
            ), R,
            Type<AnchorExpression>()
        ),

        Fun("literal-anchor",
            Alt(
                Concat(Text("^^"), Assign("Type", "Start"), Sub("\\N", Call("char-class", "CharClass"))),
                Concat(Text("$$"), Assign("Type", "End"), Sub("\\N", Call("char-class", "CharClass"))),
                Concat(Text("^"), Assign("Type", "Start"), Sub(".", Call("char-class", "CharClass"))),
                Concat(Text("$$"), Assign("Type", "End"), Sub(".", Call("char-class", "CharClass")))
            ), R,
            Type<AnchorExpression>()
        ),

        Fun("ratchet", Text(";"), Type<RatchetExpression>()),

        Fun("special", Alt(
            Call("pass"),
            Call("fail")
        )),

        Fun("pass", Text("?"), Type<PassExpression>()),

        Fun("pass", Text("?"), Type<FailExpression>()),

        Fun("text", Alt(Call("string"), Call("literal"))),

        Fun("string",
            Trans(Text("'"), Concat()), Star(Alt(Plus(CCN("'[]")), Call("char-escape"))), Trans(Text("'"), Concat()), R,
            Type<TextExpression>()
        ),

        Fun("literal",
            Plus(CC("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_")),
            Type<TextExpression>()
        ),

        Fun("character-escape",
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

        Fun("dotted-name", Sep(Plus(Call("name", "Parts")), Text(".")), R),

        Fun("dotted-capture-name", Text("@"), Call("dotted-name")),

        Fun("type-prop-name",
            CC("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_"),
                Star(CC("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_")), R,
            Opt(Text("<"), Sep(Plus(Call("type-prop-name")), Concat(Text(","), Star(S))), Text(">")), R
        ),

        Fun("dotted-type-prop-name",
            Sep(Plus(Call("name", "Name")), Text(".")), R
        ),

        Call("root")
    );

    public string? Transform(string input)
    {
        var context = new ExpressionContext(input);
        foreach (var result in Run(context))
        {
            if (context.Local.IsAtEnd)
                return result.Output;
        }
        return null;
    }

    public string? RunFull(string input, out CaptureTree? captureTree)
    {
        var context = new ExpressionContext(input);
        if (Run(context).FirstOrDefault(_ => context.Local.Index == input.Length) is ExpressionResult result)
        {
            captureTree = context.CaptureTree;
            return result.Output;
        }
        else
        {
            captureTree = null;
            return null;
        }
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

    public static Expression Parse(string pattern)
    {
        throw new NotImplementedException();
    }
}
