namespace SorokChatServer.Exceptions
{
    public class UnauthorizationException : Exception
    {
        public UnauthorizationException() : base("Unauthorizated") { }

        public UnauthorizationException(string message) : base(message) { }
    }
}
