using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.SunFlowerAdditions
{
    /// <summary>
    /// 巫婆长袍切换（Ragnarok）
    /// </summary>
    public class EquipChange : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 10000;
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "EquipChange", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Elements[SagaLib.Elements.Holy] -= 100;
            actor.Elements[SagaLib.Elements.Dark] += 100;
            actor.Buff.BodyDarkElementUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            SkillHandler.Instance.ShowEffectByActor(actor, 5241);
            actor.Elements[SagaLib.Elements.Holy] += 100;
            actor.Elements[SagaLib.Elements.Dark] -= 100;
            actor.Buff.BodyDarkElementUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}
