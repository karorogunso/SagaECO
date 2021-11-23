
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Ranger
{
    /// <summary>
    /// 濕滑陷阱（スリップトラップ）
    /// </summary>
    public class CswarSleep : Trap
    {
        bool MobUse;
        public CswarSleep()
            : base(false, PosType.sActor)
        {
            this.MobUse = false;
        }
        public CswarSleep(bool MobUse)
            : base(false, PosType.sActor)
        {
            this.MobUse = MobUse;
        }
        public override void BeforeProc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            LifeTime = 2000 + 1000 * level;
            uint[] Ranges={0,100,200,200,300,300};
            Range = Ranges[level];
        }
        public override void  ProcSkill(Actor sActor, Actor mActor, ActorSkill actor, SkillArg args, Map map, int level,float factor)
        {
            if (MobUse)
            {
                level = 5;
            }
            int[] lifetimes = { 0, 3000, 4000, 5000, 5000, 6000 };
            int rate = 20 + 10 * level;
            int lifetime = lifetimes[level];
            if (!mActor.Status.Additions.ContainsKey("Sleep"))
            {
                if (SkillHandler.Instance.CanAdditionApply(sActor, mActor, SkillHandler.DefaultAdditions.Sleep , rate))
                {
                    MoveSpeedDown sk = new MoveSpeedDown(args.skill, mActor, lifetime);
                    SkillHandler.ApplyAddition(mActor, sk);
                }
            }
            if (!mActor.Status.Additions.ContainsKey("硬直"))
            {
                if (SkillHandler.Instance.CanAdditionApply(sActor, mActor, SkillHandler.DefaultAdditions.硬直 , rate))
                {
                    Stiff sk2 = new Stiff(args.skill, mActor, lifetime);
                    SkillHandler.ApplyAddition(mActor, sk2);
                }
            }

        }     
    }
}