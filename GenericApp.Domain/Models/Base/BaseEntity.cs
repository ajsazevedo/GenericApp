using GenericApp.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Domain.Models.Base
{
    public abstract class BaseEntity : IEntity
    {
        protected BaseEntity(long id = default)
        {
            Id = id;
        }
        [Key]
        public virtual long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User Creator { get; set; }
        public User Updater { get; set; }
    }
}
