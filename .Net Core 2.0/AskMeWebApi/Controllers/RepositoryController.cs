using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AskMeLib;

namespace AskMeWebApi.Controllers {
  [Produces("application/json")]
  public class RepositoryController : Controller {

    [HttpGet]
    [Route("api/Repository/{name?}")]
    public JsonResult Get(string name = "") {
      using ( TRepository Repository = new TRepository(name) ) {
        if ( !Repository.Open() ) {
          return new JsonResult(null);
        }
        return new JsonResult(Repository.ToJson());
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