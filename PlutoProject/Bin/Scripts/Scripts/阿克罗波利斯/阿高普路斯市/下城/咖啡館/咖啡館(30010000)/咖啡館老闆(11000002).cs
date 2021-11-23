using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Quests;
using SagaDB.Item;
//所在地圖:咖啡館(30010000) NPC基本信息:咖啡館老闆(11000002) X:1 Y:0
namespace SagaScript.M30010000
{
    public class S11000002 : Event
    {
        public S11000002()
        {
            this.EventID = 11000002;

            //任務服務台相關設定
            this.leastQuestPoint = 1;

            this.alreadyHasQuest = "任务进行的还顺利吗?$R;";

            this.gotNormalQuest = "那就拜托了。$R;" +
                                  "$R等任务完成以后，再来找我吧。$R;";

            this.gotTransportQuest = "是啊，道具太重了吧?$R;" +
                                     "$R所以不能一次传送的话，$R;" +
                                     "分成几次给我也可以的。$R;";

            this.questCompleted = "真是辛苦了。$R;" +
                                  "$R恭喜你，任务完成了。$R;" +
                                  "$P来! 收下报酬吧!$R;";

            this.transport = "哦哦…全部都收集好了吗?$R;";

            this.questCanceled = "嗯…如果是你，我相信你能做到的，$R;" +
                                 "很期待呢……$R;";

            this.questFailed = "……$R;" +
                               "$P失败了?$R;" +
                               "$R真是闹了大事，$R;" +
                               "不知道该说什么好啊?$R;" +
                               "$P这次实在没办法了，$R;" +
                               "下次一定要成功啊!$R;" +
                               "$R知道了吧?$R;";

            this.notEnoughQuestPoint = "嗯…$R;" +
                                       "$R现在没有要特别拜托的事情啊，$R;" +
                                       "再去冒险怎么样?$R;";

            this.questTooEasy = "唔…但是对你来说，$R;" +
                                "说不定是太简单的任务。$R;" +
                                "$R那样也没关系嘛?$R;";

            this.questTooHard = "这对你来说有点困难啊?$R;" +
                                "$R这样也没关系嘛?$R;"; 
        }
        
        public override void OnEvent(ActorPC pc)
        {
            BitMask<Valentine_Day_00> Valentine_Day_00_mask = pc.CMask["Valentine_Day_00"];                                                                             //活動：情人節
            BitMask<White_Valentine_Day_00> White_Valentine_Day_00_mask = pc.CMask["White_Valentine_Day_00"];                                                           //活動：白色情人節
            BitMask<Halloween_00> Halloween_00_mask = pc.CMask["Halloween_00"];                                                                                         //活動：萬聖節
            BitMask<Hunting_Mushroom_00> Hunting_Mushroom_00_mask = pc.CMask["Hunting_Mushroom_00"];                                                                    //活動：狩獵蘑菇
            BitMask<Emil_Letter> Emil_Letter_mask = pc.CMask["Emil_Letter"];                                                                                            //任務：埃米爾介紹書
            BitMask<Last_Words> Last_Words_mask = pc.CMask["Last_Words"];                                                                                               //任務：古魯杜的遺言

            Valentine_Day_00_mask.SetValue(Valentine_Day_00.情人節活動期間, false);                                                                                     //情人節活動期間                開/關
            White_Valentine_Day_00_mask.SetValue(White_Valentine_Day_00.白色情人節活動期間, false);                                                                     //白色情人節活動期間            開/關
            Halloween_00_mask.SetValue(Halloween_00.萬聖節活動期間, false);                                                                                             //萬聖節活動期間                開/關
            Hunting_Mushroom_00_mask.SetValue(Hunting_Mushroom_00.狩獵蘑菇活動期間, false);                                                                             //狩獵蘑菇活動期間              開/關

            if (Valentine_Day_00_mask.Test(Valentine_Day_00.情人節活動期間) &&
                pc.Fame > 10)
            {
                情人節(pc);
            }

            if (White_Valentine_Day_00_mask.Test(White_Valentine_Day_00.白色情人節活動期間) &&
                pc.Fame > 10)
            {
                白色情人節(pc);
            }

            if (Halloween_00_mask.Test(Halloween_00.萬聖節活動期間))
            {
                萬聖節(pc);
            }

            if (Hunting_Mushroom_00_mask.Test(Hunting_Mushroom_00.狩獵蘑菇活動期間) &&
                pc.Fame > 10)
            {
                狩獵蘑菇(pc);
            }

            if (!Emil_Letter_mask.Test(Emil_Letter.埃米爾介紹書任務完成) &&
                CountItem(pc, 10043081) > 0)
            {
                埃米爾介紹書(pc);
                return;
            }

            //if (!Last_Words_mask.Test(Last_Words.已經與古魯杜的女兒們進行對話) &&
            //    pc.Level >= 35 &&
            //    pc.Fame >= 20)
            //{
            //    古魯杜的遺言(pc);
            //    return;
            //}

            switch (Select(pc, "要不要喝一杯?", "", "买东西", "卖东西", "任务服务台", "什么都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 4);
                    break;

                case 2:
                    OpenShopSell(pc, 4);
                    break;

                case 3:
                    HandleQuest(pc, 6);
                    break;

                case 4:
                    break;
            }            
        }

        void 情人節(ActorPC pc)
        {
            if (pc.Fame > 10)
            {
                咖啡館的情人節禮物(pc);
                return;
            }
        }

        void 咖啡館的情人節禮物(ActorPC pc)
        {
            BitMask<Valentine_Day_01> Valentine_Day_01_mask = pc.CMask["Valentine_Day_01"];                                                                             //活動：咖啡館的情人節禮物(情人節)

            switch (Select(pc, "要不要喝一杯?", "", "买东西", "卖东西", "任务服务台", "交换『酒屋的感谢券』", "什么都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 4);
                    break;

                case 2:
                    OpenShopSell(pc, 4);
                    break;

                case 3:
                    HandleQuest(pc, 6);
                    break;

                case 4:
                    if (!Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花牆紙) &&
                        !Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花地板))
                    {
                        交換情人節禮物(pc);
                        return;
                    }
                    else
                    {
                        PlaySound(pc, 2041, false, 100, 50);

                        Say(pc, 11000002, 131, "你已经交换过了吗?$R;" +
                                               "$R如果有剩下的票，$R;" +
                                               "可以送给朋友喔。$R;", "酒馆老板");                    
                    }
                    break;

                case 5:
                    break;
            }
        }

        void 交換情人節禮物(ActorPC pc)
        {
            BitMask<Valentine_Day_01> Valentine_Day_01_mask = pc.CMask["Valentine_Day_01"];                                                                             //活動：咖啡館的情人節禮物(情人節)

            if(CountItem(pc, 10048002) >= 15)
            {
                if (!Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花牆紙) &&
                    !Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花地板))
                {
                    switch (Select(pc, "想要交换哪一样礼物呢?", "", "交换『心形花纹墙纸』", "交换『心形花纹地板』"))
                    {
                        case 1:
                            交換心花牆紙(pc);
                            return;
                            
                        case 2:
                            交換心花地板(pc);
                            return;
                    }
                }
               
                if (!Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花牆紙) &&
                    Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花地板))
                {
                    交換心花牆紙(pc);
                    return;
                }

                if (Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花牆紙) &&
                    !Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花地板))
                {
                    交換心花地板(pc);
                    return;
                }
            }
            else
            {
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 0, 65535, "『酒屋的感谢券』数量不足。$R;", " ");

                Say(pc, 11000002, 131, "活动期间在「酒馆」承接『情人节系列』任务，$R;" +
                                       "任务成功的话，$R;" +
                                       "就可以得到『酒屋的感谢券』。$R;" +
                                       "$R收集15张『酒屋的感谢券』的话，$R;" +
                                       "就可以交换漂亮的礼物喔。$R;", "酒馆老板");            
            }
        }

        void 交換心花牆紙(ActorPC pc)
        {
            BitMask<Valentine_Day_01> Valentine_Day_01_mask = pc.CMask["Valentine_Day_01"];                                                                             //活動：咖啡館的情人節禮物(情人節)

            Say(pc, 11000002, 131, "哦哦!$R;" +
                                   "$R这不是『酒屋的感谢券』吗?$R;" +
                                   "$P要用15张『酒屋的感谢券』$R;" +
                                   "来交换『心形花纹墙纸』吗?$R;" +
                                   "$R虽然之前就讲过了，$R;" +
                                   "但还是要提醒您一下，$R;" +
                                   "因为礼物有点重，$R;" +
                                   "所以要注意一下啊。$R;", "酒馆老板");

            switch(Select(pc, "怎么办呢?", "", "不交换", "交换"))
            {
                case 1:
                    break;

                case 2:
                    Valentine_Day_01_mask.SetValue(Valentine_Day_01.交換心花牆紙, true);

                    TakeItem(pc, 10048002, 15);

                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 30040012, 1);
                    Say(pc, 0, 65535, "得到『心形花纹墙纸』!$R;", " ");

                    Say(pc, 11000002, 131, "平时总是在麻烦你，$R;" +
                                           "真的非常感谢你呀。$R;" +
                                           "$R以后也会继续期待你的表现!$R;", "酒馆老板");
                    break;
            }
        }

        void 交換心花地板(ActorPC pc)
        {
            BitMask<Valentine_Day_01> Valentine_Day_01_mask = pc.CMask["Valentine_Day_01"];                                                                             //活動：咖啡館的情人節禮物(情人節)

            Say(pc, 11000002, 131, "哦哦!$R;" +
                                   "$R这不是『酒屋的感谢券』吗?$R;" +
                                   "$P要用15张『酒屋的感谢券』$R;" +
                                   "来交换『心形花纹地板』吗?$R;" +
                                   "$R虽然之前就讲过了，$R;" +
                                   "但还是要提醒您一下，$R;" +
                                   "因为礼物有点重，$R;" +
                                   "所以要注意一下啊。$R;", "酒馆老板");

            switch(Select(pc, "怎么办呢?", "", "不交换", "交换"))
            {
                case 1:
                    break;

                case 2:
                    Valentine_Day_01_mask.SetValue(Valentine_Day_01.交換心花牆紙, true);

                    TakeItem(pc, 10048002, 15);

                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 30050113, 1);
                    Say(pc, 0, 65535, "得到『心形花纹墙纸』!$R;", " ");

                    Say(pc, 11000002, 131, "平时总是在麻烦你，$R;" +
                                           "真的非常感谢你呀。$R;" +
                                           "$R以后也会继续期待你的表现!$R;", "酒馆老板");
                    break;
            }
        }

        void 白色情人節(ActorPC pc)
        {
            if (pc.Fame > 10)
            {
                咖啡館的白色情人節禮物(pc);
                return;
            }
        }

        void 咖啡館的白色情人節禮物(ActorPC pc)
        {
            BitMask<White_Valentine_Day_01> White_Valentine_Day_01_mask = pc.CMask["White_Valentine_Day_01"];                                                           //活動：咖啡館的白色情人節禮物(白色情人節)

            switch (Select(pc, "要不要喝一杯?", "", "买东西", "卖东西", "任务服务台", "交换『酒屋的感谢券』", "什么都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 4);
                    break;

                case 2:
                    OpenShopSell(pc, 4);
                    break;

                case 3:
                    HandleQuest(pc, 6);
                    break;

                case 4:
                    if (!White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換小地毯) &&
                        !White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換大地毯))
                    {
                        交換白色情人節禮物(pc);
                        return;
                    }
                    else
                    {
                        PlaySound(pc, 2041, false, 100, 50);

                        Say(pc, 11000002, 131, "你已经交换过了吗?$R;" +
                                               "$R如果有剩下的票，$R;" +
                                               "可以送给朋友喔。$R;", "酒馆老板");
                    }
                    break;

                case 5:
                    break;
            }
        }

        void 交換白色情人節禮物(ActorPC pc)
        {
            BitMask<White_Valentine_Day_01> White_Valentine_Day_01_mask = pc.CMask["White_Valentine_Day_01"];                                                           //活動：咖啡館的白色情人節禮物(白色情人節)

            if (CountItem(pc, 10048002) >= 15)
            {
                if (!White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換小地毯) &&
                    !White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換大地毯))
                {
                    switch (Select(pc, "想要交换哪一样礼物呢?", "", "交换『小地毯 (白色情人节)』", "交换『大地毯 (白色情人节)』"))
                    {
                        case 1:
                            交換小地毯(pc);
                            return;

                        case 2:
                            交換大地毯(pc);
                            return;
                    }
                }

                if (!White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換小地毯) &&
                    White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換大地毯))
                {
                    交換小地毯(pc);
                    return;
                }

                if (White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換小地毯) &&
                    !White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換大地毯))
                {
                    交換大地毯(pc);
                    return;
                }
            }
            else
            {
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 0, 65535, "『酒屋的感谢券』数量不足。$R;", " ");

                Say(pc, 11000002, 131, "活动期间在「酒馆」承接『白色情人节系列』任务，$R;" +
                                       "任务成功的话，$R;" +
                                       "就可以得到『酒屋的感谢券』。$R;" +
                                       "$R收集15张『酒屋的感谢券』的话，$R;" +
                                       "就可以交换漂亮的礼物喔。$R;", "酒馆老板");
            }
        }

        void 交換小地毯(ActorPC pc)
        {
            BitMask<White_Valentine_Day_01> White_Valentine_Day_01_mask = pc.CMask["White_Valentine_Day_01"];                                                           //活動：咖啡館的白色情人節禮物(白色情人節)

            Say(pc, 11000002, 131, "哦哦!$R;" +
                                   "$R这不是『酒屋的感谢券』吗?$R;" +
                                   "$P要用15张『酒屋的感谢券』$R;" +
                                   "来交换『小地毯 (白色情人节)』吗?$R;" +
                                   "$R虽然之前就讲过了，$R;" +
                                   "但还是要提醒您一下，$R;" +
                                   "因为礼物有点重，$R;" +
                                   "所以要注意一下啊。$R;", "酒馆老板");

            switch (Select(pc, "怎么办呢?", "", "不交换", "交换"))
            {
                case 1:
                    break;

                case 2:
                    White_Valentine_Day_01_mask.SetValue(White_Valentine_Day_01.交換小地毯, true);

                    TakeItem(pc, 10048002, 15);

                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 31130027, 1);
                    Say(pc, 0, 65535, "得到『小地毯 (白色情人节)』!$R;", " ");

                    Say(pc, 11000002, 131, "平时总是在麻烦你，$R;" +
                                           "真的非常感谢你呀。$R;" +
                                           "$R以后也会继续期待你的表现!$R;", "酒馆老板");
                    break;
            }
        }

        void 交換大地毯(ActorPC pc)
        {
            BitMask<White_Valentine_Day_01> White_Valentine_Day_01_mask = pc.CMask["White_Valentine_Day_01"];                                                           //活動：咖啡館的白色情人節禮物(白色情人節)

            Say(pc, 11000002, 131, "哦哦!$R;" +
                                   "$R这不是『酒屋的感谢券』吗?$R;" +
                                   "$P要用15张『酒屋的感谢券』$R;" +
                                   "来交换『大地毯 (白色情人节)』吗?$R;" +
                                   "$R虽然之前就讲过了，$R;" +
                                   "但还是要提醒您一下，$R;" +
                                   "因为礼物有点重，$R;" +
                                   "所以要注意一下啊。$R;", "酒馆老板");

            switch (Select(pc, "怎么办呢?", "", "不交换", "交换"))
            {
                case 1:
                    break;

                case 2:
                    White_Valentine_Day_01_mask.SetValue(White_Valentine_Day_01.交換大地毯, true);

                    TakeItem(pc, 10048002, 15);

                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 31130109, 1);
                    Say(pc, 0, 65535, "得到『大地毯 (白色情人节)』!$R;", " ");

                    Say(pc, 11000002, 131, "平时总是在麻烦你，$R;" +
                                           "真的非常感谢你呀。$R;" +
                                           "$R以后也会继续期待你的表现!$R;", "酒馆老板");
                    break;
            }
        }

        void 萬聖節(ActorPC pc)
        {
            BitMask<Halloween_03> Halloween_03_mask = pc.CMask["Halloween_03"];                                                                                         //活動：搗蛋糖(萬聖節)

            if (!Halloween_03_mask.Test(Halloween_03.已經向咖啡館老闆索取搗蛋糖))
            {
                搗蛋糖任務(pc);
                return;
            }
        }

        void 搗蛋糖任務(ActorPC pc)
        {
            BitMask<Halloween_03> Halloween_03_mask = pc.CMask["Halloween_03"];                                                                                         //活動：搗蛋糖(萬聖節)

            switch (Select(pc, "想要怎么做呢?", "", "跟往常一样打招呼", "不给糖，就捣蛋!!"))
            {
                case 1:
                    break;

                case 2:
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
                    {
                        if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025800 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024350 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024351 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024352 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024353 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024354 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024355 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024356 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024357 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024358 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022500 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022600 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022700 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022800)
                        {
                            Say(pc, 11000002, 131, "啊…是妖怪吗?$R;" +
                                                   "$R哟!$R;" +
                                                   "不要做恶作剧，我给你糖果。$R;", "酒馆老板");

                            if (CheckInventory(pc, 10009200, 1))
                            {
                                Halloween_03_mask.SetValue(Halloween_03.已經向咖啡館老闆索取搗蛋糖, true);

                                GiveItem(pc, 10009200, 1);
                            }
                            else
                            {
                                PlaySound(pc, 2041, false, 100, 50);

                                Say(pc, 11000002, 131, "……$R;" +
                                                       "$P行李太多，没办法给您报酬啊?!$R;" +
                                                       "$R可以把不要的道具扔掉一些，$R;" +
                                                       "或者是减少点行李以后，再来吧。$R;", "酒馆老板");                        
                            }
                        }
                    }
                    else
                    {
                        Say(pc, 11000002, 131, "啊…这样不行的啊!$R;" +
                                               "回去打扮以后，再过来吧。$R;", "酒馆老板");                  
                    }
                    break;
            }
        }

        void 狩獵蘑菇(ActorPC pc)
        {
            switch (Select(pc, "要不要喝一杯?", "", "买东西", "卖东西", "任务服务台", "学做『蘑菇料理』", "什么都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 4);
                    break;

                case 2:
                    OpenShopSell(pc, 4);
                    break;

                case 3:
                    HandleQuest(pc, 6);
                    break;

                case 4:
                    學習蘑菇料理(pc);
                    break;

                case 5:
                    break;
            }
        }

        void 學習蘑菇料理(ActorPC pc)
        {
            Say(pc, 11000002, 131, "现在要教你『蘑菇料理』的做法，$R;" +
                                   "你想学哪一种?$R;", "酒馆老板");

            switch (Select(pc, "想要了解哪种烹调方法呢?", "", "蘑菇汤", "蘑菇包", "蘑菇咖喱", "蘑菇大杂烩", "没兴趣"))
            {
                case 1:
                    Say(pc, 11000002, 131, "烹调『蘑菇汤』，$R;" +
                                           "需要有『蘑菇』、『野菜汁』，$R;" +
                                           "这两种材料。$R;", "酒馆老板");
                    break;

                case 2:
                    Say(pc, 11000002, 131, "烹调『蘑菇包』，$R;" +
                                           "需要有『蘑菇』、『面包树果实』、『盐』，$R;" +
                                           "这三种材料。$R;", "酒馆老板");
                    break;

                case 3:
                    Say(pc, 11000002, 131, "烹调『蘑菇咖喱』，$R;" +
                                           "需要有『蘑菇』、『肉』、『矿泉水』$R;" +
                                           "以及『香辛料』，$R;" +
                                           "这四种材料。$R;", "酒馆老板");
                    break;

                case 4:
                    Say(pc, 11000002, 131, "烹调『蘑菇大杂烩』，$R;" +
                                           "需要有『蘑菇』、『高级肉』、『矿泉水』$R;" +
                                           "以及『奶油』，$R;" +
                                           "这四种材料。$R;", "酒馆老板");
                    break;

                case 5:
                    Say(pc, 11000002, 131, "哈…?$R;", "酒馆老板");
                    break;
            }
        }

        void 埃米爾介紹書(ActorPC pc)
        {
            BitMask<Emil_Letter> Emil_Letter_mask = pc.CMask["Emil_Letter"];                                                                                            //任務：埃米爾介紹書

            int selection;

            Emil_Letter_mask.SetValue(Emil_Letter.埃米爾介紹書任務開始, true);

            Say(pc, 11000002, 131, "啊?!$R;" +
                                   "那个莫非是『埃米尔介绍书』?$R;" +
                                   "$R是吗? 来的正好!$R;" +
                                   "$P您是初心者吧?$R;" +
                                   "$R是第一次来这里，很辛苦吧?$R;" +
                                   "我请客! 您随便吃吧!$R;", "酒馆老板");

            PlaySound(pc, 2040, false, 100, 50);
            Heal(pc);
            Say(pc, 0, 65535, "招待了亲自做的蛋糕和红茶！$R;", " ");

            Say(pc, 11000002, 131, "您没有想过要挑战看看任务?$R;", "酒馆老板");

            selection = Select(pc, "想要挑战任务吗?", "", "有什么样的任务?", "挑战任务", "放弃");

            while (selection != 3)
            {
                switch (selection)
                {
                    case 1:
                        任務種類詳細解說(pc);
                        break;

                    case 2:
                        擊退皮露露(pc);
                        break;

                    case 3:
                        break;
                }

                selection = Select(pc, "想要挑战任务吗?", "", "有什么样的任务?", "挑战任务", "放弃");
            }
        }

        void 任務種類詳細解說(ActorPC pc)
        {
            int selection;

            Say(pc, 11000002, 131, "任务的要求几乎都意外的简单!$R;" +
                                   "$R「酒馆」除了提供餐饮外，$R;" +
                                   "也会介绍一些工作给冒险者唷!$R;" +
                                   "$P久而久之，口碑越来越好了，$R;" +
                                   "所以在「阿克罗波利斯」周围，$R;" +
                                   "开了许多分店。$R;" +
                                   "$R出差的时后，一定要去看看啊。$R;" +
                                   "$P任务的内容主要是以$R;" +
                                   "「击退魔物」、「收集/搬运道具」等。$R;" +
                                   "$R当然会根据任务的内容，$R;" +
                                   "给予您等值的报酬!$R;" +
                                   "$P工作内容不同，执行方式也不同。$R;" +
                                   "$R想听详细的说明吗?$R;", "酒馆老板");

            selection = Select(pc, "想听什么说明呢?", "", "任务的注意事项", "关于「击退任务」", "关于「收集任务」", "关于「搬运任务」", "什么也不听");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000002, 131, "成功完成任务的话，$R;" +
                                               "可以得到相对应的经验值和报酬。$R;" +
                                               "$R但托付的任务并不是很多，$R;" +
                                               "有时候会供过于求，不太平衡啊!$R;" +
                                               "$P所以工作是有次数限制的。$R;" +
                                               "$R真是非常抱歉，因为还有别的冒险者，$R;" +
                                               "所以没办法只好这样，请你理解!$R;" +
                                               "$P除此之外，$R;" +
                                               "为了避免有人承接任务却没有回报，$R;" +
                                               "所以任务都定了时限呀!$R;" +
                                               "$R规定时间内没有完成任务，$R;" +
                                               "就会当作任务失败!$R;" +
                                               "这个任务，就会给别的冒险者了。$R;" +
                                               "$P剩余的任务点数和任务所剩时间，$R;" +
                                               "可以在「任务视窗」确认喔。$R;" +
                                               "$R尽量不要失败，$R;" +
                                               "请努力吧!$R;", "酒馆老板");
                        break;

                    case 2:
                        Say(pc, 11000002, 131, "「击退任务」就是要在指定的区域，$R;" +
                                               "抓到指定数量的魔物。$R;" +
                                               "$P例如：$R;" +
                                               "击退「阿克罗尼亚东方平原」的$R;" +
                                               "5只「皮露露」。$R;" +
                                               "$R接受这样的任务时，$R;" +
                                               "只要抓住指定区域的5只「皮露露」，$R;" +
                                               "任务就算成功了!$R;" +
                                               "$P其他地方的「皮露露」，$R;" +
                                               "并不会列入计算的。请多留意呀!$R;" +
                                               "$P委托内容和完成进度等，$R;" +
                                               "可以在「任务视窗」随时确认哦!$R;" +
                                               "$R执行任务时，只要打开这个视窗，$R;" +
                                               "就可以随时确认，很方便吧?$R;" +
                                               "$P任务成功后，要记得回报，$R;" +
                                               "这样才可以拿到报酬喔。$R;" +
                                               "$R关於「报酬」，$R;" +
                                               "可以在任何附近的「任务服务台」拿到，$R;" +
                                               "所以只要到附近的「服务台」就可以了。$R;", "酒馆老板");
                        break;

                    case 3:
                        Say(pc, 11000002, 131, "「收集任务」就是收集指定道具的任务!$R;" +
                                               "$P如果接到收集3个『杰利科』的任务。$R;" +
                                               "$R只要想尽办法收集3个『杰利科』，$R;" +
                                               "就算任务完成了。$R;" +
                                               "$P收集完以后，$R;" +
                                               "把道具拿到「任务服务台」就可以了。$R;" +
                                               "$R接受「收集任务」时，$R;" +
                                               "选择「任务服务台」$R;" +
                                               "就会显示交易视窗喔!$R;" +
                                               "$P把收集的道具，$R;" +
                                               "从道具视窗移到交易视窗的左边，$R;" +
                                               "$R点击『确认』再点击『交易』，$R;" +
                                               "道具就交易到「服务台」了。$R;" +
                                               "$P交易指定数量后，$R;" +
                                               "任务就算成功了。$R;" +
                                               "$R如果道具太重，$R;" +
                                               "一次交易不了，可以分批送出喔。$R;" +
                                               "$P我会清点交易的道具的。$R;" +
                                               "$R我不会算错啦，尽管放心!!$R;", "酒馆老板");
                        break;

                    case 4:
                        Say(pc, 11000002, 131, "「搬运任务」是从委托人那裡取得道具，$R;" +
                                               "然后转交给收件人的任务!$R;" +
                                               "$P例如：$R;" +
                                               "在「下城」的$R;" +
                                               "「酒馆的麦当娜」那里$R;" +
                                               "取得4个『杰利科』。$R;" +
                                               "$R然后拿给「酒馆」的$R;" +
                                               "「酒馆老板」。$R;" +
                                               "$P接到这样的任务的话，$R;" +
                                               "只要从店外的「麦当娜」那裡取得4个『杰利科』，$R;" +
                                               "把道具转交给我，就算成功了。$R;" +
                                               "$P要给予运送道具，$R;" +
                                               "只要跟相关的人交谈就可以了。$R;" +
                                               "$R任务成功以后，$R;" +
                                               "就跟「击退任务」一样，$R;" +
                                               "到「任务服务台」，拿取报酬就可以了。$R;", "酒馆老板");
                        break;
                }

                selection = Select(pc, "想听什么说明呢?", "", "任务的注意事项", "关于「击退任务」", "关于「收集任务」", "关于「搬运任务」", "什么也不听");
            }
        }

        void 擊退皮露露(ActorPC pc)
        {
            BitMask<Emil_Letter> Emil_Letter_mask = pc.CMask["Emil_Letter"];                                                                                            //任務：埃米爾介紹書

            Say(pc, 11000002, 131, "最近「阿克罗波利斯」周围，$R;" +
                                   "出现了很多「皮露露」，$R;" +
                                   "能不能击退呢?$R;" +
                                   "$R「皮露露」是像布丁的天蓝色魔物。$R;" +
                                   "$P在任务清单选择任务后，$R;" +
                                   "点击『确认』，就可以接受任务了。$R;" +
                                   "$R那么，您想挑战吗?$R;", "酒馆老板");

            switch (Select(pc, "想怎么做呢?", "", "挑战任务", "再听一次说明", "放弃"))
            {
                case 1:
                    if (pc.QuestRemaining > 0)
                    {
                        Emil_Letter_mask.SetValue(Emil_Letter.埃米爾介紹書任務完成, true);

                        TakeItem(pc, 10043081, 1);

                        HandleQuest(pc, 1);
                    }
                    else
                    {
                        Say(pc, 11000002, 131, "真是的，任务点数竟然是『0』呀!!$R;" +
                                               "$R只好下次再来吧。$R;", "酒馆老板");
                    }
                    break;

                case 2:
                    擊退皮露露(pc);
                    break;

                case 3:
                    break;
            }
        }

        void 古魯杜的遺言(ActorPC pc)
        {
            BitMask<Last_Words> Last_Words_mask = pc.CMask["Last_Words"];                                                                                               //任務：古魯杜的遺言

            if (!Last_Words_mask.Test(Last_Words.古魯杜的遺言任務開始))
            {
                Say(pc, 11000002, 131, "哦哦! 是" + pc.Name + "呀!$R;" +
                                       "$R不好意思，$R;" +
                                       "有件事情想拜托你帮忙。$R;" +
                                       "$P工作的内容是$R;" +
                                       "担任『遗嘱的见证人』。$R;" +
                                       "$R是不是有点无理啊?$R;" +
                                       "$P但像你一样会守口如瓶，$R;" +
                                       "让人信赖的冒险者不多啊?$R;" +
                                       "$P任务的报酬是『1万金币』。$R;" +
                                       "怎么样? 要接受委托吗?$R;", "酒馆老板");

                switch (Select(pc, "怎么办呢?", "", "但是现在很忙啊…", "接受委托"))
                {
                    case 1:

                        Say(pc, 11000002, 131, "这样啊?$R;" +
                                               "$R那还真是可惜，$R;" +
                                               "如果改变主意的话，$R;" +
                                               "请再跟我说吧。$R;", "酒馆老板");
                        break;

                    case 2:
                        Last_Words_mask.SetValue(Last_Words.古魯杜的遺言任務開始, true);

                        Say(pc, 11000002, 131, "真是感谢啊…$R;" +
                                               "这下令我放下心头上的大石了。$R;" +
                                               "$P这是作为订金的『3000金币』。$R;" +
                                               "$R委託完成后，$R;" +
                                               "会给予剩于的金币的。$R;" +
                                               "$P世界著名的「古鲁杜先生」的$R;" +
                                               "「遗嘱」打开的那天，$R;" +
                                               "去当「见证人」就可以了。$R;" +
                                               "$P他家在「摩戈岛」上，$R;" +
                                               "是位于城市东边的建筑物。$R;" +
                                               "$R准备好就出发吧!$R;" +
                                               "$P他的女儿们都还在这座城市。$R;" +
                                               "$R要出发前去「摩戈岛」前，$R;" +
                                               "最好是去跟她们说说话吧。$R;" +
                                               "$R她们分别叫「铂金」、「银」以及「翡翠」。$R;" +
                                               "$P在「剧场」附近做生意，$R;" +
                                               "三人一组的商人就是她们。$R;" +
                                               "$R因为都是站着的，$R;" +
                                               "所以很容易找到的。$R;" +
                                               "$P这个是『摩戈入国许可证』，$R;" +
                                               "在进入『摩戈岛』的时候会用到的。$R;" +
                                               "$R要好好保管喔!$R;", "酒馆老板");

                        PlaySound(pc, 2040, false, 100, 50);
                        GiveItem(pc, 10041900, 1);
                        pc.Gold += 3000;
                        Say(pc, 0, 65535, "得到『3000金币』!$R;", " ");
                        Say(pc, 0, 65535, "得到『摩戈入国许可证』!$R;", " ");
                        break;
                }
            }
            else
            {
                Say(pc, 11000002, 131, "「古鲁杜先生」的女儿，$R;" +
                                       "她们在「剧场」附近做生意，$R;" +
                                       "去跟她们说说话吧!$R;", "酒馆老板");
            }
        }
    }
}