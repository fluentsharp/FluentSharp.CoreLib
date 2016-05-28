using System;
using System.Reflection;


namespace FluentSharp.CoreLib.Tests
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitTestMethodReferenceAttribute : Attribute
    {
        public MethodInfo MethodToInvoke { get; set; }

        public UnitTestMethodReferenceAttribute(string methodName)
        {
            MethodToInvoke =  this.type().assembly().method(methodName);
            if (MethodToInvoke.isNull())
                "[UnitTestMethodReferenceAttribute] Could not find method {0} in assembly {1}".error(methodName, this.type().assembly()); 
        }

    }
}
