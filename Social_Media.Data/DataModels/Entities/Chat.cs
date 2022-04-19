using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Social_Media.Data.DataModels.Entities.Interfaces;
using Social_Media.Data.DataModels.Entities_Identity;

namespace Social_Media.Data.DataModels.Entities
{
    public class Chat : IEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string CreaterId { get; set; }
        [NotMapped]
        public User Creater { get; set; }
        public Guid? PostId { get; set;}
        public bool IsOnlyFriends { get; set; }
        [NotMapped]
        public Post Post { get; set; }
        public List<Massage> UserMassage { get; set; }
    }
}
