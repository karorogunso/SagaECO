using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19003 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 5f;
            List<Actor> da = new List<Actor>();
            da.Add(dActor);
            uint daoriHP = dActor.HP;
            int damage = SkillHandler.Instance.PhysicalAttack(sActor,da,args,SkillHandler.DefType.Def,Elements.Neutral,0,factor,false,0.3f,false);

            if(damage > daoriHP)
            {
                uint heal = (uint)(damage * 3);
                sActor.HP += heal;
                if (sActor.HP > sActor.MaxHP)
                    sActor.HP = sActor.MaxHP;
                SkillHandler.Instance.ShowVessel(sActor,(int)-heal);
                sActor.EP += 1000;
            }
            sActor.EP += 500;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
        #endregion
    }
}
