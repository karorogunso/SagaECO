//#define Thai

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
#if Thai
            this.VISIT_OUR_HOMEPAGE = "ยินดีต้อนรับสู่ ECO-THAI ( www.eco-thai.com)";
            //this.VISIT_OUR_HOMEPAGE = "ยินดีต้อนรับสู่ ECO-WASABI ( www.ecowasabi.com)";
            //this.VISIT_OUR_HOMEPAGE = " ยินดีต้อนรับสู่ ECO-KITTY (www.eco-kitty.net)";
#else
            this.VISIT_OUR_HOMEPAGE = "Plese visit this Emulator,SagaECO's homepage: http://www.sagaeco.com";
#endif
            this.ATCOMMAND_DESC = new Dictionary<string, string>();
            this.ATCOMMAND_DESC.Add("/who", "Displays the current number of players online");
            this.ATCOMMAND_DESC.Add("/motion", "To do routine expressions");
            this.ATCOMMAND_DESC.Add("/vcashshop", "Opens the Mall Shop");
            this.ATCOMMAND_DESC.Add("/user", "Displays the current online players, names and map names");
            this.ATCOMMAND_DESC.Add("/commandlist", "Shows all the available commands");
            this.ATCOMMAND_DESC.Add("!warp", "Teleports to the specified map and coordinates");
            this.ATCOMMAND_DESC.Add("!announce", "Announces a global message");
            this.ATCOMMAND_DESC.Add("!heal", "Recover HP/MP/SP");
            this.ATCOMMAND_DESC.Add("!level", "Adjust the level to a specified value");
            this.ATCOMMAND_DESC.Add("!gold", "Adjust the player's gold");
            this.ATCOMMAND_DESC.Add("!shoppoint", "Adjust the player's mall points");
            this.ATCOMMAND_DESC.Add("!hairstyle", "Adjust the players hairstyle");
            this.ATCOMMAND_DESC.Add("!haircolor", "Adjust the players haircolor");
            this.ATCOMMAND_DESC.Add("!job", "Change the players job profession");
            this.ATCOMMAND_DESC.Add("!joblevel", "Adjust the job level to a specified value");
            this.ATCOMMAND_DESC.Add("!statpoints", "Adjust the player stat points to the specified value");
            this.ATCOMMAND_DESC.Add("!skillpoints", "Adjust the player skill points to the specified value，!skillpoints [job] value，job has 1,2-1,2-2");
            this.ATCOMMAND_DESC.Add("!event", "Calls the specified EventID script");
            this.ATCOMMAND_DESC.Add("!hairext", "Adjust the current players attached hair");
            this.ATCOMMAND_DESC.Add("!playersize", "Adjust the current players size");
            this.ATCOMMAND_DESC.Add("!item", "Summons a specifed item");
            this.ATCOMMAND_DESC.Add("!speed", "Adjust the players speed");
            this.ATCOMMAND_DESC.Add("!revive", "Resurrect the player");
            this.ATCOMMAND_DESC.Add("!kick", "Kick away the designated player");
            this.ATCOMMAND_DESC.Add("!kickall", "Kicked away all online players");
            this.ATCOMMAND_DESC.Add("!jump", "Teleport to a specific player");
            this.ATCOMMAND_DESC.Add("!recall", "Call a specific player");
            this.ATCOMMAND_DESC.Add("!mob", "Brush out the specified number of the designated monster");
            this.ATCOMMAND_DESC.Add("!summon", "Summon a monster as a pet");
            this.ATCOMMAND_DESC.Add("!summonme", "Summon yourself as a pet");
            this.ATCOMMAND_DESC.Add("!spawn", "Strange point of making brushes");
            this.ATCOMMAND_DESC.Add("!effect", "Show the specified effect");
            this.ATCOMMAND_DESC.Add("!skill", "Acquisition of a skill");
            this.ATCOMMAND_DESC.Add("!skillclear", "Clear all the skills");
            this.ATCOMMAND_DESC.Add("!who", "Shows the number of current online players");
            this.ATCOMMAND_DESC.Add("!who2", "Shows the number and name of the current online players");
            this.ATCOMMAND_DESC.Add("!go", "Teleport to a city");
            this.ATCOMMAND_DESC.Add("!info", "Displays the current map element nformation");
            this.ATCOMMAND_DESC.Add("!cash", "Adjust the players cash");
            this.ATCOMMAND_DESC.Add("!reloadscript", "Reload the scripts");
            this.ATCOMMAND_DESC.Add("!reloadconfig", "Reload the configs(ECOShop,ShopDB,monster,Quests,Treasure,Theater)");
            this.ATCOMMAND_DESC.Add("!raw", "Send Packet");


            this.ATCOMMAND_COMMANDLIST = "Usage： /commandlist";
            this.ATCOMMAND_MODE_PARA = "Usage: !mode 1-2";
            this.ATCOMMAND_PK_MODE_INFO = "PK Mode:On";
            this.ATCOMMAND_NORMAL_MODE_INFO = "PK Mode:Off";
            this.ATCOMMAND_WARP_PARA = "Usage: !warp mapID x y";
            this.ATCOMMAND_NO_ACCESS = "You don't have access to this command!";
            this.ATCOMMAND_ITEM_PARA = "Usage: !item itemID";
            this.ATCOMMAND_ITEM_NO_SUCH_ITEM = "No such item!";
            this.ATCOMMAND_ANNOUNCE_PARA = "Usage: !announce xxoo";
            this.ATCOMMAND_HEAL_MESSAGE = "HP/MP/SP recovered";
            this.ATCOMMAND_LEVEL_PARA = "Usage: !level xxoo";
            this.ATCOMMAND_GOLD_PARA = "Usage: !gold xxoo";
            this.ATCOMMAND_SHOPPOINT_PARA = "Usage: !shoppoint xxoo";
            this.ATCOMMAND_HAIR_PAEA = "Use: !hair StyleByte WigByte ColorByte";
            this.ATCOMMAND_HAIR_ERROR = "Cannot change color for current hair style";
            this.ATCOMMAND_HAIRSTYLE_PARA = "Usage: !hairstyle 1-15";
            this.ATCOMMAND_HAIRCOLOR_PARA = "Usage: !haircolor 1-22";
            this.ATCOMMAND_HAIRCOLOR_ERROR = "Cannot change color for current hair style";
            this.ATCOMMAND_PLAYERSIZE_PARA = "Usage: !playersize size (Defaultsize:1000)";
            this.ATCOMMAND_SPAWN_PARA = "Usage: !spawn mobid amount range delay";
            this.ATCOMMAND_SPAWN_SUCCESS = "Spawn:{0} amount:{1} range:{2} delay:{3} added";

            this.PLAYER_LOG_IN = "Player:{0} logged in.";
            this.PLAYER_LOG_OUT = "Player:{0} logged out.";
            this.CLIENT_CONNECTING = "Client(Version:{0}) is trying to connect...";
            this.NEW_CLIENT = "New client from: {0}";
            this.INITIALIZATION = "Starting Initialization...";
            this.ACCEPTING_CLIENT = "Accepting clients.";
            this.ATCOMMAND_KICK_PARA = "Usage: !kick name";
            this.ATCOMMAND_KICKALL_PARA = "Usage: !kickall";
            this.ATCOMMAND_SPEED_PARA = "Usage: !speed xxoo (Defaultspeed:420)";
            this.ATCOMMAND_JUMP_PARA = "Usage: !jump playername";
            this.ATCOMMAND_HAIREXT_PARA = "Usage: !hairext 1-52";
            this.ITEM_ADDED = "Got {1} [{0}]";
            this.ITEM_DELETED = "Lost {1} [{0}]";
            this.ITEM_WARE_GET = "Get {1} [{0}] from ware house";
            this.ITEM_WARE_PUT = "Put {1} [{0}] into ware house";

            this.GET_EXP = "Get BaseEXP {0}  JobEXP {1}";
            this.ATCOMMAND_ONLINE_PLAYER_INFO = "Online Player:";

            this.NPC_EventID_NotFound = "EVENTID {0} not found";
            this.NPC_EventID_NotFound_Msg = "...";
            this.NPC_INPUT_BANK = "Enter amount. Current balance:{0}";
            this.NPC_BANK_NOT_ENOUGH_GOLD = "Not Enough Gold！";

            this.ATCOMMAND_MOB_ERROR = "Your Command is error";
            this.ATCOMMAND_WARP_ERROR = "Don't Warp this Map";

            this.PET_FRIENDLY_DOWN = "{0}'s friendly towards you is decreased.";

            this.POSSESSION_EXP = "Get (Possession) BaseEXP {0}  JobEXP {1} ";
            this.POSSESSION_DONE = "Possessioned to [{0}]";
            this.POSSESSION_RIGHT = "Right hand";
            this.POSSESSION_LEFT = "Left hand";
            this.POSSESSION_NECK = "Necklace";
            this.POSSESSION_ARMOR = "Armor";

            this.QUEST_HOW_TO_DO = "How to do?";
            this.QUEST_NOT_CANCEL = "Nothing will be canceled";
            this.QUEST_CANCEL = "Cancel quest";
            this.QUEST_CANCELED = "Quest is canceled......$R;";
            this.QUEST_REWARDED = "Got Reward!$R;";
            this.QUEST_FAILED = "Quest failed.....$R;";
            this.QUEST_IF_TAKE_QUEST = "Do you want to take the quest?";
            this.QUEST_TAKE = "Take it";
            this.QUEST_NOT_TAKE = "Don't take it";
            this.QUEST_TRANSPORT_GET = "Got courier luggage.";
            this.QUEST_TRANSPORT_GIVE = "Gave courier luggage.";

            this.PARTY_NEW_NAME = "New Party";

            this.SKILL_ACTOR_DELETE = "[{0}] destoried!!!";
            this.SKILL_STATUS_ENTER = "Became {0} Status";
            this.SKILL_STATUS_LEAVE = "{0} Status canceled";
            this.SKILL_DECOY = "Decoy:";

            this.ITEM_TREASURE_OPEN = "Please select the box";
            this.ITEM_TREASURE_NO_NEED = "There is no need to open the box";
            this.ITEM_IDENTIFY = "Please select the items to be identified";
            this.ITEM_IDENTIFY_NO_NEED = "There is no need to identify the items";
            this.ITEM_IDENTIFY_RESULT = "Identification Results: {0} -> {1}";
            this.ITEM_UNIDENTIFIED_NONE = "None";
            this.ITEM_UNIDENTIFIED_HELM = "Helmet";
            this.ITEM_UNIDENTIFIED_ACCE_HEAD = "Head Accessory";
            this.ITEM_UNIDENTIFIED_ACCE_FACE0 = "Face Accessory 1";
            this.ITEM_UNIDENTIFIED_ACCE_FACE1 = "Face Accessory 2";
            this.ITEM_UNIDENTIFIED_ACCE_FACE2 = "Face Accessory 3";
            this.ITEM_UNIDENTIFIED_ACCE_NECK = "Necklace";
            this.ITEM_UNIDENTIFIED_ACCE_FINGER = "Ring";
            this.ITEM_UNIDENTIFIED_ARMOR_UPPER = "Upper Body Armor";
            this.ITEM_UNIDENTIFIED_ARMOR_LOWER = "Lower Body Armor";
            this.ITEM_UNIDENTIFIED_ONEPIECE = "Dress";
            this.ITEM_UNIDENTIFIED_OVERALLS = "Work Pants";
            this.ITEM_UNIDENTIFIED_BODYSUIT = "Body suit";
            this.ITEM_UNIDENTIFIED_FACEBODYSUIT = "Face Body suit";
            this.ITEM_UNIDENTIFIED_BACKPACK = "Backpack";
            this.ITEM_UNIDENTIFIED_COAT = "Coat";
            this.ITEM_UNIDENTIFIED_SOCKS = "Socks";
            this.ITEM_UNIDENTIFIED_BOOTS = "Boots";
            this.ITEM_UNIDENTIFIED_SLACKS = "Slacks";
            this.ITEM_UNIDENTIFIED_LONGBOOTS = "Long Boots";
            this.ITEM_UNIDENTIFIED_HALFBOOTS = "Half Boots";
            this.ITEM_UNIDENTIFIED_FULLFACE = "Full Face";
            this.ITEM_UNIDENTIFIED_SHORT_SWORD = "Dagger";
            this.ITEM_UNIDENTIFIED_SWORD = "Sword";
            this.ITEM_UNIDENTIFIED_RAPIER = "Rapier";
            this.ITEM_UNIDENTIFIED_CLAW = "Claw";
            this.ITEM_UNIDENTIFIED_KNUCKLE = "Knucle";
            this.ITEM_UNIDENTIFIED_SHIELD = "Shield";
            this.ITEM_UNIDENTIFIED_HAMMER = "Hammer";
            this.ITEM_UNIDENTIFIED_AXE = "Axe";
            this.ITEM_UNIDENTIFIED_SPEAR = "Spear";
            this.ITEM_UNIDENTIFIED_STAFF = "Staff";
            this.ITEM_UNIDENTIFIED_THROW = "Throwing Weapon";
            this.ITEM_UNIDENTIFIED_BOW = "Bow";
            this.ITEM_UNIDENTIFIED_ARROW = "Arrow";
            this.ITEM_UNIDENTIFIED_GUN = "Gun";
            this.ITEM_UNIDENTIFIED_BULLET = "Bullet";
            this.ITEM_UNIDENTIFIED_HANDBAG = "Handbag";
            this.ITEM_UNIDENTIFIED_LEFT_HANDBAG = "Left Handbag";
            this.ITEM_UNIDENTIFIED_BOOK = "Book";
            this.ITEM_UNIDENTIFIED_INSTRUMENT = "Instrument";
            this.ITEM_UNIDENTIFIED_ROPE = "Rope";
            this.ITEM_UNIDENTIFIED_CARD = "Card";
            this.ITEM_UNIDENTIFIED_ETC_WEAPON = "???";
            this.ITEM_UNIDENTIFIED_SHOES = "Shoes";
            this.ITEM_UNIDENTIFIED_MONEY = "Money";
            this.ITEM_UNIDENTIFIED_FOOD = "Food";
            this.ITEM_UNIDENTIFIED_POTION = "Potion";
            this.ITEM_UNIDENTIFIED_MARIONETTE = "Marionette";
            this.ITEM_UNIDENTIFIED_GOLEM = "Golem";
            this.ITEM_UNIDENTIFIED_TREASURE_BOX = "Treasure Box";
            this.ITEM_UNIDENTIFIED_CONTAINER = "Container";
            this.ITEM_UNIDENTIFIED_TIMBER_BOX = "Timer Box";
            this.ITEM_UNIDENTIFIED_SEED = "Seed";
            this.ITEM_UNIDENTIFIED_SCROLL = "Scroll";
            this.ITEM_UNIDENTIFIED_SKILLBOOK = "Skill Book";
            this.ITEM_UNIDENTIFIED_PET = "Pet";
            this.ITEM_UNIDENTIFIED_PET_NEKOMATA = "Pet Nekomata";
            this.ITEM_UNIDENTIFIED_PET_YOUHEI = "Pet Youhei";
            this.ITEM_UNIDENTIFIED_BACK_DEMON = "Kay Tsutsumi";
            this.ITEM_UNIDENTIFIED_RIDE_PET = "Riding Pet";
            this.ITEM_UNIDENTIFIED_USE = "Props";
            this.ITEM_UNIDENTIFIED_PETFOOD = "Pet Food";
            this.ITEM_UNIDENTIFIED_STAMP = "Stamp";
            this.ITEM_UNIDENTIFIED_FG_FURNITURE = "Furniture";
            this.ITEM_UNIDENTIFIED_FG_BASEBUILD = "Parts";
            this.ITEM_UNIDENTIFIED_FG_ROOM_WALL = "Wallpaper";
            this.ITEM_UNIDENTIFIED_FG_ROOM_FLOOR = "Flooring";
            this.ITEM_UNIDENTIFIED_ITDGN = "Unknown thing";
            this.ITEM_UNIDENTIFIED_ROBOT_GROW = "Robot";
            this.ITEM_UNIDENTIFIED_COSTUME = "Special Clothing";

            this.FG_NAME = "This is {0}'s Flying garden";
            this.FG_NOT_FOUND = "You don't own a flying garden";
            this.FG_ALREADY_CALLED = "You've already called the flying garden";
            this.FG_CANNOT = "You can't call flying garden here";
            this.FG_FUTNITURE_SETUP = "{0} Placed ({1}/{2})";
            this.FG_FUTNITURE_REMOVE = "{0} Removed ({1}/{2})";
            this.FG_FUTNITURE_MAX = "Cannot place furniture any more";


            this.ITD_HOUR = "Hour";
            this.ITD_MINUTE = "Minute";
            this.ITD_SECOND = "Second";
            this.ITD_CRASHING = "This dungeon will collapse in {0}.";
            this.ITD_CREATED = "'s dungeon is created";
            this.ITD_PARTY_DISMISSED = "Party disbanded. This dungeon will collapse";
            this.ITD_QUEST_CANCEL = "Players canceled the quest. The dungeon will collapse";
            this.ITD_SELECT_DUUNGEON = "Select Dungeon";
            this.ITD_DUNGEON_NAME = " Dungeon";

            this.THEATER_WELCOME = "Welcome to cinema!";
            this.THEATER_COUNTDOWN = "{0} will be showed in {1} minutes";
            this.NPC_SHOP_CP_GET = "Got {0} CP.";
            this.NPC_SHOP_ECOIN_GET = "Got {0} ecoin";
            this.NPC_SHOP_CP_LOST = "Lost {0} CP";
            this.NPC_SHOP_ECOIN_LOST = "Lost {0} ecoin";

            this.WRP_ENTER = "You are now in the Battle of Champion";
            this.WRP_GOT = "You've got {0} WRP";
            this.WRP_LOST = "You've lost {0} WRP";
            this.DEATH_PENALTY = "You've lost EXP due to death penalty";

            this.ODWAR_PREPARE = "DEMs are marching towards {0}, will arrive in about {1} minutes";
            this.ODWAR_PREPARE2 = "Please reinforce immediately";
            this.ODWAR_START = "City Defence War started";
            this.ODWAR_SYMBOL_DOWN = "Symobl·Nr.{0} has been destoried!!!";
            this.ODWAR_SYMBOL_ACTIVATE = "Symbol·Nr.{0} has been activated!!!";
            this.ODWAR_LOSE = "West Fort is captured by DEMs!!!";
            this.ODWAR_WIN = "West Fort's Defence War was successful!";
            this.ODWAR_WIN2 = "West Fort's Symbols are now generation Defence Field!";
            this.ODWAR_WIN3 = "Enemies are retreating from West Fort!";
            this.ODWAR_WIN4 = "Enemies retreated! We won!";
            this.ODWAR_CAPTURE = "We successfully captured West Fort City!!";

            this.EP_INCREASE = "EP will increase in {0} hours";
            this.EP_INCREASED = "EP increased {0} points";

            this.NPC_ITEM_FUSION_RECHOOSE = "I want to choose again";
            this.NPC_ITEM_FUSION_CANCEL = "I want to cancel it";
            this.NPC_ITEM_FUSION_CONFIRM = "Success Rate{1}% {0}G";

        }

        public override string EnglishName
        {
            get { return "English"; }
        }

        public override string LocalName
        {
            get { return "English"; }
        }
    }
}
