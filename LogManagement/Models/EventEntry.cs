using System;

namespace LogManagement.Models
{
   public class EventEntry
   {
       public string Id { get; set; }
       public string SessionId { get; set; }
       public string MachineName { get; set; } 
       public string IpAddress { get; set; }
       public string Status { get; set; }
       public decimal? ResponseTime { get; set; }
       public string Title { get; set; }
       public string CallType { get; set; }
       public object Request { get; set; }
       public object Response { get; set; }
       public bool IsDebug { get; set; }
       public DateTime DateTime { get; set; }
    }
}
