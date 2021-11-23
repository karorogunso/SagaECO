using System.Collections.Generic;


namespace SagaDB.Iris
{
    public enum ReleaseAbility
    {
        HP最大値上昇Plus,//1
        MP最大値上昇Plus,//2
        SP最大値上昇Plus,//3
        HP回復力上昇Perc,//4
        MP回復力上昇Perc,//5
        SP回復力上昇Perc,//6
        STR上昇Plus,//7
        DEX上昇Plus,//8
        INT上昇Plus,//9
        VIT上昇Plus,//10
        AGI上昇Plus,//11
        MAG上昇Plus,//12
        ATK値上昇Plus,//22  13
        MATK値上昇Plus,//23  14
        SHIT値上昇Plus,//24   15
        SHIT値上昇Perc,//25   16
        LHIT値上昇Plus,//26 17
        LHIT値上昇Perc,//27 18
        DEF上昇Plus,//13 19
        武器攻撃力上昇Plus,//28 20 
        武器攻撃力上昇Perc,//29 21
        防具防御力上昇Plus,//30 22
        防具防御力上昇Perc,//14 23
        被物理ダメージ減少Perc,//15 24
        クリティカル上昇Perc,//16 25
        ガード率上昇Perc,//17 26
        ペイロード上昇Perc,//18 27
        キャパシティ上昇Perc,//19 28
        Lv差命中減少軽減Perc,//20 29
        ペット成長率上昇Perc,//21 30
    }

    public enum CardSlot
    {
        胸,
        武器,
        服,
    }

    public enum Rarity
    {
        Common=1,
        Uncommon,
        Rare,
        SuperRare,
        Special,
    }
}