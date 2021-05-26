using System;

namespace GenericApp.Domain.Models.Base
{
    public interface IEntity<TKeyType>
    {
        TKeyType Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        string Creator { get; set; }
        string Updater { get; set; }
    }
}