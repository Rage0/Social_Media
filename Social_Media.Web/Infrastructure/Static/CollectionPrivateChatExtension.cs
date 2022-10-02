using Social_Media.Data.DataModels.Entities;
using System;
using System.Collections.Generic;

namespace Social_Media.Web.Infrastructure
{
    public static class CollectionPrivateChatExtension
    {
        public static bool HasSamePrivateChat(this ICollection<PrivateChat> privateChats, ICollection<PrivateChat> privateChatsAnother)
        {
            foreach (PrivateChat privateChat in privateChats)
            {
                Guid chatId = privateChat.Id;
                foreach (PrivateChat privateChatAnother in privateChatsAnother)
                {
                    Guid chatIdAnother = privateChatAnother.Id;
                    if (chatId == chatIdAnother)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
