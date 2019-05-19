using System;
using System.Collections.Generic;
using System.Text;

namespace MyDIContainer.Tests.TestClasses.ConstructorSetter
{
    public class CtorSword : ICtorIWeapon
    {
        // ConstructorInfo.Name =".ctor"//ctor with primitive parameter
        public CtorSword(string s)
        {
        }

        // ConstructorInfo.Name =".ctor"
        public CtorSword(CtorMaterial material)
        {
        }
    }
}
