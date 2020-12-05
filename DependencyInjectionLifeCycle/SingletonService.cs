using System;

namespace DependencyInjectionLifeCycle
{
    public class SingletonService
    {
        public int Total { get; }
        public SingletonService()
        {
            Total = Convert.ToInt32(Environment.GetEnvironmentVariable("SingletonTotal"));
        }
    }
}
