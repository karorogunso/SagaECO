using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// 神經衰弱（クラッター）
    /// </summary>
    public class Clutter : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 10 + 10 * level;
            int lifetime = new int[] { 0, 15000, 20000, 25000, 27000, 30000 }[level];
            //if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, "MagIntDexDownOne", rate))
            if (SagaLib.Global.Random.Next(0, 99) < rate)
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MagIntDexDownOne", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            if (actor is ActorPC)
            {
                //INT
                int int_add = new int[] { 0, 6, 7, 9, 11, 12 }[level] * -1;
                if (skill.Variable.ContainsKey("MagIntDexDownOne_int"))
                    skill.Variable.Remove("MagIntDexDownOne_int");
                skill.Variable.Add("MagIntDexDownOne_int", int_add);
                actor.Status.int_skill -= (short)int_add;

                //MAG
                int mag_add = new int[] { 0, 6, 7, 9, 11, 12 }[level] * -1;
                if (skill.Variable.ContainsKey("MagIntDexDownOne_mag"))
                    skill.Variable.Remove("MagIntDexDownOne_mag");
                skill.Variable.Add("MagIntDexDownOne_mag", mag_add);
                actor.Status.mag_skill -= (short)mag_add;

                //DEX
                int dex_add = -(6 + level * 2);
                if (skill.Variable.ContainsKey("MagIntDexDownOne_dex"))
                    skill.Variable.Remove("MagIntDexDownOne_dex");
                skill.Variable.Add("MagIntDexDownOne_dex", dex_add);
                actor.Status.dex_skill -= (short)dex_add;
                actor.Buff.INTDown = true;
                actor.Buff.DEXDown = true;
                actor.Buff.MAGDown = true;
            }
            else if (actor is ActorMob)
            {
                int max_matk_add = (int)(actor.Status.max_matk * (0.10f + 0.04f * level));
                int min_matk_add = (int)(actor.Status.min_matk * (0.10f + 0.04f * level));
                int magic_reduce = (int)((float)(0.10f + 0.04f * level) * 100.0f);
                int mdef_add = 10 + 4 * level;

                if (skill.Variable.ContainsKey("MagIntDexDownOne_MinMatk"))
                    skill.Variable.Remove("MagIntDexDownOne_MinMatk");
                skill.Variable.Add("MagIntDexDownOne_MinMatk", min_matk_add);
                actor.Status.min_matk_skill -= (short)min_matk_add;

                if (skill.Variable.ContainsKey("MagIntDexDownOne_MaxMatk"))
                    skill.Variable.Remove("MagIntDexDownOne_MaxMatk");
                skill.Variable.Add("MagIntDexDownOne_MaxMatk", max_matk_add);
                actor.Status.max_matk_skill -= (short)max_matk_add;

                if (skill.Variable.ContainsKey("MagIntDexDownOne_MagicReduce"))
                    skill.Variable.Remove("MagIntDexDownOne_MagicReduce");
                skill.Variable.Add("MagIntDexDownOne_MagicReduce", magic_reduce);
                actor.Status.MagicRuduceRate -= magic_reduce;

                if (skill.Variable.ContainsKey("MagIntDexDownOne_MDef"))
                    skill.Variable.Remove("MagIntDexDownOne_MDef");
                skill.Variable.Add("MagIntDexDownOne_MDef", mdef_add);
                actor.Status.mdef_skill -= (short)mdef_add;
                actor.Buff.MinMagicAtkDown = true;
                actor.Buff.MaxMagicAtkDown = true;
                actor.Buff.MagicAvoidDown = true;
                actor.Buff.MagicDefDown = true;
            }

            if (actor is ActorPC)
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            else
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, false);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor is ActorPC)
            {
                //INT
                actor.Status.int_skill += (short)skill.Variable["MagIntDexDownOne_int"];

                //MAG
                actor.Status.mag_skill += (short)skill.Variable["MagIntDexDownOne_mag"];

                //DEX
                actor.Status.dex_skill += (short)skill.Variable["MagIntDexDownOne_dex"];
                actor.Buff.INTDown = false;
                actor.Buff.DEXDown = false;
                actor.Buff.MAGDown = false;
            }
            else if (actor is ActorMob)
            {
                actor.Status.min_matk_skill += (short)skill.Variable["MagIntDexDownOne_MinMatk"];
                actor.Status.max_matk_skill += (short)skill.Variable["MagIntDexDownOne_MaxMatk"];
                actor.Status.MagicRuduceRate += skill.Variable["MagIntDexDownOne_MagicReduce"];
                actor.Status.mdef_skill += (short)skill.Variable["MagIntDexDownOne_MDef"];
                actor.Buff.MinMagicAtkDown = false;
                actor.Buff.MaxMagicAtkDown = false;
                actor.Buff.MagicAvoidDown = false;
                actor.Buff.MagicDefDown = false;
            }

            if (actor is ActorPC)
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            else
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, false);
        }
        #endregion
    }
}
