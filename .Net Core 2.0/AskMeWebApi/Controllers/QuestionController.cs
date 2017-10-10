using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AskMeLib;
using System.IO;

namespace AskMeWebApi.Controllers {
  [Produces("application/json")]
  public class QuestionController : Controller {

    [HttpGet]
    [Route("api/Question/{repository}/{name}")]
    public JsonResult Get(string name = "") {
      if ( name == "" ) {
        List<string> RetVal = new List<string>();
        foreach(string RepositoryNameItem in Directory.GetFiles(TRepository.GlobalRepositoryRoot, "repository.xml", SearchOption.AllDirectories)) {
          RetVal.Add(Path.GetDirectoryName(RepositoryNameItem));
        }
        return new JsonResult(RetVal);
      } else {
        using ( TRepository Repository = new TRepository(name) ) {
          if ( !Repository.Open() ) {
            return new JsonResult(TRepository.Empty);
          }
          return new JsonResult(Repository.ToJson().Content);
        }
      }

    }

  }
}