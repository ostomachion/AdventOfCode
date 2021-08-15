using System;
using System.Linq;
using System.Collections.Generic;

namespace Kleene
{
    public abstract class Expression
    {
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
            /*
                <root> { <ws> <expression> <ws> }

                <expression> { <bullet-alt> | <trans> }

                <bullet-alt> {
                    ('-' <trans>::Items)+
                    ::AltExpression
                }

                <trans> {
                    <alt>:Input ;
                    (<ws> '/' <ws> <alt>:Output ::TransformExpression)? ;
                }

                <alt> {
                    <concat>:Items+ % (<ws> '|' <ws> ::AltExpression) ;
                }

                <concat> {
                    <capture>:Items+ % (<ws> ::ConcatExpression) ;
                }

                <capture> {
                    # Special case. Captures on calls have to be handled by the call expression itself.
                    <call>:expression
                    (=expression
                        (':' <dotted-name>:CaptureName ::CaptureExpression)? ;
                        ::CallExpression
                    )
                    |
                    <req>:Expression ;
                    (':' <dotted-name>:Name ::CaptureExpression)? ;
                }

                <req> {
                    <quant>:Expression ;
                    ('!' ::ReqExpression)? ;
                }
                
                <quant> {
                    <item>:Expression ;
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
                    '(:' <dotted-name> <ws> '=' <ws> <static> ')' ;
                }

                <subexpression> {
                    '(?'
                    <ws> <static> <ws> ;
                    (<expression> <ws>)?
                    ')' ;
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
                    ':' <type-prop-name>:Name ;
                    (
                        <ws> '{' <ws>
                        ((<type-prop-name>:Name <ws> '=' <ws> <static>:Value):Properties)* % (',' <ws>)
                        <ws> '}'
                    )? ;
                    ::TypeSetExpression
                }

                <function> {
                    '<' <name>:Name '>' <ws> '{' <ws> <expression Expression>  <ws> '}' ;
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
                    ::CharClassExpression
                    (
                        - '\\' / '\'
                        - '\n' / '[n]'
                        - '\t' / '[t]'
                        - '\h' / ' [t]'
                        - '\s' / ' [t][n]'
                        - '\d' / '0123456789'
                        - '\u' / 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'
                        - '\l' / 'abcdefghijklmnopqrstuvwxyz'
                        - '\a' / 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'
                        - '\w' / 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789'
                    ) ;
                }

                <negative-predefined-char-class> {
                    ::CharClassExpression
                    ?:Negated
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
                        - '\W' / 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789
                    ) ;
                }
                
                <literal-char-class> {
                    '[<]'
                    ('^':Negated)?
                    (
                        - <character-escape>
                        - <positive-predefined-char-class>
                        - [^[<][>]\\]+
                    )
                    '[>]' ;
                }

                <anchor> { <predefined-anchor> | <literal-anchor> }

                <predefined-anchor> {
                    [<>]:start
                    ('!':Negated)
                    (
                        - <char-class>:CharClass
                        - (?'\w' <char-class>:CharClass) # Default char-class to \w
                    )
                    [<>]:end

                    (
                        - (?@start = '<') (?@end = '<') (:Type = Start)
                        - (?@start = '>') (?@end = '>') (:Type = End)
                        - (?@start = '<') (?@end = '>') (:Type = Outer)
                        - (?@start = '>') (?@end = '<') (:Type = Inner)
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

                <character-escape> {
                    ('[<]'/?)
                    (
                        - ('[']')
                        - ('<'/'[<]')
                        - ('>'/'[>]')
                        - ('n'/'[n]')
                        - ('r'/'[r]')
                        - ('t'/'[t]')
                        # - <UnicodeEscapes> # [U+0000]
                    )
                    ('[>]'/?);
                }

                <static> {
                    - <dotted-capture-name>::Name ::StaticCapture
                    - <text>::Value ::StaticText
                }


                <ws> { (\s+ | <comment>)* ; }

                <comment> { (<block-comment> | <line-comment>) ; }

                <block-comment> { '#{' [^}]+ '}' ; }

                <line-comment> { '#' [^[n]]* ; }

                <name> { (\a \w*)+ % '[-_]' ; }

                <capture-name> { '@' <name> ; }

                <dotted-name> { '@' (<name>:Parts)+ % '.' ; }

                <dotted-capture-name> { '@' <dotted-name> ; }

                <type-prop-name> {
                    [\a_][\w_]+ ;
                    ('<' <type-prop-name>+ % (',' \s*) '>')? ;
                }

                <dotted-type-prop-name> { (<name>:Name)+ % '.' ; }
                
                <hex> { [\dABCDEFabcdef] }
            */
            throw new NotImplementedException();
        }
    }
}
