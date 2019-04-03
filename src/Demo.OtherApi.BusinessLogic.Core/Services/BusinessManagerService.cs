using Newtonsoft.Json;
using Demo.OtherApi.BusinessLogic.Core.Models;
using Demo.OtherApi.BusinessLogic.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.OtherApi.BusinessLogic.Core.Services
{
    public class BusinessManagerService : IBusinessManagerService
    {
        private List<BusinessManagerMessage> _messages;
        private Dictionary<string, string> _results;

        public BusinessManagerService()
        {
            _messages = new List<BusinessManagerMessage>();
            _results = new Dictionary<string, string>();
        }

        public async Task AddConflict(string message)
        {
            BusinessManagerMessage domainMessage = new BusinessManagerMessage("", message, BusinessManagerMessageType.Conflict);
            await Task.Run(() => {
                _messages.Add(domainMessage);
            });
        }

        public async Task AddNotFound(string message)
        {
            BusinessManagerMessage domainMessage = new BusinessManagerMessage("", message, BusinessManagerMessageType.NotFound);
            await Task.Run(() => {
                if (!_messages.Any(c => c.BusinessManagerMessageType == BusinessManagerMessageType.NotFound))
                    _messages.Add(domainMessage);
            });
        }

        public async Task AddResult(string key, object value)
        {
            await Task.Run(() => { _results.Add(key, JsonConvert.SerializeObject(value)); });
        }

        public async Task<T> GetResult<T>(string key)
        {
            if (!_results.Keys.Any(x => x.Equals(key, StringComparison.InvariantCultureIgnoreCase)))
                throw new KeyNotFoundException($"The key ${key} was not found");

            return await Task.Run(() => { return JsonConvert.DeserializeObject<T>(_results[key]); });
        }

        public string GetConflicts()
        {
            StringBuilder stringBuilder = new StringBuilder();
            var conflicts = _messages.Where(x => x.BusinessManagerMessageType == BusinessManagerMessageType.Conflict);
            foreach (var item in conflicts)
            {
                stringBuilder.AppendLine(item.Value);
            }
            return stringBuilder.ToString();
        }

        public bool HasConflicts()
        {
            return _messages.Any(x => x.BusinessManagerMessageType == BusinessManagerMessageType.Conflict);
        }

        public void Dispose()
        {
            _messages = new List<BusinessManagerMessage>();
        }

        public string GetNotFounds()
        {
            StringBuilder stringBuilder = new StringBuilder();
            var conflicts = _messages.Where(x => x.BusinessManagerMessageType == BusinessManagerMessageType.NotFound);
            foreach (var item in conflicts)
            {
                stringBuilder.AppendLine(item.Value);
            }
            return stringBuilder.ToString();
        }

        public bool HasNotFounds()
        {
            return _messages.Any(x => x.BusinessManagerMessageType == BusinessManagerMessageType.NotFound);
        }
    }
}
