using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Cardinal
{
    //挫败
    class Frustrate : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            int lifetime = 180000;
            if (sActor.type == ActorType.MOB)
            {
                lifetime = 20000;
            }
            short[] range = { 0, 100, 200, 100, 200, 100 };
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, range[level], true);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, i, "Frustrate", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(i, skill);
                }
            }
        }
        #endregion

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            float rankdef = 0.09f + 0.03f * skill.skill.Level;
            float subdef = 0.16f + 0.08f * skill.skill.Level;
            if (actor.type == ActorType.PC)
            {
                RemoveAddition(actor, "PetPlantDefupSelf");
                RemoveAddition(actor, "PetDefupSelf");
                RemoveAddition(actor, "MarioDamUp");
                RemoveAddition(actor, "Caution");
                RemoveAddition(actor, "Warn");
                RemoveAddition(actor, "RobotDefUp");
                RemoveAddition(actor, "重装铠化");
                RemoveAddition(actor, "Concentricity");
                RemoveAddition(actor, "PoisonReate2");
                RemoveAddition(actor, "PetDogDefUp");
                RemoveAddition(actor, "DefUpAvoDown");
                RemoveAddition(actor, "SolidBody");
                RemoveAddition(actor, "EvilSpirit");
                RemoveAddition(actor, "RustBody");
                RemoveAddition(actor, "EnergyShield");
                RemoveAddition(actor, "Sacrifice");
                RemoveAddition(actor, "DevineBarrier");
                RemoveAddition(actor, "EnergyBarrier");
                RemoveAddition(actor, "StrVitAgiDownOne");
                RemoveAddition(actor, "DefUpCircle");
                RemoveAddition(actor, "AtkUp_DefUp_SpdDown_AvoDown");
                RemoveAddition(actor, "SoulOfEarth");
                RemoveAddition(actor, "SpeedEnchant");
                RemoveAddition(actor, "Frustrate");
                RemoveAddition(actor, "BarrierShield");
                RemoveAddition(actor, "ForceShield");
                if (skill.Variable.ContainsKey("Frustrate_DEF"))
                    skill.Variable.Remove("Frustrate_DEF");
                skill.Variable.Add("Frustrate_DEF", (int)(actor.Status.def * rankdef));
                actor.Status.def_skill -= (short)(actor.Status.def * rankdef);

                if (skill.Variable.ContainsKey("Frustrate_MDEF"))
                    skill.Variable.Remove("Frustrate_MDEF");
                skill.Variable.Add("Frustrate_MDEF", (int)(actor.Status.mdef * rankdef));
                actor.Status.mdef_skill -= (short)(actor.Status.mdef * rankdef);


                actor.Buff.DefRateDown = true;
                actor.Buff.MagicDefRateDown = true;
                actor.Buff.DefRateUp = false;
                actor.Buff.MagicDefRateUp = false;
            }
            else
            {
                if (skill.Variable.ContainsKey("Frustrate_DEF"))
                    skill.Variable.Remove("Frustrate_DEF");
                skill.Variable.Add("Frustrate_DEF", (int)(actor.Status.def * rankdef));
                actor.Status.def_skill -= (short)(actor.Status.def * rankdef);
                if (skill.Variable.ContainsKey("Frustrate_DEF_ADD"))
                    skill.Variable.Remove("Frustrate_DEF_ADD");
                skill.Variable.Add("Frustrate_DEF_ADD", (int)(actor.Status.def_add * subdef));
                actor.Status.def_add_skill -= (short)(actor.Status.def_add * subdef);

                if (skill.Variable.ContainsKey("Frustrate_MDEF"))
                    skill.Variable.Remove("Frustrate_MDEF");
                skill.Variable.Add("Frustrate_MDEF", (int)(actor.Status.mdef * rankdef));
                actor.Status.mdef_skill -= (short)(actor.Status.mdef * rankdef);
                if (skill.Variable.ContainsKey("Frustrate_MDEF_ADD"))
                    skill.Variable.Remove("Frustrate_MDEF_ADD");
                skill.Variable.Add("Frustrate_MDEF_ADD", (int)(actor.Status.mdef_add * subdef));
                actor.Status.mdef_add_skill -= (short)(actor.Status.mdef_add * subdef);
                actor.Buff.DefRateDown = true;
                actor.Buff.DefDown = true;
                actor.Buff.MagicDefRateDown = true;
                actor.Buff.MagicDefDown = true;
            }

            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                actor.Status.def_skill += (short)skill.Variable["Frustrate_DEF"];
                actor.Status.mdef_skill += (short)skill.Variable["Frustrate_MDEF"];
                actor.Buff.DefRateDown = false;
                actor.Buff.MagicDefRateDown = false;
            }
            else
            {
                actor.Status.def_skill += (short)skill.Variable["Frustrate_DEF"];
                actor.Status.def_add_skill += (short)skill.Variable["Frustrate_DEF_ADD"];
                actor.Status.mdef_skill += (short)skill.Variable["Frustrate_MDEF"];
                actor.Status.mdef_add_skill += (short)skill.Variable["Frustrate_MDEF_ADD"];
                actor.Buff.DefRateDown = false;
                actor.Buff.DefDown = false;
                actor.Buff.MagicDefRateDown = false;
                actor.Buff.MagicDefDown = false;
            }

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
    }
}
