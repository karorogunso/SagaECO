using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Title;

namespace SagaMap.PC
{
    public partial class StatusFactory : Singleton<StatusFactory>
    {
        private void CalcTitleBonus(ActorPC pc)
        {
            if (pc.MainTitle != 0 && TitleFactory.Instance.Items.ContainsKey(pc.MainTitle))
            {
                Title item = TitleFactory.Instance.Items[pc.MainTitle];
                pc.Status.hp_title = (short)item.hp;
                pc.Status.mp_title = (short)item.mp;
                pc.Status.sp_title = (short)item.sp;
                pc.Status.min_atk1_title = (short)item.atk_min;
                pc.Status.min_atk2_title = (short)item.atk_min;
                pc.Status.min_atk3_title = (short)item.atk_min;
                pc.Status.max_atk1_title = (short)item.atk_max;
                pc.Status.max_atk2_title = (short)item.atk_max;
                pc.Status.max_atk3_title = (short)item.atk_max;
                pc.Status.min_matk_title = (short)item.matk_min;
                pc.Status.max_matk_title = (short)item.matk_max;
                pc.Status.def_add_title = (short)item.def;
                pc.Status.mdef_add_title = (short)item.mdef;
                pc.Status.cri_title = (short)item.cri;
                pc.Status.cri_avoid_title = (short)item.cri_avoid;
                pc.Status.hit_melee_title = (short)item.hit_melee;
                pc.Status.hit_ranged_title = (short)item.hit_range;
                pc.Status.avoid_melee_title = (short)item.avoid_melee;
                pc.Status.avoid_ranged_title = (short)item.avoid_range;
                pc.Status.aspd_title = (short)item.aspd;
                pc.Status.cspd_title = (short)item.cspd;
            }
            else
            {
                pc.Status.hp_title = 0;
                pc.Status.mp_title =0;
                pc.Status.sp_title = 0;
                pc.Status.min_atk1_title = 0;
                pc.Status.min_atk2_title = 0;
                pc.Status.min_atk3_title = 0;
                pc.Status.max_atk1_title = 0;
                pc.Status.max_atk2_title = 0;
                pc.Status.max_atk3_title = 0;
                pc.Status.min_matk_title = 0;
                pc.Status.max_matk_title = 0;
                pc.Status.def_add_title = 0;
                pc.Status.mdef_add_title = 0;
                pc.Status.cri_title = 0;
                pc.Status.cri_avoid_title = 0;
                pc.Status.hit_melee_title = 0;
                pc.Status.hit_ranged_title = 0;
                pc.Status.avoid_melee_title = 0;
                pc.Status.avoid_ranged_title = 0;
                pc.Status.aspd_title = 0;
                pc.Status.cspd_title = 0;
            }
            Network.Client.MapClient.FromActorPC(pc).SendCharInfoUpdate();
            return;
        }
    }

 
}
