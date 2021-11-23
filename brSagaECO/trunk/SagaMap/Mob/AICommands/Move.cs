using System;
using System.Collections.Generic;
using System.Text;

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
        public void Update(object para)
        {
            try
            {                
                MapNode node;
                if (this.status == CommandStatus.FINISHED)
                    return;
                if (mob.CannotAttack > DateTime.Now && mob.Mode.isAnAI)
                    return;
                if (path.Count == 0 || !mob.CanMove)
                {
                    this.status = CommandStatus.FINISHED;
                    return;
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
