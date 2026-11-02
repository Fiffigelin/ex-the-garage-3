using System;

namespace Garage3.UI;

public delegate bool TryParser<T>(string input, out T value);

public static class ConsoleInput
{
  public static bool TryParseString(string input, out string value)
  {
    value = input;
    return !string.IsNullOrWhiteSpace(input);
  }

  public static bool TryParseUpperString(string input, out string value)
  {
    value = input.ToUpper();
    return !string.IsNullOrWhiteSpace(input);
  }

  public static bool TryParseOptionalString(string input, out string? value)
  {
    value = string.IsNullOrWhiteSpace(input) ? null : input;
    return true;
  }

  public static bool TryParseOptionalInt(string input, out int? value)
  {
    value = null;

    if (string.IsNullOrWhiteSpace(input))
    {
      return true;
    }

    if (int.TryParse(input, out int number) && number >= 0)
    {
      value = number;
      return true;
    }

    return false;
  }

  public static T Read<T>(
  string message,
  string errorMessage,
  TryParser<T> parser,
  Func<T, bool>? isValid = null)
  {
    while (true)
    {
      Console.Write(message);
      string input = (Console.ReadLine() ?? string.Empty).Trim();

      if (parser(input, out T? value) && (isValid == null || isValid(value)))
      {
        return value;
      }

      Console.WriteLine(errorMessage);
    }
  }

  public static int NumberOfProperty(string message)
  {
    return Read<int>(
      message,
      "You need to enter a value higher than 0 and no more than 8. Please try again!",
      int.TryParse,
      wheels => wheels > 0 && wheels <= 8
    );
  }

  public static bool TextToBool(string message)
  {
    while (true)
    {
      int input = ConsoleMenu.MenuHandler([message], ["Yes", "No"]);

      switch (input)
      {
        case -1:
          continue;
        case 0:
          return true;
        case 1:
          return false;
        default:
          throw new ArgumentException("Ops, something went wrong!");
      }
    }
  }
}