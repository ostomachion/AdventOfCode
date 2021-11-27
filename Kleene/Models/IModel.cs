using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Kleene.Tests")]

namespace Kleene.Models;

public interface IModel<out T>
{
    T Convert();
}
