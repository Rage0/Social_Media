using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Social_Media.Data.Models.Entities_Identity;
using Social_Media.Data.Models.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

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
        public User Creater { get; set; }
    }
}
