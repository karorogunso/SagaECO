using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Scout
{
    public class Hiding:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("Hiding"))
                return -7;
            else
                return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            args.dActor = 0;//不显示效果
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "Hiding", int.MaxValue, 1000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            skill.OnUpdate += this.UpdateEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void UpdateEventHandler(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("count"))
                skill.Variable["count"] += 1;
            else
                skill.Variable.Add("count", 0);
            if (skill.Variable["count"] >= 5)
            {
                ActorPC dActorPC = (ActorPC)actor;
                if (actor.SP > 0 && dActorPC.Motion != SagaLib.MotionType.SIT)
                {
                    Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                    actor.SP -= 1;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
                }
                else
                {
                    actor.Status.Additions["Hiding"].AdditionEnd();
                    actor.Status.Additions.Remove("Hiding");
                }
            }
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.Transparent = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.Transparent = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}
