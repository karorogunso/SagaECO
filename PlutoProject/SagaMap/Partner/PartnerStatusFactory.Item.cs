using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Partner;
using SagaDB.Iris;
using SagaDB.DEMIC;

namespace SagaMap.Partner
{
    public partial class StatusFactory : Singleton<StatusFactory>
    {
        private void ClearPartnerEquipBouns(ActorPartner partner)
        {

            partner.Status.atk1_item = 0;
            partner.Status.atk2_item = 0;
            partner.Status.atk3_item =0;
            partner.Status.matk_item =0;

            partner.Status.def_item = 0; //装备提供左防（%）
            partner.Status.mdef_item = 0;  //装备提供左防（%）
            partner.Status.hit_melee_item = 0;
            partner.Status.hit_ranged_item = 0;
            partner.Status.avoid_melee_item = 0;
            partner.Status.avoid_ranged_item = 0;
            partner.Status.hit_critical_item = 0;
            partner.Status.avoid_critical_item = 0;
            partner.Status.hit_magic_item = 0;
            partner.Status.avoid_magic_item = 0;

            partner.Status.hp_item = 0;
            partner.Status.sp_item = 0;
            partner.Status.mp_item = 0;
            partner.Status.speed_item = 0;
            partner.Status.hp_recover_item = 0;
            partner.Status.mp_recover_item = 0;
            partner.Status.sp_recover_item = 0;
            partner.Status.aspd_item = 0;
            partner.Status.cspd_item = 0;

        }
        private void CalcPartnerEquipBonus(ActorPartner partner)
        {
            partner.Status.ClearItem();
            Dictionary<EnumPartnerEquipSlot, Item> equips = partner.equipments;
            foreach (EnumPartnerEquipSlot j in equips.Keys)
            {
                Item i = equips[j];
                if (i.Stack == 0)
                    continue;
                partner.Status.atk1_item = (short)(partner.Status.atk1_item + i.BaseData.atk1 + i.Atk1);
                partner.Status.atk2_item = (short)(partner.Status.atk2_item + i.BaseData.atk2 + i.Atk2);
                partner.Status.atk3_item = (short)(partner.Status.atk3_item + i.BaseData.atk3 + i.Atk3);
                partner.Status.matk_item = (short)(partner.Status.matk_item + i.BaseData.matk + i.MAtk);

                partner.Status.def_item = (short)(partner.Status.def_item + i.BaseData.def + i.Def); //装备提供左防（%）
                partner.Status.mdef_item = (short)(partner.Status.mdef_item + i.BaseData.mdef + i.MDef);  //装备提供左防（%）
                partner.Status.hit_melee_item = (short)(partner.Status.hit_melee_item + i.BaseData.hitMelee + i.HitMelee);
                partner.Status.hit_ranged_item = (short)(partner.Status.hit_ranged_item + i.BaseData.hitRanged + i.HitRanged);
                partner.Status.avoid_melee_item = (short)(partner.Status.avoid_melee_item + i.BaseData.avoidMelee + i.AvoidMelee);
                partner.Status.avoid_ranged_item = (short)(partner.Status.avoid_ranged_item + i.BaseData.avoidRanged + i.AvoidRanged);
                partner.Status.hit_critical_item = (short)(partner.Status.hit_critical_item + i.BaseData.hitCritical + i.HitCritical);
                partner.Status.avoid_critical_item = (short)(partner.Status.avoid_critical_item + i.BaseData.avoidCritical + i.AvoidCritical);
                partner.Status.hit_magic_item = (short)(partner.Status.hit_magic_item + i.BaseData.hitMagic + i.HitMagic);
                partner.Status.avoid_magic_item = (short)(partner.Status.avoid_magic_item + i.BaseData.avoidMagic + i.AvoidMagic);

                partner.Status.hp_item = (short)(partner.Status.hp_item + i.BaseData.hp + i.HP);
                partner.Status.sp_item = (short)(partner.Status.sp_item + i.BaseData.sp + i.SP);
                partner.Status.mp_item = (short)(partner.Status.mp_item + i.BaseData.mp + i.MP);
                partner.Status.speed_item = (int)(partner.Status.speed_item + i.BaseData.speedUp + i.SpeedUp);
                partner.Status.hp_recover_item = (short)(partner.Status.hp_recover_item + i.BaseData.hpRecover + i.HPRecover);
                //pc.Status.mp_recover_item = (short)(pc.Status.mp_recover_item + i.BaseData.mpRecover + i.MPRecover);
                //pc.Status.sp_recover_item = (short)(pc.Status.sp_recover_item + i.BaseData.spRecover + i.SPRecover); sb的gongho这时候在item里只有魔恢复力不分mp和sp了
                partner.Status.aspd_item = (short)(partner.Status.aspd_item + i.ASPD);
                partner.Status.cspd_item = (short)(partner.Status.cspd_item + i.CSPD);

                if (i.BaseData.speedUp != 0 || i.SpeedUp != 0)
                {
                    partner.e.PropertyUpdate(UpdateEvent.SPEED, 0);
                }
                if (i.BaseData.itemType == ItemType.UNION_COSTUME)
                {
                    foreach (Elements k in partner.Elements.Keys)
                    {
                        partner.Status.elements_item[k] += i.BaseData.element[k];
                    }
                }
                if (i.BaseData.itemType == ItemType.UNION_WEAPON)
                {
                    foreach (Elements k in partner.Elements.Keys)
                    {
                        partner.Status.attackElements_item[k] += i.BaseData.element[k];
                    }
                }
            }
            //SagaMap.Manager.MapClientManager.Instance.FindClient(pc).OnPlayerElements();
        }      
    }
}
