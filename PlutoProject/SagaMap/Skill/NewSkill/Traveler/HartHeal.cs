using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Mob;
using SagaDB.Skill;

namespace SagaMap.Skill.SkillDefinations.Traveler
{
    class HartHeal : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = -1;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //向右的判定矩型

            List<Actor> actors = map.GetRoundAreaActors(dActor.X,dActor.Y,300);
            //List<Actor> actors = map.GetRoundAreaActors(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 300);
     
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                //if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }
            //SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Holy, factor);
       
            SkillHandler.Instance.MagicAttack(sActor, affected, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
