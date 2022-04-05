using Social_Media.Data.Models.Entities.Interfaces;
using Social_Media.Data.Models.Entities_Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Entities
{
    public class Massage : IEntity
    {
        [Required]
        public string Discription { get; set; }
        [Required]
        public Guid Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public Chat UsingChat { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CreaterId { get; set; }
        public User Creater { get; set; }
    }
}
