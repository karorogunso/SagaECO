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
    class RobotAtkUp : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            ActorPet pet = SkillHandler.Instance.GetPet(pc);
            if (pet == null)
            {
                return -54;//需回傳"需裝備寵物"
            }
            if (SkillHandler.Instance.CheckMobType(pet, "MACHINE_RIDE_ROBOT"))
            {
                //return 0;
                return -54;//封印
            }
            return -54;//需回傳"需裝備寵物"
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 180000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "RobotAtkUp", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            float rank = 0.25f + 0.25f * skill.skill.Level;
            if (skill.Variable.ContainsKey("RobotAtkUp1"))
                skill.Variable.Remove("RobotAtkUp1");
            skill.Variable.Add("RobotAtkUp1", (int)(actor.Status.max_atk1 * rank));
            actor.Status.max_atk1_skill += (short)(actor.Status.max_atk1 * rank);

            if (skill.Variable.ContainsKey("RobotAtkUp2"))
                skill.Variable.Remove("RobotAtkUp2");
            skill.Variable.Add("RobotAtkUp2", (int)(actor.Status.max_atk2 * rank));
            actor.Status.max_atk2_skill += (short)(actor.Status.max_atk2 * rank);

            if (skill.Variable.ContainsKey("RobotAtkUp3"))
                skill.Variable.Remove("RobotAtkUp3");
            skill.Variable.Add("RobotAtkUp3", (int)(actor.Status.max_atk3 * rank));
            actor.Status.max_atk3_skill += (short)(actor.Status.max_atk3 * rank);


            if (skill.Variable.ContainsKey("RobotAtkUp4"))
                skill.Variable.Remove("RobotAtkUp4");
            skill.Variable.Add("RobotAtkUp4", (int)(actor.Status.min_atk1 * rank));
            actor.Status.min_atk1_skill += (short)(actor.Status.min_atk1 * rank);

            if (skill.Variable.ContainsKey("RobotAtkUp5"))
                skill.Variable.Remove("RobotAtkUp5");
            skill.Variable.Add("RobotAtkUp5", (int)(actor.Status.min_atk2 * rank));
            actor.Status.min_atk2_skill += (short)(actor.Status.min_atk2 * rank);

            if (skill.Variable.ContainsKey("RobotAtkUp6"))
                skill.Variable.Remove("RobotAtkUp6");
            skill.Variable.Add("RobotAtkUp6", (int)(actor.Status.min_atk3 * rank));
            actor.Status.min_atk3_skill += (short)(actor.Status.min_atk3 * rank);
            actor.Buff.三转2足ATKUP = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1_skill -= (short)(skill.Variable["RobotAtkUp1"]);
            actor.Status.max_atk2_skill -= (short)(skill.Variable["RobotAtkUp2"]);
            actor.Status.max_atk3_skill -= (short)(skill.Variable["RobotAtkUp3"]);

            actor.Status.min_atk1_skill -= (short)(skill.Variable["RobotAtkUp4"]);
            actor.Status.min_atk2_skill -= (short)(skill.Variable["RobotAtkUp5"]);
            actor.Status.min_atk3_skill -= (short)(skill.Variable["RobotAtkUp6"]);
            actor.Buff.三转2足ATKUP = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

            int[] lifetime = { 0, 60000, 80000, 100000, 125000, 150000 };
            DefaultBuff skill2 = new DefaultBuff(skill.skill, actor, "RobotAtkDown", lifetime[skill.skill.Level]);
            skill.OnAdditionStart += this.StartEventHandler2;
            skill.OnAdditionEnd += this.EndEventHandler2;
            SkillHandler.ApplyAddition(actor, skill);
        }
        void StartEventHandler2(Actor actor, DefaultBuff skill)
        {
            actor.Buff.RobotUnknowStateDown3RD = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler2(Actor actor, DefaultBuff skill)
        {
            actor.Buff.RobotUnknowStateDown3RD = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
//if (i.Status.Additions.ContainsKey("イレイザー") 