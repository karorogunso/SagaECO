using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Manager;
using SagaMap.ActorEventHandlers;
using SagaDB.Mob;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30006 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public static List<Actor> affected;
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 1300, false);
            affected = new List<Actor>();
            foreach (var i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    if (i.Status.Additions.ContainsKey("炎鬼缠身"))
                    {
                        affected.Add(i);
                    }
                }
            }
            count2 = 0;
            if (affected.Count != 0)
            {
                炎鬼支配 sc = new 炎鬼支配(args.skill, sActor, sActor, 10000, 0, args);
                SkillHandler.ApplyAddition(sActor, sc);
            }
        }
        public static int count2 = 0;
        class 炎鬼支配 : DefaultBuff
        {
            public 炎鬼支配(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage, SkillArg arg)
                : base(skill, sActor, dActor, "炎鬼支配", lifetime, 100, damage, arg)
            {

                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate2 += this.TimerUpdate;

            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {

            }

            void TimerUpdate(Actor sActor, Actor dActor, DefaultBuff skill, SkillArg arg, int damage)
            {
                try
                {
                    int count = affected.Count;
                    if (count > 20) count = 20;
                    if (count2 < count)
                    {
                        Actor actor = affected[count2];
                        Map map = SagaMap.Manager.MapManager.Instance.GetMap(actor.MapID);
                        actor.Status.Additions.Remove("炎鬼缠身");
                        if (!actor.Status.Additions.ContainsKey("炎鬼烙印"))
                        {
                            炎鬼烙印 s = new 炎鬼烙印(arg.skill, sactor, dActor, 90000, 0);
                            SkillHandler.ApplyAddition(actor, s);
                        }
                        ActorMob 炎鬼 = SpawnMob(26040001, actor.MapID, SagaLib.Global.PosX16to8(actor.X, map.Width), SagaLib.Global.PosY16to8(actor.Y, map.Height), 1, 1, 0, 炎鬼Info(), 炎鬼AI());
                        炎鬼自滅 w = new 炎鬼自滅(炎鬼, 5);
                        w.Activate();
                        count2++;
                    }
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
            }
        }
        public class 炎鬼自滅 : MultiRunTask
        {
            Actor actor;
            public 炎鬼自滅(Actor actor,int second)
            {
                this.dueTime = second * 1000;
                this.actor = actor;
            }
            public override void CallBack()
            {
                Map map = SagaMap.Manager.MapManager.Instance.GetMap(actor.MapID);
                map.SendEffect(actor, 4400);
                actor.HP = 0;
                actor.Buff.死んだふり = true;
                actor.e.OnDie();
                this.Deactivate();
            }
        }
        public static ActorMob SpawnMob(uint MobID, uint MapID, byte x, byte y, int range, int count, int delay, ActorMob.MobInfo mobinfo, SagaMap.Mob.AIMode Ai)
        {
            Map map = MapManager.Instance.GetMap(MapID);

                ActorMob mob = new ActorMob(MobID, mobinfo);
                mob.MapID = MapID;
                int min_x, max_x, min_y, max_y;
                min_x = x - range;
                max_x = x + range;
                min_y = y - range;
                max_y = y + range;
                if (min_x < 0) min_x = 0;
                if (max_x >= map.Width)
                    max_x = map.Width - 1;
                if (min_y < 0) min_y = 0;
                if (max_y >= map.Height)
                    max_y = map.Height - 1;
                int x_new, y_new;
                x_new = (byte)SagaLib.Global.Random.Next(min_x, max_x);
                y_new = (byte)SagaLib.Global.Random.Next(min_y, max_y);

                int counter = 0;
                try
                {
                    while (map.Info.walkable[x_new, y_new] != 2)
                    {
                        if (counter > 1000 || range == 0) break;
                        x_new = (byte)SagaLib.Global.Random.Next(min_x, max_x);
                        y_new = (byte)SagaLib.Global.Random.Next(min_y, max_y);
                        counter++;
                    }
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
                mob.X = SagaLib.Global.PosX8to16((byte)x_new, map.Width);
                mob.Y = SagaLib.Global.PosY8to16((byte)y_new, map.Height);
                mob.Dir = (ushort)SagaLib.Global.Random.Next(0, 7);
                MobEventHandler eh = new MobEventHandler(mob);
                mob.e = eh;
                if (Ai != null)
                    eh.AI.Mode = Ai;
                else eh.AI.Mode = new SagaMap.Mob.AIMode(0);
                eh.AI.X_Ori = SagaLib.Global.PosX8to16(x, map.Width);
                eh.AI.Y_Ori = SagaLib.Global.PosY8to16(y, map.Height);
                eh.AI.X_Spawn = mob.X;
                eh.AI.Y_Spawn = mob.Y;
                eh.AI.MoveRange = 10000;
                eh.AI.SpawnDelay = delay * 1000;
                map.RegisterActor(mob);
                mob.invisble = false;
                mob.sightRange = 2500;
                map.OnActorVisibilityChange(mob);;
            
            return mob;
        }


        static ActorMob.MobInfo 炎鬼Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 10000;
            info.name = "炎鬼BOSS";
            info.speed = 600;
            info.atk_min = 100;
            info.atk_max = 200;
            info.matk_min = 100;
            info.matk_max = 200;
            info.def = 50;
            info.mdef = 50;
            info.def_add = 50;
            info.mdef_add = 50;
            info.hit_critical = 80;
            info.hit_magic = 80;
            info.hit_melee = 80;
            info.hit_ranged = 80;
            info.avoid_critical = 0;
            info.avoid_magic = 0;
            info.avoid_melee = 0;
            info.avoid_ranged = 0;
            info.Aspd = 400;
            info.Cspd = 100;
            info.AttackType = SagaDB.Actor.ATTACK_TYPE.BLOW;
            info.elements[SagaLib.Elements.Fire] = 50;
            info.elements[SagaLib.Elements.Earth] = 0;
            info.elements[SagaLib.Elements.Dark] = 0;
            info.elements[SagaLib.Elements.Holy] = 0;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Water] = 0;
            info.elements[SagaLib.Elements.Wind] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 0;
            info.baseExp = 10000;
            info.jobExp = 10000;
            MobData.DropData newDrop = new MobData.DropData();
            newDrop.ItemID = 10009000;
            newDrop.Rate = 10000;
            info.dropItems.Add(newDrop);
            return info;
        }
        static AIMode 炎鬼AI()
        {
            AIMode ai = new AIMode(1);
            ai.MobID = 14280000;
            ai.isNewAI = true;
            ai.Distance = 3;
            ai.ShortCD = 3;
            ai.LongCD = 3;
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 3;
            skillinfo.Rate = 60;
            skillinfo.MaxHP = 100;
            skillinfo.MinHP = 0;
            ai.SkillOfShort.Add(2115, skillinfo);
            skillinfo.CD = 3;
            skillinfo.Rate = 40;
            skillinfo.MaxHP = 100;
            skillinfo.MinHP = 0;
            ai.SkillOfShort.Add(2116, skillinfo);
            skillinfo.CD = 3;
            skillinfo.Rate = 30;
            skillinfo.MaxHP = 70;
            skillinfo.MinHP = 0;
            ai.SkillOfLong.Add(3001, skillinfo);
            skillinfo.CD = 6;
            skillinfo.Rate = 30;
            skillinfo.MaxHP = 100;
            skillinfo.MinHP = 0;
            ai.SkillOfLong.Add(3006, skillinfo);
            return ai;
        }
    }
}
