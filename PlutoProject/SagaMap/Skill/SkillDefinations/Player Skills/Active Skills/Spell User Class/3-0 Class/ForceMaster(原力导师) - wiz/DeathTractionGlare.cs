using SagaDB.Actor;
using SagaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Skill.SkillDefinations.ForceMaster
{
    /// <summary>
    /// デストラクショングレアー (DeathTractionGlare)
    /// </summary>
    public class DeathTractionGlare : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //初始威力
            float factor = 12.5f + 0.5f * level;
            //前置追加威力
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                //检测是否存在3298
                if (pc.Skills2_1.ContainsKey(3298) || pc.DualJobSkill.Exists(x => x.ID == 3298))
                {
                    //这里取副职等级
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 3298))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3298).Level;

                    //这里取主职等级
                    var mainlv = 0;
                    if (pc.Skills2_1.ContainsKey(3298))
                        mainlv = pc.Skills2_1[3298].Level;

                    //这里取等级最高等级用来做居合的倍率加成
                    factor += Math.Max(duallv, mainlv) * 2.0f - 0.5f;
                }


            }
            //获取当前地图
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (sActor.type != ActorType.PC)
            {
                int longth = Map.Distance(sActor, dActor);
                if (SagaLib.Global.Random.Next(1, 100) < 50)
                {
                    List<Actor> actorszero = map.GetActorsArea(sActor, 700, true);
                    foreach (Actor i in actorszero)
                    {
                        if (Map.Distance(sActor, i) > longth)
                        {
                            longth = Map.Distance(sActor, i);
                            dActor = i;
                            break;
                        }

                    }
                }


            }
            //获取设置中心3*3范围的怪物
            List<Actor> actors = map.GetActorsArea(dActor, 200, true);
            List<Actor> affected = new List<Actor>();
            args.affectedActors.Clear();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }
            //发送一个无属性aoe伤害
            SkillHandler.Instance.MagicAttack(sActor, affected, args, Elements.Neutral, factor);
            //念咒结束后2秒追加一个第二段伤害
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(3429, level, 2000));
        }

        #endregion
    }
}
