
using Chat.Domain.Attributes;
using Chat.Domain.Core.Events;

using NetDevPack.Messaging;
using Newtonsoft.Json;

namespace Chat.Infra.Data.EventSourcing
{
    [Bean]
    public class SqlEventStore : IEventStore
    {


        public void Save<T>(T theEvent) where T : Event
        {
            // Using Newtonsoft.Json because System.Text.Json
            // is a sad joke to be considered "Done"

            // The System.Text don't know how serialize a
            // object with inherited properties, I said is sad...
            // Yes! I tried: options = new JsonSerializerOptions { WriteIndented = true };

            //var serializedData = JsonConvert.SerializeObject(theEvent);

            //var storedEvent = new StoredEvent(
            //    theEvent,
            //    serializedData,
            //    "test");

            //_eventStoreRepository.Store(storedEvent);
        }
    }
}