
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Gunner
{
    /// <summary>
    /// 徹甲彈（徹甲弾）
    /// </summary>
    public class ApiBullet : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return SkillHandler.Instance.CheckPcGunAndBullet(sActor);
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PcBulletDown(sActor);
            float factor = 3.5f + 0.5f * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 900, false);
            List<Actor> realAffected = new List<Actor>();
            SkillHandler.ActorDirection dir = SkillHandler.Instance.GetDirection(sActor);
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    int XDiff, YDiff;
                    SkillHandler.Instance.GetXYDiff(map, sActor, act, out XDiff, out YDiff);
                    switch (dir)
                    {
                        case SkillHandler.ActorDirection.East:
                            if (YDiff == 0 && XDiff > 0)
                            {
                                realAffected.Add(act);
                            }
                            break;
                        case SkillHandler.ActorDirection.North:
                            if (XDiff == 0 && YDiff > 0)
                            {
                                realAffected.Add(act);
                            }
                            break;
                        case SkillHandler.ActorDirection.South:
                            if (XDiff == 0 && YDiff < 0)
                            {
                                realAffected.Add(act);
                            }
                            break;
                        case SkillHandler.ActorDirection.West:
                            if (YDiff == 0 && XDiff < 0)
                            {
                                realAffected.Add(act);
                            }
                            break;
                    }
                }
            }
            SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}