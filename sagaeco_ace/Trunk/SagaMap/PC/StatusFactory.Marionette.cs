using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;

namespace SagaMap.PC
{
    public partial class StatusFactory : Singleton<StatusFactory>
    {
        private void CalcMarionetteBonus(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                pc.Status.agi_mario = pc.Marionette.agi;
                pc.Status.def_add_mario = pc.Marionette.def_add;
                pc.Status.def_mario = pc.Marionette.def;
                pc.Status.dex_mario = pc.Marionette.dex;
                pc.Status.hp_mario = pc.Marionette.hp;
                pc.Status.hp_recover_mario = pc.Marionette.hp_recover;
                pc.Status.int_mario = pc.Marionette.intel;
                pc.Status.mag_mario = pc.Marionette.mag;
                pc.Status.max_atk1_mario = pc.Marionette.max_atk1;
                pc.Status.max_atk2_mario = pc.Marionette.max_atk2;
                pc.Status.max_atk3_mario = pc.Marionette.max_atk3;
                pc.Status.min_atk1_mario = pc.Marionette.min_atk1;
                pc.Status.min_atk2_mario = pc.Marionette.min_atk2;
                pc.Status.min_atk3_mario = pc.Marionette.min_atk3;
                pc.Status.max_matk_mario = pc.Marionette.max_matk;
                pc.Status.min_matk_mario = pc.Marionette.min_matk;
                pc.Status.mdef_add_mario = pc.Marionette.mdef_add;
                pc.Status.mdef_mario = pc.Marionette.mdef;
                pc.Status.mp_mario = pc.Marionette.mp;
                pc.Status.mp_recover_mario = pc.Marionette.mp_recover;
                pc.Status.sp_mario = pc.Marionette.sp;
                pc.Status.str_mario = pc.Marionette.str;
                pc.Status.vit_mario = pc.Marionette.vit;
            }
            else
            {
                pc.Status.agi_mario = 0;
                pc.Status.def_add_mario = 0;
                pc.Status.def_mario = 0;
                pc.Status.dex_mario = 0;
                pc.Status.hp_mario = 0;
                pc.Status.hp_recover_mario = 0;
                pc.Status.int_mario = 0;
                pc.Status.mag_mario = 0;
                pc.Status.max_atk1_mario = 0;
                pc.Status.max_atk2_mario = 0;
                pc.Status.max_atk3_mario = 0;
                pc.Status.min_atk1_mario = 0;
                pc.Status.min_atk2_mario = 0;
                pc.Status.min_atk3_mario = 0;
                pc.Status.max_matk_mario = 0;
                pc.Status.min_matk_mario = 0;
                pc.Status.mdef_add_mario = 0;
                pc.Status.mdef_mario = 0;
                pc.Status.mp_mario = 0;
                pc.Status.mp_recover_mario = 0;
                pc.Status.sp_mario = 0;
                pc.Status.str_mario = 0;
                pc.Status.vit_mario = 0;
            }
        }
    }

 
}
