using Microsoft.VisualBasic.FileIO;

namespace Data.Models;
public class Logs { 
    public List<LogEntry> LogEntries { get; set; }

    public Logs() { 
    LogEntries = new List<LogEntry>();
    }

    public void Register(LogEntry entry)
    {
        LogEntries.Add(entry);
    }



}