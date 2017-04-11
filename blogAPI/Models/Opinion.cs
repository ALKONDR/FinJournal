using System;

namespace blogAPI.Models
{
    public class Opinion
    {
        DateTime Date { get; set; }
        string Author { get; set; } = string.Empty;
    }
}