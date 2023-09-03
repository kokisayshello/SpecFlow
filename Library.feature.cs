using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using TechTalk.SpecFlow.Assist;
using System.Net;
using System.Text;
using System.Net.Http.Json;
using static System.Reflection.Metadata.BlobBuilder;
using System.Xml.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace LibraryApi.Tests
{
    [Binding]
    public class LibraryApiSteps
    {
        private string apiUrl = "http://localhost:9000/api/books/";
        private readonly HttpClient httpClient = new HttpClient();
        private HttpResponseMessage response;
        private string requestBody;
        private string responseBody;

        [Given(@"URL address (.*)")]
        public void GivenURLAddress(string url)
        {
            apiUrl = url;
        }

        [When(@"User sends POST request with JSON:")]
        public async Task WhenUserSendsPOSTRequestWithJSON(Table table)
        {
            var requestData = table.CreateInstance<BookRequest>();
            requestBody = JsonConvert.SerializeObject(requestData);

            var content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");
            response = await httpClient.PostAsync(apiUrl, content);

            responseBody = await response.Content.ReadAsStringAsync();
        }

        [Then(@"The response is 200 OK and JSON:")]
        public void ThenTheResponseIs200OKAndJSON(Table expectedResponseTable)
        {
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

            var expectedResponse = expectedResponseTable.CreateInstance<BookResponse>();
            var actualResponse = JsonConvert.DeserializeObject<BookResponse>(responseBody);

            Assert.AreEqual(expectedResponse.Id, actualResponse.Id);
            Assert.AreEqual(expectedResponse.Title, actualResponse.Title);
            Assert.AreEqual(expectedResponse.Description, actualResponse.Description);
            Assert.AreEqual(expectedResponse.Author, actualResponse.Author);
        }

        [When(@"User sends GET request to (.*)")]
        public async Task WhenUserSendsGETRequestToHttpLocalhostApiBooks(string endpoint)
        {
            response = await httpClient.GetAsync(endpoint);
        }

        [Then(@"The response is (.*) OK and empty body \[]")]
        public async Task ThenTheResponseShouldBeOKAndEmptyBody(int p0)
        {
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

            string responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("[]", responseContent);

        }

        [When(@"User sends PUT request with JSON:")]
        public async Task WhenUserSendsPUTRequestWithJSONBody(Table table)
        {
            var requestData = table.CreateInstance<BookRequest>();
            requestBody = JsonConvert.SerializeObject(requestData);

            var content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");
            response = await httpClient.PutAsync(apiUrl, content);

            responseBody = await response.Content.ReadAsStringAsync();
        }

        [When(@"User sends DELETE request to (.*)")]
        public async Task WhenUserSendsDELETERequestToHttpLocalhostApiBooks(string endpoint)
        {
            response = await httpClient.DeleteAsync(endpoint);
        }

        [Then(@"The response is (.*) No Content and empty body")]
        public async Task ThenTheResponseIsNoContentAndEmptyBody(int p0)
        {
            Assert.AreEqual(System.Net.HttpStatusCode.NoContent, response.StatusCode);

            string responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("", responseContent);

        }

        [Then(@"The response is 200 OK and JSON body:")]
        public async Task ThenTheResponseIsOKAndJSONBody(Table table)
        {
            // Verify the HTTP status code
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

            // Parse the JSON response
            string responseContent = await response.Content.ReadAsStringAsync();
            var expectedData = table.CreateInstance<BookResponse>();

            // You will need to implement a class ExpectedData with properties Id, Title, Description, and Author.
            // Deserialize the JSON response and compare it with expectedData.
            // Here's an example using Newtonsoft.Json:
            var actualData = JsonConvert.DeserializeObject<BookResponse>(responseContent);

            // Assert that the actual data matches the expected data
            Assert.AreEqual(expectedData.Id, actualData.Id);
            Assert.AreEqual(expectedData.Title, actualData.Title);
            Assert.AreEqual(expectedData.Description, actualData.Description);
            Assert.AreEqual(expectedData.Author, actualData.Author);
        }

        public class Book
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Author { get; set; }
        }  

        [Binding]
        public class BookSearchSteps
        {
            private readonly HttpClient httpClient = new HttpClient();
            private HttpResponseMessage response;
            private List<Book> books;

            [Given(@"I have made a GET request to ""(.*)""")]
            public async Task GivenIHaveMadeAGETRequestTo(string url)
            {
                response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
            }

            [When(@"I receive a response")]
            public async Task WhenIReceiveAResponse()
            {
                var content = await response.Content.ReadAsStringAsync();
                books = JsonConvert.DeserializeObject<List<Book>>(content);
            }

            [Then(@"the response should contain a book with the following details:")]
            public void ThenTheResponseShouldContainABookWithTheFollowingDetails(Table table)
            {
                var expectedBook = table.CreateInstance<Book>();
                var actualBook = books.FirstOrDefault(b => b.Id == expectedBook.Id);

                Assert.NotNull(actualBook);
                Console.WriteLine("Expected Result for Author is " + expectedBook.Author);
                Console.WriteLine("Actual Result Author is " + actualBook.Author);
                Console.WriteLine("Expected Result for Title is " + expectedBook.Title);
                Console.WriteLine("Actual Result Title is " + actualBook.Title);
                Console.WriteLine("Expected Result for Description is " + expectedBook.Description);
                Console.WriteLine("Actual Result Description is " + actualBook.Description);
                Console.WriteLine("Expected Result for Id is " + expectedBook.Id);
                Console.WriteLine("Actual Result for Id  is " + actualBook.Id);
                Assert.AreEqual(expectedBook.Title, actualBook.Title);
                Assert.AreEqual(expectedBook.Description, actualBook.Description);
                Assert.AreEqual(expectedBook.Author, actualBook.Author);
                Assert.AreEqual(expectedBook.Id, actualBook.Id);

            }
        }

        public class BookRequest
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Author { get; set; }
        }

        public class BookResponse
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Author { get; set; }
        }
    }
}
