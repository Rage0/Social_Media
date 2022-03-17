using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities.Interfaces;

namespace Social_Media.Web.Models.ChatViewModels
{
    public class ChatingRoomViewModel
    {
        public Chat CurrentChat { get; set; }
        public Massage MassageModel { get; set; }
    }
}
