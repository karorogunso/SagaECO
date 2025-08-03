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

            if (sActor.type == ActorType.MOB)
            {
                //MobUse
                //Check if target is using 反射
                if (dActor.Status.Additions.ContainsKey("MagicReflect"))
                {
                    Actor temp = dActor;
                    dActor = sActor;
                    sActor = temp;
                    temp.Status.Additions.Remove("MagicReflect");
                }
            }

            SkillHandler.Instance.MagicAttack(sActor, dActor, args, Elements.Dark, factor);
            int lifetime = 10000 + 5000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "DarkChains", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int[] down = { 0, 13, 15, 18, 18, 18 };
            if (actor.type == ActorType.PC)
            {
                actor.Buff.STRDown = true;
                actor.Buff.DEXDown = true;
                actor.Buff.MAGDown = true;
                actor.Buff.INTDown = true;
                actor.Buff.AGIDown = true;

                if (skill.Variable.ContainsKey("DarkChains"))
                    skill.Variable.Remove("DarkChains");
                skill.Variable.Add("DarkChains", down[skill.skill.Level]);

                actor.Status.str_skill -= (short)down[skill.skill.Level];
                actor.Status.dex_skill -= (short)down[skill.skill.Level];
                actor.Status.mag_skill -= (short)down[skill.skill.Level];
                actor.Status.int_skill -= (short)down[skill.skill.Level];
                actor.Status.agi_skill -= (short)down[skill.skill.Level];
            }
            else
            {

                actor.Buff.MinAtkDown = true;
                actor.Buff.MaxAtkDown = true;
                actor.Buff.ShortHitDown = true;
                actor.Buff.MinAtkDown = true;
                actor.Buff.MaxAtkDown = true;
                actor.Buff.MagicDefDown = true;
                actor.Buff.ShortDodgeDown = true;

                if (skill.Variable.ContainsKey("DarkChains"))
                    skill.Variable.Remove("DarkChains");
                skill.Variable.Add("DarkChains", down[skill.skill.Level]);

                actor.Status.min_atk1_skill -= (short)down[skill.skill.Level];
                actor.Status.min_atk2_skill -= (short)down[skill.skill.Level];
                actor.Status.min_atk3_skill -= (short)down[skill.skill.Level];
                actor.Status.max_atk1_skill -= (short)down[skill.skill.Level];
                actor.Status.max_atk2_skill -= (short)down[skill.skill.Level];
                actor.Status.max_atk3_skill -= (short)down[skill.skill.Level];
                actor.Status.min_matk_skill -= (short)down[skill.skill.Level];
                actor.Status.max_matk_skill -= (short)down[skill.skill.Level];
                actor.Status.hit_melee_skill -= (short)down[skill.skill.Level];
                actor.Status.hit_ranged_skill -= (short)down[skill.skill.Level];
                actor.Status.avoid_melee_skill -= (short)down[skill.skill.Level];
                actor.Status.avoid_ranged_skill -= (short)down[skill.skill.Level];
                actor.Status.mdef_skill -= (short)down[skill.skill.Level];
            }

            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                actor.Buff.STRDown = false;
                actor.Buff.DEXDown = false;
                actor.Buff.MAGDown = false;
                actor.Buff.INTDown = false;
                actor.Buff.AGIDown = false;
                actor.Status.str_skill += (short)skill.Variable["DarkChains"];
                actor.Status.dex_skill += (short)skill.Variable["DarkChains"];
                actor.Status.mag_skill += (short)skill.Variable["DarkChains"];
                actor.Status.int_skill += (short)skill.Variable["DarkChains"];
                actor.Status.agi_skill += (short)skill.Variable["DarkChains"];
            }
            else
            {
                actor.Buff.MinAtkDown = false;
                actor.Buff.MaxAtkDown = false;
                actor.Buff.ShortHitDown = false;
                actor.Buff.MinAtkDown = false;
                actor.Buff.MaxAtkDown = false;
                actor.Buff.MagicDefDown = false;
                actor.Buff.ShortDodgeDown = false;
                actor.Status.min_atk1_skill += (short)skill.Variable["DarkChains"];
                actor.Status.min_atk2_skill += (short)skill.Variable["DarkChains"];
                actor.Status.min_atk3_skill += (short)skill.Variable["DarkChains"];
                actor.Status.max_atk1_skill += (short)skill.Variable["DarkChains"];
                actor.Status.max_atk2_skill += (short)skill.Variable["DarkChains"];
                actor.Status.max_atk3_skill += (short)skill.Variable["DarkChains"];
                actor.Status.min_matk_skill += (short)skill.Variable["DarkChains"];
                actor.Status.max_matk_skill += (short)skill.Variable["DarkChains"];
                actor.Status.hit_melee_skill += (short)skill.Variable["DarkChains"];
                actor.Status.hit_ranged_skill += (short)skill.Variable["DarkChains"];
                actor.Status.avoid_melee_skill += (short)skill.Variable["DarkChains"];
                actor.Status.avoid_ranged_skill += (short)skill.Variable["DarkChains"];
                actor.Status.mdef_skill += (short)skill.Variable["DarkChains"];


            }

            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
