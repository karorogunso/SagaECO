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
    /// 音速鞭子（ソニックウィップ）起始阶段(拉人)
    /// </summary>
    public class SonicWhip : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (!SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.ROPE) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                return -5;
            }
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            float factor = 0.3f + 0.25f * level;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Skills3.ContainsKey(992) || pc.DualJobSkill.Exists(x => x.ID == 992))
                {

                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 992))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 992).Level;
                    var mainlv = 0;
                    if (pc.Skills3.ContainsKey(992))
                        mainlv = pc.Skills3[992].Level;
                    factor += 0.1f * Math.Max(duallv, mainlv);
                }
            }
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.SLASH;
            args.delayRate = 4.5f;
            short[] pos = new short[2];
            pos[0] = sActor.X;
            pos[1] = sActor.Y;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.MoveActor(Map.MOVE_TYPE.START, dActor, pos, dActor.Dir, 20000, true, MoveType.BATTLE_MOTION);
            //int lifetime = 1000;
            //Stiff dskill = new Stiff(args.skill, dActor, lifetime);
            //SkillHandler.ApplyAddition(dActor, dskill);

            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            if(!dActor.Buff.Dead)
            {
                AutoCastInfo aci = SkillHandler.Instance.CreateAutoCastInfo(2431, level, 2000);
                args.autoCast.Add(aci);
            }
            
        }
        #endregion
    }
}