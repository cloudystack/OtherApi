using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.OtherApi.Util.Options
{
    public class SwaggerOptions
    {
        public string Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TermsOfService { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Endpoint { get; set; }
    }
}
