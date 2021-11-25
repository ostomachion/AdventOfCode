namespace Kleene.Tests
{
    public static class Meta
    {
        public static readonly string Expression = @"<root> { <ws> <expression>:value <ws> ; }

<expression> { <bullet-alt>:value | <trans>:value }

<bullet-alt> {
    ('-' <ws> <trans>:Items)+ % <ws>;
    ::AltExpression
}

<trans> {
    <alt>:Input ;
    (<ws> '/' <ws> <alt>:Output ::TransformExpression)? ;
}

<alt> {
    (<concat>:Items)+ % (<ws> '|' <ws> ::AltExpression) ;
}

#TODO: Use <postfix> for items once left-recursion is implemented.
# For now, you have to put parentheses around captures to add a quantifier.

<concat> {
    (<capture>:Items)+ % (<ws> ::ConcatExpression) ;
}

<capture> {
    # Special case. Captures on calls have to be handled by the call expression itself.
    <call>:expression ;
    (=expression
        (':' <dotted-name>:CaptureName)? ;
        ::CallExpression
    )
    |
    <quant>:Expression ;
    (':' <dotted-name>:Name ::CaptureExpression)? ;
}

<quant> {
    <req>:Expression ;
    (
        ::RepExpression
        (
            - '*'
            - ('+'/1):Min
            - '^' \d+:Min '+'
            - '^' \d+:Min '-' \d+:Max
            - '^'( \d+:Min):Max

            - ('?'/1) ::OptExpression
        ) ;
        (('?'/Lazy):Eval)? ;
        ( <ws> '%' <ws> <capture>:Separator )? ;
    )? ;
}

<req> {
    <item>:Expression ;
    ('!' ::ReqExpression)? ;
}

<item> {
    - <assignment>
    - <subexpression>
    - <scope>
    - <atomic>
    - <group>
    - <backreference>
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
        ::AssignmentExpression
}

<subexpression> {
    '(?'
    <ws> <static>:Input <ws> ;
    (<expression>:Expression <ws>)?
    ')' ;
    ::SubExpression
}

<scope> {
    '(='
    <ws> <dotted-name>:Name <ws> ;
    (<expression>:Expression <ws>)?
    ')' ;
    ::ScopeExpression
}

<atomic> {
    '(>'
    <ws> <expression>:Expression <ws>
    ')' ;
    ::AtomicExpression
}

<group> {
    '(' <ws> <expression>:Expression <ws> ')' ;
}

<backreference> {
    <dotted-capture-name>:Name ::BackreferenceExpression ;
}

<type-set> {
    '::' <type-prop-name>:Name ;
    (
        <ws> '{' <ws>
        ((<type-prop-name>:Name <ws> '=' <ws> <static>:Value):Properties)* % (',' <ws>)
        <ws> '}'
    )? ;
    ::TypeSetExpression
}

<function> {
    '<' <name>:Name '>' <ws> '{' <ws> <expression>:Expression <ws> '}' ;
    ::FunctionExpression
}

<call> {
    '<' <name>:Name '>' ;
    ::CallExpression
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
    ::CharClassExpression
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
    ::CharClassExpression
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
    ::AnchorExpression
}

<literal-anchor> {
    (
        - '^^' (:Type = Start) (?'\N' <char-class>:CharClass)
        - '$$' (:Type = End) (?'\N' <char-class>:CharClass)
        - '^' (:Type = Start) (?'.' <char-class>:CharClass)
        - '$' (:Type = End) (?'.' <char-class>:CharClass)
    ) ;
    ::AnchorExpression
}

<ratchet> { ';' ::RatchetExpression }

<special> {
    - <pass>
    - <fail>
}

<pass> { '?' ::PassExpression }

<fail> { '!' ::FailExpression }

<text> { <string> | <literal> }

<string> {
    ('[']'/?) ([^'[<][>]]+ | <char-escape>)* ('[']'/?) ;
    ::TextExpression
}

<literal> {
    [\w_]+
    ::TextExpression
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

<dotted-name> {(<name>:Parts)+ % '.' ; }

<dotted-capture-name> { '@' <dotted-name> ; }

<type-prop-name> {
    [\a_][\w_]* ;
    ('<' <type-prop-name>+ % (',' \s*) '>')? ;
}

<dotted-type-prop-name> { (<name>:Name)+ % '.' ; }

<root>";
    }
}