using System;
using System.Text.Json.Serialization;

namespace GenericApp.Domain.Dto.Models.Base
{
    public abstract class EntityDto
    {
        [JsonIgnore]
        public long Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual UserDto Creator { get; set; }
        [JsonIgnore]
        public virtual UserDto Updater { get; set; }
    }
}
