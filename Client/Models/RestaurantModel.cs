// **********************************************************
// Assembly         : JustEat.Client.Shell.Models
// Author           : Edward Trenchard
// Created          : 17-07-2016
//
// **********************************************************
// <copyright file="RestaurantModel.cs" company="">
//     Copyright (c) All rights reserved.
// </copyright>
// <summary></summary>

using System.Collections.Generic;

namespace JustEat.Client.Shell.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class RestaurantModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double RatingStars { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> CuisineTypes { get; set; }
    }
}
