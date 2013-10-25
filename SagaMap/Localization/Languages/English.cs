using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaMap.Localization;

namespace SagaMap.Localization.Languages
{
    public class English : Strings
    {
        public English()
        {
            this.ATCOMMAND_NO_ACCESS = "You don't have access to this command!";
            this.ATCOMMAND_ITEM_PARA = "Usage: !item itemID";
            this.ATCOMMAND_ITEM_NO_SUCH_ITEM = "No such item!";
            this.PLAYER_LOG_IN = "Player:{0} logged in.";
            this.CLIENT_CONNECTING = "Client(Version:{0}) is trying to connect...";
            this.NEW_CLIENT = "New client from: {0}";
            this.INITIALIZATION = "Starting Initialization...";
            this.ACCEPTING_CLIENT = "Accepting clients.";

            this.ITEM_ADDED = "Got {1} [{0}]";
            this.ITEM_DELETED = "Lost {1} [{0}]";
        }
    }
}
