using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S14012 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("彼岸火湖CD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 20000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorPC me = (ActorPC)sActor;

            if (sActor.EP < level * 1000u)    //移除EP
                sActor.EP = 0;
            else sActor.EP -= level * 1000u;


            /*-------------------魔法阵的技能体-----------------*/
            ActorSkill actor2 = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(14012, 1), sActor);
            actor2.Name = "火湖特效";
            actor2.MapID = sActor.MapID;
            actor2.X = sActor.X;
            actor2.Y = sActor.Y;
            actor2.e = new ActorEventHandlers.NullEventHandler();
            map.RegisterActor(actor2);
            actor2.invisble = false;
            map.OnActorVisibilityChange(actor2);
            actor2.Stackable = false;
            /*-------------------魔法阵的技能体-----------------*/

            OtherAddition skillX = new OtherAddition(null, sActor, "彼岸火湖特效持续", lifetime);
            skillX.OnAdditionEnd += (x, z) =>
            {
                map.DeleteActor(actor2);
            };
            SkillHandler.ApplyAddition(sActor, skillX);

            List<Actor> affected = map.GetActorsArea(sActor, 500, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act == null) continue;
                if (act.type == ActorType.PC && act.Buff.Dead != true && !act.Status.Additions.ContainsKey("彼岸火湖")) //&& !act.Status.Additions.ContainsKey("彼岸火湖CD")
                {
                    OtherAddition skill = new OtherAddition(null, act, "彼岸火湖", lifetime);
                    skill.OnAdditionStart += (s, e) =>
                    {
                        act.OnBuffCallBackList.Add("彼岸火湖", (x, y, z) =>
                        {
                            if (x.Buff.三转红锤子ウェポンエンハンス)
                            {
                                z += (int)(z * x.TInt["彼岸火湖增伤"] / 100f);
                                SkillHandler.Instance.ShowEffectOnActor(y, 5293, sActor);
                            }
                            return z;
                        });

                        act.TInt["彼岸火湖增伤"] = level * 10;
                        act.Buff.三转红锤子ウェポンエンハンス = true;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, act, true);
                    };
                    skill.OnAdditionEnd += (s, e) =>
                    {
                        act.OnBuffCallBackList.Remove("彼岸火湖");

                        act.TInt["彼岸火湖增伤"] = 0;
                        act.Buff.三转红锤子ウェポンエンハンス = false;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, act, true);
                    };
                    SkillHandler.ApplyAddition(act, skill);
                }
                SkillHandler.Instance.ShowEffectOnActor(act, 4253, sActor);
            }
            OtherAddition skill2 = new OtherAddition(args.skill, sActor, "彼岸火湖CD", 120000);
            SkillHandler.ApplyAddition(sActor, skill2);
            OtherAddition skill3 = new OtherAddition(args.skill, sActor, "彼岸焚烧", 20000); //实际上不需要单独分出来，但是考虑到可能后期有被动改变烈焰焚烧强化效果时间，就拉出来了
            SkillHandler.ApplyAddition(sActor, skill3);
        }
    }
}

