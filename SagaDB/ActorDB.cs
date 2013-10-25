using System;
using System.Collections.Generic;
using System.Text;


using SagaDB.Actor;

namespace SagaDB
{
    public interface ActorDB
    {       

        /// <summary>
        /// Write the given character to the database.
        /// </summary>
        /// <param name="user">Character that needs to be writen.</param>
        void SaveChar(ActorPC aChar);

        void CreateChar(ActorPC aChar,int account_id);

        void DeleteChar(ActorPC aChar);

        ActorPC GetChar(uint charID);

        bool CharExists(string name);

        uint GetAccountID(uint charID);

        uint[] GetCharIDs(int account_id);

        string GetCharName(uint id);

        bool Connect();

        bool isConnected();
    }
}
