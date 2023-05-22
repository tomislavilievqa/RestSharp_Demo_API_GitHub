using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
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

namespace GitHub_API_Testing
{
    public class ApiTesting
    {
        private RestClient client;
        private RestClientOptions options;
        public const string baseURL = "https://api.github.com";
        //public RestRequest publicAPI = new RestRequest("https://flatrocktech-headless-qa.s9.cloudrock.info/wp-admin/admin-ajax.php?action=frt_jobs"); 
        //public RestRequest privateAPI = new RestRequest("https://flatrock.jobs.personio.com/xml"); 
        public const string username = "tomislavilievqa";
        public const string password = "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";
        public const string repoName = "postmanapi";
        // I am just testing the pull/push requests
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
        //[Test]
        //[Timeout(1000)]
        //[Category("GET")]
        //public void Test_CollectionsAreEqual()
        //{
        //    // Arrange
        //    RestRequest request = new RestRequest($"/repos/{username}/{repoName}/issues/1", Method.Get);
            
        //    request.AddHeader("Content-Type", "application/json");
        //    var response = this.client.Execute(request);

        //    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "HTTP Expected Status Code");

        //    var issue = JsonSerializer.Deserialize<Issue>(response.Content);
            
        //    Assert.That(issue.title, Is.EqualTo("First Issue"));
        //    Assert.That(issue.number, Is.EqualTo(1));
        //}

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

        [Test]
        [Timeout(1000)]
        [Category("GET")]
        public void Test_GetAllIssues()
        {
            
            RestRequest request = new RestRequest($"/repos/{username}/{repoName}/issues", Method.Get);

            var response = this.client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "HTTP Expected Status Code");

            var issues = JsonSerializer.Deserialize<List<Issue>>(response.Content);

            foreach (var issue in issues)
            {
                Assert.That(issue.title, Is.Not.Empty);
                Assert.That(issues.Count, Is.GreaterThan(0));
                Assert.That(issue.body, Is.Not.Empty);
                Assert.That(issue.id, Is.InstanceOf<long>());
            }
        }

        [Test]
        [Timeout(1000)]
        [Category("GET")]
        public void Test_GetLabelsOfSingleIssues()
        {

            RestRequest request = new RestRequest($"/repos/{username}/{repoName}/issues/1/labels", Method.Get);

            var response = this.client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "HTTP Expected Status Code");

            var labels = JsonSerializer.Deserialize<List<Labels>>(response.Content);
            var len = labels.Count;
            for (int i = 0; i < len; i++)
            {
                Assert.That(labels[i].name, Is.Not.Empty);
                Assert.That(labels[i].name, Is.InstanceOf<string>());
                Assert.That(labels[i].description, Is.InstanceOf<string>());
                Assert.That(labels[i].description, Is.Not.Empty);
            }
        }

        [Test]
        [Timeout(1000)]
        [Category("GET")]
        public void Test_GetInvalidIssue()
        {

            RestRequest request = new RestRequest($"/repos/{username}/{repoName}/issues/1000", Method.Get);

            var response = this.client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "HTTP Expected Status Code");

        }

        [Test]
        [Timeout(1000)]
        [Category("GET")]
        public void Test_GetAllCommentsOfAnIssues()
        {

            RestRequest request = new RestRequest($"/repos/{username}/{repoName}/issues/1/comments", Method.Get);

            var response = this.client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "HTTP Expected Status Code");

            var comments = JsonSerializer.Deserialize<List<Comments>>(response.Content);

            foreach (var comment in comments)
            {
                Assert.That(comment.body, Is.Not.Empty);
                Assert.That(comment.id, Is.GreaterThan(0));
                Assert.That(comment.id, Is.InstanceOf<long>());
                Assert.That(comment.body, Is.InstanceOf<string>());
            }
        }

        [Test]
        [Timeout(1000)]
        [Category("GET")]
        public void Test_GetCommentByID()
        {

            RestRequest request = new RestRequest($"/repos/{username}/{repoName}/issues/comments/1499502735", Method.Get);

            var response = this.client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "HTTP Expected Status Code");

            var comment = JsonSerializer.Deserialize<Comments>(response.Content);

            Assert.That(comment.body, Is.Not.Empty);
            Assert.That(comment.id, Is.GreaterThan(0));
            Assert.That(comment.id, Is.InstanceOf<long>());
            Assert.That(comment.body, Is.InstanceOf<string>());
        }

        [Test]
        [Category("POST")]
        public void Test_CreateAnissue()
        {
            //Arrange          

            RestRequest request = new RestRequest($"/repos/{username}/{repoName}/issues", Method.Post);

            var body = new
            {
                title = "Last one",
                body = "Last one",
                labels = new string[] { "test" },
                number = 11
            };

            request.AddBody(body);

            //Act
            var response = this.client.Execute(request);

            var issue = JsonSerializer.Deserialize<Issue>(response.Content);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "HTTP Status Code 201");
            Assert.That(issue.title, Is.EqualTo(body.title), "Object created with valid title");
            Assert.That(issue.body, Is.EqualTo(body.body), "Object created with valid body");
            Assert.That(issue.number, Is.GreaterThan(0), "Object created with valid number");
        }

        [Test]
        [Category("POST")]
        public void Test_CreateAnissueUnauthorized()
        {
            //Arrange          
            RestRequest request = new RestRequest($"/repos/{username}/{repoName}/issues", Method.Post);

            var body = new
            {
                title = "Test",
                body = "Test",
            };

            request.AddBody(body);

            var response = this.client.Execute(request);

            var issue = JsonSerializer.Deserialize<Issue>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "HTTP Status Code 403");

        }

        [Test]
        [Category("POST")]
        public void Test_CreateAnInvalidIssue()
        {
            //Arrange          
            RestRequest request = new RestRequest($"/repos/{username}/{repoName}/issues", Method.Post);

            var body = new
            {
                //"Title is missing.Only body is given."
                body = "Test111"
            };

            request.AddBody(body);

            var response = this.client.Execute(request);

            var issue = JsonSerializer.Deserialize<Issue>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.UnprocessableEntity), "HTTP Status Code 422");

        }

        [Test]
        [Category("POST")]
        public void Test_CreateACommentToAnIssue()
        {
            //Arrange          
            RestRequest request = new RestRequest($"/repos/{username}/{repoName}/issues/1/comments", Method.Post);

            var body = new
            {
                body = "test1",

            };

            request.AddBody(body);

            var response = this.client.Execute(request);

            var comments = JsonSerializer.Deserialize<Comments>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "HTTP Status Code 201");
            Assert.That(comments.body, Is.EqualTo(body.body), "Object created with valid body");
            Assert.That(comments.body, Is.InstanceOf<string>());

        }

        [Test]
        [Category("POST")]
        public void Test_AddLabelsToAnIssue()
        {
            //Arrange          
            RestRequest request = new RestRequest($"/repos/{username}/{repoName}/issues/143/labels", Method.Post);

            var body = new[]
            {
                new { name = "test", description = "some description", color = "f29513"},
                new { name = "test1241", description = "test15315", color = "f29513"},
                new { name = "test124134", description = "test1134313141", color = "f29513"}

            };

            request.AddBody(body);

            var response = this.client.Execute(request);

            var label = JsonSerializer.Deserialize<List<Labels>>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "HTTP Status Code 200");

            for (int i = 0; i < label.Count; i++)
            {
                Assert.That(label[i].name, Is.EqualTo(body[i].name));
                Assert.That(label[i].description, Is.EqualTo(body[i].description));
                Assert.That(label[i].color, Is.EqualTo(body[i].color));
            }

        }

        [Test]
        [Category("PATCH")]
        public void Test_UpdateAnissueUnauthorized()
        {


        }

        [Test]
        [Category("PATCH")]
        public void Test_DeleteAnissueUnauthorized()
        {


        }

        [Test]
        [Category("PATCH")]
        public void Test_DeleteAllLabelsFromIssue()
        {


        }
    }
}

