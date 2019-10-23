using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JustEat.Services.BusinessLogic.SharedInterfaces;
using JustEat.Services.SharedInterfaces.DataContracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JustEat.Services.BusinessLogic
{
    public class RestaurantsBusinessLogic : IRestaurantsBusinessLogic
    {
        private HttpClient _httpClient;
        private HttpResponseMessage _response;
        private readonly StringBuilder _errorLoggerSb;


        public RestaurantsBusinessLogic(HttpClient httpClient, HttpResponseMessage response,
            StringBuilder errorLoggerSb)
        {
            _httpClient = httpClient;
            _response = response;
            _errorLoggerSb = errorLoggerSb;
        }

        public IEnumerable<RestaurantServiceModel> GetCurrentlyAvailableRestaurantsByOutcode(string outcode)
        {
            // todo split into seperate methods
            // todo - make async another day

            var result = new List<RestaurantServiceModel>();
            var restUrl = $"restaurants?q={outcode}";
            var content = string.Empty;
            
            try
            {
                // get response, waiting for result
                _response = Task.Run(() => _httpClient.GetAsync(restUrl)).Result;
            }
            catch (Exception ex)
            {
                // log exception message;
                _errorLoggerSb.AppendLine(ex.Message);
                if (ex.InnerException != null) _errorLoggerSb.AppendLine(ex.InnerException.Message);
                return result;
            }
            
            // have we been successful?
            if (!_response.IsSuccessStatusCode)
            {
                // log and return
                _errorLoggerSb.AppendLine($"Response Status: {_response.StatusCode}");
                return result;
            }

            try
            {
                // read as string, waiting for result
                content = Task.Run(() => _response.Content.ReadAsStringAsync()).Result;
            }
            catch (Exception ex)
            {
                // log exception message;
                _errorLoggerSb.AppendLine(ex.Message);
                if (ex.InnerException != null) _errorLoggerSb.AppendLine(ex.InnerException.Message);
                return result;
            }
            
            // parse the content
            var jo = JObject.Parse(content);

            // make our anonymous restaurants where IsOpenNow
            var restaurants = from r in jo["Restaurants"]
                              where (bool)r["IsOpenNow"]
                              select new
                              {
                                  Name = (string) r["Name"],
                                  RatingStars = (double) r["RatingStars"],
                                  CuisineTypes = ((JArray) r["CuisineTypes"])
                                  .Select(cta => (string) cta["Name"])
                              };

            // deserialize to our service model
            foreach (var restaurant in restaurants)
            {
                result.Add(new RestaurantServiceModel
                {
                    Name = restaurant.Name,
                    RatingStars = restaurant.RatingStars,
                    CuisineTypes = restaurant.CuisineTypes
                });
            }
            
            return result;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                //dispose of CLR objects
                _httpClient?.Dispose();
                _response?.Dispose();
            }

            //dispose of non-CLR objects
            /* none */

            _disposed = true;
        }

        ~RestaurantsBusinessLogic()
        {
            Dispose(false);
        }

        #endregion
    }
}
