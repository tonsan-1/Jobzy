namespace Jobzy.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessageManager
    {
        int GetUnreadMessagesCount(string userId);

        Task<IEnumerable<T>> GetAllUserConversations<T>(string userId);

        Task<string> GetConversationLastMessage(string currentUserId, string userId);

        Task<DateTime> GetConversationLastMessageSentDate(string currentUserId, string userId);

        Task<IEnumerable<T>> GetMessages<T>(string userId, string recipientId);

        Task CreateAsync(string senderId, string recipientId, string content);

        Task MarkAllMessagesAsRead(string currentUserId, string userId);
    }
}
