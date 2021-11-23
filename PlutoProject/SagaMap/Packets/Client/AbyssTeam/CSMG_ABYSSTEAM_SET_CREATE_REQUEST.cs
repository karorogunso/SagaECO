using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ABYSSTEAM_SET_CREATE_REQUEST : Packet
    {
        public CSMG_ABYSSTEAM_SET_CREATE_REQUEST()
        {
            this.offset = 2;
        }

        public override Packet New()
        {
            return new CSMG_ABYSSTEAM_SET_CREATE_REQUEST();
        }
        //奈落隊伍建立 TSTR(隊伍名稱) TSTR(訊息) TSTR(密碼) 00 BYTE(0重新1存點) BYTE(最低LV) BYTE(最高LV) AWORD(04職業許可 2進制 全允許 0F 0F 0F 03)
        public string TeamName
        {
            get
            {
                this.offset = 2;
                byte len = this.GetByte();
                return Encoding.UTF8.GetString(this.GetBytes(len)).Replace("/0","");
            }
        }
        public string Comment
        {
            get
            {
                byte len = this.GetByte();
                return Encoding.UTF8.GetString(this.GetBytes(len)).Replace("/0", "");
            }
        }
        public string Password
        {
            get
            {
                byte len = this.GetByte();
                return Encoding.UTF8.GetString(this.GetBytes(len)).Replace("/0", "");
            }
        }
        public bool IsFromSave
        {
            get
            {
                offset += 1;
                if (this.GetByte() == 1)
                    return true;
                return false;
            }
        }
        public byte MinLV
        {
            get
            {
                return this.GetByte();
            }
        }
        public byte MaxLV
        {
            get
            {
                return this.GetByte();
            }
        }
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnAbyssTeamSetCreateRequest(this);
        }

    }
}