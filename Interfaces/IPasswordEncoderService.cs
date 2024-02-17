namespace SorokChatServer.Interfaces
{
    public interface IPasswordEncoderService
    {
        public string Encode(string password);
        public bool Verify(string password, string encodedPassword);
    }
}
