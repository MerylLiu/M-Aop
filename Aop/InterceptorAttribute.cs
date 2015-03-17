using System;

namespace Aop
{
    public abstract class InterceptorAttribute : Attribute
    {
        public abstract void OnExecuting();

        public abstract void OnExecuted();
    }
}