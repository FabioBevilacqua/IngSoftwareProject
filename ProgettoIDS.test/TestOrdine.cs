
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using NUnit.Framework;
namespace ProgettoIDS.test
{
    public class TestOrdine
    {
        private readonly string base_address = "https://ingsoftwareproject.azurewebsites.net";
        private HttpClient httpClient;

        [SetUp]
        public void Setup()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new System.Uri(this.base_address);
        }

        [Test]
        public async Task TestGetOrder()
        {
            var response = await this.httpClient.GetAsync("api/Ordine");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [Test]
        public async Task TestCreateOrderUtenteInesistente()
        {
            var response = await this.httpClient.PostAsync("api/Ordine?idUtente=0", null);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task TestCreateOrder()
        {
            var response = await this.httpClient.PostAsync("api/Ordine?idUtente=2", null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task TestAppProductOrder()
        {
            var response = await this.httpClient.PutAsync("api/Ordine/AddProductToOrder?id=1&idProdotto=0", null);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        //[Test]
        //public async Task TestDeleteOrder()
        //{
        //    var response = await this.httpClient.DeleteAsync("api/Ordine?id=3");
        //    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        //}


    }
}