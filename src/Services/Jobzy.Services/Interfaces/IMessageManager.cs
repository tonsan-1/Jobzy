namespace Jobzy.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessageManager
    {
        Task CreateAsync(string senderId, string recipientId, string content);

        Task MarkAllMessagesAsReadAsync(string currentUserId, string userId);

        Task<string> GetConversationLastMessageAsync(string currentUserId, string userId);

        Task<DateTime> GetConversationLastMessageSentDateAsync(string currentUserId, string userId);

        Task<IEnumerable<T>> GetMessagesAsync<T>(string userId, string recipientId);

        Task<IEnumerable<T>> GetAllUserConversationsAsync<T>(string userId);

        int GetUnreadMessagesCount(string userId);
    }
}
