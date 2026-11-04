using Garage3.Tests.Fakes;
using Garage3.UI;

namespace Garage3.Tests;


// Gör att testerna inte körs parallellt och krashar
[Collection("Console")]
public class ConsoleInputReadTests
{
  private const string ColorError = "You need to enter a value higher than 2 chars.Please try again!";

  // Read
  [Fact]
  public void Read_ValidInputFirstTime_ReturnsValueWithNoError()
  {
    using var console = new ConsoleRedirect("red");

    string result = ConsoleInput.Read<string>(
      "Color: ",
      ColorError,
      ConsoleInput.TryParseString,
      value => value.Length > 2 && value.Length <= 50);

    Assert.Equal("red", result);
    Assert.DoesNotContain(ColorError, console.Output);
  }

  // Oboy! Får felmeddelande om att minnet tar slut pga while(true)
  // Måste komma ut ur funktionen och fick lägga till en valid input. 
  [Theory]
  [InlineData("", "red")]
  [InlineData(" ", "red")]
  [InlineData("12", "red")]
  public void Read_InvalidInput_ReturnsErrorMessage(string invalidInput, string validInput)
  {
    using var console = new ConsoleRedirect(invalidInput, validInput);

    string result = ConsoleInput.Read<string>(
      "Color: ",
      ColorError,
      ConsoleInput.TryParseString,
      value => value.Length > 2 && value.Length <= 50);

    Assert.Equal(validInput, result);
    Assert.Contains(ColorError, console.Output);
  }

  // NumberOfProperty
  [Theory]
  [InlineData("1", 1)]
  [InlineData("8", 8)]
  public void NumberOfProperty_InRange_ReturnsAsValid(string input, int expected)
  {
    using var console = new ConsoleRedirect(input);

    Assert.Equal(expected, ConsoleInput.NumberOfProperty("Wheels: "));
  }

  [Fact]
  public void NumberOfProperty_OutOfRange_KeepsAskingUntilValidInput()
  {
    using var console = new ConsoleRedirect("0", "9", "abc", "4");

    int result = ConsoleInput.NumberOfProperty("Wheels: ");

    Assert.Equal(4, result);
  }
}