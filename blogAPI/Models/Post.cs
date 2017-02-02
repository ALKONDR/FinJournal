using blogAPI.Interfaces;

namespace blogAPI.Models
{
    public class Post : IPostable
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Post(int id, string text)
        {
            this.Id = id;
            this.Text = text;
        }
    }
}