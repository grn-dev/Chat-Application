using EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Infra.Data.Logging
{
    public class LogCollection : IDisposable
    {
        //Must be lower case always
        public const string ContextEventLogName = "database_log_event";
        public const string RequestEventLogName = "request_log_event";

        private readonly IEventBus _eventBus;
        private readonly Dictionary<string, List<string>> _logs;

        public LogCollection(IEventBus eventBus)
        {
            _logs = new Dictionary<string, List<string>>();
            _eventBus = eventBus;
        }

        public void AddLogJson(string eventLogName, string log)
        {
            if (!_logs.ContainsKey(eventLogName))
                _logs.Add(eventLogName, new List<string>());

            _logs[eventLogName].Add(log);
        }

        public List<string> GetAllLogs()
        {
            return _logs.SelectMany(pair => pair.Value).ToList();
        }

        public void RemoveLog(string logEventName)
        {
            if (!_logs.ContainsKey(logEventName))
                return;

            _logs[logEventName].Clear();
        }

        public void Dispose()
        {
            foreach (var (key, value) in _logs)
                foreach (var item in value)
                {
                    try
                    {
                        // _eventBus.Publish(new LogIntegrationEvent(item, key));
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
        }
    }
}