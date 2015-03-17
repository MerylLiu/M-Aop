using System;

namespace Test13.Aop
{
    public abstract class InterceptorAttribute : Attribute
    {
        public abstract void OnExecuting();

        public abstract void OnExecuted();
    }
}