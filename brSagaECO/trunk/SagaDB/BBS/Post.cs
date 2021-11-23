using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.BBS
{
    public class Post
    {
        DateTime date;
        string name;
        string title;
        string content;

        public DateTime Date { get { return this.date; } set { this.date = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public string Title { get { return this.title; } set { this.title = value; } }
        public string Content { get { return this.content; } set { this.content = value; } }
    }

    public class Mail
    {
        DateTime date;
        string name;
        string title;
        string content;

        public DateTime Date { get { return this.date; } set { this.date = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public string Title { get { return this.title; } set { this.title = value; } }
        public string Content { get { return this.content; } set { this.content = value; } }
    }
}
