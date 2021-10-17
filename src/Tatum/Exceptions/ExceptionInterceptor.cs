using AspectInjector.Broker;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace TatumPlatform
{
    [Aspect(Scope.Global)]
    [Injection(typeof(LogCall))]
    public class LogCall : Attribute
    {
        [Advice(Kind.Before)] // you can have also After (async-aware), and Around(Wrap/Instead) kinds
        public void LogEnter([Argument(Source.Name)] string name)
        {
            Console.WriteLine($"Calling '{name}' method...");   //you can debug it	
        }
    }

    [Injection(typeof(CatchErrorsAttribute))]
    [Aspect(Scope.Global)]
    public class CatchErrorsAttribute : Attribute
    {
        private static MethodInfo _asyncErrorHandler = typeof(CatchErrorsAttribute).GetMethod(nameof(CatchErrorsAttribute.WrapAsync), BindingFlags.NonPublic | BindingFlags.Static);
        [Advice(Kind.Around)]
        public object Around([Argument(Source.Arguments)] object[] args,
            [Argument(Source.Target)] Func<object[], object> target,
            [Argument(Source.ReturnType)] Type retType)
        {
            try
            {
                if (typeof(Task).IsAssignableFrom(retType))
                {
                    var syncResultType = retType.IsConstructedGenericType ? retType.GenericTypeArguments[0] : typeof(object);
                    return _asyncErrorHandler.MakeGenericMethod(syncResultType).Invoke(this, new object[] { target, args });
                }
                else
                {
                    return target(args);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static async Task<T> WrapAsync<T>(Func<object[], object> target, object[] args)
        {
            try
            {
                return await (Task<T>)target(args);
            }
            catch (TatumException ex)
            {
                Console.WriteLine(ex.HttpStatusCode.ToString() + ex.Message);
                return default(T);
            }
        }
    }
}
