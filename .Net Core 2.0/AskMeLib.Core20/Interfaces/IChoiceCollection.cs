﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public interface IChoiceCollection : IObjectBase {

    List<IChoice> Items { get; set; }

  }
}
