using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Immutable;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace GitHub_API_Testing
{
    internal class Labels
    {
        public long id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public string description { get; set; }
    }
}
