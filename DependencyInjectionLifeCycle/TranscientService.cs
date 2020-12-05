using System;

namespace DependencyInjectionLifeCycle
{
    public class TranscientService
    {
        public int Total { get; }

        public TranscientService()
        {
            Total = Convert.ToInt32(Environment.GetEnvironmentVariable("TranscientTotal"));
        }
    }
}
