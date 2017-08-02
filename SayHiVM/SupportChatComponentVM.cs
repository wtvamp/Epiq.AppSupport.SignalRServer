using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DotNetify;
namespace ViewModels
{
    public class SupportChatMessage
    {
        public int type;
        public string image;
        public string text;
    }

    public interface ISupportMessageService
    {
        List<SupportChatMessage> GetAll();

        void Add(SupportChatMessage newMesssage);
    }

    public class ListSupportMessageService : ISupportMessageService
    {

        public ListSupportMessageService()
        {
            _allChatMessages = new List<SupportChatMessage>()
            {
                new SupportChatMessage()
                {
                    image = "http://lorempixel.com/50/50/cats/",
                    text = "Hello! Good Morning!",
                    type = 0
                },
                new SupportChatMessage()
                {
                    image = "http://lorempixel.com/50/50/animals/",
                    text = "Hello to you! Good Morning!",
                    type = 1
                }
            };
        }

        private List<SupportChatMessage> _allChatMessages;

        public void Add(SupportChatMessage newMesssage)
        {
            _allChatMessages.Add(newMesssage);
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
            _supportMessageService.Add(chatMessage);
        };

        /// <summary>
        /// List of all messages.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public IEnumerable<SupportChatMessage> messages => _supportMessageService.GetAll().Select(i => new SupportChatMessage
        {
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