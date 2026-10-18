using System;

namespace Garage.ConsoleUtilities;

public static class TitleConsole
{
  /* 
  * Thank you for visiting https://asciiart.website/
  * This ASCII pic can be found at https://asciiart.website/art/5946"
  */
  public static void DisplayMainTitle()
  {
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("       .---;-,");
    Console.WriteLine("   __ /_,{)|__;._");
    Console.WriteLine(".''` _     :  _  `.  .:::;.    .::'");
    Console.WriteLine("'--(_)------(_)--' `      '::'");
    Console.ResetColor();
  }
}