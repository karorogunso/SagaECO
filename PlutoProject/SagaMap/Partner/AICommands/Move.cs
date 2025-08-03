using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Scripting;

namespace SagaMap.Partner.AICommands
{
    public class Move : AICommand
    {
        private CommandStatus status;
        private short x, y;
        private PartnerAI partnerai;

        List<MapNode> path;
        int index = 0;

        public Move(PartnerAI partnerai, short x, short y)
        {
            this.partnerai = partnerai;
            this.partnerai.map.FindFreeCoord(x, y, out x, out y);
            this.x = x;
            this.y = y;
            if (partnerai.Mode.NoMove)
            {
                this.status = CommandStatus.FINISHED;
            }
            else
            {
                path = partnerai.FindPath(Global.PosX16to8(partnerai.Partner.X, partnerai.map.Width), Global.PosY16to8(partnerai.Partner.Y, partnerai.map.Height), Global.PosX16to8(x, partnerai.map.Width), Global.PosY16to8(y, partnerai.map.Height));
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
                if (partnerai.CannotAttack > DateTime.Now && partnerai.Mode.isAnAI)
                    return;
                if (path.Count == 0 || !partnerai.CanMove)
                {
                    this.status = CommandStatus.FINISHED;
                    return;
                }
                if(partnerai.Partner.type == SagaDB.Actor.ActorType.PARTNER)
                {
                    if(((SagaDB.Actor.ActorPartner)partnerai.Partner).Owner != null)
                    {
                        if (((SagaDB.Actor.ActorPartner)partnerai.Partner).Owner.HP == 0)
                            return;
                    }
                }
                if (index + 1 < path.Count)
                {
                    node = path[index];
                    short[] dst = new short[2] { Global.PosX8to16(node.x, partnerai.map.Width), Global.PosY8to16(node.y, partnerai.map.Height) };
                    partnerai.map.MoveActor(Map.MOVE_TYPE.START, partnerai.Partner, dst, PartnerAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), 0, true);
                }
                else
                {
                    node = path[path.Count - 1];
                    short[] dst = new short[2] { Global.PosX8to16(node.x, partnerai.map.Width), Global.PosY8to16(node.y, partnerai.map.Height) };
                    partnerai.map.MoveActor(Map.MOVE_TYPE.START, partnerai.Partner, dst, PartnerAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), (ushort)(partnerai.Partner.Speed / 10), true);
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
            path = partnerai.FindPath(Global.PosX16to8(partnerai.Partner.X, partnerai.map.Width), Global.PosY16to8(partnerai.Partner.Y, partnerai.map.Height), Global.PosX16to8(x, partnerai.map.Width), Global.PosY16to8(y, partnerai.map.Height));
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
