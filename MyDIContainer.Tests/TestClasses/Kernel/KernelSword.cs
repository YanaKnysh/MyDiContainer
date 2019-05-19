using System;
using System.Collections.Generic;
using System.Text;

namespace MyDIContainer.Tests.TestClasses.Kernel
{
    public class KernelSword : IKernelWeapon
    {
        [Inject]
        public int Count { get; set; }
    }
}
