using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMeWebApi {
  public class BLJsonActionResultString : IActionResult {

    private string Value;

    public BLJsonActionResultString(string result) {
      Value = result;
    }

    public async Task ExecuteResultAsync(ActionContext context) {
      context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
      context.HttpContext.Response.ContentType = "application/json";
      await context.HttpContext.Response.WriteAsync(Value);
    }

  }
}
