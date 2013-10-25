using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB
{
    public class Account
    {
        private int account_id = -1;
        private string name;
        private string password;
        private string deletepass;
        private byte gmlevel;

        private List<Actor.ActorPC> chars = new List<SagaDB.Actor.ActorPC>();

        public string Name { get { return this.name; } set { this.name = value; } }
        public string Password { get { return this.password; } set { this.password = value; } }
        public string DeletePassword { get { return this.deletepass; } set { this.deletepass = value; } }
        public int AccountID { get { return this.account_id; } set { this.account_id = value; } }
        public List<Actor.ActorPC> Characters { get { return this.chars; } set { this.chars = value; } }
        public byte GMLevel { get { return this.gmlevel; } set { this.gmlevel = value; } }
    }
}
