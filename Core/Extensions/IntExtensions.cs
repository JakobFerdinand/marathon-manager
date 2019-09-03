namespace System
{
    public static class IntExtensions
    {
        public static TimeSpan Seconds(this int @this) => TimeSpan.FromSeconds(@this);
    }
}
