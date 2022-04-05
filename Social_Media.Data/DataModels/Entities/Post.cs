using System;
using Social_Media.Data.Models.Entities_Identity;
using Social_Media.Data.Models.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Entities
{
    public class Post : IEntity
    {
        public string Title { get; set; }
        public string RouteToPhoto { get; set; }
        public int Liked { get; set; }
        [Required]
        public string Discription { get; set; }
        [Required]
        public Guid Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public Chat UsingChat { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CreaterId { get; set; }
        public User Creater { get; set; }
    }
}
