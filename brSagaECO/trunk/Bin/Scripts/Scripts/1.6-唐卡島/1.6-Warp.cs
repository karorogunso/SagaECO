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
    public class P10001258 : RandomPortal
    {
        public P10001258()
        {
            Init(10001258, 10023000, 152, 192, 155, 196);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:上城(10023000)
    //目標坐標:(152,192) ~ (155,196)

    public class P10001271 : RandomPortal
    {
        public P10001271()
        {
            Init(10001271, 20023000, 133, 32, 138, 34);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:馬克特碼頭(20023000)
    //目標坐標:(133,32) ~ (138,34)

    public class P10001272 : RandomPortal
    {
        public P10001272()
        {
            Init(10001272, 30154000, 6, 15, 6, 15);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:活動木偶第一研究所(30154000)
    //目標坐標:(6,15) ~ (6,15)

    public class P10001273 : RandomPortal
    {
        public P10001273()
        {
            Init(10001273, 10062000, 208, 101, 209, 102);
        }
    }
    //原始地圖:活動木偶第一研究所(30154000)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(208,101) ~ (209,102)

    public class P10001274 : RandomPortal
    {
        public P10001274()
        {
            Init(10001274, 30154001, 6, 15, 6, 15);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:活動木偶第二研究所(30154001)
    //目標坐標:(6,15) ~ (6,15)

    public class P10001275 : RandomPortal
    {
        public P10001275()
        {
            Init(10001275, 10062000, 208, 141, 209, 142);
        }
    }
    //原始地圖:活動木偶第二研究所(30154001)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(208,141) ~ (209,142)

    public class P10001276 : RandomPortal
    {
        public P10001276()
        {
            Init(10001276, 30156000, 7, 16, 7, 16);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:活動木偶本部(30156000)
    //目標坐標:(7,16) ~ (7,16)

    public class P10001277 : RandomPortal
    {
        public P10001277()
        {
            Init(10001277, 10062000, 179, 143, 180, 144);
        }
    }
    //原始地圖:活動木偶本部(30156000)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(179,143) ~ (180,144)

    public class P10001278 : RandomPortal
    {
        public P10001278()
        {
            Init(10001278, 30155000, 6, 11, 6, 11);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:多爾斯軍本部(30155000)
    //目標坐標:(6,11) ~ (6,11)

    public class P10001279 : RandomPortal
    {
        public P10001279()
        {
            Init(10001279, 10062000, 174, 122, 175, 123);
        }
    }
    //原始地圖:多爾斯軍本部(30155000)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(174,122) ~ (175,123)

    public class P10001280 : RandomPortal
    {
        public P10001280()
        {
            Init(10001280, 30157000, 6, 13, 6, 13);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:機械師本部(30157000)
    //目標坐標:(6,13) ~ (6,13)

    public class P10001281 : RandomPortal
    {
        public P10001281()
        {
            Init(10001281, 10062000, 131, 28, 132, 28);
        }
    }
    //原始地圖:機械師本部(30157000)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(131,28) ~ (132,28)

    public class P10001282 : RandomPortal
    {
        public P10001282()
        {
            Init(10001282, 30152000, 6, 11, 6, 11);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:雷奧的活動木偶工作室(30152000)
    //目標坐標:(6,11) ~ (6,11)

    public class P10001283 : RandomPortal
    {
        public P10001283()
        {
            Init(10001283, 10062000, 124, 46, 124, 46);
        }
    }
    //原始地圖:雷奧的活動木偶工作室(30152000)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(124,46) ~ (124,46)

    public class P10001284 : RandomPortal
    {
        public P10001284()
        {
            this.EventID = 10001284;
            //Init(10001284, 30152001, 6, 11, 6, 11);
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.飛空庭完成) &&
                !Neko_05_cmask.Test(Neko_05.得到茜子))
            {
                if (pc.PossesionedActors.Count != 0)
                {
                    Warp(pc, 30152001, 6, 11);
                    return;
                }
                pc.CInt["Neko_05_Map_02"] = CreateMapInstance(50016000, 10062000, 110, 88);
                Warp(pc, (uint)pc.CInt["Neko_05_Map_02"], 6, 11);
                //EVENTMAP_IN 16 1 8 4 2
                return;
            }
            if ((Neko_05_amask.Test(Neko_05.茜子任务开始) && Neko_05_cmask.Test(Neko_05.把電腦唯讀記憶體給哈爾列爾利) && !Neko_05_cmask.Test(Neko_05.得到茜子)) ||
                (Neko_05_amask.Test(Neko_05.茜子任务开始) && Neko_05_cmask.Test(Neko_05.进入光塔) && !Neko_05_cmask.Test(Neko_05.飛空庭完成) && CountItem(pc, 10057900) >= 1) ||
                (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                !Neko_05_amask.Test(Neko_05.茜子任务结束) &&
                Neko_05_cmask.Test(Neko_05.开始交谈) &&
                !Neko_05_cmask.Test(Neko_05.尋找瑪莎的蹤跡)))
            {
                if (pc.PossesionedActors.Count != 0)
                {
                    Warp(pc, 30152001, 6, 11);
                    return;
                }
                pc.CInt["Neko_05_Map_01"] = CreateMapInstance(50015000, 10062000, 110, 88);
                Warp(pc, (uint)pc.CInt["Neko_05_Map_01"], 6, 11);
                //EVENTMAP_IN 15 1 6 10 4
                return;
            }
            if (pc.Level > 29 && pc.Fame > 19 && !Neko_05_amask.Test(Neko_05.茜子任务结束))
            {
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if ((pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900) ||
                        (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902) ||
                        (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017903) ||
                        (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017905) ||
                        (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017906))
                    {
                        if (pc.PossesionedActors.Count != 0)
                        {
                            Warp(pc, 30152001, 6, 11);
                            return;
                        }
                        pc.CInt["Neko_05_Map_01"] = CreateMapInstance(50015000, 10062000, 110, 88);
                        Warp(pc, (uint)pc.CInt["Neko_05_Map_01"], 6, 11);
                        //EVENTMAP_IN 15 1 6 10 4
                        return;
                    }
                }
            }
            Warp(pc, 30152001, 6, 11);

            //EVENTEND
            //EVT1000128405
            /*
            Say(pc, 0, 131, "從裡邊傳出聲音來了$R;");
            Say(pc, 0, 131, "現在正跟客人商談呢$R;" +
                "$R對不起，等一會兒再來吧$R;", "莉塔？");
            //*/
            //EVENTEND
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:莉塔的活動木偶工作室(30152001)
    //目標坐標:(6,11) ~ (6,11)

    public class P10001285 : RandomPortal
    {
        public P10001285()
        {
            Init(10001285, 10062000, 110, 88, 110, 88);
        }
    }
    //原始地圖:莉塔的活動木偶工作室(30152001)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(110,88) ~ (110,88)

    public class P10001286 : RandomPortal
    {
        public P10001286()
        {
            Init(10001286, 30153000, 6, 11, 6, 11);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:卡米羅的機械工場(30153000)
    //目標坐標:(6,11) ~ (6,11)

    public class P10001287 : RandomPortal
    {
        public P10001287()
        {
            Init(10001287, 10062000, 138, 33, 138, 33);
        }
    }
    //原始地圖:卡米羅的機械工場(30153000)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(138,33) ~ (138,33)

    public class P10001288 : RandomPortal
    {
        public P10001288()
        {
            Init(10001288, 30153001, 6, 11, 6, 11);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:利迪亞的機械工場(30153001)
    //目標坐標:(6,11) ~ (6,11)

    public class P10001289 : RandomPortal
    {
        public P10001289()
        {
            Init(10001289, 10062000, 120, 78, 120, 78);
        }
    }
    //原始地圖:利迪亞的機械工場(30153001)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(120,78) ~ (120,78)

    public class P10001290 : RandomPortal
    {
        public P10001290()
        {
            Init(10001290, 30151000, 6, 13, 6, 13);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:唐卡政務大樓(30151000)
    //目標坐標:(6,13) ~ (6,13)

    public class P10001291 : RandomPortal
    {
        public P10001291()
        {
            Init(10001291, 10062000, 92, 150, 92, 152);
        }
    }
    //原始地圖:唐卡政務大樓(30151000)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(92,150) ~ (92,152)

    public class P10001292 : RandomPortal
    {
        public P10001292()
        {
            Init(10001292, 30150000, 4, 7, 4, 7);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:唐卡民家(30150000)
    //目標坐標:(4,7) ~ (4,7)

    public class P10001293 : RandomPortal
    {
        public P10001293()
        {
            Init(10001293, 10062000, 41, 120, 41, 120);
        }
    }
    //原始地圖:唐卡民家(30150000)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(41,120) ~ (41,120)

    public class P10001294 : RandomPortal
    {
        public P10001294()
        {
            Init(10001294, 30011006, 3, 5, 3, 5);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:砦壁之咖啡館(30011006)
    //目標坐標:(3,5) ~ (3,5)

    public class P10001295 : RandomPortal
    {
        public P10001295()
        {
            Init(10001295, 10062000, 135, 82, 135, 82);
        }
    }
    //原始地圖:砦壁之咖啡館(30011006)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(135,82) ~ (135,82)

    public class P10001296 : RandomPortal
    {
        public P10001296()
        {
            Init(10001296, 30060007, 4, 7, 4, 7);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:唐卡商店(30060007)
    //目標坐標:(4,7) ~ (4,7)

    public class P10001297 : RandomPortal
    {
        public P10001297()
        {
            Init(10001297, 10062000, 125, 92, 125, 92);
        }
    }
    //原始地圖:唐卡商店(30060007)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(125,92) ~ (125,92)

    public class P10001298 : RandomPortal
    {
        public P10001298()
        {
            Init(10001298, 30060008, 4, 7, 4, 7);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:唐卡商店(30060008)
    //目標坐標:(4,7) ~ (4,7)

    public class P10001299 : RandomPortal
    {
        public P10001299()
        {
            Init(10001299, 10062000, 118, 185, 119, 185);
        }
    }
    //原始地圖:唐卡商店(30060008)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(118,185) ~ (119,185)
    public class P10001345 : RandomPortal
    {
        public P10001345()
        {
            Init(10001345, 30021005, 3, 5, 3, 5);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:唐卡寶石商店(30021005)
    //目標坐標:(3,5) ~ (3,5)

    public class P10001346 : RandomPortal
    {
        public P10001346()
        {
            Init(10001346, 10062000, 116, 133, 116, 133);
        }
    }
    //原始地圖:唐卡寶石商店(30021005)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(116,133) ~ (116,133)

    public class P10001347 : RandomPortal
    {
        public P10001347()
        {
            Init(10001347, 30002003, 3, 5, 3, 5);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:唐卡秘密商店(30002003)
    //目標坐標:(3,5) ~ (3,5)

    public class P10001348 : RandomPortal
    {
        public P10001348()
        {
            Init(10001348, 10062000, 139, 154, 139, 154);
        }
    }
    //原始地圖:唐卡秘密商店(30002003)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(139,154) ~ (139,154)

    public class P10001349 : RandomPortal
    {
        public P10001349()
        {
            Init(10001349, 30020007, 3, 5, 3, 5);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:唐卡裁縫店(30020007)
    //目標坐標:(3,5) ~ (3,5)

    public class P10001350 : RandomPortal
    {
        public P10001350()
        {
            Init(10001350, 10062000, 65, 153, 65, 153);
        }
    }
    //原始地圖:唐卡裁縫店(30020007)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(65,153) ~ (65,153)

    public class P10001351 : RandomPortal
    {
        public P10001351()
        {
            Init(10001351, 30012005, 3, 5, 3, 5);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:唐卡武器商店(30012005)
    //目標坐標:(3,5) ~ (3,5)

    public class P10001352 : RandomPortal
    {
        public P10001352()
        {
            Init(10001352, 10062000, 54, 154, 54, 154);
        }
    }
    //原始地圖:唐卡武器商店(30012005)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(54,154) ~ (54,154)

    public class P10001353 : RandomPortal
    {
        public P10001353()
        {
            Init(10001353, 30014003, 3, 5, 3, 5);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:唐卡古董商店(30014003)
    //目標坐標:(3,5) ~ (3,5)

    public class P10001354 : RandomPortal
    {
        public P10001354()
        {
            Init(10001354, 10062000, 46, 160, 46, 160);
        }
    }
    //原始地圖:唐卡古董商店(30014003)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(46,160) ~ (46,160)

    public class P10001355 : RandomPortal
    {
        public P10001355()
        {
            Init(10001355, 30010010, 3, 5, 3, 5);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:唐卡咖啡館(30010010)
    //目標坐標:(3,5) ~ (3,5)

    public class P10001356 : RandomPortal
    {
        public P10001356()
        {
            Init(10001356, 10062000, 58, 166, 58, 166);
        }
    }
    //原始地圖:唐卡咖啡館(30010010)
    //目標地圖:唐卡島(10062000)
    //目標坐標:(58,166) ~ (58,166)

    public class P10001357 : RandomPortal
    {
        public P10001357()
        {
            Init(10001357, 10059000, 68, 147, 72, 150);
        }
    }
    //原始地圖:唐卡島(10062000)
    //目標地圖:瑪依瑪依島(10059000)
    //目標坐標:(68,147) ~ (72,150)
}