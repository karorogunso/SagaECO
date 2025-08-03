using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap;
using SagaMap.Scripting;

namespace SagaMap.Partner.AICommands
{    
    public class Chase : AICommand
    {
        private CommandStatus status;
        private Actor dest;
        private PartnerAI partnerai;
        
        List<MapNode> path;
        int index = 0;

        public short x = 0, y = 0;

        public Chase(PartnerAI partnerai, Actor dest)
        {
            this.partnerai = partnerai;
            this.dest = dest;
            x = dest.X;
            y = dest.Y;
            if (partnerai.Partner.MapID != dest.MapID || !partnerai.CanMove)
            {
                this.Status = CommandStatus.FINISHED;
                return;
            }
            if (partnerai.Mode.RunAway)
            {
                if (PartnerAI.GetLengthD(partnerai.Partner.X, partnerai.Partner.Y, dest.X, dest.Y) < 2000)
                {
                    if (Global.Random.Next(0, 99) < 20)
                    {
                        int range = Global.Random.Next(100, 1000);
                        x = (short)(partnerai.Partner.X - dest.X);
                        y = (short)(partnerai.Partner.Y - dest.Y);
                        x = (short)(partnerai.Partner.X + x / PartnerAI.GetLengthD(0, 0, x, y) * range);
                        y = (short)(partnerai.Partner.Y + y / PartnerAI.GetLengthD(0, 0, x, y) * range);
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
            path = partnerai.FindPath(Global.PosX16to8(partnerai.Partner.X, partnerai.map.Width), Global.PosY16to8(partnerai.Partner.Y, partnerai.map.Height), Global.PosX16to8(x, partnerai.map.Width), Global.PosY16to8(y, partnerai.map.Height));
            this.Status = CommandStatus.INIT;
        }
        public string GetName() { return "Chase"; }
        public void Update(object para)
        {
            try
            {
                ActorPartner partner = null;
                if (this.partnerai.Partner.type == ActorType.PARTNER)
                    partner = (ActorPartner)this.partnerai.Partner;
                MapNode node;
                if (partnerai.Mode.NoMove || !partnerai.CanMove)
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
                if (this.dest.Status.Additions.ContainsKey("Through"))
                {
                    this.Status = CommandStatus.FINISHED;
                }
                if (this.Status == CommandStatus.FINISHED)
                    return;
                Actor chasedest = dest;
                if (partner.ai_mode == 1 && partner.Owner!= null)
                    chasedest = partner.Owner;
                float size;
                if (partnerai.Mode.isAnAI)
                {
                    size = partnerai.needlen;
                }
                else
                {
                    size = ((ActorPartner)partnerai.Partner).BaseData.range;
                }
                bool ifNeko = false;
                if (partnerai.Partner.type == ActorType.PET)
                {
                    if (((ActorPartner)partnerai.Partner).BaseData.partnertype == SagaDB.Partner.PartnerType.MAGIC_CREATURE)
                    {
                        ifNeko = true;
                    }
                }
                if (PartnerAI.GetLengthD(partnerai.Partner.X, partnerai.Partner.Y, chasedest.X, chasedest.Y) <= (size * 150) && !ifNeko)
                {
                    if (!this.partnerai.Mode.RunAway || Global.Random.Next(0, 99) < 70)
                    {
                        this.partnerai.map.FindFreeCoord(this.partnerai.Partner.X, this.partnerai.Partner.Y, out x, out y, this.partnerai.Partner);
                        if (this.partnerai.Partner.X == x && this.partnerai.Partner.Y == y || this.partnerai.Mode.RunAway)
                        {
                            this.Status = CommandStatus.FINISHED;
                            return;
                        }
                        else
                        {
                            short[] dst = new short[2] { x, y };
                            partnerai.map.MoveActor(Map.MOVE_TYPE.START, partnerai.Partner, dst, PartnerAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), (ushort)(partnerai.Partner.Speed / 20), true);
                            return;
                        }
                    }
                }
                if (index + 1 < path.Count && !partnerai.Partner.Status.Additions.ContainsKey("SkillCast"))
                {
                    node = path[index];
                    short[] dst = new short[2] { Global.PosX8to16(node.x, partnerai.map.Width), Global.PosY8to16(node.y, partnerai.map.Height) };
                    partnerai.map.MoveActor(Map.MOVE_TYPE.START, partnerai.Partner, dst, PartnerAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), (ushort)(partnerai.Partner.Speed / 20), true);
                    if (partnerai.Mode.isAnAI)
                        partnerai.CannotAttack = DateTime.Now.AddMilliseconds(1500);
                }
                else
                {
                    if (path.Count == 0)
                    {
                        this.Status = CommandStatus.FINISHED;
                        return;
                    }
                    node = path[path.Count - 1];
                    short[] dst = new short[2] { Global.PosX8to16(node.x, partnerai.map.Width), Global.PosY8to16(node.y, partnerai.map.Height) };
                    if (partnerai.map.GetActorsArea(dst[0], dst[1], 50).Count > 0 && !ifNeko)
                    {
                        this.partnerai.map.FindFreeCoord(chasedest.X, chasedest.Y, out x, out y, this.partnerai.Partner);
                        path = partnerai.FindPath(Global.PosX16to8(partnerai.Partner.X, partnerai.map.Width), Global.PosY16to8(partnerai.Partner.Y, partnerai.map.Height), Global.PosX16to8(x, partnerai.map.Width), Global.PosY16to8(y, partnerai.map.Height));
                        index = 0;
                        return;
                    }
                    partnerai.map.MoveActor(Map.MOVE_TYPE.START, partnerai.Partner, dst, PartnerAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), (ushort)(partnerai.Partner.Speed / 20), true);
                    if ((PartnerAI.GetLengthD(partnerai.Partner.X, partnerai.Partner.Y, chasedest.X, chasedest.Y) > (50 + size * 100) && !partnerai.Mode.RunAway))
                    {
                        if (partnerai.Partner.MapID != chasedest.MapID)
                        {
                            this.Status = CommandStatus.FINISHED;
                            return;
                        }
                        path = partnerai.FindPath(Global.PosX16to8(partnerai.Partner.X, partnerai.map.Width), Global.PosY16to8(partnerai.Partner.Y, partnerai.map.Height), Global.PosX16to8(chasedest.X, partnerai.map.Width), Global.PosY16to8(chasedest.Y, partnerai.map.Height));
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
            path = partnerai.FindPath(Global.PosX16to8(partnerai.Partner.X, partnerai.map.Width), Global.PosY16to8(partnerai.Partner.Y, partnerai.map.Height), Global.PosX16to8(x, partnerai.map.Width), Global.PosY16to8(y, partnerai.map.Height));
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
