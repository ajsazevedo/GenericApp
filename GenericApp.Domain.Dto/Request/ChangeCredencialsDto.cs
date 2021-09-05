namespace GenericApp.Domain.Dto.Request
{
    public class ChangeCredencialsDto : CredencialsDto
    {
        public ChangeCredencialsDto(string login, string password) : base(login, password)
        {
        }

        public string NewPassword { get; set; }
    }
}
