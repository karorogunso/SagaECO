using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SagaLib;
using SagaDB;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Manager;
using SagaMap.Network.Client;
using System.Data;

namespace SagaMap
{

    class Process : MapServer
    {
        string action;
        uint charid, itemid;
        private ContainerType continer;
        ushort qty;

        //MySQLActorDB sql = new MySQLActorDB(Configuration.Instance.DBHost, Configuration.Instance.DBPort, Configuration.Instance.DBName, Configuration.Instance.DBUser, Configuration.Instance.DBPass);

        public Process()
        {
        }


        public void Action(uint charid, uint itemid, ushort qty)
        {
            this.charid = charid;
            this.qty = qty;
            this.itemid = itemid;

        }
        public void Query(uint charid)
        {
            this.charid = charid;
        }

        public void Announce(string msg)
        {
            try
            {
                foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                {
                    i.SendAnnounce(msg);
                }

            }
            catch (Exception) { }


        }

        public int CheckAPIItem(uint charid, MapClient client)
        {

            //System.Threading.Thread.Sleep(2000);

            string sqlstr = string.Format("SELECT * FROM `apiitem` WHERE `char_id`='" + charid + "' AND status = 0 ORDER BY `request_time` DESC;");
            //MapClient client = MC(charid);


            //MySQLActorDB sql = ConnectToDB();
            //MySQLActorDB sql = new MySQLActorDB(Configuration.Instance.DBHost, Configuration.Instance.DBPort, Configuration.Instance.DBName, Configuration.Instance.DBUser, Configuration.Instance.DBPass);
            DataRowCollection result = Logger.defaultSql.SQLExecuteQuery(sqlstr);

            int count = 0;

            foreach (DataRow i in result)
            {
                count++;

                //Item Instance
                Item item = ItemFactory.Instance.GetItem((uint)i["item_id"]);
                qty = (ushort)i["qty"];
                item.Stack = qty;


                //Execute Add Item
                client.AddItem(item, true);

                //Save Record
                Logger.defaultSql.SQLExecuteNonQuery("UPDATE apiitem SET process_time = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', status = 1 WHERE apiitem_id = " + i["apiitem_id"] + ";");

                //sql.SQLExecuteNonQuery(str);
            }
            return count;
        }
        public void SaveOfflineItem(uint charid, uint itemid, uint qty)
        {
            //MySQLActorDB sql = ConnectToDB();
            //MySQLActorDB sql = new MySQLActorDB(Configuration.Instance.DBHost, Configuration.Instance.DBPort, Configuration.Instance.DBName, Configuration.Instance.DBUser, Configuration.Instance.DBPass);
            Logger.defaultSql.SQLExecuteNonQuery("INSERT INTO apiitem VALUES (null," + charid + "," + itemid + "," + qty + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',null,0);");
            //sql.SQLExecuteNonQuery(str);

        }
        public void AddItem(MapClient i, uint itemid, ushort qty)
        {
            //Item Instance
            Item item = ItemFactory.Instance.GetItem(itemid);
            item.Stack = qty;


            //Execute Add Item
            i.AddItem(item, true);


            //Save Record
            //MySQLActorDB sql = ConnectToDB();
            //MySQLActorDB sql = new MySQLActorDB(Configuration.Instance.DBHost, Configuration.Instance.DBPort, Configuration.Instance.DBName, Configuration.Instance.DBUser, Configuration.Instance.DBPass);
            Logger.defaultSql.SQLExecuteNonQuery("INSERT INTO apiitem VALUES (null," + i.Character.CharID + "," + itemid + "," + qty + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',1);");
            //sql.SQLExecuteNonQuery(str);
        }
        public bool InvQuery()
        {

            //Client client = new Client();

            //Get Char Info
            ActorPC pc = charDB.GetChar(charid);

            List<Item> lists = pc.Inventory.GetContainer(ContainerType.BODY);


            //Check if Char is online
            MapClient i;
            var chr = from c in MapClientManager.Instance.OnlinePlayer
                      where c.Character.Name == pc.Name
                      select c;

            i = chr.First();
            AddItem(i, itemid, qty);
            Logger.ShowInfo("API Command execute successfully. (" + pc.Name + ")");




            return true;
        }
        public bool Load()
        {





            //Client client = new Client();

            //Get Char Info
            ActorPC pc = charDB.GetChar(charid);

            if (pc == null)
            {
                Logger.ShowError("NO SUCH CHARID" + charid);
                return false;
            }

            //Check if Char is online
            MapClient i;
            var chr = from c in MapClientManager.Instance.OnlinePlayer
                      where c.Character.Name == pc.Name
                      select c;
            if (chr.Count() == 0)
            {
                try
                {
                    SaveOfflineItem(charid, itemid, qty);
                }
                catch
                {
                    Logger.ShowError("ERROR ON SAVE OFFLINE APIITEM");
                }
                Logger.ShowInfo("Player: " + pc.Name + " is offline, Item will be process on next login");
                return true;
            }
            else
            {
                i = chr.First();
                AddItem(i, itemid, qty);
                Logger.ShowInfo("API Command execute successfully. (" + pc.Name + ")");
            }





            return true;



        }
    }
}
