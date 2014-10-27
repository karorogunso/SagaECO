using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaMap.Localization;

namespace SagaMap.Localization.Languages
{
    public class Thai : Strings
    {
        public Thai()
        {
            this.ATCOMMAND_NO_ACCESS = "คุณไม่สามารถใช้คำสั่งนี้ได้!";
            this.ATCOMMAND_ITEM_PARA = "Usage: !item itemID";
            this.ATCOMMAND_ITEM_NO_SUCH_ITEM = "ไม่มีไอเท็มนี้";
            this.PLAYER_LOG_IN = "Player:{0} ได้เข้าสู่เกม.";
            this.CLIENT_CONNECTING = "ตัวเกม(Version:{0}) พยายามจะเชื่อมต่อ...";
            this.NEW_CLIENT = "New client from: {0}";
            this.INITIALIZATION = "Starting Initialization...";
            this.ACCEPTING_CLIENT = "Accepting clients.";

            this.ITEM_ADDED = "Got {1} [{0}]";
            this.ITEM_DELETED = "Lost {1} [{0}]";
        }
    }
}

