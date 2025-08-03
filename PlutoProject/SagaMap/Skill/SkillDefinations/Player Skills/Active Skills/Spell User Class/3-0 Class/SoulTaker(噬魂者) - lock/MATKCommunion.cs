using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.SoulTaker
{
    public class MATKCommunion : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Party != null) return 0;
            else return -12;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 600000;
            List<Actor> realAffected = new List<Actor>();
            ActorPC sPC = (ActorPC)sActor;
            foreach (ActorPC act in sPC.Party.Members.Values)
            {
                if (act.Online)
                {
                    if (act.Party.ID != 0 && !act.Buff.Dead && act.MapID == sActor.MapID)
                    {
                        DefaultBuff skill = new DefaultBuff(args.skill, act, "MATKCommunion", lifetime);
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
            int max_matk_add = 120 + 36 * level;
            int[] exercises = new int[] { 0, 31, 58, 65, 72, 80 };
            //pvpʱ Ч������
            ActorPC pc = actor as ActorPC;
            if (pc.Mode == PlayerMode.COLISEUM_MODE)
                max_matk_add = exercises[level];

            if (skill.Variable.ContainsKey("MATKCommunion"))
                skill.Variable.Remove("MATKCommunion");
            skill.Variable.Add("MATKCommunion", max_matk_add);
            actor.Status.max_matk_skill += (short)max_matk_add;
            actor.Buff.MagicAtkUp3RD = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.max_matk_skill -= (short)skill.Variable["MATKCommunion"];
            actor.Buff.MagicAtkUp3RD = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
