using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AskMeWebApi {
  public class BLJsonActionResultChars : IActionResult {

    private char[] Value;

    public BLJsonActionResultChars(string result) {
      Value = result.ToCharArray();
    }

    public Task ExecuteResultAsync(ActionContext context) {
      context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
      context.HttpContext.Response.ContentType = "application/json";
      using ( BinaryWriter writer = new BinaryWriter(context.HttpContext.Response.Body) ) {
        writer.Write(Value);
        writer.Flush();
      }
      return Task.CompletedTask;
    }

  }
}
