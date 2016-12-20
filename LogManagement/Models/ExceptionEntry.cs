using System;

namespace LogManagement.Models
{
    public class ExceptionEntry
    {
        public string Id { get; set; }
        public string SessionId { get; set; }
        public string MachineName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Severity { get; set; }
        public string Priority { get; set; }
        public string IpAddress { get; set; }
        public string StackStrace { get; set; }
        public string Exception { get; set; }
        public bool IsDebug { get; set; }
        public DateTime DateTime { get; set; }
    }
}
