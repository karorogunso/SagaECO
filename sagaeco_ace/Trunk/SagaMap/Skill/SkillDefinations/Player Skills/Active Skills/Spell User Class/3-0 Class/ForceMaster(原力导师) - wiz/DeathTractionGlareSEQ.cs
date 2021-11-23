using SagaDB.Actor;
using SagaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Skill.SkillDefinations.ForceMaster
{
    /// <summary>
    /// デストラクショングレアー 后续
    /// </summary>
    public class DeathTractionGlareSEQ : ISkill
    {
        public int TryCast(SagaDB.Actor.ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            //设置威力
            float factor = 12.5f + 0.5f * level;
            //获取当前地图
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //获取设置中心3*3的怪物
            List<Actor> actors = map.GetActorsArea(dActor, 200, true);
            List<Actor> affected = new List<Actor>();
            args.affectedActors.Clear();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }
            //发送一个无属性AOE伤害
            SkillHandler.Instance.MagicAttack(sActor, affected, args, Elements.Neutral, factor);
        }
    }
}
