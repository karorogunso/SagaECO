using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    class WindStorm : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.8f;
            ActorSkill actorS = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //Logger.ShowDebug(string.Format("劫雷滅世=>({0},{1})",args.x, args.y), Logger.defaultlogger);
            //Logger.ShowDebug(string.Format("劫雷滅世=>({0},{1})", SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height)), Logger.defaultlogger);
            List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 300, null);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }

            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Wind, factor);
        }
        #endregion
    }
}
