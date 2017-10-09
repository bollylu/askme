using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BLTools;
using System.Diagnostics;
using BLTools.Debugging;
using System.IO;

namespace MakeQuestions {
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application {
    public const string PARAM_LOGFILE = "log";
    public const string PARAM_LOGBASE = "logbase";
    public const string PARAM_CONFIG = "config";

    public const string DEFAULT_PROD_LOGBASE = @"c:\Logs";
    public const string DEFAULT_DEV_LOGBASE = @"c:\Logs";
    public const string DEFAULT_CONFIG = "config.xml";

    public static SplitArgs Args;

    public static bool InDesignMode {
      get {
        return !(Application.Current is App);
      }
    }
    public static bool SecurityLevelAdvanced = false;

    public static string RunningUser = $@"{Environment.UserDomainName}\{Environment.UserName}";

    

    private void Application_Startup(object sender, StartupEventArgs e) {
      Args = new SplitArgs(Environment.GetCommandLineArgs());

      Trace.AutoFlush = true;
      SetLogDestination();

      ApplicationInfo.ApplicationStart();
    }

    private void Application_Exit(object sender, ExitEventArgs e) {
      ApplicationInfo.ApplicationStop();
    }

    public static string GetPictureFullname(string name = "default") {
      return $"/MakeQuestions;component/Pictures/{name}.png";
    }

    public static void SetLogDestination() {
      string LogBase;
      if (!System.Diagnostics.Debugger.IsAttached) {
        LogBase = Args.GetValue<string>(PARAM_LOGBASE, DEFAULT_PROD_LOGBASE);
      } else {
        LogBase = Args.GetValue<string>(PARAM_LOGBASE, DEFAULT_DEV_LOGBASE);
      }

      string LogFile = Args.GetValue<string>(PARAM_LOGFILE, "");

      string ConfigFile = Args.GetValue<string>(PARAM_CONFIG, DEFAULT_CONFIG);

      Trace.Listeners.Clear();
      Trace.Listeners.Add(new DefaultTraceListener());

      if (LogFile != "") {
        Trace.Listeners.Add(new TimeStampTraceListener(Path.Combine(LogBase, LogFile)));
      } else {
        string LogFilename = Path.Combine(LogBase, $"{ Path.GetFileNameWithoutExtension(TraceFactory.GetTraceDefaultLogFilename()) }-{ Path.GetFileNameWithoutExtension(ConfigFile) }.log");
        Trace.Listeners.Add(new TimeStampTraceListener(LogFilename));
      }

      foreach (TraceListener TraceListenerItem in Trace.Listeners) {
        if (TraceListenerItem is TimeStampTraceListener) {
          TimeStampTraceListener LocalTimestampTraceListener = TraceListenerItem as TimeStampTraceListener;
          LocalTimestampTraceListener.DisplayUserId = true;
          LocalTimestampTraceListener.DisplayComputerName = true;
        }
      }

    }
  }
}
