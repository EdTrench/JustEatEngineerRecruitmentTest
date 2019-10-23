using System;
using System.Collections.Generic;
using System.ServiceModel;
using JustEat.Services.SharedInterfaces.DataContracts;

namespace JustEat.Services.SharedInterfaces.ServiceContracts
{
    [ServiceContract]
    public interface IRestaurantsService
    {
        [OperationContract]
        IEnumerable<RestaurantServiceModel> GetCurrentlyAvailableRestaurantsByOutcode(string outcode);
    }
}