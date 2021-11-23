using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S16005 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("十里冰封CD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 3000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorPC me = (ActorPC)sActor;
            List<Actor> affected = map.GetActorsArea(sActor, 400, true);
            List<Actor> realAffected = new List<Actor>();
            SkillHandler.Instance.ShowEffectOnActor(dActor, 5078);
            foreach (Actor act in affected)
            {
                if (act == null) continue;
                if (act == dActor) lifetime = 6000;
                if (!act.Status.Additions.ContainsKey("Invincible") && !act.Buff.Dead && !act.Status.Additions.ContainsKey("十里冰封CD"))
                {
                    OtherAddition skill = new OtherAddition(args.skill, act, "Invincible", lifetime);
                    skill.OnAdditionStart += (s, e) =>
                    {
                        act.Buff.三转宙斯盾イージス = true;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, act, true);
                    };
                    skill.OnAdditionEnd += (s, e) =>
                    {
                        act.Buff.三转宙斯盾イージス = false;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, act, true);
                    };
                    SkillHandler.ApplyAddition(act, skill);
                    OtherAddition skill2 = new OtherAddition(args.skill, act, "十里冰封CD", 60000);
                    SkillHandler.ApplyAddition(act, skill2);
                }
                if (!act.Status.Additions.ContainsKey("十里冰封") && !act.Buff.Dead)
                {
                    OtherAddition skill3 = new OtherAddition(args.skill, act, "十里冰封", lifetime);
                    skill3.OnAdditionStart += (s, e) =>
                    {
                        act.Buff.Frosen = true;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, act, true);
                    };
                    skill3.OnAdditionEnd += (s, e) =>
                    {
                        act.Buff.Frosen = false;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, act, true);
                    };
                    SkillHandler.ApplyAddition(act, skill3);
                }
            }
        }
    }
}
