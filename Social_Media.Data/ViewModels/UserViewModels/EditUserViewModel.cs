using Social_Media.Data.DataModels.Entities;
using Social_Media.Data.DataModels.Entities_Identity;
using System.ComponentModel.DataAnnotations;

namespace Social_Media.Data.ViewModels.UserViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Id { get; set; }
    }
}
