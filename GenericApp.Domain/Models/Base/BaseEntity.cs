using GenericApp.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericApp.Domain.Models.Base
{
    public abstract class BaseEntity : IEntity
    {
        protected BaseEntity(long id = default)
        {
            Id = id;
        }
        [Column("id"), Key]
        public virtual long Id { get; set; }
        [Column("created_at"), Required]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("creator_id"), Required]
        public User Creator { get; set; }
        [ForeignKey("updater_id")]
        public User Updater { get; set; }
    }
}
