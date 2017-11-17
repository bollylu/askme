using AskMeLib;
using BLTools.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AskMeSdkForCSharp {
  public class TAskMeServer : IDisposable {

    public const string DEFAULT_SERVER_ROOT = "http://localhost:34567";

    public static string GlobalServerRoot {
      get {
        if ( string.IsNullOrWhiteSpace(_GlobalServerRoot) ) {
          return DEFAULT_SERVER_ROOT;
        }
        return _GlobalServerRoot;
      }
      set {
        _GlobalServerRoot = value;
      }
    }
    private static string _GlobalServerRoot;

    public Uri ServerRoot {
      get {
        if ( _ServerRoot == null ) {
          return new Uri(GlobalServerRoot);
        }
        return _ServerRoot;
      }
      set {
        _ServerRoot = value;
      }
    }
    private Uri _ServerRoot;

    #region --- Constructor(s) ---------------------------------------------------------------------------------
    public TAskMeServer() {
    }

    public TAskMeServer(string serverUri) {
      ServerRoot = new Uri(serverUri);
    }

    public TAskMeServer(Uri serverUri) {
      ServerRoot = serverUri;
    }
    public void Dispose() {
      ServerRoot = null;
    }
    #endregion --- Constructor(s) ------------------------------------------------------------------------------

    public async Task<TRepository> GetRepository(string name = TRepository.DEFAULT_REPOSITORY_PATH) {
      using ( TRestApi RestApi = new TRestApi(ServerRoot) ) {
        IJsonValue ValueFromServer = await RestApi.DoJsonStringRequest($"api/repository/{name}");
        if ( RestApi.LastStatusCode != HttpStatusCode.OK ) {
          return null;
        }
        return new TRepository(ValueFromServer);
      }
    }

    public async Task<List<string>> GetRepositoryList() {
      using ( TRestApi RestApi = new TRestApi(ServerRoot) ) {
        JsonArray ValueFromServer = await RestApi.DoJsonStringRequest($"api/repository/") as JsonArray;
        if ( RestApi.LastStatusCode != HttpStatusCode.OK ) {
          return new List<string>();
        }
        return ValueFromServer.OfType<JsonString>().Select(x => x.Value).ToList();
      }
    }

    public async Task<List<string>> GetRepositoryContentList(string name) {
      using ( TRestApi RestApi = new TRestApi(ServerRoot) ) {
        JsonArray ValueFromServer = await RestApi.DoJsonStringRequest($"api/repository/GetContent/{name}") as JsonArray;
        if ( RestApi.LastStatusCode != HttpStatusCode.OK ) {
          return null;
        }
        return ValueFromServer.OfType<JsonString>().Select(x => x.Value).ToList();
      }
    }

    public async Task<List<TQuestionFile>> GetRepositoryContent(string name) {
      using ( TRestApi RestApi = new TRestApi(ServerRoot) ) {
        IJsonValue ValueFromServer = await RestApi.DoJsonStringRequest($"api/repository/GetContent/{name}");
        if ( RestApi.LastStatusCode != HttpStatusCode.OK ) {
          return null;
        }

        List<TQuestionFile> RetVal = new List<TQuestionFile>();
        foreach(JsonObject JsonQuestionFileItem in ValueFromServer as JsonArray) {
          RetVal.Add(new TQuestionFile(JsonQuestionFileItem));
        }

        return RetVal;
      }
    }
  }
}
