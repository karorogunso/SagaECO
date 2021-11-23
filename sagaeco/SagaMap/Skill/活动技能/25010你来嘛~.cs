using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S25010 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("你来嘛CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            
            MobAI ai = new MobAI(dActor, true);
            List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(dActor.X, map.Width), SagaLib.Global.PosY16to8(dActor.Y, map.Height),
            SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height));

            if (path.Count < 2)
                return;
            if (!dActor.Status.Additions.ContainsKey("丰饶之土"))
            {
                short[] pos = new short[2];
                pos[0] = SagaLib.Global.PosX8to16(path[path.Count / 2-1].x, map.Width);
                pos[1] = SagaLib.Global.PosY8to16(path[path.Count / 2-1].y, map.Height);
                map.MoveActor(Map.MOVE_TYPE.START, dActor, pos, sActor.Dir, 800, true, MoveType.BATTLE_MOTION);//拉拽，距离=两人距离的一半
            }
            if (sActor.type == ActorType.PC)
            {
                int cdtime = 25000;
                if (sActor.Status.Additions.ContainsKey("涌动之水"))
                    cdtime /= 2;
                OtherAddition cd = new OtherAddition(null, sActor, "你来嘛CD", cdtime);
                cd.OnAdditionEnd += (s, e) =>
                {
                    Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("『你来嘛』冷却完毕。");
                };
                SkillHandler.ApplyAddition(sActor, cd);
            }


        }
    }
}
