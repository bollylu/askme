using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using BLTools;
using AskMeLib;
using System.Text;

namespace SimpleRestApiCore {
  // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
  public class AskMeMW {
    private readonly RequestDelegate _next;

    public AskMeMW(RequestDelegate next) {
      _next = next;
    }

    public Task Invoke(HttpContext httpContext) {
      Console.WriteLine($"Request for {httpContext.Request.Path} received ({httpContext.Request.ContentLength ?? 0} bytes)");

      switch (httpContext) {
        case GetFilesContext is GetFilesHttpContext:
          break;
      }
      IQueryCollection QueryArgs = httpContext.Request.Query;

      string RepositoryPath;
      try {
        RepositoryPath = QueryArgs["repo"].First();
      } catch {
        RepositoryPath = "data";
      }

      int ResponseCode = 404;
      StringBuilder ResponseBody = new StringBuilder();

      if ( QueryArgs.ContainsKey("repo") ) {
        using ( IRepository LocalRepository = new TRepository(RepositoryPath) ) {
          if ( LocalRepository.Open() ) {
            ResponseBody.AppendLine($"Repository : {LocalRepository.ToString()}");
            ResponseBody.AppendLine(LocalRepository.GetContentList());
            ResponseCode = 201;
          }
        }
      }

      if ( QueryArgs.ContainsKey("files") ) {
        GetFilesHttpContext GetFilesContext = new GetFilesHttpContext(httpContext);
        using ( IRepository LocalRepository = new TRepository(GetFilesContext.RepositoryPath) ) {
          if ( LocalRepository.Open() ) {
            IEnumerable<IQuestionFile> Files = LocalRepository.GetContent();
            foreach ( IQuestionFile QuestionFileItem in Files ) {
              QuestionFileItem.ReadHeader();
              QuestionFileItem.ReadData();
              ResponseBody.Append(QuestionFileItem.ToJSon());
            }
            ResponseCode = 201;
          }
        }
      }

      httpContext.Response.StatusCode = ResponseCode;
      byte[] Output = Encoding.UTF8.GetBytes(ResponseBody.ToString());
      httpContext.Response.Body.Write(Output, 0, Output.Length);





      return Task.FromResult(0); // _next(httpContext);
    }
  }

  // Extension method used to add the middleware to the HTTP request pipeline.
  public static class AskMeMWExtensions {
    public static IApplicationBuilder UseAskMeMW(this IApplicationBuilder builder) {
      return builder.UseMiddleware<AskMeMW>();
    }
  }
}
