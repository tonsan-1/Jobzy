namespace Jobzy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class MessageManager : IMessageManager
    {
        private readonly IDeletableEntityRepository<Message> repository;

        public MessageManager(IDeletableEntityRepository<Message> repository)
        {
            this.repository = repository;
        }

        public async Task CreateAsync(string senderId, string recipientId, string content)
        {
            var message = new Message
            {
                SenderId = senderId,
                RecipientId = recipientId,
                Content = content,
            };

            await this.repository.AddAsync(message);
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllUserConversations<T>(string userId)
        {
            var sentMessagesToUser = this.repository
                .All()
                .Where(x => x.SenderId == userId || x.RecipientId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Include(x => x.Sender)
                .Select(x => x.Sender);

            var receivedMessageFromUser = this.repository
                .All()
                .Where(x => x.SenderId == userId || x.RecipientId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Include(x => x.Recipient)
                .Select(x => x.Recipient);

            var allUserConversations = await sentMessagesToUser
                .Concat(receivedMessageFromUser)
                .Where(x => x.Id != userId)
                .Distinct()
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();

            return allUserConversations;
        }

        public async Task<string> GetConversationLastMessage(string currentUserId, string userId)
        {
            return await this.repository
                .All()
                .Where(x => (x.RecipientId == currentUserId && x.SenderId == userId) ||
                            (x.RecipientId == userId && x.SenderId == currentUserId))
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => x.Sender.Id == currentUserId ? $"You: {x.Content}" : x.Content)
                .FirstOrDefaultAsync();
        }

        public async Task<DateTime> GetConversationLastMessageSentDate(string currentUserId, string userId)
        {
            return await this.repository
                .All()
                .Where(x => (x.RecipientId == currentUserId && x.SenderId == userId) ||
                            (x.RecipientId == userId && x.SenderId == currentUserId))
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => x.CreatedOn)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetMessages<T>(string userId, string recipientId)
        {
            var messages = await this.repository
                .All()
                .Where(x => (x.RecipientId == recipientId && x.SenderId == userId) ||
                            (x.RecipientId == userId && x.SenderId == recipientId))
                .OrderBy(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();

            return messages;
        }

        public int GetUnreadMessagesCount(string userId)
        {
            return this.repository
                .All()
                .Where(x => x.RecipientId == userId && !x.IsRead)
                .GroupBy(x => x.SenderId)
                .Count();
        }

        public async Task MarkAllMessagesAsRead(string currentUserId, string userId)
        {
            var messages = await this.repository
                .All()
                .Where(x => x.RecipientId == currentUserId && x.SenderId == userId)
                .ToListAsync();

            foreach (var message in messages)
            {
                message.IsRead = true;

                this.repository.Update(message);
                await this.repository.SaveChangesAsync();
            }
        }
    }
}
