using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19105:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("Poison2"))
                return -7;
             return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int damage = (int)(sActor.MaxHP * 0.1f);
            Poison2 p2 = new Poison2(args.skill, sActor, 22000, 70);
            SkillHandler.ApplyAddition(sActor, p2);
            AtkUp au = new AtkUp(args.skill, sActor, 15000,150);
            SkillHandler.ApplyAddition(sActor, au);
            AspdUp asu = new AspdUp(args.skill, sActor, 15000, 400);
            SkillHandler.ApplyAddition(sActor, asu);
            HitCriUp hcu = new HitCriUp(args.skill, sActor, 15000, 50);
            SkillHandler.ApplyAddition(sActor, hcu);
            HitMeleeUp hmu = new HitMeleeUp(args.skill, sActor, 15000, 80);
            SkillHandler.ApplyAddition(sActor, hmu);
            HitRangeUp hru = new HitRangeUp(args.skill, sActor, 15000, 80);
            SkillHandler.ApplyAddition(sActor, hru);

            sActor.MP = sActor.MaxMP;
            sActor.SP = sActor.MaxSP;
            SkillHandler.Instance.ShowVessel(sActor, (int)-sActor.MP, (int)-sActor.SP);
        }
        #endregion
    }
}
