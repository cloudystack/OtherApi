using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.OtherApi.DataAccess.Core.Models
{
    public class AppSetting : EntityBase
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Value { get; set; }

        public static class Factory
        {
            public static AppSetting Create(string id, string value, string createdBy)
            {
                var entity = new AppSetting()
                {
                    Id = id,
                    Value = value,
                    CreatedBy = createdBy
                };

                return entity;
            }
        }
    }
}
