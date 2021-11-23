
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Knight
{
    /// <summary>
    /// 遠距離防護（ディフレクトアロー）
    /// </summary>
    public class DifrectArrow : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            //需裝備盾牌
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 1000 * (35 - 5 * level);
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "DifrectArrow", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //遠距迴避
            int avoid_ranged_add = (int)(10 + 10 * level);
            if (skill.Variable.ContainsKey("DifrectArrow_avoid_ranged"))
                skill.Variable.Remove("DifrectArrow_avoid_ranged");
            skill.Variable.Add("DifrectArrow_avoid_ranged", avoid_ranged_add);
            actor.Status.avoid_ranged_skill += (short)avoid_ranged_add;

            actor.Buff.LongDodgeUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
     
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //遠距迴避
            actor.Status.avoid_ranged_skill -= (short)skill.Variable["DifrectArrow_avoid_ranged"];

            actor.Buff.LongDodgeUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
