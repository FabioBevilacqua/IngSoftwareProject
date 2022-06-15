
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
namespace ProgettoIDS.test
{
    public class TestOrdine
    {
        private string base_address = "https://ingsoftwareproject.azurewebsites.net";
        // this.base_address = "https://localhost:44320";

        private HttpClient httpClient;


        [SetUp]
        public void Setup()
        {
            this.httpClient = new HttpClient();


            this.httpClient.BaseAddress = new System.Uri(this.base_address);
        }

        [Test, Order(0)]
        public async Task TestGetOrder()
        {
            var response = await this.httpClient.GetAsync("api/Ordine");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [Test, Order(1)]
        public async Task TestCreateOrderUtenteInesistente()
        {
            var response = await this.httpClient.PostAsync("api/Ordine/CreateOrder?idUtente=-1", null);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test, Order(2)]
        public async Task TestCreateOrder()
        {
            var response = await this.httpClient.PostAsync("api/Ordine/CreateOrder?idUtente=1", null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Order(3)]
        public async Task TestAppProductOrder()
        {
            var response = await this.httpClient.PutAsync("api/Ordine/AddProductToOrder?id=1&idProdotto=1", null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Order(4)]
        public async Task TestAppProductOrder_OrderNotFound()
        {
            var response = await this.httpClient.PutAsync("api/Ordine/AddProductToOrder?id=0&idProdotto=1", null);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test, Order(5)]
        public async Task TestAppProductOrder_ProductNotFound()
        {
            var response = await this.httpClient.PutAsync("api/Ordine/AddProductToOrder?id=1&idProdotto=0", null);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test, Order(6)]
        public async Task TestDeleteOrder()
        {
            var response = await this.httpClient.DeleteAsync("api/Ordine?id=0");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }


    }
}