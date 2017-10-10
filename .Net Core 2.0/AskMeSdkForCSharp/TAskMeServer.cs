using AskMeLib;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AskMeSdkForCSharp {
  public class TAskMeServer : IDisposable {
    public Uri ServerRoot { get; set; }

    public TAskMeServer(string serverUri) {
      ServerRoot = new Uri(serverUri);
    }

    public void Dispose() {
      ServerRoot = null;
    }

    public async Task<TRepository> GetRepository(string name="") {
      using (TRestApi RestApi = new TRestApi(ServerRoot)) {
        JsonString ValueFromServer = new JsonString(await RestApi.DoStringRequest($"repository/{name}"));
        if (RestApi.LastStatusCode==HttpStatusCode.OK) {
          return new TRepository(ValueFromServer);
        }
        return null;
      }
    }
  }
}
