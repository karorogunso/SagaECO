using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    public class WaterStorm : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            //short TestX, TestY;
            switch (level)
            {
                case 1:
                    factor = 1.2f;
                    break;
                case 2:
                    factor = 1.7f;
                    break;
                case 3:
                    factor = 2.2f;
                    break;
                case 4:
                    factor = 3.7f;
                    break;
                case 5:
                    factor = 3.2f;
                    break;
            }
            ActorSkill actorS = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //actorS.MapID = sActor.MapID;
            //actorS.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            //actorS.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            //actorS.e = new ActorEventHandlers.NullEventHandler();
            //map.RegisterActor(actorS);
            //actorS.invisble = false;
            //List<Actor> actors = map.GetActorsArea(actorS, 300, false);
            List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 200);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }
            //map.DeleteActor(actorS);

            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Water, factor);
        }
        #endregion
    }
}
