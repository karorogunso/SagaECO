using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    /// <summary>
    /// 寒氣勢力（ウォーターオーラ）
    /// </summary>
    public class SoulOfWater:ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (dActor.Status.Additions.ContainsKey("DevineBarrier"))
            {
                return -12;
            }
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 10000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "SoulOfWater", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short LDef = 0, RDef = 0;
            int level = skill.skill.Level;
            switch (level)
            {
                case 1:
                    LDef = 10;
                    break;
                case 2:
                    LDef = 20;
                    break;
                case 3:
                    LDef = 30;
                    RDef = 40;
                    break;
            }
            SkillHandler.RemoveAddition(actor, "MagicShield");

            //左魔防
            if (skill.Variable.ContainsKey("SoulOfWater_mdef"))
                skill.Variable.Remove("SoulOfWater_mdef");
            skill.Variable.Add("SoulOfWater_mdef", LDef);
            actor.Status.mdef_skill += (short)LDef;

            //右魔防
            if (skill.Variable.ContainsKey("SoulOfWater_mdef_add"))
                skill.Variable.Remove("SoulOfWater_mdef_add");
            skill.Variable.Add("SoulOfWater_mdef_add", RDef);
            actor.Status.mdef_add_skill += (short)RDef;

            //actor.Buff.魔法防御力上昇 = true;
            actor.Buff.MDefUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //左魔防
            actor.Status.mdef_skill -= (short)skill.Variable["SoulOfWater_mdef"];

            //右魔防
            actor.Status.mdef_add_skill -= (short)skill.Variable["SoulOfWater_mdef_add"];
 

            //actor.Buff.魔法防御力上昇 = false;
            actor.Buff.MDefUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
