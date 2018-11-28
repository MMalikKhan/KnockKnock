using KnockKnockAPI.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KnockKnockAPITest
{
    public class ReverseWordTest
    {
       
        //[Fact]
        [Theory]
        [InlineData("hello", "olleh")]
        public void ReverseWordSuccesfulTest(string samplevalue, string expected)
        {
           //Arrange
           //or use this short equivalent 
           var logger = Mock.Of<ILogger<ReverseWordsController>>();
           var controller = new ReverseWordsController(logger);
            //Act
            var value = controller.Get(samplevalue);

            //Assert
            Assert.Equal(expected, value.Value);
        }
    }
}
