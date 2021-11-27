namespace Kleene.Tests
{
    public static class Meta
    {
        public static readonly string Expression2 = @"a";
        public static readonly string Expression = @":::Kleene.Models

<root> { <ws> <expression>:value <ws> ; }

<expression> { <bullet-alt>:value | <trans>:value }

<bullet-alt> {
    ('-' <ws> <trans>:Expressions)+ % <ws>;
    ::AltExpressionModel
}

<trans> {
    <alt>:value ;
    (<ws> '/' <ws> <alt>:Output @/value/Input ::TransformExpressionModel)? ;
}

<alt> {
    <concat>:value+ % (<ws> '|' <ws> ::AltExpressionModel:alt) ;
    ((?@alt) @/value/Expressions)?
}

#TODO: Use <postfix> for items once left-recursion is implemented.
# For now, you have to put parentheses around captures to add a quantifier.

<concat> {
    <capture>:value+ % (<ws> ::ConcatExpressionModel:concat) ;
    ((?@concat) @/value/Expressions)
}

<capture> {
    <quant>:value ;
    (':' <dotted-name>:Name @/value/Expression ::CaptureExpressionModel)? ;
}

<quant> {
    <req>:value ;
    (
        ::RepExpressionModel
        (
            - '*'
            - ('+'/1):Count.Min
            - '^' \d+:Count.Min '+'
            - '^' \d+:Count.Min '-' \d+:Count.Max
            - '^'( \d+:Count.Min):Count.Max

            - '?' ::OptExpressionModel
        ) ;
        (('?'/Lazy):Eval)? ;
        ( <ws> '%' <ws> <capture>:Separator )?
        @/value/Expression;
    )? ;
}

<req> {
    <item>:value ;
    ('!' @/value/Expression ::ReqExpressionModel)? ;
}

<item> {
    - <assignment>
    - <subexpression>
    - <scope>
    - <atomic>
    - <group>
    - <backreference>
    - <rename>
    - <using>
    - <type-set>
    - <function>
    - <call>
    - <char-class>
    - <anchor>
    - <ratchet>
    - <special>
    - <text>
}

<assignent> {
    '(:' <dotted-name>:Name <ws> '=' <ws> <static>:Value ')' ;
        ::AssignmentExpressionModel
}

<subexpression> {
    '(?'
    <ws> <static>:Input <ws> ;
    (<expression>:Expression <ws>)?
    ')' ;
    ::SubExpressionModel
}

<scope> {
    '(='
    <ws> <dotted-name>:Name <ws> ;
    (<expression>:Expression <ws>)?
    ')' ;
    ::ScopeExpressionModel
}

<atomic> {
    '(>'
    <ws> <expression>:Expression <ws>
    ')' ;
    ::AtomicExpressionModel
}

<group> {
    '(' <ws> <expression>:value <ws> ')' ;
}

<backreference> {
    <dotted-capture-name>:Name ::BackreferenceExpressionModel ;
}

<rename> {
    '@/' <dotted-name>:Name
    '/' <name>:NewName ;
    ::RenameExpressionModel
}

<using> { ':::' <dotnet-namespace-name>:NamespaceName ::UsingExpressionModel ; }

<type-set> {
    '::' <dotnet-type-name>:Name ;
    (
        <ws> '{' <ws>
        ((<dotnet-name>:TypeName <ws> '=' <ws> <static>:Value):Properties)* % (',' <ws>)
        <ws> '}'
    )? ;
    ::TypeSetExpressionModel
}

<function> {
    '<' <name>:Name '>' <ws> '{' <ws> <expression>:Expression <ws> '}' ;
    ::FunctionExpressionModel
}

<call> {
    '<' <name>:Name '>' ;
     (':' <dotted-name>:CaptureName)? ;
    ::CallExpressionModel
}

<char-class> { <predefined-char-class> | <literal-char-class> }

<predefined-char-class> {
    - <positive-predefined-char-class>
    - <negative-predefined-char-class>
}

<positive-predefined-char-class> {
    (
        <positive-predefined-char-class-chars>:Characters ;
    ):CharacterClass
    ::CharClassExpressionModel
}

<positive-predefined-char-class-chars> {
    - '\\' / '\'
    - '\t' / '[t]'
    - '\h' / ' [t]'
    - '\s' / ' [t][n]'
    - '\d' / '0123456789'
    - '\u' / 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'
    - '\l' / 'abcdefghijklmnopqrstuvwxyz'
    - '\a' / 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'
    - '\w' / 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789'
}

<negative-predefined-char-class> {
    (
        (
            - '.'  / ''
            - '\N' / '[n]'
            - '\T' / '[t]'
            - '\H' / ' [t]'
            - '\S' / ' [t][n]'
            - '\D' / '0123456789'
            - '\U' / 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'
            - '\L' / 'abcdefghijklmnopqrstuvwxyz'
            - '\A' / 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'
            - '\W' / 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789'
        ):Characters ;
        ?:Negated
    ):CharacterClass
    ::CharClassExpressionModel
}

<literal-char-class> {
    (
        '[<]'
        ('^':Negated)?
        ((
            - <char-escape>
            - <positive-predefined-char-class-chars>
            - [^[<][>]\\]+
        )+):Characters
        '[>]' ;
    ):CharacterClass
    ::CharClassExpressionModel
}

<anchor> { <predefined-anchor> | <literal-anchor> }

<predefined-anchor> {
    [<>]:start
    ('!':Negated)?
    (
        - <char-class>:CharClass
        - (?'\w' <char-class>:CharClass) # Default char-class to \w
    )
    [<>]:end
    (
        - (?@start '<') (?@end '<') (:Type = Start)
        - (?@start '>') (?@end '>') (:Type = End)
        - (?@start '<') (?@end '>') (:Type = Outer)
        - (?@start '>') (?@end '<') (:Type = Inner)
    ) ;
    ::AnchorExpressionModel
}

<literal-anchor> {
    (
        - '^^' (:Type = Start) (?'\N' <char-class>:CharClass)
        - '$$' (:Type = End) (?'\N' <char-class>:CharClass)
        - '^' (:Type = Start) (?'.' <char-class>:CharClass)
        - '$' (:Type = End) (?'.' <char-class>:CharClass)
    ) ;
    ::AnchorExpressionModel
}

<ratchet> { ';' ::RatchetExpressionModel }

<special> {
    - <pass>
    - <fail>
}

<pass> { '?' ::PassExpressionModel }

<fail> { '!' ::FailExpressionModel }

<text> { <string> | <literal> }

<string> {
    ('[']'/?) ([^'[<][>]]+ | <char-escape>)* ('[']'/?) ;
    ::TextExpressionModel
}

<literal> {
    [\w_]+
    ::TextExpressionModel
}

<char-escape> {
    ('[<]'/?)
    (
        - ('[']')
        - ('<'/'[<]')
        - ('>'/'[>]')
        - ('n'/'[n]')
        - ('t'/'[t]')
        # - <UnicodeEscapes> # [U+0000]
    )
    ('[>]'/?) ;
}

<static> { <backreference> | <text> }

<ws> { (\s+ | <comment>)* ; }

<comment> { (<block-comment> | <line-comment>) ; }

<block-comment> { '#{' [^}]* '}' ; }

<line-comment> { '#' [^[n]]* ; }

<name> { (\a \w*)+ % [-_] ; }

<capture-name> { '@' <name> ; }

<dotted-name> { <name>+ % '.' ; }

<dotted-capture-name> { '@' <dotted-name> ; }

<dotnet-namespace-name> { <dotnet-name>+ % '.' }

<dotnet-type-name> {
    (<dotnet-namespace-name> '.')?
    <dotnet-name>
    ('<' <dotnet-type-name>+ % (',' WS) '>')? ;
}

<dotnet-name> { [\a_][\w_]* ; }

<root>";
    }
}