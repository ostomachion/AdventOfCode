using System.Reflection;

namespace Kleene;

public class CaptureTreeNode
{
    public CaptureTree Tree { get; }
    public string Name { get; set;  }
    public CaptureTreeNode? Parent { get; set; }
    private readonly Stack<CaptureTreeNode> children = new();
    public ExpressionResult? Value { get; set; }
    public IEnumerable<CaptureTreeNode> Children => children;
    public bool IsOpen { get; set; }
    public bool IsFunctionBoundary { get; set; }
    public bool IsPropertyBoundary => Name != "" && Char.IsUpper(Name[0]);

    public IEnumerable<CaptureTreeNode> this[CaptureName? name]
    {
        get
        {
            if (name is null)
            {
                return new[] { this };
            }

            var head = children.Where(x => x.Name == name.Head);
            return head.SelectMany(x => x[name.Tail]);
        }
    }

    public CaptureTreeNode(CaptureTree tree, string name)
    {
        Tree = tree;
        Name = name;
    }

    public void Add(CaptureTreeNode node)
    {
        node.Parent = this;
        children.Push(node);
    }

    public void Unadd()
    {
        var node = children.Pop();
        node.Parent = null;
    }
    public IEnumerable<CaptureTreeNode> GetPropertyNodes()
    {
        foreach (var child in Children.Reverse())
        {
            if (child.IsPropertyBoundary)
            {
                yield return child;
            }
            else if (child.Name != "!T")
            {
                foreach (var node in child.GetPropertyNodes())
                {
                    yield return node;
                }
            }
        }
    }

    public T? Parse<T>()
    {
        T? value = default;
        object? obj = value;
        Parse(typeof(T), ref obj);
        return (T?)obj;
    }

    public void Parse(Type type, ref object? value)
    {
        // Reflection code is never pretty.
        
        if (Parent is not null && !IsPropertyBoundary)
            throw new InvalidOperationException();

        if (Value is null)
             throw new InvalidOperationException();

        var text = Value.Output;

        var typeNode = GetTypeAssignmentNode();
        type = typeNode is null ? type : Type.GetType(typeNode["FullName"].First().Value!.Output) ?? throw new Exception($"The type name '{typeNode["FullName"].First().Value!.Output}' could not be found.");

        if (type.IsAssignableFrom(typeof(string)))
        {
            value = text;
        }
        else if (type.IsAssignableFrom(typeof(bool)))
        {
            value = true;
        }
        else if (type.GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly,
                new[] { typeof(string) }) is MethodInfo implicitCast && implicitCast.ReturnType == type)
        {
            try
            {
                value = implicitCast.Invoke(null, new object[] { text })!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not parse '{text}' as {type.FullName}.", ex);
            }
        }
        else if (type.GetMethod("op_Explicit", BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly,
                new[] { typeof(string) }) is MethodInfo explicitCast && explicitCast.ReturnType == type)
        {
            try
            {
                value = explicitCast.Invoke(null, new object[] { text })!;
            }
            catch
            {
                throw new Exception($"Could not parse '{text}' as {type.FullName}.");
            }
        }
        else if (type.IsEnum)
        {
            if (Enum.TryParse(type, text, out var enumValue))
            {
                value = enumValue!;
            }
            else if (Enum.GetUnderlyingType(type) == typeof(byte))
            {
                if (byte.TryParse(text, out var v))
                    value = Enum.ToObject(type, v);
                else
                    throw new Exception($"Could not parse '{text}' as {type.FullName}.");
            }
            else if (Enum.GetUnderlyingType(type) == typeof(sbyte))
            {
                if (sbyte.TryParse(text, out var v))
                    value = Enum.ToObject(type, v);
                else
                    throw new Exception($"Could not parse '{text}' as {type.FullName}.");
            }
            else if (Enum.GetUnderlyingType(type) == typeof(short))
            {
                if (short.TryParse(text, out var v))
                    value = Enum.ToObject(type, v);
                else
                    throw new Exception($"Could not parse '{text}' as {type.FullName}.");
            }
            else if (Enum.GetUnderlyingType(type) == typeof(ushort))
            {
                if (ushort.TryParse(text, out var v))
                    value = Enum.ToObject(type, v);
                else
                    throw new Exception($"Could not parse '{text}' as {type.FullName}.");
            }
            else if (Enum.GetUnderlyingType(type) == typeof(int))
            {
                if (int.TryParse(text, out var v))
                    value = Enum.ToObject(type, v);
                else
                    throw new Exception($"Could not parse '{text}' as {type.FullName}.");
            }
            else if (Enum.GetUnderlyingType(type) == typeof(uint))
            {
                if (uint.TryParse(text, out var v))
                    value = Enum.ToObject(type, v);
                else
                    throw new Exception($"Could not parse '{text}' as {type.FullName}.");
            }
            else if (Enum.GetUnderlyingType(type) == typeof(long))
            {
                if (long.TryParse(text, out var v))
                    value = Enum.ToObject(type, v);
                else
                    throw new Exception($"Could not parse '{text}' as {type.FullName}.");
            }
            else if (Enum.GetUnderlyingType(type) == typeof(ulong))
            {
                if (ulong.TryParse(text, out var v))
                    value = Enum.ToObject(type, v);
                else
                    throw new Exception($"Could not parse '{text}' as {type.FullName}.");
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        else if (type.GetMethod("TryParse", BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly,
                new[] { typeof(string), type.MakeByRefType() }) is MethodInfo tryParse && tryParse.ReturnType == typeof(bool))
        {
            var parameters = new object?[] { text, null };
            if ((bool)tryParse.Invoke(null, parameters)!)
            {
                value = parameters[1]!;
            }
            else
            {
                throw new Exception($"Could not parse '{text}' as {type.FullName}.");
            }
        }
        else if (type != typeof(Expression) /* TODO: Remove this */ && type.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly,
                new[] { typeof(string) }) is MethodInfo parse && parse.ReturnType == type)
        {
            try
            {
                value = parse.Invoke(null, new object[] { text })!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not parse '{text}' as {type.FullName}.", ex);
            }
        }
        else
        {
            try
            {
                value = Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not parse '{text}' as {type.FullName}.", ex);
            }
        }

        // These two loops will do some extra work if there are shadowed captures for scalar properties.
        if (typeNode is not null)
        {
            foreach (var propertyNode in typeNode["Properties"].First().Children)
            {
                SetPropertyValue(propertyNode, type, ref value);
            }
        }

        foreach (var propertyNode in GetPropertyNodes())
        {
            SetPropertyValue(propertyNode, type, ref value);
        }

        static void SetPropertyValue(CaptureTreeNode propertyNode, Type type, ref object value)
        {
            var property = type.GetProperty(propertyNode.Name);
            if (property is null)
                throw new Exception($"Type {type.FullName} does not contain a public property '{propertyNode.Name}'.");

            var propertyValue = property.GetValue(value);

            var ienumerable = property.PropertyType.GetInterfaces().Concat(new [] { property.PropertyType }).FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            var nullable = property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
            MethodInfo? addMethod = null;
            if (ienumerable is not null)
            {
                var itemType = ienumerable?.GetGenericArguments()[0]!;
                var listType = typeof(List<>).MakeGenericType(itemType);

                if (property.PropertyType.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance, new[] { itemType! }) is MethodInfo add
                    && property.PropertyType.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, Array.Empty<Type>()) is ConstructorInfo ctor)
                {
                    if (property.GetValue(value) is null)
                        property.SetValue(value, ctor.Invoke(Array.Empty<object>()));
                    addMethod = add;

                }
                else if (property.PropertyType.IsAssignableFrom(listType))
                {
                    var propertyType = propertyValue?.GetType()!;
                    addMethod = listType.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance, new[] { itemType! })!;
                    if (propertyValue is null)
                    {
                        property.SetValue(value, listType.GetConstructor(Array.Empty<Type>())!.Invoke(Array.Empty<object>()));
                    }
                    else if (!propertyType.IsGenericType || propertyType.GetGenericTypeDefinition() != typeof(List<>))
                    {
                        var list = listType.GetConstructor(Array.Empty<Type>())!.Invoke(Array.Empty<object>());
                        foreach (object item in (IEnumerable)propertyValue)
                        {
                            addMethod.Invoke(list, new[] { item });
                        }
                        property.SetValue(value, list);
                    }
                }
                // TODO: Assignable from array type = itemType.MakeArrayType()
            }

            var parseType = nullable ? property.PropertyType.GetGenericArguments()[0] : property.PropertyType;
            propertyNode.Parse(parseType, ref propertyValue);
            try
            {
                if (addMethod is null)
                {
                    property.SetValue(value, propertyValue);
                }
                else
                {
                    addMethod.Invoke(property.GetValue(value), new[] { propertyValue });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not set property '{propertyNode.Name}' on type {type.FullName}.", ex);
            }
        }
    }

    public CaptureTreeNode? GetTypeAssignmentNode(bool lookUp = true)
    {
        if (Name == "!T")
            return this;

        foreach (var node in Children.Where(x => !x.IsPropertyBoundary))
        {
            if (node.GetTypeAssignmentNode(lookUp: false) is CaptureTreeNode value)
                return value;
        }

        if (this.IsPropertyBoundary || this.Parent is null)
            return null;

        return lookUp ? Parent.GetTypeAssignmentNode() : null;
    }
}
