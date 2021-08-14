using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class TypeAssignmentExpression : Expression
    {
        public string TypeName { get; }
        public IEnumerable<TypeAssignmentProperty> Properties { get; }
        public CaptureName? Scope { get; }

        public TypeAssignmentExpression(string typeName, IEnumerable<TypeAssignmentProperty> properties, CaptureName? scope = null)
        {
            TypeName = typeName;
            Properties = properties;
            Scope = scope;
        }

        public override IEnumerable<ExpressionResult> Run(ExpressionContext context)
        {
            context.CaptureTree.Open("!T");
            context.CaptureTree.Set("FullName", new(TypeName));
            if (Scope is not null)
                context.CaptureTree.Set("Scope", new(Scope.ToString()!));
            context.CaptureTree.Open("Properties");
            foreach (var property in Properties.OrderBy(x => x.Name))
            {
                ExpressionResult? value = property.Value.GetValue(context);
                if (value is not null)
                {
                    context.CaptureTree.Set(property.Name, value);
                }
            }
            context.CaptureTree.Close("!T", new ExpressionResult(""));

            yield return new("");

            context.CaptureTree.Unclose("!T");
            foreach (var property in Properties.OrderByDescending(x => x.Name))
            {
                ExpressionResult? value = property.Value.GetValue(context);
                if (value is not null)
                {
                    context.CaptureTree.Unset(property.Name);
                }
            }
            context.CaptureTree.Unopen("Properties");
            if (Scope is not null)
                context.CaptureTree.Unset("Scope");
            context.CaptureTree.Unset("FullName");
            context.CaptureTree.Unopen("!T");
        }
    }
}
