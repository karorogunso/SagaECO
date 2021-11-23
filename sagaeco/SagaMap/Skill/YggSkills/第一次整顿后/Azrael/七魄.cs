using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class 七魄
    {
        public static ActorSkill Show(Actor sActor,Actor dActor,string name, uint id)
        {
            Map map = SkillHandler.GetActorMap(sActor);

            short[] pos = map.GetRandomPosAroundPos(dActor.X, dActor.Y, 200);

            ActorSkill actor2 = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(id, 1), sActor);
            actor2.Name = name;
            actor2.MapID = sActor.MapID;
            actor2.X = pos[0];
            actor2.Y = pos[1];
            actor2.Speed = 600;
            actor2.e = new ActorEventHandlers.NullEventHandler();
            map.RegisterActor(actor2);
            actor2.invisble = false;
            map.OnActorVisibilityChange(actor2);
            actor2.Stackable = false;

            七魄移动 t = new 七魄移动(sActor, dActor, actor2, map);
            t.Activate();

            OtherAddition lifetime = new OtherAddition(null, sActor, "["+ name +"]", 30000);
            lifetime.OnAdditionEnd += (s, e) =>
            {
                if (map.GetActor(actor2.ActorID) != null)
                    map.DeleteActor(actor2);
                t.Deactivate();
            };
            SkillHandler.ApplyAddition(sActor, lifetime);
            return actor2;
        }
    }
    public class 七魄移动 : MultiRunTask
    {
        Actor sActor;
        Actor dActor;
        ActorSkill skill;
        Map map;
        public 七魄移动(Actor actor, Actor target, ActorSkill soul,Map map)
        {
            this.map = map;
            sActor = actor;
            dActor = target;
            skill = soul;
            dueTime = 2000;
            period = 500;
        }
        public override void CallBack()
        {
            try
            {
                period = SagaLib.Global.Random.Next(200, 1000);
                if (sActor == null|| dActor == null||dActor.HP == dActor.MaxHP || sActor.Buff.Dead || dActor.Buff.Dead || sActor.MapID != map.ID)
                {
                    Deactivate();
                    map.DeleteActor(skill);
                }
                if (!map.Actors.ContainsKey(skill.ActorID))
                    Deactivate();

                MobAI ai = new MobAI(skill, true);
                short[] pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 200);
                List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(skill.X, map.Width), SagaLib.Global.PosY16to8(skill.Y, map.Height),
                SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height));
                pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                map.MoveActor(Map.MOVE_TYPE.START, skill, pos, 0, 3000);
            }
            catch(Exception ex)
            {
                Deactivate();
                Logger.ShowError(ex);
                map.DeleteActor(skill);
            }
        }
    }
}
