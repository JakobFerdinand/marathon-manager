using Core.Models;

namespace System
{
    public static partial class RunnerExtensions
    {
        public static bool IsValid(this Runner @this)
            => @this is null
            ? true
            : !@this.Firstname.IsNullOrEmpty()
              && !@this.Lastname.IsNullOrEmpty()
              && (@this.Category != null || @this.CategoryId > 0);
    }
}
