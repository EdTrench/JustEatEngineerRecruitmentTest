using System.Text;

namespace JustEat.Services.BusinessLogic
{
    public class BusinessStuffService
    {
        public string ReverseStringArray(string[] strArray)
        {
            var result = new StringBuilder();

            for (var i = strArray.Length - 1; i >= 0; i--)
            {
                result = result.Append(strArray[i]);
            }

            return result.ToString();
        }
    }
}
