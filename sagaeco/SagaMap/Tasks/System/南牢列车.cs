using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.ODWar;

using SagaMap.Manager;
using SagaMap.Skill;
using SagaMap.Network.Client;
using SagaMap.Mob;

namespace SagaMap.Tasks.System
{
    public class 南牢列车 : MultiRunTask
    {
        public 南牢列车()
        {
            this.period = 2000;
            this.dueTime = 0;
        }

        static 南牢列车 instance;

        public static 南牢列车 Instance
        {
            get
            {
                if (instance == null)
                    instance = new 南牢列车();
                return instance;
            }
        }
        uint ss = 0;
        public override void CallBack()
        {
            Period = Global.Random.Next(1500, 2000);
            DateTime now = DateTime.Now;
            Map map = MapManager.Instance.GetMap(20020000);
            Map map2 = MapManager.Instance.GetMap(20021000);

            ss++;
            if (ss % 5 == 0)
            {
                create(map, 150, 12, 150, 251);
                create(map, 105, 251, 105, 12);
            }

            create(map2, 213, 16, 213, 23);
            create(map2, 213, 58, 213, 79);
        }

        void create(Map map, byte x1,byte y1,byte x2,byte y2)
        {
            SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(8477, 1);
            ActorSkill actor = new ActorSkill(skill, null);
            actor.Name = "列车";
            actor.MapID = map.ID;
            actor.X = Global.PosX8to16(x1, map.Width);
            actor.Y = Global.PosY8to16(y1, map.Height);
            actor.Speed = 1300;
            actor.e = new ActorEventHandlers.NullEventHandler();
            map.RegisterActor(actor);
            //Logger.ShowError(actor.MapID.ToString()+" " +actor.ActorID.ToString());
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            actor.Stackable = false;
            Activator timer = new Activator(actor,map,x2,y2);
            timer.Activate();
        }
        private class Activator : MultiRunTask
        {
            Map map;
            ActorSkill skill;
            byte x, y;
            int count = 0;
            int maxcount = 1000;
            public Activator(ActorSkill skill , Map map, byte x2, byte y2)
            {
                
                period = 45;
                this.skill = skill;
                this.map = map;
                x = x2;
                y = y2;
            }
            public override void CallBack()
            {
                try
                {
                    if (count % 2 == 0)
                    {
                        List<Actor> actors = map.GetRoundAreaActors(skill.X, skill.Y, 300);
                        foreach (Actor j in actors)
                        {
                            if (j != null)
                            {
                                if (j.type == ActorType.PC && j.HP > 0)
                                {
                                    int damage = 5000;

                                    SkillHandler.Instance.CauseDamage(j, j, damage);
                                    SkillHandler.Instance.ShowVessel(j, damage);
                                    //SkillHandler.Instance.PushBack(skill, j, 1);
                                    if (j.HP <= 0 && j.Buff.Dead)
                                        MapClient.FromActorPC((ActorPC)j).TitleProccess((ActorPC)j, 66, 1);
                                }
                            }
                        }

                        MobAI ai = new MobAI(skill, true);
                        List<MapNode> path = ai.FindPath(Global.PosX16to8(skill.X, map.Width), Global.PosY16to8(skill.Y, map.Height),
                           x, y);
                        int deltaX = path[0].x;
                        int deltaY = path[0].y;
                        MapNode node = new MapNode();
                        node.x = (byte)deltaX;
                        node.y = (byte)deltaY;
                        path.Add(node);
                        short[] pos = new short[2];
                        pos[0] = Global.PosX8to16(path[0].x, map.Width);
                        pos[1] = Global.PosY8to16(path[0].y, map.Height);
                        map.MoveActor(Map.MOVE_TYPE.START, skill, pos, 0, 200);
                    }
                    count++;
                    if((Global.PosX16to8(skill.X, map.Width) == x && Global.PosY16to8(skill.Y, map.Height) == y) || count >= maxcount)
                    {
                        map.DeleteActor(skill);
                        Deactivate();
                    }
                }
                catch (Exception ex)
                {
                    map.DeleteActor(skill);
                    Deactivate();
                    Logger.ShowError(ex);
                }
            }
        }
    }
}
