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
using SagaDB.PProtect;
using SagaMap.Packets.Server;


namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public void OnPProtectCreatedIniti(Packets.Client.CSMG_PPROTECT_CREATED_INITI p)
        {
            SSMG_PPROTECT_CREATED_INITI p1 = new SSMG_PPROTECT_CREATED_INITI();
            this.netIO.SendPacket(p1);
        }

        /// <summary>
        /// 打开列表
        /// </summary>
        public void OnPProtectListOpen(Packets.Client.CSMG_PPROTECT_LIST_OPEN p)
        {
            ushort max = 0;
            List<PProtect> pp = PProtectManager.Instance.GetPProtectsOfPage(p.Page, out max, p.Search);
            //pp.Add(new PProtect { ID = 0xffff, Leader = this.chara, Name = "123", MaxMember = 5, Message = "233", TaskID = 12000 });
            //pp.Add(new PProtect { ID = 0xffee, Leader = this.chara, Name = "987", MaxMember = 1, Message = "000", TaskID = 12000 });
            SSMG_PPROTECT_LIST p1 = new SSMG_PPROTECT_LIST();
            
            p1.PageMax = max;
            if (p.Page < max)
                p1.Page = p.Page;
            else
                p1.Page = max;
            p1.List = pp;
            string ss = p1.DumpData();
            this.netIO.SendPacket(p1);
        }

        PProtect pp;
        /// <summary>
        /// 创建招募
        /// </summary>
        public void OnPProtectCreated(Packets.Client.CSMG_PPROTECT_CREATED_INFO p)
        {
            pp = new PProtect();
            pp.Leader = this.Character;
            pp.Members.Add(pp.Leader);
            pp.Name = p.name;
            pp.Password = p.password;
            pp.Message = p.message;
            pp.MaxMember = p.maxMember;
            pp.TaskID = p.taskID;

            PProtectManager.Instance.ADD(pp);

            SSMG_PPROTECT_CREATED_RESULT p1 = new SSMG_PPROTECT_CREATED_RESULT();

            this.netIO.SendPacket(p1);

            SSMG_PPROTECT_CHAT_INFO p2 = new SSMG_PPROTECT_CHAT_INFO();
            p2.SetData(this.Character, 0, 0, 0, 0, 0, 0);
            this.netIO.SendPacket(p2);
        }


        /// <summary>
        /// 修改招募信息
        /// </summary>
        public void OnPProtectCreatedRevise(Packets.Client.CSMG_PPROTECT_CREATED_REVISE p)
        {
            if (pp == null)
                return;
            pp.Name = p.name;
            pp.Password = p.password;
            pp.Message = p.message;
            pp.MaxMember = p.maxMember;
            pp.TaskID = p.taskID;

            
            SSMG_PPROTECT_CREATED_REVISE_RESULT p1 = new SSMG_PPROTECT_CREATED_REVISE_RESULT();
            p1.SetData(p.name, p.message, p.taskID, p.maxMember, 0, 0);
            //string ss = p1.DumpData();
            this.netIO.SendPacket(p1);

            for (int i = 0; i < pp.Members.Count; i++)
            {
                var client = MapClientManager.Instance.FindClient(pp.Members[i]);
                if (client != null && client.Character != this.Character)
                {
                    p1 = new SSMG_PPROTECT_CREATED_REVISE_RESULT();
                    p1.SetData(p.name, p.message, p.taskID, p.maxMember, 0, 0);
                    client.netIO.SendPacket(p1);
                }
            }


        }

        /// <summary>
        /// 加入
        /// </summary>
        public void OnPProtectADD(Packets.Client.CSMG_PPROTECT_ADD p)
        {
            addPProtect(p.PPID, p.Password);
        }

        public void OnPProtectADD1(Packets.Client.CSMG_PPROTECT_ADD_1 p)
        {
            PProtect ppt = PProtectManager.Instance.GetPProtect(p.PPID);
            if (ppt != null)
            {
                SSMG_PPROTECT_CREATED_ADD_RESULT_1 p1 = new SSMG_PPROTECT_CREATED_ADD_RESULT_1();
                p1.List = ppt.Members;
                this.netIO.SendPacket(p1);
            }
        }

        void addPProtect(uint ppid, string password)
        {
            if (pp != null)
            {
                OnPProtectCreatedOut(null);
            }

            PProtect ppt = PProtectManager.Instance.GetPProtect(ppid);
            if (ppt != null)
            {
                if (!ppt.IsPassword || ppt.Password == password)
                {

                    SSMG_PPROTECT_CHAT_INFO p2;
                    for (int i = 0; i < ppt.Members.Count; i++)
                    {
                        var client = MapClientManager.Instance.FindClient(ppt.Members[i]);
                        if (client != null)
                        {
                            p2 = new SSMG_PPROTECT_CHAT_INFO();
                            p2.SetData(this.Character, (byte)ppt.Members.Count, 0, 0, 0, 0, 0);//
                            client.netIO.SendPacket(p2);

                            SSMG_PPROTECT_CHAT_INFO p3 = new SSMG_PPROTECT_CHAT_INFO();
                            p3.SetData(ppt.Members[i], (byte)i, 0, 0, 0, 0, 0);
                            this.netIO.SendPacket(p3);
                        }
                    }
                    p2 = new SSMG_PPROTECT_CHAT_INFO();
                    p2.SetData(this.Character, (byte)ppt.Members.Count, 0, 0, 0, 0, 0);//

                    this.netIO.SendPacket(p2);
                    ppt.Members.Add(this.Character);
                    SSMG_PPROTECT_CREATED_ADD_RESULT p1 = new SSMG_PPROTECT_CREATED_ADD_RESULT();
                    p1.SetData(ppt.Name, password, 0, 1, 0);
                    this.netIO.SendPacket(p1);
                    pp = ppt;
                }
                else
                {
                    SSMG_PPROTECT_CREATED_ADD_RESULT p1 = new SSMG_PPROTECT_CREATED_ADD_RESULT();
                    p1.SetData("", "", 0xFB, 0, 0xFF);
                    this.netIO.SendPacket(p1);
                    //密码错误
                }
            }
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        public void OnPProtectReady(Packets.Client.CSMG_PPROTECT_READY p)
        {
            if (pp != null && pp.Leader == this.Character)
            {
                //队长进入房间操作
                return;
            }
            SSMG_PPROTECT_READY_RESULT p1 = new SSMG_PPROTECT_READY_RESULT();
            SSMG_PPROTECT_READY p2;
            switch (p.State)
            {
                case 1://准备
                    if(true)
                    {
                        //条件符合
                        p1.Code = 1;

                        for (int i = 0; i < pp.Members.Count; i++)
                        {
                            var client = MapClientManager.Instance.FindClient(pp.Members[i]);
                            if (client != null && client.Character != this.Character)
                            {
                                p2 = new SSMG_PPROTECT_READY();
                                p2.Index = (byte)pp.Members.IndexOf(this.Character);
                                p2.Code = 1;
                                client.netIO.SendPacket(p2);
                            }
                        }

                    }
                    else
                    {
                        //条件不符
                        p1.Code = 0xFE;
                    }
                    break;
                case 0://取消
                    {
                        for (int i = 0; i < pp.Members.Count; i++)
                        {
                            var client = MapClientManager.Instance.FindClient(pp.Members[i]);
                            if (client != null && client.Character != this.Character)
                            {
                                p2 = new SSMG_PPROTECT_READY();
                                p2.Index = (byte)pp.Members.IndexOf(this.Character);
                                p2.Code = 0;
                                client.netIO.SendPacket(p2);
                            }
                        }
                    }
                    p1.Code = 0;
                    break;
            }
            this.netIO.SendPacket(p1);
        }


        /// <summary>
        /// 退出招募
        /// </summary>
        public void OnPProtectCreatedOut(Packets.Client.CSMG_PPROTECT_CREATED_OUT p)
        {
            if (pp == null)
                return;
            if(this.Character == pp.Leader)
            {
                //招募人退出
                PProtectManager.Instance.Remove(pp.ID);

                for (int i = 0; i < pp.Members.Count; i++)
                {
                    var client = MapClientManager.Instance.FindClient(pp.Members[i]);
                    if (client != null)
                    {
                        SSMG_PPROTECT_CREATED_OUT_RESULT p1 = new SSMG_PPROTECT_CREATED_OUT_RESULT();
                        p1.SetName(pp.Name);
                        client.netIO.SendPacket(p1);
                    }
                }
                pp.Members.Clear();
            }
            else
            {
                //成员退出
                for (int i = 0; i < pp.Members.Count; i++)
                {
                    var client = MapClientManager.Instance.FindClient(pp.Members[i]);
                    if (client != null && client.Character != this.Character)
                    {
                        SSMG_PPROTECT_CREATED_OUT p1 = new SSMG_PPROTECT_CREATED_OUT();
                        //int iii = pp.Members.IndexOf(this.Character);
                        p1.Index = (byte)pp.Members.IndexOf(this.Character);
                        client.netIO.SendPacket(p1);
                    }
                }
                SSMG_PPROTECT_CREATED_OUT_RESULT_1 p2 = new SSMG_PPROTECT_CREATED_OUT_RESULT_1();
                p2.SetName(pp.Name);
                this.netIO.SendPacket(p2);

                pp.Members.Remove(this.Character);
            }
            pp = null;
        }
    }
}