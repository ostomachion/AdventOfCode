namespace AdventOfCode.Helpers.Extensions
{
    public static class BooleanExtensions
    {
        public static void Set(this ref bool value) => value = true;
        public static void Unset(this ref bool value) => value = false;
        public static void Toggle(this ref bool value) => value = !value;
    }
}
