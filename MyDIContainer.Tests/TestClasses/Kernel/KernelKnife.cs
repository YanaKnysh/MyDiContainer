using System;
using System.Collections.Generic;
using System.Text;

namespace MyDIContainer.Tests.TestClasses.Kernel
{
    public class KernelKnife : IKernelWeapon
    {
        [Inject]
        public int Count { get; set; }

        [Inject]
        public KernelMaterial KernelMaterial { get; set; }

        public string State { get; private set; }

        public KernelKnife(KernelMaterial kernelMaterial)
        {
            KernelMaterial = kernelMaterial;
        }
    }
}
