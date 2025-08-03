using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 神聖光界（ディバインバリア）
    /// </summary>
    public class MobDevineBarrier : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            RemoveAddition(dActor, "SoulOfEarth");
            RemoveAddition(dActor, "SoulOfWater");
            RemoveAddition(dActor, "MagicBarrier");
            RemoveAddition(dActor, "EnergyBarrier");

            int lifetime = 110000 + 15000 * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 100, true);
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.MOB && !SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, act, "MobDevineBarrier", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(act, skill);
                    SkillHandler.Instance.ShowEffectByActor(act, 4019);
                }
            }

            //sActor. = 4019;
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short LDef = 0, RDef = 0, LMDef = 0, RMDef = 0;
            int level = skill.skill.Level;
            switch (level)
            {
                case 1:
                    LDef = 5;
                    RDef = 5;
                    LMDef = 5;
                    RMDef = 10;
                    break;
                case 2:
                    LDef = 7;
                    RDef = 10;
                    LMDef = 7;
                    RMDef = 15;
                    break;
                case 3:
                    LDef = 9;
                    RDef = 10;
                    LMDef = 12;
                    RMDef = 15;
                    break;
                case 4:
                    LDef = 11;
                    RDef = 15;
                    LMDef = 15;
                    RMDef = 20;
                    break;
                case 5:
                    LDef = 15;
                    RDef = 20;
                    LMDef = 25;
                    RMDef = 20;
                    break;
            }
            //左防
            if (skill.Variable.ContainsKey("DevineBarrier_LDef"))
                skill.Variable.Remove("DevineBarrier_LDef");
            skill.Variable.Add("DevineBarrier_LDef", LDef);
            //右防
            if (skill.Variable.ContainsKey("DevineBarrier_RDef"))
                skill.Variable.Remove("DevineBarrier_RDef");
            skill.Variable.Add("DevineBarrier_RDef", RDef);
            //左魔防
            if (skill.Variable.ContainsKey("DevineBarrier_LMDef"))
                skill.Variable.Remove("DevineBarrier_LMDef");
            skill.Variable.Add("DevineBarrier_LMDef", LMDef);
            //右魔防
            if (skill.Variable.ContainsKey("DevineBarrier_RMDef"))
                skill.Variable.Remove("DevineBarrier_RMDef");
            skill.Variable.Add("DevineBarrier_RMDef", RMDef);

            //左防
            actor.Status.def_skill += LDef;
            //右防
            actor.Status.def_add_skill += RDef;
            //左魔防
            actor.Status.mdef_skill += LMDef;
            //右魔防
            actor.Status.mdef_add_skill += RMDef;

            actor.Buff.DefUp = true;
            actor.Buff.DefRateUp = true;
            actor.Buff.MagicDefUp = true;
            actor.Buff.MagicDefRateUp = true;

            EffectArg arg = new EffectArg();
            arg.effectID = 4019;
            arg.actorID = actor.ActorID;


            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, arg, actor, true);





        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //左防
            actor.Status.def_skill -= (short)skill.Variable["DevineBarrier_LDef"];
            //右防
            actor.Status.def_add_skill -= (short)skill.Variable["DevineBarrier_RDef"];
            //左魔防
            actor.Status.mdef_skill -= (short)skill.Variable["DevineBarrier_LMDef"];
            //右魔防
            actor.Status.mdef_add_skill -= (short)skill.Variable["DevineBarrier_RMDef"];
            actor.Buff.DefUp = false;
            actor.Buff.DefRateUp = false;
            actor.Buff.MagicDefUp = false;
            actor.Buff.MagicDefRateUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        public void RemoveAddition(Actor actor, String additionName)
        {
            if (actor.Status.Additions.ContainsKey(additionName))
            {
                Addition addition = actor.Status.Additions[additionName];
                actor.Status.Additions.Remove(additionName);
                if (addition.Activated)
                {
                    addition.AdditionEnd();
                }
                addition.Activated = false;
            }
        }
        #endregion
    }
}