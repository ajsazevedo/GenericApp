namespace GenericApp.Domain.Dto.Request
{
    public class CredencialsDto
    {
        public CredencialsDto(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; set; }
        public string Password { get; set; }
    }
}
