using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    /// <summary>
    /// 精靈之怒 (エレメンタルラース)
    /// </summary>
    public class ElementalWrath : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = new float[] { 0, 1.4f, 1.85f, 2.325f, 2.775f, 3.25f }[level];
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //ActorSkill actor = new ActorSkill(args.skill, sActor);
            //设定技能体位置
            //actor.MapID = sActor.MapID;
            //actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            //actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
            //actor.e = new ActorEventHandlers.NullEventHandler();
            //List<Actor> affected = map.GetActorsArea(actor, 300, false);
            List<Actor> affected = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 300, new Actor[] { });
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                }
            }

            Dictionary<Actor, int> FinalDamage = new Dictionary<Actor, int>();
            Dictionary<Actor, int> firedmgtable = SkillHandler.Instance.CalcMagicAttackWithoutDamage(sActor, realAffected, args, SagaLib.Elements.Fire, factor);
            Dictionary<Actor, int> waterdmgtable = SkillHandler.Instance.CalcMagicAttackWithoutDamage(sActor, realAffected, args, SagaLib.Elements.Water, factor);
            Dictionary<Actor, int> winddmgtable = SkillHandler.Instance.CalcMagicAttackWithoutDamage(sActor, realAffected, args, SagaLib.Elements.Wind, factor);
            Dictionary<Actor, int> earthdmgtable = SkillHandler.Instance.CalcMagicAttackWithoutDamage(sActor, realAffected, args, SagaLib.Elements.Earth, factor);

            foreach (var item in firedmgtable)
            {
                if (!FinalDamage.ContainsKey(item.Key))
                    FinalDamage.Add(item.Key, item.Value);
                else
                    FinalDamage[item.Key] += item.Value;
            }

            foreach (var item in waterdmgtable)
            {
                if (!FinalDamage.ContainsKey(item.Key))
                    FinalDamage.Add(item.Key, item.Value);
                else
                    FinalDamage[item.Key] += item.Value;
            }
            foreach (var item in winddmgtable)
            {
                if (!FinalDamage.ContainsKey(item.Key))
                    FinalDamage.Add(item.Key, item.Value);
                else
                    FinalDamage[item.Key] += item.Value;
            }
            foreach (var item in earthdmgtable)
            {
                if (!FinalDamage.ContainsKey(item.Key))
                    FinalDamage.Add(item.Key, item.Value);
                else
                    FinalDamage[item.Key] += item.Value;
            }

            List<int> dmglst = new List<int>();
            foreach (var item in FinalDamage)
            {
                dmglst.Add(item.Value);
            }
            SkillHandler.Instance.FixAttackList(sActor, realAffected, args, SkillHandler.DefType.MDef, SagaLib.Elements.Neutral, 50, dmglst);
        }
        #endregion
    }
}
