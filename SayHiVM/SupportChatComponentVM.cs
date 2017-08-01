using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DotNetify;
namespace ViewModels
{
    public interface ISupportMessageService
    {
        List<SupportChatComponentVM.SupportChatMessage> GetAll();
    }

    public class ListSupportMessageService : ISupportMessageService
    {
        List<SupportChatComponentVM.SupportChatMessage> ISupportMessageService.GetAll()
        {
            return new List<SupportChatComponentVM.SupportChatMessage>()
            {
                new SupportChatComponentVM.SupportChatMessage()
                {
                    image = "http://lorempixel.com/50/50/cats/",
                    text = "Hello! Good Morning!",
                    type = 0
                },
                new SupportChatComponentVM.SupportChatMessage()
                {
                    image = "http://lorempixel.com/50/50/animals/",
                    text = "Hello to you! Good Morning!",
                    type = 1
                }
            };
        }
    }

    public class SupportChatComponentVM : BaseVM
    {
        public class SupportChatMessage
        {
            public int type;
            public string image;
            public string text;
        }

        /// <summary>
        /// List of employees.
        /// </summary>
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