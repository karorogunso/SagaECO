using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Wizard
{
    public class MagicShield:ISkill
    {
        bool MobUse;
        public MagicShield()
        {
            this.MobUse = false;
        }
        public MagicShield(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("MagicShieldCD"))
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
            SkillCD cd = new SkillCD(args.skill, sActor, "MagicShieldCD", 20000);
            SkillHandler.ApplyAddition(sActor, cd);
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MagicShield", life);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short maxmatk = (short)(actor.Status.max_matk * (float)(0.12f - (skill.skill.Level * 0.02f)));
            short minmatk = (short)(actor.Status.min_matk * (float)(0.12f - (skill.skill.Level * 0.02f)));

            if (skill.Variable.ContainsKey("MagicShieldMaxMAtk"))
                skill.Variable.Remove("MagicShieldMaxMAtk");
            skill.Variable.Add("MagicShieldMaxMAtk", maxmatk);
            actor.Status.max_matk_skill += (short)maxmatk;

            if (skill.Variable.ContainsKey("MagicShieldMinMAtk"))
                skill.Variable.Remove("MagicShieldMinMAtk");
            skill.Variable.Add("MagicShieldMinMAtk", minmatk);
            actor.Status.min_matk_skill += (short)minmatk;

            if (actor.type == ActorType.PC)
                ((ActorPC)actor).TInt["MagicShieldlv"] = skill.skill.Level;
            if (actor.type == ActorType.PC)
                SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("进入魔法加护状态");
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.max_matk_skill -= (short)skill.Variable["MagicShieldMaxMAtk"];
            actor.Status.min_matk_skill -= (short)skill.Variable["MagicShieldMinMAtk"];

            if (actor.type == ActorType.PC)
                ((ActorPC)actor).TInt["MagicShieldlv"] = 0;
            if (actor.type == ActorType.PC)
                SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("魔法加护状态消失");
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