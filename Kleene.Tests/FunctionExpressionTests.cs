using System.Linq;
using Xunit;

namespace Kleene.Tests
{
    public class FunctionExpressionTests
    {
        [Fact]
        public void Match()
        {
            // Given
            var expression = new FunctionExpression("foo", new TextExpression("bar"));

            // When
            var context = new ExpressionContext("");
            var result = expression.Run(context).FirstOrDefault();

            // Then
            Assert.Equal("", result?.Input);
            Assert.Equal("", result?.Output);
            Assert.Collection(context.FunctionList.OrderBy(x => x.Key),
                item =>
                {
                    Assert.Equal("foo", item.Key);
                    Assert.IsType<TextExpression>(item.Value);
                    Assert.Equal("bar", (item.Value as TextExpression)!.Value)
                    ;
                }
            );
        }
    }
}
