using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustEat.Services.SharedInterfaces.DataContracts;

namespace JustEat.Services.BusinessLogic.SharedInterfaces
{
    public interface IRestaurantsBusinessLogic : IDisposable
    {
        IEnumerable<RestaurantServiceModel> GetCurrentlyAvailableRestaurantsByOutcode(string outcode);
    }
}
