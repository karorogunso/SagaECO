using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12110 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            dActor = SkillHandler.Instance.GetdActor(sActor, args);
            if (dActor == null) return;
            SkillHandler.Instance.ChangdeWeapons(sActor, 0);

            byte count = (byte)(2 + sActor.TInt["音速投掷额外次数"]);
            sActor.TInt["音速投掷额外次数"]++;
            if (sActor.TInt["音速投掷额外次数"] > 7)
                sActor.TInt["音速投掷额外次数"] = 7;


            if (!sActor.Status.Additions.ContainsKey("音速投掷额外次数提升"))
            {
                sActor.TInt["音速投掷额外次数"] = 1;
                OtherAddition skill = new OtherAddition(null, sActor, "音速投掷额外次数提升", 2000);
                skill.OnAdditionEnd += (s, e) =>
                {
                    SkillHandler.Instance.ShowEffectOnActor(sActor, 4283);
                    sActor.TInt["音速投掷额外次数"] = 0;
                };
                SkillHandler.ApplyAddition(sActor, skill);
            }
            else
            {
                Addition 音速投掷额外次数 = sActor.Status.Additions["音速投掷额外次数提升"];
                TimeSpan span = new TimeSpan(0, 0, 0, 2);
                ((OtherAddition)音速投掷额外次数).endTime = DateTime.Now + span;
                if (sActor.TInt["音速投掷额外次数"] < 7)
                {
                    sActor.TInt["音速投掷额外次数"] += 1;
                }
            }

            float factor = 0.7f + level * 0.1f;
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.SLASH;
            args.delayRate = 2f;

            if (sActor.Status.Additions.ContainsKey("暗樱"))
                factor += factor * sActor.TInt["暗樱提升%"] / 100f;

            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < count; i++)
                dest.Add(dActor);
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor, false);
            if(sActor.TInt["音速投掷额外次数"]< 7)
            SkillHandler.Instance.ShowEffectByActor(sActor, 4133);
            else
                SkillHandler.Instance.ShowEffectByActor(sActor, 4163);

            uint epheal = 200;
            sActor.EP += epheal;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
    }
}
