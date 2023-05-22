using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Immutable;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;

namespace FRTRedesign_Personio_API
{
    public class Postman_API_Tests
    {
        private RestClient client;
        private RestClientOptions options;
        public const string baseURL = "https://api.github.com";
        //public RestRequest publicAPI = new RestRequest("https://flatrocktech-headless-qa.s9.cloudrock.info/wp-admin/admin-ajax.php?action=frt_jobs"); 
        //public RestRequest privateAPI = new RestRequest("https://flatrock.jobs.personio.com/xml"); 
        public const string username = "tomislavilievqa";
        public const string password = "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";
        public const string repoName = "postmanapi";
        // result in a two fields
        //public RestResponse responsePublic;
        //public RestResponse responsePrivate;

        [SetUp]
        public void Setup()
        {
            // request public API
            // request for the private API          
            this.options = new RestClientOptions(baseURL);
            options.Authenticator = new HttpBasicAuthenticator(username, password);
            this.client = new RestClient(options);

        }

        [Test]
        [Timeout(1000)]
        [Category("GET")]
        public void Test_GetSingleIssue()
        {
            // Arrange
            RestRequest request = new RestRequest($"/repos/{username}/{repoName}/issues/1", Method.Get);

            request.AddHeader("Content-Type", "application/json");
            var response = this.client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "HTTP Expected Status Code");

            var issue = JsonSerializer.Deserialize<Issue>(response.Content);

            Assert.That(issue.title, Is.EqualTo("First Issue"));
            Assert.That(issue.number, Is.EqualTo(1));
        }
    }
}
