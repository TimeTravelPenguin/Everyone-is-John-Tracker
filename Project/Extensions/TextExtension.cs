#region Title Header

// Name: Phillip Smith
// 
// Solution: EveryoneIsJohn
// Project: EveryoneIsJohn
// File Name: TextExtension.cs
// 
// Current Data:
// 2021-02-08 5:42 PM
// 
// Creation Date:
// 2021-02-08 5:28 PM

#endregion

using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace EveryoneIsJohn.Extensions
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