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
        private uint bank;
        private bool banned;
        private string lastIP = "";
        private string lastIP2 = "";

        private List<Actor.ActorPC> chars = new List<SagaDB.Actor.ActorPC>();

        /// <summary>
        /// 帐号名
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }
        
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get { return this.password; } set { this.password = value; } }
        
        /// <summary>
        /// 人物删除密码
        /// </summary>
        public string DeletePassword { get { return this.deletepass; } set { this.deletepass = value; } }
        
        /// <summary>
        /// 帐号ID
        /// </summary>
        public int AccountID { get { return this.account_id; } set { this.account_id = value; } }
        
        /// <summary>
        /// 帐号所有人物
        /// </summary>
        public List<Actor.ActorPC> Characters { get { return this.chars; } set { this.chars = value; } }
        
        /// <summary>
        /// GM权限
        /// </summary>
        public byte GMLevel { get { return this.gmlevel; } set { this.gmlevel = value; } }

        /// <summary>
        /// 银行余额
        /// </summary>
        public uint Bank { get { return this.bank; } set { this.bank = value; } }

        /// <summary>
        /// 帐号是否被封
        /// </summary>
        public bool Banned { get { return this.banned; } set { this.banned = value; } }

        /// <summary>
        /// 上次登录IP
        /// </summary>
        public string LastIP { get { return this.lastIP; } set { this.lastIP = value; } }

        /// <summary>
        /// 补偿IP
        /// </summary>
        public string LastIP2 { get { return this.lastIP2; } set { this.lastIP2 = value; } }
    }
}
