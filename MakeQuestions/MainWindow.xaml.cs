using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLTools.WPF;

namespace MakeQuestions {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {

    public string AppTitle {
      get {
        return _AppTitle;
      }
      set {
        _AppTitle = value;
        this.Dispatch(() => {
          Title = _AppTitle;
        });
      }
    }
    private string _AppTitle;

    public MainWindow() {
      InitializeComponent();
      DataContext = new MainWindowVM();
    }
  }
}
