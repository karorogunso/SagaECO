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
        private void CalcAnotherBonus(ActorPC pc)
        {
            pc.Status.str_another = 0;
            pc.Status.mag_another = 0;
            pc.Status.agi_another = 0;
            pc.Status.dex_another = 0;
            pc.Status.vit_another = 0;
            pc.Status.int_another = 0;
            pc.Status.hp_another = 0;
            pc.Status.mp_another = 0;
            pc.Status.sp_another = 0;
            pc.Status.min_atk1_another = 0;
            pc.Status.min_atk2_another = 0;
            pc.Status.min_atk3_another = 0;
            pc.Status.max_atk1_another = 0;
            pc.Status.max_atk2_another = 0;
            pc.Status.max_atk3_another = 0;
            pc.Status.min_matk_another = 0;
            pc.Status.max_matk_another = 0;
            pc.Status.def_add_another = 0;
            pc.Status.mdef_add_another = 0;
            pc.Status.hit_melee_another = 0;
            pc.Status.hit_ranged_another = 0;
            pc.Status.avoid_melee_another = 0;
            pc.Status.avoid_ranged_another = 0;
            Network.Client.MapClient.FromActorPC(pc).SendCharInfoUpdate();
            return;
        }
    }

 
}
