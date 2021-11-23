using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    /// 無拍子（無拍子）
    /// </summary>
    public class Transporter :  ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            short[] pos = new short[2];
            Map map=Manager.MapManager.Instance.GetMap(sActor.MapID);
            pos[0] = SagaLib.Global.PosX8to16(args.x, map.Width);
            pos[1] = SagaLib.Global.PosY8to16(args.y, map.Height);
            map.MoveActor(Map.MOVE_TYPE.START, sActor, pos, 20000, 20000, true);
         
            float factor = 0.7f + 0.3f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
        }

        #endregion
    }
}
