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
        private void CalcTamaireBonus(ActorPC pc)
        {
            pc.Status.hp_tamaire = 0;
            pc.Status.mp_tamaire = 0;
            pc.Status.sp_tamaire = 0;
            pc.Status.min_atk1_tamaire = 0;
            pc.Status.min_atk2_tamaire = 0;
            pc.Status.min_atk3_tamaire = 0;
            pc.Status.max_atk1_tamaire = 0;
            pc.Status.max_atk2_tamaire = 0;
            pc.Status.max_atk3_tamaire = 0;
            pc.Status.min_matk_tamaire = 0;
            pc.Status.max_matk_tamaire = 0;
            pc.Status.def_add_tamaire = 0;
            pc.Status.mdef_add_tamaire = 0;
            pc.Status.hit_melee_tamaire = 0;
            pc.Status.hit_ranged_tamaire = 0;
            pc.Status.avoid_melee_tamaire = 0;
            pc.Status.avoid_ranged_tamaire = 0;
            Network.Client.MapClient.FromActorPC(pc).SendCharInfoUpdate();
            return;
        }
    }

 
}
