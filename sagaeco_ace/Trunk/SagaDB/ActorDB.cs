using System;
using System.Collections.Generic;
using System.Text;


using SagaDB.Actor;
using SagaDB.BBS;

namespace SagaDB
{
    public interface ActorDB
    {

        /// <summary>
        /// Write the given character to the database.
        /// </summary>
        /// <param name="aChar">Character that needs to be writen.</param>
        void SaveChar(ActorPC aChar);

        void SaveChar(ActorPC aChar, bool fullinfo);

        void SaveChar(ActorPC aChar, bool itemInfo, bool fullinfo);

        void CreateChar(ActorPC aChar, int account_id);

        void DeleteChar(ActorPC aChar);

        ActorPC GetChar(uint charID);

        ActorPC GetChar(uint charID, bool fullinfo);

        bool CharExists(string name);

        uint GetAccountID(ActorPC pc);

        uint GetAccountID(uint charID);

        uint[] GetCharIDs(int account_id);

        string GetCharName(uint id);

        bool Connect();

        bool isConnected();

        /// <summary>
        /// 取得指定玩家的好友列表
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <returns>好友列表</returns>
        List<ActorPC> GetFriendList(ActorPC pc);

        /// <summary>
        /// 取得添加指定玩家为好友的玩家列表
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <returns>玩家列表</returns>
        List<ActorPC> GetFriendList2(ActorPC pc);

        void AddFriend(ActorPC pc, uint charID);

        bool IsFriend(uint char1, uint char2);

        void DeleteFriend(uint char1, uint char2);

        Party.Party GetParty(uint id);

        void NewParty(Party.Party party);

        void SaveParty(Party.Party party);

        void DeleteParty(Party.Party party);

        Ring.Ring GetRing(uint id);

        void NewRing(Ring.Ring ring);

        void SaveRing(Ring.Ring ring, bool saveMembers);

        void DeleteRing(Ring.Ring ring);

        void RingEmblemUpdate(Ring.Ring ring, byte[] buf);

        byte[] GetRingEmblem(uint ring_id, DateTime date, out bool needUpdate, out DateTime newTime);

        List<BBS.Post> GetBBSPage(uint bbsID, int page);

        List<BBS.Mail> GetMail(ActorPC pc);

        bool BBSNewPost(ActorPC poster, uint bbsID, string title, string content);

        ActorPC LoadServerVar();

        void SaveServerVar(ActorPC fakepc);

        void GetVShop(ActorPC pc);

        void SaveVShop(ActorPC pc);

        void SaveWRP(ActorPC pc);

        void SaveSkill(ActorPC pc);

        void GetSkill(ActorPC pc);

        List<ActorPC> GetWRPRanking();

        List<SagaDB.FFarden.FFarden> GetFFList();

        void SaveFF(Ring.Ring ring);

        void SaveSerFF(Server.Server ser);

        void GetSerFFurniture(Server.Server ser);

        void SaveFFCopy(Dictionary<SagaDB.FFarden.FurniturePlace, List<ActorFurniture>> Furnitures);

        void CreateFF(ActorPC pc);

        void GetFF(ActorPC pc);

        uint GetFFRindID(uint ffid);

        void GetFFurniture(SagaDB.Ring.Ring ring);

        void GetFFurnitureCopy(Dictionary<SagaDB.FFarden.FurniturePlace, List<ActorFurniture>> Furnitures);

        void SavaLevelLimit();

        void GetLevelLimit();
        uint AddNewGift(Gift gift);
        List<BBS.Gift> GetGifts(ActorPC pc);
    }
}
