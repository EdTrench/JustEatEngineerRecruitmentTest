using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JustEat.Services.SharedInterfaces.DataContracts
{
    [DataContract]
    public class RestaurantServiceModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public double RatingStars { get; set; }

        [DataMember]
        public IEnumerable<string> CuisineTypes { get; set; } 
    }
}