using GenericApp.Domain.Dto.Models.Base;
using GenericApp.Infra.Common.Enums;
using System;
using System.Text.Json.Serialization;

namespace GenericApp.Domain.Dto.Models
{
    public class UserDto : EntityDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("active")]
        public bool Active { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonPropertyName("role")]
        public Role Role { get; set; }
        [JsonIgnore]
        public DateTime? PasswordValidDate { get; set; }
        public bool IsPasswordValid()
        {
            if (PasswordValidDate.HasValue)
                return ((DateTime)PasswordValidDate).Date >= DateTime.Now.Date;
            return true;
        }
    }
}
