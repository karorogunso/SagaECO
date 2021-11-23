using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.TreasureHunter
{
    /// <summary>
    /// 鞭子拖拉（キャッチ）
    /// </summary>
    public class Catch : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.ROPE) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                if (dActor.type == ActorType.MOB)
                {
                    if(SkillHandler.Instance.isBossMob((ActorMob)dActor))
                    {
                        return -14;
                    }
                }
                return 0;
            }
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            short[] pos = new short[2];
            pos[0] = sActor.X;
            pos[1] = sActor.Y;
            //SkillHandler.Instance.FixAttack(sActor, dActor, args, sActor.WeaponElement, 1f);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            int lifetime = 1000 ;
            Stiff dskill = new Stiff(args.skill, dActor, lifetime);
            //拖拽逻辑试修复
            map.MoveActor(Map.MOVE_TYPE.START, dActor, pos, dActor.Dir, 20000, true, MoveType.BATTLE_MOTION);
            SkillHandler.ApplyAddition(dActor, dskill);

        }
        #endregion
    }
}