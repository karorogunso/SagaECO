using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Swordman
{
    /// <summary>
    /// 居和2段
    /// </summary>
    public class Iai2 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (CheckPossible(pc))
                return 0;
            else
                return -5;
        }

        bool CheckPossible(Actor sActor)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) || pc.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            if (CheckPossible(sActor))
            {
                args.type = ATTACK_TYPE.SLASH;
                factor = 1.2f + 0.3f * level;
                if (sActor is ActorPC)
                {
                    int lv = 0;
                    ActorPC pc = sActor as ActorPC;
                    if (pc.Skills3.ContainsKey(1117))
                    {
                        lv = pc.Skills3[1117].Level;
                        factor += (3.0f + lv);
                        //斩击无双
                        SkillHandler.Instance.SetNextComboSkill(sActor, 2401);
                        //百鬼哭
                        SkillHandler.Instance.SetNextComboSkill(sActor, 2235);
                        //神速斩
                        SkillHandler.Instance.SetNextComboSkill(sActor, 2527);
                        //霞斬り
                        SkillHandler.Instance.SetNextComboSkill(sActor, 2233);
                        //アギト砕き
                        SkillHandler.Instance.SetNextComboSkill(sActor, 2234);
                        //巨木断ち
                        SkillHandler.Instance.SetNextComboSkill(sActor, 2231);
                        //兜割り
                        SkillHandler.Instance.SetNextComboSkill(sActor, 2134);
                        //隼の太刀
                        SkillHandler.Instance.SetNextComboSkill(sActor, 2232);
                        //ジリオンブレイド
                        SkillHandler.Instance.SetNextComboSkill(sActor, 2534);
                    }
                }

                SkillHandler.Instance.SetNextComboSkill(sActor, 2202);

                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            }
        }
        #endregion
    }
}