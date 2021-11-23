using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Scripting
{
    public enum UIType
    {
        Status = 1,
        Item,
        EquipFull,
        Skill,
        FriendList,
        MailList,
        Quest = 7,
        Minimap,
        Macro,
        LookForParty,
        LookForTrade,
        LookForInformation,
        LookForChatRoom,
        LookForMaster,
        Stamp = 20,
        FGEquipt,
        WRPRanking = 23,
        MiniGameRanking = 24,
        ECoin,
        RingWhat = 27,
        FishingRanking,
        MonsterGuide,
        Furniture = 30,
        MinimapSmall,
        MinimapLarge,
        FGardenEquip = 34,
        Element,
        EquipSlot,
        EquipOnly
    }
}

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SHOW_UI : Packet
    {
        public SSMG_NPC_SHOW_UI()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x0606;

            this.PutUInt(1);
        }

        public UIType UIType
        {
            set
            {
                this.PutUInt((uint)value, 6);
                this.PutUInt(0xffffffff);
                this.PutUInt(0xffffffff);
                this.PutByte(0x0);
            }
        }

    }
}

