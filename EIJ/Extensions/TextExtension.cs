#region Title Header

// Name: Phillip Smith
// 
// Solution: EIJ
// Project: EIJ
// File Name: TextExtension.cs
// 
// Current Data:
// 2021-02-13 7:50 PM
// 
// Creation Date:
// 2021-02-13 7:42 PM

#endregion

using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace EIJ.Extensions
{
  public class TextExtension : MarkupExtension
  {
    private readonly string _fileName;

    public TextExtension(string fileName)
    {
      _fileName = fileName;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      if (_fileName is null)
      {
        // Simple error handling
        return string.Empty;
      }

      var uri = new Uri("pack://application:,,,/" + _fileName);
      using (var stream = Application.GetResourceStream(uri)?.Stream)
      {
        using (var reader = new StreamReader(stream ?? throw new InvalidOperationException(), Encoding.UTF8))
        {
          return reader.ReadToEnd();
        }
      }
    }
  }
}