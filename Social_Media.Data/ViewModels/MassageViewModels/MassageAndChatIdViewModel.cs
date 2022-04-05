using Social_Media.Data.Models.Entities;
using System;

namespace Social_Media.Web.Models.MassageViewModels
{
    public class MassageAndChatIdViewModel
    {
        public Massage Massage { get; set; }
        public Guid ChatId { get; set; }
    }
}
