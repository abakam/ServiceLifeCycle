using System;

namespace DependencyInjectionLifeCycle
{
    public class Util
    {
        public static void SetEnvironmentVariables()
        {
            Environment.SetEnvironmentVariable("SingletonTotal", "1");
            Environment.SetEnvironmentVariable("ScopedTotal", "1");
            Environment.SetEnvironmentVariable("TranscientTotal", "1");
        }

        public static void IncrementSingletonValue()
        {
            int newValue = Convert.ToInt32(Environment.GetEnvironmentVariable("SingletonTotal")) + 1;
            Environment.SetEnvironmentVariable("SingletonTotal", newValue.ToString());
        }

        public static void IncrementScopedValue()
        {
            int newValue = Convert.ToInt32(Environment.GetEnvironmentVariable("ScopedTotal")) + 1;
            Environment.SetEnvironmentVariable("ScopedTotal", newValue.ToString());
        }

        public static void IncrementTranscientValue()
        {
            int newValue = Convert.ToInt32(Environment.GetEnvironmentVariable("TranscientTotal")) + 1;
            Environment.SetEnvironmentVariable("TranscientTotal", newValue.ToString());
        }
    }
}
