using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
using Microsoft.Net.Http.Headers;

namespace AskMeWebApi {
  public class BLJsonOutputFormatter : Microsoft.AspNetCore.Mvc.Formatters.TextOutputFormatter {

    public BLJsonOutputFormatter() {
      SupportedEncodings.Add(Encoding.UTF8);
      SupportedEncodings.Add(Encoding.Unicode);
      SupportedMediaTypes.Add(MediaTypeHeaderValues.ApplicationJson);
      SupportedMediaTypes.Add(MediaTypeHeaderValues.TextJson);
      SupportedMediaTypes.Add(MediaTypeHeaderValues.ApplicationAnyJsonSyntax);
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding) {
      if ( context == null ) {
        throw new ArgumentNullException(nameof(context));
      }

      if ( selectedEncoding == null ) {
        throw new ArgumentNullException(nameof(selectedEncoding));
      }

      using ( var writer = context.WriterFactory(context.HttpContext.Response.Body, selectedEncoding) ) {
        if (context.Object is string StringContent) {
          await writer.WriteAsync(StringContent);
          await writer.FlushAsync();
        }
        if ( context.Object is byte[] BytesContent ) {
          await writer.WriteAsync(Encoding.UTF8.GetString(BytesContent));
          await writer.FlushAsync();
        }
      }
    }
  }

  internal class MediaTypeHeaderValues {
    public static readonly MediaTypeHeaderValue ApplicationJson
        = MediaTypeHeaderValue.Parse("application/json").CopyAsReadOnly();

    public static readonly MediaTypeHeaderValue TextJson
        = MediaTypeHeaderValue.Parse("text/json").CopyAsReadOnly();

    public static readonly MediaTypeHeaderValue ApplicationJsonPatch
        = MediaTypeHeaderValue.Parse("application/json-patch+json").CopyAsReadOnly();

    public static readonly MediaTypeHeaderValue ApplicationAnyJsonSyntax
        = MediaTypeHeaderValue.Parse("application/*+json").CopyAsReadOnly();
  }
}
