using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.OtherApi.DataAccess.Core.Models
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.Status = EntityStatus.New;
        }

        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public string CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public EntityStatus Status { get; set; }

        public void Update(string updatedBy)
        {
            this.UpdatedBy = updatedBy;
            this.UpdatedOn = DateTime.UtcNow;
        }

        public void Delete()
        {
            this.Status = EntityStatus.Deleted;
        }
    }

    public enum EntityStatus
    {
        New,
        Active,
        Inactive,
        Deleted
    }
}
