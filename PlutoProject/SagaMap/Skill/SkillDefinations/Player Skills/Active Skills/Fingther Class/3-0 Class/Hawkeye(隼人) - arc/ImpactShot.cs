using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations.Hawkeye
{
    /// <summary>
    /// ジリオンブレイド
    /// </summary>
    public class ImpactShot : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return SkillHandler.Instance.CheckPcLongAttack(sActor);
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PcArrowAndBulletDown(sActor);
            float factor = new float[] { 0, 9f, 9.5f, 10f, 10.5f, 11f }[level];
            int movenum = new int[] { 0, 0, 2, 3, 4, 5 }[level];
            int Stuntime = new int[] { 0, 2500, 3500, 3500, 4500, 5000 }[level];

            List<Actor> actors = Manager.MapManager.Instance.GetMap(sActor.MapID).GetActorsArea(dActor, 100, true);
            List<Actor> affected = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    affected.Add(item);
                    if (SkillHandler.Instance.CanAdditionApply(sActor, item, SkillHandler.DefaultAdditions.Stun, 5 * level + 50))
                    {
                        Additions.Global.Stun skill = new SagaMap.Skill.Additions.Global.Stun(args.skill, item, Stuntime);
                        SkillHandler.ApplyAddition(item, skill);
                    }
                }


            }
            args.type = ATTACK_TYPE.STAB;
            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, sActor.WeaponElement, factor);
            if (level > 1)
                SkillHandler.Instance.PushBack(dActor, sActor, movenum,20000,MoveType.VANISH);
        }
    }
}
