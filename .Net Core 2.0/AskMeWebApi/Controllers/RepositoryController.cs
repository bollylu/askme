using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AskMeLib;
using System.IO;
using Microsoft.Extensions.Logging;

namespace AskMeWebApi.Controllers {

  [Produces("application/json")]
  public class RepositoryController : Controller {

    private readonly ILogger<RepositoryController> _Log;

    public RepositoryController(ILogger<RepositoryController> logger) {
      _Log = logger;
    }

    [HttpGet]
    [Route("api/Repository/{name?}")]
    public JsonResult Get(string name = "") {
      _Log.LogDebug("Getting Repository");
      if ( name == "" ) {
        List<string> RetVal = new List<string>();
        foreach ( string RepositoryNameItem in Directory.GetFiles(TRepository.GlobalRepositoryRoot, "repository.xml", SearchOption.AllDirectories) ) {
          RetVal.Add(Path.GetDirectoryName(RepositoryNameItem));
        }
        return new JsonResult(RetVal);
      } else {
        using ( TRepository Repository = new TRepository(name) ) {
          if ( !Repository.Open() ) {
            return new JsonResult(TRepository.Empty);
          }
          return new JsonResult(Repository);
        }
      }

    }

    [HttpGet]
    [Route("api/Repository/GetContent")]
    public JsonResult GetContent(string name = "") {
      using ( TRepository Repository = new TRepository(name) ) {
        if ( !Repository.Open() ) {
          return new JsonResult(null);
        }
        return new JsonResult(Repository.QFiles);
      }

    }
  }
}