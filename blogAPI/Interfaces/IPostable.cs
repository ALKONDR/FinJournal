using System;
using System.Collections.Generic;

using blogAPI.Models;

namespace blogAPI.Interfaces
{
    public interface IPostable
    {
        DateTime Date { get; set; }
        User Author { get; set; }
        string Content { get; set; }
        List<Opinion> Likes { get; set; }
        List<Opinion> Dislikes { get; set; }
    }
}