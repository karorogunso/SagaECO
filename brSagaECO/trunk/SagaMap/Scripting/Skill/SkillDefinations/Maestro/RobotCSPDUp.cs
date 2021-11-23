using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.Maestro
{
    class RobotCSPDUp : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            ActorPet pet = SkillHandler.Instance.GetPet(pc);
            if (pet == null)
            {
                return -53;//需回傳"需裝備寵物"
            }
            if (SkillHandler.Instance.CheckMobType(pet, "MACHINE_RIDE_ROBOT"))
            {
                return 0;
            }
            return -53;//需回傳"需裝備寵物"
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 1800000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "RobotCSPDUp", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            float rank = 0.25f + 0.25f * skill.skill.Level;
            if (skill.Variable.ContainsKey("RobotCSPDUp"))
                skill.Variable.Remove("RobotCSPDUp");
            skill.Variable.Add("RobotCSPDUp", (int)(actor.Status.cspd * rank));
            actor.Status.cspd_skill += (short)(actor.Status.cspd * rank);


            actor.Buff.三转机器人攻速上升 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.cspd_skill -= (short)(skill.Variable["RobotCSPDUp"]);

            actor.Buff.三转机器人攻速上升 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

            int[] lifetime = { 0, 60000, 80000, 100000, 125000, 150000 };
            DefaultBuff skill2 = new DefaultBuff(skill.skill, actor, "RobotCSPDUp", lifetime[skill.skill.Level]);
            skill.OnAdditionStart += this.StartEventHandler2;
            skill.OnAdditionEnd += this.EndEventHandler2;
            SkillHandler.ApplyAddition(actor, skill);
        }
        void StartEventHandler2(Actor actor, DefaultBuff skill)
        {
            actor.Buff.三转机器人攻速下降 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler2(Actor actor, DefaultBuff skill)
        {
            actor.Buff.三转机器人攻速下降 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
//if (i.Status.Additions.ContainsKey("イレイザー") 