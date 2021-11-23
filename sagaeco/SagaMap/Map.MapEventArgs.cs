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
        public bool send = false;
    }

    public class MoveArg : MapEventArgs
    {
        public ushort x, y, dir;
        public MoveType type;
    }

    public class EffectArg : MapEventArgs
    {
        public Actor caster;//特效制作者
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
            SkillArg arg = new SkillArg()
            {
                sActor = sActor,
                dActor = dActor,
                skill = skill,
                x = x,
                y = y,
                argType = argType,
                affectedActors = affectedActors
            };
            return arg;
        }

        public SkillArg SplitPart(int index, int count) //获取技能的副本，从index开始连续count个目标对象。
        {
            SkillArg arg = new SkillArg()
            {
                sActor = sActor,
                dActor = dActor,
                skill = skill,
                x = x,
                y = y,
                argType = argType,
            };
            if (index == 0) sActor = 0;
            for (int i = index; i < affectedActors.Count && i < index + count; i++)
            {
                arg.affectedActors.Add(affectedActors[i]);
                arg.flag.Add(flag[i]);
                arg.hp.Add(hp[i]);
                arg.mp.Add(mp[i]);
                arg.sp.Add(sp[i]);
            }
            return arg;
        }


        public SkillArg CloneWithoutSkill()
        {
            SkillArg arg = new SkillArg();
            SagaDB.Skill.Skill skill = new SagaDB.Skill.Skill();
            skill.BaseData = new SagaDB.Skill.SkillData();
            skill.BaseData.id = 0;
            arg.sActor = sActor;
            arg.dActor = dActor;
            arg.skill = skill;
            arg.x = x;
            arg.y = y;
            arg.argType = argType;
            return arg;
        }
        public void Init()
        {
            hp = new List<int>();
            mp = new List<int>();
            sp = new List<int>();
            flag = new List<AttackFlag>();
            for (int i = 0; i < affectedActors.Count; i++)
            {
                flag.Add((AttackFlag)0);
                hp.Add(0);
                mp.Add(0);
                sp.Add(0);
            }
        }
        public void Add(SkillArg arg)
        {
            for (int i = 0; i < arg.affectedActors.Count; i++)
            {
                affectedActors.Add(arg.affectedActors[i]);
                flag.Add(arg.flag[i]);
                hp.Add(arg.hp[i]);
                mp.Add(arg.mp[i]);
                sp.Add(arg.sp[i]);
            }
        }
        public void AddSameActor(SkillArg arg)
        {
            int count = affectedActors.Count;
            for (int i = 0; i < arg.affectedActors.Count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (arg.affectedActors[i].ActorID == affectedActors[j].ActorID)
                    {
                        hp[j] += arg.hp[i];
                        mp[j] += arg.mp[i];
                        sp[j] += arg.sp[i];
                        break;
                    }
                    if (j == count - 1)
                    {
                        affectedActors.Add(arg.affectedActors[i]);
                        flag.Add(arg.flag[i]);
                        hp.Add(arg.hp[i]);
                        mp.Add(arg.mp[i]);
                        sp.Add(arg.sp[i]);
                    }
                }
            }
        }
        public void Remove(Actor actor)
        {
            if (affectedActors.Contains(actor))
            {
                hp.RemoveAt(affectedActors.IndexOf(actor));
                mp.RemoveAt(affectedActors.IndexOf(actor));
                sp.RemoveAt(affectedActors.IndexOf(actor));
                flag.RemoveAt(affectedActors.IndexOf(actor));
                affectedActors.Remove(actor);
            }
        }

        public void Extend(int count)
        {
            for (int i = 0; i < count; i++)
                hp.Add(0);
            for (int i = 0; i < count; i++)
                mp.Add(0);
            for (int i = 0; i < count; i++)
                sp.Add(0);
            for (int i = 0; i < count; i++)
                flag.Add((AttackFlag)0);
        }
    }
}
