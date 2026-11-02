using System;
using System.Collections.Generic;

namespace Garage3.UI.Table;

public class RowData<T> where T : class
{
  public T Data { get; }
  public string Row { get; }
  public Func<bool> OnSelect { get; }

  public RowData(
    T data, string row, Func<bool> onSelect)
  {
    Data = data;
    Row = row;
    OnSelect = onSelect;
  }
}

// namnbyte
// jisses att gå tillbaka till sin kod, ska skriva lite kommentarer XD
public static class Table
{
  public static void PrintTable<T>(
    Func<List<string>> header,
    List<string> columnHeader,
    List<int> columnWidths,
    List<RowData<T>> rows,
    Func<List<string>> footer) where T : class
  {
    int selectedIndex = 0;
    string line = new string('-', CalculateTableWidth(columnWidths));
    string columns = FormatRow(columnHeader, columnWidths);
    while (true)
    {
      Console.Clear();
      ConsoleUI.DisplayMainTitle();
      Console.WriteLine();

      foreach (string s in header())
      {
        Console.WriteLine(s);
      }

      Console.WriteLine("View Vehicle with 'Enter' or Exit with 'Escape'");

      Console.WriteLine($"\n{line}");
      Console.WriteLine(columns);
      Console.WriteLine(line);

      if (rows.Count == 0)
      {
        Console.WriteLine("   There's no vehicles parked.");
      }
      else
      {
        for (int i = 0; i < rows.Count; i++)
        {
          if (i == selectedIndex)
          {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{rows[i].Row}");
            Console.ResetColor();
          }
          else
          {
            Console.WriteLine($"{rows[i].Row}");
          }
        }
        Console.WriteLine(line);

        if (footer is not null)
        {
          foreach (string f in footer())
          {
            Console.WriteLine(f);
          }
        }
      }

      ConsoleKey consoleKeykey = Console.ReadKey(true).Key;

      switch (consoleKeykey)
      {
        case ConsoleKey.UpArrow:
          selectedIndex--;

          if (selectedIndex < 0)
          {
            selectedIndex = rows.Count - 1;
          }
          break;
        case ConsoleKey.DownArrow:
          selectedIndex++;

          if (selectedIndex >= rows.Count)
          {
            selectedIndex = 0;
          }
          break;
        case ConsoleKey.Enter:

          if (rows.Count == 0)
          {
            continue;
          }

          bool isRemoved = rows[selectedIndex].OnSelect.Invoke();

          if (isRemoved)
          {
            rows = ResizeRows(rows, rows[selectedIndex]);
            selectedIndex = Math.Max(0, Math.Min(selectedIndex, rows.Count - 1));
          }
          continue;
        case ConsoleKey.Escape:
          return;
        default:
          Console.WriteLine("Wrong key.");
          break;
      }
    }
  }

  // table utan att man ska kunna bläddra i den.
  // kan baka in denna i den första tabellen
  // men det är inte huvudsyftet med uppgiften vilket jag alltid missar
  // sidequest-mania!
  public static void PrintNoIntegrationTable<T>(
    Func<List<string>> header,
    List<string> columnHeader,
    List<int> columnWidths,
    List<RowData<T>> rows) where T : class
  {
    string line = new string('-', CalculateTableWidth(columnWidths));
    string columns = FormatRow(columnHeader, columnWidths);

    foreach (string s in header())
    {
      Console.WriteLine(s);
    }

    Console.WriteLine("Exit with 'Escape'");
    Console.WriteLine($"\n{line}");
    Console.WriteLine(columns);
    Console.WriteLine(line);

    if (rows.Count == 0)
    {
      Console.WriteLine("   There's no vehicles parked.");
    }
    else
    {
      for (int i = 0; i < rows.Count; i++)
      {
        Console.WriteLine($"{rows[i].Row}");
      }
      Console.WriteLine(line);
    }

  }

  public static RowData<T> FormatDataRow<T>(T data, List<string> texts, List<int> columnWidths, Func<bool> function) where T : class
  {
    return new RowData<T>(data, FormatRow(texts, columnWidths), function);
  }

  private static string FormatRow(List<string> header, List<int> columnWidth)
  {
    List<string> formatted = [];

    for (int i = 0; i < columnWidth.Count; i++)
    {
      int width = Math.Abs(columnWidth[i]);
      string text = Truncate(header[i], width);

      formatted.Add(columnWidth[i] < 0 ? text.PadRight(width) : text.PadLeft(width));
    }
    return string.Join(" | ", formatted);
  }

  private static string Truncate(string text, int width)
  {
    if (text.Length <= width)
    {
      return text;
    }

    return width == 1 ? text[..1] : text[..(width - 1)] + "...";
  }

  private static int CalculateTableWidth(List<int> widths)
  {
    int sum = 0;
    foreach (int i in widths)
    {
      sum += Math.Abs(i);
    }

    return sum + (widths.Count - 1) * 3;
  }

  // eftersom det är en konsoll och inte något som dynamiskt ändras behöver denna köras för att skriva om tabellen
  private static List<RowData<T>> ResizeRows<T>(List<RowData<T>> rows, RowData<T> data) where T : class
  {
    List<RowData<T>> newRows = rows;

    newRows.Remove(data);
    return newRows;
  }
}