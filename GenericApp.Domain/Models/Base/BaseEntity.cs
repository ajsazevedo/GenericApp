using Flunt.Notifications;
using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Domain.Models.Base
{
    public abstract class BaseEntity<TKeyType> : /*Notifiable<Notification>,*/ IEntity<TKeyType>
    {
        protected BaseEntity(TKeyType id = default)
        {
            Id = id;
        }
        [Key]
        public virtual TKeyType Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Creator { get; set; }
        public string Updater { get; set; }

        public Type GetIdType()
        {
            return typeof(TKeyType);
        }
    }
}
