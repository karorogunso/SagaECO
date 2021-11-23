    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Mob;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations
{
    class S25009: ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("泼冷水CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x,map.Width), SagaLib.Global.PosY8to16(args.y,map.Height), 200, true);
            foreach (Actor i in actors)//遍历目标，不需要检查合法性，mob、玩家一起处理。
            {
                if (i.type == ActorType.PC || i.type == ActorType.MOB)//检查目标
                {
                    if (!i.Status.Additions.ContainsKey("泼冷水"))
                    {
                        OtherAddition skill = new OtherAddition(null, i, "泼冷水", 8000);
                        skill.OnAdditionStart += (x, e) =>
                        {
                            if (i.type == ActorType.PC)
                                Network.Client.MapClient.FromActorPC((ActorPC)i).SendSystemMessage("你进入了『泼冷水』状态！");
                            i.TInt["SPEEDDOWN"] = 400;
                            i.Speed = 400;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SPEED_UPDATE, null, i, true);
                            i.Buff.SpeedDown = true;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, i, true);
                        };
                        skill.OnAdditionEnd += (x, e) =>
                        {
                            i.TInt["SPEEDDOWN"] = 0;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SPEED_UPDATE, null, i, true);
                            i.Buff.SpeedDown = false;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, i, true);
                            if (i.type == ActorType.PC)
                                Network.Client.MapClient.FromActorPC((ActorPC)i).SendSystemMessage("『泼冷水』状态结束了！");
                        };
                        SkillHandler.ApplyAddition(i, skill);
                    }
                    //Stun skill = new Stun(null, item, 3000);
                    //SkillHandler.ApplyAddition(item, skill);
                    if (sActor.type == ActorType.PC)
                    {
                        int cdtime = 60000;
                        if (sActor.Status.Additions.ContainsKey("涌动之水"))
                            cdtime /= 2;
                        OtherAddition cd = new OtherAddition(null, sActor, "泼冷水CD", cdtime);
                        cd.OnAdditionEnd += (s, e) =>
                        {
                            Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("『泼冷水』冷却完毕。");
                        };
                        SkillHandler.ApplyAddition(sActor, cd);
                    }

                }
            }
        }
        #endregion
    }
}
