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
            if (sActor.Party != null)
                return 0;
            else
                return -12;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPC pc = sActor as ActorPC;
            foreach (var item in pc.Party.Members.Values)
            {
                if (item.Online && item.MapID == pc.MapID && !item.Buff.Dead)
                {
                    int lifetime = 600000;
                    DefaultBuff skill = new DefaultBuff(args.skill, item, "HPCommunion", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(item, skill);
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int[] MaxHPs = new int[] { 0, 800, 1100, 1400, 1700, 2000 };
            //MaxHP
            int MaxHP_add = MaxHPs[skill.skill.Level];
            if (skill.Variable.ContainsKey("HPCommunion_MaxHP"))
                skill.Variable.Remove("HPCommunion_MaxHP");
            skill.Variable.Add("HPCommunion_MaxHP", MaxHP_add);
            actor.Status.hp_skill += (short)MaxHP_add;
            actor.Buff.MaxHPUp3RD = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //MaxHP
            actor.Status.hp_skill -= (short)skill.Variable["HPCommunion_MaxHP"];
            actor.Buff.MaxHPUp3RD = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        #endregion
    }
}
