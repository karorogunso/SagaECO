using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    ///  硬化毒（硬化毒）
    /// </summary>
    public class PosionReate2 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            uint itemID = 10000353;//刺客的內服藥（硬化毒）
            if (SkillHandler.Instance.CountItem(pc, itemID) > 0)
            {
                SkillHandler.Instance.TakeItem(pc, itemID, 1);
                return 0;
            }
            return -2;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int PMlv = 0;
            if (!(sActor is ActorPC))
                return;

            ActorPC pc = sActor as ActorPC;
            int lifeTime = 150000;
            if (pc.Skills3.ContainsKey(994) || pc.DualJobSkill.Exists(x => x.ID == 994))
            {
                //这里取副职的加成技能专精等级
                var duallv = 0;
                if (pc.DualJobSkill.Exists(x => x.ID == 994))
                    duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 994).Level;

                //这里取主职的加成技能等级
                var mainlv = 0;
                if (pc.Skills3.ContainsKey(994))
                    mainlv = pc.Skills3[994].Level;

                //这里取等级最高的加成技能等级用来做时间加成
                PMlv = Math.Max(duallv, mainlv);
                //ParryResult += pc.Skills[116].Level * 3;
                lifeTime += lifeTime * (int)(1.5f + 0.5f * PMlv);
            }
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "PoisonReate2", lifeTime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            ActorPC pc = actor as ActorPC;
            //毒药大师等级判定
            int PMlv = 0;
            int rate = 50;
            if (pc.Skills3.ContainsKey(994) || pc.DualJobSkill.Exists(x => x.ID == 994))
            {
                //这里取副职的加成技能专精等级
                var duallv = 0;
                if (pc.DualJobSkill.Exists(x => x.ID == 994))
                    duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 994).Level;

                //这里取主职的加成技能等级
                //var mainlv = 0;
                if (pc.Skills3.ContainsKey(994))
                    PMlv = pc.Skills3[994].Level;

                //这里取等级最高的加成技能来减少中毒概率(5级免疫)
                rate = rate - 10 * Math.Max(duallv,PMlv);
                if (rate <= 0)
                {
                    rate = 0;
                }
            }
            int level = skill.skill.Level;
            float lDown = 0.01f + 0.03f * level + 0.01f * PMlv;
            short rDown = (short)(8 + 2 * level);
            //左防禦
            int def_add = (int)((float)actor.Status.def * lDown);
            if (skill.Variable.ContainsKey("PosionReate2_def"))
                skill.Variable.Remove("PosionReate2_def");
            skill.Variable.Add("PosionReate2_def", def_add);
            actor.Status.def_skill += (short)def_add;

            //右防禦
            if (skill.Variable.ContainsKey("PosionReate2_def_add"))
                skill.Variable.Remove("PosionReate2_def_add");
            skill.Variable.Add("PosionReate2_def_add", rDown);
            actor.Status.def_add_skill += rDown;

            actor.Buff.DefUp = true;
            actor.Buff.DefRateUp = true;
            //中毒?
            if (SkillHandler.Instance.CanAdditionApply(actor, actor, SkillHandler.DefaultAdditions.Poison, rate))
            {
                Additions.Global.Poison nskill = new SagaMap.Skill.Additions.Global.Poison(skill.skill, actor, 7000);
                SkillHandler.ApplyAddition(actor, nskill);
            }
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //左防禦
            actor.Status.def_skill -= (short)skill.Variable["PosionReate2_def"];

            //右防禦
            actor.Status.def_add_skill -= (short)skill.Variable["PosionReate2_def_add"];

            actor.Buff.DefUp = false;
            actor.Buff.DefRateUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}



