using System;

namespace Isotrol
{
    public class IsotrolLogEventArgs : EventArgs
    {
        public bool IsError { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
