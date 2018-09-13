using System;
using System.Reflection;

namespace UI.RunnerManagement.Helpers
{
    public static class ILMemberAccessor
    {
        public static Func<object, TRet> GetPrivateGetPropertyGeneric<TRet>(Type classType, string propertyName)
        {
            return GetGetPropertyGenericMethod<TRet>(classType, classType.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance));
        }

        public static Func<object, TRet> GetGetPropertyGenericMethod<TRet>(Type classType, PropertyInfo propertyInfo)
        {
            MethodInfo getMethod = propertyInfo.GetGetMethod(true);
            Type concreteFuncType = typeof(Func<,>).MakeGenericType(classType, getMethod.ReturnType);
            Delegate getDelegate = Delegate.CreateDelegate(concreteFuncType, getMethod);
            MethodInfo castMethod = typeof(ILMemberAccessor).GetMethod("CastDelegateToGenericFunc", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(classType, getMethod.ReturnType);
            return (Func<object, TRet>)castMethod.Invoke(null, new object[] { getDelegate });
        }
    }
}
