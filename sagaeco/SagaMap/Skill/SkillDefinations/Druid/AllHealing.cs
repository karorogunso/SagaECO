using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Network.Client;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Druid
{
    public class AllHealing : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            switch (level)
            {
                case 1:
                    factor = -2.8f;
                    break;
                case 2:
                    factor = -3.2f;
                    break;
                case 3:
                    factor = -3.6f;
                    break;
                case 4:
                    factor = -4.2f;
                    break;
                case 5:
                    factor = -5.0f;
                    break;
            }
            if (sActor.Status.Additions.ContainsKey("Cardinal"))//3转10技提升治疗量
                factor = factor * sActor.Status.Cardinal_Rank;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 800, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.PC)
                {
                    ActorPC m = (ActorPC)act;
                    if (m.Buff.Dead != true)
                    {
                        realAffected.Add(act);
                    }
                    else if(m.Online)
                    {
                        m.Buff.紫になる = true;
                        MapClient.FromActorPC(m).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, m, true);
                        m.TInt["Revive"] = level;
                        MapClient.FromActorPC(m).EventActivate(0xF1000000);
                        m.TStr["Revive"] = sActor.Name;
                        MapClient.FromActorPC(m).SendSystemMessage(string.Format("玩家 {0} 正在请求你复活",sActor.Name));
                    }
                    if (!sActor.Status.Additions.ContainsKey("AllHealing"))
                    {
                        DefaultBuff skill = new DefaultBuff(args.skill, act, "AllHealing", level * 24 * 1000);
                        skill.OnAdditionStart += this.StartEventHandler;
                        skill.OnAdditionEnd += this.EndEventHandler;
                        SkillHandler.ApplyAddition(act, skill);
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, factor);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.mp_recover_skill += 15;
            actor.Status.hp_recover_skill += 15;
            actor.Status.sp_recover_skill += 15;
            actor.Buff.HPRecUp = true;
            actor.Buff.SPRecUp = true;
            actor.Buff.MPRecUp = true;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.mp_recover_skill -= 15;
            actor.Status.hp_recover_skill -= 15;
            actor.Status.sp_recover_skill -= 15;
            actor.Buff.HPRecUp = false;
            actor.Buff.SPRecUp = false;
            actor.Buff.MPRecUp = false;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
