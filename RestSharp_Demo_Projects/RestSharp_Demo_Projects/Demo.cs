using RestSharp;
using RestSharp.Authenticators;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace RestSharp_Demo_Projects
{
    internal class Demo
    {

        static void Main(string[] args)
        {
            // GET REQUESTS
            //host
            //RestClient client = new RestClient("https://api.github.com");

            ////endpoint, if we make GET is not mandatory to add the method
            //RestRequest request = new RestRequest("/repos/{user}/{repoName}/issues", Method.Get);
            //request.AddUrlSegment("user", "tomislavilievqa");
            //request.AddUrlSegment("repoName", "postmanapi");

            //var response = client.Execute(request);

            //Console.WriteLine($"Response: " + response.StatusCode);
            ////initially the response is in string format (ex. eqivj523i5jv242v) and we are getting the content with .content property
            //Console.WriteLine($"Response: " + response.Content);

            //var issues = JsonSerializer.Deserialize<List<Issue>>(response.Content);

            //foreach (var issue in issues)
            //{
            //    Console.WriteLine($"Issue name: " + issue.title);
            //    Console.WriteLine($"Issue number: " + issue.number);
            //}


            // POST REQUESTS

            RestClient client = new RestClient("https://api.github.com");

            var authenticator = client.Authenticator;
            authenticator = new HttpBasicAuthenticator
                ("tomislavilievqa", "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");


            RestRequest request = new RestRequest("/repos/{user}/{repoName}/issues", Method.Post);
            request.AddUrlSegment("user", "tomislavilievqa");
            request.AddUrlSegment("repoName", "postmanapi");



            var issueBody = new
            {
                title = "New issue from RestSharp" + DateTime.Now.Ticks,
                body = "some body for the issue",
                labels = new string[] { "bug", "enhancement", "test" }

            };

            request.AddBody(issueBody);
            var response = client.Execute(request);

            Console.WriteLine($"Response: " + response.StatusCode);

            var issue = JsonSerializer.Deserialize<Issue>(response.Content);

            Console.WriteLine($"Issue name: " + issue.title);
            Console.WriteLine($"Issue body: " + issue.body);
            Console.WriteLine($"Issue labels: " + issue.labels);



        }




    }
}