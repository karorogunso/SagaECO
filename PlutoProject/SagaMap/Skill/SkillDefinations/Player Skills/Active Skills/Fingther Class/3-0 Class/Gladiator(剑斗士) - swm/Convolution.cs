
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Gladiator
{
    /// <summary>
    /// 大车轮 (大車輪)
    /// </summary>
    public class Convolution : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type != ActorType.PC)
            {
                level = 5;
            }
            float factor = 1.3f + 0.5f * level;
            ActorPC pc = (ActorPC)sActor;
            //if (pc.SkillsReserve.ContainsKey(2116))
            //{
            //    factor += 1.5f + 0.5f * pc.SkillsReserve[2116].Level;
            //}
            //else if (pc.Skills2_1.ContainsKey(2116))
            //{
            //    factor += 1.5f + 0.5f * pc.Skills2_1[2116].Level;
            //}
            //这里取副职的旋风剑等级
            var duallv = 0;
            if (pc.DualJobSkill.Exists(x => x.ID == 2116))
                duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 2116).Level;

            //这里取主职的旋风剑等级
            var mainlv = 0;
            if (pc.Skills2_1.ContainsKey(2116))
                mainlv = pc.Skills2_1[2116].Level;

            //这里取保留职业职的旋风剑等级
            var Reservelv = 0;
            if (pc.SkillsReserve.ContainsKey(2116))
                Reservelv = pc.SkillsReserve[2116].Level;

            //这里取等级最高的旋风剑等级用来做倍率加成运算
            int tmp = Math.Max(duallv, mainlv);
            factor += (3.0f + Math.Max(tmp, Reservelv));
            //这里并不知道顿足的概率, 所以先认为的设定初始20% 每级重力波习得增加5%
            int MoveSlowDownRate = 20;
            //if (pc.SkillsReserve.ContainsKey(2354))
            //{
            //    MoveSlowDownRate += 5 * pc.SkillsReserve[2354].Level;
            //}
            //else if (pc.Skills2_1.ContainsKey(2354))
            //{
            //    MoveSlowDownRate += 5 * pc.Skills2_1[2354].Level;
            //}
            //这里取副职的重力波等级
            var duallv2 = 0;
            if (pc.DualJobSkill.Exists(x => x.ID == 2354))
                duallv2 = pc.DualJobSkill.FirstOrDefault(x => x.ID == 2354).Level;

            //这里取主职的重力波等级
            var mainlv2 = 0;
            if (pc.Skills2_1.ContainsKey(2354))
                mainlv = pc.Skills2_1[2354].Level;

            //这里取保留职业职的重力波等级
            var Reservelv2 = 0;
            if (pc.SkillsReserve.ContainsKey(2354))
                Reservelv2 = pc.SkillsReserve[2354].Level;

            //这里取等级最高的重力波等级用来做顿足率加成运算
            int tmp2 = Math.Max(duallv2, mainlv2);
            MoveSlowDownRate += 5 * Math.Max(tmp2, Reservelv2);
            //factor += (3.0f + Math.Max(tmp2, Reservelv2));

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 200, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                    SkillHandler.Instance.PushBack(sActor, act, 3);
                    if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.鈍足, MoveSlowDownRate))
                    {
                        //这里并不知道顿足的持续时间, 先暂时设定为本技能1级时持续1秒, 每级增加0.25秒 满级顿足 2.25秒
                        Additions.Global.鈍足 skill = new SagaMap.Skill.Additions.Global.鈍足(args.skill, act, (int)(750 + 250 * level));
                        SkillHandler.ApplyAddition(act, skill);
                    }
                }
            }
            SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}                     
