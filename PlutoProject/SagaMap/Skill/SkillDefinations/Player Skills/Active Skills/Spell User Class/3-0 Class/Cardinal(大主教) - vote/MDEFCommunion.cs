using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Cardinal
{
    public class MDEFCommunion : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Party != null)
                return 0;
            else
                return -12;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 600000;
            ActorPC sPC = (ActorPC)sActor;
            foreach (ActorPC act in sPC.Party.Members.Values)
            {
                if (act.Online)
                {
                    if (act.Party.ID == sPC.Party.ID && !act.Buff.Dead && act.MapID == sPC.MapID)
                    {
                        DefaultBuff skill = new DefaultBuff(args.skill, act, "MDEFCommunion", lifetime);
                        skill.OnAdditionStart += this.StartEventHandler;
                        skill.OnAdditionEnd += this.EndEventHandler;
                        SkillHandler.ApplyAddition(act, skill);
                    }
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            int mdef_add_add = 120 + 36 * level;
            if (skill.Variable.ContainsKey("MDEFCommunion"))
                skill.Variable.Remove("MDEFCommunion");
            skill.Variable.Add("MDEFCommunion", mdef_add_add);
            actor.Status.mdef_add_skill += (short)mdef_add_add;
            actor.Buff.MagicDefUp3RD = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.mdef_add_skill -= (short)skill.Variable["MDEFCommunion"];
            actor.Buff.MagicDefUp3RD = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
