using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S25006 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("大车轮CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 200, false);//获取周围5*5的目标

            foreach (Actor i in actors)//遍历目标，不需要检查合法性，mob、玩家一起处理。
            {
                if (i.type == ActorType.PC || i.type == ActorType.MOB)//检查目标
                {
                    if (!i.Status.Additions.ContainsKey("丰饶之土"))
                    {
                        SkillHandler.Instance.PushBack(sActor, i, 4);//击退4格
                    }
                    //Stun skill = new Stun(null, item, 3000);
                    //SkillHandler.ApplyAddition(item, skill);
                }
            }
            if (sActor.type == ActorType.PC)
            {
                int cdtime = 3000;
                if (sActor.Status.Additions.ContainsKey("涌动之水"))
                    cdtime /= 2;
                OtherAddition cd = new OtherAddition(null, sActor, "大车轮CD", cdtime);
                //cd.OnAdditionEnd += (s, e) =>
                //{
                    //Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("『大车轮』冷却完毕。");
                //};
                SkillHandler.ApplyAddition(sActor, cd);
            }



        }
    }
}
