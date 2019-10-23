using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JustEat.Services.BusinessLogic.Tests
{
    [TestClass]
    public class BusinessStuffServiceTests
    {
        [TestMethod]
        public void BusinessStuffService_ShouldReverseArray()
        {
            // arrange
            var service = new BusinessStuffService();
            var input = new []{"t","h","i","s"};

            // act
            var result = service.ReverseStringArray(input);

            // asset
            Assert.AreEqual(result, "siht");
        }
    }
}
