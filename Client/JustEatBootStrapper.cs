using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JustEat.Client.ServiceContracts.Proxies;
using JustEat.Client.Shell.Models;
using JustEat.Client.Shell.Proxies;
using JustEat.Client.Shell.Validators;
using JustEat.Services;
using JustEat.Services.BusinessLogic;
using JustEat.Services.BusinessLogic.SharedInterfaces;
using JustEat.Services.SharedInterfaces.DataContracts;
using JustEat.Services.SharedInterfaces.ServiceContracts;

namespace JustEat.Client.Shell
{

    /// <summary>
    /// 
    /// </summary>
    public class JustEatBootStrapper
    {
        private HttpClient _httpClient;
        private HttpResponseMessage _response;

        private InputValidator _inputValidator;

        private StringBuilder _errorLoggerSb;

        private IRestaurantsBusinessLogic _restaurantsBusinessLogic;
        private IRestaurantsService _restaurantsService;
        private IRestaurantsProxy _restaurantsProxy;

        /// <summary>
        /// 
        /// </summary>
        public void ConfigureContainer()
        {
            //todo - move settings to config file
            _httpClient = new HttpClient {BaseAddress = new Uri("https://public.je-apis.com/")};
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-GB");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic VGVjaFRlc3RBUEk6dXNlcjI=");
            _httpClient.DefaultRequestHeaders.Add("Accept-Tenant", "uk");

            _response = new HttpResponseMessage();

            _inputValidator = new InputValidator();

            _errorLoggerSb = new StringBuilder();

            _restaurantsBusinessLogic = new RestaurantsBusinessLogic(_httpClient, _response, _errorLoggerSb);
            _restaurantsService = new RestaurantsService(_restaurantsBusinessLogic);
            _restaurantsProxy = new RestaurantsProxy(_restaurantsService);
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializeMapper()
        {
            Mapper.Initialize(
                cfg =>
                {
                    // create maps
                    cfg.CreateMap<RestaurantServiceModel, RestaurantModel>();
                });
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void StartJustEatClient()
        {
            while (true)
            {
                // display start information
                DisplayStartInformation();

                // get the input
                var input = GetInputModel();

                // exit?
                if (IsInputExit(input)) return;

                // is input valid?
                if (IsInputValid(input))
                {
                    var timer = new Stopwatch();

                    timer.Start();

                    // get the restaurants
                    var restaurants = GetRestaurantsByOutcode(input.Outcode);

                    timer.Stop();

                    var enumeratedRestaurants = restaurants as RestaurantModel[] ?? restaurants.ToArray();
                    
                    // display restaurants
                    DisplayRestaurants(enumeratedRestaurants);

                    // display execution information
                    DisplayExecutionInformation(enumeratedRestaurants.Length, timer.ElapsedMilliseconds);

                    // display errors
                    DisplayErrorLogger();
                }
                else
                {
                    // display invalid input
                    DisplayInvalidInput();
                }                
            }
        }
        private void DisplayErrorLogger()
        {
            // display exception logger
            if (_errorLoggerSb.Length > 0) Console.WriteLine(_errorLoggerSb);

            // clear any exception messages, ready to go again
            _errorLoggerSb.Clear();
        }

        private IEnumerable<RestaurantModel> GetRestaurantsByOutcode(string outcode)
        {
            // get restaurants
            var restaurantsByOutcode = Task.Run(() => _restaurantsProxy.GetRestaurantsByOutcode(outcode)).Result;

            // map to model
            var result = Mapper.Map<IEnumerable<RestaurantModel>>(restaurantsByOutcode);

            return result;
        }

        private bool IsInputValid(InputModel inputModel)
        {
            return (_inputValidator.Validate(inputModel).IsValid);
        }

        private static void DisplayRestaurants(IEnumerable<RestaurantModel> restaurantModels)
        {
            // view
            Console.WriteLine();
            Console.WriteLine("Start of file");
            foreach (var restaurant in restaurantModels)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"Restaurant Name : {restaurant.Name}");
                Console.WriteLine($"Restaurant Rating Stars : {restaurant.RatingStars}");
                Console.WriteLine($"Cusisine Type(s) : {string.Join(", ", restaurant.CuisineTypes)}");
            }
            Console.WriteLine("-----------------------------");
            Console.WriteLine("End of file");
            Console.WriteLine();

        }

        private static void DisplayInvalidInput()
        {
            Console.WriteLine("Invalid outcode.");
            Console.WriteLine();
        }

        private static void DisplayStartInformation()
        {
            Console.WriteLine("Type 'exit' and press Enter to Exit client.");
            Console.WriteLine("Please type an outcode and press Enter to return a list of currently available restaurants for submitted outcode.");
            Console.Write(">");
        }

        private static void DisplayExecutionInformation(long restaurantCount, long elapsedMilliseconds)
        {
            Console.WriteLine($"Return count {restaurantCount} and took {elapsedMilliseconds} ms.");
            Console.WriteLine();
        }

        private static InputModel GetInputModel()
        {
            var input = new InputModel { Outcode = Console.ReadLine() };
            return input;
        }

        private static bool IsInputExit(InputModel inputModel)
        {
            return (inputModel.Outcode == "exit");
        }
    }
}
