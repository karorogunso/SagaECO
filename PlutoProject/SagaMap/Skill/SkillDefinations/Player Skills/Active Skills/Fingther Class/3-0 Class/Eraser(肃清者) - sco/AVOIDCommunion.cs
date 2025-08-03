using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Eraser
{
    public class AVOIDCommunion : ISkill
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
            List<Actor> realAffected = new List<Actor>();
            ActorPC sPC = (ActorPC)sActor;
            foreach (ActorPC act in sPC.Party.Members.Values)
            {
                if (act.Online)
                {
                    if (act.Party.ID != 0 && !act.Buff.Dead && act.MapID == sActor.MapID)
                    {
                        DefaultBuff skill = new DefaultBuff(args.skill, act, "AVOIDCommunion", lifetime);
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
            int avoid_add = 50 + 20 * level;
            int[] exercises = new int[] { 0, 42, 57, 74, 90, 105, 200 };
            //pvp时 闪避共有效果修正
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = actor as ActorPC;
                if (pc.Mode == PlayerMode.COLISEUM_MODE)
                    avoid_add = exercises[level];
            }


            if (skill.Variable.ContainsKey("AVOIDCommunionAdd"))
                skill.Variable.Remove("AVOIDCommunionAdd");
            skill.Variable.Add("AVOIDCommunionAdd", avoid_add);
            actor.Status.avoid_melee_skill += (short)avoid_add;
            actor.Status.avoid_ranged_skill += (short)avoid_add;
            actor.Buff.AvoidUp3RD = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.avoid_melee_skill -= (short)skill.Variable["AVOIDCommunionAdd"];
            actor.Status.avoid_ranged_skill -= (short)skill.Variable["AVOIDCommunionAdd"];
            actor.Buff.AvoidUp3RD = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
