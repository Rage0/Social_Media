using Social_Media.Data.Models.Entities_Identity;
using System;


namespace Social_Media.Data.Models.Entities.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public User Creater { get; set; }
    }
}
