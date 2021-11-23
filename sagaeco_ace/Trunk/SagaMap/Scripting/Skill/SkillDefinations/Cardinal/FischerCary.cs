using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.Cardinal
{
    class FischerCary : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float rank = 0.3f + 0.1f * level;
            uint heal = (uint)(dActor.MaxHP * rank);
            dActor.HP += heal;
            if (dActor.HP > dActor.MaxHP)
                dActor.HP = dActor.MaxHP;
            args.affectedActors.Add(dActor);
            args.Init();
            if (args.flag.Count > 0)
            {
                args.flag[0] |= SagaLib.AttackFlag.HP_HEAL | SagaLib.AttackFlag.NO_DAMAGE;
                args.hp[0]=((int)heal);
            }
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, dActor, true);
        }
        #endregion
    }
}
