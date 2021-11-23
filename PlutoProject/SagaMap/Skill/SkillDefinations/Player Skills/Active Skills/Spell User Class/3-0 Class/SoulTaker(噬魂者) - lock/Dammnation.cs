using SagaDB.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Skill.SkillDefinations.SoulTaker
{
    /// <summary>
    /// 3431 ダムネイション (Dammnation) 天谴
    /// </summary>
    public class Dammnation : ISkill
    {
        public int TryCast(SagaDB.Actor.ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            float factor = 9.0f + 4.5f * level;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                //检测3392可能存在的位置
                if (pc.Skills3.ContainsKey(3392) || pc.DualJobSkill.Exists(x => x.ID == 3392))
                {
                    //这里取副职的3392等级
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 3392))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3392).Level;

                    //这里取主职的3392等级
                    var mainlv = 0;
                    if (pc.Skills3.ContainsKey(3392))
                        mainlv = pc.Skills3[3392].Level;

                    //这里取等级最高的3392等级用来参与运算
                    factor += 0.5f * Math.Max(duallv, mainlv);
                    //factor += pc.Skills3[3392].Level * 0.5f;
                }

                //检测3420可能存在的位置
                if (pc.Skills3.ContainsKey(3420) || pc.DualJobSkill.Exists(x => x.ID == 3420))
                {
                    //这里取副职的3420等级
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 3420))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3420).Level;

                    //这里取主职的3420等级
                    var mainlv = 0;
                    if (pc.Skills3.ContainsKey(3420))
                        mainlv = pc.Skills3[3420].Level;

                    //这里取等级最高的3420等级用来参与运算
                    factor += 0.5f * Math.Max(duallv, mainlv);
                    //factor += pc.Skills3[3420].Level * 0.5f;
                }
                    
            }
            
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (sActor.type != ActorType.PC)
            {
                List<Actor> actorszero = map.GetActorsArea(sActor, 700, true);
                foreach (Actor i in actorszero)
                {
                    if (SagaLib.Global.Random.Next(1, actorszero.Count()) == 1)
                    {
                        dActor = i;
                        break;
                    }

                }

            }
            List<Actor> actors = map.GetActorsArea(dActor, 300, true);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Dark, factor);
        }
    }
}
