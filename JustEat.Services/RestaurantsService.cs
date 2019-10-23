using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustEat.Services.BusinessLogic.SharedInterfaces;
using JustEat.Services.SharedInterfaces.DataContracts;
using JustEat.Services.SharedInterfaces.ServiceContracts;

namespace JustEat.Services
{
    public class RestaurantsService : IRestaurantsService
    {
        private readonly IRestaurantsBusinessLogic _restaurantsBusinessLogic;


        public RestaurantsService(IRestaurantsBusinessLogic restaurantsBusinessLogic)
        {
            _restaurantsBusinessLogic = restaurantsBusinessLogic;
        }

        public IEnumerable<RestaurantServiceModel> GetCurrentlyAvailableRestaurantsByOutcode(string outcode)
        {
            var result = _restaurantsBusinessLogic.GetCurrentlyAvailableRestaurantsByOutcode(outcode);
            return result;
        }
    }
}