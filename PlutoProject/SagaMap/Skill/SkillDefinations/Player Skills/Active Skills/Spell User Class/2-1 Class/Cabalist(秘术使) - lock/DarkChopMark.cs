
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    /// <summary>
    /// 黑暗苦痛（ダークペイン）
    /// </summary>
    public class DarkChopMark : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //float factor = 1.5f + 0.3f * level;
            //List<Actor> actors = map.GetActorsArea(sActor, 100, false);
            //List<Actor> affected = new List<Actor>();
            //foreach (Actor i in actors)
            //{
            //    if (i is ActorPC)
            //        if (!SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
            //        {
            //            affected.Add(i);
            //        }


            //}
            //SkillHandler.Instance.MagicAttack(sActor, affected, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Dark, factor);
            
        }
        #endregion
    }
}