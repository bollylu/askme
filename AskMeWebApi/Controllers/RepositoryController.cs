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

  public class RepositoryController : Controller {

    private readonly ILogger<RepositoryController> _Log;

    public RepositoryController(ILogger<RepositoryController> logger) {
      _Log = logger;
    }

    [HttpGet]
    [Route("api/Repository")]
    public async Task<IActionResult> GetRepositoryList(string name = "") {
      await Task.Yield();
      _Log.LogDebug("Getting repository list");

      JsonArray RetVal = new JsonArray();
      foreach ( string RepositoryNameItem in Directory.EnumerateFiles(TRepository.GlobalRepositoryRoot, "repository.xml", SearchOption.AllDirectories) ) {
        string DirectoryName = Path.GetDirectoryName(RepositoryNameItem);
        RetVal.Add(new JsonString(DirectoryName.Split("\\").Last()));
      }
      return new BLJsonActionResultString(RetVal.RenderAsString());

    }

    [HttpGet]
    [Route("api/Repository/{name}")]
    public async Task<IActionResult> GetRepository(string name = "") {
      await Task.Yield();
      _Log.LogDebug("Getting Repository");

      using ( TRepository Repository = new TRepository(name) ) {

        if ( !Repository.Open() ) {
          return NotFound(TRepository.Empty.ToJson().RenderAsString());
        }

        JsonObject RetVal = Repository.ToJson() as JsonObject;
        return new BLJsonActionResultString(RetVal.RenderAsString());

      }

    }


    [HttpGet]
    [Route("api/Repository/GetContentList/{name}")]
    public async Task<IActionResult> GetRepositoryContent(string name = "") {
      await Task.Yield();
      _Log.LogDebug($"Getting repository {name} content");

      using ( TRepository Repository = new TRepository(name) ) {
        if ( !Repository.Open() ) {
          return new BLJsonActionResultString(JsonNull.Default.RenderAsString());
        }
        
        JsonArray RetVal = new JsonArray();
        foreach ( IQuestionFile QFileItem in Repository.GetContent() ) {
          RetVal.Add(new JsonString(QFileItem.Name));
        }
        return new BLJsonActionResultString(RetVal.RenderAsString());
      }

    }

    [HttpGet]
    [Route("api/Repository/GetContent/{name}")]
    public async Task<IActionResult> GetRepositoryQuestions(string name = "") {
      await Task.Yield();
      _Log.LogDebug($"Getting repository {name} questions files");

      using ( TRepository Repository = new TRepository(name) ) {
        if ( !Repository.Open() ) {
          return new BLJsonActionResultString(JsonNull.Default.RenderAsString());
        }

        JsonArray RetVal = new JsonArray();
        foreach ( IQuestionFile QFileItem in Repository.GetContent() ) {
          RetVal.Add(QFileItem.ToJson());
        }
        return new BLJsonActionResultString(RetVal.RenderAsString());
      }

    }
  }




}