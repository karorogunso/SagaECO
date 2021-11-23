using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaLib;
using SagaMap;
using SagaMap.Scripting;

namespace SagaMap.Mob.AICommands
{
    public class Move : AICommand
    {
        private CommandStatus status;
        private short x, y;
        private MobAI mob;

        List<MapNode> path;
        int index = 0;
        public DateTime BackTimer = DateTime.Now;
        public Move(MobAI mob, short x, short y)
        {
            this.mob = mob;
            this.mob.map.FindFreeCoord(x, y, out x, out y);
            this.x = x;
            this.y = y;
            if (mob.Mode.NoMove)
            {
                this.status = CommandStatus.FINISHED;
            }
            else
            {
                path = mob.FindPath(Global.PosX16to8(mob.Mob.X, mob.map.Width), Global.PosY16to8(mob.Mob.Y, mob.map.Height), Global.PosX16to8(x, mob.map.Width), Global.PosY16to8(y, mob.map.Height));
                this.status = CommandStatus.INIT;
            }
        }

        public string GetName() { return "Move"; }
        void returnAndInitialize()
        {
            if (mob.Mob.type == ActorType.GOLEM) return;
            short[] pos = new short[2] { mob.X_pb, mob.Y_pb };
            mob.map.MoveActor(Map.MOVE_TYPE.START, mob.Mob, pos, 1, 1000, false, MoveType.WARP2);
            mob.Mob.HP = mob.Mob.MaxHP;

            if (mob.Mob.type == ActorType.MOB)
            {
                if (((ActorMob)mob.Mob).AttackedForEvent != 0)
                    mob.Mob.e.OnActorReturning(mob.Mob);
                ((ActorMob)mob.Mob).AttackedForEvent = 0;
                mob.Mob.BattleStartTime = DateTime.Now;
            }


            if (mob.Mob.Slave.Count > 0)
            {
                Actor[] s = new Actor[mob.Mob.Slave.Count + 10];
                mob.Mob.Slave.CopyTo(s);
                for (int i = 0; i < mob.Mob.Slave.Count; i++)
                {
                    if (s[i] != null)
                    {
                        if (mob.Mob.HP > 0)
                            Skill.SkillHandler.Instance.ShowEffectByActor(s[i], 4310);
                        ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)s[i].e;
                        s[i].Buff.死んだふり = true;
                        eh.OnDie(false);
                        mob.map.DeleteActor(s[i]);
                    }
                }
            }

            //清空HP相关SKILL状态
            mob.SkillOfHPClear();
            mob.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, mob.Mob, false);
        }
        public void Update(object para)
        {
            try
            {
                MapNode node;
                if (mob.Mob==null || this.status == CommandStatus.FINISHED)
                    return;
                if (mob.Cannotmovebeforefight) return;
                if (mob.CannotAttack > DateTime.Now && mob.Mode.isAnAI)
                    return;
                if (mob.Mob.Status.Additions.ContainsKey("石像坐下休息"))
                    return;
                if (path.Count == 0 || !mob.CanMove)
                {
                    this.status = CommandStatus.FINISHED;
                    return;
                }
                if (DateTime.Now > this.mob.BackTimer.AddSeconds(4))
                    this.mob.BackTimer = DateTime.Now;
                if (DateTime.Now > this.mob.BackTimer.AddSeconds(2) && !mob.noreturn)//2秒后返回
                {
                    if (mob.Mob.HP < mob.Mob.MaxHP)
                        returnAndInitialize();
                }
                if(mob.Mob.type == ActorType.GOLEM)
                {
                    if(!mob.Mode.RunAway && mob.Mob.Status.Additions.ContainsKey("石像击杀怪物CD") && Global.Random.Next(0,100)< 20 && !mob.Mob.Status.Additions.ContainsKey("石像坐下休息") && !mob.Mob.Status.Additions.ContainsKey("石像坐下休息CD"))
                    {
                        int lefttime = Global.Random.Next(10000, 185000);
                        ((Skill.Additions.Global.OtherAddition)mob.Mob.Status.Additions["石像击杀怪物CD"]).endTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, lefttime);
                        Skill.Additions.Global.OtherAddition skills = new Skill.Additions.Global.OtherAddition(null, mob.Mob, "石像坐下休息", lefttime);
                        skills.dueTime = 2000;
                        skills.OnAdditionStart += (s, e) =>
                        {

                            ((ActorGolem)mob.Mob).Motion = 135;
                            ((ActorGolem)mob.Mob).MotionLoop = true;
                            ChatArg parg = new ChatArg();
                            parg.motion = MotionType.SIT;
                            parg.loop = 1;
                            mob.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, parg, mob.Mob, true);
                        };
                        skills.OnAdditionEnd += (s, e) =>
                        {
                            ((ActorGolem)mob.Mob).MotionLoop = false;
                            Skill.SkillHandler.RemoveAddition(mob.Mob, "石像击杀怪物CD");
                            Skill.Additions.Global.OtherAddition skills2 = new Skill.Additions.Global.OtherAddition(null, mob.Mob, "石像坐下休息CD", 300000);
                            Skill.SkillHandler.ApplyAddition(mob.Mob, skills2);
                        };
                        Skill.SkillHandler.ApplyAddition(mob.Mob, skills);
                        return;
                    }
                }
                if (index + 1 < path.Count)
                {
                    node = path[index];
                    short[] dst = new short[2] { Global.PosX8to16(node.x, mob.map.Width), Global.PosY8to16(node.y, mob.map.Height) };
                    mob.map.MoveActor(Map.MOVE_TYPE.START, mob.Mob, dst, MobAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), 0, true);
                }
                else
                {
                    node = path[path.Count - 1];
                    short[] dst = new short[2] { Global.PosX8to16(node.x, mob.map.Width), Global.PosY8to16(node.y, mob.map.Height) };
                    mob.map.MoveActor(Map.MOVE_TYPE.START, mob.Mob, dst, MobAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), (ushort)(mob.Mob.Speed / 10), true);
                    this.Status = CommandStatus.FINISHED;
                }
                index++;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, null);
            }

        }

        public void FindPath()
        {
            path = mob.FindPath(Global.PosX16to8(mob.Mob.X, mob.map.Width), Global.PosY16to8(mob.Mob.Y, mob.map.Height), Global.PosX16to8(x, mob.map.Width), Global.PosY16to8(y, mob.map.Height));
            index = 0;
        }

        public CommandStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        public void Dispose()
        {
            this.status = CommandStatus.FINISHED;
        }
    }
}
