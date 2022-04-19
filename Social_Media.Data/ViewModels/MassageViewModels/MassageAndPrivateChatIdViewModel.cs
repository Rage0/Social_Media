using Social_Media.Data.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Data.ViewModels.MassageViewModels
{
    public class MassageAndPrivateChatIdViewModel
    {
        public Massage Massage { get; set; }
        public Guid PrivateChatId { get; set; }
        public string UserName { get; set; }
        public string FriendName { get; set; }
    }
}
