using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Social_Media.Data.Models.Entities.Interfaces;
using Social_Media.Data.Models.Entities_Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Entities
{
    public class Chat : IEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CreaterId { get; set; }
        [NotMapped]
        public User Creater { get; set; }
        [NotMapped]
        public List<User> Members { get; set; }
        public Guid PostId { get; set;}
        public Post Post { get; set; }
        public List<Massage> UserMassage { get; set; }
    }
}
