
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Gambler
{
    /// <summary>
    /// 幸福輪盤（ルーレットヒーリング）
    /// </summary>
    public class RouletteHeal : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = -(1.0f + 0.6f * level);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 100, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (!SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                }
            }
            int healCount = SagaLib.Global.Random.Next(0, realAffected.Count-1);
            affected.Clear();
            for (int i = 0; i < healCount; i++)
            {
                affected.Add(realAffected[i]);
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Holy, factor);
        }
        #endregion
    }
}