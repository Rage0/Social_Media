using Social_Media.Data.DataModels.Entities.Interfaces;
using Social_Media.Data.DataModels.Entities_Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Social_Media.Data.DataModels.Entities
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
        public PrivateChat PrivateChat { get; set; }
        public string CreaterId { get; set; }
        public User Creater { get; set; }
    }
}
