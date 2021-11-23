using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12114 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("沙月剧毒丸CD"))
                return -30;
            if (pc.Status.Additions.ContainsKey("Poison2"))
                return -7;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            StableAddition skill = new StableAddition(null, sActor, "沙月剧毒丸CD", 120000);
            skill.OnAdditionEnd += (s, e) =>
             {
                 SkillHandler.SendSystemMessage(sActor, "可以再次使用『沙月剧毒丸』了。");
                 SkillHandler.Instance.ShowEffectOnActor(sActor, 4381);
             };
            SkillHandler.ApplyAddition(sActor, skill);

            //OtherAddition skill2 = new OtherAddition(null, sActor, "沙月剧毒丸吸血", 22000);
            //SkillHandler.ApplyAddition(sActor, skill2);
            //int damage = (int)(sActor.MaxHP * 0.1f);
            Poison2 p2 = new Poison2(args.skill, sActor, 22000, 38);
            SkillHandler.ApplyAddition(sActor, p2);
            //AtkUp au = new AtkUp(args.skill, sActor, 15000,300);
            //SkillHandler.ApplyAddition(sActor, au);

            OtherAddition skill2 = new OtherAddition(null, sActor, "沙月剧毒丸伤害提升", 15000);
            SkillHandler.ApplyAddition(sActor, skill2);


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
            sActor.EP = sActor.MaxEP;
            SkillHandler.Instance.ShowVessel(sActor, 0, (int)-sActor.MP, (int)-sActor.SP);
        }
        #endregion
    }
}
