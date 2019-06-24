using APIAutomation.DataModel;
using System;
using System.IO;
using System.Net.Http;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using APIAutomation.Utilities;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APIAutomation.StepDef
{
    [Binding]
    public class APIFeatureSteps
    {
        HttpClient client;
        List<URLList> objURLList;
        String rparameter = null;
        String email = null;
        String password = null;
        String jsonString = null;
        StringContent httpContent = null;
        Dictionary<String, String> dDict = new Dictionary<String, String>();
        Register objRegister = new Register();



        APIFeatureSteps()
        {
            var jsonURLListString = File.ReadAllText(@"C://Users//sruth//source//repos//APIAutomation//APIAutomation//DataModel//URLList.json");  //for this always keep a copy of Json Handler file in ur project directory
            objURLList = JsonConvert.DeserializeObject<List<URLList>>(jsonURLListString);
        }


        [Given(@"I have a HTTP Client")]
        public void GivenIHaveAHTTPClient()
        {
            client = new HttpClient();
        }

        [Given(@"I am testing URL")]
        public void GivenIAmTestingURL()
        {

            client.BaseAddress = new Uri(objURLList[0].baseURL);

        }

        [Given(@"I have a route parameter")]
        public void GivenIHaveARouteParameter()
        {
            rparameter = objURLList[0].routeParameter;
        }

        [Given(@"I accept content-type (.*)")]
        public void GivenIAcceptContent_TypeApplicationJson(string contentType)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("token", "37cb9e58-99db-423c-9da5-42d5627614c5");
        }

        [Given(@"the following parameters")]
        public void GivenTheFollowingParameters(Table table)
        {
            dDict = ConvertTable.converttoDictionary(table);


        }

        [Given(@"I have the request body")]
        public void GivenIHaveTheRequestBody()
        {
            jsonString = JsonConvert.SerializeObject(dDict, Formatting.Indented);
             httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
        }

        [Given(@"I send the request")]
        public void GivenISendTheRequestAsync()
        {
            using (HttpResponseMessage response = client.PostAsync(rparameter, httpContent).Result)
            {
                if (response.IsSuccessStatusCode)
                {

                    objRegister = JsonConvert.DeserializeObject<Register>(response.Content.ReadAsStringAsync().Result);
                    objRegister.responseStatus = response.StatusCode.ToString();
                }
            }
        }

        [Given(@"verify the response")]
        public void GivenVerifyTheResponse()
        {
            Assert.AreEqual("200", objRegister.responseStatus);
            Assert.IsNotNull(objRegister.token);
            Assert.IsNotNull(objRegister.id);

        }
    }
}