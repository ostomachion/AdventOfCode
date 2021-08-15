using System.Linq;
using System;
using Xunit;

namespace Kleene.Tests
{
    public class CallExpressionTests
    {
        [Fact]
        public void Match()
        {
            // Given
            var expression = new ConcatExpression(new Expression[] {
                new FunctionExpression("foo", new TextExpression("x")),
                new CallExpression("foo")
            });

            // When
            var result = expression.Transform("x");

            // Then
            Assert.Equal("x", result);
        }

        [Fact]
        public void Mismatch()
        {
            // Given
            var expression = new ConcatExpression(new Expression[] {
                new FunctionExpression("foo", new TextExpression("x")),
                new CallExpression("foo")
            });

            // When
            var result = expression.Transform("y");

            // Then
            Assert.Null(result);
        }

        [Fact]
        public void Undefined()
        {
            // Given
            var expression = new ConcatExpression(new Expression[] {
                new FunctionExpression("foo", new TextExpression("x")),
                new CallExpression("bar")
            });

            // When
            var result = expression.Transform("x");

            // Then
            Assert.Null(result);
        }

        [Fact]
        public void Shadow()
        {
            // Given
            var expression = new ConcatExpression(new Expression[] {
                new FunctionExpression("foo", new TextExpression("x")),
                new FunctionExpression("foo", new TextExpression("y")),
                new CallExpression("foo")
            });

            // When
            var result = expression.Transform("y");

            // Then
            Assert.Equal("y", result);
        }

        [Fact]
        public void Capture()
        {
            // Given
            var expression = new ConcatExpression(new Expression[] {
                new FunctionExpression("foo", new TextExpression("x")),
                new CallExpression("foo", "bar"),
                new BackreferenceExpression("bar")
            });

            // When
            var result = expression.Transform("xx");

            // Then
            Assert.Equal("xx", result);
        }

        [Fact]
        public void Subcapture()
        {
            // Given
            var expression = new ConcatExpression(new Expression[] {
                new FunctionExpression("foo", new AssignmentExpression("baz", new TextExpression("x"))),
                new CallExpression("foo", "bar"),
                new BackreferenceExpression("bar.baz")
            });

            // When
            var result = expression.Transform("x");

            // Then
            Assert.Equal("x", result);
        }

        [Fact]
        public void BlockedSubcapture()
        {
            // Given
            var expression = new ConcatExpression(new Expression[] {
                new FunctionExpression("foo", new AssignmentExpression("bar", new TextExpression("x"))),
                new CallExpression("foo"),
                new BackreferenceExpression("bar")
            });

            // When
            var result = expression.Transform("x");

            // Then
            Assert.Null(result);
        }

        [Fact]
        public void BlockOuterCapture()
        {
            // Given
            var expression = new ConcatExpression(new Expression[] {
                new AssignmentExpression("bar", new TextExpression("x")),
                new FunctionExpression("foo", new BackreferenceExpression("bar")),
                new CallExpression("foo")
            });

            // When
            var result = expression.Transform("x");

            // Then
            Assert.Null(result);
        }

        [Fact]
        public void BlockOuterCaptureWithCallCapture()
        {
            // Given
            var expression = new ConcatExpression(new Expression[] {
                new AssignmentExpression("bar", new TextExpression("x")),
                new FunctionExpression("foo", new BackreferenceExpression("bar")),
                new CallExpression("foo", "baz")
            });

            // When
            var result = expression.Transform("x");

            // Then
            Assert.Null(result);
        }
    }
}
