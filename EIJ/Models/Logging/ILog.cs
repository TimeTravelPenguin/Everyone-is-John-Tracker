#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: ILog.cs
// 
// Current Data:
// 2021-02-13 7:50 PM
// 
// Creation Date:
// 2021-02-13 7:42 PM

#endregion

using System;

namespace EIJ.Models.Logging
{
  public interface ILog
  {
    string LogMessage { get; }
    DateTime TimeStamp { get; }
    string LogTimeStamp { get; }
  }
}