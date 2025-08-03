using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Guardian
{
    /// <summary>
    /// ストロングボディ
    /// </summary>
    public class StrongBody : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        int[] left_add = { 0, 5, 7, 9, 11, 13 };
        int[] left_add_another = { 0, 1, 3, 5, 7, 9 };
        float[] right_add = { 0, 0.02f, 0.04f, 0.06f, 0.08f, 0.1f };
        float[] hp_add = { 0, 0.1f, 0.15f, 0.2f, 0.25f, 0.3f };
        int[] lifetime = { 0, 30000, 40000, 50000, 60000, 70000 };
        Actor me;
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            me = sActor;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 300, true);
            List<Actor> realAffected = new List<Actor>();
            ActorPC sPC = (ActorPC)sActor;
            if (sPC.Party != null)
            {
                foreach (Actor act in affected)
                {
                    if (act.type == ActorType.PC)
                    {
                        ActorPC aPC = (ActorPC)act;
                        if (aPC.Party != null && sPC.Party != null)
                        {
                            if ((aPC.Party.ID == sPC.Party.ID) && aPC.Party.ID != 0 && !aPC.Buff.Dead && aPC.PossessionTarget == 0)
                            {
                                if (act.Buff.NoRegen) continue;

                                if (aPC.Party.ID == sPC.Party.ID)
                                {
                                    realAffected.Add(act);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                realAffected.Add(sActor);
            }
            foreach (Actor act in realAffected)
            {
                if (act.type == ActorType.PC)
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, act, "RustBody", lifetime[level]);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(act, skill);
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (me == actor)
            {
                if (skill.Variable.ContainsKey("Rust_LEFT_DEF"))
                    skill.Variable.Remove("Rust_LEFT_DEF");
                skill.Variable.Add("Rust_LEFT_DEF", left_add[skill.skill.Level]);
                actor.Status.def_skill += (short)left_add[skill.skill.Level];
            }
            else
            {
                if (skill.Variable.ContainsKey("Rust_LEFT_DEF"))
                    skill.Variable.Remove("Rust_LEFT_DEF");
                skill.Variable.Add("Rust_LEFT_DEF", left_add_another[skill.skill.Level]);
                actor.Status.def_skill += (short)left_add_another[skill.skill.Level];
            }


            int rust_def_add = (int)(actor.Status.def_add * right_add[skill.skill.Level]);
            if (skill.Variable.ContainsKey("Rust_RIGHT_DEF"))
                skill.Variable.Remove("Rust_RIGHT_DEF");
            skill.Variable.Add("Rust_RIGHT_DEF", rust_def_add);
            actor.Status.def_add_skill += (short)rust_def_add;

            int rust_hp_add = (int)(actor.MaxHP * hp_add[skill.skill.Level]);
            if (skill.Variable.ContainsKey("Rust_MAXHP"))
                skill.Variable.Remove("Rust_MAXHP");
            skill.Variable.Add("Rust_MAXHP", rust_hp_add);
            actor.Status.hp_skill += (short)rust_hp_add;

            actor.Buff.DefUp = true;
            actor.Buff.DefRateUp = true;
            actor.Buff.MaxHPUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.def_skill -= (short)skill.Variable["Rust_LEFT_DEF"];
            actor.Status.def_add_skill -= (short)skill.Variable["Rust_RIGHT_DEF"];
            actor.Status.hp_skill -= (short)skill.Variable["Rust_MAXHP"];

            actor.Buff.DefUp = false;
            actor.Buff.DefRateUp = false;
            actor.Buff.MaxHPUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
