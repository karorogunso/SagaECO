using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Eraser
{
    /// <summary>
    /// 悪鬼
    /// </summary>
    public class EvilSpirit : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.2f + 0.4f * level;
            int lifetime = 15000 + 15000 * level;
            byte rate = (byte)(20 * level); 
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.BLOW;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < 4; i++)
            {
                dest.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SagaLib.Elements.Neutral, factor);
            if (SagaLib.Global.Random.Next(0, 100) < rate)
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "EvilSpirit", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int[] value = { 0, 15, 17, 20 };
            if (skill.Variable.ContainsKey("EvilSpirit_agi_down"))
                skill.Variable.Remove("EvilSpirit_agi_down");
            skill.Variable.Add("EvilSpirit_agi_down", value[skill.skill.Level]);
            actor.Status.agi_skill -= (short)value[skill.skill.Level];

            actor.Buff.AGI減少 = true;
            if (skill.skill.Level >= 2)
            {
                if (skill.Variable.ContainsKey("EvilSpirit_dex_down"))
                    skill.Variable.Remove("EvilSpirit_dex_down");
                skill.Variable.Add("EvilSpirit_dex_down", value[skill.skill.Level]);
                actor.Status.dex_skill -= (short)value[skill.skill.Level];

                if (skill.Variable.ContainsKey("EvilSpirit_vit_down"))
                    skill.Variable.Remove("EvilSpirit_vit_down");
                skill.Variable.Add("EvilSpirit_vit_down", value[skill.skill.Level]);
                actor.Status.vit_skill -= (short)value[skill.skill.Level];
                actor.Buff.DEX減少 = true;
                actor.Buff.VIT減少 = true;
            }
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.agi_skill += (short)skill.Variable["EvilSpirit_agi_down"];
            if (skill.skill.Level >= 2)
            {
                actor.Status.dex_skill += (short)skill.Variable["EvilSpirit_dex_down"];
                actor.Status.vit_skill += (short)skill.Variable["EvilSpirit_vit_down"];
                actor.Buff.DEX減少 = false;
                actor.Buff.VIT減少 = false;
            }
            actor.Buff.AGI減少 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
