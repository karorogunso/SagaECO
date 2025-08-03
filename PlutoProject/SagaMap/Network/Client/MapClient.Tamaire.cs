using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Npc;
using SagaDB.Quests;
using SagaDB.Party;
using SagaLib;
using SagaMap;
using SagaMap.Manager;
using SagaDB.Tamaire;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {

        public void OnTamaireRentalRequest(Packets.Client.CSMG_TAMAIRE_RENTAL_REQUEST p)
        {
            ActorPC lender = MapServer.charDB.GetChar(p.Lender);
            TamaireRentalManager.Instance.ProcessRental(this.Character,lender);
            SendTamaire();
            PC.StatusFactory.Instance.CalcStatus(this.Character);
            SendPlayerInfo();
        }

        public void SendTamaire()
        {
            if (this.Character.TamaireRental == null)
                return;
            if (this.Character.TamaireRental.CurrentLender == 0)
                return;
            Packets.Server.SSMG_TAMAIRE_RENTAL p = new Packets.Server.SSMG_TAMAIRE_RENTAL();
            ActorPC lender = MapServer.charDB.GetChar(this.Character.TamaireRental.CurrentLender);
            p.JobType = lender.TamaireLending.JobType;
            p.RentalDue = this.Character.TamaireRental.RentDue - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            p.Factor = (short)((1f-TamaireRentalManager.Instance.CalcFactor(lender.TamaireLending.Baselv - this.Character.Level))*1000);
            this.netIO.SendPacket(p);
        }

        public void OnTamaireRentalTerminateRequest(Packets.Client.CSMG_TAMAIRE_RENTAL_TERMINATE_REQUEST p)
        {
            OnTamaireRentalTerminate(1);
        }

        public void OnTamaireRentalTerminate(byte reason)
        {
            Packets.Server.SSMG_TAMAIRE_RENTAL_TERMINATE p = new Packets.Server.SSMG_TAMAIRE_RENTAL_TERMINATE();
            p.Reason = reason;
            this.netIO.SendPacket(p);
        }

        public void OpenTamaireListUI()
        {
            Packets.Server.SSMG_TAMAIRE_LIST_UI p = new Packets.Server.SSMG_TAMAIRE_LIST_UI();
            this.netIO.SendPacket(p);
        }
    }
}