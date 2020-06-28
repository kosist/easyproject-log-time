using Support.Helper;
using Xunit;

namespace UnitTests
{
    public class SpentTimeValidationTest
    {
        [Fact]
        public void TestCheckTimeFormatPattern()
        {
            var validationResult = false;
            var correctTime = "02:30";
            validationResult = SpentTimeValidation.CheckTimeFormatPattern(correctTime);
            Assert.True(validationResult, $"Tested string {correctTime}");

            var timeNotComplete = "02:3";
            validationResult = SpentTimeValidation.CheckTimeFormatPattern(timeNotComplete);
            Assert.False(validationResult, $"Tested string {timeNotComplete}");

            var timeNotComplete2 = ":35";
            validationResult = SpentTimeValidation.CheckTimeFormatPattern(timeNotComplete2);
            Assert.False(validationResult, $"Tested string {timeNotComplete2}");

            var timeWithoutSemicolon = "1234";
            validationResult = SpentTimeValidation.CheckTimeFormatPattern(timeWithoutSemicolon);
            Assert.False(validationResult, $"Tested string {timeWithoutSemicolon}");

            var timeLonger = "23:345";
            validationResult = SpentTimeValidation.CheckTimeFormatPattern(timeLonger);
            Assert.False(validationResult, $"Tested string {timeLonger}");
            
            var timeMinutesRange = "12:67";
            validationResult = SpentTimeValidation.CheckTimeFormatPattern(timeMinutesRange);
            Assert.False(validationResult, $"Tested string {timeMinutesRange}");

            var timeHoursRange = "34:32";
            validationResult = SpentTimeValidation.CheckTimeFormatPattern(timeHoursRange);
            Assert.True(validationResult, $"Tested string {timeHoursRange}");
        }
    }
}