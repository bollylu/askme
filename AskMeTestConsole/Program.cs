﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools.ConsoleExtension;
using AskMeLib;
using BLTools;
using BLTools.Debugging;
using System.IO;
using AskMeWebService;

namespace AskMeTestConsole {
  class Program {
    static void Main(string[] args) {

      SplitArgs Args = new SplitArgs(args);

      TraceFactory.AddTraceConsole();
      TraceFactory.AddTraceDefaultLogFilename();
      ApplicationInfo.ApplicationStart();

      if (Args.IsDefined("help") || Args.IsDefined("?")) {
        Usage();
      }

      string RepositoryPath = Args.GetValue<string>("repository", @".\");
      string Command = Args.GetValue<string>("command", "list");
      string DataFile = Args.GetValue<string>("file", "");
      string Category = Args.GetValue<string>("category", "");
      string Language = Args.GetValue<string>("language", "FR");


      if (Command == "load") {
        using (AskMeWebServiceClient Client = new AskMeWebServiceClient()) {
          TQuestionFileWCF TempFile = Client.GetQuestionFile(DataFile);
          IQuestionFile TestFile = new TQuestionFile(TempFile as IQuestionFile);
          if (TestFile == null) {
            Usage($"Data file is missing or access is denied : {DataFile}");
          }
          //TestFile.ReadData();
          foreach (TQuestionCollection QuestionsItem in TestFile.Items) {
            QuestionsItem.Ask();
          }
          ConsoleExtension.Pause(1000);
        }

      }

      if (Command == "list") {
        AskMeWebServiceClient Client = new AskMeWebServiceClient();

        Console.WriteLine(Client.GetRepositoryList());

        //using (TRepository Repository = new TRepository(RepositoryPath)) {
        //  Console.WriteLine(Repository.GetContentList(Category, Language));
        //}
        ConsoleExtension.Pause(10000);
      }

    }

    static void Usage(string message = "") {
      if (message != "") {
        Console.WriteLine(message);
      }
      Console.WriteLine($@"AskMe v{"0.1"}");
      Console.WriteLine(@"Usage: AskMe /help | /?");
      Console.WriteLine(@"             [/repository=<folder path> (default=.\)]");
      Console.WriteLine(@"             [/command=list (default)|load]");
      Console.WriteLine(@"             [/data=<data file>]");
      ConsoleExtension.Pause();
      Environment.Exit(1);
    }
  }
}
