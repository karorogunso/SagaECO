using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Eraser
{
    /// <summary>
    /// ヴェノムブラスト
    /// </summary>
    public class VenomBlast : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.60f + 0.2f * level;


            if (sActor is ActorPC)
            {
                var pc = sActor as ActorPC;
                //if (pc.SkillsReserve.ContainsKey(2142))//毒霧
                //{
                //    factor += 2.0f + 0.3f * pc.SkillsReserve[2142].Level;
                //}
                //else if (pc.Skills2_1.ContainsKey(2142))
                //{
                //    factor += 2.0f + 0.3f * pc.Skills2_1[2142].Level;
                //}
                //在所有可能的位置搜索毒雾技能
                if (pc.SkillsReserve.ContainsKey(2142) || pc.Skills2_1.ContainsKey(2142) || pc.DualJobSkill.Exists(x => x.ID == 2142))
                {
                    //这里取副职的毒雾等级
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 2142))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 2142).Level;

                    //这里取主职的毒雾等级
                    var mainlv = 0;
                    if (pc.Skills2_1.ContainsKey(2142))
                        mainlv = pc.Skills2_1[2142].Level;

                    //这里取主职的毒雾等级
                    var Reservelv = 0;
                    if (pc.SkillsReserve.ContainsKey(2142))
                        Reservelv = pc.SkillsReserve[2142].Level;

                    //这里取等级最高的毒雾等级参与运算
                    int tmp = Math.Max(duallv, mainlv);
                    factor += (2.0f + 0.3f * Math.Max(tmp, Reservelv));
                    //待检验,可能有错误
                }

            }
            int elements;
            if (sActor.WeaponElement != SagaLib.Elements.Neutral)
            {
                elements = sActor.Status.attackElements_item[sActor.WeaponElement]
                                    + sActor.Status.attackElements_skill[sActor.WeaponElement]
                                    + sActor.Status.attackelements_iris[sActor.WeaponElement];
            }
            else
            {
                elements = 0;
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            //int damaga = SkillHandler.Instance.CalcDamage(true, sActor, dActor, args, SkillHandler.DefType.Def, sActor.WeaponElement, elements, factor);
            //SkillHandler.Instance.CauseDamage(sActor, dActor, damaga);
            //SkillHandler.Instance.ShowVessel(dActor, damaga);





            //SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            //args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(2542, level, 1000));
        }
        #endregion
    }
}
