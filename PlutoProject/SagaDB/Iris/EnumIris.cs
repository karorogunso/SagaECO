using System.Collections.Generic;


namespace SagaDB.Iris
{
    public enum ReleaseAbility
    {
        /// <summary>
        /// HP最大値＋上昇
        /// </summary>
        HP_MAX_UP,
        /// <summary>
        /// HP最大値％上昇
        /// </summary>
        HP_PER_UP,
        /// <summary>
        /// MP最大値＋上昇
        /// </summary>
        MP_MAX_UP,
        /// <summary>
        /// MP最大値％上昇
        /// </summary>
        MP_PER_UP,
        /// <summary>
        /// SP最大値＋上昇
        /// </summary>
        SP_MAX_UP,
        /// <summary>
        /// SP最大値％上昇
        /// </summary>
        SP_PER_UP,
        /// <summary>
        /// HP回復力％上昇
        /// </summary>
        HP_REC_UP,
        /// <summary>
        /// MP回復力％上昇
        /// </summary>
        MP_REC_UP,
        /// <summary>
        /// SP回復力％上昇
        /// </summary>
        SP_REC_UP,
        /// <summary>
        /// 技能消耗MP％减少
        /// </summary>
        SKILL_MP_PER_DOWN,
        /// <summary>
        /// 技能消耗SP％减少
        /// </summary>
        SKILL_SP_PER_DOWN,
        /// <summary>
        /// STR＋上昇
        /// </summary>
        STR_UP,
        /// <summary>
        /// DEX＋上昇
        /// </summary>
        DEX_UP,
        /// <summary>
        /// INT＋上昇
        /// </summary>
        INT_UP,
        /// <summary>
        /// VIT＋上昇
        /// </summary>
        VIT_UP,
        /// <summary>
        /// AGI＋上昇
        /// </summary>
        AGI_UP,
        /// <summary>
        /// MAG＋上昇
        /// </summary>
        MAG_UP,
        /// <summary>
        /// ATK値＋上昇
        /// </summary>
        ATK_FIX_UP,
        /// <summary>
        /// ATK％上昇
        /// </summary>
        ATK_PER_UP,
        /// <summary>
        /// M.ATK値＋上昇
        /// </summary>
        MATK_FIX_UP,
        /// <summary>
        /// M.ATK％上昇
        /// </summary>
        MATK_PER_UP,
        /// <summary>
        /// S.HIT値＋上昇
        /// </summary>
        SHIT_FIX_UP,
        /// <summary>
        /// S.HIT値％上昇
        /// </summary>
        SHIT_PER_UP,
        /// <summary>
        /// L.HIT値＋上昇
        /// </summary>
        LHIT_FIX_UP,
        /// <summary>
        /// L.HIT値％上昇
        /// </summary>
        LHIT_PER_UP,
        /// <summary>
        /// S.AVOID＋上昇
        /// </summary>
        SAVOID_FIX_UP,
        /// <summary>
        /// S.AVOID％上昇
        /// </summary>
        SAVOID_PER_UP,
        /// <summary>
        /// L.AVOID＋上昇
        /// </summary>
        LAVOID_FIX_UP,
        /// <summary>
        /// L.AVOID％上昇
        /// </summary>
        LAVOID_PER_UP,
        /// <summary>
        /// DEF＋上昇（左防）
        /// </summary>
        DEF_UP,
        /// <summary>
        /// DEF％上昇（左防，但已经没有卡片使用该参数）
        /// </summary>
        DEF_PER_UP,
        /// <summary>
        /// M.DEF＋上昇（左防）
        /// </summary>
        MDEF_FIX_UP,
        /// <summary>
        /// M.DEF％上昇（左防，但已经没有卡片使用该参数）
        /// </summary>
        MDEF_PER_UP,
        /// <summary>
        /// 武器攻撃力＋上昇
        /// </summary>
        WEAPON_FIX_UP,
        /// <summary>
        /// 武器攻撃力％上昇
        /// </summary>
        WEAPON_PER_UP,
        /// <summary>
        /// 防具防御力＋上昇（右防）
        /// </summary>
        EQUIP_DEF_FIX_UP,
        /// <summary>
        /// 防具防御力％上昇（右防）
        /// </summary>
        EQUIP_DEF_UP,
        /// <summary>
        /// 命中成功率上昇（已经没有卡片使用该参数）
        /// </summary>
        HIT_UP,
        /// <summary>
        /// 回避成功率上昇
        /// </summary>
        AVOID_UP,
        /// <summary>
        /// 被物理ダメージ％減少
        /// </summary>
        BDAMAGE_DOWN,
        /// <summary>
        /// 被魔法ダメージ％減少
        /// </summary>
        MDAMAGE_DOWN,
        /// <summary>
        /// 被属性攻撃ダメージ％減少
        /// </summary>
        ELMDMG_PER_DWON,
        /// <summary>
        /// PT参加中攻撃ダメージ％上昇（在任何队伍中即可生效）
        /// </summary>
        PT_DAMUP,
        /// <summary>
        /// PT参加中被攻撃ダメージ％減少（在任何队伍中即可生效）
        /// </summary>
        PT_DAMDOWN,
        /// <summary>
        /// 奈落で攻撃ダメージ％上昇
        /// </summary>
        HELL_DAMUP,
        /// <summary>
        /// 奈落で被攻撃ダメージ％減少
        /// </summary>
        HELL_DAMDOWN,
        /// <summary>
        /// クリティカル％上昇
        /// </summary>
        CRITICAL_UP,
        /// <summary>
        /// クリティカル回避％上昇
        /// </summary>
        CRIAVOID_UP,
        /// <summary>
        /// ガード率％上昇
        /// </summary>
        GUARD_UP,
        /// <summary>
        /// 最大重量＋上昇
        /// </summary>
        PAYLOAD_FIX_UP,
        /// <summary>
        /// 最大重量％上昇
        /// </summary>
        PAYLOAD_UP,
        /// <summary>
        /// 最大容量＋上昇
        /// </summary>
        CAPACITY_FIX_UP,
        /// <summary>
        /// 最大容量％上昇
        /// </summary>
        CAPACITY_UP,
        /// <summary>
        /// 死亡時自動復活％
        /// </summary>
        DEAD_REVIVE,
        /// <summary>
        /// Lv差命中減少％軽減
        /// </summary>
        LV_DIFF_DOWN,
        /// <summary>
        /// 毒耐性力上昇
        /// </summary>
        REGI_POISON_UP,
        /// <summary>
        /// 石化耐性力上昇
        /// </summary>
        REGI_STONE_UP,
        /// <summary>
        /// 睡眠耐性力上昇
        /// </summary>
        REGI_SLEEP_UP,
        /// <summary>
        /// 沈黙耐性力上昇
        /// </summary>
        REGI_SILENCE_UP,
        /// <summary>
        /// 鈍足化耐性力上昇
        /// </summary>
        REGI_SLOW_UP,
        /// <summary>
        /// 混乱耐性力上昇
        /// </summary>
        REGI_CONFUSION_UP,
        /// <summary>
        /// 氷化耐性力上昇（冰冻）
        /// </summary>
        REGI_ICE_UP,
        /// <summary>
        /// 気絶耐性力上昇
        /// </summary>
        REGI_STUN_UP,
        /// <summary>
        /// 能力低下無効化率上昇
        /// </summary>
        CAN_BTPDOWN_PER,
        /// <summary>
        /// 物理スキル詠唱速度減少
        /// </summary>
        P_CSPD_PER_DOWN,
        /// <summary>
        /// 物理スキル詠唱速度上昇
        /// </summary>
        P_CSPD_PER_UP,
        /// <summary>
        /// 魔法スキル詠唱速度減少
        /// </summary>
        M_CSPD_PER_DOWN,
        /// <summary>
        /// 魔法スキル詠唱速度上昇
        /// </summary>
        M_CSPD_PER_UP,
        /// <summary>
        /// Lv差回避％上昇
        /// </summary>
        LV_DIFF_UP,
        /// <summary>
        /// パートナー近接攻撃＋上昇
        /// </summary>
        PART_R_STPARAM0,
        /// <summary>
        /// パートナー魔法攻撃＋上昇
        /// </summary>
        PART_R_STPARAM1,
        /// <summary>
        /// パートナー耐久力＋上昇
        /// </summary>
        PART_R_STPARAM2,
        /// <summary>
        /// パートナー精神力＋上昇
        /// </summary>
        PART_R_STPARAM3,
        /// <summary>
        /// パートナー攻撃精度＋上昇
        /// </summary>
        PART_R_STPARAM4,
        /// <summary>
        /// パートナーフットワーク＋上昇
        /// </summary>
        PART_R_STPARAM5,
        /// <summary>
        /// パートナーHP回復率％上昇
        /// </summary>
        PART_R_HPHEAL_UP,
        /// <summary>
        /// パートナーHP最大値＋上昇
        /// </summary>
        PART_R_HPMAX_FIX_UP,
        /// <summary>
        /// パートナーHP最大値％上昇
        /// </summary>
        PART_R_HPMAX_UP,
        /// <summary>
        /// パートナーMP回復率＋上昇
        /// </summary>
        PART_MPHEAL_FIX_UP,
        /// <summary>
        /// パートナー最大MP＋上昇
        /// </summary>
        PART_MPMAX_FIX_UP,
        /// <summary>
        /// パートナー最大MP％上昇
        /// </summary>
        PART_MPMAX_UP,
        /// <summary>
        /// パートナーSP回復率＋上昇
        /// </summary>
        PART_SPHEAL_FIX_UP,
        /// <summary>
        /// パートナー最大SP＋上昇
        /// </summary>
        PART_SPMAX_FIX_UP,
        /// <summary>
        /// パートナー最大SP％上昇
        /// </summary>
        PART_SPMAX_UP,
        /// <summary>
        /// パートナーATK＋上昇
        /// </summary>
        PART_ATK_FIX_UP,
        /// <summary>
        /// パートナーATK％上昇
        /// </summary>
        PART_ATK_PER_UP,
        /// <summary>
        /// パートナーM.ATK＋上昇
        /// </summary>
        PART_MATK_FIX_UP,
        /// <summary>
        /// パートナーM.ATK％上昇
        /// </summary>
        PART_MATK_PER_UP,
        /// <summary>
        /// パートナーS.HIT＋上昇
        /// </summary>
        PART_SHIT_FIX_UP,
        /// <summary>
        /// パートナーS.HIT％上昇
        /// </summary>
        PART_SHIT_PER_UP,
        /// <summary>
        /// パートナーL.HIT＋上昇
        /// </summary>
        PART_LHIT_FIX_UP,
        /// <summary>
        /// パートナーL.HIT％上昇
        /// </summary>
        PART_LHIT_PER_UP,
        /// <summary>
        /// パートナーS.AVOID＋上昇
        /// </summary>
        PART_SAVOID_FIX_UP,
        /// <summary>
        /// パートナーS.AVOID％上昇
        /// </summary>
        PART_SAVOID_PER_UP,
        /// <summary>
        /// パートナーL.AVOID＋上昇
        /// </summary>
        PART_LAVOID_FIX_UP,
        /// <summary>
        /// パートナーL.AVOID％上昇
        /// </summary>
        PART_LAVOID_PER_UP,
        /// <summary>
        /// パートナーDEF(-)＋上昇
        /// </summary>
        PART_DEF_MINUS_FIX_UP,
        /// <summary>
        /// パートナーDEF(％)＋上昇
        /// </summary>
        PART_DEF_PER_FIX_UP,
        /// <summary>
        /// パートナーM.DEF(-)＋上昇
        /// </summary>
        PART_MDEF_MINUS_FIX_UP,
        /// <summary>
        /// パートナーM.DEF(％)＋上昇
        /// </summary>
        PART_MDEF_PER_FIX_UP,
        /// <summary>
        /// 装備パートナー移動速度＋上昇
        /// </summary>
        PART_MOVESPEED_UP,
        /// <summary>
        /// パートナー攻撃無属性＋上昇
        /// </summary>
        PART_R_ATKELEM_NONE,
        /// <summary>
        /// パートナー攻撃火属性＋上昇
        /// </summary>
        PART_R_ATKELEM_FIRE,
        /// <summary>
        /// パートナー攻撃水属性＋上昇
        /// </summary>
        PART_R_ATKELEM_WATER,
        /// <summary>
        /// パートナー攻撃風属性＋上昇
        /// </summary>
        PART_R_ATKELEM_WIND,
        /// <summary>
        /// パートナー攻撃土属性＋上昇
        /// </summary>
        PART_R_ATKELEM_EARTH,
        /// <summary>
        /// パートナー攻撃光属性＋上昇
        /// </summary>
        PART_R_ATKELEM_LIGHT,
        /// <summary>
        /// パートナー攻撃闇属性＋上昇
        /// </summary>
        PART_R_ATKELEM_DARK,
        /// <summary>
        /// パートナー防御無属性＋上昇
        /// </summary>
        PART_R_DEFELEM_NONE,
        /// <summary>
        /// パートナー防御火属性＋上昇
        /// </summary>
        PART_R_DEFELEM_FIRE,
        /// <summary>
        /// パートナー防御水属性＋上昇
        /// </summary>
        PART_R_DEFELEM_WATER,
        /// <summary>
        /// パートナー防御風属性＋上昇
        /// </summary>
        PART_R_DEFELEM_WIND,
        /// <summary>
        /// パートナー防御土属性＋上昇
        /// </summary>
        PART_R_DEFELEM_EARTH,
        /// <summary>
        /// パートナー防御光属性＋上昇
        /// </summary>
        PART_R_DEFELEM_LIGHT,
        /// <summary>
        /// パートナー防御闇属性＋上昇
        /// </summary>
        PART_R_DEFELEM_DARK,
        /// <summary>
        /// パートナー用獲得経験値上昇
        /// </summary>
        PART_EXP_UP,
        /// <summary>
        /// 獲得パートナー信頼度上昇
        /// </summary>
        PART_RELIANCE_UP,
        /// <summary>
        /// 人間系獲得U.P信頼度上昇
        /// </summary>
        PART_RELIANCE_HUMAN,
        /// <summary>
        /// 鳥系獲得U.P信頼度上昇
        /// </summary>
        PART_RELIANCE_BIRD,
        /// <summary>
        /// 動物系獲得U.P信頼度上昇
        /// </summary>
        PART_RELIANCE_ANIMAL,
        /// <summary>
        /// 昆虫系獲得U.P信頼度上昇
        /// </summary>
        PART_RELIANCE_INSECT,
        /// <summary>
        /// 魔法生物系獲得U.P信頼度上昇
        /// </summary>
        PART_RELIANCE_MAGIC_CREATURE,
        /// <summary>
        /// 植物系獲得U.P信頼度上昇
        /// </summary>
        PART_RELIANCE_PLANT,
        /// <summary>
        /// 水中生物系獲得U.P信頼度上昇
        /// </summary>
        PART_RELIANCE_WATER_ANIMAL,
        /// <summary>
        /// 機械系獲得U.P信頼度上昇
        /// </summary>
        PART_RELIANCE_MACHINE,
        /// <summary>
        /// 岩系獲得U.P信頼度上昇
        /// </summary>
        PART_RELIANCE_ROCK,
        /// <summary>
        /// 精霊系獲得U.P信頼度上昇
        /// </summary>
        PART_RELIANCE_ELEMENT,
        /// <summary>
        /// 死霊系獲得U.P信頼度上昇
        /// </summary>
        PART_RELIANCE_UNDEAD,
        /// <summary>
        /// 次回食事時間％短縮
        /// </summary>
        PART_FOODTIME_PER,
        /// <summary>
        /// 負傷時間短縮（分）（AA（图书馆）和AAA用）
        /// </summary>
        PART_DEATHPENA_TIME_FIX,
        /// <summary>
        /// ＰＰの獲得信頼度％上昇
        /// </summary>
        PP_RELIANCE_UP,
        /// <summary>
        /// 獲得???????????％上昇
        /// </summary>
        PP_PARTNER_POINT_UP,
        /// <summary>
        /// ＰＰの個人貢献度％上昇
        /// </summary>
        PP_POINT_UP,
        /// <summary>
        /// ＰＰ内戦闘不能復帰時間短縮
        /// </summary>
        PP_PENA_TIME_DOWN,
        /// <summary>
        /// ?????襲来時HP回復(%)
        /// </summary>
        PP_WAVE_HP_REC,
        /// <summary>
        /// 攻撃属性値+X上昇（闇）
        /// </summary>
        ELEMENT_ADD_DARK,
        /// <summary>
        /// 攻撃属性値+X上昇（光）
        /// </summary>
        ELEMENT_ADD_LIGHT,
        /// <summary>
        /// 攻撃属性値+X上昇（火）
        /// </summary>
        ELEMENT_ADD_FIRE,
        /// <summary>
        /// 攻撃属性値+X上昇（水）
        /// </summary>
        ELEMENT_ADD_WATER,
        /// <summary>
        /// 攻撃属性値+X上昇（土）
        /// </summary>
        ELEMENT_ADD_EARTH,
        /// <summary>
        /// 攻撃属性値+X上昇（風）
        /// </summary>
        ELEMENT_ADD_WIND,
        /// <summary>
        /// 耐久度減少確率減少
        /// </summary>
        DUR_DAMAGE_DOWN,
        /// <summary>
        /// 状態異常成功率上昇
        /// </summary>
        CHGSTATE_RATE_UP,
        /// <summary>
        /// 食べ物系アイテム効果上昇
        /// </summary>
        FOOD_UP,
        /// <summary>
        /// ポーション系アイテム効果上昇
        /// </summary>
        POTION_UP,
        /// <summary>
        /// 人間系攻撃ダメージ上昇
        /// </summary>
        DAMUP_HUMAN,
        /// <summary>
        /// 鳥系攻撃ダメージ上昇
        /// </summary>
        DAMUP_BIRD,
        /// <summary>
        /// 動物系攻撃ダメージ上昇
        /// </summary>
        DAMUP_ANIMAL,
        /// <summary>
        /// 昆虫系攻撃ダメージ上昇
        /// </summary>
        DAMUP_INSECT,
        /// <summary>
        /// 魔法生物系攻撃ダメージ上昇
        /// </summary>
        DAMUP_MAGIC_CREATURE,
        /// <summary>
        /// 植物系攻撃ダメージ上昇
        /// </summary>
        DAMUP_PLANT,
        /// <summary>
        /// 水中生物系攻撃ダメージ上昇
        /// </summary>
        DAMUP_WATER_ANIMAL,
        /// <summary>
        /// 機械系攻撃ダメージ上昇
        /// </summary>
        DAMUP_MACHINE,
        /// <summary>
        /// 岩系攻撃ダメージ上昇
        /// </summary>
        DAMUP_ROCK,
        /// <summary>
        /// 精霊系攻撃ダメージ上昇
        /// </summary>
        DAMUP_ELEMENT,
        /// <summary>
        /// 死霊系攻撃ダメージ上昇
        /// </summary>
        DAMUP_UNDEAD,
        /// <summary>
        /// 人間系被攻撃ダメージ減少
        /// </summary>
        DAMDOWN_HUMAN,
        /// <summary>
        /// 鳥系被攻撃ダメージ減少
        /// </summary>
        DAMDOWN_BIRD,
        /// <summary>
        /// 動物系被攻撃ダメージ減少
        /// </summary>
        DAMDOWN_ANIMAL,
        /// <summary>
        /// 昆虫系被攻撃ダメージ減少
        /// </summary>
        DAMDOWN_INSECT,
        /// <summary>
        /// 魔法生物系被攻撃ダメージ減少
        /// </summary>
        DAMDOWN_MAGIC_CREATURE,
        /// <summary>
        /// 植物系被攻撃ダメージ減少
        /// </summary>
        DAMDOWN_PLANT,
        /// <summary>
        /// 水中生物系被攻撃ダメージ減少
        /// </summary>
        DAMDOWN_WATER_ANIMAL,
        /// <summary>
        /// 機械系被攻撃ダメージ減少
        /// </summary>
        DAMDOWN_MACHINE,
        /// <summary>
        /// 岩系被攻撃ダメージ減少
        /// </summary>
        DAMDOWN_ROCK,
        /// <summary>
        /// 精霊系被攻撃ダメージ減少
        /// </summary>
        DAMDOWN_ELEMENT,
        /// <summary>
        /// 死霊系被攻撃ダメージ減少
        /// </summary>
        DAMDOWN_UNDEAD,
        /// <summary>
        /// 人間系基本経験値上昇
        /// </summary>
        EXP_HUMAN,
        /// <summary>
        /// 鳥系基本経験値上昇
        /// </summary>
        EXP_BIRD,
        /// <summary>
        /// 動物系基本経験値上昇
        /// </summary>
        EXP_ANIMAL,
        /// <summary>
        /// 昆虫系基本経験値上昇
        /// </summary>
        EXP_INSECT,
        /// <summary>
        /// 魔法生物系基本経験値上昇
        /// </summary>
        EXP_MAGIC_CREATURE,
        /// <summary>
        /// 植物系基本経験値上昇
        /// </summary>
        EXP_PLANT,
        /// <summary>
        /// 水中生物系基本経験値上昇
        /// </summary>
        EXP_WATER_ANIMAL,
        /// <summary>
        /// 機械系基本経験値上昇
        /// </summary>
        EXP_MACHINE,
        /// <summary>
        /// 岩系基本経験値上昇
        /// </summary>
        EXP_ROCK,
        /// <summary>
        /// 精霊系基本経験値上昇
        /// </summary>
        EXP_ELEMENT,
        /// <summary>
        /// 死霊系基本経験値上昇
        /// </summary>
        EXP_UNDEAD,
        /// <summary>
        /// 人間系職業経験値上昇
        /// </summary>
        JEXP_HUMAN,
        /// <summary>
        /// 鳥系職業経験値上昇
        /// </summary>
        JEXP_BIRD,
        /// <summary>
        /// 動物系職業経験値上昇
        /// </summary>
        JEXP_ANIMAL,
        /// <summary>
        /// 昆虫系職業経験値上昇
        /// </summary>
        JEXP_INSECT,
        /// <summary>
        /// 魔法生物系職業経験値上昇
        /// </summary>
        JEXP_MAGIC_CREATURE,
        /// <summary>
        /// 植物系職業経験値上昇
        /// </summary>
        JEXP_PLANT,
        /// <summary>
        /// 水中生物系職業経験値上昇
        /// </summary>
        JEXP_WATER_ANIMAL,
        /// <summary>
        /// 機械系職業経験値上昇
        /// </summary>
        JEXP_MACHINE,
        /// <summary>
        /// 岩系職業経験値上昇
        /// </summary>
        JEXP_ROCK,
        /// <summary>
        /// 精霊系職業経験値上昇
        /// </summary>
        JEXP_ELEMENT,
        /// <summary>
        /// 死霊系職業経験値上昇
        /// </summary>
        JEXP_UNDEAD,
        /// <summary>
        /// 人間系命中上昇
        /// </summary>
        HIT_HUMAN,
        /// <summary>
        /// 鳥系命中上昇
        /// </summary>
        HIT_BIRD,
        /// <summary>
        /// 動物系命中上昇
        /// </summary>
        HIT_ANIMAL,
        /// <summary>
        /// 昆虫系命中上昇
        /// </summary>
        HIT_INSECT,
        /// <summary>
        /// 魔法生物系命中上昇
        /// </summary>
        HIT_MAGIC_CREATURE,
        /// <summary>
        /// 植物系命中上昇
        /// </summary>
        HIT_PLANT,
        /// <summary>
        /// 水中生物系命中上昇
        /// </summary>
        HIT_WATER_ANIMAL,
        /// <summary>
        /// 機械系命中上昇
        /// </summary>
        HIT_MACHINE,
        /// <summary>
        /// 岩系命中上昇
        /// </summary>
        HIT_ROCK,
        /// <summary>
        /// 精霊系命中上昇
        /// </summary>
        HIT_ELEMENT,
        /// <summary>
        /// 死霊系命中上昇
        /// </summary>
        HIT_UNDEAD,
        /// <summary>
        /// 人間系回避上昇
        /// </summary>
        AVOID_HUMAN,
        /// <summary>
        /// 鳥系回避上昇
        /// </summary>
        AVOID_BIRD,
        /// <summary>
        /// 動物系回避上昇
        /// </summary>
        AVOID_ANIMAL,
        /// <summary>
        /// 昆虫系回避上昇
        /// </summary>
        AVOID_INSECT,
        /// <summary>
        /// 魔法生物系回避上昇
        /// </summary>
        AVOID_MAGIC_CREATURE,
        /// <summary>
        /// 植物系回避上昇
        /// </summary>
        AVOID_PLANT,
        /// <summary>
        /// 水中生物系回避上昇
        /// </summary>
        AVOID_WATER_ANIMAL,
        /// <summary>
        /// 機械系回避上昇
        /// </summary>
        AVOID_MACHINE,
        /// <summary>
        /// 岩系回避上昇
        /// </summary>
        AVOID_ROCK,
        /// <summary>
        /// 精霊系回避上昇
        /// </summary>
        AVOID_ELEMENT,
        /// <summary>
        /// 死霊系回避上昇
        /// </summary>
        AVOID_UNDEAD,
        /// <summary>
        /// 人間系クリティカル率上昇
        /// </summary>
        CRITICAL_HUMAN,
        /// <summary>
        /// 鳥系クリティカル率上昇
        /// </summary>
        CRITICAL_BIRD,
        /// <summary>
        /// 動物系クリティカル率上昇
        /// </summary>
        CRITICAL_ANIMAL,
        /// <summary>
        /// 昆虫系クリティカル率上昇
        /// </summary>
        CRITICAL_INSECT,
        /// <summary>
        /// 魔法生物系クリティカル率上昇
        /// </summary>
        CRITICAL_MAGIC_CREATURE,
        /// <summary>
        /// 植物系クリティカル率上昇
        /// </summary>
        CRITICAL_PLANT,
        /// <summary>
        /// 水中生物系クリティカル率上昇
        /// </summary>
        CRITICAL_WATER_ANIMAL,
        /// <summary>
        /// 機械系クリティカル率上昇
        /// </summary>
        CRITICAL_MACHINE,
        /// <summary>
        /// 岩系クリティカル率上昇
        /// </summary>
        CRITICAL_ROCK,
        /// <summary>
        /// 精霊系クリティカル率上昇
        /// </summary>
        CRITICAL_ELEMENT,
        /// <summary>
        /// 死霊系クリティカル率上昇
        /// </summary>
        CRITICAL_UNDEAD,
        /// <summary>
        /// マリオネット使用時間延長（活动木偶）
        /// </summary>
        MARIO_TIME_UP,
        /// <summary>
        /// マリオネット再使用時間短縮（活动木偶）
        /// </summary>
        MARIO_REUSE_TIME_CUT,
        /// <summary>
        /// ?????????効果時間延長(分)
        /// </summary>
        A_TRANS_TIME_UP,
        /// <summary>
        /// ?????????再使用時間短縮
        /// </summary>
        A_TRANS_TIME_CUT,
        /// <summary>
        /// 売却価格上昇
        /// </summary>
        SELLRATE_UP,
        /// <summary>
        /// 購入価格減少
        /// </summary>
        BUYRATE_DOWN,
        /// <summary>
        /// 力強化成功確率％上昇
        /// </summary>
        BOOST_POWER,
        /// <summary>
        /// 魔力強化成功確率％上昇
        /// </summary>
        BOOST_MAGIC,
        /// <summary>
        /// 命強化成功確率％上昇
        /// </summary>
        BOOST_HP,
        /// <summary>
        /// ??????強化成功確率％上昇
        /// </summary>
        BOOST_CRITICAL,
        /// <summary>
        /// スロット拡張成功確率％上昇
        /// </summary>
        SLOT_ENCHANT,
        /// <summary>
        /// ??????????成功確率％上昇
        /// </summary>
        CARD_ASSEMBLY,
        /// <summary>
        /// 獲得エクスパンションＰ＋上昇
        /// </summary>
        EXPANSION_POINT_FIX_UP,
        /// <summary>
        /// 獲得セクトＰ％上昇
        /// </summary>
        SECT_POINT_PER_UP,
        /// <summary>
        /// 獲得マテリアルＰ％上昇
        /// </summary>
        MATERIAL_POINT_PER_UP,
        /// <summary>
        /// 釣りレア確率＋上昇
        /// </summary>
        FISH_RARE_RATE_FIX_UP,
        /// <summary>
        /// 釣り大物確率＋上昇
        /// </summary>
        FISH_BIG_RATE_FIX_UP,
        /// <summary>
        /// 作物成長度＋上昇
        /// </summary>
        SEED_GROWTH_FIX_UP,
        /// <summary>
        /// 作物愛情度＋上昇
        /// </summary>
        SEED_AFFECTION_FIX_UP,
        /// <summary>
        /// 憑依速度上昇
        /// </summary>
        TRANCE_SPEED_UP,
        /// <summary>
        /// 憑依者貫通率軽減
        /// </summary>
        TRANS_DAMAGE_NOCHG_UP,
        /// <summary>
        /// ワールドゲージＰ＋％上昇
        /// </summary>
        RECYCLE_WOLRDPOINT_UP,
        /// <summary>
        /// クエスト報酬ＥＰ＋上昇
        /// </summary>
        QUEST_EP_UP,
        /// <summary>
        /// クエスト報酬ＭＰ＋上昇
        /// </summary>
        QUEST_MP_UP,
        /// <summary>
        /// クエスト報酬％上昇
        /// </summary>
        QUEST_UP,
        /// <summary>
        /// アビス?マター入手量＋上昇
        /// </summary>
        AM_FIX_UP,
        /// <summary>
        /// アビス?マター入手量％上昇
        /// </summary>
        AM_PER_UP,
        /// <summary>
        /// 獲得基本経験値上昇
        /// </summary>
        EXP_UP,
        /// <summary>
        /// 獲得職業経験値上昇
        /// </summary>
        JEXP_UP,
        /// <summary>
        /// 生産時獲得経験値％上昇
        /// </summary>
        ITEMCREATE_EXP_UP,
        /// <summary>
        /// 獲得ページ経験値上昇
        /// </summary>
        PAGE_EXP_UP,
        /// <summary>
        /// ??????????獲得経験値上昇
        /// </summary>
        DJOB_JEXP_UP,
        /// <summary>
        /// 獲得ＣＰ％上昇
        /// </summary>
        CP_UP,
        /// <summary>
        /// レアドロップ率上昇
        /// </summary>
        RAREDROP_UP,
        /// <summary>
        /// AC入場ペナルティ時間短縮
        /// </summary>
        AA_PENA_TIME_DOWN,
        /// <summary>
        /// 対???????????減少軽減
        /// </summary>
        SUB_CHAMP,
        /// <summary>
        /// 騎士団演習報酬％上昇
        /// </summary>
        KWAR_UP,
        /// <summary>
        /// 騎士団演習再入場時間短縮
        /// </summary>
        KWAR_PENA_TIME_DOWN,
        /// <summary>
        /// スケール変更
        /// </summary>
        SCALE_CHG,
        /// <summary>
        /// ドロップアイテム追加確率上昇
        /// </summary>
        DROP_ITEM_ADD,
        /// <summary>
        /// 合成失敗物精製確率上昇
        /// </summary>
        ITEM_SYNTHE_FAIL_UP,
        /// <summary>
        /// 合成失敗物精製確率減少
        /// </summary>
        ITEM_SYNTHE_SUCCESS_UP,
        /// <summary>
        /// 猛毒耐性力上昇
        /// </summary>
        REGI_HIPOISON_UP,
        NONE
    }

    public enum CardSlot
    {
        胸,
        武器,
        服,
    }

    public enum Rarity
    {
        Common = 1,
        Uncommon,
        Rare,
        SuperRare,
        Special,
    }

    public enum DrawType
    {
        Random,
        NomalOnly,
        UnCommonOnly,
        RarityOnly,
        SuperRarityOnly,
        AtleastOneSuperRarity
    }
}