using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap;
using SagaMap.Scripting;

namespace SagaMap.Mob.AICommands
{    
    public class Chase : AICommand
    {
        private CommandStatus status;
        private Actor dest;
        private MobAI mob;
        
        List<MapNode> path;
        int index = 0;

        public short x = 0, y = 0;
        
        public Chase(MobAI mob, Actor dest)
        {
            this.mob = mob;
            this.dest = dest;
            x = dest.X;
            y = dest.Y;
            if (mob.Mob.MapID != dest.MapID || !mob.CanMove)
            {
                this.Status = CommandStatus.FINISHED;
                return;
            }
            if (mob.Mode.RunAway)
            {
                if (MobAI.GetLengthD(mob.Mob.X, mob.Mob.Y, dest.X, dest.Y) < 2000)
                {
                    if (Global.Random.Next(0, 99) < 20)
                    {
                        int range = Global.Random.Next(100, 1000);
                        x = (short)(mob.Mob.X - dest.X);
                        y = (short)(mob.Mob.Y - dest.Y);
                        x = (short)(mob.Mob.X + x / MobAI.GetLengthD(0, 0, x, y) * range);
                        y = (short)(mob.Mob.Y + y / MobAI.GetLengthD(0, 0, x, y) * range);
                    }
                    else
                    {
                        this.status = CommandStatus.FINISHED;
                        return;
                    }
                }
                else
                {
                    this.Status = CommandStatus.FINISHED;
                    return;
                }
            }
            path = mob.FindPath(Global.PosX16to8(mob.Mob.X, mob.map.Width), Global.PosY16to8(mob.Mob.Y, mob.map.Height), Global.PosX16to8(x, mob.map.Width), Global.PosY16to8(y, mob.map.Height));
            this.Status = CommandStatus.INIT;
        }
        public string GetName() { return "Chase"; }
        public void Update(object para)
        {
            try
            {
                MapNode node;
                if (mob.Mode.NoMove || !mob.CanMove)
                    return;
                if (this.dest.Status == null)
                {
                    this.status = CommandStatus.FINISHED;
                    return;
                }
                //if (mob.Mode.isAnAI && mob.CannotMove)
                //    return;
                if (this.dest.Status.Additions.ContainsKey("Hiding"))
                {
                    this.Status = CommandStatus.FINISHED;
                }
                if (this.dest.Status.Additions.ContainsKey("Cloaking"))
                {
                    this.Status = CommandStatus.FINISHED;
                }
                if (this.dest.Status.Additions.ContainsKey("Invisible"))
                {
                    this.Status = CommandStatus.FINISHED;
                }
                if (this.dest.Status.Additions.ContainsKey("Through"))
                {
                    this.Status = CommandStatus.FINISHED;
                }
                if (this.Status == CommandStatus.FINISHED)
                    return;
                float size;
                if (mob.Mode.isAnAI)
                {
                    size = mob.needlen;
                }
                else if (mob.Mob.type != ActorType.PC)
                {
                    size = ((ActorMob)mob.Mob).BaseData.range;
                }
                else
                    size = 1;
                bool ifNeko = false;
                if (mob.Mob.type == ActorType.PET)
                {
                    if (((ActorPet)mob.Mob).BaseData.mobType == SagaDB.Mob.MobType.MAGIC_CREATURE)
                    {
                        ifNeko = true;
                    }
                }
            
                if (MobAI.GetLengthD(mob.Mob.X, mob.Mob.Y, dest.X, dest.Y) <= (size * 150) && !ifNeko)
                {
                    if (!this.mob.Mode.RunAway || Global.Random.Next(0, 99) < 70)
                    {
                        this.mob.map.FindFreeCoord(this.mob.Mob.X, this.mob.Mob.Y, out x, out y, this.mob.Mob);
                        if (this.mob.Mob.X == x && this.mob.Mob.Y == y || this.mob.Mode.RunAway)
                        {
                            this.Status = CommandStatus.FINISHED;
                            return;
                        }
                        else
                        {
                            short[] dst = new short[2] { x, y };
                            mob.map.MoveActor(Map.MOVE_TYPE.START, mob.Mob, dst, MobAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), (ushort)(mob.Mob.Speed / 20), true);
                            return;
                        }
                    }
                }
                if (index + 1 < path.Count)
                {
                    node = path[index];
                    short[] dst = new short[2] { Global.PosX8to16(node.x, mob.map.Width), Global.PosY8to16(node.y, mob.map.Height) };
                    mob.map.MoveActor(Map.MOVE_TYPE.START, mob.Mob, dst, MobAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), (ushort)(mob.Mob.Speed / 20), true);
                    if (mob.Mode.isAnAI)
                        mob.CannotAttack = DateTime.Now.AddMilliseconds(1500);
                }
                else
                {
                    if (path.Count == 0)
                    {
                        this.Status = CommandStatus.FINISHED;
                        return;
                    }
                    node = path[path.Count - 1];
                    short[] dst = new short[2] { Global.PosX8to16(node.x, mob.map.Width), Global.PosY8to16(node.y, mob.map.Height) };
                    if (mob.map.GetActorsArea(dst[0], dst[1], 50).Count > 0 && !ifNeko)
                    {
                        this.mob.map.FindFreeCoord(dest.X, dest.Y, out x, out y, this.mob.Mob);
                        path = mob.FindPath(Global.PosX16to8(mob.Mob.X, mob.map.Width), Global.PosY16to8(mob.Mob.Y, mob.map.Height), Global.PosX16to8(x, mob.map.Width), Global.PosY16to8(y, mob.map.Height));
                        index = 0;
                        return;
                    }
                    mob.map.MoveActor(Map.MOVE_TYPE.START, mob.Mob, dst, MobAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), (ushort)(mob.Mob.Speed / 20), true);
                    if ((MobAI.GetLengthD(mob.Mob.X, mob.Mob.Y, dest.X, dest.Y) > (50 + size * 100) && !mob.Mode.RunAway))
                    {
                        if (mob.Mob.MapID != dest.MapID)
                        {
                            this.Status = CommandStatus.FINISHED;
                            return;
                        }
                        path = mob.FindPath(Global.PosX16to8(mob.Mob.X, mob.map.Width), Global.PosY16to8(mob.Mob.Y, mob.map.Height), Global.PosX16to8(dest.X, mob.map.Width), Global.PosY16to8(dest.Y, mob.map.Height));
                        index = -1;
                    }
                    else
                    {
                        this.Status = CommandStatus.FINISHED;
                        return;
                    }
                }
                index++;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, null);
                this.Status = CommandStatus.FINISHED;
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

        public List<MapNode> GetPath() { return path; }

        public void Dispose()
        {
            this.status = CommandStatus.FINISHED;
        }

    }
}
