namespace Garage3.Tests.Fakes;

// Jag vill testa min Read() funktion som hanterar användarens input.
// Problemet var att funktionen använder Console.ReadLine(), vilket
// normalt kräver att en användare skriver in information manuellt

// Den här hjälpfunktionen gör det möjligt att simulera användarens
// input och fånga upp programmets output under testerna.

// Console.In => Strömmen som Console.ReadLine() läser från
// Console.Out => Strömmer som Console.WriteLine() skriver till

// IDisposable används för att återställa konsolens ursprungliga
// input och output när testet är färdigt

// Jag har tagit hjälp av AI för denna hjälpfunktion
public sealed class ConsoleRedirect : IDisposable
{
  private readonly TextReader _originalIn = Console.In;
  private readonly TextWriter _originalOut = Console.Out;
  private readonly StringWriter _output = new();

  public ConsoleRedirect(params string[] inputLines)
  {
    Console.SetIn(new StringReader(string.Join(Environment.NewLine, inputLines)));
    Console.SetOut(_output);
  }

  public string Output => _output.ToString();
  public void Dispose()
  {
    Console.SetIn(_originalIn);
    Console.SetOut(_originalOut);
    _output.Dispose();
  }
}