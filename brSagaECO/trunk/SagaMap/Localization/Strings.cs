using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Localization
{
    public abstract class Strings
    {
        public string VISIT_OUR_HOMEPAGE;

        #region Console messages
        public string PLAYER_LOG_IN;
        public string PLAYER_LOG_OUT;
        public string CLIENT_CONNECTING;
        public string NEW_CLIENT;
        public string INITIALIZATION;
        public string ACCEPTING_CLIENT;
        #endregion

        #region Ingame messages
        public string ITEM_ADDED;
        public string ITEM_DELETED;
        public string ITEM_WARE_PUT;
        public string ITEM_WARE_GET;
        public string GET_EXP;

        #endregion
        public string SHOP_OPEN;
        #region ATCommands
        public string ATCOMMAND_COMMANDLIST;
        public Dictionary<string, string> ATCOMMAND_DESC;

        public string ATCOMMAND_TA_PAEA;

        public string ATCOMMAND_NO_ACCESS;
        public string ATCOMMAND_ITEM_PARA;
        public string ATCOMMAND_ITEM_NO_SUCH_ITEM;
        public string ATCOMMAND_ITEM_IDSEARCH;
        public string ATCOMMAND_ANNOUNCE_PARA;
        public string ATCOMMAND_WARP_PARA;
        public string ATCOMMAND_HEAL_MESSAGE;
        public string ATCOMMAND_LEVEL_PARA;
        public string ATCOMMAND_GOLD_PARA;
        public string ATCOMMAND_SHOPPOINT_PARA;
        public string ATCOMMAND_HAIR_PAEA;
        public string ATCOMMAND_HAIR_ERROR;
        public string ATCOMMAND_HAIRSTYLE_PARA;
        public string ATCOMMAND_HAIRCOLOR_PARA;
        public string ATCOMMAND_HAIRCOLOR_ERROR;
        public string ATCOMMAND_PLAYERSIZE_PARA;
        public string ATCOMMAND_KICK_PARA;
        public string ATCOMMAND_KICKALL_PARA;
        public string ATCOMMAND_SPEED_PARA;
        public string ATCOMMAND_JUMP_PARA;
        public string ATCOMMAND_HAIREXT_PARA;
        public string ATCOMMAND_ONLINE_PLAYER_INFO;
        public string ATCOMMAND_MODE_PARA;
        public string ATCOMMAND_MOB_ERROR;
        public string ATCOMMAND_WARP_ERROR;
        public string ATCOMMAND_PK_MODE_INFO;
        public string ATCOMMAND_NORMAL_MODE_INFO;
        public string ATCOMMAND_SPAWN_PARA;
        public string ATCOMMAND_SPAWN_SUCCESS;
        #endregion

        //Npc Message
        public string NPC_EventID_NotFound;
        public string NPC_EventID_NotFound_Msg;
        public string NPC_INPUT_BANK;
        public string NPC_BANK_NOT_ENOUGH_GOLD;

        public string PET_FRIENDLY_DOWN;

        public string POSSESSION_EXP;
        public string POSSESSION_DONE;
        public string POSSESSION_RIGHT;
        public string POSSESSION_LEFT;
        public string POSSESSION_NECK;
        public string POSSESSION_ARMOR;

        //Quest Messages
        public string QUEST_HOW_TO_DO;
        public string QUEST_NOT_CANCEL;
        public string QUEST_CANCEL;
        public string QUEST_CANCELED;
        public string QUEST_REWARDED;
        public string QUEST_FAILED;
        public string QUEST_IF_TAKE_QUEST;
        public string QUEST_TAKE;
        public string QUEST_NOT_TAKE;
        public string QUEST_TRANSPORT_GET;
        public string QUEST_TRANSPORT_GIVE;

        //Party Strings
        public string PARTY_NEW_NAME;

        //Skills
        public string SKILL_ACTOR_DELETE;
        public string SKILL_STATUS_ENTER;
        public string SKILL_STATUS_LEAVE;
        public string SKILL_DECOY;

        public string ITEM_TREASURE_OPEN;
        public string ITEM_TREASURE_NO_NEED;
        public string ITEM_IDENTIFY;
        public string ITEM_IDENTIFY_NO_NEED;
        public string ITEM_IDENTIFY_RESULT;
        public string ITEM_UNIDENTIFIED_NONE;
        public string ITEM_UNIDENTIFIED_HELM;
        public string ITEM_UNIDENTIFIED_ACCE_HEAD;
        public string ITEM_UNIDENTIFIED_ACCE_FACE0;
        public string ITEM_UNIDENTIFIED_ACCE_FACE1;
        public string ITEM_UNIDENTIFIED_ACCE_FACE2;
        public string ITEM_UNIDENTIFIED_ACCE_NECK;
        public string ITEM_UNIDENTIFIED_ACCE_FINGER;
        public string ITEM_UNIDENTIFIED_ARMOR_UPPER;
        public string ITEM_UNIDENTIFIED_ARMOR_LOWER;
        public string ITEM_UNIDENTIFIED_ONEPIECE;
        public string ITEM_UNIDENTIFIED_COSTUME;
        public string ITEM_UNIDENTIFIED_EQ_ALLSLOT;
        public string ITEM_UNIDENTIFIED_OVERALLS;
        public string ITEM_UNIDENTIFIED_BODYSUIT;
        public string ITEM_UNIDENTIFIED_FACEBODYSUIT;
        public string ITEM_UNIDENTIFIED_BACKPACK;
        public string ITEM_UNIDENTIFIED_COAT;
        public string ITEM_UNIDENTIFIED_SOCKS;
        public string ITEM_UNIDENTIFIED_BOOTS;
        public string ITEM_UNIDENTIFIED_SLACKS;
        public string ITEM_UNIDENTIFIED_LONGBOOTS;
        public string ITEM_UNIDENTIFIED_HALFBOOTS;
        public string ITEM_UNIDENTIFIED_FULLFACE;
        public string ITEM_UNIDENTIFIED_SHORT_SWORD;
        public string ITEM_UNIDENTIFIED_SWORD;
        public string ITEM_UNIDENTIFIED_RAPIER;
        public string ITEM_UNIDENTIFIED_CLAW;
        public string ITEM_UNIDENTIFIED_KNUCKLE;
        public string ITEM_UNIDENTIFIED_SHIELD;
        public string ITEM_UNIDENTIFIED_HAMMER;
        public string ITEM_UNIDENTIFIED_AXE;
        public string ITEM_UNIDENTIFIED_SPEAR;
        public string ITEM_UNIDENTIFIED_STAFF;
        public string ITEM_UNIDENTIFIED_THROW;
        public string ITEM_UNIDENTIFIED_BOW;
        public string ITEM_UNIDENTIFIED_ARROW;
        public string ITEM_UNIDENTIFIED_GUN;
        public string ITEM_UNIDENTIFIED_BULLET;
        public string ITEM_UNIDENTIFIED_HANDBAG;
        public string ITEM_UNIDENTIFIED_LEFT_HANDBAG;
        public string ITEM_UNIDENTIFIED_BOOK;
        public string ITEM_UNIDENTIFIED_INSTRUMENT;
        public string ITEM_UNIDENTIFIED_ROPE;
        public string ITEM_UNIDENTIFIED_CARD;
        public string ITEM_UNIDENTIFIED_ETC_WEAPON;
        public string ITEM_UNIDENTIFIED_SHOES;
        public string ITEM_UNIDENTIFIED_MONEY;
        public string ITEM_UNIDENTIFIED_FOOD;
        public string ITEM_UNIDENTIFIED_POTION;
        public string ITEM_UNIDENTIFIED_MARIONETTE;
        public string ITEM_UNIDENTIFIED_GOLEM;
        public string ITEM_UNIDENTIFIED_TREASURE_BOX;
        public string ITEM_UNIDENTIFIED_CONTAINER;
        public string ITEM_UNIDENTIFIED_TIMBER_BOX;
        public string ITEM_UNIDENTIFIED_SEED;
        public string ITEM_UNIDENTIFIED_SCROLL;
        public string ITEM_UNIDENTIFIED_SKILLBOOK;
        public string ITEM_UNIDENTIFIED_PET;
        public string ITEM_UNIDENTIFIED_PET_NEKOMATA;
        public string ITEM_UNIDENTIFIED_PET_YOUHEI;
        public string ITEM_UNIDENTIFIED_BACK_DEMON;
        public string ITEM_UNIDENTIFIED_RIDE_PET;
        public string ITEM_UNIDENTIFIED_USE;
        public string ITEM_UNIDENTIFIED_PETFOOD;
        public string ITEM_UNIDENTIFIED_STAMP;
        public string ITEM_UNIDENTIFIED_FG_FURNITURE;
        public string ITEM_UNIDENTIFIED_FG_BASEBUILD;
        public string ITEM_UNIDENTIFIED_FG_ROOM_WALL;
        public string ITEM_UNIDENTIFIED_FG_ROOM_FLOOR;
        public string ITEM_UNIDENTIFIED_ITDGN;
        public string ITEM_UNIDENTIFIED_ROBOT_GROW;


        //FGarden
        public string FG_NAME;
        public string FG_ALREADY_CALLED;
        public string FG_NOT_FOUND;
        public string FG_CANNOT;
        public string FG_FUTNITURE_SETUP;
        public string FG_FUTNITURE_REMOVE;
        public string FG_FUTNITURE_MAX;
        //Dungeon
        public string ITD_HOUR;
        public string ITD_MINUTE;
        public string ITD_SECOND;
        public string ITD_CRASHING;
        public string ITD_CREATED;
        public string ITD_PARTY_DISMISSED;
        public string ITD_QUEST_CANCEL;
        public string ITD_SELECT_DUUNGEON;
        public string ITD_DUNGEON_NAME;

        public string THEATER_WELCOME;
        public string THEATER_COUNTDOWN;

        public string NPC_SHOP_CP_GET;
        public string NPC_SHOP_ECOIN_GET;
        public string NPC_SHOP_CP_LOST;
        public string NPC_SHOP_ECOIN_LOST;

        public string DEATH_PENALTY;
        public string WRP_GOT;
        public string WRP_LOST;
        public string WRP_ENTER;

        public string ODWAR_PREPARE;
        public string ODWAR_PREPARE2;
        public string ODWAR_START;
        public string ODWAR_SYMBOL_DOWN;
        public string ODWAR_SYMBOL_ACTIVATE;
        public string ODWAR_WIN;
        public string ODWAR_WIN2;
        public string ODWAR_WIN3;
        public string ODWAR_WIN4;
        public string ODWAR_LOSE;
        public string ODWAR_CAPTURE;

        public string EP_INCREASE;
        public string EP_INCREASED;

        public string NPC_ITEM_FUSION_RECHOOSE;
        public string NPC_ITEM_FUSION_CANCEL;
        public string NPC_ITEM_FUSION_CONFIRM;

        public abstract string EnglishName { get; }

        public abstract string LocalName { get; }
    }
}
