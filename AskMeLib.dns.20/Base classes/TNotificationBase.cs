using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools;

namespace AskMeLib {
  public class TNotificationBase {

    public static event EventHandler<IntAndMessageEventArgs> OnNotifyProgress;
    public static event EventHandler<IntAndMessageEventArgs> OnNotifyError;

    internal void NotifyProgress(string message, ErrorLevel errorLevel = ErrorLevel.Info) {
      if (OnNotifyProgress != null) {
        OnNotifyProgress(null, new IntAndMessageEventArgs((int)errorLevel, message));
      }
    }

    internal void NotifyError(string message, ErrorLevel errorLevel = ErrorLevel.Warning) {
      if (OnNotifyError != null) {
        OnNotifyError(null, new IntAndMessageEventArgs((int)errorLevel, message));
      }
    }

  }

  public static class Notifyer {

    private static TNotificationBase Notify {
      get {
        if (_Notify == null) {
          _Notify = new TNotificationBase();
        }
        return _Notify;
      }
    }
    private static TNotificationBase _Notify;

    public static void NotifyProgress(string message, ErrorLevel errorLevel = ErrorLevel.Info) {
      Notify.NotifyProgress(message, errorLevel);
    }

    public static void NotifyError(string message, ErrorLevel errorLevel = ErrorLevel.Warning) {
      Notify.NotifyError(message, errorLevel);
    }
  }
}
