using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// ジリオンブレイド
    /// </summary>
    public class ZillionBladeMob : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 25.0f;//ジリオンスマイトはDEF60以上で3万以上受ける。-wiki

            List<Actor> actors = Manager.MapManager.Instance.GetMap(sActor.MapID).GetActorsArea(dActor, 100, true);
            List<Actor> affected = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                    affected.Add(item);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, sActor.WeaponElement, factor);
        }
    }
}
