// **********************************************************
// Assembly         : JustEat.Client.ServiceContracts.Proxies
// Author           : Edward Trenchard
// Created          : 17-07-2016
//
// **********************************************************
// <copyright file="IRestaurantsProxy.cs" company="">
//     Copyright (c) All rights reserved.
// </copyright>
// <summary></summary>

using System.Collections.Generic;
using JustEat.Services.SharedInterfaces.DataContracts;

namespace JustEat.Client.ServiceContracts.Proxies
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRestaurantsProxy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outcode"></param>
        /// <returns></returns>
        IEnumerable<RestaurantServiceModel> GetRestaurantsByOutcode(string outcode);
    }
}
