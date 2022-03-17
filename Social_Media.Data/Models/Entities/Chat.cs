using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social_Media.Data.Models.Entities.Interfaces;
using Social_Media.Data.Models.Entities_Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections;

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
        [NotMapped]
        public User Creater { get; set; }
        [NotMapped]
        [Column("Members")]
        public List<User> Members { get; set; }
        public Guid PostId { get; set;}
        public Post Post { get; set; }
        public List<Massage> UserMassage { get; set; }
    }
}
