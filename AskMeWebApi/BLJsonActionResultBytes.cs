using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AskMeWebApi {
  public class BLJsonActionResultBytes : IActionResult {

    private byte[] Value;

    public BLJsonActionResultBytes(byte[] result) {
      Value = new byte[result.Length];
      Buffer.BlockCopy(result, 0, Value, 0, result.Length);
    }

    public BLJsonActionResultBytes(string result) {
      Value = result.Select<char, byte>(x => (byte)x).ToArray();
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
