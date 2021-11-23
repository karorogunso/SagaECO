using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SagaLib;
using SagaMap;
using SagaDB;
using SagaDB.Actor;
using SagaMap.Network.Client;
using SagaMap.Scripting;

namespace SagaAPI
{



    class Process
    {
        string action;
        private MySQLActorDB actordb;
        uint charid, itemid;
        ushort qty;

        public Process(string action)
        {
            this.action = action;
        }
        public void Action( uint charid, uint itemid, ushort qty)
        {
            this.charid = charid;
            this.qty = qty;
            this.itemid = itemid;

        }
        public bool Load()
        {

           Logger.ShowInfo("ACTION:" + action + " ITEM:" + itemid + " QTY:" + qty);



            return true;



        }
    }
}
