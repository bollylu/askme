using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public interface IXmlBase {
    string Name { get; set; }
    string Description { get; set; }

    string StorageLocation { get; set; }
  }
}
