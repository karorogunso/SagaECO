using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12103 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            switch (level)
            {
                case 1:
                    sActor.TInt["PoisonDamageUP"] = 30;
                    sActor.TInt["ApplyPoisonRate"] = 1;
                    break;
                case 2:
                    sActor.TInt["PoisonDamageUP"] = 50;
                    sActor.TInt["ApplyPoisonRate"] = 3;
                    break;
                case 3:
                    sActor.TInt["PoisonDamageUP"] = 70;
                    sActor.TInt["ApplyPoisonRate"] = 5;
                    break;
            }
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (!sActor.Status.Additions.ContainsKey("ApplyPoison"))
            {
                OtherAddition oa = new OtherAddition(args.skill, sActor, "ApplyPoison", 30000);
                oa.OnAdditionStart += (s, e) =>
                {
                    sActor.Buff.WeaponDark = true;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
                };
                oa.OnAdditionEnd += (s, e) =>
                {
                    sActor.TInt["PoisonDamageUP"] = 0;
                    sActor.TInt["ApplyPoisonRate"] = 0;
                    sActor.Buff.WeaponDark = false;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
                };
                SkillHandler.ApplyAddition(sActor, oa);
            }
            else
            {
                Addition ApplyPoison = sActor.Status.Additions["ApplyPoison"];
                TimeSpan span = new TimeSpan(0, 0, 0, 0, 30000);
                ((OtherAddition)ApplyPoison).endTime = DateTime.Now + span;
            }
            SkillHandler.Instance.ShowEffectOnActor(sActor, 5119);
        }
    }
}
