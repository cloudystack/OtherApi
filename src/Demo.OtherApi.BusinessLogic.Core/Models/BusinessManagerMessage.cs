using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.OtherApi.BusinessLogic.Core.Models
{
    public class BusinessManagerMessage 
    {
        public Guid BusinessManagerMessageId { get; private set; }
        public BusinessManagerMessageType BusinessManagerMessageType { get; set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }

        public BusinessManagerMessage(string key, string value, BusinessManagerMessageType type)
        {
            BusinessManagerMessageId = Guid.NewGuid();
            BusinessManagerMessageType = type;
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}
