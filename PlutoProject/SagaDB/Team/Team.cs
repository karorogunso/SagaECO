using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;

namespace SagaDB.Team
{
    public class Team
    {
        uint id;
        string name, pass, comment;
        int startingfloor, nowfloor1, nowfloor2;
        ActorPC leader;
        Dictionary<byte, ActorPC> members = new Dictionary<byte, ActorPC>();
        List<PC_JOB> jobrequirements = new List<PC_JOB>();
        public uint MaxMember = 30;
        byte minlv, maxlv;

        VariableHolder<string, string> tStrVar = new VariableHolder<string, string>("");
        VariableHolder<string, int> tIntVar = new VariableHolder<string, int>(0);
        VariableHolderA<string, BitMask> tMask = new VariableHolderA<string, BitMask>();
        VariableHolderA<string, DateTime> tTimeVar = new VariableHolderA<string, DateTime>();

        /// 临时字符串变量集
        /// </summary>
        public VariableHolder<string, string> TStr { get { return this.tStrVar; } }
        /// <summary>
        /// 临时整数变量集
        /// </summary>
        public VariableHolder<string, int> TInt { get { return this.tIntVar; } }

        /// <summary>
        /// 临时标识变量集
        /// </summary>
        public VariableHolderA<string, BitMask> TMask { get { return this.tMask; } }

        public VariableHolderA<string, DateTime> TTime { get { return this.tTimeVar; } }
        /// <summary>
        /// 队伍的ID
        /// </summary>
        public uint ID { get { return this.id; } set { this.id = value; } }

        /// <summary>
        /// 队伍名字
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }

        /// <summary>
        /// 队伍密碼
        /// </summary>
        public string Pass { get { return this.pass; } set { this.pass = value; } }

        /// <summary>
        /// 队伍留言
        /// </summary>
        public string Comment { get { return this.comment; } set { this.comment = value; } }

        /// <summary>
        /// 队伍最低等級要求
        /// </summary>
        public byte MinLevel { get { return this.minlv; } set { this.minlv = value; } }
        /// <summary>
        /// 队伍最高等級要求
        /// </summary>
        public byte MaxLevel { get { return this.maxlv; } set { this.maxlv = value; } }

        /// <summary>
        /// 队伍的起始層數
        /// </summary>
        public int StartingFloor { get { return this.startingfloor; } set { this.startingfloor = value; } }

        /// <summary>
        /// 队伍的現在所在層數(較淺層)
        /// </summary>
        public int NowFloor1 { get { return this.nowfloor1; } set { this.nowfloor1= value; } }

        /// <summary>
        /// 队伍的現在所在層數(較深層)
        /// </summary>
        public int NowFloor2 { get { return this.nowfloor2; } set { this.nowfloor2 = value; } }

        /// <summary>
        /// 队伍職業要求
        /// </summary>
        public List<PC_JOB> JobRequirements { get { return this.jobrequirements; } set { this.jobrequirements = value; } }

        /// <summary>
        /// 取得指定队伍成员
        /// </summary>
        /// <param name="index">索引ID</param>
        /// <returns>成员玩家</returns>
        public ActorPC this[byte index]
        {
            get
            {
                if (members.ContainsKey(index))
                    return members[index];
                else
                    return null;
            }
        }

        /// <summary>
        /// 队长
        /// </summary>
        public ActorPC Leader { get { return this.leader; } set { this.leader = value; } }

        /// <summary>
        /// 队伍成员
        /// </summary>
        public Dictionary<byte, ActorPC> Members { get { return this.members; } }

        /// <summary>
        /// 检查某个玩家是否是队伍成员
        /// </summary>
        /// <param name="char_id">玩家的CharID</param>
        /// <returns>是否是队伍成员</returns>
        public bool IsMember(uint char_id)
        {
            var chr =
                from c in members.Values
                where c.CharID == char_id
                select c;
            return (chr.Count() != 0);
        }

        /// <summary>
        /// 检查某个玩家是否是队伍成员
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <returns>是否是队伍成员</returns>
        public bool IsMember(ActorPC pc)
        {
            return IsMember(pc.CharID);
        }
        /// <summary>
        /// 取得成员人数
        /// </summary>
        public int MemberCount { get { return members.Count; } }

        /// <summary>
        /// 取得某个玩家成员ID
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <returns>成员ID，如果不是队伍成员则返回-1</returns>
        public byte IndexOf(ActorPC pc)
        {
            foreach (byte i in members.Keys)
            {
                if (members[i].CharID == pc.CharID)
                    return i;
            }
            return 255;
        }

        /// <summary>
        /// 取得某个玩家成员ID
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <returns>成员ID，如果不是队伍成员则返回-1</returns>
        public byte IndexOf(uint pc)
        {
            foreach (byte i in members.Keys)
            {
                if (members[i].CharID == pc)
                    return i;
            }
            return 255;
        }

        /// <summary>
        /// 成员上线，替换离线Actor
        /// </summary>
        /// <param name="newPC">新Actor</param>
        public void MemberOnline(ActorPC newPC)
        {
            if (!IsMember(newPC))
                return;
            byte index = (byte)IndexOf(newPC);
            members[index] = newPC;
            if (leader.CharID == newPC.CharID)
                leader = newPC;
        }

        /// <summary>
        /// 添加新的成员
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <returns>队伍中的索引</returns>
        public byte NewMember(ActorPC pc)
        {
            if (IsMember(pc))
                return (byte)IndexOf(pc);
            for (byte i = 0; i < 8; i++)
            {
                if (!members.ContainsKey(i))
                {
                    members.Add(i, pc);
                    return i;
                }
            }
            return 255;
        }

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="pc">玩家</param>
        public void DeleteMemeber(ActorPC pc)
        {
            members.Remove(IndexOf(pc));
        }

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="pc">玩家</param>
        public void DeleteMemeber(uint pc)
        {
            members.Remove(IndexOf(pc));
        }

        /// <summary>
        /// 根据CharID查找成员
        /// </summary>
        /// <param name="pc">玩家</param>
        public ActorPC SearchMemeber(uint pc)
        {
            foreach (byte i in members.Keys)
            {
                if (members[i].CharID == pc)
                    return members[i];
            }
            return null;
            
        }

    }
}