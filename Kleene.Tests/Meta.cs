namespace Kleene.Tests
{
    public static class Meta
    {
        public static readonly string Expression = @":::Kleene

<root> { <ws> <expression>:value <ws> ; }

<expression> { <bullet-alt>:value | <trans>:value }

<bullet-alt> {
    ('-' <ws> <trans>:Expressions)+ % <ws>;
    ::AltExpression.Model
}

<trans> {
    <alt>:value ;
    (<ws> '/' <ws> <alt>:Output @/value/Input ::TransformExpression.Model)? ;
}

<alt> {
    <concat>:value+ % (<ws> '|' <ws> ::AltExpression.Model:alt) ;
    ((?@alt) @/value/Expressions)?
}

#TODO: Use <postfix> for items once left-recursion is implemented.
# For now, you have to put parentheses around captures to add a quantifier.

<concat> {
    <capture>:value+ % (<ws> ::ConcatExpression.Model:concat) ;
    ((?@concat) @/value/Expressions)
}

<capture> {
    <quant>:value ;
    (':' <dotted-name>:Name @/value/Expression ::CaptureExpression.Model)? ;
}

<quant> {
    <req>:value ;
    (
        ::RepExpression.Model
        (
            - '*'
            - ('+'/1):Min
            - '^' \d+:Min '+'
            - '^' \d+:Min '-' \d+:Max
            - '^'( \d+:Min):Max

            - ('?'/1) ::OptExpression.Model
        ) ;
        (('?'/Lazy):Eval)? ;
        ( <ws> '%' <ws> <capture>:Separator )?
        @/value/Expression;
    )? ;
}

<req> {
    <item>:value ;
    ('!' @/value/Expression ::ReqExpression.Model)? ;
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
        ::AssignmentExpression.Model
}

<subexpression> {
    '(?'
    <ws> <static>:Input <ws> ;
    (<expression>:Expression <ws>)?
    ')' ;
    ::SubExpression.Model
}

<scope> {
    '(='
    <ws> <dotted-name>:Name <ws> ;
    (<expression>:Expression <ws>)?
    ')' ;
    ::ScopeExpression.Model
}

<atomic> {
    '(>'
    <ws> <expression>:Expression <ws>
    ')' ;
    ::AtomicExpression.Model
}

<group> {
    '(' <ws> <expression>:value <ws> ')' ;
}

<backreference> {
    <dotted-capture-name>:Name ::BackreferenceExpression.Model ;
}

<rename> {
    '@/' <dotted-name>:Name
    '/' <name>:NewName ;
    ::RenameExpression.Model
}

<using> { ':::' <dotnet-namespace-name>:Name ::UsingExpression.Model ; }

<type-set> {
    '::' <dotnet-type-name>:Name ;
    (
        <ws> '{' <ws>
        ((<dotnet-name>:Name <ws> '=' <ws> <static>:Value):Properties)* % (',' <ws>)
        <ws> '}'
    )? ;
    ::TypeSetExpression.Model
}

<function> {
    '<' <name>:Name '>' <ws> '{' <ws> <expression>:Expression <ws> '}' ;
    ::FunctionExpression.Model
}

<call> {
    '<' <name>:Name '>' ;
     (':' <dotted-name>:CaptureName)? ;
    ::CallExpression.Model
}

<char-class> { <predefined-char-class> | <literal-char-class> }

<predefined-char-class> {
    - <positive-predefined-char-class>
    - <negative-predefined-char-class>
}

<positive-predefined-char-class> {
    (
        - '\\' / '\'
        - '\t' / '[t]'
        - '\h' / ' [t]'
        - '\s' / ' [t][n]'
        - '\d' / '0123456789'
        - '\u' / 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'
        - '\l' / 'abcdefghijklmnopqrstuvwxyz'
        - '\a' / 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'
        - '\w' / 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789'
    ) ;
    ::CharClassExpression.Model
}

<negative-predefined-char-class> {
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
    ) ;
    ?:Negated
    ::CharClassExpression.Model
}

<literal-char-class> {
    '[<]'
    ('^':Negated)?
    (
        - <char-escape>
        - <positive-predefined-char-class>
        - [^[<][>]\\]+
    )+
    '[>]' ;
    ::CharClassExpression.Model
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
    ::AnchorExpression.Model
}

<literal-anchor> {
    (
        - '^^' (:Type = Start) (?'\N' <char-class>:CharClass)
        - '$$' (:Type = End) (?'\N' <char-class>:CharClass)
        - '^' (:Type = Start) (?'.' <char-class>:CharClass)
        - '$' (:Type = End) (?'.' <char-class>:CharClass)
    ) ;
    ::AnchorExpression.Model
}

<ratchet> { ';' ::RatchetExpression.Model }

<special> {
    - <pass>
    - <fail>
}

<pass> { '?' ::PassExpression.Model }

<fail> { '!' ::FailExpression.Model }

<text> { <string> | <literal> }

<string> {
    ('[']'/?) ([^'[<][>]]+ | <char-escape>)* ('[']'/?) ;
    ::TextExpression.Model
}

<literal> {
    [\w_]+
    ::TextExpression.Model
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

<dotted-name> {<name>+ % '.' ; }

<dotted-capture-name> { '@' <dotted-name> ; }

<dotnet-namespace-name> { <dotnet-name>+ % '.' ; }

<dotnet-type-name> {
    (<dotnet-namespace-name> '.')?
    <dotnet-name>
    ('<' <dotnet-type-name>+ % (',' WS) '>')? ;
}

<dotnet-name> { [\a_][\w_]* ; }

<root>";
    }
}