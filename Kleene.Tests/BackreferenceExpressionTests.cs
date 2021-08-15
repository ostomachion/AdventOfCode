using System.Linq;
using System;
using Xunit;

namespace Kleene.Tests
{
    public class BackreferenceExpressionTests
    {
        [Fact]
        public void Match()
        {
            // Given
            var expression = new ConcatExpression(new Expression[]
            {
                new CaptureExpression("foo", new TextExpression("x")),
                new BackreferenceExpression("foo")
            });

            // When
            var result = expression.Transform("xx");

            // Then
            Assert.Equal("xx", result);
        }

        [Fact]
        public void Mismatch()
        {
            // Given
            var expression = new ConcatExpression(new Expression[]
            {
                new CaptureExpression("foo", new TextExpression("x")),
                new BackreferenceExpression("foo")
            });

            // When
            var result = expression.Transform("xy");

            // Then
            Assert.Null(result);
        }

        [Fact]
        public void Undefined()
        {
            // Given
            var expression = new ConcatExpression(new Expression[]
            {
                new CaptureExpression("foo", new TextExpression("x")),
                new BackreferenceExpression("bar")
            });

            // When
            var result = expression.Transform("xx");

            // Then
            Assert.Null(result);
        }

        [Fact]
        public void Shadow()
        {
            // Given
            var expression = new ConcatExpression(new Expression[]
            {
                new CaptureExpression("foo", new TextExpression("x")),
                new CaptureExpression("foo", new TextExpression("y")),
                new BackreferenceExpression("foo")
            });

            // When
            var result = expression.Transform("xyy");

            // Then
            Assert.Equal("xyy", result);
        }

        [Fact]
        public void Nested()
        {
            // Given
            var expression = new ConcatExpression(new Expression[]
            {
                new CaptureExpression("foo", new CaptureExpression("bar", new TextExpression("x"))),
                new BackreferenceExpression("foo.bar")
            });

            // When
            var result = expression.Transform("xx");

            // Then
            Assert.Equal("xx", result);
        }


        [Fact]
        public void Ancestor()
        {
            // Given
            var expression = new ConcatExpression(new Expression[]
            {
                new AssignmentExpression("foo", "x"),
                new CaptureExpression("foo", new CaptureExpression("bar", new TextExpression("x"))),
                new BackreferenceExpression("foo.bar")
            });

            // When
            var result = expression.Transform("xx");

            // Then
            Assert.Equal("xx", result);
        }
    }
}
