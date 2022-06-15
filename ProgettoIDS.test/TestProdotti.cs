using NUnit.Framework;
using Models;
using System.Collections.Generic;
using System.Linq;
using ProgettoIDS.Controllers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace ProgettoIDS.test
{
    public class TestProdotti
    {
        private readonly string base_address = "https://ingsoftwareproject.azurewebsites.net";
        private HttpClient httpClient;

        private Prodotto getProdottoTest()
        {
            var prodotto = new Prodotto();
            prodotto.Descrizione = $"Prodotto test_{DateTime.Now.ToUniversalTime()}";
            prodotto.Prezzo = new Random().Next(1,10);
            prodotto.Quantita = new Random().Next(20);
            return prodotto;
        }

        [SetUp]
        public void Setup()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new System.Uri(this.base_address);
        }

        [Test]
        public async Task TestCreazioneProdotto()
        {
            var prodotto = this.getProdottoTest();
            var json = JsonConvert.SerializeObject(prodotto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync("api/prodotto", data);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task TestQuantit‡NonValida()
        {
            var prodotto = this.getProdottoTest();
            prodotto.Quantita = 101;
            var json = JsonConvert.SerializeObject(prodotto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync("api/prodotto", data);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task TestCreazioneProdottoDeletedAt()
        {
            var prodotto = this.getProdottoTest();
            prodotto.Deleted_At = DateTime.Now;
            var json = JsonConvert.SerializeObject(prodotto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync("api/prodotto", data);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task TestPrezzoNonValidoMax()
        {
            var prodotto = this.getProdottoTest();
            prodotto.Prezzo = 1500;
            var json = JsonConvert.SerializeObject(prodotto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync("api/prodotto", data);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task TestPrezzoNonValidoMin()
        {
            var prodotto = this.getProdottoTest();
            prodotto.Prezzo = 0;
            var json = JsonConvert.SerializeObject(prodotto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync("api/prodotto", data);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task TestParametroNonValido()
        {
            var response = await this.httpClient.GetAsync("api/prodotto/GetById?id=-1");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Test]
        public async Task TestParametroValido()
        {
            var response = await this.httpClient.GetAsync("api/prodotto/GetById?id=1");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}