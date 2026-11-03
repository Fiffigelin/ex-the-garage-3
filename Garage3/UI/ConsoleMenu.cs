using System;
using System.Collections.Generic;
using System.Linq;

namespace Garage3.UI;

public class MultibleMenuItem<T>
{
  public T Item { get; set; }
  public string Label { get; set; }

  public MultibleMenuItem(T item, string label)
  {
    Item = item;
    Label = label;
  }
}

// ändrade namn för kontinuitet
public static class ConsoleMenu
{
  public static int MenuHandler(List<string> previousTexts, List<string> options, bool useEscape = true)
  {
    if (options.Count == 0)
    {
      return -1;
    }

    int selectedIndex = 0;

    while (true)
    {
      ConsoleUI.DisplayMainTitle();

      foreach (string text in previousTexts)
      {
        Console.WriteLine(text);
      }

      for (int i = 0; i < options.Count; i++)
      {
        if (i == selectedIndex)
        {
          Console.BackgroundColor = ConsoleColor.Blue;
          Console.WriteLine($"> {options[i]} ");
          Console.ResetColor();
        }
        else
        {
          Console.WriteLine($"  {options[i]} ");
        }
      }

      Console.WriteLine();
      ConsoleKey consoleKey = Console.ReadKey(true).Key;

      switch (consoleKey)
      {
        case ConsoleKey.UpArrow:
          selectedIndex--;

          if (selectedIndex < 0)
          {
            selectedIndex = options.Count - 1;
          }
          break;
        case ConsoleKey.DownArrow:
          selectedIndex++;

          if (selectedIndex >= options.Count)
          {
            selectedIndex = 0;
          }
          break;
        case ConsoleKey.Enter:
          return selectedIndex;
        case ConsoleKey.Escape:
          if (useEscape)
          {
            return -1;
          }

          break;
        default:
          Console.WriteLine("Använd Enter för att välja.");
          break;
      }
    }
  }
  public static List<T> MultibleMenuHandler<T>(List<string> previousTexts, List<MultibleMenuItem<T>> options)
  {
    if (options.Count == 0)
    {
      return [];
    }

    List<T> selectedItems = [];
    int selectedIndex = 0;

    string[] additionalLabels = ["All", "Continue"];
    int menuCount = options.Count + additionalLabels.Length;

    while (true)
    {
      ConsoleUI.DisplayMainTitle();

      foreach (string text in previousTexts)
      {
        Console.WriteLine(text);
      }

      for (int i = 0; i < menuCount; i++)
      {
        bool isCurrent = i == selectedIndex;
        bool isAll = i == 0;
        bool isContinue = i == menuCount - 1;

        bool isSelected = !isAll && !isContinue &&
                          selectedItems.Contains(options[i - 1].Item);

        string label = isAll
          ? additionalLabels[0]
          : isContinue
            ? additionalLabels[1]
            : options[i - 1].Label;

        if (isCurrent)
        {
          Console.BackgroundColor = ConsoleColor.Blue;
          Console.Write("> ");
        }
        else if (isSelected)
        {
          Console.BackgroundColor = ConsoleColor.Cyan;
          Console.Write("✓ ");
        }
        else
        {
          Console.Write("  ");
        }

        Console.WriteLine($"{label}");
        Console.ResetColor();
      }

      Console.WriteLine();
      ConsoleKey consoleKey = Console.ReadKey(true).Key;

      switch (consoleKey)
      {
        case ConsoleKey.UpArrow:
          selectedIndex--;

          if (selectedIndex < 0)
          {
            selectedIndex = menuCount - 1;
          }
          break;
        case ConsoleKey.DownArrow:
          selectedIndex++;

          if (selectedIndex >= menuCount)
          {
            selectedIndex = 0;
          }
          break;
        case ConsoleKey.Enter:
          // exit
          if (selectedIndex == 0)
          {
            return [.. options.Select(option => option.Item)];
          }
          else if (selectedIndex == menuCount - 1)
          {
            return selectedItems;
          }

          // toggle item
          T item = options[selectedIndex - 1].Item;

          if (selectedItems.Contains(item))
          {
            selectedItems.Remove(item);
          }
          else
          {
            selectedItems.Add(item);
          }

          break;
        default:
          Console.WriteLine("Use Enter to choose one or more.");
          break;
      }
    }
  }
}
