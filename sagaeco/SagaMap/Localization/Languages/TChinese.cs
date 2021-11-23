using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaMap.Localization;

namespace SagaMap.Localization.Languages
{
    public class TChinese : Strings
    {
        public TChinese()
        {
            this.VISIT_OUR_HOMEPAGE = "請訪問糢擬器SagaECO的主頁：http://www.sagaeco.com";

            this.ATCOMMAND_DESC = new Dictionary<string, string>();
            this.ATCOMMAND_DESC.Add("/who", "显示当前在线玩家数量");
            this.ATCOMMAND_DESC.Add("/motion", "做指定动作表情");
            this.ATCOMMAND_DESC.Add("/vcashshop", "打开道具商城");
            this.ATCOMMAND_DESC.Add("/user", "显示当前在线玩家名字以及地图名");
            this.ATCOMMAND_DESC.Add("/commandlist", "显示所有当前玩家能用的命令列表");
            this.ATCOMMAND_DESC.Add("!warp", "瞬移到指定地图的指定坐标");
            this.ATCOMMAND_DESC.Add("!announce", "发布全服公告");
            this.ATCOMMAND_DESC.Add("!heal", "瞬间恢复当前HP/MP/SP");
            this.ATCOMMAND_DESC.Add("!level", "调整人物等级到指定数值");
            this.ATCOMMAND_DESC.Add("!gold", "调整当前玩家的现金");
            this.ATCOMMAND_DESC.Add("!shoppoint", "调整当前帐号的商城点数");
            this.ATCOMMAND_DESC.Add("!hairstyle", "调整玩家发型");
            this.ATCOMMAND_DESC.Add("!haircolor", "调整玩家发色");
            this.ATCOMMAND_DESC.Add("!job", "调整玩家职业");
            this.ATCOMMAND_DESC.Add("!joblevel", "调整人物职业等级到指定数值");
            this.ATCOMMAND_DESC.Add("!statpoints", "调整玩家剩余属性点数到指定数值");
            this.ATCOMMAND_DESC.Add("!skillpoints", "调整玩家剩余技能点，!skillpoints [job] 数值，job有1，2－1，2－2");
            this.ATCOMMAND_DESC.Add("!event", "调用指定EventID的脚本");
            this.ATCOMMAND_DESC.Add("!hairext", "调整当前人物的附加头发");
            this.ATCOMMAND_DESC.Add("!playersize", "调整当前人物的人物大小");
            this.ATCOMMAND_DESC.Add("!item", "制造指定数量的指定道具");
            this.ATCOMMAND_DESC.Add("!speed", "调整当前人物移动速度");
            this.ATCOMMAND_DESC.Add("!revive", "复活当前人物");
            this.ATCOMMAND_DESC.Add("!kick", "踢掉指定玩家");
            this.ATCOMMAND_DESC.Add("!kickall", "踢掉所有在线玩家");
            this.ATCOMMAND_DESC.Add("!jump", "瞬移到指定玩家处");
            this.ATCOMMAND_DESC.Add("!recall", "瞬移指定玩家到此玩家处");
            this.ATCOMMAND_DESC.Add("!mob", "刷出指定数量指定怪物");
            this.ATCOMMAND_DESC.Add("!summon", "召唤某怪物为宠物");
            this.ATCOMMAND_DESC.Add("!summonme", "召唤自己为宠物");
            this.ATCOMMAND_DESC.Add("!spawn", "制作刷怪点");
            this.ATCOMMAND_DESC.Add("!effect", "显示指定特效");
            this.ATCOMMAND_DESC.Add("!skill", "习得某技能");
            this.ATCOMMAND_DESC.Add("!skillclear", "忘记所有技能");
            this.ATCOMMAND_DESC.Add("!who", "显示当前在线玩家的名字");
            this.ATCOMMAND_DESC.Add("!who2", "显示当前在线玩家的名字，所在地图以及坐标");
            this.ATCOMMAND_DESC.Add("!go", "快速移动到某城市");
            this.ATCOMMAND_DESC.Add("!info", "显示当前地图单元的元素信息");
            this.ATCOMMAND_DESC.Add("!cash", "调整当前人物的现金");
            this.ATCOMMAND_DESC.Add("!reloadscript", "重新读取脚本");
            this.ATCOMMAND_DESC.Add("!reloadconfig", "重新读取某设定(ECOShop,ShopDB,monster,Quests,Treasure,Theater)");
            this.ATCOMMAND_DESC.Add("!raw", "发送封包");
            
            this.ATCOMMAND_COMMANDLIST = "您能使用的命令有：";
            this.ATCOMMAND_MODE_PARA = "用法: !mode 1-2";
            this.ATCOMMAND_PK_MODE_INFO = "PK模式已開啟";
            this.ATCOMMAND_NORMAL_MODE_INFO = "PK模式已關閉";
            this.ATCOMMAND_WARP_PARA = "用法: !warp 地圖ID x y";
            this.ATCOMMAND_NO_ACCESS = "你沒有訪問這條命令的權限！";
            this.ATCOMMAND_ITEM_PARA = "用法: !item 物品ID";
            this.ATCOMMAND_ITEM_NO_SUCH_ITEM = "沒有這件物品！";
            this.ATCOMMAND_ANNOUNCE_PARA = "用法: !announce 內容";
            this.ATCOMMAND_HEAL_MESSAGE = "HP/MP/SP 全滿";
            this.ATCOMMAND_LEVEL_PARA = "用法: !level 等級";
            this.ATCOMMAND_GOLD_PARA = "用法: !gold 數量";
            this.ATCOMMAND_SHOPPOINT_PARA = "用法: !shoppoint 數量";
            this.ATCOMMAND_HAIR_PAEA = "用法: !hair Style數值 Wig數值 Color數值";
            this.ATCOMMAND_HAIR_ERROR = "當前髮型無法更換顏色";
            this.ATCOMMAND_HAIRSTYLE_PARA = "用法: !hairstyle 1-15";
            this.ATCOMMAND_HAIRCOLOR_PARA = "用法: !haircolor 1-22";
            this.ATCOMMAND_HAIRCOLOR_ERROR = "當前髮型無法更換顏色";
            this.ATCOMMAND_PLAYERSIZE_PARA = "用法: !playersize 數值 (默認大小:1000)";
            this.ATCOMMAND_SPAWN_PARA = "用法: !spawn 怪物ID 數量 範圍 刷怪時間";
            this.ATCOMMAND_SPAWN_SUCCESS = "Spawn:{0} amount:{1} range:{2} delay:{3} added";

            this.PLAYER_LOG_IN = "玩家：{0} 已經登錄";
            this.PLAYER_LOG_OUT = "玩家：{0} 已登出";
            this.CLIENT_CONNECTING = "客户端(版本號:{0}) 正在嘗試連接中...";
            this.NEW_CLIENT = "新客户端： {0}";
            this.INITIALIZATION = "開啟初始化……";
            this.ACCEPTING_CLIENT = "開始接受客户端。";
            this.ATCOMMAND_KICK_PARA = "用法: !kick 名字";
            this.ATCOMMAND_KICKALL_PARA = "用法: !kickall";
            this.ATCOMMAND_SPEED_PARA = "用法: !speed 數值 (默認速度:420)";
            this.ATCOMMAND_JUMP_PARA = "用法: !jump 玩家名字";
            this.ATCOMMAND_HAIREXT_PARA = "用法: !hairext 1-52";

            this.ITEM_ADDED = "得到{1}個[{0}]";
            this.ITEM_DELETED = "失去{1}個[{0}]";
            this.ITEM_WARE_GET = "取出[{0}] {1}個";
            this.ITEM_WARE_PUT = "存放[{0}] {1}個";
            this.GET_EXP = "得到基本經驗值 {0}  職業經驗值 {1}";
            this.ATCOMMAND_ONLINE_PLAYER_INFO = "當前在線玩家:";
            this.NPC_EventID_NotFound = "NPC未實裝!$R無法找到腳本(Eventid={0})";
            this.NPC_EventID_NotFound_Msg = "..噓..別嘈";
            this.ATCOMMAND_MOB_ERROR = "你的召喚怪物命令使用錯誤!";
            this.ATCOMMAND_WARP_ERROR = "無法轉移到指定地圖";

            this.PET_FRIENDLY_DOWN = "{0}的親密度減少了";

            this.POSSESSION_EXP = "得到了(憑依)基本經驗值 {0}  職業經驗值 {1} ";
            this.POSSESSION_DONE = "憑依在[{0}]了";
            this.POSSESSION_RIGHT = "右手";
            this.POSSESSION_LEFT = "左手";
            this.POSSESSION_NECK = "項鏈";
            this.POSSESSION_ARMOR = "盔甲";

            this.NPC_INPUT_BANK = "輸入金額 目前有{0}個金幣";
            this.NPC_BANK_NOT_ENOUGH_GOLD = "所持金額不足！";

            this.QUEST_HOW_TO_DO = "怎麼做好呢？";
            this.QUEST_NOT_CANCEL = "什麼也不取消";
            this.QUEST_CANCEL = "取消任務";
            this.QUEST_CANCELED = "任務取消了……$R;";
            this.QUEST_REWARDED = "收到了報酬！$R;";
            this.QUEST_FAILED = "任務失敗了……$R;";
            this.QUEST_IF_TAKE_QUEST = "要接受委託嗎";
            this.QUEST_TAKE = "接受";
            this.QUEST_NOT_TAKE = "不接受";
            this.QUEST_TRANSPORT_GET = "收到行李";
            this.QUEST_TRANSPORT_GIVE = "東西已經轉交了";

            this.PARTY_NEW_NAME = "新的隊伍";

            this.SKILL_ACTOR_DELETE = "[{0}] 消滅了!!!";
            this.SKILL_STATUS_ENTER = "成了{0}狀態";
            this.SKILL_STATUS_LEAVE = "解除了{0}狀態";
            this.SKILL_DECOY = "分身：";

            this.ITEM_TREASURE_OPEN = "請選擇要開封的箱子";
            this.ITEM_TREASURE_NO_NEED = "没有需要打开的箱子";
            this.ITEM_IDENTIFY = "請選擇要鑑定的物品";
            this.ITEM_IDENTIFY_NO_NEED = "没有需要鑑定的物品";
            this.ITEM_IDENTIFY_RESULT = "鑑定結果: {0} -> {1}";
            this.ITEM_UNIDENTIFIED_NONE = "雜貨";
            this.ITEM_UNIDENTIFIED_HELM = "頭盔";
            this.ITEM_UNIDENTIFIED_ACCE_HEAD = "頭飾";
            this.ITEM_UNIDENTIFIED_ACCE_FACE0 = "臉部裝飾 1";
            this.ITEM_UNIDENTIFIED_ACCE_FACE1 = "臉部裝飾 2";
            this.ITEM_UNIDENTIFIED_ACCE_FACE2 = "臉部裝飾 3";
            this.ITEM_UNIDENTIFIED_ACCE_NECK = "項鏈";
            this.ITEM_UNIDENTIFIED_ACCE_FINGER = "戒指";
            this.ITEM_UNIDENTIFIED_ARMOR_UPPER = "上身防具";
            this.ITEM_UNIDENTIFIED_ARMOR_LOWER = "下身防具";
            this.ITEM_UNIDENTIFIED_ONEPIECE = "連身裙";
            this.ITEM_UNIDENTIFIED_OVERALLS = "工作褲";
            this.ITEM_UNIDENTIFIED_BODYSUIT = "全身套裝";
            this.ITEM_UNIDENTIFIED_FACEBODYSUIT = "全身套裝";
            this.ITEM_UNIDENTIFIED_BACKPACK = "背囊";
            this.ITEM_UNIDENTIFIED_COAT = "外套";
            this.ITEM_UNIDENTIFIED_SOCKS = "襪子";
            this.ITEM_UNIDENTIFIED_BOOTS = "靴子";
            this.ITEM_UNIDENTIFIED_SLACKS = "下衣";
            this.ITEM_UNIDENTIFIED_LONGBOOTS = "長靴";
            this.ITEM_UNIDENTIFIED_HALFBOOTS = "半長靴";
            this.ITEM_UNIDENTIFIED_FULLFACE = "面具";
            this.ITEM_UNIDENTIFIED_SHORT_SWORD = "短刀";
            this.ITEM_UNIDENTIFIED_SWORD = "劍";
            this.ITEM_UNIDENTIFIED_RAPIER = "幼長劍";
            this.ITEM_UNIDENTIFIED_CLAW = "棒子";
            this.ITEM_UNIDENTIFIED_KNUCKLE = "關節";
            this.ITEM_UNIDENTIFIED_SHIELD = "盾牌";
            this.ITEM_UNIDENTIFIED_HAMMER = "鐵鎚";
            this.ITEM_UNIDENTIFIED_AXE = "斧頭";
            this.ITEM_UNIDENTIFIED_SPEAR = "槍";
            this.ITEM_UNIDENTIFIED_STAFF = "魔杖";
            this.ITEM_UNIDENTIFIED_THROW = "投擲物";
            this.ITEM_UNIDENTIFIED_BOW = "弓";
            this.ITEM_UNIDENTIFIED_ARROW = "箭";
            this.ITEM_UNIDENTIFIED_GUN = "槍";
            this.ITEM_UNIDENTIFIED_BULLET = "子彈";
            this.ITEM_UNIDENTIFIED_HANDBAG = "手提包";
            this.ITEM_UNIDENTIFIED_LEFT_HANDBAG = "手提包(左)";
            this.ITEM_UNIDENTIFIED_BOOK = "書";
            this.ITEM_UNIDENTIFIED_INSTRUMENT = "樂器";
            this.ITEM_UNIDENTIFIED_ROPE = "繩子";
            this.ITEM_UNIDENTIFIED_CARD = "卡";
            this.ITEM_UNIDENTIFIED_ETC_WEAPON = "???";
            this.ITEM_UNIDENTIFIED_SHOES = "鞋";
            this.ITEM_UNIDENTIFIED_MONEY = "金幣";
            this.ITEM_UNIDENTIFIED_FOOD = "食物";
            this.ITEM_UNIDENTIFIED_POTION = "藥水";
            this.ITEM_UNIDENTIFIED_MARIONETTE = "活動木偶";
            this.ITEM_UNIDENTIFIED_GOLEM = "石像";
            this.ITEM_UNIDENTIFIED_TREASURE_BOX = "寶物箱";
            this.ITEM_UNIDENTIFIED_CONTAINER = "集裝箱";
            this.ITEM_UNIDENTIFIED_TIMBER_BOX = "木箱";
            this.ITEM_UNIDENTIFIED_SEED = "種子";
            this.ITEM_UNIDENTIFIED_SCROLL = "捲軸";
            this.ITEM_UNIDENTIFIED_SKILLBOOK = "技能書";
            this.ITEM_UNIDENTIFIED_PET = "寵物";
            this.ITEM_UNIDENTIFIED_PET_NEKOMATA = "凱堤";
            this.ITEM_UNIDENTIFIED_PET_YOUHEI = "傭兵";
            this.ITEM_UNIDENTIFIED_BACK_DEMON = "凱堤";
            this.ITEM_UNIDENTIFIED_RIDE_PET = "騎乘寵物";
            this.ITEM_UNIDENTIFIED_USE = "道具";
            this.ITEM_UNIDENTIFIED_PETFOOD = "寵物食物";
            this.ITEM_UNIDENTIFIED_STAMP = "印章";
            this.ITEM_UNIDENTIFIED_FG_FURNITURE = "傢俱";
            this.ITEM_UNIDENTIFIED_FG_BASEBUILD = "部件";
            this.ITEM_UNIDENTIFIED_FG_ROOM_WALL = "牆紙";
            this.ITEM_UNIDENTIFIED_FG_ROOM_FLOOR = "地板";
            this.ITEM_UNIDENTIFIED_ITDGN = "不知名的東西";
            this.ITEM_UNIDENTIFIED_ROBOT_GROW = "強化部件";
            this.ITEM_UNIDENTIFIED_COSTUME = "特殊服裝";


            this.FG_NAME = "這是{0}的飛空庭";
            this.FG_NOT_FOUND = "沒有飛空庭";
            this.FG_ALREADY_CALLED = "已經召喚了飛空庭";
            this.FG_CANNOT = "在這裡無法召喚飛空庭";
            this.FG_FUTNITURE_SETUP = "{0} 已放置 ({1}/{2}个)";
            this.FG_FUTNITURE_REMOVE = "{0} 已拆除 ({1}/{2}个)";
            this.FG_FUTNITURE_MAX = "不能再設置道具";

            this.ITD_HOUR = "小時";
            this.ITD_MINUTE = "分鍾";
            this.ITD_SECOND = "秒";
            this.ITD_CRASHING = "這個地牢 {0} 後會崩潰";
            this.ITD_CREATED = "的地牢形成了";
            this.ITD_PARTY_DISMISSED = "隊伍解散了，這個地牢會消失";
            this.ITD_QUEST_CANCEL = "製作者取消了任務，這個地牢會消失";
            this.ITD_SELECT_DUUNGEON = "請選擇要進去的地牢";
            this.ITD_DUNGEON_NAME = " 的地牢";

            this.THEATER_WELCOME = "歡迎光臨電影院！";
            this.THEATER_COUNTDOWN = "{0} 將于 {1} 分鐘后開始播放";

            this.NPC_SHOP_CP_GET = "得到 {0} CP.";
            this.NPC_SHOP_ECOIN_GET = "得到 {0} ecoin";
            this.NPC_SHOP_CP_LOST = "失去 {0} CP";
            this.NPC_SHOP_ECOIN_LOST = "失去 {0} ecoin";

            this.WRP_ENTER = "冠軍戰場參戰中";
            this.WRP_GOT = "取得 {0}點WRP";
            this.WRP_LOST = "失去 {0}點WRP";
            this.DEATH_PENALTY = "由于死亡懲罰經驗值等降低了";

            this.ODWAR_PREPARE = "DEM正在向{0}进军，预计{1}分钟后到达";
            this.ODWAR_PREPARE2 = "请有能力的勇者们前往支援！";
            this.ODWAR_START = "都市防御战开始了！";
            this.ODWAR_SYMBOL_DOWN = "象征·{0}号机被破坏！！！";
            this.ODWAR_SYMBOL_ACTIVATE = "象征·{0}号机成功地展开了！！！";
            this.ODWAR_LOSE = "西部要塞城被DEM攻陷了！！";
            this.ODWAR_WIN = "西部要塞的防御战胜利了！";
            this.ODWAR_WIN2 = "西部要塞的象征开始展开防御力场！";
            this.ODWAR_WIN3 = "敌军开始从西部要塞撤退！";
            this.ODWAR_WIN4 = "敌军撤退了！我们胜利了！";
            this.ODWAR_CAPTURE = "勇者们成功地夺回了西部要塞城！！";

            this.EP_INCREASE = "离增加EP還有{0}小時";
            this.EP_INCREASED = "EP增加了{0}点";

            this.NPC_ITEM_FUSION_RECHOOSE = "重新選！";
            this.NPC_ITEM_FUSION_CANCEL = "還是算了";
            this.NPC_ITEM_FUSION_CONFIRM = "成功率{1}％ {0}G";

        }

        public override string EnglishName
        {
            get { return "TChinese"; }
        }

        public override string LocalName
        {
            get { return "繁體中文"; }
        }
    }
}
