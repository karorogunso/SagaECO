using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gladiator
{
    /// <summary>
    /// HPコミュニオン
    /// </summary>
    public class HPCommunion : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPC pc = (ActorPC)sActor;
            if (pc.Party != null)
            {
                foreach (var ppc in pc.Party.Members)
                {
                    if (ppc.Value  != pc)
                    {
                        ApplySkill(ppc.Value , args);
                    }
                }
            }
            ApplySkill(pc, args);               
        }
        public void ApplySkill(Actor dActor, SkillArg args)
        {
            int lifetime = 600000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "HPCommunion", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int[] MaxHPs = new int[] { 0,624 , 768 , 912 , 1056 , 1180 };
            //MaxHP
            int MaxHP_add = MaxHPs[skill.skill.Level];
            if (skill.Variable.ContainsKey("HPCommunion_MaxHP"))
                skill.Variable.Remove("HPCommunion_MaxHP");
            skill.Variable.Add("HPCommunion_MaxHP", MaxHP_add);
            actor.Status.hp_skill += (short)MaxHP_add;
            actor.Buff.三转HP增强 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //MaxHP
            actor.Status.hp_skill -= (short)skill.Variable["HPCommunion_MaxHP"];
            actor.Buff.三转HP增强 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
   
        }
        #endregion
    }
}
