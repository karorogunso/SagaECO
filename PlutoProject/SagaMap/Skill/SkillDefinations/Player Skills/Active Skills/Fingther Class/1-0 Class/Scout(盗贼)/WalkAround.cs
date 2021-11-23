
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Scout
{
    /// <summary>
    /// 卑劣的襲擊（回り込み）
    /// </summary>
    public class WalkAround : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //SkillHandler.Instance.Warp(sActor, 2, 2000, SagaLib.MoveType.JUMP);
            byte x, y;
            SkillHandler.Instance.GetTBackPos(Manager.MapManager.Instance.GetMap(sActor.MapID), dActor, out x, out y);

            short[] pos = new short[2];
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            pos[0] = SagaLib.Global.PosX8to16(x, map.Width);
            pos[1] = SagaLib.Global.PosY8to16(y, map.Height);
            map.MoveActor(Map.MOVE_TYPE.START, sActor, pos, (ushort)(dActor.Dir / 45), 20000, true, SagaLib.MoveType.QUICKEN);

            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Stiff, 50))
            {
                Additions.Global.Stiff skill = new SagaMap.Skill.Additions.Global.Stiff(args.skill, dActor, 3000);
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        #endregion
    }
}