using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.SunFlowerAdditions
{
    /// <summary>
    /// 审判(Ragnarok)
    /// </summary>
    public class Judex : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
         
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 4.0f;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            List<Actor> actors = Manager.MapManager.Instance.GetMap(sActor.MapID).GetActorsArea(dActor, 200, true);
            List<Actor> affected = new List<Actor>();
            foreach (var item in actors)
            {
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                        affected.Add(item);
            }
                    

            //List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 200, null);
            //List<Actor> affected = new List<Actor>();
            //foreach (Actor i in actors)
            //{
            //    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
            //        affected.Add(i);
            //}
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Holy, factor);

        }
        
        #endregion
    }
}
