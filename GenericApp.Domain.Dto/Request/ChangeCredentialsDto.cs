namespace GenericApp.Domain.Dto.Request
{
    public class ChangeCredentialsDto : CredentialsDto
    {
        public ChangeCredentialsDto(string username, string password) : base(username, password)
        {
        }

        public string NewPassword { get; set; }
    }
}
