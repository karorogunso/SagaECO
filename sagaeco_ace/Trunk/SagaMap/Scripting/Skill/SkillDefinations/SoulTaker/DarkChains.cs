using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.SoulTaker
{
    class DarkChains : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.2f + 0.3f * level;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, Elements.Dark, factor);
            int lifetime = 10000 + 5000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "DarkChains", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int[] down = {0,13,15,18,18,18};
            actor.Buff.STR減少 = true;
            actor.Buff.DEX減少 = true;
            actor.Buff.MAG減少 = true;
            actor.Buff.INT減少 = true;
            actor.Buff.AGI減少 = true;

            if (skill.Variable.ContainsKey("DarkChains"))
                skill.Variable.Remove("DarkChains");
            skill.Variable.Add("DarkChains",down[skill.skill.Level]);

            actor.Status.str_skill -= (short)down[skill.skill.Level];
            actor.Status.dex_skill -= (short)down[skill.skill.Level];
            actor.Status.mag_skill -= (short)down[skill.skill.Level];
            actor.Status.int_skill -= (short)down[skill.skill.Level];
            actor.Status.agi_skill -= (short)down[skill.skill.Level];
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.STR減少 = false;
            actor.Buff.DEX減少 = false;
            actor.Buff.MAG減少 = false;
            actor.Buff.INT減少 = false;
            actor.Buff.AGI減少 = false;
            actor.Status.str_skill -= (short)skill.Variable["DarkChains"];
            actor.Status.dex_skill -= (short)skill.Variable["DarkChains"];
            actor.Status.mag_skill -= (short)skill.Variable["DarkChains"];
            actor.Status.int_skill -= (short)skill.Variable["DarkChains"];
            actor.Status.agi_skill -= (short)skill.Variable["DarkChains"];
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
