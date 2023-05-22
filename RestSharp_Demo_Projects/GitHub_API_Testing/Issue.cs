using System.Reflection.Metadata.Ecma335;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Immutable;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace GitHub_API_Testing
{
    internal class Issue
    {
        public string title { get; set; }
        public string body { get; set; }
        public int number { get; set; }
        public List<Labels> labels { get; set; }
        public long id { get; set; }
        public string state { get; set; }
        //public List<Comments> comments { get; set; }

    }
}