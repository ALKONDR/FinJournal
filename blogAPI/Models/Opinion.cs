using System;

namespace blogAPI.Models
{
    public class Opinion
    {
        DateTime Date { get; set; }
        User Author { get; set; }
    }
}