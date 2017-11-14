using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AskMeLib;
using System.IO;
using BLTools.Json;

namespace AskMeWebApi.Controllers {
  [Produces("application/json")]
  public class QuestionController : Controller {

    [HttpGet]
    [Route("api/Question/{repository}/{name}")]
    public JsonResult Get(string name = "") {
      if ( name == "" ) {
        JsonArray RetVal = new JsonArray();
        foreach(string RepositoryNameItem in Directory.GetFiles(TRepository.GlobalRepositoryRoot, "repository.xml", SearchOption.AllDirectories)) {
          RetVal.AddItem(new JsonString(Path.GetDirectoryName(RepositoryNameItem)));
        }
        return new JsonResult(RetVal.RenderAsString());
      } else {
        using ( TRepository Repository = new TRepository(name) ) {
          if ( !Repository.Open() ) {
            return new JsonResult(TRepository.Empty.ToJson().RenderAsString());
          }
          return new JsonResult(Repository.ToJson().RenderAsString());
        }
      }

    }

  }
}