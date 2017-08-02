using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DotNetify;
namespace ViewModels
{
    public class SupportChatMessage
    {
        public int Id;
        public int type;
        public string image;
        public string text;
    }

    public interface ISupportMessageService
    {
        List<SupportChatMessage> GetAll();

        int Add(SupportChatMessage newMesssage);
    }

    public class ListSupportMessageService : ISupportMessageService
    {
        private List<SupportChatMessage> _allChatMessages;
        private int idCount = 2;

        public ListSupportMessageService()
        {
            _allChatMessages = new List<SupportChatMessage>()
            {
                new SupportChatMessage()
                {
                    Id = 0,
                    image = "http://lorempixel.com/50/50/cats/",
                    text = "Hello! Good Morning!",
                    type = 0
                },
                new SupportChatMessage()
                {
                    Id = 1,
                    image = "http://lorempixel.com/50/50/animals/",
                    text = "Hello to you! Good Morning!",
                    type = 1
                }
            };
        }

        public int Add(SupportChatMessage newMesssage)
        {
            newMesssage.Id = idCount;
            idCount++;
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
            this.AddList(nameof(messages), new SupportChatMessage()
            {
                Id = _supportMessageService.Add(chatMessage),
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
            type = i.type,
            text = i.text,
            image = i.image
        });

        private readonly ISupportMessageService _supportMessageService;

        public SupportChatComponentVM()
        {
            _supportMessageService = new ListSupportMessageService();
        }

    }
}