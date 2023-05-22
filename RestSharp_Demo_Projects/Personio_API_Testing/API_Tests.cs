using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
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

namespace FRT_Personio_API_Testing
{
    public class Tests
    {
        public class ApiTesting
        {
            private RestClient clientPublic;
            private RestClient clientPrivate;
            private RestClientOptions optionsPublic;
            private RestClientOptions optionsPrivate;
            public const string baseUrlPublic = "https://flatrocktech-headless-qa.s9.cloudrock.info";
            public const string baseUrlPrivate = "https://flatrock.jobs.personio.com";
            public RestRequest publicAPI = new RestRequest("/wp-admin/admin-ajax.php?action=frt_jobs");
            public RestRequest privateAPI = new RestRequest("/xml");
            public const string username = "tomislavilievqa";
            public const string password = "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";
            public const string repoName = "postmanapi";
            // result in a two fields
            //public RestResponse responsePublic;
            //public RestResponse responsePrivate;

            [SetUp]
            public void Setup()
            {

                this.optionsPublic = new RestClientOptions(baseUrlPublic);
                this.optionsPrivate = new RestClientOptions(baseUrlPrivate);
                //options.Authenticator = new HttpBasicAuthenticator(username, password);
                this.clientPublic = new RestClient(optionsPublic);
                this.clientPrivate = new RestClient(optionsPrivate);

            }

            [Test]
            [Category("GET")]
            public void Test_GetAllPositions()
            {

                // Act             
                RestResponse responsePublic = this.clientPublic.Execute(publicAPI);
                RestResponse responsePrivate = this.clientPrivate.Execute(privateAPI);
                
                Assert.That(responsePublic.StatusCode, Is.EqualTo(HttpStatusCode.OK), "HTTP Expected Status Code");
                Assert.That(responsePrivate.StatusCode, Is.EqualTo(HttpStatusCode.OK), "HTTP Expected Status Code");
                               
                
                var positionsPublic = System.Text.Json.JsonSerializer.Deserialize<List<Positions>>(responsePublic.Content);           
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(responsePrivate.Content);

                XmlNodeConverter converter = new XmlNodeConverter();
                string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode<List<Positions>>(doc);
                ;

                

            }
        }
    }
}