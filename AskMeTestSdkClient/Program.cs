using System;
using BLTools;
using BLTools.ConsoleExtension;
using BLTools.Debugging;
using AskMeSdkForCSharp;
using AskMeLib;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AskMeTestSdkClient {
  class Program {

    private static async Task Main(string[] args) {
      #region --- Program parameters and startup --------------------------------------------
      //TraceFactory.AddTraceConsole();
      ApplicationInfo.ApplicationStart();
      ConsoleExtension.Pause("Waiting for the server to start...", 5000, true, true);

      SplitArgs Args = new SplitArgs(args);

      string ServerRoot = Args.GetValue<string>("server", "http://localhost:25459");
      TAskMeServer.GlobalServerRoot = ServerRoot;
      #endregion --- Program parameters and startup --------------------------------------------

      string RepositoryName;
      do {
        RepositoryName = await SelectRepository();

        await DisplayRepositoryContent(RepositoryName);

      } while ( RepositoryName != "" );

      ApplicationInfo.ApplicationStop();
      ConsoleExtension.Pause();
    }

    private static async Task<string> SelectRepository() {
      List<string> RepositoryNames = new List<string>();

      try {
        using ( TAskMeServer Server = new TAskMeServer() ) {
          RepositoryNames.AddRange(await Server.GetRepositoryList());
        }
      } catch {
        return "";
      }

      RepositoryNames.Add("### Abort ###");

      int Index = ConsoleExtension.InputList(RepositoryNames, " Repository list ", "Please select a repository : ");
      if ( Index < RepositoryNames.Count ) {
        return RepositoryNames[Index - 1];
      } else {
        return "";
      }
    }

    private static async Task DisplayRepositoryContent(string repositoryName) {

      if (repositoryName=="") {
        return;
      }

      Console.WriteLine($"Content of {repositoryName}");

      using ( TAskMeServer Server = new TAskMeServer() ) {
        TRepository SelectedRepository = await Server.GetRepository(repositoryName);
        Console.WriteLine(SelectedRepository.ToString());
        foreach (TQuestionFile QuestionFileItem in await Server.GetRepositoryContent(repositoryName) ) {
          foreach(TQuestionCollection QuestionCollectionItem in QuestionFileItem.Items) {
            Ask(QuestionCollectionItem);
          }
        }
      }

    }

    private static void Ask(TQuestionCollection questions) {
      if (questions==null) {
        return;
      }
      questions.Render();
    }
  }
}
