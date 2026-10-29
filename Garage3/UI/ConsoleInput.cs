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
}