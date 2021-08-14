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

        public abstract IEnumerable<ExpressionResult> Run(ExpressionContext context);

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
                    - <group>
                    - <backreference>
                    - <type-set>
                    - <function>
                    - <call>
                    - <char-class>
                    - <anchor>
                    - <special>
                    - <text>
                }

                <assignent> {
                    '(:' <dotted-name> <ws> '=' <ws> <static> ')' ;
                }

                <static> {
                    - <dotted-capture-name>::Name ::StaticCapture
                    - <text>::Value ::StaticText
                }

                <subexpression> {
                    '(?'
                    <ws> <static> <ws> ;
                    (<expression> <ws>)?
                    ')' ;
                }

                <group> {
                    '(' ('>' ::AtomicExpression)? ;
                    <ws> <expression>:Expression <ws> ')' ;
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
                    (<ws> '=' <ws> <quant>:Scope)? ;
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
                        - '\\' / '\' ::TextExpression
                        - '\n' / '[n]' ::TextExpression
                        - '\t' / '[t]' ::TextExpression
                        - '\h' / ' [t]'
                        - '\s' / ' [t][n]'
                        - '\d' / '0..9'
                        - '\w' / 'A..Za..z0..9'
                    ) ;
                }

                <negative-predefined-char-class> {
                    ::CharClassExpression
                    ?:Negated
                    (
                        - '.' / '' ::AnyExpression
                        - '\N' / '[n]'
                        - '\T' / '[t]'
                        - '\H' / ' [t]'
                        - '\S' / ' [t][n]'
                        - '\D' / '0..9'
                        - '\W' / 'A..Za..z0..9'
                    ) ;
                }
                
                <literal-char-class> {
                    '[<]'
                    ('^':Negated)?
                    (
                        - <char-class-range>
                        - [^[<][>]]+
                        - <character-escape>
                        - <positive-predefined-char-class>
                    )
                    '[>]' ;
                }

                <char-class-range> {
                    [A-Za-z0-9] '..' [A-Za-z0-9] ;
                }

                <anchor> { <predefined-anchor> | <literal-anchor> }

                <predefined-anchor> {
                    [<>):start
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
                    [A..Za..z0..9_]+
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


                <ws> { (\s+ | <comment>)* ; }

                <comment> { (<block-comment> | <line-comment>) ; }

                <block-comment> { '#{' [^}]+ '}' ; }

                <line-comment> { '#' [^[n]]* ; }

                <name> { ([A..Za..z] [A..Za..z0..9]*)+ % '[-_]' ; }

                <capture-name> { '@' <name> ; }

                <dotted-name> { '@' (<name>:Parts)+ % '.' ; }

                <dotted-capture-name> { '@' <dotted-name> ; }

                <type-prop-name> {
                    [A-Za-z_][A-Za-z0-9_]+ ;
                    ('<' <type-prop-name>+ % (',' \s*) '>')? ;
                }

                <dotted-type-prop-name> { (<name>:Name)+ % '.' ; }
                
                <hex> { [0-9A-Fa-f] }
            */
            throw new NotImplementedException();
        }
    }
}
