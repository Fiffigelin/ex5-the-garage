using System;
using System.Data;
using Garage.Domain.Vehicles;

namespace Garage.ConsoleUtilities;

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
public static class GenericTable
{
  public static void PrintTable<T>(string[] header, string[] columnHeader, int[] columnWidths, RowData<T>[] rows, string[] footer) where T : class
  {
    int selectedIndex = 0;
    string line = new string('-', CalculateTableWidth(columnWidths));
    string columns = FormatRow(columnHeader, columnWidths);
    while (true)
    {
      Console.Clear();
      TitleConsole.DisplayMainTitle();
      Console.WriteLine();

      foreach (string s in header)
      {
        Console.WriteLine(s);
      }

      Console.WriteLine($"\n{line}");
      Console.WriteLine(columns);
      Console.WriteLine(line);

      if (rows.Length == 0)
      {
        Console.WriteLine("   There's no vehicles parked.");
      }
      else
      {
        for (int i = 0; i < rows.Length; i++)
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
          foreach (string f in footer)
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
            selectedIndex = rows.Length - 1;
          }
          break;
        case ConsoleKey.DownArrow:
          selectedIndex++;

          if (selectedIndex >= rows.Length)
          {
            selectedIndex = 0;
          }
          break;
        case ConsoleKey.Enter:

          if (rows.Length == 0)
          {
            break;
          }

          bool isRemoved = rows[selectedIndex].OnSelect.Invoke();

          if (isRemoved)
          {
            rows = ResizeRows(rows, rows[selectedIndex]);
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

  public static RowData<T> FormatDataRow<T>(T data, string[] texts, int[] columnWidths, Func<bool> function) where T : class
  {
    return new RowData<T>(data, FormatRow(texts, columnWidths), function);
  }

  private static string FormatRow(string[] header, int[] columnWidth)
  {
    string[] formatted = new string[columnWidth.Length];

    for (int i = 0; i < columnWidth.Length; i++)
    {
      int width = Math.Abs(columnWidth[i]);
      string text = Truncate(header[i], width);

      formatted[i] = columnWidth[i] < 0 ? text.PadRight(width) : text.PadLeft(width);
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

  private static int CalculateTableWidth(int[] widths)
  {
    int sum = 0;
    foreach (int i in widths)
    {
      sum += Math.Abs(i);
    }

    return sum + (widths.Length - 1) * 3;
  }

  private static RowData<T>[] ResizeRows<T>(RowData<T>[] rows, RowData<T> data) where T : class
  {
    RowData<T>[] newRows = new RowData<T>[rows.Length - 1];
    int index = 0;

    for (int i = 0; i < rows.Length; i++)
    {
      // så bra saker man hittar och lär sig mer om
      // https://learn.microsoft.com/en-us/dotnet/api/system.array.indexof?view=net-10.0
      if (i == Array.IndexOf(rows, data))
      {
        continue;
      }

      newRows[index] = rows[i];
      index++;
    }

    return newRows;
  }
}