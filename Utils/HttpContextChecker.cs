using SorokChatServer.Exceptions;

namespace SorokChatServer.Utils
{
    public static class HttpContextChecker
    {
        public static void Check(IHttpContextAccessor context)
        {
            if (context == null || context.HttpContext == null)
            {
                throw new ServerException($"{nameof(context)} is null");
            }
        }
    }
}
