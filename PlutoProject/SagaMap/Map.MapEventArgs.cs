using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;


using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Map;
using SagaLib;
using SagaMap.Manager;

namespace SagaMap
{
    public partial class Map
    {
        internal object RegisterActor()
        {
            throw new NotImplementedException();
        }
    }

    public class ChatArg : MapEventArgs
    {
        public string content;
        public MotionType motion;
        public byte loop;
        public uint emotion;
        public byte expression;
    }

    public class MoveArg : MapEventArgs
    {
        public ushort x, y, dir;
        public MoveType type;
    }

    public class EffectArg : MapEventArgs
    {
        public uint actorID;
        public uint effectID;
        public byte x = 0xFF, y = 0xFF;
        public bool oneTime = true;
        public ushort height = 0xFFFF;
    }

    public class PossessionArg : MapEventArgs
    {
        public uint fromID;
        public uint toID;
        public int result;
        public string comment;
        public bool cancel = false;
        public byte x;
        public byte y;
        public byte dir;
    }

    public class AutoCastInfo
    {
        public uint skillID;
        public byte level;
        public int delay;
        public byte x = 0xff;
        public byte y = 0xff;
    }

    public class SkillArg : MapEventArgs
    {
        public enum ArgType
        {
            Attack,
            Cast,
            Active,
            Item_Cast,
            Item_Active,
            Actor_Active,
        }

        public ArgType argType = ArgType.Attack;
        public uint sActor, dActor;
        public List<Actor> affectedActors = new List<Actor>();
        public Item item;
        public SagaDB.Skill.Skill skill;
        public byte x, y;
        public ATTACK_TYPE type;
        public List<int> hp = new List<int>(), mp = new List<int>(), sp = new List<int>();
        public List<AttackFlag> flag = new List<AttackFlag>();
        public uint delay;
        public short result;
        public uint inventorySlot;
        public List<AutoCastInfo> autoCast = new List<AutoCastInfo>();
        public bool useMPSP = true;
        public bool showEffect = true;
        public float delayRate = 1f;

        public SkillArg Clone()
        {
            SkillArg arg = new SkillArg();
            arg.sActor = this.sActor;
            arg.dActor = this.dActor;
            arg.skill = this.skill;
            arg.x = this.x;
            arg.y = this.y;
            arg.argType = this.argType;
            arg.affectedActors = this.affectedActors;
            return arg;
        }
        public SkillArg CloneWithoutSkill()
        {
            SkillArg arg = new SkillArg();
            SagaDB.Skill.Skill skill = new SagaDB.Skill.Skill();
            skill.BaseData = new SagaDB.Skill.SkillData();
            skill.BaseData.id = 0;
            arg.sActor = this.sActor;
            arg.dActor = this.dActor;
            arg.skill = skill;
            arg.x = this.x;
            arg.y = this.y;
            arg.argType = this.argType;
            return arg;
        }
        public void Init()
        {
            this.hp = new List<int>();
            this.mp = new List<int>();
            this.sp = new List<int>();
            this.flag = new List<AttackFlag>();
            for (int i = 0; i < this.affectedActors.Count; i++)
            {
                this.flag.Add((AttackFlag)0);
                this.hp.Add(0);
                this.mp.Add(0);
                this.sp.Add(0);
            }
        }
        public void Add(SkillArg arg)
        {
            for (int i = 0; i < arg.affectedActors.Count; i++)
            {
                this.affectedActors.Add(arg.affectedActors[i]);
                this.flag.Add(arg.flag[i]);
                this.hp.Add(arg.hp[i]);
                this.mp.Add(arg.mp[i]);
                this.sp.Add(arg.sp[i]);
            }
        }
        public void AddSameActor(SkillArg arg)
        {
            int count = this.affectedActors.Count;
            for (int i = 0; i < arg.affectedActors.Count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (arg.affectedActors[i].ActorID == this.affectedActors[j].ActorID)
                    {
                        this.hp[j] += arg.hp[i];
                        this.mp[j] += arg.mp[i];
                        this.sp[j] += arg.sp[i];
                        break;
                    }
                    if (j == count - 1)
                    {
                        this.affectedActors.Add(arg.affectedActors[i]);
                        this.flag.Add(arg.flag[i]);
                        this.hp.Add(arg.hp[i]);
                        this.mp.Add(arg.mp[i]);
                        this.sp.Add(arg.sp[i]);
                    }
                }
            }
        }
        public void Remove(Actor actor)
        {
            if (this.affectedActors.Contains(actor))
            {
                this.hp.RemoveAt(this.affectedActors.IndexOf(actor));
                this.mp.RemoveAt(this.affectedActors.IndexOf(actor));
                this.sp.RemoveAt(this.affectedActors.IndexOf(actor));
                this.flag.RemoveAt(this.affectedActors.IndexOf(actor));
                this.affectedActors.Remove(actor);
            }
        }

        public void Extend(int count)
        {
            for (int i = 0; i < count; i++)
                this.hp.Add(0);
            for (int i = 0; i < count; i++)
                this.mp.Add(0);
            for (int i = 0; i < count; i++)
                this.sp.Add(0);
            for (int i = 0; i < count; i++)
                this.flag.Add((AttackFlag)0);
        }
    }
}
