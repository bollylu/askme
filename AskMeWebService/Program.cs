using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using BLTools.ConsoleExtension;

namespace AskMeWebService {
  class Program {
    static void Main(string[] args) {

      Uri BaseAddress = new Uri("http://10.100.200.252:8888/askme");
      try {
        using (ServiceHost Host = new ServiceHost(typeof(TAskMeWebService), BaseAddress)) {
          ServiceMetadataBehavior Behavior = new ServiceMetadataBehavior();
          Behavior.HttpGetEnabled = true;
          Behavior.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
          Host.Description.Behaviors.Add(Behavior);

          Host.Open();

          ConsoleExtension.Pause($"The service is running at {BaseAddress.ToString()}");

          Host.Close();
        }
      } catch (Exception ex) {
        ConsoleExtension.Pause(ex.Message);
      }
      Environment.Exit(0);
    }
  }
}
