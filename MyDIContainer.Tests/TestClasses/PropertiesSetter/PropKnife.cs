using System;
using System.Collections.Generic;
using System.Text;

namespace MyDIContainer.Tests.TestClasses.PropertiesSetter
{
    public class PropKnife
    {
        [Inject]
        public int Count { get; set; }
    }
}
