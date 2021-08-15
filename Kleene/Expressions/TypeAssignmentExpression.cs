using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class TypeAssignmentExpression : Expression
    {
        public string TypeName { get; }
        public IEnumerable<TypeAssignmentProperty> Properties { get; }

        public TypeAssignmentExpression(string typeName, IEnumerable<TypeAssignmentProperty> properties)
        {
            TypeName = typeName;
            Properties = properties;
        }

        public override IEnumerable<ExpressionResult> RunInternal(ExpressionContext context)
        {
            context.CaptureTree.Open("!T");
            context.CaptureTree.Set("FullName", new(TypeName));
            context.CaptureTree.Open("Properties");
            foreach (var property in Properties.OrderBy(x => x.Name))
            {
                ExpressionResult? value = property.Value.GetValue(context);
                if (value is not null)
                {
                    context.CaptureTree.Set(property.Name, value);
                }
            }
            context.CaptureTree.Close("!T", new());

            yield return new();

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
            context.CaptureTree.Unset("FullName");
            context.CaptureTree.Unopen("!T");
        }
    }
}
