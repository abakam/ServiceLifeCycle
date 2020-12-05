using System;

namespace DependencyInjectionLifeCycle
{
    public class ScopedService
    {
        public int Total { get; }

        public ScopedService()
        {
            Total = Convert.ToInt32(Environment.GetEnvironmentVariable("ScopedTotal"));
        }
    }
}
