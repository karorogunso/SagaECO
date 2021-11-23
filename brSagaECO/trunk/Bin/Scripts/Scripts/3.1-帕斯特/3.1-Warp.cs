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
    public class P10000464 : RandomPortal
    {
        public P10000464()
        {
            Init(10000464, 10017001, 133, 67, 136, 70);
        }
    }
    //原始地圖:帕斯特街道(10017000)
    //目標地圖:農夫農場(10017001)
    //目標坐標:(133,67) ~ (136,70)

    public class P10000465 : RandomPortal
    {
        public P10000465()
        {
            Init(10000465, 10017000, 133, 67, 136, 70);
        }
    }
    //原始地圖:農夫農場(10017001)
    //目標地圖:帕斯特街道(10017000)
    //目標坐標:(133,67) ~ (136,70)

    public class P10000807 : RandomPortal
    {
        public P10000807()
        {
            Init(10000807, 10054000, 41, 139, 41, 140);
        }
    }
    //原始地圖:約克之家(30090001)
    //目標地圖:謎之團要塞(10054000)
    //目標坐標:(41,139) ~ (41,140)

    public class P10000808 : RandomPortal
    {
        public P10000808()
        {
            Init(10000808, 30090001, 4, 7, 4, 7);
        }
    }
    //原始地圖:謎之團要塞(10054000)
    //目標地圖:約克之家(30090001)
    //目標坐標:(4,7) ~ (4,7)

    public class P10000809 : RandomPortal
    {
        public P10000809()
        {
            Init(10000809, 30131001, 6, 1, 6, 1);
        }
    }
    //原始地圖:謎之團要塞(10054000)、謎之團要塞北部(10054100)
    //目標地圖:謎之團本部(30131001)
    //目標坐標:(6,1) ~ (6,1)


    public class P10000811 : RandomPortal
    {
        public P10000811()
        {
            Init(10000811, 10018100, 251, 80, 253, 83);
        }
    }
    //原始地圖:牛牛草原(10056000)
    //目標地圖:東方海角(10018100)
    //目標坐標:(251,80) ~ (253,83)

    public class P10000812 : RandomPortal
    {
        public P10000812()
        {
            Init(10000812, 10057000, 2, 138, 4, 141);
        }
    }
    //原始地圖:牛牛草原(10056000)
    //目標地圖:帕斯特市(10057000)
    //目標坐標:(2,138) ~ (4,141)

    public class P10000813 : RandomPortal
    {
        public P10000813()
        {
            Init(10000813, 10056000, 251, 140, 253, 143);
        }
    }
    //原始地圖:帕斯特市(10057000)
    //目標地圖:牛牛草原(10056000)
    //目標坐標:(251,140) ~ (253,143)

    public class P10000814 : RandomPortal
    {
        public P10000814()
        {
            Init(10000814, 10054001, 2, 202, 4, 205);
        }
    }
    //原始地圖:牛牛草原(10056000)
    //目標地圖:謎之團要塞(10054001)
    //目標坐標:(2,202) ~ (4,205)

    public class P10000815 : RandomPortal
    {
        public P10000815()
        {
            Init(10000815, 10056000, 207, 2, 210, 4);
        }
    }
    //原始地圖:謎之團要塞(10054001)
    //目標地圖:牛牛草原(10056000)
    //目標坐標:(207,2) ~ (210,4)

    public class P10000816 : RandomPortal
    {
        public P10000816()
        {
            Init(10000816, 10057000, 166, 2, 169, 4);
        }
    }
    //原始地圖:謎之團要塞(10054001)
    //目標地圖:帕斯特市(10057000)
    //目標坐標:(166,2) ~ (169,4)

    public class P10000817 : Event
    {
        public P10000817()
        {
            this.EventID = 10000817;
        }

        public override void OnEvent(ActorPC pc)
        {//EVT10000817
            if (pc.Account.GMLevel >= 100)
            {
                switch (Select(pc, "管理者管理模式", "", "去中立島", "去海盜島", "去聖女島", "不去"))
                {
                    case 1:
                        Warp(pc, 10054100, 224, 86);
                        break;

                    case 2:
                        Warp(pc, 10054100, 123, 77);
                        break;

                    case 3:
                        Warp(pc, 10054000, 72, 140);
                        break;
                }
                return;
            }

            Say(pc, 131, "有通往下城的階梯唷$R;");

            if (pc.Quest != null)
            {
                if (pc.Quest.ID == 10031402 || pc.Quest.ID == 10031403)
                {
                    switch (Select(pc, "怎麼辦呢？", "", "去海盜島", "去聖女島", "不去"))
                    {
                        case 1:
                            Warp(pc, 10054100, 123, 77);
                            break;

                        case 2:
                            Warp(pc, 10054000, 72, 140);
                            break;
                    }
                    return;
                }
            }

            switch (Select(pc, "怎麼辦呢？", "", "去中立島", "去聖女島", "不去"))
            {
                case 1:
                    Warp(pc, 10054100, 224, 86);
                    break;

                case 2:
                    Warp(pc, 10054000, 72, 140);
                    break;
            }
        }
    }
    //原始地圖:謎之團本部(30131001)
    //目標地圖:謎之團要塞北部(10054100)
    //目標坐標:(224,86) ~ (225,87)

    public class P10000818 : RandomPortal
    {
        public P10000818()
        {
            Init(10000818, 10054100, 123, 77, 123, 79);
        }
    }
    //原始地圖:謎之團本部(30131001)
    //目標地圖:謎之團要塞北部(10054100)
    //目標坐標:(123,77) ~ (123,79)

    public class P10000819 : RandomPortal
    {
        public P10000819()
        {
            Init(10000819, 10054000, 72, 140, 72, 140);
        }
    }
    //原始地圖:謎之團本部(30131001)
    //目標地圖:謎之團要塞(10054000)
    //目標坐標:(72,140) ~ (72,140)

    public class P10000822 : RandomPortal
    {
        public P10000822()
        {
            Init(10000822, 10057000, 134, 251, 137, 253);
        }
    }
    //原始地圖:榖倉地帶(10068000)
    //目標地圖:帕斯特市(10057000)
    //目標坐標:(134,251) ~ (137,253)

    public class P10000824 : RandomPortal
    {
        public P10000824()
        {
            Init(10000824, 10057000, 251, 114, 253, 117);
        }
    }
    //原始地圖:東方地牢(20090000)
    //目標地圖:帕斯特市(10057000)
    //目標坐標:(251,114) ~ (253,117)

    public class P10000825 : RandomPortal
    {
        public P10000825()
        {
            Init(10000825, 20091000, 48, 251, 51, 253);
        }
    }
    //原始地圖:東方地牢(20090000)
    //目標地圖:毒濕地(20091000)
    //目標坐標:(48,251) ~ (51,253)

    public class P10000826 : RandomPortal
    {
        public P10000826()
        {
            Init(10000826, 20090000, 48, 2, 51, 4);
        }
    }
    //原始地圖:毒濕地(20091000)
    //目標地圖:東方地牢(20090000)
    //目標坐標:(48,2) ~ (51,4)

    public class P10000827 : RandomPortal
    {
        public P10000827()
        {
            Init(10000827, 20091000, 92, 251, 95, 253);
        }
    }
    //原始地圖:東方地牢(20090000)
    //目標地圖:毒濕地(20091000)
    //目標坐標:(92,251) ~ (95,253)

    public class P10000828 : RandomPortal
    {
        public P10000828()
        {
            Init(10000828, 20090000, 93, 2, 96, 4);
        }
    }
    //原始地圖:毒濕地(20091000)
    //目標地圖:東方地牢(20090000)
    //目標坐標:(93,2) ~ (96,4)

    public class P10000829 : RandomPortal
    {
        public P10000829()
        {
            Init(10000829, 20092000, 122, 251, 125, 253);
        }
    }
    //原始地圖:東方地牢(20090000)
    //目標地圖:原始森林(20092000)
    //目標坐標:(122,251) ~ (125,253)

    public class P10000830 : RandomPortal
    {
        public P10000830()
        {
            Init(10000830, 20090000, 228, 2, 231, 4);
        }
    }
    //原始地圖:原始森林(20092000)
    //目標地圖:東方地牢(20090000)
    //目標坐標:(228,2) ~ (231,4)

    public class P10000831 : RandomPortal
    {
        public P10000831()
        {
            Init(10000831, 20091000, 186, 225, 190, 226);
        }
    }
    //原始地圖:原始森林(20092000)
    //目標地圖:毒濕地(20091000)
    //目標坐標:(186,225) ~ (190,226)

    public class P10000832 : Event
    {
        public P10000832()
        {
            this.EventID = 10000832;//, 20090000, 34, 66, 37, 69);
        }
        public override void OnEvent(ActorPC pc)
        {
            BitMask<Crusade_Pluto> Crusade_Pluto_mask = pc.CMask["Crusade_Pluto"];

            if (pc.Account.GMLevel >= 100)
            {
                switch (Select(pc, "管理者管理模式", "", "去精靈們的秘密基地", "去下台的大佐那裡", "去冥王的叢林"))
                {
                    case 1:
                        ShowEffect(pc, 4081);
                        Say(pc, 0, 131, "「翡翠」正在閃爍著光芒呀$R;");
                        Warp(pc, 20092000, 168, 33);
                        break;
                    case 2:
                        Say(pc, 0, 131, "沒有資格的人，快滾吧$R;");
                        Warp(pc, 20090000, 35, 67);
                        break;
                    case 3:
                        if (CountItem(pc, 10012600) >= 1)
                        {
                            ShowEffect(pc, 4023);
                            Say(pc, 0, 131, "「赤銅寶物珠」破碎了$R;");
                            Crusade_Pluto_mask.SetValue(Crusade_Pluto.幫助討伐, false);
                            Crusade_Pluto_mask.SetValue(Crusade_Pluto.拒絕討伐, false);
                            TakeItem(pc, 10012600, 1);
                            Warp(pc, 20092001, 168, 33);
                            return;
                        }
                        ShowEffect(pc, 4023);
                        Crusade_Pluto_mask.SetValue(Crusade_Pluto.幫助討伐, false);
                        Crusade_Pluto_mask.SetValue(Crusade_Pluto.拒絕討伐, false);
                        TakeItem(pc, 10012600, 1);
                        Warp(pc, 20092001, 168, 33);
                        break;
                }
                return;
            }
            if (CountItem(pc, 10013002) >= 1)
            {
                ShowEffect(pc, 4081);
                Say(pc, 0, 131, "「翡翠」正在閃爍著光芒呀$R;");
                Warp(pc, 20092000, 168, 33);
                return;
            }
            if (CountItem(pc, 10012600) >= 1 && Crusade_Pluto_mask.Test(Crusade_Pluto.幫助討伐))
            {
                Say(pc, 0, 131, "…!?$R;" +
                    "好像聽到了普羅莎娜的聲音呀$R;");
                Say(pc, 0, 131, "可以幫忙到冥王的叢林$R打敗背叛的臣民嗎？$R;", "普羅莎娜");
                switch (Select(pc, "去冥王的叢林嗎？", "", "去冥王的叢林", "我不想去"))
                {
                    case 1:
                        if (CountItem(pc, 10012600) >= 1)
                        {
                            ShowEffect(pc, 4023);
                            Say(pc, 0, 131, "「赤銅寶物珠」破碎了$R;");
                            Crusade_Pluto_mask.SetValue(Crusade_Pluto.幫助討伐, false);
                            Crusade_Pluto_mask.SetValue(Crusade_Pluto.拒絕討伐, false);
                            TakeItem(pc, 10012600, 1);
                            Warp(pc, 20092001, 168, 33);
                            return;
                        }
                        ShowEffect(pc, 4023);
                        Crusade_Pluto_mask.SetValue(Crusade_Pluto.幫助討伐, false);
                        Crusade_Pluto_mask.SetValue(Crusade_Pluto.拒絕討伐, false);
                        TakeItem(pc, 10012600, 1);
                        Warp(pc, 20092001, 168, 33);
                        break;
                    case 2:
                        Say(pc, 0, 131, "好，$R尊重您的決定。$R;", "普羅莎娜");
                        Crusade_Pluto_mask.SetValue(Crusade_Pluto.幫助討伐, false);
                        Crusade_Pluto_mask.SetValue(Crusade_Pluto.拒絕討伐, false);
                        TakeItem(pc, 10012600, 1);
                        break;
                }
                return;
            }
            Say(pc, 0, 131, "沒有資格的人，快滾吧$R;");
            Warp(pc, 20090000, 35, 67);
        }
    }
    //原始地圖:毒濕地(20091000)
    //目標地圖:東方地牢(20090000)
    //目標坐標:(34,66) ~ (37,69)

    public class P10000833 : RandomPortal
    {
        public P10000833()
        {
            Init(10000833, 20092000, 168, 32, 171, 34);
        }
    }
    //原始地圖:毒濕地(20091000)
    //目標地圖:原始森林(20092000)
    //目標坐標:(168,32) ~ (171,34)

    public class P10000834 : RandomPortal
    {
        public P10000834()
        {
            Init(10000834, 20092000, 108, 77, 110, 79);
        }
    }
    //原始地圖:原始森林(20092000)
    //目標地圖:原始森林(20092000)
    //目標坐標:(108,77) ~ (110,79)

    public class P10000835 : RandomPortal
    {
        public P10000835()
        {
            Init(10000835, 10069000, 143, 209, 146, 211);
        }
    }
    //原始地圖:榖倉地帶(10068000)
    //目標地圖:不死皇城(10069000)
    //目標坐標:(143,209) ~ (146,211)

    public class P10000836 : RandomPortal
    {
        public P10000836()
        {
            Init(10000836, 10069000, 143, 209, 146, 211);
        }
    }
    //原始地圖:榖倉地帶(10068000)
    //目標地圖:不死皇城(10069000)
    //目標坐標:(143,209) ~ (146,211)

    public class P10000954 : RandomPortal
    {
        public P10000954()
        {
            Init(10000954, 30090000, 4, 7, 4, 7);
        }
    }
    //原始地圖:牛牛草原(10056000)
    //目標地圖:寵物小屋(30090000)
    //目標坐標:(4,7) ~ (4,7)

    public class P10000955 : RandomPortal
    {
        public P10000955()
        {
            Init(10000955, 10056000, 180, 56, 180, 56);
        }
    }
    //原始地圖:寵物小屋(30090000)
    //目標地圖:牛牛草原(10056000)
    //目標坐標:(180,56) ~ (180,56)

    public class P10000968 : RandomPortal
    {
        public P10000968()
        {
            Init(10000968, 30013002, 3, 5, 3, 5);
        }
    }
    //原始地圖:謎之團要塞(10054000)
    //目標地圖:要塞的武器製造所(30013002)
    //目標坐標:(3,5) ~ (3,5)

    public class P10000969 : RandomPortal
    {
        public P10000969()
        {
            Init(10000969, 10054000, 171, 137, 171, 137);
        }
    }
    //原始地圖:要塞的武器製造所(30013002)
    //目標地圖:謎之團要塞(10054000)
    //目標坐標:(171,137) ~ (171,137)

    public class P10000978 : RandomPortal
    {
        public P10000978()
        {
            Init(10000978, 10054000, 153, 159, 154, 160);
        }
    }
    //原始地圖:謎之團要塞(10054001)
    //目標地圖:謎之團要塞(10054000)
    //目標坐標:(153,159) ~ (154,160)

    public class P10000979 : RandomPortal
    {
        public P10000979()
        {
            Init(10000979, 10054001, 153, 165, 154, 166);
        }
    }
    //原始地圖:謎之團要塞(10054000)
    //目標地圖:謎之團要塞(10054001)
    //目標坐標:(153,165) ~ (154,166)

    public class P10000980 : RandomPortal
    {
        public P10000980()
        {
            Init(10000980, 10056000, 39, 90, 41, 91);
        }
    }
    //原始地圖:普羅莎娜的監獄(30141003)
    //目標地圖:牛牛草原(10056000)
    //目標坐標:(39,90) ~ (41,91)

    public class P10000981 : RandomPortal
    {
        public P10000981()
        {
            Init(10000981, 30141003, 11, 14, 12, 15);
        }
    }
    //原始地圖:牛牛草原(10056000)
    //目標地圖:普羅莎娜的監獄(30141003)
    //目標坐標:(11,14) ~ (12,15)

    public class P10001031 : RandomPortal
    {
        public P10001031()
        {
            Init(10001031, 30131001, 5, 9, 7, 10);
        }
    }
    //原始地圖:謎之團要塞(10054000)
    //目標地圖:謎之團本部(30131001)
    //目標坐標:(5,9) ~ (7,10)

    public class P10001032 : RandomPortal
    {
        public P10001032()
        {
            Init(10001032, 10054100, 43, 64, 43, 64);
        }
    }
    //原始地圖:海盜會館(30131002)
    //目標地圖:謎之團要塞北部(10054100)
    //目標坐標:(43,64) ~ (43,64)

    public class P10001033 : RandomPortal
    {
        public P10001033()
        {
            Init(10001033, 30131002, 5, 9, 7, 10);
        }
    }
    //原始地圖:謎之團要塞北部(10054100)
    //目標地圖:海盜會館(30131002)
    //目標坐標:(5,9) ~ (7,10)
}