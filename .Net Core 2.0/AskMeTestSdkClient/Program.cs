using System;
using BLTools;
using BLTools.ConsoleExtension;
using BLTools.Debugging;
using AskMeSdkForCSharp;
using AskMeLib;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AskMeTestSdkClient {
  class Program {
    static void Main(string[] args) {
      Task.Run(() => MainAsync(args)).Wait();
    }

    private static async Task MainAsync(string[] args) {
      TraceFactory.AddTraceConsole();
      ApplicationInfo.ApplicationStart();
      ConsoleExtension.Pause("Waiting for the server to start...", 5000, true, true);

      SplitArgs Args = new SplitArgs(args);

      string ServerRoot = Args.GetValue<string>("server", "http://localhost:25459");
      TAskMeServer.GlobalServerRoot = ServerRoot;

      Trace.WriteLine($"Request to server {ServerRoot}");

      try {
        TRepository Repository;
        using ( TAskMeServer Server = new TAskMeServer() ) {

          Repository = await Server.GetRepository();
          if ( Repository != null ) {
            Console.WriteLine(Repository.ToString());
          }

          Repository = await Server.GetRepository("SecondRepository");
          if ( Repository != null ) {
            Console.WriteLine(Repository.ToString());
          }
        }
      } catch { }

      ApplicationInfo.ApplicationStop();
      ConsoleExtension.Pause();
    }
  }
}
