using Garage3.UI;

namespace Garage3.Tests;

public class InputTests
{
  // utöver klasserna så hanteras inputs och mycket validering av ConsoleInput.cs

  // tryparsestring
  [Theory]
  [InlineData("abc", true)]
  [InlineData("", false)]
  [InlineData(" ", false)]
  public void TryParseString_StringInput_ReturnsExpectedValidation(string input, bool expected)
  {
    bool result = ConsoleInput.TryParseString(input, out string value);

    Assert.Equal(expected, result);
    Assert.Equal(input, value);
  }

  // tryparseupperstring
  [Fact]
  public void TryParseUpperString_LowerCaseInput_ReturnsTrueAndUpper()
  {
    bool result = ConsoleInput.TryParseUpperString("abc123", out string value);

    Assert.True(result);
    Assert.Equal("ABC123", value);
  }

  //tryparseoptionalstring
  [Theory]
  [InlineData("")]
  [InlineData(" ")]
  public void TryParseOptionalString_EmptyOrWhiteSpace_ReturnsTrueAndNull(string input)
  {
    bool result = ConsoleInput.TryParseOptionalString(input, out string? value);

    Assert.True(result);
    Assert.Null(value);
  }

  [Fact]
  public void TryParseOptionalString_WithValue_ReturnsTrueAndText()
  {
    bool result = ConsoleInput.TryParseOptionalString("red", out string? value);

    Assert.True(result);
    Assert.Equal("red", value);
  }

  // tryparseoptionalint
  [Theory]
  [InlineData("")]
  [InlineData(" ")]
  public void TryParseOptionalInt_EmpytOrWhiteSpace_ReturnsTrueAndNull(string input)
  {
    bool result = ConsoleInput.TryParseOptionalInt(input, out int? value);

    Assert.True(result);
    Assert.Null(value);
  }

  [Theory]
  [InlineData("0", 0)]
  [InlineData("4", 4)]
  [InlineData("100", 100)]
  public void TryParseOptionalInt_PositiveNumber_ReturnsTrueAndNumber(string input, int expected)
  {
    bool result = ConsoleInput.TryParseOptionalInt(input, out int? value);

    Assert.True(result);
    Assert.Equal(expected, value);
  }

  [Theory]
  [InlineData("-5")]
  [InlineData("abc")]
  [InlineData("1.5")]
  public void TryParseOptionalInt_InvalidInput_ReturnsFalseAndNull(string input)
  {
    bool result = ConsoleInput.TryParseOptionalInt(input, out int? value);

    Assert.False(result);
    Assert.Null(value);
  }

}
