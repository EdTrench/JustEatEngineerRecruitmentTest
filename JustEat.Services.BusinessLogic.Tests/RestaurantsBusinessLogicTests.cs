using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using JustEat.Services.BusinessLogic.SharedInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JustEat.Services.BusinessLogic.Tests
{
    [TestClass]
    public class RestaurantsBusinessLogicTests
    {
        #region Test Initialization

        private HttpClient _httpClient;
        private HttpResponseMessage _response;
        private StringBuilder _errorLoggerSb;
        private IRestaurantsBusinessLogic _restaurantsBusinessLogic;
        
        [TestInitialize]
        public void Setup()
        {
            // todo could use NSubstitute?

            _httpClient = new HttpClient { BaseAddress = new Uri("https://public.je-apis.com/") };
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-GB");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic VGVjaFRlc3RBUEk6dXNlcjI=");
            _httpClient.DefaultRequestHeaders.Add("Accept-Tenant", "uk");

            _response = new HttpResponseMessage();

            _errorLoggerSb = new StringBuilder();

            _restaurantsBusinessLogic = new RestaurantsBusinessLogic(_httpClient, _response, _errorLoggerSb);
        }

        #endregion

        [TestMethod]
        public void TestRestaurants()
        {
            // arrange
            var result = _restaurantsBusinessLogic.GetCurrentlyAvailableRestaurantsByOutcode("BS4");
            // act

            // assert

        }
    }
}
