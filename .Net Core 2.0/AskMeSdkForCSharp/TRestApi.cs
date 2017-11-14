using AskMeLib;
using BLTools.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BLTools;

namespace AskMeSdkForCSharp {
  public class TRestApi : IDisposable {

    private HttpClient Client = new HttpClient();

    public HttpStatusCode LastStatusCode { get; private set; }

    public TRestApi(string baseAddress) {
      Client.BaseAddress = new Uri(baseAddress);
    }
    public TRestApi(Uri baseAddress) {
      Client.BaseAddress = baseAddress;
    }

    public async Task<HttpResponseMessage> DoRequest(string request, string body = "") {
      HttpResponseMessage Response;
      if ( string.IsNullOrWhiteSpace(body) ) {
        Response = await Client.GetAsync(request);
      } else {
        StringContent Content = new StringContent(body);
        Response = await Client.PostAsync(request, Content);
      }

      return Response;
    }

    public async Task<IJsonValue> DoJsonStringRequest(string request) {
      string Response = await DoStringRequest(request, null);
      return JsonValue.Parse(Response);
    }
    public async Task<IJsonValue> DoJsonStringRequest(string request, IJsonValue body) {
      return JsonValue.Parse(await DoStringRequest(request, body));
    }

    public async Task<string> DoStringRequest(string request) {
      return await DoStringRequest(request, null);
    }
    public async Task<string> DoStringRequest(string request, IJsonValue body) {
      Trace.WriteLine($"Request : {request}");
      HttpResponseMessage RequestResponse;
      if ( body == null ) {
        RequestResponse = await DoRequest(request);
      } else {
        RequestResponse = await DoRequest(request, body.RenderAsString());
      }
      LastStatusCode = RequestResponse.StatusCode;
      if ( !RequestResponse.IsSuccessStatusCode ) {
        string Error = $"Request failed : {RequestResponse.StatusCode} : {RequestResponse.ReasonPhrase}";
        Trace.WriteLine(Error);
        throw new ApplicationException(Error);
      }
      HttpContent ResultContent = RequestResponse.Content;
      byte[] Temp = await ResultContent.ReadAsByteArrayAsync();
      string RetVal = Encoding.UTF8.GetString(Temp);

      return RetVal;

    }

    public async Task<byte[]> DoBytesRequest(string request) {
      return await DoBytesRequest(request, null);
    }

    public async Task<byte[]> DoBytesRequest(string request, IJsonValue body) {
      Console.WriteLine($"Request : {request}");
      HttpResponseMessage RequestResponse;
      if ( body == null ) {
        RequestResponse = await DoRequest(request);
      } else {
        RequestResponse = await DoRequest(request, body.RenderAsString());
      }
      LastStatusCode = RequestResponse.StatusCode;
      if ( !RequestResponse.IsSuccessStatusCode ) {
        Console.WriteLine($"Request failed : {RequestResponse.StatusCode} : {RequestResponse.ReasonPhrase}");
        return null;
      }
      return await RequestResponse.Content.ReadAsByteArrayAsync();
    }

    public void Dispose() {
      Client.Dispose();
    }
  }
}
