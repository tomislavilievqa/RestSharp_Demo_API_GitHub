using System.Reflection.Metadata.Ecma335;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Immutable;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace GitHub_API_Testing
{
    internal class Comments
    {
        public string body { get; set; }
        public long id { get; set; }
    }
}
