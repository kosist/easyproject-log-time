using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

namespace APIPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            //var client = new RestClient("https://anv.easyproject.cz");

            //var request = new RestRequest("time_entries.xml", Method.GET, DataFormat.Xml);
            //request.AddHeader("content-type", "application/xml");
            //request.AddParameter("key", "a6531eb52dfbb0076075887bea7aa7bbd94c507b");
            //request.AddParameter("project_id", "240");

            ////var response = client.Get(request);
            //var response = client.Execute<List<TimeEntryXML>>(request);
            //foreach (var timeEntryXml in response.Data)
            //{
            //    Console.WriteLine($"Issue ID: {timeEntryXml.Issue.Id}");
            //}

            string easyProjectAPIKey = Environment.GetEnvironmentVariable("EasyProjectAPIKey", EnvironmentVariableTarget.User);
            Console.WriteLine(easyProjectAPIKey);
        }
    }

    [DeserializeAs(Name = "time_entry")]
    public class TimeEntryXML
    {
        public Project Project { get; set; }
        public Issue Issue { get; set; }
    }

    [DeserializeAs(Name = "project")]
    public class Project
    {
        [DeserializeAs(Name = "id")]
        public int Id { get; set; }
    }

    [DeserializeAs(Name = "issue")]
    public class Issue
    {
        [DeserializeAs(Name = "id")]
        public int Id { get; set; }
    }
}
