using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    ///  百鬼哭（百鬼哭）
    /// </summary>
    public class aHundredSpriteCry : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }

        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args    , byte level)
        {
            if(sActor.type!=ActorType.PC)
            {
                level = 5;
            }
            float factor = new float[] { 0, 1.7f, 1.8f, 1.9f, 2.0f, 2.1f, 2.5f }[level];

            int[] attackTimes = { 0, 3, 3, 4, 4, 5, 10 };
            if (sActor is ActorPC)
            {
                int lv = 0;
                ActorPC pc = sActor as ActorPC;
                //不管是主职还是副职, 只要习得剑圣技能, 都会导致combo成立, 这里一步就行了
                if (pc.Skills3.ContainsKey(1117) || pc.DualJobSkill.Exists(x => x.ID == 1117))
                {
                    //lv = pc.Skills3[1117].Level;
                    //这里取副职的剑圣等级
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 1117))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 1117).Level;

                    //这里取主职的剑圣等级
                    var mainlv = 0;
                    if (pc.Skills3.ContainsKey(1117))
                        mainlv = pc.Skills3[1117].Level;

                    //这里取等级最高的剑圣等级用来做倍率加成
                    //18.01.21修正剑圣提升倍率
                    //factor += (2.4f + 0.1f * Math.Max(duallv, mainlv));
                    factor += (0.3f + 0.1f * Math.Max(duallv, mainlv));
                    //ジリオンブレイド
                    SkillHandler.Instance.SetNextComboSkill(sActor, 2534);
                }
            }
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.SLASH;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < attackTimes[level]; i++)
                dest.Add(dActor);
            args.delayRate = 4.5f;
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
