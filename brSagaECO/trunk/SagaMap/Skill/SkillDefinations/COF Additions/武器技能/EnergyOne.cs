using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Wizard
{
    public class EnergyOneForWeapon : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(pc, dActor))
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
            SkillArg arg2 = new SkillArg();
            arg2 = new SkillArg();
            arg2.sActor = ((ActorPC)sActor).ActorID;
            arg2.type = (ATTACK_TYPE)0xff;
            arg2.affectedActors.Add(((ActorPC)sActor));
            arg2.Init();
            SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, arg2, ((ActorPC)sActor), true);
            if (sActor.type == ActorType.PC)
            {
                short WeaponATK = ((ActorPC)sActor).Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.matk;
                short mpadd = 0;
                if (WeaponATK > 10)
                    WeaponATK = (short)(10 + (short)((WeaponATK - 10) * 0.2f));
                //if (((ActorPC)sActor).MaxHealMpForWeapon > WeaponATK)
                //    mpadd = WeaponATK;
                //else
                //    mpadd = ((ActorPC)sActor).MaxHealMpForWeapon;

                ((ActorPC)sActor).MP += (uint)mpadd;
                if (((ActorPC)sActor).MP > ((ActorPC)sActor).MaxMP)
                    ((ActorPC)sActor).MP = ((ActorPC)sActor).MaxMP;
                SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendActorHPMPSP(sActor);

                float factor = 0.8f;
                SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);


            }
        }

        #endregion
    }
}
