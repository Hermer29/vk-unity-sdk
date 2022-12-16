using System;

namespace VkSdk.Runtime.Utility
{
    [Serializable]
    public class SerializedTime
    {
        public int seconds;
        public int minutes;
        public int hours;

        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(hours, minutes, seconds);
        }
    }
}