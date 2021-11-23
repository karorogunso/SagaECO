
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 拔刀術（ストライクブロウ）
    /// </summary>
    public class StrikeBlow : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Status.Additions.ContainsKey("StrikeBlow"))
                return -30;
            else
                return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] lifetime = { 0, 1000, 1250, 1500, 1750, 2000 };
            DefaultBuff skill2 = new DefaultBuff(args.skill, sActor, "StrikeBlow", lifetime[level]);
            SkillHandler.ApplyAddition(sActor, skill2);
            args.type = ATTACK_TYPE.SLASH;
            float factor = 1.0f + 0.3f * level;
            short[] pos = new short[2];
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            pos[0] = dActor.X;
            pos[1] = dActor.Y;
            map.MoveActor(Map.MOVE_TYPE.START, sActor, pos, sActor.Dir, 20000, true, SagaLib.MoveType.BATTLE_MOTION);

            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}