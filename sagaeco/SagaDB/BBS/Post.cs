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
        uint id;
        DateTime date;
        string name;
        string title;
        string content;

        public uint MailID { get { return this.id; } set { this.id = value; } }
        public DateTime Date { get { return this.date; } set { this.date = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public string Title { get { return this.title; } set { this.title = value; } }
        public string Content { get { return this.content; } set { this.content = value; } }
    }

    public class Gift
    {
        uint id;
        uint accountid;
        DateTime date;
        string name;
        string title;
        Dictionary<uint, ushort> items = new Dictionary<uint, ushort>();

        public uint MailID { get { return this.id; } set { this.id = value; } }
        public uint AccountID { get { return this.accountid; } set { this.accountid = value; } }
        public DateTime Date { get { return this.date; } set { this.date = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public string Title { get { return this.title; } set { this.title = value; } }
        public Dictionary<uint, ushort> Items { get { return this.items; } set { this.items = value; } }
    }
}
