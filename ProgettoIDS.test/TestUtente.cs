using Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoIDS.test
{
    public class TestUtente
    {
        private readonly string base_address = "https://ingsoftwareproject.azurewebsites.net";
        private HttpClient httpClient;

        [SetUp]
        public void Setup()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new System.Uri(this.base_address);
        }

        [Test, Order(0)]
        public async Task CreateUtente()
        {
            var utente = new Utente();
            utente.Username = "fabio87@live.it";
            var json = JsonConvert.SerializeObject(utente);
            var utenteContentHttp = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync("api/Utente", utenteContentHttp);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
