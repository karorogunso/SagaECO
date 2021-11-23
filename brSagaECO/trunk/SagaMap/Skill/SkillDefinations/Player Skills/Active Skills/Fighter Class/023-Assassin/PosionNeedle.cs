using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    ///  毒暗器（ポイズンニードル）
    /// </summary>
    public class PosionNeedle : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            uint itemID=10038102;//毒針
            if (SkillHandler.Instance.CountItem(sActor, itemID) >= 1)
            {
                SkillHandler.Instance.TakeItem(sActor, itemID, 1);
                return 0;
            }
            return -57;
            
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 25 + 5 * level;
            if (SkillHandler.Instance.CanAdditionApply(sActor,dActor,"PosionNeedle", rate))
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "PosionNeedle", 3000);
                skill.OnAdditionStart += this.StartEvent;
                skill.OnAdditionEnd += this.EndEvent;
                SkillHandler.ApplyAddition(dActor, skill);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, 1.0f);
        }
        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Poison = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

            int value;
            if (skill.Variable.ContainsKey("PosionNeedle_ATK1"))
                skill.Variable.Remove("PosionNeedle_ATK1");
            value = actor.Status.min_atk_ori / 2;
            skill.Variable.Add("PosionNeedle_ATK1", value);
            actor.Status.min_atk1_skill -= (short)value;
            actor.Status.min_atk2_skill -= (short)value;
            actor.Status.min_atk3_skill -= (short)value;
            if (skill.Variable.ContainsKey("PosionNeedle_ATK2"))
                skill.Variable.Remove("PosionNeedle_ATK2");
            value = actor.Status.max_atk_ori / 2;
            skill.Variable.Add("PosionNeedle_ATK2", value);
            actor.Status.max_atk1_skill -= (short)value;
            actor.Status.max_atk2_skill -= (short)value;
            actor.Status.max_atk3_skill -= (short)value;
            if (skill.Variable.ContainsKey("PosionNeedle_MATK"))
                skill.Variable.Remove("PosionNeedle_MATK");
            value = actor.Status.min_matk_ori / 2;
            skill.Variable.Add("PosionNeedle_MATK", value);
            actor.Status.min_matk_skill -= (short)value;
            if (skill.Variable.ContainsKey("PosionNeedle_MATK2"))
                skill.Variable.Remove("PosionNeedle_MATK2");
            value = actor.Status.max_matk_ori / 2;
            skill.Variable.Add("PosionNeedle_MATK2", value);
            actor.Status.max_matk_skill -= (short)value;
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Poison = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

            int value = skill.Variable["PosionNeedle_ATK1"];
            actor.Status.min_atk1_skill += (short)value;
            actor.Status.min_atk2_skill += (short)value;
            actor.Status.min_atk3_skill += (short)value;
            value = skill.Variable["PosionNeedle_ATK2"];
            actor.Status.max_atk1_skill += (short)value;
            actor.Status.max_atk2_skill += (short)value;
            actor.Status.max_atk3_skill += (short)value;
            value = skill.Variable["PosionNeedle_MATK"];
            actor.Status.min_matk_skill += (short)value;
            value = skill.Variable["PosionNeedle_MATK2"];
            actor.Status.max_matk_skill += (short)value;
        }

        #endregion
    }
}
