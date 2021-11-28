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

    public T Parse<T>() => (T)Parse(typeof(T));

    public object Parse(Type type)
    {
        // Reflection code is never pretty.
        
        if (Parent is not null && !IsPropertyBoundary)
            throw new InvalidOperationException();

        if (Value is null)
             throw new InvalidOperationException();

        var text = Value.Output;

        var typeNode = GetTypeAssignmentNode();
        type = typeNode is null ? type : Type.GetType(typeNode["FullName"].First().Value!.Output) ?? throw new Exception($"The type name '{typeNode["FullName"].First().Value!.Output}' could not be found.");

        // TODO: Get properties to set.
        Dictionary<string, dynamic> properties = new();
        if (typeNode is not null)
        {
            foreach (var propertyNode in typeNode["Properties"].First().Children)
            {
                if (propertyNode.Name == "NamespaceName") ;
                var propValue = GetPropertyValue(propertyNode, type, out Type? collectionType);
                if (properties.ContainsKey(propertyNode.Name))
                {
                    if (collectionType is null)
                    {
                        properties[propertyNode.Name] = propValue;
                    }
                    else
                    {
                        properties[propertyNode.Name].Add(propValue);
                    }
                }
                else
                {
                    if (collectionType is null)
                    {
                        properties.Add(propertyNode.Name, propValue);
                    }
                    else
                    {
                        dynamic list = Activator.CreateInstance(typeof(List<>).MakeGenericType(collectionType))!;
                        list.Add(propValue);
                        properties.Add(propertyNode.Name, list);
                    }
                }
            }
        }

        List<string> set = new();
        foreach (var propertyNode in GetPropertyNodes())
        {
            var propValue = GetPropertyValue(propertyNode, type, out var collectionType);
            if (properties.ContainsKey(propertyNode.Name))
            {
                if (collectionType is null)
                {
                    properties[propertyNode.Name] = propValue;
                }
                else
                {
                    var list = properties[propertyNode.Name];
                    if (!set.Contains(propertyNode.Name))
                        list.Clear();
                    list.GetType().GetMethod("Add")!.Invoke(list, new[] { propValue });
                }
            }
            else
            {
                if (collectionType is null)
                {
                    properties.Add(propertyNode.Name, propValue);
                }
                else
                {
                    dynamic list = Activator.CreateInstance(typeof(List<>).MakeGenericType(collectionType))!;
                    list.GetType().GetMethod("Add")!.Invoke(list, new [] { propValue });
                    properties.Add(propertyNode.Name, list);
                }
            }
            set.Add(propertyNode.Name);
        }

        object value;
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
        else if (type.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly,
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
            value = null!;
            var ctors = type.GetConstructors().Select(x => (Ctor: x, Params: x.GetParameters())).OrderByDescending(x => x.Params.Length);
            foreach (var (ctor, parameters) in ctors)
            {
                if (type == typeof(CharacterClass)) ;
                List<KeyValuePair<(string? PropName, string ParamName), object>> args = new();
                foreach (var p in parameters)
                {
                    var prop = properties.Keys.FirstOrDefault(x =>
                        !args.Any(y => y.Key.PropName == x) &&
                        x.Equals(p.Name, StringComparison.OrdinalIgnoreCase) &&
                        (
                            p.ParameterType.IsAssignableFrom(properties[x].GetType()) ||
                            p.ParameterType.IsArray && properties[x].GetType().IsGenericType &&
                            properties[x].GetType().GetGenericTypeDefinition() == typeof(List<>) &&
                            p.ParameterType.GetElementType()!.IsAssignableFrom(properties[x].GetType().GetGenericArguments()[0])
                        ));

                    var isParams = p.GetCustomAttributes(typeof(ParamArrayAttribute), false).Any();
                    var isBool = p.ParameterType == typeof(bool) && type.GetProperties().Any(x => x.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase));
                    var isNullable = new NullabilityInfoContext().Create(p).WriteState == NullabilityState.Nullable;
                    if (!p.HasDefaultValue && !(isParams || isBool || isNullable) && prop is null)
                        break;

                    args.Add(new((prop, p.Name), prop is null ?
                        (
                            isParams ? Array.CreateInstance(p.ParameterType.GetElementType()!, 0) :
                            isBool ? false :
                            isNullable ? null :
                            p.DefaultValue
                        ) :
                        p.ParameterType.IsArray ? properties[prop].ToArray() :
                        properties[prop]));
                        
                }
                if (args.Count == parameters.Length)
                {
                    value = ctor.Invoke(args.Select(x => x.Value).ToArray());
                    foreach (var p in args.Where(x => x.Key.PropName is not null))
                        properties.Remove(p.Key.PropName!);
                    break;
                }
            }

            if (value is null)
                throw new Exception($"Could not create a value of type {type.FullName}.");
        }

        foreach (var prop in properties)
        {
            type.GetProperty(prop.Key)!.SetValue(value, prop.Value);
        }

        return value;

        static object GetPropertyValue(CaptureTreeNode propertyNode, Type type, out Type? collectionType)
        {
            var property = type.GetProperty(propertyNode.Name);
            if (property is null)
                throw new Exception($"Type {type.FullName} does not contain a public property '{propertyNode.Name}'.");

            var ienumerable = property.PropertyType.GetInterfaces().Concat(new [] { property.PropertyType })
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            collectionType = property.PropertyType.IsAssignableFrom(typeof(string)) ? null : ienumerable?.GetGenericArguments()[0];

            var nullable = property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
            var parseType = nullable ? property.PropertyType.GetGenericArguments()[0] : property.PropertyType;

            return propertyNode.Parse(parseType);
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
