using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// 竜眼開放·極 
    /// </summary>
    public class Ryuugankakusen : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 120000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "DragonEyeOpen", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("DragonEyeOpen"))
                skill.Variable.Remove("DragonEyeOpen");
            skill.Variable.Add("DragonEyeOpen", 150);

            actor.Buff.OpenDragonEye = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("DragonEyeOpen"))
                skill.Variable.Remove("DragonEyeOpen");

            actor.Buff.OpenDragonEye = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        #endregion
    }
}
