using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class P10000475 : RandomPortal
    {
        public P10000475()
        {
            Init(10000475, 10050000, 102, 142, 105, 144);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:北方中央山脈(10050000)
    //目標坐標:(102,142) ~ (105,144)

    public class S10000600 : Event
    {
        public S10000600()
        {
            this.EventID = 10000600;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            if (Job2X_12_mask.Test(Job2X_12.搜集紋章紙) && !Job2X_12_mask.Test(Job2X_12.給予紋章紙))
            {
                Warp(pc, 30010005, 3, 5);
                return;
            }

            Warp(pc, 30010001, 3, 5);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:諾頓咖啡館(30010001)
    //目標坐標:(3,5) ~ (3,5)

    public class P10000601 : RandomPortal
    {
        public P10000601()
        {
            Init(10000601, 10065000, 65, 186, 65, 187);
        }
    }
    //原始地圖:諾頓咖啡館(30010001)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(65,186) ~ (65,187)

    public class P10000602 : RandomPortal
    {
        public P10000602()
        {
            Init(10000602, 30012001, 3, 5, 3, 5);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:諾頓武器商人(30012001)
    //目標坐標:(3,5) ~ (3,5)

    public class P10000603 : RandomPortal
    {
        public P10000603()
        {
            Init(10000603, 10065000, 65, 172, 65, 173);
        }
    }
    //原始地圖:諾頓武器商人(30012001)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(65,172) ~ (65,173)

    public class P10000606 : RandomPortal
    {
        public P10000606()
        {
            Init(10000606, 30001001, 2, 4, 2, 4);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:諾頓魔法商店(30001001)
    //目標坐標:(2,4) ~ (2,4)

    public class P10000607 : RandomPortal
    {
        public P10000607()
        {
            Init(10000607, 10065000, 65, 152, 65, 153);
        }
    }
    //原始地圖:諾頓魔法商店(30001001)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(65,152) ~ (65,153)

    public class P10000608 : RandomPortal
    {
        public P10000608()
        {
            Init(10000608, 30060001, 4, 7, 4, 7);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:諾頓商店(30060001)
    //目標坐標:(4,7) ~ (4,7)

    public class P10000609 : RandomPortal
    {
        public P10000609()
        {
            Init(10000609, 10065000, 65, 146, 65, 147);
        }
    }
    //原始地圖:諾頓商店(30060001)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(65,146) ~ (65,147)

    public class P10000610 : RandomPortal
    {
        public P10000610()
        {
            Init(10000610, 30011001, 3, 5, 3, 5);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:諾頓鑑定所(30011001)
    //目標坐標:(3,5) ~ (3,5)

    public class P10000611 : RandomPortal
    {
        public P10000611()
        {
            Init(10000611, 10065000, 65, 132, 65, 133);
        }
    }
    //原始地圖:諾頓鑑定所(30011001)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(65,132) ~ (65,133)

    public class P10000612 : RandomPortal
    {
        public P10000612()
        {
            Init(10000612, 30011002, 3, 5, 3, 5);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:諾頓鑑定所(30011002)
    //目標坐標:(3,5) ~ (3,5)

    public class P10000613 : RandomPortal
    {
        public P10000613()
        {
            Init(10000613, 10065000, 38, 186, 38, 187);
        }
    }
    //原始地圖:諾頓鑑定所(30011002)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(38,186) ~ (38,187)

    public class P10000614 : RandomPortal
    {
        public P10000614()
        {
            Init(10000614, 30060002, 4, 7, 4, 7);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:諾頓商店(30060002)
    //目標坐標:(4,7) ~ (4,7)

    public class P10000615 : RandomPortal
    {
        public P10000615()
        {
            Init(10000615, 10065000, 38, 172, 38, 173);
        }
    }
    //原始地圖:諾頓商店(30060002)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(38,172) ~ (38,173)

    public class P10000616 : RandomPortal
    {
        public P10000616()
        {
            Init(10000616, 30020002, 3, 5, 3, 5);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:諾頓裁縫店(30020002)
    //目標坐標:(3,5) ~ (3,5)

    public class P10000617 : RandomPortal
    {
        public P10000617()
        {
            Init(10000617, 10065000, 38, 152, 38, 153);
        }
    }
    //原始地圖:諾頓裁縫店(30020002)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(38,152) ~ (38,153)

    public class P10000618 : RandomPortal
    {
        public P10000618()
        {
            Init(10000618, 30021001, 3, 5, 3, 5);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:諾頓寶石商店(30021001)
    //目標坐標:(3,5) ~ (3,5)

    public class P10000619 : RandomPortal
    {
        public P10000619()
        {
            Init(10000619, 10065000, 38, 146, 38, 147);
        }
    }
    //原始地圖:諾頓寶石商店(30021001)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(38,146) ~ (38,147)

    public class P10000620 : RandomPortal
    {
        public P10000620()
        {
            Init(10000620, 30010002, 3, 5, 3, 5);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:諾頓咖啡館(30010002)
    //目標坐標:(3,5) ~ (3,5)

    public class P10000621 : RandomPortal
    {
        public P10000621()
        {
            Init(10000621, 10065000, 38, 132, 38, 133);
        }
    }
    //原始地圖:諾頓咖啡館(30010002)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(38,132) ~ (38,133)

    public class P10000622 : RandomPortal
    {
        public P10000622()
        {
            Init(10000622, 30163000, 11, 23, 13, 24);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:魔法行會總部門廊(30163000)
    //目標坐標:(11,23) ~ (13,24)

    public class P10000623 : RandomPortal
    {
        public P10000623()
        {
            Init(10000623, 10065000, 59, 80, 63, 82);
        }
    }
    //原始地圖:魔法行會總部門廊(30163000)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(59,80) ~ (63,82)


    /*
    public class P10000624 : RandomPortal
    {
        public P10000624()
        {
            Init(10000624, 30164000, 8, 16, 10, 16);
        }
    }
    */
    public class S10000624 : Event
    {
        public S10000624()
        {
            this.EventID = 10000624;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (wsj_mask.Test(wsj.扫把))
            {
                if (CountItem(pc, 10013200) > 0)
                {
                    PlaySound(pc, 3102, false, 100, 50);
                    ShowEffect(pc, 5051);
                    Wait(pc, 990);
                    PlaySound(pc, 2235, false, 100, 50);

                    Say(pc, 0, 0, "『破結界石』が砕け散った！$R;", "");
                    Warp(pc, 30164000, 9, 16);
                    TakeItem(pc, 10013200, 1);
                    return;
                }
                Say(pc, 0, 0, "結界によって封印されている。$R;", "");
                return;
            }
            BitMask<FallenTitantia> FallenTitantia_mask = pc.CMask["FallenTitantia"];
            //EVT1000062405
            if (pc.Level < 40)
            {
                if (pc.Job >= (PC_JOB)41 && pc.Job < (PC_JOB)80)
                /*pc.Job == PC_JOB.WIZARD
                || pc.Job == PC_JOB.SORCERER
                || pc.Job == PC_JOB.SAGE
                || pc.Job == PC_JOB.SHAMAN
                || pc.Job == PC_JOB.ELEMENTER
                || pc.Job == PC_JOB.ENCHANTER
                || pc.Job == PC_JOB.VATES
                || pc.Job == PC_JOB.DRUID
                || pc.Job == PC_JOB.BARD
                || pc.Job == PC_JOB.WARLOCK
                || pc.Job == PC_JOB.GAMBLER
                || pc.Job == PC_JOB.NECROMANCER)//*/
                {
                    switch (Select(pc, "進入哪間房間呢", "", "大導師房間", "總部房間", "放棄"))
                    {
                        case 1:
                            Warp(pc, 30164000, 9, 16);
                            break;
                        case 2:
                            Warp(pc, 30165000, 2, 6);
                            break;
                    }
                    return;
                }
            }
            if (pc.Level > 39)
            {
                if (pc.Job >= (PC_JOB)41 && pc.Job < (PC_JOB)80)
                /*pc.Job == PC_JOB.WIZARD
                || pc.Job == PC_JOB.SORCERER
                || pc.Job == PC_JOB.SAGE
                || pc.Job == PC_JOB.SHAMAN
                || pc.Job == PC_JOB.ELEMENTER
                || pc.Job == PC_JOB.ENCHANTER
                || pc.Job == PC_JOB.VATES
                || pc.Job == PC_JOB.DRUID
                || pc.Job == PC_JOB.BARD
                || pc.Job == PC_JOB.WARLOCK
                || pc.Job == PC_JOB.GAMBLER
                || pc.Job == PC_JOB.NECROMANCER)//*/
                {

                    if (!FallenTitantia_mask.Test(FallenTitantia.任务开始))
                    {
                        Say(pc, 131, "看得見傳送點裡$R;" +
                            "扭曲的空間！$R;");
                        switch (Select(pc, "跳進去嗎？", "", "放棄", "跳進去。"))
                        {
                            case 1:
                                switch (Select(pc, "進入哪間房間呢", "", "大導師房間", "總部房間", "放棄"))
                                {
                                    case 1:
                                        Warp(pc, 30164000, 9, 16);
                                        break;
                                    case 2:
                                        Warp(pc, 30165000, 2, 6);
                                        break;
                                }
                                break;
                            case 2:
                                FallenTitantia_mask.SetValue(FallenTitantia.任务开始, true);

                                Warp(pc, 30165001, 2, 6);
                                break;
                        }
                        return;
                    }
                    //*/
                    else
                    {
                        switch (Select(pc, "進入哪間房間呢", "", "大導師房間", "總部房間", "秘密房間", "放棄"))
                        {
                            case 1:
                                Warp(pc, 30164000, 9, 16);
                                break;
                            case 2:
                                Warp(pc, 30165000, 2, 6);
                                break;
                            case 3:
                                Warp(pc, 30165001, 2, 6);
                                break;
                        }
                    }
                    return;
                }
            }
            Warp(pc, 30165000, 2, 6);

        }
    }

    //原始地圖:魔法行會總部門廊(30163000)
    //目標地圖:大導師的房間(30164000)
    //目標坐標:(8,16) ~ (10,16)

    public class P10000625 : RandomPortal
    {
        public P10000625()
        {
            Init(10000625, 30163000, 11, 3, 13, 4);
        }
    }
    //原始地圖:大導師的房間(30164000)
    //目標地圖:魔法行會總部門廊(30163000)
    //目標坐標:(11,3) ~ (13,4)

    public class P10000626 : RandomPortal
    {
        public P10000626()
        {
            Init(10000626, 30165000, 2, 6, 2, 6);
        }
    }
    //原始地圖:魔法行會總部門廊(30163000)
    //目標地圖:總部的房間(30165000)
    //目標坐標:(2,6) ~ (2,6)

    public class P10000627 : RandomPortal
    {
        public P10000627()
        {
            Init(10000627, 30163000, 11, 3, 13, 4);
        }
    }
    //原始地圖:總部的房間(30165000)
    //目標地圖:魔法行會總部門廊(30163000)
    //目標坐標:(11,3) ~ (13,4)

    public class P10000628 : RandomPortal
    {
        public P10000628()
        {
            Init(10000628, 30160000, 10, 21, 13, 23);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:諾頓宮殿宴會廳(30160000)
    //目標坐標:(10,21) ~ (13,23)

    public class P10000629 : RandomPortal
    {
        public P10000629()
        {
            Init(10000629, 10065000, 50, 21, 53, 24);
        }
    }
    //原始地圖:諾頓宮殿宴會廳(30160000)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(50,21) ~ (53,24)

    public class P10000630 : RandomPortal
    {
        public P10000630()
        {
            Init(10000630, 10067000, 36, 61, 38, 62);
        }
    }
    //原始地圖:諾頓宮殿宴會廳(30160000)
    //目標地圖:女王謁見之間(10067000)
    //目標坐標:(36,61) ~ (38,62)

    public class P10000631 : RandomPortal
    {
        public P10000631()
        {
            Init(10000631, 30160000, 10, 2, 13, 3);
        }
    }
    //原始地圖:女王謁見之間(10067000)
    //目標地圖:諾頓宮殿宴會廳(30160000)
    //目標坐標:(10,2) ~ (13,3)



    public class S10000632 : Event
    {
        public S10000632()
        {
            this.EventID = 10000632;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10043303) > 0)
            {
                Warp(pc, 30162000, 8, 16);
                return;
            }
            if (pc.Gender == PC_GENDER.MALE)
            {
                Warp(pc, 30162000, 8, 16);
                return;
            }
            Say(pc, 131, "女士們請回吧$R;");
        }
    }

    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:諾頓王國軍等候室(30162000)
    //目標坐標:(8,16) ~ (10,17)

    public class P10000633 : RandomPortal
    {
        public P10000633()
        {
            Init(10000633, 10065000, 32, 6, 33, 7);
        }
    }
    //原始地圖:諾頓王國軍等候室(30162000)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(32,6) ~ (33,7)


    public class S10000634 : Event
    {
        public S10000634()
        {
            this.EventID = 10000634;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10005581) > 0)
            {
                Warp(pc, 30162001, 8, 16);
                return;
            }
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                Warp(pc, 30162001, 8, 16);
                return;
            }
            Say(pc, 131, "男士們請回吧$R;");
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:諾頓王國軍等候室(30162001)
    //目標坐標:(8,16) ~ (10,17)

    public class P10000635 : RandomPortal
    {
        public P10000635()
        {
            Init(10000635, 10065000, 70, 6, 71, 7);
        }
    }
    //原始地圖:諾頓王國軍等候室(30162001)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(70,6) ~ (71,7)

    public class P10000642 : RandomPortal
    {
        public P10000642()
        {
            Init(10000642, 30163000, 11, 23, 13, 24);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:魔法行會總部門廊(30163000)
    //目標坐標:(11,23) ~ (13,24)

    public class P10000643 : RandomPortal
    {
        public P10000643()
        {
            Init(10000643, 30163000, 11, 23, 13, 24);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:魔法行會總部門廊(30163000)
    //目標坐標:(11,23) ~ (13,24)

    public class P10000644 : RandomPortal
    {
        public P10000644()
        {
            Init(10000644, 30163000, 11, 23, 13, 24);
        }
    }
    //原始地圖:諾頓海濱長廊(10065000)
    //目標地圖:魔法行會總部門廊(30163000)
    //目標坐標:(11,23) ~ (13,24)

    public class P10000646 : RandomPortal
    {
        public P10000646()
        {
            Init(10000646, 30165001, 2, 6, 2, 6);
        }
    }
    //原始地圖:魔法行會總部門廊(30163000)
    //目標地圖:總部的秘密房間(30165001)
    //目標坐標:(2,6) ~ (2,6)
}