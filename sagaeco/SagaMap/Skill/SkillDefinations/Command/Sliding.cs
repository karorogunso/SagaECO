
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 滑動追擊（スライディング）
    /// </summary>
    public class Sliding : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //無拍子到指定目標前方
            short[] pos = new short[2];
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //byte x, y;
            //SkillHandler.Instance.GetTFrontPos(map, dActor, out x, out y);

            pos[0] = SagaLib.Global.PosX8to16(args.x, map.Width);
            pos[1] = SagaLib.Global.PosY8to16(args.y, map.Height);
            map.MoveActor(Map.MOVE_TYPE.START, sActor, pos, 20000, 3000, true);
        }
        #endregion
    }
}