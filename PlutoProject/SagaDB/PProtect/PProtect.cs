using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;


namespace SagaDB.PProtect
{
    public class PProtect
    {
        uint id;
        string name;
        ActorPC leader;
        List<ActorPC> members = new List<ActorPC>();
        public byte MaxMember = 4;

        string message;
        string password;
        uint taskID;
        byte state;


        /// <summary>
        /// 队伍序列ID
        /// </summary>
        public uint ID { get { return this.id; } set { this.id = value; } }
        /// <summary>
        /// 副本ID
        /// </summary>
        public uint TaskID { get { return this.taskID; } set { this.taskID = value; } }

        /// <summary>
        /// 队伍名字
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }
        /// <summary>
        /// 召集人
        /// </summary>
        public ActorPC Leader { get { return this.leader; } set { this.leader = value; } }

        /// <summary>
        /// 队伍成员
        /// </summary>
        public List<ActorPC> Members { get { return this.members; } }
        /// <summary>
        /// 取得成员人数
        /// </summary>
        public int MemberCount { get { return members.Count; } }

        /// <summary>
        /// 招募信息
        /// </summary>
        public string Message { get { return this.message; } set { this.message = value; } }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get { return this.password; } set { this.password = value; } }

        /// <summary>
        /// 是否加密
        /// </summary>
        public bool IsPassword { get { return !string.IsNullOrEmpty(this.password); } }
        
        /// <summary>
        /// 状态 0为招募中 1为游戏中
        /// </summary>
        public byte IsRun { get { return this.state; } set { this.state = value; } }
    }
}
