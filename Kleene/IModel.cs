using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Kleene.Tests")]

namespace Kleene;

internal interface IModel<out T>
{
    T Convert();
}
