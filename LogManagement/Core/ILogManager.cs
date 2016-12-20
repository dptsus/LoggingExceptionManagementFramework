using System;
using LogManagement.Models;

namespace LogManagement.Core
{
    public interface ILogManager
    {
        void LogApplicationCalls(EventEntry eventLogEntry);
        void LogExceptions(ExceptionEntry exception);
        string CreateExceptionString(Exception e);
    }
}
