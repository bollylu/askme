using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AskMeLib;
using System.IO;
using Microsoft.Extensions.Logging;
using BLTools;
using BLTools.Json;
using Newtonsoft.Json;

namespace AskMeWebApi.Controllers {

  //[Produces("application/json")]
  public class RepositoryController : Controller {

    private readonly ILogger<RepositoryController> _Log;

    public RepositoryController(ILogger<RepositoryController> logger) {
      _Log = logger;
    }

    [HttpGet]
    [Route("api/Repository/{name?}")]
    public async Task<IActionResult> Get(string name = "") {
      await Task.Yield();
      _Log.LogDebug("Getting Repository");

      if ( name == "" ) {

        JsonArray RetVal = new JsonArray();
        foreach ( string RepositoryNameItem in Directory.GetFiles(TRepository.GlobalRepositoryRoot, "repository.xml", SearchOption.AllDirectories) ) {
          RetVal.AddItem(new JsonString(Path.GetDirectoryName(RepositoryNameItem)));
        }
        //return new BLJsonActionResultBytes(RetVal.RenderAsString().Replace("\\", "\\\\"));
        //return new BLJsonActionResultChars(RetVal.RenderAsString().Replace("\\","\\\\"));
        return new BLJsonActionResultString(RetVal.RenderAsString());

      } else {

        using ( TRepository Repository = new TRepository(name) ) {

          if ( !Repository.Open() ) {
            return NotFound(TRepository.Empty.ToJson().RenderAsString());
          }

          JsonObject RetVal = Repository.ToJson() as JsonObject;
          return new BLJsonActionResultString(RetVal.RenderAsString());

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