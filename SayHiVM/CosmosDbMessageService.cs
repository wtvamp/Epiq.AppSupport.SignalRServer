using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using ViewModels;

namespace SayHiVM
{
    public class CosmosDbMessageService: ISupportMessageService
    {
        private const string EndpointUrl = "https://epiqchat.documents.azure.com:443/";
        private const string PrimaryKey = "g18RgWhceFHR5Ii9uXYOvaD7JViQvXnibugZbn0OFJHsw0rKeUcIuUmuwDVZD7xZ3yv5LAK4tnklEpX0KFOa1w==";
        private DocumentClient client;

        public CosmosDbMessageService()
        {
            client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
            client.CreateDatabaseIfNotExistsAsync(new Database { Id = "EpiqMessages_oa" });

            client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("EpiqMessages_oa"), new DocumentCollection { Id = "EpiqMessagesCollection_oa" });

            var newId = "5da2854c-ef7c-4238-8329-cad5104a6160";
            var newId2 = "3b02a287-fe80-48fc-9e58-57fd330c8366";

            CreateFamilyDocumentIfNotExists("EpiqMessages_oa", "EpiqMessagesCollection_oa", new SupportChatMessage()
            {
                Id = newId,
                id = newId2,
                image = "https://upload.wikimedia.org/wikipedia/en/1/17/Batman-BenAffleck.jpg",
                text = "This is Batman...",
                type = 0
            });
            CreateFamilyDocumentIfNotExists("EpiqMessages_oa", "EpiqMessagesCollection_oa", new SupportChatMessage()
            {
                Id = newId,
                id = newId2,
                image = "https://www.redbulletin.com/sites/default/files/styles/sharing-thumbnail/public/images/article-thumbnail-smartphone/joker_1.jpg?itok=gwMU6YAY",
                text = "Hey Bats!  I'd like to report an accident with a little bird.",
                type = 1
            });

        }
        public List<SupportChatMessage> GetAll()
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<SupportChatMessage> messageQuery = this.client.CreateDocumentQuery<SupportChatMessage>(
                UriFactory.CreateDocumentCollectionUri("EpiqMessages_oa", "EpiqMessagesCollection_oa"),
                queryOptions).Take(50);

            var returnList = new List<SupportChatMessage>();
            foreach (SupportChatMessage message in messageQuery)
            {
                returnList.Add(message);
            }

            return returnList;
        }

        public string Add(SupportChatMessage newMesssage)
        {
            var newId = Guid.NewGuid().ToString();
            newMesssage.Id = newId;
            newMesssage.id = newId;
            CreateFamilyDocumentIfNotExists("EpiqMessages_oa", "EpiqMessagesCollection_oa", newMesssage);
            return newMesssage.Id;
        }


        private void CreateFamilyDocumentIfNotExists(string databaseName, string collectionName, SupportChatMessage message)
        {
            try
            {
                var result = client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, message.Id)).Result;
            }
            catch (AggregateException de)
            {
                    var result = client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), message).Result;

            }
        }

    }
}
