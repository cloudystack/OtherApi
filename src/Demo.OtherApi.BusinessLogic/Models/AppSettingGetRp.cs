using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.OtherApi.BusinessLogic.Models
{
    public class AppSettingListRp
    {
        public List<AppSettingListItemRp> Items { get; set; }
    }

    public class AppSettingListItemRp
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }

    public class AppSettingGetRp
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }
}
