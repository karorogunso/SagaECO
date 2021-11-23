using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 極速連擊（ラッシュ）
    /// </summary>
    public class Rush : ISkill
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
            float factor;// = 0.75f + 0.15f * level;
            //uint MartialArtDamUp_SkillID = 125;
            ActorPC actorPC = (ActorPC)sActor;
            //if (actorPC.Skills2.ContainsKey(MartialArtDamUp_SkillID))
            //{
            //    if (actorPC.Skills2[MartialArtDamUp_SkillID].Level == 3)
            //    {
            //        factor = 0.98f + 0.32f * level;
            //    }
            //}
            //if (actorPC.SkillsReserve.ContainsKey(MartialArtDamUp_SkillID))
            //{
            //    if (actorPC.SkillsReserve[MartialArtDamUp_SkillID].Level == 3)
            //    {
            //        factor = 0.98f + 0.32f * level;
            //    }
            //}
            var duallv = 0;
            if (actorPC.DualJobSkill.Exists(x => x.ID == 125))
                duallv = actorPC.DualJobSkill.FirstOrDefault(x => x.ID == 125).Level;

            var mainlv = 0;
            if (actorPC.Skills2_2.ContainsKey(125))
                mainlv = actorPC.Skills2_2[125].Level;

            var duallv3 = 0;
            if (actorPC.DualJobSkill.Exists(x => x.ID == 984))
                duallv3 = actorPC.DualJobSkill.FirstOrDefault(x => x.ID == 984).Level;

            var mainlv3 = 0;
            if (actorPC.Skills3.ContainsKey(984))
                mainlv3 = actorPC.Skills3[984].Level;

            if ((mainlv == 3 || duallv == 3) && (mainlv3 == 5 || mainlv3 == 5))
            {
                factor = new float[] { 0, 1.85f, 2.15f, 2.46f, 2.76f, 3.08f }[level];
            }
            else if ((mainlv == 3 || duallv == 3) && (mainlv3 != 5 || mainlv3 != 5))
            {
                factor = new float[] { 0, 1.17f, 1.36f, 1.56f, 1.75f, 1.95f }[level];
            }
            else if ((mainlv != 3 || duallv != 3) && (mainlv3 == 5 || mainlv3 == 5))
            {
                factor = new float[] { 0, 1.42f, 1.65f, 1.89f, 2.13f, 2.37f }[level];
            }
            else
            {
                factor = new float[] { 0, 0.9f, 1.05f, 1.2f, 1.35f, 1.5f }[level];
            }
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.BLOW;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < 4; i++)
            {
                dest.Add(dActor);
            }
            args.delayRate = 4.5f;
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
