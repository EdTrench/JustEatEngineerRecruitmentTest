using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustEat.Client.ServiceContracts.Proxies;
using JustEat.Services.SharedInterfaces.DataContracts;
using JustEat.Services.SharedInterfaces.ServiceContracts;

namespace JustEat.Client.Shell.Proxies
{
    public class RestaurantsProxy 
        : IRestaurantsProxy
    {
        private readonly IRestaurantsService _restaurantsService;

        public RestaurantsProxy(IRestaurantsService restaurantsService)
        {
            _restaurantsService = restaurantsService;
        }

        public IEnumerable<RestaurantServiceModel> GetRestaurantsByOutcode(string outcode)
        {
            // todo async, channel factory, wcf, binding, et al
            var result = _restaurantsService.GetCurrentlyAvailableRestaurantsByOutcode(outcode);
            return result;
        }
    }
}
