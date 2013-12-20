using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaLib;
using SagaMap;
using SagaMap.Manager;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public void SendActorID()
        {
            Packets.Server.SSMG_ACTOR_SPEED p2 = new SagaMap.Packets.Server.SSMG_ACTOR_SPEED();
            p2.ActorID = this.Character.ActorID;
            p2.Speed = 10;
            this.netIO.SendPacket(p2);
        }

        public void SendActorMode()
        {
            Packets.Server.SSMG_ACTOR_MODE p3 = new SagaMap.Packets.Server.SSMG_ACTOR_MODE();
            p3.ActorID = this.Character.ActorID;
            p3.Mode1 = 2;
            this.netIO.SendPacket(p3);
        }

        public void SendCharOption()
        {
            Packets.Server.SSMG_ACTOR_OPTION p4 = new SagaMap.Packets.Server.SSMG_ACTOR_OPTION();
            p4.Option = SagaMap.Packets.Server.SSMG_ACTOR_OPTION.Options.NONE;
            this.netIO.SendPacket(p4);
        }        

        public void SendCharInfo()
        {
            Packets.Server.SSMG_SYSTEM_MESSAGE p6 = new SagaMap.Packets.Server.SSMG_SYSTEM_MESSAGE();
            p6.Message = SagaMap.Packets.Server.SSMG_SYSTEM_MESSAGE.Messages.GAME_SMSG_RECV_POSTUREBLOW_TEXT;
            this.netIO.SendPacket(p6);

            Packets.Server.SSMG_PLAYER_INFO p1 = new SagaMap.Packets.Server.SSMG_PLAYER_INFO();
            p1.Player = this.Character;
            this.netIO.SendPacket(p1);

            Packets.Server.SSMG_PLAYER_STATUS_EXTEND pp = new SagaMap.Packets.Server.SSMG_PLAYER_STATUS_EXTEND();
            pp.Speed = 420;
            this.netIO.SendPacket(pp);
        }

        public void OnMove(Packets.Client.CSMG_PLAYER_MOVE p)
        {
            switch (p.MoveType)
            {
                case MoveType.RUN:
                    this.Map.MoveActor(Map.MOVE_TYPE.START, this.Character, new short[2] { p.X, p.Y }, p.Dir, this.Character.Speed);
                    break;
            }
        }

        public void OnRequestPCInfo(Packets.Client.CSMG_ACTOR_REQUEST_PC_INFO p)
        {
            Packets.Server.SSMG_ACTOR_PC_INFO p1 = new SagaMap.Packets.Server.SSMG_ACTOR_PC_INFO();
            ActorPC pc = (ActorPC)this.map.GetActor(p.ActorID);
            p1.Actor = pc;
            Logger.ShowInfo(this.netIO.DumpData(p1));
            this.netIO.SendPacket(p1);
        }




        internal void SendItemAdd(Item item, ContainerType containerType, InventoryAddResult result, int p)
        {
            throw new NotImplementedException();
        }

        internal void OnItemEquipt(Packets.Client.CSMG_ITEM_EQUIPT cSMG_ITEM_EQUIPT)
        {
            throw new NotImplementedException();
        }

        internal void OnItemGet(Packets.Client.CSMG_ITEM_GET cSMG_ITEM_GET)
        {
            throw new NotImplementedException();
        }

        internal void OnItemDrop(Packets.Client.CSMG_ITEM_DROP cSMG_ITEM_DROP)
        {
            throw new NotImplementedException();
        }

        internal void SendItems()
        {
            throw new NotImplementedException();
        }
    }
}
