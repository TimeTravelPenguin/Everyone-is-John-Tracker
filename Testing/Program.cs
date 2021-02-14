#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: Testing
// File Name: Program.cs
// 
// Current Data:
// 2021-02-14 8:56 PM
// 
// Creation Date:
// 2021-02-14 8:56 PM

#endregion

using System;
using EIJ.Models.DiceRoller;

namespace Testing
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var dr = new DiceRollPattern("2d  6+1 1");
      Console.WriteLine();
    }
  }
}