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
      ConsoleExtension.Pause("Waiting for the server to start...", 5000, true, true);

      SplitArgs Args = new SplitArgs(args);

      string ServerRoot = Args.GetValue<string>("server", "http://localhost:25459/api");

      Trace.WriteLine($"Request to server {ServerRoot}");

      try {
        using ( TAskMeServer Server = new TAskMeServer(ServerRoot) ) {
          TRepository Repository = await Server.GetRepository(TRepository.DEFAULT_REPOSITORY_PATH);
          if ( Repository != null ) {
            Console.WriteLine(Repository.ToString());
          }
        }
      } catch { }

      ConsoleExtension.Pause();
    }
  }
}
