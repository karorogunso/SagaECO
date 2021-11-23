using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class 冰棍的冻结 : DefaultDeBuff
    {
        public 冰棍的冻结(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount = 15)
            : base(skill, actor, "冰棍的冻结", lifetime, 1000)
        {
            if (SkillHandler.Instance.isBossMob(actor))
            {
                if (!actor.Status.Additions.ContainsKey("BOSSPoison免疫"))
                {
                    DefaultBuff BOSSPoison免疫 = new DefaultBuff(skill, actor, "BOSSPoison免疫", 60000);
                    SkillHandler.ApplyAddition(actor, BOSSPoison免疫);
                }
                else
                    this.Enabled = false;
            }
            if (actor.Status.Additions.ContainsKey("冰棍的冻结"))
            {
                actor.Status.Additions["冰棍的冻结"].TotalLifeTime += (int)(lifetime * (1f + Math.Max((actor.Status.debuffee_bonus / 100), -0.9f)));
                this.Enabled = false;
            }
            else
            {
                if (this.Variable.ContainsKey("冰棍的冻结"))
                    this.Variable.Remove("冰棍的冻结");
                this.Variable.Add("冰棍的冻结",amount);
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate += this.TimerUpdate;
            }
        }

        void StartEvent(Actor actor, DefaultDeBuff skill)
        {
            SkillHandler.Instance.ShowEffectOnActor(actor, 5078);
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Frosen = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("变成一根冰棍了！！[冰棍层数3/3]");
        }

        void EndEvent(Actor actor, DefaultDeBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            try
            {
                actor.Plies = 0;
                if (actor.HP > 0 && !actor.Buff.Dead)
                {
                    if (skill.Variable["冰棍的冻结"] < 1)
                        skill.Variable["冰棍的冻结"] = 1;
                    SkillHandler.Instance.CauseDamage(actor, actor, (int)(actor.MaxHP * skill.Variable["冰棍的冻结"] / 100f));
                    SkillHandler.Instance.ShowVessel(actor, (int)(actor.MaxHP * skill.Variable["冰棍的冻结"] / 100f));
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            actor.Buff.Frosen = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }

        void TimerUpdate(Actor actor, DefaultDeBuff skill)
        {
            //测试去除技能同步锁ClientManager.EnterCriticalArea();
            try
            {
                if (actor.HP > 0 && !actor.Buff.Dead)
                {
                    Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                    if (skill.Variable["冰棍的冻结"] < 1)
                        skill.Variable["冰棍的冻结"] = 1;
                    SkillHandler.Instance.CauseDamage(actor, actor, (int)(actor.MaxHP * skill.Variable["冰棍的冻结"] / 100f));
                    SkillHandler.Instance.ShowVessel(actor, (int)(actor.MaxHP * skill.Variable["冰棍的冻结"] / 100f));
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            //测试去除技能同步锁ClientManager.LeaveCriticalArea();
        }
    }
}
