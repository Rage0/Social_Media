using Social_Media.Data.DataModels.Entities.Interfaces;
using Social_Media.Data.DataModels.Entities_Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.DataModels.Entities
{
    public class PrivateChat : IEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public List<User> Members { get; set; }
        public List<Massage> Massages { get; set; }
    }
}
