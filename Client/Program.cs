
using System.Text;

namespace JustEat.Client.Shell
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // bootstrapper - poor man's di
            var bs = new JustEatBootStrapper();
            bs.ConfigureContainer();

            // initialize mapper
            bs.InitializeMapper();

            // the program
            bs.StartJustEatClient();
        }
    }
}