using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Wizard
{
    public class EnergyShield:ISkill
    {
         bool MobUse;
        public EnergyShield()
        {
            this.MobUse = false;
        }
        public EnergyShield(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("EnergyShieldCD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int life = 0;
            if (MobUse)
            {
                level = 5;
            }
            life = 20000;
            SkillCD cd = new SkillCD(args.skill, sActor, "EnergyShieldCD", 20000);
            SkillHandler.ApplyAddition(sActor, cd);

            DefaultBuff skill = new DefaultBuff(args.skill, dActor , "EnergyShield", life);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short maxatk1 = (short)(actor.Status.max_atk1 * (float)(0.12f - (skill.skill.Level * 0.02f)));
            short maxatk2 = (short)(actor.Status.max_atk2 * (float)(0.12f - (skill.skill.Level * 0.02f)));
            short maxatk3 = (short)(actor.Status.max_atk3 * (float)(0.12f - (skill.skill.Level * 0.02f)));
            short minatk1 = (short)(actor.Status.min_atk1 * (float)(0.12f - (skill.skill.Level * 0.02f)));
            short minatk2 = (short)(actor.Status.min_atk2 * (float)(0.12f - (skill.skill.Level * 0.02f)));
            short minatk3 = (short)(actor.Status.min_atk3 * (float)(0.12f - (skill.skill.Level * 0.02f)));

            if (skill.Variable.ContainsKey("EnergyShieldMaxAtk1"))
                skill.Variable.Remove("EnergyShieldMaxAtk1");
            skill.Variable.Add("EnergyShieldMaxAtk1", maxatk1);
            actor.Status.max_atk1_skill += (short)maxatk1;

            if (skill.Variable.ContainsKey("EnergyShieldMaxAtk2"))
                skill.Variable.Remove("EnergyShieldMaxAtk2");
            skill.Variable.Add("EnergyShieldMaxAtk2", maxatk2);
            actor.Status.max_atk2_skill += (short)maxatk2;

            if (skill.Variable.ContainsKey("EnergyShieldMaxAtk3"))
                skill.Variable.Remove("EnergyShieldMaxAtk3");
            skill.Variable.Add("EnergyShieldMaxAtk3", maxatk3);
            actor.Status.max_atk3_skill += (short)maxatk3;

            if (skill.Variable.ContainsKey("EnergyShieldMinAtk1"))
                skill.Variable.Remove("EnergyShieldMinAtk1");
            skill.Variable.Add("EnergyShieldMinAtk1", minatk1);
            actor.Status.min_atk1_skill += (short)minatk1;

            if (skill.Variable.ContainsKey("EnergyShieldMinAtk2"))
                skill.Variable.Remove("EnergyShieldMinAtk2");
            skill.Variable.Add("EnergyShieldMinAtk2", minatk2);
            actor.Status.min_atk2_skill += (short)minatk2;

            if (skill.Variable.ContainsKey("EnergyShieldMinAtk3"))
                skill.Variable.Remove("EnergyShieldMinAtk3");
            skill.Variable.Add("EnergyShieldMinAtk3", minatk3);
            actor.Status.min_atk3_skill += (short)minatk3;

            if (actor.type == ActorType.PC)
                ((ActorPC)actor).TInt["EnergyShieldlv"] = skill.skill.Level;
            if (actor.type == ActorType.PC)
                SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("进入能量加护状态");
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["EnergyShieldMaxAtk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["EnergyShieldMaxAtk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["EnergyShieldMaxAtk3"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["EnergyShieldMaxAtk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["EnergyShieldMinAtk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["EnergyShieldMinAtk3"];

            if (actor.type == ActorType.PC)
                SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("能量加护状态消失");
            if (actor.type == ActorType.PC)
                ((ActorPC)actor).TInt["EnergyShieldlv"] = 0;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
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
        #endregion
    }
}