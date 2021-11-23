using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace XLSTool
{
    public class ItemPPGSettings
    {
        #region Data
        /*public string name, desc;
        public uint id, price;
        public uint iconID;
        public uint imageID;
        public ushort equipVolume, possessionWeight, weight, volume;
        public ItemTypes itemType;
        public int repairItem, enhancementItem;
        public uint events;
        public bool receipt, dye, stock, doubleHand, usable;
        public byte color;
        public ushort durability;
        public PC_JOB jointJob;
        public uint eventID, effectID;
        public ushort activateSkill, possibleSkill, passiveSkill, possessionSkill, possessionPassiveSkill;
        public TargetType target;
        public ActiveType activeType;
        public byte range;
        public uint duration;
        public byte effectRange;
        public bool isRate;
        public uint cast, delay;
        public short hp, mp, sp, weightUp, volumeUp, speedUp;
        public short str, dex, intel, vit, agi, mag, luk, cha;
        public short atk1, atk2, atk3, matk, def, mdef;
        public short hitMelee, hitRanged, hitMagic;
        public short avoidMelee, avoidRanged, avoidMagic;
        public short hitCritical, avoidCritical;
        public short hpRecover, mpRecover;
        public Dictionary<Elements, short> element = new Dictionary<Elements, short>();
        public Dictionary<AbnormalStatus, short> abnormalStatus = new Dictionary<AbnormalStatus, short>();
        public Dictionary<PC_RACE, bool> possibleRace = new Dictionary<PC_RACE, bool>();
        public Dictionary<PC_GENDER, bool> possibleGender = new Dictionary<PC_GENDER, bool>();
        public byte possibleLv;
        public bool possibleRebirth;
        public ushort possibleStr, possibleDex, possibleInt, possibleVit, possibleAgi, possibleMag, possibleLuk, possibleCha;
        public Dictionary<PC_JOB, bool> possibleJob = new Dictionary<PC_JOB, bool>();
        public Dictionary<Country, bool> possibleCountry = new Dictionary<Country, bool>();
        public uint marionetteID, petID;

        public byte currentSlot, maxSlot;

        public byte handMotion, handMotion2;
        public bool noTrade;
        public override string ToString()
        {
            return this.name;
        }*/
        #endregion
        #region Load
        string GetString(int idx)
        {
            if (idx < Paras.Instance.paras.Length)
                return Paras.Instance.paras[idx];
            else
                return "";
        }
        uint GetInteger(int idx)
        {
            if (idx < Paras.Instance.paras.Length)
                return uint.Parse(Paras.Instance.paras[idx]);
            else
                return 0;
        }
        ushort GetIntegerForShort(int idx)
        {
            if (idx < Paras.Instance.paras.Length)
                return ushort.Parse(Paras.Instance.paras[idx]);
            else
                return 0;
        }
        public ItemPPGSettings(string[] paras)
        {
            Paras.Instance.paras = paras;
        }
        private bool toBool(string input)
        {
            if (input == "1") return true; else return false;
        }
        #endregion
        [CategoryAttribute("常规"), DescriptionAttribute("物品的名称")]
        public string 道具名
        {
            get { return GetString(3); }
            set { Paras.Instance.paras[3] = value; }
        }
        [CategoryAttribute("常规"), DescriptionAttribute("给物品设定的ID，不可重复")]
        public uint A_ID
        {
            get { return GetInteger(0); }
            set { Paras.Instance.paras[0] = value.ToString(); }
        }
        [CategoryAttribute("外观"), DescriptionAttribute("物品的外形ID，对应ItemPict")]
        public uint ImageID
        {
            get { return GetInteger(1); }
            set { Paras.Instance.paras[1] = value.ToString(); }
        }
        [CategoryAttribute("外观"), DescriptionAttribute("物品的小图标ID")]
        public uint IconID
        {
            get { return GetInteger(2); }
            set { Paras.Instance.paras[2] = value.ToString(); }
        }
        [CategoryAttribute("基本"), DescriptionAttribute("物品的类型")]
        public ItemTypes 类型
        {
            get { return (ItemTypes)Enum.Parse(typeof(ItemTypes), Paras.Instance.paras[4]); }
            set { Paras.Instance.paras[4] = value.ToString(); }
        }
        [CategoryAttribute("基本"), DescriptionAttribute("物品的价值")]
        public uint 价值
        {
            get { return GetInteger(5); }
            set { Paras.Instance.paras[5] = value.ToString(); }
        }
        [CategoryAttribute("基本"), DescriptionAttribute("物品的重量")]
        public ushort 重量
        {
            get { return (ushort)float.Parse(Paras.Instance.paras[6]); }
            set { Paras.Instance.paras[6] = value.ToString(); }
        }
        [CategoryAttribute("基本"), DescriptionAttribute("物品的体积")]
        public ushort 体积
        {
            get { return (ushort)float.Parse(Paras.Instance.paras[7]); }
            set { Paras.Instance.paras[7] = value.ToString(); }
        }
        [CategoryAttribute("基本"), DescriptionAttribute("物品装备时所占的体积")]
        public ushort 装备时体积
        {
            get { return GetIntegerForShort(8); }
            set { Paras.Instance.paras[8] = value.ToString(); }
        }
        [CategoryAttribute("基本"), DescriptionAttribute("物品装备时所占的重量")]
        public ushort 凭依体重
        {
            get { return GetIntegerForShort(9); }
            set { Paras.Instance.paras[9] = value.ToString(); }
        }
        [CategoryAttribute("扩展"), DescriptionAttribute("修理物品时需要的道具ID")]
        public int 修理道具
        {
            get { return int.Parse(Paras.Instance.paras[10]); }
            set { Paras.Instance.paras[10] = value.ToString(); }
        }
        [CategoryAttribute("扩展")]
        public int 强化ID
        {
            get { return int.Parse(Paras.Instance.paras[11]); }
            set { Paras.Instance.paras[11] = value.ToString(); }
        }
        //paras[12]
        [CategoryAttribute("事件"), DescriptionAttribute("是否使用时触发事件")]
        public uint Events
        {
            get { return GetInteger(13); }
            set { Paras.Instance.paras[13] = value.ToString(); }
        }
        [CategoryAttribute("未知"), DescriptionAttribute("レシピ可")]
        public bool Receipt
        {
            get { return toBool(Paras.Instance.paras[14]); }
            set { if (value)Paras.Instance.paras[14] = "1"; else Paras.Instance.paras[14] = "0"; }
        }

        [CategoryAttribute("其它"), DescriptionAttribute("是否可以染色")]
        public bool 染色
        {
            get { return toBool(Paras.Instance.paras[15]); }
            set { if (value)Paras.Instance.paras[15] = "1"; else Paras.Instance.paras[14] = "0"; }
        }
        [CategoryAttribute("道具"),DescriptionAttribute("该道具是否可以叠加")]
        public bool 叠加
        {
            get { return toBool(Paras.Instance.paras[16]); }
            set { if (value)Paras.Instance.paras[16] = "1"; else Paras.Instance.paras[14] = "0"; }
        }
        [CategoryAttribute("装备属性"), DescriptionAttribute("是否为双手武器")]
        public bool 双手武器
        {
            get { return toBool(Paras.Instance.paras[17]); }
            set { if (value)Paras.Instance.paras[17] = "1"; else Paras.Instance.paras[14] = "0"; }
        }
        [CategoryAttribute("道具"), Description("该道具是否使用后消灭")]
        public bool 使用后消失
        {
            get { return toBool(Paras.Instance.paras[18]); }
            set { if (value)Paras.Instance.paras[18] = "1"; else Paras.Instance.paras[14] = "0"; }
        }
        [CategoryAttribute("未知"), DescriptionAttribute("色")]
        public byte Color
        {
            get { return byte.Parse(Paras.Instance.paras[19]); }
            set { Paras.Instance.paras[19] = value.ToString(); }
        }
        [CategoryAttribute("装备属性"), DescriptionAttribute("设置道具的最大耐久度")]
        public ushort 耐久度
        {
            get { return GetIntegerForShort(20); }
            set { Paras.Instance.paras[20] = value.ToString(); }
        }
        [CategoryAttribute("装备属性"), DescriptionAttribute("设置装备这个物品后改变的职业")]
        public ushort 装备时更变职业ID
        {
            get { return GetIntegerForShort(21); }
            set { Paras.Instance.paras[21] = value.ToString(); }
        }
        [CategoryAttribute("未知"), DescriptionAttribute("初期スロット数")]
        public byte CurrentSlot
        {
            get { return byte.Parse(Paras.Instance.paras[22]); }
            set { Paras.Instance.paras[22] = value.ToString(); }
        }
        [CategoryAttribute("未知"), DescriptionAttribute("アイテムチェンジ可否フラグ")]
        public byte MaxSlot
        {
            get { return byte.Parse(Paras.Instance.paras[23]); }
            set { Paras.Instance.paras[23] = value.ToString(); }
        }
        [CategoryAttribute("事件"), DescriptionAttribute("设置使用后触发的脚本ID")]
        public uint EventID
        {
            get { return GetInteger(24); }
            set { Paras.Instance.paras[24] = value.ToString(); }
        }
        [CategoryAttribute("事件"), DescriptionAttribute("设置使用后显示的效果ID")]
        public uint EffectID
        {
            get { return GetInteger(25); }
            set { Paras.Instance.paras[25] = value.ToString(); }
        }
        [CategoryAttribute("事件"), DescriptionAttribute("设置使用后出现的技能ID")]
        public ushort 触发技能
        {
            get { return GetIntegerForShort(26); }
            set { Paras.Instance.paras[26] = value.ToString(); }
        }
        [CategoryAttribute("装备属性"), DescriptionAttribute("设置装备后给予玩家的主动技能")]
        public ushort PossibleSkill
        {
            get { return GetIntegerForShort(27); }
            set { Paras.Instance.paras[27] = value.ToString(); }
        }
        [CategoryAttribute("装备属性"), DescriptionAttribute("设置装备后给予玩家的被动技能")]
        public ushort PossiveSkill
        {
            get { return GetIntegerForShort(28); }
            set { Paras.Instance.paras[28] = value.ToString(); }
        }
        [CategoryAttribute("装备属性"), DescriptionAttribute("设置装备后给予玩家的主动技能")]
        public ushort PossessionSkill
        {
            get { return GetIntegerForShort(29); }
            set { Paras.Instance.paras[29] = value.ToString(); }
        }
        [CategoryAttribute("装备属性"), DescriptionAttribute("设置装备后给予玩家的被动技能")]
        public ushort PossessionPassiveSkill
        {
            get { return GetIntegerForShort(30); }
            set { Paras.Instance.paras[30] = value.ToString(); }
        }
        [CategoryAttribute("使用"), DescriptionAttribute("选择使用后的目标效果")]
        public TargetType TargetType
        {
            get { return (TargetType)Enum.Parse(typeof(TargetType), Paras.Instance.paras[31]); }
            set { Paras.Instance.paras[31] = value.ToString(); }
        }
        [CategoryAttribute("使用"), DescriptionAttribute("选择使用后的自身效果")]
        public ActiveType ActiveType
        {
            get { return (ActiveType)Enum.Parse(typeof(ActiveType), Paras.Instance.paras[32]); }
            set { Paras.Instance.paras[32] = value.ToString(); }
        }
        [CategoryAttribute("使用"), DescriptionAttribute("设置使用射程")]
        public byte 射程
        {
            get { return byte.Parse(Paras.Instance.paras[33]); }
            set { Paras.Instance.paras[33] = value.ToString(); }
        }
        [CategoryAttribute("使用")]
        public uint 效果时间
        {
            get { return GetInteger(34); }
            set { Paras.Instance.paras[34] = value.ToString(); }
        }
        [CategoryAttribute("使用")]
        public byte 效果范围
        {
            get { return byte.Parse(Paras.Instance.paras[35]); }
            set { Paras.Instance.paras[35] = value.ToString(); }
        }
        [CategoryAttribute("使用"), DescriptionAttribute("効果値")]
        public bool IsRate
        {
            get { return toBool(Paras.Instance.paras[36]); }
            set { if (value)Paras.Instance.paras[36] = "1"; else Paras.Instance.paras[36] = "0"; }
        }
        [CategoryAttribute("使用"), DescriptionAttribute("设置道具使用时的吟唱时间")]
        public uint 吟唱时间
        {
            get { return GetInteger(37); }
            set { Paras.Instance.paras[37] = value.ToString(); }
        }
        [CategoryAttribute("使用"), DescriptionAttribute("设置道具使用后的CD")]
        public uint 冷却时间
        {
            get { return GetInteger(38); }
            set { Paras.Instance.paras[38] = value.ToString(); }
        }
        [CategoryAttribute("属性-基本")]
        public short Hp
        {
            get { return short.Parse(Paras.Instance.paras[39]); }
            set { Paras.Instance.paras[39] = value.ToString(); }
        }
        [CategoryAttribute("属性-基本")]
        public short Mp
        {
            get { return short.Parse(Paras.Instance.paras[40]); }
            set { Paras.Instance.paras[40] = value.ToString(); }
        }
        [CategoryAttribute("属性-基本")]
        public short Sp
        {
            get { return short.Parse(Paras.Instance.paras[41]); }
            set { Paras.Instance.paras[41] = value.ToString(); }
        }
        [CategoryAttribute("属性"), DescriptionAttribute("上升体重")]
        public short WeightUp
        {
            get { return short.Parse(Paras.Instance.paras[42]); }
            set { Paras.Instance.paras[42] = value.ToString(); }
        }
        [CategoryAttribute("属性"), DescriptionAttribute("上升体积")]
        public short VolumeUp
        {
            get { return short.Parse(Paras.Instance.paras[43]); }
            set { Paras.Instance.paras[43] = value.ToString(); }
        }
        [CategoryAttribute("属性"), DescriptionAttribute("上升速度")]
        public short SpeedUp
        {
            get { return short.Parse(Paras.Instance.paras[44]); }
            set { Paras.Instance.paras[44] = value.ToString(); }
        }
        [CategoryAttribute("属性-素质")]
        public short STR
        {
            get { return short.Parse(Paras.Instance.paras[45]); }
            set { Paras.Instance.paras[45] = value.ToString(); }
        }
        [CategoryAttribute("属性-素质")]
        public short MAG
        {
            get { return short.Parse(Paras.Instance.paras[46]); }
            set { Paras.Instance.paras[46] = value.ToString(); }
        }
        [CategoryAttribute("属性-素质")]
        public short VIT
        {
            get { return short.Parse(Paras.Instance.paras[47]); }
            set { Paras.Instance.paras[47] = value.ToString(); }
        }
        [CategoryAttribute("属性-素质")]
        public short DEX
        {
            get { return short.Parse(Paras.Instance.paras[48]); }
            set { Paras.Instance.paras[48] = value.ToString(); }
        }
        [CategoryAttribute("属性-素质")]
        public short AGI
        {
            get { return short.Parse(Paras.Instance.paras[49]); }
            set { Paras.Instance.paras[49] = value.ToString(); }
        }
        [CategoryAttribute("属性-素质")]
        public short INT
        {
            get { return short.Parse(Paras.Instance.paras[50]); }
            set { Paras.Instance.paras[50] = value.ToString(); }
        }
        [CategoryAttribute("属性")]
        public short LUK
        {
            get { return short.Parse(Paras.Instance.paras[51]); }
            set { Paras.Instance.paras[511] = value.ToString(); }
        }
        [CategoryAttribute("属性")]
        public short CHA
        {
            get { return short.Parse(Paras.Instance.paras[52]); }
            set { Paras.Instance.paras[52] = value.ToString(); }
        }
        [CategoryAttribute("属性-攻击")]
        public short ATK1
        {
            get { return short.Parse(Paras.Instance.paras[53]); }
            set { Paras.Instance.paras[53] = value.ToString(); }
        }
        [CategoryAttribute("属性-攻击")]
        public short ATK2
        {
            get { return short.Parse(Paras.Instance.paras[54]); }
            set { Paras.Instance.paras[54] = value.ToString(); }
        }
        [CategoryAttribute("属性-攻击")]
        public short ATK3
        {
            get { return short.Parse(Paras.Instance.paras[55]); }
            set { Paras.Instance.paras[55] = value.ToString(); }
        }
        [CategoryAttribute("属性-攻击")]
        public short MATK
        {
            get { return short.Parse(Paras.Instance.paras[56]); }
            set { Paras.Instance.paras[56] = value.ToString(); }
        }
        [CategoryAttribute("属性-防御")]
        public short DEF
        {
            get { return short.Parse(Paras.Instance.paras[57]); }
            set { Paras.Instance.paras[57] = value.ToString(); }
        }
        [CategoryAttribute("属性-防御")]
        public short MDEF
        {
            get { return short.Parse(Paras.Instance.paras[58]); }
            set { Paras.Instance.paras[58] = value.ToString(); }
        }
        [CategoryAttribute("属性-攻击"), DescriptionAttribute("近战命中力")]
        public short HitMelee
        {
            get { return short.Parse(Paras.Instance.paras[59]); }
            set { Paras.Instance.paras[59] = value.ToString(); }
        }
        [CategoryAttribute("属性-攻击"), DescriptionAttribute("远程命中力")]
        public short HitRanged
        {
            get { return short.Parse(Paras.Instance.paras[60]); }
            set { Paras.Instance.paras[60] = value.ToString(); }
        }
        [CategoryAttribute("属性-攻击"), DescriptionAttribute("魔法命中力")]
        public short HitMagic
        {
            get { return short.Parse(Paras.Instance.paras[61]); }
            set { Paras.Instance.paras[61] = value.ToString(); }
        }
        [CategoryAttribute("属性-防御"), DescriptionAttribute("近战闪避")]
        public short AvoidMelee
        {
            get { return short.Parse(Paras.Instance.paras[62]); }
            set { Paras.Instance.paras[62] = value.ToString(); }
        }
        [CategoryAttribute("属性-防御"), DescriptionAttribute("远程闪避")]
        public short AvoidRanged
        {
            get { return short.Parse(Paras.Instance.paras[63]); }
            set { Paras.Instance.paras[63] = value.ToString(); }
        }
        [CategoryAttribute("属性-防御"), DescriptionAttribute("魔法闪避")]
        public short AvoidMagic
        {
            get { return short.Parse(Paras.Instance.paras[64]); }
            set { Paras.Instance.paras[64] = value.ToString(); }
        }
        [CategoryAttribute("属性-攻击"), DescriptionAttribute("暴击力")]
        public short HitCritical
        {
            get { return short.Parse(Paras.Instance.paras[65]); }
            set { Paras.Instance.paras[65] = value.ToString(); }
        }
        [CategoryAttribute("属性-防御"), DescriptionAttribute("暴击回避")]
        public short AvoidCritical
        {
            get { return short.Parse(Paras.Instance.paras[66]); }
            set { Paras.Instance.paras[66] = value.ToString(); }
        }
        [CategoryAttribute("属性"), DescriptionAttribute("回复力")]
        public short HpRecover
        {
            get { return short.Parse(Paras.Instance.paras[67]); }
            set { Paras.Instance.paras[67] = value.ToString(); }
        }
        [CategoryAttribute("属性"), DescriptionAttribute("魔法回复力")]
        public short MpRecover
        {
            get { return short.Parse(Paras.Instance.paras[68]); }
            set { Paras.Instance.paras[68] = value.ToString(); }
        }
        [CategoryAttribute("属性-克制")]
        public short 无
        {
            get { return short.Parse(Paras.Instance.paras[69]); }
            set { Paras.Instance.paras[69] = value.ToString(); }
        }
        [CategoryAttribute("属性-克制")]
        public short 火
        {
            get { return short.Parse(Paras.Instance.paras[70]); }
            set { Paras.Instance.paras[70] = value.ToString(); }
        }
        [CategoryAttribute("属性-克制")]
        public short 水
        {
            get { return short.Parse(Paras.Instance.paras[71]); }
            set { Paras.Instance.paras[71] = value.ToString(); }
        }
        [CategoryAttribute("属性-克制")]
        public short 风
        {
            get { return short.Parse(Paras.Instance.paras[72]); }
            set { Paras.Instance.paras[72] = value.ToString(); }
        }
        [CategoryAttribute("属性-克制")]
        public short 地
        {
            get { return short.Parse(Paras.Instance.paras[73]); }
            set { Paras.Instance.paras[73] = value.ToString(); }
        }
        [CategoryAttribute("属性-克制")]
        public short 光
        {
            get { return short.Parse(Paras.Instance.paras[74]); }
            set { Paras.Instance.paras[74] = value.ToString(); }
        }
        [CategoryAttribute("属性-克制")]
        public short 暗
        {
            get { return short.Parse(Paras.Instance.paras[75]); }
            set { Paras.Instance.paras[75] = value.ToString(); }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 中毒抵抗
        {
            get { return short.Parse(Paras.Instance.paras[76]); }
            set { Paras.Instance.paras[76] = value.ToString(); }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 石化抵抗
        {
            get { return short.Parse(Paras.Instance.paras[77]); }
            set { Paras.Instance.paras[77] = value.ToString(); }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 麻痹抵抗
        {
            get { return short.Parse(Paras.Instance.paras[78]); }
            set { Paras.Instance.paras[78] = value.ToString(); }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 睡眠抵抗
        {
            get { return short.Parse(Paras.Instance.paras[79]); }
            set { Paras.Instance.paras[79] = value.ToString(); }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 沉默抵抗
        {
            get { return short.Parse(Paras.Instance.paras[80]); }
            set { Paras.Instance.paras[80] = value.ToString(); }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 鈍足抵抗
        {
            get { return short.Parse(Paras.Instance.paras[81]); }
            set { Paras.Instance.paras[81] = value.ToString(); }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 混乱抵抗
        {
            get { return short.Parse(Paras.Instance.paras[82]); }
            set { Paras.Instance.paras[82] = value.ToString(); }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 冰冻抵抗
        {
            get { return short.Parse(Paras.Instance.paras[83]); }
            set { Paras.Instance.paras[83] = value.ToString(); }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 昏厥抵抗
        {
            get { return short.Parse(Paras.Instance.paras[84]); }
            set { Paras.Instance.paras[84] = value.ToString(); }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("种族条件-埃米尔")]
        public bool EMIL
        {
            get { return toBool(Paras.Instance.paras[85]); }
            set { if (value)Paras.Instance.paras[85] = "1"; else Paras.Instance.paras[85] = "0"; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("种族条件-塔尼亚")]
        public bool TITANIA
        {
            get { return toBool(Paras.Instance.paras[86]); }
            set { if (value)Paras.Instance.paras[86] = "1"; else Paras.Instance.paras[86] = "0"; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("种族条件-道米尼")]
        public bool DOMINION
        {
            get { return toBool(Paras.Instance.paras[87]); }
            set { if (value)Paras.Instance.paras[87] = "1"; else Paras.Instance.paras[87] = "0"; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("种族条件-DEM")]
        public bool DEM
        {
            get { return toBool(Paras.Instance.paras[88]); }
            set { if (value)Paras.Instance.paras[88] = "1"; else Paras.Instance.paras[88] = "0"; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("性别条件")]
        public bool 男
        {
            get { return toBool(Paras.Instance.paras[89]); }
            set { if (value)Paras.Instance.paras[89] = "1"; else Paras.Instance.paras[89] = "0"; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("性别条件")]
        public bool 女
        {
            get { return toBool(Paras.Instance.paras[90]); }
            set { if (value)Paras.Instance.paras[90] = "1"; else Paras.Instance.paras[90] = "0"; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("等级条件")]
        public byte 需求等级
        {
            get { return byte.Parse(Paras.Instance.paras[91]); }
            set { Paras.Instance.paras[91] = value.ToString(); }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("转生要求条件")]
        public bool 需求转生
        {
            get { return toBool(Paras.Instance.paras[92]); }
            set { if (value)Paras.Instance.paras[92] = "1"; else Paras.Instance.paras[92] = "0"; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort 需求力量
        {
            get { return GetIntegerForShort(93); }
            set { Paras.Instance.paras[93] = value.ToString(); }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort 需求魔法
        {
            get { return GetIntegerForShort(94); }
            set { Paras.Instance.paras[94] = value.ToString(); }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort 需求体力
        {
            get { return GetIntegerForShort(95); }
            set { Paras.Instance.paras[95] = value.ToString(); }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort 需求DEX
        {
            get { return GetIntegerForShort(96); }
            set { Paras.Instance.paras[96] = value.ToString(); }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort 需求AGI
        {
            get { return GetIntegerForShort(97); }
            set { Paras.Instance.paras[97] = value.ToString(); }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort 需求智力
        {
            get { return GetIntegerForShort(98); }
            set { Paras.Instance.paras[98] = value.ToString(); }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort PossibleLuk
        {
            get { return GetIntegerForShort(99); }
            set { Paras.Instance.paras[99] = value.ToString(); }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort PossibleCha
        {
            get { return GetIntegerForShort(100); }
            set { Paras.Instance.paras[100] = value.ToString(); }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("初学者")]
        public bool A初学者
        {
            get { return toBool(Paras.Instance.paras[101]); }
            set { if (value)Paras.Instance.paras[101] = "1"; else Paras.Instance.paras[101] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("剑士")]
        public bool B剑士
        {
            get { return toBool(Paras.Instance.paras[102]); }
            set { if (value)Paras.Instance.paras[102] = "1"; else Paras.Instance.paras[102] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("剑客")]
        public bool B剑客
        {
            get { return toBool(Paras.Instance.paras[103]); }
            set { if (value)Paras.Instance.paras[103] = "1"; else Paras.Instance.paras[103] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("赏金猎人")]
        public bool B赏金猎人
        {
            get { return toBool(Paras.Instance.paras[104]); }
            set { if (value)Paras.Instance.paras[104] = "1"; else Paras.Instance.paras[104] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("剑斗士")]
        public bool B剑斗士
        {
            get { return toBool(Paras.Instance.paras[138]); }
            set { if (value)Paras.Instance.paras[138] = "1"; else Paras.Instance.paras[138] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("骑士")]
        public bool C骑士
        {
            get { return toBool(Paras.Instance.paras[105]); }
            set { if (value)Paras.Instance.paras[105] = "1"; else Paras.Instance.paras[105] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("圣骑士")]
        public bool C圣骑士
        {
            get { return toBool(Paras.Instance.paras[106]); }
            set { if (value)Paras.Instance.paras[106] = "1"; else Paras.Instance.paras[106] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("暗黑骑士")]
        public bool C暗黑骑士
        {
            get { return toBool(Paras.Instance.paras[107]); }
            set { if (value)Paras.Instance.paras[107] = "1"; else Paras.Instance.paras[107] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("守护者")]
        public bool C守护者
        {
            get { return toBool(Paras.Instance.paras[139]); }
            set { if (value)Paras.Instance.paras[139] = "1"; else Paras.Instance.paras[139] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("盗贼")]
        public bool D盗贼
        {
            get { return toBool(Paras.Instance.paras[108]); }
            set { if (value)Paras.Instance.paras[108] = "1"; else Paras.Instance.paras[108] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("刺客")]
        public bool D刺客
        {
            get { return toBool(Paras.Instance.paras[109]); }
            set { if (value)Paras.Instance.paras[109] = "1"; else Paras.Instance.paras[109] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("特工")]
        public bool D特工
        {
            get { return toBool(Paras.Instance.paras[110]); }
            set { if (value)Paras.Instance.paras[110] = "1"; else Paras.Instance.paras[110] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("肃清者")]
        public bool D肃静者
        {
            get { return toBool(Paras.Instance.paras[140]); }
            set { if (value)Paras.Instance.paras[140] = "1"; else Paras.Instance.paras[140] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("弓手")]
        public bool E弓手
        {
            get { return toBool(Paras.Instance.paras[111]); }
            set { if (value)Paras.Instance.paras[111] = "1"; else Paras.Instance.paras[111] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("猎人")]
        public bool E猎人
        {
            get { return toBool(Paras.Instance.paras[112]); }
            set { if (value)Paras.Instance.paras[112] = "1"; else Paras.Instance.paras[112] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("神枪手")]
        public bool E神枪手
        {
            get { return toBool(Paras.Instance.paras[113]); }
            set { if (value)Paras.Instance.paras[113] = "1"; else Paras.Instance.paras[113] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("隼人")]
        public bool E隼人
        {
            get { return toBool(Paras.Instance.paras[141]); }
            set { if (value)Paras.Instance.paras[141] = "1"; else Paras.Instance.paras[141] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("魔法师")]
        public bool F魔法师
        {
            get { return toBool(Paras.Instance.paras[114]); }
            set { if (value)Paras.Instance.paras[114] = "1"; else Paras.Instance.paras[114] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("魔导师")]
        public bool F魔导师
        {
            get { return toBool(Paras.Instance.paras[115]); }
            set { if (value)Paras.Instance.paras[115] = "1"; else Paras.Instance.paras[115] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("贤者")]
        public bool F贤者
        {
            get { return toBool(Paras.Instance.paras[116]); }
            set { if (value)Paras.Instance.paras[116] = "1"; else Paras.Instance.paras[116] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("魔动大师")]
        public bool F魔动大师
        {
            get { return toBool(Paras.Instance.paras[142]); }
            set { if (value)Paras.Instance.paras[142] = "1"; else Paras.Instance.paras[142] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("元素")]
        public bool G元素
        {
            get { return toBool(Paras.Instance.paras[117]); }
            set { if (value)Paras.Instance.paras[117] = "1"; else Paras.Instance.paras[117] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("精灵使")]
        public bool G精灵使
        {
            get { return toBool(Paras.Instance.paras[118]); }
            set { if (value)Paras.Instance.paras[118] = "1"; else Paras.Instance.paras[118] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("巫师")]
        public bool G无视
        {
            get { return toBool(Paras.Instance.paras[119]); }
            set { if (value)Paras.Instance.paras[119] = "1"; else Paras.Instance.paras[119] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("星术士")]
        public bool G星术士
        {
            get { return toBool(Paras.Instance.paras[143]); }
            set { if (value)Paras.Instance.paras[143] = "1"; else Paras.Instance.paras[143] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("祭司")]
        public bool H祭司
        {
            get { return toBool(Paras.Instance.paras[120]); }
            set { if (value)Paras.Instance.paras[120] = "1"; else Paras.Instance.paras[120] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("神官")]
        public bool H神官
        {
            get { return toBool(Paras.Instance.paras[121]); }
            set { if (value)Paras.Instance.paras[121] = "1"; else Paras.Instance.paras[121] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("吟游诗人")]
        public bool H吟游诗人
        {
            get { return toBool(Paras.Instance.paras[122]); }
            set { if (value)Paras.Instance.paras[122] = "1"; else Paras.Instance.paras[122] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("主教")]
        public bool H主教
        {
            get { return toBool(Paras.Instance.paras[144]); }
            set { if (value)Paras.Instance.paras[144] = "1"; else Paras.Instance.paras[144] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("魔攻师")]
        public bool I魔攻师
        {
            get { return toBool(Paras.Instance.paras[123]); }
            set { if (value)Paras.Instance.paras[123] = "1"; else Paras.Instance.paras[123] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("暗术师")]
        public bool I暗术师
        {
            get { return toBool(Paras.Instance.paras[124]); }
            set { if (value)Paras.Instance.paras[124] = "1"; else Paras.Instance.paras[124] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("死灵")]
        public bool I死灵
        {
            get { return toBool(Paras.Instance.paras[125]); }
            set { if (value)Paras.Instance.paras[125] = "1"; else Paras.Instance.paras[125] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("狩魂者")]
        public bool I狩魂者
        {
            get { return toBool(Paras.Instance.paras[145]); }
            set { if (value)Paras.Instance.paras[145] = "1"; else Paras.Instance.paras[145] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("矿工")]
        public bool J矿工
        {
            get { return toBool(Paras.Instance.paras[126]); }
            set { if (value)Paras.Instance.paras[126] = "1"; else Paras.Instance.paras[126] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("铁匠")]
        public bool J铁匠
        {
            get { return toBool(Paras.Instance.paras[127]); }
            set { if (value)Paras.Instance.paras[127] = "1"; else Paras.Instance.paras[127] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("机械师")]
        public bool J机械师
        {
            get { return toBool(Paras.Instance.paras[128]); }
            set { if (value)Paras.Instance.paras[128] = "1"; else Paras.Instance.paras[128] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("艺术家")]
        public bool J艺术家
        {
            get { return toBool(Paras.Instance.paras[146]); }
            set { if (value)Paras.Instance.paras[146] = "1"; else Paras.Instance.paras[146] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("农夫")]
        public bool K农夫
        {
            get { return toBool(Paras.Instance.paras[129]); }
            set { if (value)Paras.Instance.paras[129] = "1"; else Paras.Instance.paras[129] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("炼金术师")]
        public bool K炼金术师
        {
            get { return toBool(Paras.Instance.paras[130]); }
            set { if (value)Paras.Instance.paras[130] = "1"; else Paras.Instance.paras[130] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("木偶使")]
        public bool K木偶师
        {
            get { return toBool(Paras.Instance.paras[131]); }
            set { if (value)Paras.Instance.paras[131] = "1"; else Paras.Instance.paras[131] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("收获者")]
        public bool K收获者
        {
            get { return toBool(Paras.Instance.paras[147]); }
            set { if (value)Paras.Instance.paras[147] = "1"; else Paras.Instance.paras[147] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("冒险家")]
        public bool L冒险家
        {
            get { return toBool(Paras.Instance.paras[132]); }
            set { if (value)Paras.Instance.paras[132] = "1"; else Paras.Instance.paras[132] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("探险家")]
        public bool L探险家
        {
            get { return toBool(Paras.Instance.paras[133]); }
            set { if (value)Paras.Instance.paras[133] = "1"; else Paras.Instance.paras[133] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("考古学家")]
        public bool L考古学家
        {
            get { return toBool(Paras.Instance.paras[134]); }
            set { if (value)Paras.Instance.paras[134] = "1"; else Paras.Instance.paras[134] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("风行者")]
        public bool L风行者
        {
            get { return toBool(Paras.Instance.paras[148]); }
            set { if (value)Paras.Instance.paras[148] = "1"; else Paras.Instance.paras[148] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("商人")]
        public bool M商人
        {
            get { return toBool(Paras.Instance.paras[135]); }
            set { if (value)Paras.Instance.paras[135] = "1"; else Paras.Instance.paras[135] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("贸易商")]
        public bool M贸易商
        {
            get { return toBool(Paras.Instance.paras[136]); }
            set { if (value)Paras.Instance.paras[136] = "1"; else Paras.Instance.paras[136] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("赌徒")]
        public bool M赌徒
        {
            get { return toBool(Paras.Instance.paras[137]); }
            set { if (value)Paras.Instance.paras[137] = "1"; else Paras.Instance.paras[137] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("赌圣")]
        public bool M赌圣
        {
            get { return toBool(Paras.Instance.paras[149]); }
            set { if (value)Paras.Instance.paras[149] = "1"; else Paras.Instance.paras[149] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("小丑")]
        public bool N小丑
        {
            get { return toBool(Paras.Instance.paras[150]); }
            set { if (value)Paras.Instance.paras[150] = "1"; else Paras.Instance.paras[150] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("驯兽师")]
        public bool O驯兽师
        {
            get { return toBool(Paras.Instance.paras[159]); }
            set { if (value)Paras.Instance.paras[159] = "1"; else Paras.Instance.paras[159] = "0"; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("庭院师")]
        public bool P庭院师
        {
            get { return toBool(Paras.Instance.paras[160]); }
            set { if (value)Paras.Instance.paras[160] = "1"; else Paras.Instance.paras[160] = "0"; }
        }
        //161-164
        [CategoryAttribute("扩展"), DescriptionAttribute("变身成活动木偶的ID")]
        public uint MarionetteID
        {
            get { return GetInteger(166); }
            set { Paras.Instance.paras[166] = value.ToString(); }
        }
        [CategoryAttribute("扩展"), DescriptionAttribute("召唤出宠物的ID")]
        public uint PetID
        {
            get { return GetInteger(167); }
            set { Paras.Instance.paras[167] = value.ToString(); }
        }
        [CategoryAttribute("扩展"), DescriptionAttribute("单手动作")]
        public byte HandMotion
        {
            get { return byte.Parse(Paras.Instance.paras[168]); }
            set { Paras.Instance.paras[168] = value.ToString(); }
        }
        [CategoryAttribute("扩展"), DescriptionAttribute("双手动作")]
        public byte HandMotion2
        {
            get { return byte.Parse(Paras.Instance.paras[169]); }
            set { Paras.Instance.paras[169] = value.ToString(); }
        }
        //170-173
        [CategoryAttribute("常规")]
        public string 简介
        {
            get { return Paras.Instance.paras[174]; }
            set { Paras.Instance.paras[174] = value; }
        }
        //175
        [CategoryAttribute("扩展"), DescriptionAttribute("道具是否可以交易")]
        public bool NoTrade
        {
            get { return toBool(Paras.Instance.paras[176]); }
            set { if (value)Paras.Instance.paras[176] = "1"; else Paras.Instance.paras[176] = "0"; }
        }
    }
}
