using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Archer
{
    /// <summary>
    /// 飄渺之境
    /// </summary>
    public class LHitUp:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }



        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 1000, true, false);
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
            foreach (Actor rAct in realAffected)
            {
                int life = 0;
                life = 180000;
                DefaultBuff skill = new DefaultBuff(args.skill, rAct, "鹰怒", life);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(rAct, skill);
            }
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {

            float rate = 0.02f * skill.skill.Level;
            short max_atk1 = (short)(actor.Status.max_atk1 * rate);
            short max_atk2 = (short)(actor.Status.max_atk2 * rate);
            short max_atk3 = (short)(actor.Status.max_atk3 * rate);
            short min_atk1 = (short)(actor.Status.min_atk1 * rate);
            short min_atk2 = (short)(actor.Status.min_atk2 * rate);
            short min_atk3 = (short)(actor.Status.min_atk3 * rate);
            short max_matk = (short)(actor.Status.max_matk * rate);
            short min_matk = (short)(actor.Status.min_matk * rate);
            short cri = (short)(skill.skill.Level * 2);

            if (skill.Variable.ContainsKey("鹰怒maxatk1"))
                skill.Variable.Remove("鹰怒maxatk1");
            skill.Variable.Add("鹰怒maxatk1", max_atk1);
            actor.Status.max_atk1_skill += max_atk1;

            if (skill.Variable.ContainsKey("鹰怒maxatk2"))
                skill.Variable.Remove("鹰怒maxatk2");
            skill.Variable.Add("鹰怒maxatk2", max_atk2);
            actor.Status.max_atk2_skill += max_atk2;

            if (skill.Variable.ContainsKey("鹰怒maxatk3"))
                skill.Variable.Remove("鹰怒maxatk3");
            skill.Variable.Add("鹰怒maxatk3", max_atk3);
            actor.Status.max_atk3_skill += max_atk3;

            if (skill.Variable.ContainsKey("鹰怒minatk1"))
                skill.Variable.Remove("鹰怒minatk1");
            skill.Variable.Add("鹰怒minatk1", min_atk1);
            actor.Status.min_atk1_skill += min_atk1;

            if (skill.Variable.ContainsKey("鹰怒minatk2"))
                skill.Variable.Remove("鹰怒minatk2");
            skill.Variable.Add("鹰怒minatk2", min_atk2);
            actor.Status.min_atk2_skill += min_atk2;

            if (skill.Variable.ContainsKey("鹰怒minatk3"))
                skill.Variable.Remove("鹰怒minatk3");
            skill.Variable.Add("鹰怒minatk3", min_atk3);
            actor.Status.min_atk3_skill += min_atk3;

            if (skill.Variable.ContainsKey("鹰怒maxmatk"))
                skill.Variable.Remove("鹰怒maxmatk");
            skill.Variable.Add("鹰怒maxmatk", max_matk);
            actor.Status.max_matk_skill += max_matk;

            if (skill.Variable.ContainsKey("鹰怒minmatk"))
                skill.Variable.Remove("鹰怒minmatk");
            skill.Variable.Add("鹰怒minmatk", min_matk);
            actor.Status.min_matk_skill += min_matk;

            if (skill.Variable.ContainsKey("鹰怒CriUp"))
                skill.Variable.Remove("鹰怒CriUp");
            skill.Variable.Add("鹰怒CriUp", cri);
            actor.Status.cri_skill += cri;

            Manager.MapManager.Instance.GetMap(actor.MapID).SendEffect(actor, 5170);
            if(actor.type == ActorType.PC)
            {
                SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("受到名射手的光环的效果");
            }
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["鹰怒maxatk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["鹰怒maxatk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["鹰怒maxatk3"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["鹰怒minatk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["鹰怒minatk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["鹰怒minatk3"];
            actor.Status.max_matk_skill -= (short)skill.Variable["鹰怒maxmatk"];
            actor.Status.min_matk_skill -= (short)skill.Variable["鹰怒minmatk"];
            actor.Status.cri_skill -= (short)skill.Variable["鹰怒CriUp"];
            if (actor.type == ActorType.PC)
            {
                SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("名射手的光环的效果解除");
            }
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
