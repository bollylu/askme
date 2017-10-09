using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskMeLib;
using BLTools.MVVM;
using System.Windows;

namespace MakeQuestions {
  public class MainWindowVM : MVVMBase {

    public TRelayCommand FileOpenCommand { get; private set; }
    public TRelayCommand FileSaveCommand { get; private set; }
    public TRelayCommand FileSaveAsCommand { get; private set; }
    public TRelayCommand FileQuitCommand { get; private set; }
    public TRelayCommand HelpContactCommand { get; private set; }
    public TRelayCommand HelpAboutCommand { get; private set; }

    public TRepository SelectedRepository { get; set; }
    public List<TRepository> Repositories { get; set; }

    public MainWindowVM() {
      FileQuitCommand = new TRelayCommand(() => CmdQuit());
    }

    private void CmdQuit() {
      Application.Current.Shutdown();
    }

  }
}
