using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Eraser
{
    /// <summary>
    /// 刹那
    /// </summary>
    public class Instant : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 12.5f + 5.5f * level;
            int critbonus = new int[] { 0, 10, 15, 20, 30, 40 }[level];
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
            SkillHandler.Instance.SetNextComboSkill(sActor, 2516);
            args.type = sActor.Status.attackType;
            SkillHandler.Instance.PhysicalAttack(sActor, new List<Actor>() { dActor }, args, SkillHandler.DefType.Def, sActor.WeaponElement, 0, factor, false, 0, false, 0, critbonus);
        }
    }
}
