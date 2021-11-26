namespace Kleene;

internal interface IModel<out T>
{
    T Convert();
}
