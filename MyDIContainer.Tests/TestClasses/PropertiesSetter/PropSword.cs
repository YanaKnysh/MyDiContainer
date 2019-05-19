using System;
using System.Collections.Generic;
using System.Text;

namespace MyDIContainer.Tests.TestClasses.PropertiesSetter
{
    public class PropSword
    {
        [Inject]
        public PropMaterial Material { get; set; }
    }
}
