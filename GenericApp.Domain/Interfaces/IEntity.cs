using GenericApp.Domain.Models;
using System;

namespace GenericApp.Domain.Interfaces
{
    public interface IEntity
    {
        long Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        User Creator { get; set; }
        User Updater { get; set; }
    }
}