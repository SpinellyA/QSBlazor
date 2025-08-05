using Microsoft.VisualBasic.FileIO;

namespace Data.Models;
public class LogEntry { 
    public User User { get; set; }
    public string Action { get; set; }
    public DateTime Date { get; set; }

}