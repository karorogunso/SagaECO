using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 狂放舞蹈（ワイルドダンス）
    /// </summary>
    public class WildDance2:ISkill 
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(pc, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }

        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0.4f * level;
            //uint MartialArtDamUp_SkillID = 125;
            ActorPC actorPC = (ActorPC)sActor;
            ActorPC pc = sActor as ActorPC;
            if (pc.Skills2_2.ContainsKey(125) || pc.DualJobSkill.Exists(x => x.ID == 125))
            {
                //这里取副职的加成技能专精等级
                var duallv = 0;
                if (pc.DualJobSkill.Exists(x => x.ID == 125))
                    duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 125).Level;

                //这里取主职的加成技能等级
                var mainlv = 0;
                if (pc.Skills2_2.ContainsKey(125))
                    mainlv = pc.Skills2_2[125].Level;

                //这里取等级最高的加成技能等级用来做倍率加成
                int maxlv = Math.Max(duallv, mainlv);
                //ParryResult += pc.Skills[116].Level * 3;
                factor += 0.24f * maxlv;
            }
            //if (actorPC.Skills2.ContainsKey(MartialArtDamUp_SkillID))
            //{
            //    if (actorPC.Skills2[MartialArtDamUp_SkillID].Level == 3)
            //    {
            //        factor = 0.52f + 0.52f * level;
            //    }
            //}
            //if (actorPC.SkillsReserve.ContainsKey(MartialArtDamUp_SkillID))
            //{
            //    if (actorPC.SkillsReserve[MartialArtDamUp_SkillID].Level == 3)
            //    {
            //        factor = 0.52f + 0.52f * level;
            //    }
            //}
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.BLOW;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < 4; i++)
            {
                dest.Add(dActor);
            }
            args.delayRate = 4f;
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
