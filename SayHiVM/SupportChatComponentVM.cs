using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNetify;
using Newtonsoft.Json;
using SayHiVM;

namespace ViewModels
{
    public class SupportChatMessage
    {
        [JsonProperty(PropertyName = "id")]
        public string Id;
        [JsonProperty(PropertyName = "Id")]
        public string id;
        public int type;
        public string image;
        public string text;
    }

    public interface ISupportMessageService
    {
        List<SupportChatMessage> GetAll();

        string Add(SupportChatMessage newMesssage);
    }

    public class ListSupportMessageService : ISupportMessageService
    {
        private List<SupportChatMessage> _allChatMessages;

        public ListSupportMessageService()
        {
            var newId = "5da2854c-ef7c-4238-8329-cad5104a6160";
            var newId2 = "3b02a287-fe80-48fc-9e58-57fd330c8366";

            _allChatMessages = new List<SupportChatMessage>()
            {
                new SupportChatMessage()
                {
                    Id = newId,
                    id = newId2,
                    image = "https://upload.wikimedia.org/wikipedia/en/1/17/Batman-BenAffleck.jpg",
                    text = "This is Batman...",
                    type = 0
                },
                new SupportChatMessage()
                {
                    Id = newId,
                    id = newId2,
                    image = "https://www.redbulletin.com/sites/default/files/styles/sharing-thumbnail/public/images/article-thumbnail-smartphone/joker_1.jpg?itok=gwMU6YAY",
                    text = "Hey Bats!  I'd like to report an accident with a little bird.",
                    type = 1
                }
            };
        }

        public string Add(SupportChatMessage newMesssage)
        {
            var newId = Guid.NewGuid().ToString();
            newMesssage.Id = newId;
            newMesssage.id = newId;
            _allChatMessages.Add(newMesssage);
            return newMesssage.Id;
        }


        public List<SupportChatMessage> GetAll()
        {
            return _allChatMessages;
        }
    }

    // ReSharper disable once InconsistentNaming
    public class SupportChatComponentVM : BaseVM
    {

        /// <summary>
        /// When the Add button is clicked, this property will receive the new employee full name input.
        /// </summary>
        public Action<SupportChatMessage> Add => chatMessage =>
        {
            var newId = _supportMessageService.Add(chatMessage);
            this.AddList(nameof(messages), new SupportChatMessage()
            {
                Id = newId,
                id = newId,
                type = chatMessage.type,
                text = chatMessage.text,
                image = chatMessage.image
            });
        };

        /// <summary>
        /// If you use CRUD methods on a list, you must set the item key prop name of that list
        /// by defining a string property that starts with that list's prop name, followed by "_itemKey".
        /// </summary>
        public string messages_itemKey => nameof(SupportChatMessage.Id);

        /// <summary>
        /// List of all messages.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public IEnumerable<SupportChatMessage> messages => _supportMessageService.GetAll().Select(i => new SupportChatMessage()
        {
            Id = i.Id,
            id = i.id,
            type = i.type,
            text = i.text,
            image = i.image
        });

        private readonly ISupportMessageService _supportMessageService;

        public SupportChatComponentVM()
        {
            _supportMessageService = new CosmosDbMessageService();
        }

    }
}