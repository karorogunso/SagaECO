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
    /// 草鞭（プルウィップ）
    /// </summary>
    public class PullWhip : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.ROPE) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
                {
                    return 0;
                }
                else
                {
                    return -14;
                }
            }
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.2f + 0.7f * level;
            if(sActor.type==ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Skills2_2.ContainsKey(2337) || pc.DualJobSkill.Exists(x => x.ID == 2337))
                {

                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 2337))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 2337).Level;
                    
                    var mainlv = 0;
                    if (pc.Skills2_2.ContainsKey(2337))
                        mainlv = pc.Skills2_2[2337].Level;

                    int CATCH_Level = Math.Max(duallv, mainlv);
                    if (CATCH_Level <= 2)
                    {
                        factor += 1.15f + 0.75f * level;
                    }
                    else if (CATCH_Level > 2 && CATCH_Level < 4)
                    {
                        factor = 1.35f + 0.75f * level;
                    }
                    else
                    {
                        factor = 1.40f + 0.75f * level;
                    }
                }
                if (pc.Skills3.ContainsKey(992) || pc.DualJobSkill.Exists(x => x.ID == 992))
                {

                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 992))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 992).Level;

                    //这里取主职的剑圣等级
                    var mainlv = 0;
                    if (pc.Skills3.ContainsKey(992))
                        mainlv = pc.Skills3[992].Level;

                    //这里取等级最高的剑圣等级用来做居合的倍率加成
                    factor += 0.1f * Math.Max(duallv, mainlv);
                }
            }
            //if (sActorPC.Skills2.ContainsKey(CATCH_SkillID))
            //{
            //    int CATCH_Level= sActorPC.Skills2[CATCH_SkillID].Level;
            //    if (CATCH_Level <= 2)
            //    {
            //        factor += 1.15f + 0.75f * level;
            //    }
            //    else if (CATCH_Level > 2 && CATCH_Level < 4)
            //    {
            //        factor = 1.35f + 0.75f * level;
            //    }
            //    else
            //    {
            //        factor = 1.40f + 0.75f * level;
            //    }
            //}
            //if (sActorPC.SkillsReserve.ContainsKey(CATCH_SkillID))
            //{
            //    int CATCH_Level = sActorPC.Skills2[CATCH_SkillID].Level;
            //    if (CATCH_Level <= 2)
            //    {
            //        factor += 1.15f + 0.75f * level;
            //    }
            //    else if (CATCH_Level > 2 && CATCH_Level < 4)
            //    {
            //        factor = 1.35f + 0.75f * level;
            //    }
            //    else
            //    {
            //        factor = 1.40f + 0.75f * level;
            //    }
            //}
            //保留原始职业判定方式以防万一
            short[] pos = new short[2];
            pos[0] = sActor.X;
            pos[1] = sActor.Y;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            int lifetime = 1000;
            Stiff dskill = new Stiff(args.skill, dActor, lifetime);
            map.MoveActor(Map.MOVE_TYPE.START, dActor, pos, dActor.Dir, 20000, true, MoveType.BATTLE_MOTION);
            SkillHandler.ApplyAddition(dActor, dskill);
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            if (dActor.type == ActorType.MOB)
            {
                if(!SkillHandler.Instance.isBossMob((ActorMob)dActor))
                {
                    Stiff skill = new Stiff(args.skill, dActor, 1000);
                    SkillHandler.ApplyAddition(dActor, skill);
                }
            }
        }
        #endregion
    }
}