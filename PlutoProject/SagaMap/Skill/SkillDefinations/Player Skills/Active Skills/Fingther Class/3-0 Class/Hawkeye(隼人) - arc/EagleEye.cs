using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Hawkeye
{
    /// <summary>
    /// ホークアイ
    /// </summary>
    public class EagleEye : ISkill
    {
        #region ISkill 成員

        public int TryCast(SagaDB.Actor.ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            if (sActor.Status.Additions.ContainsKey("ホークアイ"))
            {
                sActor.Status.Additions["ホークアイ"].AdditionEnd();
            }
            ActorPC pc = (ActorPC)sActor;
            int[] lifetimes = new int[] { 0, 60000, 90000, 120000, 150000, 180000 };
            int lifetime = lifetimes[args.skill.Level];
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "ホークアイ", lifetime, 1000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            skill.OnUpdate += this.UpdateEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //X
            if (skill.Variable.ContainsKey("Save_X"))
                skill.Variable.Remove("Save_X");
            skill.Variable.Add("Save_X", actor.X);

            //Y
            if (skill.Variable.ContainsKey("Save_Y"))
                skill.Variable.Remove("Save_Y");
            skill.Variable.Add("Save_Y", actor.Y);

            int factor = new int[] { 0, 150, 175, 200, 225, 250 }[skill.skill.Level];
            if (skill.Variable.ContainsKey("ホークアイ"))
                skill.Variable.Remove("ホークアイ");
            skill.Variable.Add("ホークアイ", factor);

            actor.Buff.MainSkillPowerUp3RD = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.MainSkillPowerUp3RD = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void UpdateEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.X != (short)skill.Variable["Save_X"] || actor.Y != (short)skill.Variable["Save_Y"])
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                actor.Status.Additions["ホークアイ"].AdditionEnd();
                actor.Status.Additions.Remove("ホークアイ");
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, false);
            }
        }
        #endregion
    }
}
