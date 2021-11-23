using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaDB.Skill
{
    public enum SkillFlags
    {
        NONE=0,
        NOT_EXIST=0x1,
        MAGIC=0x2,
        PHYSIC=0x4,
        PARTY_ONLY=0x8,
        ATTACK=0x10,
        CAN_HAS_TARGET=0x20,
        SUPPORT=0x40,
        HOLY=0x80,

        DEAD_ONLY=0x200,
        KIT_RELATED=0x400,
        NO_POSSESSION=0x800,
        NOT_BEEN_POSSESSED=0x1000,
        HEART_SKILL=0x2000,
    }

    public enum EquipFlags
    {
        HAND = 0x1,
        SWORD = 0x2,
        SHORT_SWORD = 0x4,
        //MACE = 0x8,
        HAMMER = 0x8,
        AXE = 0x10,
        SPEAR = 0x20,
        THROW = 0x40,
        BOW = 0x80,
        SHIELD = 0x100,
        GUN = 0x200,
        BAG = 0x400,
        CLAW = 0x800,
        RAPIER = 0x1000,
        KNUCKLE = 0x2000,
        DUALGUN = 0x4000,
        RIFLE = 0x8000,
        STRINGS = 0x10000,
        INSTRUMENT2 = 0x20000,
        ROPE = 0x40000,
        CARD = 0x80000,
        NONE = 0x100000,
        BOOK = 0x200000,
        STAFF = 0x400000,
        BULLET = 0x800000,
        ARROW = 0x1000000,
        ETC_WEAPON = 0x2000000,
        //DEM打 0x800000,
        //DEM斩 0x1000000,
        //DEM刺 0x2000000,
        //DEM远 0x4000000,
    }

    //public enum EquipFlags
    //{
    //    HAND = 0x0000001,
    //    SWORD = 0x0000002,
    //    SHORT_SWORD = 0x0000004,
    //    MACE = 0x0000008,
    //    AXE = 0x0000010,
    //    SPEAR = 0x0000020,
    //    THROW = 0x0000040,
    //    BOW = 0x0000080,
    //    SHIELD = 0x0000100,
    //    GUN = 0x0000200,
    //    BAG = 0x0000400,
    //    CLAW = 0x0000800,
    //    Sword = 0x0001000,
    //    KNUCKLE = 0x0002000,
    //    DUAL_GUN = 0x0004000,
    //    RIFLE = 0x0008000,
    //    STRINGS = 0x0010000,
    //    INSTRUMENT2 = 0x0020000,
    //    ROPE = 0x0040000,
    //    CARD = 0x0080000,
    //    DEM斩 = 0x0100000,
    //    DEM刺 = 0x0200000,
    //    DEM打 = 0x0400000,
    //    DEM远 = 0x0800000,
    //    TWOHANDSWORD = 0x1000000,
    //    TWOHANDSTAFF = 0x2000000,
    //    TWOHANDAXE = 0x4000000,
    //    UnknowEqupType = 0x8000000
    //    //DEM打 0x800000,
    //    //DEM斩 0x1000000,
    //    //DEM刺 0x2000000,
    //    //DEM远 0x4000000,

    //}


    public class SkillData
    {
        public uint id;
        public string name;
        public bool active;
        public byte maxLv, lv;
        public byte range, target, target2, effectRange, castRange;
        public ushort mp, sp, ep;
        public byte joblv;
        public uint effect;
        public int effect2;
        public int castTime, delay, SingleCD;
        public ushort nHumei4, nHumei5, nHumei6, nHumei7, nAnim1, nAnim2, nAnim3;
        public int skillFlag, effect1,effect3, nHumei8, effect4, effect5, effect6, effect7, effect8, nHumei2;
        public BitMask<SkillFlags> flag = new BitMask<SkillFlags>();
        public BitMask<EquipFlags> equipFlag = new BitMask<EquipFlags>();

        public override string ToString()
        {
            return this.name;
        }
    }

    public class Skill
    {
        SkillData baseData;
        byte lv;
        byte joblv;
        bool nosave;

        public SkillData BaseData { get { return this.baseData; } set { this.baseData = value; } }
        public uint ID { get { return this.baseData.id; } }
        public string Name { get { return this.baseData.name; } }
        public bool NoSave { get { return this.nosave; } set { this.nosave = value; } }
        public byte MaxLevel { get { return this.baseData.maxLv; } }
        public byte Level { get { return this.lv; } set { this.lv = value; } }
        public byte JobLv { get { return this.joblv; } set { this.joblv = value; } }
        public ushort MP { get { return this.baseData.mp; } }
        public ushort SP { get { return this.baseData.sp; } }
        public ushort EP { get { return this.baseData.ep; } }
        public byte Range { get { return this.baseData.range; } set { this.baseData.range = value; } }
        public uint Effect { get { return uint.Parse(this.baseData.effect4.ToString()); } set { this.baseData.effect4 = (int)value; } }
        public byte EffectRange { get { return this.baseData.effectRange; } set { this.baseData.effectRange = value; } }
        public byte CastRange { get { return this.baseData.target2; } }
        public byte Target { get { return this.baseData.target; } set { this.baseData.target = value; } }
        public byte Target2 { get { return this.baseData.target2; } set { this.baseData.target2 = value; } }
        public int CastTime { get { return this.baseData.castTime; } }
        public int Delay { get { return this.baseData.delay; } }
        public int SinglgCD { get { return this.baseData.SingleCD; } }
        public bool Magical { get { return this.baseData.flag.Test(SkillFlags.MAGIC); } }
        public bool Physical { get { return this.baseData.flag.Test(SkillFlags.PHYSIC); } }
        public bool PartyOnly { get { return this.baseData.flag.Test(SkillFlags.PARTY_ONLY); } }
        public bool Attack { get { return this.baseData.flag.Test(SkillFlags.ATTACK); } }
        public bool CanHasTarget { get { return this.baseData.flag.Test(SkillFlags.CAN_HAS_TARGET); } }
        public bool Support { get { return this.baseData.flag.Test(SkillFlags.SUPPORT); } }
        public bool DeadOnly { get { return this.baseData.flag.Test(SkillFlags.DEAD_ONLY); } }
        public bool NoPossession { get { return this.baseData.flag.Test(SkillFlags.NO_POSSESSION); } }
        public bool NotBeenPossessed { get { return this.baseData.flag.Test(SkillFlags.NOT_BEEN_POSSESSED); } }
        

        public override string ToString()
        {
            return this.baseData.name;
        }
    }

/*
        /// <summary>
        /// 技能使用结果
        /// 未完成.理论上有62个结果目前只完成了12个
        /// </summary>
        public enum UseSkillResult
        {
            OK = 0,
            OutOfMpAndSp = 1,
            OutOfCatalysts = 2,
            UnIdendityTarget = 3,
            CantFindTarget = 4,
            ThisWeaponCantUseThisSkill = 5,
            ThatPositionCantbeSelected = 6,
            ThisStatusCantUseSkill = 7,
            CastingOtherSkill = 8,
            InterruptByLongAttack = 9,
            YouHaveNotLearnThisSkill = 10,
            TargetIsDead = 11
        }
*/
/*
* 错误代码：
* 0 success 
-1～-60
# システムメッセージ(スキル)
GAME_SMSG_SKILL_USEERR1,";MPとSPが不足しています";
GAME_SMSG_SKILL_USEERR2,";使用する触媒が不足しています";
GAME_SMSG_SKILL_USEERR3,";ターゲットが視認できません";
GAME_SMSG_SKILL_USEERR4,";ターゲットが見つかりません";
GAME_SMSG_SKILL_USEERR5,";装備中の武器ではこのスキルを使用できません";
GAME_SMSG_SKILL_USEERR6,";指定不可能な位置が選択されました";
GAME_SMSG_SKILL_USEERR7,";スキルを使用できない状態です";
GAME_SMSG_SKILL_USEERR8,";他のスキルを詠唱している為キャンセルされました";
GAME_SMSG_SKILL_USEERR9,";遠距離攻撃中の為キャンセルされました";
GAME_SMSG_SKILL_USEERR10,";スキルを習得していません";
GAME_SMSG_SKILL_USEERR11,";対象が死んでいる為ターゲットできません";
GAME_SMSG_SKILL_USEERR12,";スキル使用条件があっていません";
GAME_SMSG_SKILL_USEERR13,";スキルを使用できません";
GAME_SMSG_SKILL_USEERR14,";スキルを使用できない対象です";
GAME_SMSG_SKILL_USEERR15,";MPが不足しています";
GAME_SMSG_SKILL_USEERR16,";SPが不足しています";
GAME_SMSG_SKILL_USEERR17,";指定した地形は別のスキルの効果中です";
GAME_SMSG_SKILL_USEERR18,";近くにテントが張られています";
GAME_SMSG_SKILL_USEERR19,";アイテム使用中の為キャンセルされました";
GAME_SMSG_SKILL_USEERR20,";攻撃中の為キャンセルされました";
GAME_SMSG_SKILL_USEERR21,";反応がありませんでした";
#エラーコード22はメッセージ無し
GAME_SMSG_SKILL_USEERR23,";憑依しないと使えません";
GAME_SMSG_SKILL_USEERR24,";他の憑依者が効果中です";
GAME_SMSG_SKILL_USEERR25,";憑依中は使えないスキルです";
GAME_SMSG_SKILL_USEERR26,";使用するのに必要なお金が足りません";
GAME_SMSG_SKILL_USEERR27,";宿主が健全でないため使用できません";
GAME_SMSG_SKILL_USEERR28,";宿主が行動不能時のみ使うことができます";
GAME_SMSG_SKILL_USEERR29,";スキルが使えない場所にいます";
GAME_SMSG_SKILL_USEERR30,";前回のスキルのディレイが残っています";
GAME_SMSG_SKILL_USEERR31,";ペットがいないと使えません";
GAME_SMSG_SKILL_USEERR32,";キャンプの中ではキャンプは禁止です";
GAME_SMSG_SKILL_USEERR33,";指定した場所に敵が侵入しているため使用できませんでした";
GAME_SMSG_SKILL_USEERR34,";矢を装備していないと使用できません";
GAME_SMSG_SKILL_USEERR35,";実包を装備していないと使用できません";
GAME_SMSG_SKILL_USEERR36,";投擲武器を装備していないと使用できません";
GAME_SMSG_SKILL_USEERR37,";使用できるテントを所持していません";
GAME_SMSG_SKILL_USEERR38,";栽培に使用するアイテムを所持していません";
GAME_SMSG_SKILL_USEERR39,";鑑定できるアイテムを所持していません";
GAME_SMSG_SKILL_USEERR40,";開けることのできるアイテムを所持していません";
GAME_SMSG_SKILL_USEERR41,";合成することのできるアイテムを所持していません";
GAME_SMSG_SKILL_USEERR42,";対象が移動したため射程から外れました";
GAME_SMSG_SKILL_USEERR43,";「メタモーバトル」中は使用できません";
GAME_SMSG_SKILL_USEERR44,";これ以上このスキルを設置することは出来ません";
#エラーコード45はGAME_SYNTHE_FULL_ITEM（アイテム欄に空きがありません）で代用中
GAME_SMSG_SKILL_USEERR46,";『騎士団演習』中は使用できません";
GAME_SMSG_SKILL_USEERR47,";ペットが近くにいないと使用できません";
GAME_SMSG_SKILL_USEERR48,";敵に発見された為、失敗しました";
GAME_SMSG_SKILL_USEERR49,";宿主中は使えないスキルです";
GAME_SMSG_SKILL_USEERR50,";再度スキルを使用するには時間を置いてください";
GAME_SMSG_SKILL_USEERR51,";ペットを使役している間は使用できません";
GAME_SMSG_SKILL_USEERR52,";ボスへの使用はできません";
GAME_SMSG_SKILL_USEERR53,";アンデッド状態の対象への使用はできません";
GAME_SMSG_SKILL_USEERR54,";現在の装備ではこのスキルを使用できません";
GAME_SMSG_SKILL_USEERR55,";矢の数が足りません";
GAME_SMSG_SKILL_USEERR56,";実包の数が足りません";
GAME_SMSG_SKILL_USEERR57,";投擲武器の数が足りません";
GAME_SMSG_SKILL_USEERR58,";融合できるマリオネットが違います";
GAME_SMSG_SKILL_USEERR59,";イベント中の為、使用できません";
GAME_SMSG_SKILL_USEERR60,";設定が不許可になっている為、使用できません";
*/

}
