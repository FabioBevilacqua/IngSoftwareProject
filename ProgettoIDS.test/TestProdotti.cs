using NUnit.Framework;
using Models;
using System.Collections.Generic;
using System.Linq;
using ProgettoIDS.Controllers;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProgettoIDS.test
{
    public class TestProdotti
    {
        private readonly string base_address = "https://idsfabio.azurewebsites.net";
        private HttpClient httpClient;
        [SetUp]
        public void Setup()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new System.Uri(this.base_address);
        }

        [Test]
        public void TestParametroNonValido()
        {
            var response =  this.httpClient.GetAsync("/Prodotto/GetById?id=-1").GetAwaiter().GetResult();
            Assert.AreEqual(200, response.StatusCode);
        }

        [Test]
        public void TestParametroValido()
        {
            var response =  this.httpClient.GetAsync("/Prodotto/GetById?id=1").GetAwaiter().GetResult();
            Assert.AreEqual(200, response.StatusCode);
        }
    }
}