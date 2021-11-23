using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace XLSTool
{
    public class PPGSettings
    {
        #region Data
        public string name, desc;
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
        }
        #endregion
        #region Load
        public PPGSettings(string[] paras)
        {
            id = uint.Parse(paras[0]);
            imageID = uint.Parse(paras[1]);
            iconID = uint.Parse(paras[2]);
            name = paras[3];
            itemType = (ItemTypes)Enum.Parse(typeof(ItemTypes), paras[4]);
            price = uint.Parse(paras[5]);
            weight = (ushort)float.Parse(paras[6]);
            volume = (ushort)float.Parse(paras[7]);
            equipVolume = ushort.Parse(paras[8]);
            possessionWeight = ushort.Parse(paras[9]);
            repairItem = (int)int.Parse(paras[10]);
            enhancementItem = (int)int.Parse(paras[11]);
            events = uint.Parse(paras[13]);
            receipt = toBool(paras[14]);
            dye = toBool(paras[15]);
            stock = toBool(paras[16]);
            doubleHand = toBool(paras[17]);
            usable = toBool(paras[18]);
            color = byte.Parse(paras[19]);
            durability = ushort.Parse(paras[20]);
            if (paras[21] != "0")
            {
                jointJob = (PC_JOB)(int.Parse(paras[21]) + 1000);
            }
            else
                jointJob = PC_JOB.NONE;
            currentSlot = byte.Parse(paras[22]);
            maxSlot = byte.Parse(paras[23]);
            eventID = uint.Parse(paras[24]);
            effectID = uint.Parse(paras[25]);
            activateSkill = ushort.Parse(paras[26]);
            possibleSkill = ushort.Parse(paras[27]);
            passiveSkill = ushort.Parse(paras[28]);
            possessionSkill = ushort.Parse(paras[29]);
            possessionPassiveSkill = ushort.Parse(paras[30]);
            target = (TargetType)Enum.Parse(typeof(TargetType), paras[31]);
            activeType = (ActiveType)Enum.Parse(typeof(ActiveType), paras[32]);
            range = (byte)int.Parse(paras[33]);
            duration = uint.Parse(paras[34]);
            effectRange = byte.Parse(paras[35]);
            isRate = toBool(paras[36]);
            cast = uint.Parse(paras[37]);
            delay = uint.Parse(paras[38]);
            hp = short.Parse(paras[39]);
            mp = short.Parse(paras[40]);
            sp = short.Parse(paras[41]);
            weightUp = short.Parse(paras[42]);
            volumeUp = short.Parse(paras[43]);
            speedUp = short.Parse(paras[44]);
            str = short.Parse(paras[45]);
            mag = short.Parse(paras[46]);
            vit = short.Parse(paras[47]);
            dex = short.Parse(paras[48]);
            agi = short.Parse(paras[49]);
            intel = short.Parse(paras[50]);
            luk = short.Parse(paras[51]);
            cha = short.Parse(paras[52]);
            atk1 = short.Parse(paras[53]);
            atk2 = short.Parse(paras[54]);
            atk3 = short.Parse(paras[55]);
            matk = short.Parse(paras[56]);
            def = short.Parse(paras[57]);
            mdef = short.Parse(paras[58]);
            hitMelee = short.Parse(paras[59]);
            hitRanged = short.Parse(paras[60]);
            hitMagic = short.Parse(paras[61]);
            avoidMelee = short.Parse(paras[62]);
            avoidRanged = short.Parse(paras[63]);
            if (paras[64] != ".")
                avoidMagic = short.Parse(paras[64]);
            hitCritical = short.Parse(paras[65]);
            avoidCritical = short.Parse(paras[66]);
            hpRecover = short.Parse(paras[67]);
            mpRecover = short.Parse(paras[68]);
            for (int i = 0; i < 7; i++)
            {
                element.Add((Elements)i, short.Parse(paras[69 + i]));
            }
            for (int i = 0; i < 9; i++)
            {
                abnormalStatus.Add((AbnormalStatus)i, short.Parse(paras[76 + i]));
            }
            for (int i = 0; i < 4; i++)
            {
                possibleRace.Add((PC_RACE)i, toBool(paras[85 + i]));
            }
            for (int i = 0; i < 2; i++)
            {
                possibleGender.Add((PC_GENDER)i, toBool(paras[89 + i]));
            }
            possibleLv = byte.Parse(paras[91]);
            //转生
            possibleRebirth = toBool(paras[92]);
            possibleStr = ushort.Parse(paras[93]);
            possibleMag = ushort.Parse(paras[94]);
            possibleVit = ushort.Parse(paras[95]);
            possibleDex = ushort.Parse(paras[96]);
            possibleAgi = ushort.Parse(paras[97]);
            possibleInt = ushort.Parse(paras[98]);
            possibleLuk = ushort.Parse(paras[99]);
            possibleCha = ushort.Parse(paras[100]);
            //string[] jobs = Enum.GetNames(typeof(PC_JOB));
            //追加13个
            possibleJob.Add(PC_JOB.NOVICE, toBool(paras[101]));
            for (int i = 0; i < 36; i++)
            {
                possibleJob.Add((PC_JOB)(i / 3 * 10 + (i % 3 * 2) + 1), toBool(paras[102 + i]));
            }
            for (int i = 0; i < 12; i++)
            {
                possibleJob.Add((PC_JOB)(i * 10 + 7), toBool(paras[138 + i]));
            }
            for (int i = 0; i < 4; i++)
            {
                possibleCountry.Add((Country)i, toBool(paras[151 + i]));
            }

            possibleJob.Add(PC_JOB.JOKER, toBool(paras[150]));
            possibleJob.Add(PC_JOB.BREEDER, toBool(paras[159]));
            possibleJob.Add(PC_JOB.GARDNER, toBool(paras[160]));

            marionetteID = uint.Parse(paras[166]);
            petID = uint.Parse(paras[167]);
            handMotion = byte.Parse(paras[168]);
            handMotion2 = byte.Parse(paras[169]);
            desc = paras[174];
            noTrade = int.Parse(paras[176]) > 0;
        }
        private bool toBool(string input)
        {
            if (input == "1") return true; else return false;
        }
        #endregion
        [CategoryAttribute("常规"), DescriptionAttribute("物品的名称")]
        public string 道具名
        {
            get { return name; }
            set { name = value; }
        }
        [CategoryAttribute("常规"), DescriptionAttribute("给物品设定的ID，不可重复")]
        public uint ID
        {
            get { return id; }
            set { id = value; }
        }
        [CategoryAttribute("外观"), DescriptionAttribute("物品的外形ID，对应ItemPict")]
        public uint ImageID
        {
            get { return imageID; }
            set { imageID = value; }
        }
        [CategoryAttribute("外观"), DescriptionAttribute("物品的小图标ID")]
        public uint IconID
        {
            get { return iconID; }
            set { iconID = value; }
        }
        [CategoryAttribute("基本"), DescriptionAttribute("物品的类型")]
        public ItemTypes 类型
        {
            get { return itemType; }
            set { itemType = value; }
        }
        [CategoryAttribute("基本"), DescriptionAttribute("物品的价值")]
        public uint 价值
        {
            get { return price; }
            set { price = value; }
        }
        [CategoryAttribute("基本")]
        public ushort 重量
        {
            get { return weight; }
            set { weight = value; }
        }
        [CategoryAttribute("基本")]
        public ushort 体积
        {
            get { return volume; }
            set { volume = value; }
        }
        [CategoryAttribute("基本")]
        public ushort 装备时体积
        {
            get { return equipVolume; }
            set { equipVolume = value; }
        }
        [CategoryAttribute("基本")]
        public ushort 凭依体重
        {
            get { return possessionWeight; }
            set { possessionWeight = value; }
        }
        [CategoryAttribute("扩展"), DescriptionAttribute("修理物品时需要的道具ID")]
        public int 修理道具
        {
            get { return repairItem; }
            set { repairItem = value; }
        }
        [CategoryAttribute("扩展")]
        public int 强化ID
        {
            get { return enhancementItem; }
            set { enhancementItem = value; }
        }
        //paras[12]
        [CategoryAttribute("事件"), DescriptionAttribute("是否使用时触发事件")]
        public uint Events
        {
            get { return events; }
            set { events = value; }
        }
        [CategoryAttribute("未知"), DescriptionAttribute("レシピ可")]
        public bool Receipt
        {
            get { return receipt; }
            set { receipt = value; }
        }
        [CategoryAttribute("其它"), DescriptionAttribute("是否可以染色")]
        public bool Dye
        {
            get { return dye; }
            set { dye = value; }
        }
        [CategoryAttribute("道具"),DescriptionAttribute("该道具是否可以叠加")]
        public bool Stock
        {
            get { return stock; }
            set { stock = value; }
        }
        [CategoryAttribute("装备属性"), DescriptionAttribute("是否为双手武器")]
        public bool DoubleHand
        {
            get { return doubleHand; }
            set { doubleHand = value; }
        }
        [CategoryAttribute("道具"), Description("该道具是否使用后消灭")]
        public bool Usable
        {
            get { return usable; }
            set { usable = value; }
        }
        [CategoryAttribute("未知"), DescriptionAttribute("色")]
        public byte Color
        {
            get { return color; }
            set { color = value; }
        }
        [CategoryAttribute("装备属性"), DescriptionAttribute("设置道具的最大耐久度")]
        public ushort 耐久度
        {
            get { return durability; }
            set { durability = value; }
        }
        [CategoryAttribute("装备属性"), DescriptionAttribute("设置装备这个物品后改变的职业")]
        public PC_JOB 装备时更变职业ID
        {
            get { return jointJob; }
            set { jointJob = value; }
        }
        [CategoryAttribute("未知"), DescriptionAttribute("初期スロット数")]
        public byte CurrentSlot
        {
            get { return currentSlot; }
            set { currentSlot = value; }
        }
        [CategoryAttribute("未知"), DescriptionAttribute("アイテムチェンジ可否フラグ")]
        public byte MaxSlot
        {
            get { return maxSlot; }
            set { maxSlot = value; }
        }
        [CategoryAttribute("事件"), DescriptionAttribute("设置使用后触发的脚本ID")]
        public uint EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }
        [CategoryAttribute("事件"), DescriptionAttribute("设置使用后显示的效果ID")]
        public uint EffectID
        {
            get { return effectID; }
            set { effectID = value; }
        }
        [CategoryAttribute("事件"), DescriptionAttribute("设置使用后出现的技能ID")]
        public ushort 触发技能
        {
            get { return activateSkill; }
            set { activateSkill = value; }
        }
        [CategoryAttribute("装备属性"), DescriptionAttribute("设置装备后给予玩家的主动技能")]
        public ushort PossibleSkill
        {
            get { return possibleSkill; }
            set { possibleSkill = value; }
        }
        [CategoryAttribute("装备属性"), DescriptionAttribute("设置装备后给予玩家的被动技能")]
        public ushort PossiveSkill
        {
            get { return passiveSkill; }
            set { passiveSkill = value; }
        }
        [CategoryAttribute("装备属性"), DescriptionAttribute("设置装备后给予玩家的主动技能")]
        public ushort PossessionSkill
        {
            get { return possessionSkill; }
            set { possessionSkill = value; }
        }
        [CategoryAttribute("装备属性"), DescriptionAttribute("设置装备后给予玩家的被动技能")]
        public ushort PossessionPassiveSkill
        {
            get { return possessionPassiveSkill; }
            set { possessionPassiveSkill = value; }
        }
        [CategoryAttribute("使用"), DescriptionAttribute("选择使用后的目标效果")]
        public TargetType TargetType
        {
            get { return target; }
            set { target = value; }
        }
        [CategoryAttribute("使用"), DescriptionAttribute("选择使用后的自身效果")]
        public ActiveType ActiveType
        {
            get { return activeType; }
            set { activeType = value; }
        }
        [CategoryAttribute("使用"), DescriptionAttribute("设置使用射程")]
        public byte 射程
        {
            get { return range; }
            set { range = value; }
        }
        [CategoryAttribute("使用")]
        public uint 效果时间
        {
            get { return duration; }
            set { duration = value; }
        }
        [CategoryAttribute("使用")]
        public byte 效果范围
        {
            get { return effectRange; }
            set { effectRange = value; }
        }
        [CategoryAttribute("使用"), DescriptionAttribute("効果値")]
        public bool IsRate
        {
            get { return isRate; }
            set { isRate = value; }
        }
        [CategoryAttribute("使用")]
        public uint 吟唱时间
        {
            get { return cast; }
            set { cast = value; }
        }
        [CategoryAttribute("使用")]
        public uint 冷却时间
        {
            get { return delay; }
            set { delay = value; }
        }
        [CategoryAttribute("属性-基本")]
        public short Hp
        {
            get { return hp; }
            set { hp = value; }
        }
        [CategoryAttribute("属性-基本")]
        public short Mp
        {
            get { return mp; }
            set { mp = value; }
        }
        [CategoryAttribute("属性-基本")]
        public short Sp
        {
            get { return sp; }
            set { sp = value; }
        }
        [CategoryAttribute("属性"), DescriptionAttribute("上升体重")]
        public short WeightUp
        {
            get { return weightUp; }
            set { weightUp = value; }
        }
        [CategoryAttribute("属性"), DescriptionAttribute("上升体积")]
        public short VolumeUp
        {
            get { return volumeUp; }
            set { volumeUp = value; }
        }
        [CategoryAttribute("属性-素质")]
        public short STR
        {
            get { return str; }
            set { str = value; }
        }
        [CategoryAttribute("属性-素质")]
        public short MAG
        {
            get { return mag; }
            set { mag = value; }
        }
        [CategoryAttribute("属性-素质")]
        public short VIT
        {
            get { return vit; }
            set { vit = value; }
        }
        [CategoryAttribute("属性-素质")]
        public short DEX
        {
            get { return dex; }
            set { dex = value; }
        }
        [CategoryAttribute("属性-素质")]
        public short AGI
        {
            get { return agi; }
            set { agi = value; }
        }
        [CategoryAttribute("属性-素质")]
        public short INT
        {
            get { return intel; }
            set { intel = value; }
        }
        [CategoryAttribute("属性")]
        public short LUK
        {
            get { return luk; }
            set { luk = value; }
        }
        [CategoryAttribute("属性")]
        public short CHA
        {
            get { return cha; }
            set { cha = value; }
        }
        [CategoryAttribute("属性-攻击")]
        public short ATK1
        {
            get { return atk1; }
            set { atk1 = value; }
        }
        [CategoryAttribute("属性-攻击")]
        public short ATK2
        {
            get { return atk2; }
            set { atk2 = value; }
        }
        [CategoryAttribute("属性-攻击")]
        public short ATK3
        {
            get { return atk3; }
            set { atk3 = value; }
        }
        [CategoryAttribute("属性-攻击")]
        public short MATK
        {
            get { return matk; }
            set { matk = value; }
        }
        [CategoryAttribute("属性-防御")]
        public short DEF
        {
            get { return def; }
            set { def = value; }
        }
        [CategoryAttribute("属性-防御")]
        public short MDEF
        {
            get { return mdef; }
            set { mdef = value; }
        }
        [CategoryAttribute("属性-攻击"), DescriptionAttribute("近战命中力")]
        public short HitMelee
        {
            get { return hitMelee; }
            set { hitMelee = value; }
        }
        [CategoryAttribute("属性-攻击"), DescriptionAttribute("远程命中力")]
        public short HitRanged
        {
            get { return hitRanged; }
            set { hitRanged = value; }
        }
        [CategoryAttribute("属性-攻击"), DescriptionAttribute("魔法命中力")]
        public short HitMagic
        {
            get { return hitMagic; }
            set { hitMagic = value; }
        }
        [CategoryAttribute("属性-防御"), DescriptionAttribute("近战闪避")]
        public short AvoidMelee
        {
            get { return avoidMelee; }
            set { avoidMelee = value; }
        }
        [CategoryAttribute("属性-防御"), DescriptionAttribute("远程闪避")]
        public short AvoidRanged
        {
            get { return avoidRanged; }
            set { avoidRanged = value; }
        }
        [CategoryAttribute("属性-防御"), DescriptionAttribute("魔法闪避")]
        public short AvoidMagic
        {
            get { return avoidMagic; }
            set { avoidMagic = value; }
        }
        [CategoryAttribute("属性-攻击"), DescriptionAttribute("暴击力")]
        public short HitCritical
        {
            get { return hitCritical; }
            set { hitCritical = value; }
        }
        [CategoryAttribute("属性-防御"), DescriptionAttribute("暴击回避")]
        public short AvoidCritical
        {
            get { return avoidCritical; }
            set { avoidCritical = value; }
        }
        [CategoryAttribute("属性"), DescriptionAttribute("回复力")]
        public short HpRecover
        {
            get { return hpRecover; }
            set { hpRecover = value; }
        }
        [CategoryAttribute("属性"), DescriptionAttribute("魔法回复力")]
        public short MpRecover
        {
            get { return mpRecover; }
            set { mpRecover = value; }
        }
        [CategoryAttribute("属性-克制")]
        public short 无
        {
            get { return element[Elements.Neutral]; }
            set { element[Elements.Neutral] = value; }
        }
        [CategoryAttribute("属性-克制")]
        public short 火
        {
            get { return element[Elements.Fire]; }
            set { element[Elements.Fire] = value; }
        }
        [CategoryAttribute("属性-克制")]
        public short 水
        {
            get { return element[Elements.Water]; }
            set { element[Elements.Water] = value; }
        }
        [CategoryAttribute("属性-克制")]
        public short 风
        {
            get { return element[Elements.Wind]; }
            set { element[Elements.Wind] = value; }
        }
        [CategoryAttribute("属性-克制")]
        public short 地
        {
            get { return element[Elements.Earth]; }
            set { element[Elements.Earth] = value; }
        }
        [CategoryAttribute("属性-克制")]
        public short 光
        {
            get { return element[Elements.Holy]; }
            set { element[Elements.Holy] = value; }
        }
        [CategoryAttribute("属性-克制")]
        public short 暗
        {
            get { return element[Elements.Dark]; }
            set { element[Elements.Dark] = value; }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 中毒抵抗
        {
            get { return abnormalStatus[AbnormalStatus.Poisen]; }
            set { abnormalStatus[AbnormalStatus.Poisen] = value; }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 石化抵抗
        {
            get { return abnormalStatus[AbnormalStatus.Stone]; }
            set { abnormalStatus[AbnormalStatus.Stone] = value; }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 麻痹抵抗
        {
            get { return abnormalStatus[AbnormalStatus.Paralyse]; }
            set { abnormalStatus[AbnormalStatus.Paralyse] = value; }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 睡眠抵抗
        {
            get { return abnormalStatus[AbnormalStatus.Sleep]; }
            set { abnormalStatus[AbnormalStatus.Sleep] = value; }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 沉默抵抗
        {
            get { return abnormalStatus[AbnormalStatus.Silence]; }
            set { abnormalStatus[AbnormalStatus.Silence] = value; }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 鈍足抵抗
        {
            get { return abnormalStatus[AbnormalStatus.鈍足]; }
            set { abnormalStatus[AbnormalStatus.鈍足] = value; }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 混乱抵抗
        {
            get { return abnormalStatus[AbnormalStatus.Confused]; }
            set { abnormalStatus[AbnormalStatus.Confused] = value; }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 冰冻抵抗
        {
            get { return abnormalStatus[AbnormalStatus.Frosen]; }
            set { abnormalStatus[AbnormalStatus.Frosen] = value; }
        }
        [CategoryAttribute("属性-抵抗")]
        public short 昏厥抵抗
        {
            get { return abnormalStatus[AbnormalStatus.Stun]; }
            set { abnormalStatus[AbnormalStatus.Stun] = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("种族条件-埃米尔")]
        public bool EMIL
        {
            get { return possibleRace[PC_RACE.EMIL]; }
            set { possibleRace[PC_RACE.EMIL] = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("种族条件-塔尼亚")]
        public bool TITANIA
        {
            get { return possibleRace[PC_RACE.TITANIA]; }
            set { possibleRace[PC_RACE.TITANIA] = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("种族条件-道米尼")]
        public bool DOMINION
        {
            get { return possibleRace[PC_RACE.DOMINION]; }
            set { possibleRace[PC_RACE.DOMINION] = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("种族条件-DEM")]
        public bool DEM
        {
            get { return possibleRace[PC_RACE.DEM]; }
            set { possibleRace[PC_RACE.DEM] = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("性别条件")]
        public bool 男
        {
            get { return possibleGender[PC_GENDER.MALE]; }
            set { possibleGender[PC_GENDER.MALE] = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("性别条件")]
        public bool 女
        {
            get { return possibleGender[PC_GENDER.FEMALE]; }
            set { possibleGender[PC_GENDER.FEMALE] = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("等级条件")]
        public byte PossibleLv
        {
            get { return possibleLv; }
            set { possibleLv = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("转生要求条件")]
        public bool PossibleRebirth
        {
            get { return possibleRebirth; }
            set { possibleRebirth = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort PossibleStr
        {
            get { return possibleStr; }
            set { possibleStr = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort PossibleMag
        {
            get { return possibleMag; }
            set { possibleMag = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort PossibleVit
        {
            get { return possibleVit; }
            set { possibleVit = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort PossibleDex
        {
            get { return possibleDex; }
            set { possibleDex = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort PossibleAgi
        {
            get { return possibleAgi; }
            set { possibleAgi = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort PossibleInt
        {
            get { return possibleInt; }
            set { possibleInt = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort PossibleLuk
        {
            get { return possibleLuk; }
            set { possibleLuk = value; }
        }
        [CategoryAttribute("条件"), DescriptionAttribute("属性条件")]
        public ushort PossibleCha
        {
            get { return possibleCha; }
            set { possibleCha = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("初学者")]
        public bool NOVICE
        {
            get { return possibleJob[PC_JOB.NOVICE]; }
            set { possibleJob[PC_JOB.NOVICE] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("剑士")]
        public bool SWORDMAN
        {
            get { return possibleJob[PC_JOB.SWORDMAN]; }
            set { possibleJob[PC_JOB.SWORDMAN] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("剑客")]
        public bool BLADEMASTER
        {
            get { return possibleJob[PC_JOB.BLADEMASTER]; }
            set { possibleJob[PC_JOB.BLADEMASTER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("赏金猎人")]
        public bool BOUNTYHUNTER
        {
            get { return possibleJob[PC_JOB.BOUNTYHUNTER]; }
            set { possibleJob[PC_JOB.BOUNTYHUNTER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("剑斗士")]
        public bool GLADIATOR
        {
            get { return possibleJob[PC_JOB.GLADIATOR]; }
            set { possibleJob[PC_JOB.GLADIATOR] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("骑士")]
        public bool FENCER
        {
            get { return possibleJob[PC_JOB.FENCER]; }
            set { possibleJob[PC_JOB.FENCER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("圣骑士")]
        public bool KNIGHT
        {
            get { return possibleJob[PC_JOB.KNIGHT]; }
            set { possibleJob[PC_JOB.KNIGHT] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("暗黑骑士")]
        public bool DARKSTALKER
        {
            get { return possibleJob[PC_JOB.DARKSTALKER]; }
            set { possibleJob[PC_JOB.DARKSTALKER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("守护者")]
        public bool GUARDIAN
        {
            get { return possibleJob[PC_JOB.GUARDIAN]; }
            set { possibleJob[PC_JOB.GUARDIAN] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("盗贼")]
        public bool SCOUT
        {
            get { return possibleJob[PC_JOB.SCOUT]; }
            set { possibleJob[PC_JOB.SCOUT] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("刺客")]
        public bool ASSASSIN
        {
            get { return possibleJob[PC_JOB.ASSASSIN]; }
            set { possibleJob[PC_JOB.ASSASSIN] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("特工")]
        public bool COMMAND
        {
            get { return possibleJob[PC_JOB.COMMAND]; }
            set { possibleJob[PC_JOB.COMMAND] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("肃清者")]
        public bool ERASER
        {
            get { return possibleJob[PC_JOB.ERASER]; }
            set { possibleJob[PC_JOB.ERASER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("弓手")]
        public bool ARCHER
        {
            get { return possibleJob[PC_JOB.ARCHER]; }
            set { possibleJob[PC_JOB.ARCHER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("猎人")]
        public bool STRIKER
        {
            get { return possibleJob[PC_JOB.STRIKER]; }
            set { possibleJob[PC_JOB.STRIKER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("神枪手")]
        public bool GUNNER
        {
            get { return possibleJob[PC_JOB.GUNNER]; }
            set { possibleJob[PC_JOB.GUNNER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("隼人")]
        public bool HAWKEYE
        {
            get { return possibleJob[PC_JOB.HAWKEYE]; }
            set { possibleJob[PC_JOB.HAWKEYE] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("魔法师")]
        public bool WIZARD
        {
            get { return possibleJob[PC_JOB.WIZARD]; }
            set { possibleJob[PC_JOB.WIZARD] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("魔导师")]
        public bool SORCERER
        {
            get { return possibleJob[PC_JOB.SORCERER]; }
            set { possibleJob[PC_JOB.SORCERER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("贤者")]
        public bool SAGE
        {
            get { return possibleJob[PC_JOB.SAGE]; }
            set { possibleJob[PC_JOB.SAGE] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("魔动大师")]
        public bool FORCEMASTER
        {
            get { return possibleJob[PC_JOB.FORCEMASTER]; }
            set { possibleJob[PC_JOB.FORCEMASTER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("元素")]
        public bool SHAMAN
        {
            get { return possibleJob[PC_JOB.SHAMAN]; }
            set { possibleJob[PC_JOB.SHAMAN] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("精灵使")]
        public bool ELEMENTER
        {
            get { return possibleJob[PC_JOB.ELEMENTER]; }
            set { possibleJob[PC_JOB.ELEMENTER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("巫师")]
        public bool ENCHANTER
        {
            get { return possibleJob[PC_JOB.ENCHANTER]; }
            set { possibleJob[PC_JOB.ENCHANTER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("星术士")]
        public bool ASTRALIST
        {
            get { return possibleJob[PC_JOB.ASTRALIST]; }
            set { possibleJob[PC_JOB.ASTRALIST] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("祭司")]
        public bool VATES
        {
            get { return possibleJob[PC_JOB.VATES]; }
            set { possibleJob[PC_JOB.VATES] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("神官")]
        public bool DRUID
        {
            get { return possibleJob[PC_JOB.DRUID]; }
            set { possibleJob[PC_JOB.DRUID] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("吟游诗人")]
        public bool BARD
        {
            get { return possibleJob[PC_JOB.BARD]; }
            set { possibleJob[PC_JOB.BARD] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("主教")]
        public bool CARDINAL
        {
            get { return possibleJob[PC_JOB.CARDINAL]; }
            set { possibleJob[PC_JOB.CARDINAL] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("魔攻师")]
        public bool WARLOCK
        {
            get { return possibleJob[PC_JOB.WARLOCK]; }
            set { possibleJob[PC_JOB.WARLOCK] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("暗术师")]
        public bool CABALIST
        {
            get { return possibleJob[PC_JOB.CABALIST]; }
            set { possibleJob[PC_JOB.CABALIST] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("死灵")]
        public bool NECROMANCER
        {
            get { return possibleJob[PC_JOB.NECROMANCER]; }
            set { possibleJob[PC_JOB.NECROMANCER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("狩魂者")]
        public bool SOULTAKER
        {
            get { return possibleJob[PC_JOB.SOULTAKER]; }
            set { possibleJob[PC_JOB.SOULTAKER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("矿工")]
        public bool TATARABE
        {
            get { return possibleJob[PC_JOB.TATARABE]; }
            set { possibleJob[PC_JOB.TATARABE] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("铁匠")]
        public bool BLACKSMITH
        {
            get { return possibleJob[PC_JOB.BLACKSMITH]; }
            set { possibleJob[PC_JOB.BLACKSMITH] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("机械师")]
        public bool MACHINERY
        {
            get { return possibleJob[PC_JOB.MACHINERY]; }
            set { possibleJob[PC_JOB.MACHINERY] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("艺术家")]
        public bool MAESTRO
        {
            get { return possibleJob[PC_JOB.MAESTRO]; }
            set { possibleJob[PC_JOB.MAESTRO] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("农夫")]
        public bool FARMASIST
        {
            get { return possibleJob[PC_JOB.FARMASIST]; }
            set { possibleJob[PC_JOB.FARMASIST] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("炼金术师")]
        public bool ALCHEMIST
        {
            get { return possibleJob[PC_JOB.ALCHEMIST]; }
            set { possibleJob[PC_JOB.ALCHEMIST] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("木偶使")]
        public bool MARIONEST
        {
            get { return possibleJob[PC_JOB.MARIONEST]; }
            set { possibleJob[PC_JOB.MARIONEST] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("收获者")]
        public bool HARVEST
        {
            get { return possibleJob[PC_JOB.HARVEST]; }
            set { possibleJob[PC_JOB.HARVEST] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("冒险家")]
        public bool RANGER
        {
            get { return possibleJob[PC_JOB.RANGER]; }
            set { possibleJob[PC_JOB.RANGER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("探险家")]
        public bool EXPLORER
        {
            get { return possibleJob[PC_JOB.EXPLORER]; }
            set { possibleJob[PC_JOB.EXPLORER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("考古学家")]
        public bool TREASUREHUNTER
        {
            get { return possibleJob[PC_JOB.TREASUREHUNTER]; }
            set { possibleJob[PC_JOB.TREASUREHUNTER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("风行者")]
        public bool STRIDER
        {
            get { return possibleJob[PC_JOB.STRIDER]; }
            set { possibleJob[PC_JOB.STRIDER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("商人")]
        public bool MERCHANT
        {
            get { return possibleJob[PC_JOB.MERCHANT]; }
            set { possibleJob[PC_JOB.MERCHANT] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("贸易商")]
        public bool TRADER
        {
            get { return possibleJob[PC_JOB.TRADER]; }
            set { possibleJob[PC_JOB.TRADER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("赌徒")]
        public bool GAMBLER
        {
            get { return possibleJob[PC_JOB.GAMBLER]; }
            set { possibleJob[PC_JOB.GAMBLER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("赌圣")]
        public bool ROYALDEALER
        {
            get { return possibleJob[PC_JOB.ROYALDEALER]; }
            set { possibleJob[PC_JOB.ROYALDEALER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("小丑")]
        public bool JOKER
        {
            get { return possibleJob[PC_JOB.JOKER]; }
            set { possibleJob[PC_JOB.JOKER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("驯兽师")]
        public bool BREEDER
        {
            get { return possibleJob[PC_JOB.BREEDER]; }
            set { possibleJob[PC_JOB.BREEDER] = value; }
        }
        [CategoryAttribute("条件-职业"), DescriptionAttribute("庭院师")]
        public bool GARDNER
        {
            get { return possibleJob[PC_JOB.GARDNER]; }
            set { possibleJob[PC_JOB.GARDNER] = value; }
        }
        //161-164
        [CategoryAttribute("扩展"), DescriptionAttribute("变身成活动木偶的ID")]
        public uint MarionetteID
        {
            get { return marionetteID; }
            set { marionetteID = value; }
        }
        [CategoryAttribute("扩展"), DescriptionAttribute("召唤出宠物的ID")]
        public uint PetID
        {
            get { return petID; }
            set { petID = value; }
        }
        [CategoryAttribute("扩展"), DescriptionAttribute("单手动作")]
        public byte HandMotion
        {
            get { return handMotion; }
            set { handMotion = value; }
        }
        [CategoryAttribute("扩展"), DescriptionAttribute("双手动作")]
        public byte HandMotion2
        {
            get { return handMotion2; }
            set { handMotion2 = value; }
        }
        //170-173
        [CategoryAttribute("常规")]
        public string 简介
        {
            get { return desc; }
            set { desc = value; }
        }
        //175
        [CategoryAttribute("扩展"), DescriptionAttribute("道具是否可以交易")]
        public bool NoTrade
        {
            get { return noTrade; }
            set { noTrade = value; }
        }
    }
}
