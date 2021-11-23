using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S16004 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("业火之泉CD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 20000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorPC me = (ActorPC)sActor;
            List<Actor> affected = map.GetActorsArea(sActor, 400, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act == null) continue;
                if (act.type == ActorType.PC && act.Buff.Dead != true && !act.Status.Additions.ContainsKey("业火之泉") && !act.Status.Additions.ContainsKey("业火之泉CD"))
                {
                    OtherAddition skill = new OtherAddition(null, act, "业火之泉", lifetime);
                    skill.OnAdditionStart += (s, e) =>
                    {
                        act.Buff.三转红锤子ウェポンエンハンス = true;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, act, true);
                    };
                    skill.OnAdditionEnd += (s, e) =>
                    {
                        act.Buff.三转红锤子ウェポンエンハンス = false;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, act, true);
                    };
                    SkillHandler.ApplyAddition(act, skill);

                    SkillHandler.Instance.ShowEffectOnActor(act, 4253);

                    DefaultBuff skill2 = new DefaultBuff(args.skill, act, "业火之泉CD", 120000);
                    SkillHandler.ApplyAddition(act, skill2);
                }
            }
        }
    }
}
