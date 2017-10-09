using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Claims;
using System.Threading;

namespace SimpleRestApiCore {
  public class GetFilesHttpContext {
    public HttpContext Context { get; private set; }

    public string RepositoryPath {
      get {
        return Context.Request.Query["Repo"].FirstOrDefault();
      }
    }

    public GetFilesHttpContext(HttpContext context) {
      Context = context;
    }
  }
}
