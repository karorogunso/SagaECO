//Comment this out to deactivate the dead lock check!
#define DeadLockCheck

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaMap;
using SagaMap.Network.Client;


namespace SagaMap.Manager
{
    public sealed class MapClientManager : ClientManager
    {
        List<MapClient> clients;
        public Thread check;
        MapClientManager()
        {
           
            this.clients = new List<MapClient>();
            this.commandTable = new Dictionary<ushort, Packet>();


            /*潜在强化*/
            this.commandTable.Add(0x1F59, new Packets.Client.CSMG_ITEM_MASTERENHANCE_CLOSE());
            this.commandTable.Add(0x1F57, new Packets.Client.CSMG_ITEM_MASTERENHANCE_CONFIRM());
            this.commandTable.Add(0x1F55, new Packets.Client.CSMG_ITEM_MASTERENHANCE_SELECT());
            /*潜在强化*/

            this.commandTable.Add(0x2010, new Packets.Client.CSMG_FFGARDEN_JOIN());
            this.commandTable.Add(0x2059, new Packets.Client.CSMG_FF_FURNITURE_SETUP());
            this.commandTable.Add(0x2012, new Packets.Client.CSMG_FFGARDEN_JOIN_OTHER());
            this.commandTable.Add(0x205D, new Packets.Client.CSMG_FF_FURNITURE_REMOVE());
            this.commandTable.Add(0x203C, new Packets.Client.CSMG_FF_FURNITURE_REMOVE_CASTLE());
            this.commandTable.Add(0x200D, new Packets.Client.CSMG_FF_FURNITURE_ROOM_APPEAR());
            this.commandTable.Add(0x2046, new Packets.Client.CSMG_FF_FURNITURE_ROOM_ENTER());
            this.commandTable.Add(0x203A, new Packets.Client.CSMG_FF_CASTLE_SETUP());
            this.commandTable.Add(0x20DE, new Packets.Client.CSMG_FF_UNIT_SETUP());
            //钓鱼
            this.commandTable.Add(0x216C, new Packets.Client.CSMG_FF_FISHBAIT_EQUIP());//装备鱼饵

            this.commandTable.Add(0x000A, new Packets.Client.CSMG_SEND_VERSION());
            this.commandTable.Add(0x0010, new Packets.Client.CSMG_LOGIN());
            this.commandTable.Add(0x001E, new Packet());//dummy packet
            this.commandTable.Add(0x001F, new Packets.Client.CSMG_LOGOUT());
            this.commandTable.Add(0x001C, new Packets.Client.CSMG_SSO_LOGOUT());
            this.commandTable.Add(0x0032, new Packets.Client.CSMG_PING());
            this.commandTable.Add(0x00B8, new Packet());//dummy packet ClientCheck
            this.commandTable.Add(0x01FD, new Packets.Client.CSMG_CHAR_SLOT());
            this.commandTable.Add(0x0208, new Packets.Client.CSMG_PLAYER_STATS_UP());
            this.commandTable.Add(0x0222, new Packets.Client.CSMG_PLAYER_ELEMENTS());
            this.commandTable.Add(0x0227, new Packets.Client.CSMG_SKILL_LEARN());
            this.commandTable.Add(0x022B, new Packets.Client.CSMG_SKILL_LEVEL_UP());
            this.commandTable.Add(0x020C, new Packets.Client.CSMG_ACTOR_REQUEST_PC_INFO());
            this.commandTable.Add(0x0258, new Packets.Client.CSMG_PLAYER_STATS_PRE_CALC());
            this.commandTable.Add(0x02BE, new Packets.Client.CSMG_NPC_JOB_SWITCH());
            this.commandTable.Add(0x03E8, new Packets.Client.CSMG_CHAT_PUBLIC());
            this.commandTable.Add(0x0406, new Packets.Client.CSMG_CHAT_PARTY());
            this.commandTable.Add(0x0410, new Packets.Client.CSMG_CHAT_RING());
            this.commandTable.Add(0x041A, new Packets.Client.CSMG_CHAT_SIGN());
            this.commandTable.Add(0x05E2, new Packets.Client.CSMG_NPC_EVENT_START());
            this.commandTable.Add(0x05F1, new Packets.Client.CSMG_NPC_INPUTBOX());
            this.commandTable.Add(0x05F3, new Packets.Client.CSMG_NPC_SELECT());
            this.commandTable.Add(0x05FE, new Packets.Client.CSMG_NPC_SHOP_BUY());
            
            this.commandTable.Add(0x0600, new Packets.Client.CSMG_NPC_SHOP_SELL());
            this.commandTable.Add(0x0601, new Packets.Client.CSMG_NPC_SHOP_CLOSE());
            this.commandTable.Add(0x0637, new Packets.Client.CSMG_DEM_CHIP_CLOSE());
            this.commandTable.Add(0x0638, new Packets.Client.CSMG_DEM_CHIP_CATEGORY());
            this.commandTable.Add(0x063C, new Packets.Client.CSMG_DEM_CHIP_BUY());
            this.commandTable.Add(0x0641, new Packets.Client.CSMG_VSHOP_CLOSE());
            this.commandTable.Add(0x064A, new Packets.Client.CSMG_VSHOP_CATEGORY_REQUEST());
            this.commandTable.Add(0x0654, new Packets.Client.CSMG_VSHOP_BUY());
            this.commandTable.Add(0x07D0, new Packets.Client.CSMG_ITEM_DROP());
            this.commandTable.Add(0x07E4, new Packets.Client.CSMG_ITEM_GET());
            this.commandTable.Add(0x09C4, new Packets.Client.CSMG_ITEM_USE());
            this.commandTable.Add(0x09E2, new Packets.Client.CSMG_ITEM_MOVE());
            this.commandTable.Add(0x09E7, new Packets.Client.CSMG_ITEM_EQUIPT());
            this.commandTable.Add(0x09F7, new Packets.Client.CSMG_ITEM_WARE_CLOSE());
            this.commandTable.Add(0x09FB, new Packets.Client.CSMG_ITEM_WARE_GET());
            this.commandTable.Add(0x09FD, new Packets.Client.CSMG_ITEM_WARE_PUT());
            this.commandTable.Add(0x0A0A, new Packets.Client.CSMG_TRADE_REQUEST());
            this.commandTable.Add(0x0A14, new Packets.Client.CSMG_TRADE_CONFIRM());
            this.commandTable.Add(0x0A15, new Packets.Client.CSMG_TRADE_PERFORM());
            this.commandTable.Add(0x0A16, new Packets.Client.CSMG_TRADE_CANCEL());
            this.commandTable.Add(0x0A1B, new Packets.Client.CSMG_TRADE_ITEM());
            this.commandTable.Add(0x0A0D, new Packets.Client.CSMG_TRADE_REQUEST_ANSWER());
            this.commandTable.Add(0x0F96, new Packet());//Dummy packet, dunno what this packet does, if someone knows, tell me    -by liiir1985
            this.commandTable.Add(0x0F9F, new Packets.Client.CSMG_SKILL_ATTACK());
            this.commandTable.Add(0x0FA3, new Packets.Client.CSMG_PLAYER_RETURN_HOME());
            this.commandTable.Add(0x0FA5, new Packets.Client.CSMG_SKILL_CHANGE_BATTLE_STATUS());
            this.commandTable.Add(0x0FD2, new Packet());//Dummy packet, player dead, dunno why should client tell server,that player is dead
            this.commandTable.Add(0x11F8, new Packets.Client.CSMG_PLAYER_MOVE());
            this.commandTable.Add(0x11FE, new Packets.Client.CSMG_PLAYER_MAP_LOADED());
            this.commandTable.Add(0x121D, new Packets.Client.CSMG_CHAT_WAITTYPE());
            this.commandTable.Add(0x1D0B, new Packets.Client.CSMG_CHAT_EXPRESSION());
            this.commandTable.Add(0x1216, new Packets.Client.CSMG_CHAT_EMOTION());
            this.commandTable.Add(0x121B, new Packets.Client.CSMG_CHAT_MOTION());
            this.commandTable.Add(0x12CB, new Packets.Client.CSMG_NPC_PET_SELECT());
            this.commandTable.Add(0x1387, new Packets.Client.CSMG_SKILL_CAST());
            this.commandTable.Add(0x13B6, new Packets.Client.CSMG_NPC_SYNTHESE());
            this.commandTable.Add(0x13B9, new Packets.Client.CSMG_NPC_SYNTHESE_FINISH());
            this.commandTable.Add(0x13BA, new Packets.Client.CSMG_CHAT_SIT());
            this.commandTable.Add(0x13C0, new Packets.Client.CSMG_ITEM_EQUIPT_REPAIR());
            this.commandTable.Add(0x13C5, new Packets.Client.CSMG_ITEM_ENHANCE_SELECT());
            this.commandTable.Add(0x13C7, new Packets.Client.CSMG_ITEM_ENHANCE_CONFIRM());
            this.commandTable.Add(0x13C9, new Packets.Client.CSMG_ITEM_ENHANCE_CLOSE());
            this.commandTable.Add(0x13D9, new Packets.Client.CSMG_ITEM_FUSION());
            this.commandTable.Add(0x13DD, new Packets.Client.CSMG_ITEM_FUSION_CANCEL());
            this.commandTable.Add(0x13E3, new Packets.Client.CSMG_IRIS_ADD_SLOT_ITEM_SELECT());
            this.commandTable.Add(0x13E5, new Packets.Client.CSMG_IRIS_ADD_SLOT_CONFIRM());
            this.commandTable.Add(0x13E7, new Packets.Client.CSMG_IRIS_ADD_SLOT_CANCEL());
            this.commandTable.Add(0x140B, new Packets.Client.CSMG_IRIS_CARD_ASSEMBLE());
            this.commandTable.Add(0x140D, new Packets.Client.CSMG_IRIS_CARD_ASSEMBLE_CANCEL());
            this.commandTable.Add(0x177A, new Packets.Client.CSMG_POSSESSION_REQUEST());
            this.commandTable.Add(0x177F, new Packets.Client.CSMG_POSSESSION_CANCEL());
            this.commandTable.Add(0x17E8, new Packets.Client.CSMG_GOLEM_SHOP_SELL());
            this.commandTable.Add(0x17EA, new Packets.Client.CSMG_GOLEM_SHOP_SELL_CLOSE());
            this.commandTable.Add(0x17EB, new Packets.Client.CSMG_GOLEM_SHOP_SELL_SETUP());
            this.commandTable.Add(0x17F2, new Packets.Client.CSMG_GOLEM_WAREHOUSE());
            this.commandTable.Add(0x17F4, new Packets.Client.CSMG_GOLEM_WAREHOUSE_SET());
            this.commandTable.Add(0x17F8, new Packets.Client.CSMG_GOLEM_WAREHOUSE_GET());
            this.commandTable.Add(0x17FC, new Packets.Client.CSMG_GOLEM_SHOP_OPEN());
            this.commandTable.Add(0x17FF, new Packet());
            this.commandTable.Add(0x1803, new Packets.Client.CSMG_GOLEM_SHOP_SELL_BUY());
            this.commandTable.Add(0x181A, new Packets.Client.CSMG_GOLEM_SHOP_BUY());
            this.commandTable.Add(0x181C, new Packets.Client.CSMG_GOLEM_SHOP_BUY_CLOSE());
            this.commandTable.Add(0x181D, new Packets.Client.CSMG_GOLEM_SHOP_BUY_SETUP());
            this.commandTable.Add(0x1825, new Packet());
            this.commandTable.Add(0x1827, new Packets.Client.CSMG_GOLEM_SHOP_BUY_SELL());
            this.commandTable.Add(0x1991, new Packets.Client.CSMG_QUEST_DETAIL_REQUEST());
            this.commandTable.Add(0x1965, new Packets.Client.CSMG_QUEST_SELECT());

            this.commandTable.Add(0x191A, new Packets.Client.CSMG_PLAYER_SHOP_SELL_BUY());//商人开店
            this.commandTable.Add(0x1915, new Packet());
            this.commandTable.Add(0x190A, new Packets.Client.CSMG_PLAYER_SETSHOP_OPEN());//商人开店
            this.commandTable.Add(0x190C, new Packets.Client.CSMG_PLAYER_SETSHOP_CLOSE());//商人开店
            this.commandTable.Add(0x190D, new Packets.Client.CSMG_PLAYER_SETSHOP_SETUP());//商人开店
            this.commandTable.Add(0x1900, new Packets.Client.CSMG_PLAYER_SHOP_OPEN());//商人开店

            this.commandTable.Add(0x19C9, new Packets.Client.CSMG_PARTY_INVITE());
            this.commandTable.Add(0x19CB, new Packets.Client.CSMG_PARTY_INVITE_ANSWER());
            this.commandTable.Add(0x19CD, new Packets.Client.CSMG_PARTY_QUIT());
            this.commandTable.Add(0x19D2, new Packets.Client.CSMG_PARTY_KICK());
            this.commandTable.Add(0x19D7, new Packets.Client.CSMG_PARTY_NAME());
            this.commandTable.Add(0x1A5E, new Packets.Client.CSMG_PLAYER_CHECK_EQUIPMENT_PERMISSIONS());
            this.commandTable.Add(0x1AAE, new Packets.Client.CSMG_RING_INVITE());
            this.commandTable.Add(0x1AB7, new Packets.Client.CSMG_RING_INVITE_ANSWER(false));
            this.commandTable.Add(0x1AB8, new Packets.Client.CSMG_RING_INVITE_ANSWER(true));
            this.commandTable.Add(0x1ABD, new Packets.Client.CSMG_RING_QUIT());
            this.commandTable.Add(0x1AC2, new Packets.Client.CSMG_RING_KICK());
            this.commandTable.Add(0x1AD6, new Packets.Client.CSMG_RING_RIGHT_SET());
            this.commandTable.Add(0x1ADB, new Packets.Client.CSMG_RING_EMBLEM_UPLOAD());
            this.commandTable.Add(0x1AF5, new Packets.Client.CSMG_COMMUNITY_BBS_CLOSE());
            this.commandTable.Add(0x1AFE, new Packets.Client.CSMG_COMMUNITY_BBS_POST());
            this.commandTable.Add(0x1B08, new Packets.Client.CSMG_COMMUNITY_BBS_REQUEST_PAGE());
            this.commandTable.Add(0x1B8A, new Packets.Client.CSMG_COMMUNITY_RECRUIT_CREATE());
            this.commandTable.Add(0x1B94, new Packets.Client.CSMG_COMMUNITY_RECRUIT_DELETE());
            this.commandTable.Add(0x1B9E, new Packets.Client.CSMG_COMMUNITY_RECRUIT());
            this.commandTable.Add(0x1BA8, new Packets.Client.CSMG_COMMUNITY_RECRUIT_JOIN());
            this.commandTable.Add(0x1BAE, new Packets.Client.CSMG_COMMUNITY_RECRUIT_REQUEST_ANS());
            this.commandTable.Add(0x1BF8, new Packets.Client.CSMG_FGARDEN_EQUIPT());
            this.commandTable.Add(0x1C02, new Packets.Client.CSMG_FGARDEN_FURNITURE_SETUP());
            this.commandTable.Add(0x1C07, new Packets.Client.CSMG_FGARDEN_FURNITURE_USE());
            this.commandTable.Add(0x1C0C, new Packets.Client.CSMG_FGARDEN_FURNITURE_REMOVE());
            this.commandTable.Add(0x1C11, new Packets.Client.CSMG_FGARDEN_FURNITURE_RECONFIG());
            this.commandTable.Add(0x1D4C, new Packets.Client.CSMG_PLAYER_GREETINGS());
            this.commandTable.Add(0x1DB0, new Packets.Client.CSMG_IRIS_CARD_OPEN());
            this.commandTable.Add(0x1DB2, new Packets.Client.CSMG_IRIS_CARD_CLOSE());
            this.commandTable.Add(0x1DB6, new Packets.Client.CSMG_IRIS_CARD_INSERT());
            this.commandTable.Add(0x1DBB, new Packets.Client.CSMG_IRIS_CARD_REMOVE());
            this.commandTable.Add(0x1DC9, new Packets.Client.CSMG_IRIS_CARD_LOCK());
            this.commandTable.Add(0x1E47, new Packets.Client.CSMG_DEM_DEMIC_CLOSE());
            this.commandTable.Add(0x1E4C, new Packets.Client.CSMG_DEM_DEMIC_INITIALIZE());
            this.commandTable.Add(0x1E4E, new Packets.Client.CSMG_DEM_DEMIC_CONFIRM());
            this.commandTable.Add(0x1E50, new Packets.Client.CSMG_DEM_STATS_PRE_CALC());
            this.commandTable.Add(0x1E5B, new Packets.Client.CSMG_DEM_COST_LIMIT_CLOSE());
            this.commandTable.Add(0x1E5C, new Packets.Client.CSMG_DEM_COST_LIMIT_BUY());
            this.commandTable.Add(0x1E7D, new Packets.Client.CSMG_DEM_FORM_CHANGE());
            this.commandTable.Add(0x1E83, new Packets.Client.CSMG_DEM_PARTS_CLOSE());
            this.commandTable.Add(0x1E87, new Packets.Client.CSMG_DEM_PARTS_EQUIP());
            this.commandTable.Add(0x1E88, new Packets.Client.CSMG_DEM_PARTS_UNEQUIP());
            this.commandTable.Add(0x1EBE, new Packets.Client.CSMG_ITEM_CHANGE());//110武器人形
            this.commandTable.Add(0x1EC0, new Packets.Client.CSMG_ITEM_CHANGE_CANCEL());//110武器人形
            this.commandTable.Add(0x1EDD, new Packets.Client.CSMG_CHAR_FORM());//3转相位外观变换
            this.commandTable.Add(0x1EDE, new Packet());
            this.commandTable.Add(0x1FE0, new Packet()); //申请师徒 1F E0 00 00 00 00
            this.commandTable.Add(0x0064, new Packets.Client.CSMG_0064());//未知封包
            this.commandTable.Add(0x0037, new Packet()); //未知封包 00 37 00
            this.commandTable.Add(0x0262, new Packets.Client.CSMG_PLAYER_EQUIP_OPEN()); //申请查看装备 02 62 00 00 00 00


            //申请换脸
            this.commandTable.Add(0x1CF2, new Packets.Client.CSMG_ITEM_FACEVIEW());
            this.commandTable.Add(0x1CF4, new Packets.Client.CSMG_ITEM_FACECHANGE());
            this.commandTable.Add(0x1CF6, new Packets.Client.CSMG_ITEM_FACEVIEW_CLOSE());
            //Partner system
            //this.commandTable.Add(0x217C, new Packets.Client.CSMG_PARTNER_PERK_PREVIEW());
            //this.commandTable.Add(0x217E, new Packets.Client.CSMG_PARTNER_PERK_CONFIRM());
            //this.commandTable.Add(0x2180, new Packets.Client.CSMG_PARTNER_AI_MODE_SELECTION());
            //this.commandTable.Add(0x2182, new Packets.Client.CSMG_PARTNER_AI_DETAIL_OPEN());
            //this.commandTable.Add(0x2184, new Packets.Client.CSMG_PARTNER_AI_DETAIL_SETUP());
            //this.commandTable.Add(0x2186, new Packets.Client.CSMG_PARTNER_AI_DETAIL_CLOSE());
            //this.commandTable.Add(0x218A, new Packets.Client.CSMG_PARTNER_SETEQUIP());
            //this.commandTable.Add(0x2199, new Packets.Client.CSMG_PARTNER_SETFOOD());
            //this.commandTable.Add(0x219B, new Packets.Client.CSMG_PARTNER_AUTOFEED());
            //this.commandTable.Add(0x219D, new Packets.Client.CSMG_PARTNER_UPDATE_REQUEST());

            this.waitressQueue = new AutoResetEvent(true);            

            //deadlock check
            check = new Thread(new ThreadStart(this.checkCriticalArea));
            check.Name = string.Format("DeadLock checker({0})", check.ManagedThreadId);
#if DeadLockCheck
            check.Start();
#endif
        }

        public void Abort()
        {
            check.Abort();
            this.packetCoordinator.Abort();
        }

        public static MapClientManager Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly MapClientManager instance = new MapClientManager();
        }

        public void Announce(string txt)
        {
            try
            {
                foreach (MapClient i in OnlinePlayer)
                {
                    try
                    {
                        i.SendAnnounce(txt);
                    }
                    catch { }
                }
            }
            catch { }
        }


        /// <summary>
        /// Connects new clients
        /// </summary>
        public override void NetworkLoop(int maxNewConnections)
        {
            for (int i = 0; listener.Pending() && i < maxNewConnections; i++)
            {
                Socket sock = listener.AcceptSocket();
                string ip = sock.RemoteEndPoint.ToString().Substring(0, sock.RemoteEndPoint.ToString().IndexOf(':'));
                Logger.ShowInfo(string.Format(LocalManager.Instance.Strings.NEW_CLIENT, sock.RemoteEndPoint.ToString()), null);
                MapClient client = new MapClient(sock, this.commandTable);
                clients.Add(client);
            }
        }

        public List<MapClient> Clients
        {
            get
            {
                return this.clients;
            }
        }

        public override void OnClientDisconnect(Client client_t)
        {
            clients.Remove((MapClient)client_t);
        }

        public MapClient FindClient(ActorPC pc)
        {
            return FindClient(pc.CharID);
        }

        public override Client GetClient(uint actorID)
        {
            var chr = from c in OnlinePlayer
                      where c.Character.ActorID == actorID
                      select c;
            if (chr.Count() != 0)
                return chr.First();
            else
                return null;
        }
        public override Client GetClientForName(string actorName)
        {
            var chr = from c in OnlinePlayer
                      where c.Character.Name == actorName
                      select c;
            if (chr.Count() != 0)
                return chr.First();
            else
                return null;
        }
        public List<MapClient> OnlinePlayer
        {
            get
            {
                List<MapClient> list = new List<MapClient>();
                foreach (MapClient i in clients)
                {
                    if (i.netIO.Disconnected)
                        continue;
                    if (i.Character == null)
                        continue;
                    if (!i.Character.Online)
                        continue;
                    list.Add(i);
                }
                return list;
            }
        }

        public MapClient FindClient(uint charID)
        {
            var chr = from c in OnlinePlayer
                  where c.Character.CharID == charID
                  select c;
            if (chr.Count() != 0)
                return chr.First();
            else
                return null;
        }
    }
}
