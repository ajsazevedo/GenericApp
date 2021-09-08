namespace GenericApp.Domain.Dto.Request
{
    public class CredentialsDto
    {
        public CredentialsDto(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
