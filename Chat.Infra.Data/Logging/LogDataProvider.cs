using Audit.Core;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Chat.Infra.Data.Logging
{
    public class LogDataProvider : AuditDataProvider
    {
        private readonly Func<LogCollection> _logCollectionProvider;

        public LogDataProvider(Func<LogCollection> logCollectionProvider)
        {
            _logCollectionProvider = logCollectionProvider;
        }

        public override object InsertEvent(AuditEvent auditEvent)
        {
            try
            {
                _logCollectionProvider().AddLogJson(auditEvent.EventType, auditEvent.ToJson());
            }
            catch (Exception)
            {
                // ignored
            }

            return default;
        }

        public override void ReplaceEvent(object eventId, AuditEvent auditEvent)
        {
        }

        public override T GetEvent<T>(object eventId)
        {
            var fileName = eventId.ToString();
            return JsonConvert.DeserializeObject<T>("");
        }

        public override Task<object> InsertEventAsync(AuditEvent auditEvent)
        {
            try
            {
                _logCollectionProvider().AddLogJson(auditEvent.EventType, auditEvent.ToJson());
            }
            catch (Exception)
            {
                // ignored
            }

            return Task.FromResult(default(object));
        }

        public override Task ReplaceEventAsync(object eventId, AuditEvent auditEvent)
        {
            return Task.FromResult(0);
        }

        public override Task<T> GetEventAsync<T>(object eventId)
        {
            return null;
        }
    }
}