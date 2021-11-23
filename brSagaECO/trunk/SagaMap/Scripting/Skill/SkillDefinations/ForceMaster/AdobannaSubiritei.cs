
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.ForceMaster
{
    /// <summary>
    /// アドバンスアビリティー
    /// </summary>
    public class AdobannaSubiritei : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 100000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "AdobannaSubiritei", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            float[] MinAttack = new float[] { 0, 0.7f, 0.8f, 0.9f};

            //最小攻擊
            int min_matk_add = (int)((actor.Status.max_matk - actor.Status.min_matk) * MinAttack[skill.skill.Level]);
            if (skill.Variable.ContainsKey("AdobannaSubiritei"))
                skill.Variable.Remove("AdobannaSubiritei");
            skill.Variable.Add("AdobannaSubiritei", min_matk_add);
            actor.Status.min_matk_skill += (short)min_matk_add;

            actor.Buff.三转アドバンスアビリテイー = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最小攻擊
            actor.Status.min_matk_skill -= (short)skill.Variable["AdobannaSubiritei"];

            actor.Buff.三转アドバンスアビリテイー = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
