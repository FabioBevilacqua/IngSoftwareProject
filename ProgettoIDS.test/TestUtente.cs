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

        [Test, Order(1)]
        public async Task CreateUtente()
        {
            var utente = new Utente();
            utente.Username = $"fabio{DateTime.Now.ToUniversalTime()}@l.it";
            utente.Username = utente.Username.Replace("/","");
            utente.Username = utente.Username.Replace(" ","");
            var json = JsonConvert.SerializeObject(utente);
            var utenteContentHttp = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync("api/Utente", utenteContentHttp);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test, Order(2)]
        public async Task CreateUtenteUsernameMin()
        {
            var utente = new Utente();
            utente.Username = $"fabio{DateTime.Now.ToUniversalTime()}@l.it";
            utente.Username = utente.Username.Substring(0,4);
            var json = JsonConvert.SerializeObject(utente);
            var utenteContentHttp = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync("api/Utente", utenteContentHttp);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test, Order(3)]
        public async Task CreateUtenteUsernameMax()
        {
            var utente = new Utente();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            utente.Username = new string(Enumerable.Repeat(chars, 32).Select(s => s[new Random().Next(s.Length)]).ToArray());
            
            var json = JsonConvert.SerializeObject(utente);
            var utenteContentHttp = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync("api/Utente", utenteContentHttp);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Test, Order(0)]
        public async Task CreateUtenteGiàEsistente() //Caso di test in cui l'utente già esiste
        {
            var utente = new Utente();
            utente.Username = "fabio87@live.it";
            var json = JsonConvert.SerializeObject(utente);
            var utenteContentHttp = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync("api/Utente", utenteContentHttp);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
