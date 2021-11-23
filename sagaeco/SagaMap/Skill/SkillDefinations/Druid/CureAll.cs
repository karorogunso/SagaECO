using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Druid
{
    /// <summary>
    /// 女神的加護（アレス）
    /// </summary>
    public class CureAll : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("EvilSoul"))
            {
                return -7;
            }
            if (dActor.type == ActorType.MOB)
            {
                ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)dActor.e;
                if (eh.AI.Mode.Symbol)
                    return -14;
            }
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factors = 0.1f * level;
            if (sActor.Status.Additions.ContainsKey("Cardinal"))//3转10技提升治疗量
                factors = factors * sActor.Status.Cardinal_Rank;
            int[] rate = {92,96,96,97,98,99,99,99,99,99};

            HealingForCure hfc = new HealingForCure(args.skill, sActor, dActor, 30000, 0);
            SkillHandler.ApplyAddition(sActor, hfc);
            //SkillHandler.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, -factors);
            SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID), dActor, 5076);
            bool cure = false;
            if (SagaLib.Global.Random.Next(0, 99) < rate[level-1])
            {
                cure = true;
            }
            if (cure)
            {
                RemoveAddition(dActor, "Poison");
                RemoveAddition(dActor, "鈍足");
                RemoveAddition(dActor, "Stone");
                RemoveAddition(dActor, "Silence");
                RemoveAddition(dActor, "Stun");
                RemoveAddition(dActor, "Sleep");
                RemoveAddition(dActor, "Frosen");
                RemoveAddition(dActor, "Confuse");
            }
        }
        public void RemoveAddition(Actor actor, String additionName)
        {
            if (actor.Status.Additions.ContainsKey(additionName))
            {
                Addition addition = actor.Status.Additions[additionName];
                actor.Status.Additions.Remove(additionName);
                if (addition.Activated)
                {
                    addition.AdditionEnd();
                }
                addition.Activated = false;
            }
        }
        class HealingForCure : DefaultBuff
        {
            public HealingForCure(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage)
                : base(skill, sActor, dActor, "HealingForCure", lifetime, 3000, damage)
            {

                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate2 += this.TimerUpdate;

            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {

            }

            void TimerUpdate(Actor sActor, Actor dActor, DefaultBuff skill, SkillArg arg, int damage)
            {
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (dActor.HP > 0 && !dActor.Buff.Dead)
                    {
                        float factors = 0.1f * skill.skill.Level;
                        damage = SkillHandler.Instance.CalcDamage(false, sActor, dActor, null, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, 100, -factors);
                        SkillHandler.Instance.CauseDamage(sActor, dActor, damage);
                        SkillHandler.Instance.ShowVessel(dActor, damage);
                    }
                }
                catch (Exception ex)
                {
                   SagaLib.Logger.ShowError(ex);
                }
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}
