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
using SagaLogin;
using SagaLogin.Manager;

namespace SagaLogin.Network.Client
{
    public partial class LoginClient : SagaLib.Client
    {
        public void OnGetGiftsRequest(Packets.Client.CSMG_TOOL_GIFTS p)
        {
            if (account.GMLevel < 250) return;
            byte Type = p.type;
            string Title = p.Title;
            string Sender = p.Sender;
            string Content = p.Content;
            string AcccountIDs = p.CharIDs;
            string GiftIDs = p.GiftIDs;
            string Days = p.Days;

            try
            {
                Dictionary<uint, ushort> Gifts = new Dictionary<uint, ushort>();
                GiftIDs = GiftIDs.Replace("\r\n", "@");
                string[] paras = GiftIDs.Split('@');
                for (int i = 0; i < paras.Length; i++)
                {
                    string[] pa = paras[i].Split(',');
                    Gifts.Add(uint.Parse(pa[0]), ushort.Parse(pa[1]));
                }

                Dictionary<string, uint> Recipients = new Dictionary<string, uint>();
                if (Type == 1)//发给制定账号
                {
                    AcccountIDs = AcccountIDs.Replace(",", "@");
                    paras = AcccountIDs.Split('@');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        uint ID = uint.Parse(paras[i]);
                        Recipients.Add(ID.ToString(), ID);
                    }
                }
                else if(Type == 5)
                {
                    AcccountIDs = AcccountIDs.Replace(",", "@");
                    paras = AcccountIDs.Split('@');
                    for (int i = 0; i < paras.Length; i++)
                    {
                        List<Account> Accounts = LoginServer.accountDB.GetAllAccount();
                        Account a = new Account();
                        foreach (var item in Accounts)
                        {
                            if (item.Name == paras[i])
                            {
                                a = item;
                                break;
                            }
                        }
                       if (a != null)
                       {
                            uint ID = (uint)a.AccountID;
                            Recipients.Add(ID.ToString(), ID);
                       }
                    }
                }

                else if (Type == 2 || Type == 12)//发送给在线账号
                {
                    List<LoginClient> aids = LoginClientManager.Instance.FindAllOnlineAccounts();
                    Dictionary<string, DateTime> sd = new Dictionary<string, DateTime>();
                    foreach (var item in aids)
                    {
                        uint ID = (uint)item.account.AccountID;
                        if(Type == 2)
                            Recipients.Add(ID.ToString(), ID);
                        else if (Type == 12)
                        {
                            if (Recipients.ContainsKey(item.account.LastIP))
                            {
                                if (sd[item.account.LastIP] < item.account.lastLoginTime)
                                {
                                    Recipients[item.account.LastIP] = ID;
                                    sd[item.account.LastIP] = item.account.lastLoginTime;
                                }
                            }
                            else
                            {
                                Recipients.Add(item.account.LastIP, ID);
                                sd.Add(item.account.LastIP, item.account.lastLoginTime);
                            }
                        }
                    }
                }
                else if (Type == 3 || Type == 13)//发送给所有账号
                {
                    List<Account> Accounts = LoginServer.accountDB.GetAllAccount();
                    Dictionary<string, DateTime> sd = new Dictionary<string, DateTime>();
                    foreach (var item in Accounts)
                    {
                        uint ID = (uint)item.AccountID;
                        if (Type == 3)
                            Recipients.Add(ID.ToString(), ID);
                        else if (Type == 13)
                        {
                            if (Recipients.ContainsKey(item.LastIP))
                            {
                                if (sd[item.LastIP] < item.lastLoginTime)
                                {
                                    Recipients[item.LastIP] = ID;
                                    sd[item.LastIP] = item.lastLoginTime;
                                }
                            }
                            else
                            {
                                Recipients.Add(item.LastIP, ID);
                                sd.Add(item.LastIP, item.lastLoginTime);
                            }
                        }
                    }
                }
                else if (Type == 4 || Type == 14)//发送给N天内登录过的账号
                {
                    int day = int.Parse(Days);
                    List<Account> Accounts = LoginServer.accountDB.GetAllAccount();
                    Dictionary<string, DateTime> sd = new Dictionary<string, DateTime>();
                    foreach (var item in Accounts)
                    {
                        if ((DateTime.Now - item.lastLoginTime).Days <= day)
                        {
                            uint ID = (uint)item.AccountID;
                            if (Type == 4)
                                Recipients.Add(ID.ToString(), ID);
                            else if (Type == 14)
                            {
                                if (Recipients.ContainsKey(item.LastIP))
                                {
                                    if (sd[item.LastIP] < item.lastLoginTime)
                                    {
                                        Recipients[item.LastIP] = ID;
                                        sd[item.LastIP] = item.lastLoginTime;
                                    }
                                }
                                else
                                {
                                    Recipients.Add(item.LastIP, ID);
                                    sd.Add(item.LastIP, item.lastLoginTime);
                                }
                            }
                        }
                    }
                }
                foreach (var item in Recipients.Values)
                {
                    SagaDB.BBS.Gift gift = new SagaDB.BBS.Gift();
                    gift.AccountID = item;
                    gift.Date = DateTime.Now;
                    gift.Items = Gifts;
                    gift.Name = Title;
                    gift.Title = Content;
                    AddGift(gift);
                }
                SendResult(0, "礼物发送成功！");


                Logger log = new Logger("礼物记录.txt");
                string logtext = "\r\n-操作者账号：" + account.AccountID;
                logtext += "\r\n-类型：" + Type;
                logtext += "\r\n-标题：" + Title;
                logtext += "\r\n-内容：" + Content;
                logtext += "\r\n-接收者人数：" + Recipients.Count;
                if (Type == 4)
                    logtext += "\r\n-设定的天数：" + Days;
                logtext += "\r\n-物品：";
                foreach (var item in Gifts.Keys)
                    logtext += "\r\n   -ID:" + item + "   -Stack:" + Gifts[item];
                if(Type == 1)
                {
                    logtext += "\r\n-接收者ID:\r\n   ";
                    foreach (var item in Recipients.Values)
                        logtext += item + ",";
                }
                logtext += "\r\n=======================================================\r\n";
                log.WriteLog(logtext);
            }
            catch(Exception ex)
            {
                SendResult(1, "礼物处理失败！" + ex.Message);
                Logger.ShowError(ex);
            }
        }
        public void SendResult(byte type,string text)
        {
            Packets.Server.SSMG_TOOL_RESULT p = new Packets.Server.SSMG_TOOL_RESULT();
            p.type = type;
            p.Text = text;
            this.netIO.SendPacket(p);
        }
        public void AddGift(SagaDB.BBS.Gift gift)
        {
            uint ID = LoginServer.charDB.AddNewGift(gift);
            LoginClient client = LoginClientManager.Instance.FindClientAccountID(gift.AccountID);
            gift.MailID = ID;
            if(client != null)
                client.SendSingleGift(gift);
        }
        public void SendSingMail(SagaDB.BBS.Mail mail)
        {
            if (selectedChar == null) return;
            Packets.Server.SSMG_MAIL p = new Packets.Server.SSMG_MAIL();
            p.mail = mail;
            netIO.SendPacket(p);
        }

        public void SendMails()
        {
            if (selectedChar == null) return;
            if (selectedChar.Mails != null && selectedChar.Mails.Count >= 1)
            {
                for (int i = 0; i < selectedChar.Mails.Count; i++)
                {
                    Packets.Server.SSMG_MAIL p = new Packets.Server.SSMG_MAIL();
                    p.mail = selectedChar.Mails[i];
                    netIO.SendPacket(p);
                }
            }
        }

        public void SendSingleGift(SagaDB.BBS.Gift gift)
        {
            if (selectedChar == null) return;
            Packets.Server.SSMG_GIFT p = new Packets.Server.SSMG_GIFT();
            p.mails = gift;
            netIO.SendPacket(p);

        }

        public void SendGifts()
        {
            if (selectedChar == null) return;
            LoginServer.charDB.GetGifts(selectedChar);
            for (int i = 0; i < selectedChar.Gifts.Count; i++)
            {
                Packets.Server.SSMG_GIFT p = new Packets.Server.SSMG_GIFT();
                p.mails = selectedChar.Gifts[i];
                netIO.SendPacket(p);
            }
        }
    }
}
