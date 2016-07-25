using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools.ConsoleExtension;
using AskMeLib;
using BLTools;
using BLTools.Debugging;
using System.IO;

namespace AskMeTestConsole {
  class Program {
    static void Main(string[] args) {

      SplitArgs Args = new SplitArgs(args);

      TraceFactory.AddTraceConsole();

      if (Args.IsDefined("help") || Args.IsDefined("?")) {
        Usage();
      }
      string Command = Args.GetValue<string>("command", "list");
      string DataFile = Args.GetValue<string>("data", "");

      if (Command=="load") {
        if (!File.Exists(DataFile)) {
          Usage("Data file is missing or access is denied");
        }
        

      }

      if (Command=="list") {
        foreach(string FileItem in Directory.GetFiles(DataFile, "*.qcm")) {
          TQuestionFile TestFile = new TQuestionFile(FileItem);
          TQuestionFileHeader Header = TestFile.ReadHeader();
          if (Header != null) {
            Console.Write($"File : {FileItem}");
            Console.Write($", Name : {Header.Name}");
            Console.Write($", Description : {Header.Description}");
            Console.Write($", Langage : {Header.Language}");
            Console.WriteLine();
          }
        }
        ConsoleExtension.Pause();
      }
      

    }

    static void Usage(string message="") {
      if (message!="") {
        Console.WriteLine(message);
      }
      Console.WriteLine($"AskMe v{"0.1"}");
      Console.WriteLine("Usage: AskMe /help | /?");
      Console.WriteLine("             [/Command=list (default)|load]");
      Console.WriteLine("             [/data=<data file>]");
      Environment.Exit(1);
    }
  }
}
