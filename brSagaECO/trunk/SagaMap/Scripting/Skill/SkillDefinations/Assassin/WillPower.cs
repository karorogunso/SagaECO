using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    ///  氣合（気合）
    /// </summary>
    public class WillPower :ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 300, true, false);
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
            args.affectedActors = realAffected;
            args.Init();
            int lifetime = 60000;
            foreach (Actor rAct in realAffected)
            {
                DefaultBuff skill = new DefaultBuff(args.skill, rAct, "WarCry", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(rAct, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short max_atk1_add = (short)(actor.Status.max_atk1 * skill.skill.Level * 0.04f);
            short max_atk2_add = (short)(actor.Status.max_atk2 * skill.skill.Level * 0.04f);
            short max_atk3_add = (short)(actor.Status.max_atk3 * skill.skill.Level * 0.04f);
            short max_matk_add = (short)(actor.Status.max_atk1 * skill.skill.Level * 0.04f);

            if (skill.Variable.ContainsKey("WarCry_max_atk1_add"))
                skill.Variable.Remove("WarCry_max_atk1_add");
            skill.Variable.Add("WarCry_max_atk1_add", max_atk1_add);

            if (skill.Variable.ContainsKey("WarCry_max_atk2_add"))
                skill.Variable.Remove("WarCry_max_atk2_add");
            skill.Variable.Add("WarCry_max_atk2_add", max_atk2_add);

            if (skill.Variable.ContainsKey("WarCry_max_atk3_add"))
                skill.Variable.Remove("WarCry_max_atk3_add");
            skill.Variable.Add("WarCry_max_atk3_add", max_atk3_add);

            if (skill.Variable.ContainsKey("WarCry_max_matk_add"))
                skill.Variable.Remove("WarCry_max_matk_add");
            skill.Variable.Add("WarCry_max_matk_add", max_matk_add);

            actor.Status.max_atk1_skill += max_atk1_add;
            actor.Status.max_atk2_skill += max_atk2_add;
            actor.Status.max_atk3_skill += max_atk3_add;
            actor.Status.max_matk_skill += max_matk_add;
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["WarCry_max_atk1_add"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["WarCry_max_atk2_add"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["WarCry_max_atk3_add"];
            actor.Status.max_matk_skill -= (short)skill.Variable["WarCry_max_matk_add"];
        }
        /*public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPC actorPC = (ActorPC)sActor;
            int[] StatusCounts = {0, 1, 1, 2, 2, 3 };
            int[] CureRates = {0, 30, 50, 30, 50, 50 };
            int StatusCount=StatusCounts[level];
            int CureRate=CureRates[level];
            int i = 0;
            try
            {
                List<string> WillBeRemove = new List<string>();
                foreach (KeyValuePair<string, Addition> s in actorPC.Status.Additions)
                {
                    if (i < StatusCount)
                    {
                        if (SagaLib.Global.Random.Next(0, 99) < CureRate)
                        {
                            Addition addition = (Addition)s.Value;
                            WillBeRemove.Add(s.Key);
                            if (addition.Activated)
                            {
                                addition.AdditionEnd();
                            }
                            addition.Activated = false;
                            i++;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                foreach (var AdditionName in WillBeRemove)
                {
                    actorPC.Status.Additions.Remove(AdditionName);
                }
            }
            catch (Exception)
            {
            }
                
        }*/
        #endregion
    }
}
