using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:鍊金術師行會(30048000) NPC基本信息:鍊金術師總管(11000023) X:3 Y:3
namespace SagaScript.M30048000
{
    public class S11000023 : Event
    {
        public S11000023()
        {
            this.EventID = 11000023;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_10> Job2X_10_mask = pc.CMask["Job2X_10"];

            Say(pc, 131, "我是管理鍊金術師們的$R;" +
                "鍊金術師總管$R;");
            if (pc.Job == PC_JOB.FARMASIST && Job2X_10_mask.Test(Job2X_10.轉職開始))//_3a49)
            {
                switch (Select(pc, "做什麼呢？", "", "精製道具", "合成藥物", "收我為徒吧", "什麼也不做。"))
                {
                    case 1:
                        Synthese(pc, 2009, 3);
                        break;
                    case 2:
                        Synthese(pc, 2022, 5);
                        break;
                    case 3:
                        if (!Job2X_10_mask.Test(Job2X_10.提問開始))//_3a50)
                        {
                            Job2X_10_mask.SetValue(Job2X_10.提問開始, true);

                            Say(pc, 131, "那就開始考試了$R;" +
                                "$P從現在開始給您10個題。$R;" +
                                "只要把答案的道具拿來給我就行了$R;" +
                                "$R要全部正確才能通過考試喔。$R;");
                            int a = Global.Random.Next(1, 3);
                            switch (a)
                            {
                                case 1:
                                    Job2X_10_mask.SetValue(Job2X_10.題型1, true);
                                    Job2X_10_mask.SetValue(Job2X_10.提問第一題, true);

                                    Say(pc, 131, "第一題$R;" +
                                        "$R做治癒藥水的藥草是什麼？$R;" +
                                        "要把兩種都答出來。$R;");
                                    break;
                                case 2:
                                    Job2X_10_mask.SetValue(Job2X_10.題型2, true);
                                    Job2X_10_mask.SetValue(Job2X_10.提問第一題, true);

                                    Say(pc, 131, "第一題$R;" +
                                        "$R做治癒藥水的藥草是什麼？$R;" +
                                        "要把兩種都答出來。$R;");
                                    break;
                                case 3:
                                    Job2X_10_mask.SetValue(Job2X_10.題型3, true);
                                    Job2X_10_mask.SetValue(Job2X_10.提問第一題, true);

                                    Say(pc, 131, "第一題$R;" +
                                        "$R做治癒藥水的藥草是什麼？$R;" +
                                        "要把兩種都答出來。$R;");
                                    break;
                            }
                            return;
                        }
                        if (Job2X_10_mask.Test(Job2X_10.所有問題回答正確))
                        {
                            轉職(pc);
                            return;
                        }
                        if (Job2X_10_mask.Test(Job2X_10.提問第十題))
                        {
                            回答第十題(pc);
                            return;
                        }
                        if (Job2X_10_mask.Test(Job2X_10.提問第九題))
                        {
                            回答第九題(pc);
                            return;
                        }
                        if (Job2X_10_mask.Test(Job2X_10.提問第八題))
                        {
                            回答第八題(pc);
                            return;
                        }
                        if (Job2X_10_mask.Test(Job2X_10.提問第七題))
                        {
                            回答第七題(pc);
                            return;
                        }
                        if (Job2X_10_mask.Test(Job2X_10.提問第六題))
                        {
                            回答第六題(pc);
                            return;
                        }
                        if (Job2X_10_mask.Test(Job2X_10.提問第五題))
                        {
                            回答第五題(pc);
                            return;
                        }
                        if (Job2X_10_mask.Test(Job2X_10.提問第四題))
                        {
                            回答第四題(pc);
                            return;
                        }
                        if (Job2X_10_mask.Test(Job2X_10.提問第三題))
                        {
                            回答第三題(pc);
                            return;
                        }
                        if (Job2X_10_mask.Test(Job2X_10.提問第二題))
                        {
                            回答第二題(pc);
                            return;
                        }
                        if (Job2X_10_mask.Test(Job2X_10.提問第一題))
                        {
                            回答第一題(pc);
                            return;
                        }
                        break;
                    case 4:
                        Say(pc, 131, "不論什麼時候都可以再來找我喔$R;");
                        break;
                }
                return;
            }

            if (pc.Job == PC_JOB.FARMASIST && pc.JobLevel1 >= 30)
            {
                switch (Select(pc, "做什麼呢？", "", "精製道具", "合成藥物", "收我為徒吧", "什麼也不做"))
                {
                    case 1:
                        Synthese(pc, 2009, 3);
                        break;
                    case 2:
                        Synthese(pc, 2022, 5);
                        break;
                    case 3:
                        Say(pc, 131, "我不收徒弟的。$R;" +
                            "$R…若想跟我一樣，$R;" +
                            "做鍊金術師怎麼樣啊？$R;" +
                            "$P要成為鍊金術師$R;" +
                            "必須通過行會舉辦的$R;" +
                            "藥物組合技術考試呢$R;" +
                            "$P合格率是0.8%，$R;" +
                            "考試雖然有點難，$R;" +
                            "$R但是是很好的職業喔$R;");
                        switch (Select(pc, "要考試嗎？", "", "考試", "我最討厭考試呢"))
                        {
                            case 1:
                                Job2X_10_mask.SetValue(Job2X_10.轉職開始, true);

                                Say(pc, 131, "好，考官就由我來擔任。$R;" +
                                    "$R考試有點長，$R;" +
                                    "但是没有時間限制。$R;" +
                                    "所以慢慢的盡全力去完成它吧。$R;" +
                                    "$P那麼準備好了$R;" +
                                    "就再來和我説話吧。$R;");
                                break;
                            case 2:
                                Say(pc, 131, "不論什麼時候都可以再來找我喔$R;");
                                break;
                        }
                        break;
                    case 4:
                        Say(pc, 131, "不論什麼時候都可以再來找我喔$R;");
                        break;
                }
                return;
            }

            if (!Job2X_10_mask.Test(Job2X_10.第一場對話))//_3A16)
            {
                Job2X_10_mask.SetValue(Job2X_10.第一場對話, true);

                Say(pc, 131, "我們鍊金術師有變換物質的能力。$R;" +
                    "$R可以做提高物質純度的精製道具，$R;" +
                    "提取原材料藥效成分的合成藥物。$R;" +
                    "$P只要把材料拿來的話$R不管什麼時候都會幫助您的喔。$R;");
                return;
            }
            switch (Select(pc, "做什麼呢？", "", "煉製道具", "合成藥物", "收我為徒吧", "什麼也不做"))
            {
                case 1:
                    Synthese(pc, 2009, 3);
                    break;
                case 2:
                    Synthese(pc, 2022, 5);
                    break;
                case 3:
                    Say(pc, 131, "我不收弟子的$R;");
                    break;
                case 4:
                    Say(pc, 131, "不論什麼時候都可以再來找我喔$R;");
                    break;
            }
        }

        void 回答第一題(ActorPC pc)
        {
            BitMask<Job2X_10> Job2X_10_mask = pc.CMask["Job2X_10"];

            if (CountItem(pc, 10004901) >= 1 && CountItem(pc, 10004903) >= 1)
            {
                TakeItem(pc, 10004901, 1);
                TakeItem(pc, 10004903, 1);
                Job2X_10_mask.SetValue(Job2X_10.提問第二題, true);
                //_4a15 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "對$R;" +
                    "$R治癒藥水的材料$R是苦草和辛香草。$R;" +
                    "第二題$R;" +
                    "$R做魔法藥水的草藥是什麼？$R;" +
                    "兩種都要説出來呢$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                "$R再跟您説一遍。$R;" +
                "$R第一題$R;" +
                "$R做治癒藥水的藥草是什麼？$R;" +
                "要把兩種都答出來。$R;");
        }

        void 回答第二題(ActorPC pc)
        {
            BitMask<Job2X_10> Job2X_10_mask = pc.CMask["Job2X_10"];

            if (CountItem(pc, 10004906) >= 1 && CountItem(pc, 10004907) >= 1)
            {
                TakeItem(pc, 10004906, 1);
                TakeItem(pc, 10004907, 1);
                Job2X_10_mask.SetValue(Job2X_10.提問第三題, true);
                //_4a16 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "對了！$R;" +
                    "$R魔法藥水的材料$R是甜草或者甜香草。$R;" +
                    "$P第三題$R;" +
                    "$R耐力藥水的材料是什麼？$R;" +
                    "兩種都要説出來。$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                "$R再跟您説一遍題目。$R;" +
                "$P第二題$R;" +
                "$R做魔法藥水的草藥是什麼？$R;" +
                "兩種都要説出來呢$R;");
        }

        void 回答第三題(ActorPC pc)
        {
            BitMask<Job2X_10> Job2X_10_mask = pc.CMask["Job2X_10"];

            if (CountItem(pc, 10004905) >= 1 && CountItem(pc, 10004902) >= 1)
            {
                TakeItem(pc, 10004905, 1);
                TakeItem(pc, 10004902, 1);
                Job2X_10_mask.SetValue(Job2X_10.提問第四題, true);
                //_4a17 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "對了$R;" +
                    "$R耐力藥水的材料$R是香酸草和香草。$R;" +
                    "$P第四題$R;" +
                    "$R多功能藥水的材料是什麼？$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                "$R再跟您説一遍題目。$R;" +
                "$P第三題$R;" +
                "$R耐力藥水的材料是什麼？$R;" +
                "兩種都要説出來。$R;");
        }

        void 回答第四題(ActorPC pc)
        {
            BitMask<Job2X_10> Job2X_10_mask = pc.CMask["Job2X_10"];

            if (CountItem(pc, 10004904) >= 1)
            {
                TakeItem(pc, 10004904, 1);
                Job2X_10_mask.SetValue(Job2X_10.提問第五題, true);
                //_4a18 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "對了，$R多功能藥水的材料$R是獨特香辛草。$R;" +
                    "$P第五題$R;" +
                    "$R復活藥水的材料是什麼？$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                "$R再跟您説一遍題目。$R;" +
                "$P第四題$R;" +
                "$R多功能藥水的材料是什麼？$R;");
        }

        void 回答第五題(ActorPC pc)
        {
            BitMask<Job2X_10> Job2X_10_mask = pc.CMask["Job2X_10"];

            if (Job2X_10_mask.Test(Job2X_10.題型1))
            {
                if (CountItem(pc, 10004908) >= 1)
                {
                    TakeItem(pc, 10004908, 1);
                    Job2X_10_mask.SetValue(Job2X_10.提問第六題, true);
                    //_4a19 = true;
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "對了$R;" +
                        "復活藥水的材料$R是獨特香草$R;" +
                        "$P第六題$R;" +
                        "$R硏磨劑的材料是什麼？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                    "$R再跟您説一遍題目。$R;" +
                    "$P第五題$R;" +
                    "$R復活藥水的材料是什麼？$R;");
                return;
            }

            if (Job2X_10_mask.Test(Job2X_10.題型2))
            {
                if (CountItem(pc, 10004908) >= 1)
                {
                    TakeItem(pc, 10004904, 1);
                    Job2X_10_mask.SetValue(Job2X_10.提問第六題, true);
                    //_4a19 = true;
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "對了$R;" +
                        "$R復活藥水的材料是獨特香草$R;" +
                        "$P第六題$R;" +
                        "$R製造柔順劑的液體是什麼？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                    "$R再跟您説一遍題目。$R;" +
                    "$P第五題$R;" +
                    "$R復活藥水的材料是什麼？$R;");
                return;
            }

            if (CountItem(pc, 10004908) >= 1)
            {
                TakeItem(pc, 10004904, 1);
                Job2X_10_mask.SetValue(Job2X_10.提問第六題, true);
                //_4a19 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "對了$R;" +
                    "$R復活藥水的材料是$R獨特香草$R;" +
                    "$P第六題$R;" +
                    "$R製造顏料去除劑$R必要的粉末是什麼？$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                "$R再跟您説一遍題目。$R;" +
                "$P第五題$R;" +
                "$R復活藥水的材料是什麼？$R;");

        }

        void 回答第六題(ActorPC pc)
        {
            BitMask<Job2X_10> Job2X_10_mask = pc.CMask["Job2X_10"];

            if (Job2X_10_mask.Test(Job2X_10.題型1))
            {
                if (CountItem(pc, 10014600) >= 1)
                {
                    TakeItem(pc, 10014600, 1);
                    Job2X_10_mask.SetValue(Job2X_10.提問第七題, true);
                    //_4a20 = true;
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "對了$R;" +
                        "$R研磨劑的材料$R是鵝卵石。$R;" +
                        "$P第七題$R;" +
                        "$R石化藥的材料是蒸餾水和什麼？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                    "$R再跟您説一遍題目。$R;" +
                    "$P第六題$R;" +
                    "$R研磨劑的材料是什麼？$R;");
                return;
            }

            if (Job2X_10_mask.Test(Job2X_10.題型2))
            {
                if (CountItem(pc, 10000304) >= 1)
                {
                    TakeItem(pc, 10000304, 1);
                    Job2X_10_mask.SetValue(Job2X_10.提問第七題, true);
                    //_4a20 = true;
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "對了，$R是乙醚$R;" +
                        "$P第七題$R;" +
                        "$R製造安眠藥的材料是蒸餾水和什麼？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                    "$R再跟您説一遍題目。$R;" +
                    "$P第六題$R;" +
                    "$R製造柔順劑的液體是什麼？$R;");
                return;
            }

            if (CountItem(pc, 10001112) >= 1)
            {
                TakeItem(pc, 10001112, 1);
                Job2X_10_mask.SetValue(Job2X_10.提問第七題, true);
                //_4a20 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "對了，$R那個材料就是無色粉末$R;" +
                    "$P第七題$R;" +
                    "製造沉默藥的材料是蒸餾水和什麼？$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                "$R再跟您説一遍題目。$R;" +
                "$P第六題$R;" +
                "$R製造顏料去除劑$R必要的粉末是什麼？$R;");
        }

        void 回答第七題(ActorPC pc)
        {
            BitMask<Job2X_10> Job2X_10_mask = pc.CMask["Job2X_10"];

            if (Job2X_10_mask.Test(Job2X_10.題型1))
            {
                if (CountItem(pc, 10012550) >= 1)
                {
                    TakeItem(pc, 10012550, 1);
                    Job2X_10_mask.SetValue(Job2X_10.提問第八題, true);
                    //_4a21 = true;
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "對了，$R答案是蒸餾水和$R;" +
                        "古代咕咕雞的眼睛$R;" +
                        "$P第八題$R;" +
                        "$R護髮劑裡要使用的液體是什麼？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                    "$R再跟您説一遍題目。$R;" +
                    "$P第七題$R;" +
                    "$R石化藥的材料是蒸餾水和什麼？$R;");
                return;
            }

            if (Job2X_10_mask.Test(Job2X_10.題型2))
            {
                if (CountItem(pc, 10025205) >= 1)
                {
                    TakeItem(pc, 10025205, 1);
                    Job2X_10_mask.SetValue(Job2X_10.提問第八題, true);
                    //_4a21 = true;
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "對，$R材料是蒸餾水和薔薇花刺。$R;" +
                        "$P第八題$R;" +
                        "$R製造頭髮顏色的液體是什麼？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                    "$R再跟您説一遍題目。$R;" +
                    "$P第七題$R;" +
                    "$R製造安眠藥的材料是蒸餾水和什麼？$R;");
                return;
            }

            if (CountItem(pc, 10009150) >= 1)
            {
                TakeItem(pc, 10009150, 1);
                Job2X_10_mask.SetValue(Job2X_10.提問第八題, true);
                //_4a21 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "$R材料就是$R蒸餾水和雪花。$R;" +
                    "$P第八題$R;" +
                    "$R杰利科藥水的材料是？$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                "$R再跟您説一遍題目。$R;" +
                "$P第七題$R;" +
                "$R製造沉默藥的材料是蒸餾水和什麼？$R;");
        }

        void 回答第八題(ActorPC pc)
        {
            BitMask<Job2X_10> Job2X_10_mask = pc.CMask["Job2X_10"];
            
            if (Job2X_10_mask.Test(Job2X_10.題型1))
            {
                if (CountItem(pc, 10001905) >= 1)
                {
                    TakeItem(pc, 10001905, 1);
                    Job2X_10_mask.SetValue(Job2X_10.提問第九題, true);
                    //_4a22 = true;
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "對了$R;" +
                        "是用對毛髮有益的樹皮精華素做的。$R;" +
                        "$P第九題$R;" +
                        "$R製造火焰香水時$R必要的植物是什麼？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                    "$R再跟您説一遍題目。$R;" +
                    "$P第八題$R;" +
                    "$R護髮劑裡要使用的液體是什麼？$R;");
                return;
            }

            if (Job2X_10_mask.Test(Job2X_10.題型2))
            {
                if (CountItem(pc, 10001907) >= 1)
                {
                    TakeItem(pc, 10001907, 1);
                    Job2X_10_mask.SetValue(Job2X_10.提問第九題, true);
                    //_4a22 = true;
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "對，$R就是凱莉娥油。$R;" +
                        "$P第九題$R;" +
                        "$R製造光明香水的植物是什麼？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                    "$R再跟您説一遍題目。$R;" +
                    "$P第八題$R;" +
                    "$R製造頭髮顏色的液體是什麼？$R;");
                return;
            }

            if (CountItem(pc, 10032800) >= 1)
            {
                TakeItem(pc, 10032800, 1);
                Job2X_10_mask.SetValue(Job2X_10.提問第九題, true);
                //_4a22 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "對，$R材料就是杰利科$R;" +
                    "$P第九題$R;" +
                    "$R水靈香水的製造材料是什麼植物？$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                "$R再跟您説一遍題目。$R;" +
                "$P第八題$R;" +
                "$R杰利科藥水的材料是？$R;");

        }

        void 回答第九題(ActorPC pc)
        {
            BitMask<Job2X_10> Job2X_10_mask = pc.CMask["Job2X_10"];

            if (Job2X_10_mask.Test(Job2X_10.題型1))
            {
                if (CountItem(pc, 10043201) >= 1)
                {
                    TakeItem(pc, 10043201, 1);
                    Job2X_10_mask.SetValue(Job2X_10.提問第十題, true);
                    //_4a23 = true;
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "對了$R;" +
                        "$R製造火焰香水時$R必要的植物是紅色花瓣。$R;" +
                        "$P最後的問題$R;" +
                        "$R製造膠水的液體是什麼？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                    "$R再跟您説一遍題目。$R;" +
                    "$P第九題$R;" +
                    "$R製造火焰香水時$R必要的植物是什麼？$R;");
                return;
            }

            if (Job2X_10_mask.Test(Job2X_10.題型2))
            {
                if (CountItem(pc, 10043204) >= 1)
                {
                    TakeItem(pc, 10043204, 1);
                    Job2X_10_mask.SetValue(Job2X_10.提問第十題, true);
                    //_4a23 = true;
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "對，$R就是白色花瓣。$R;" +
                        "$P最後一題$R;" +
                        "$R製作鍍金液的液體是什麼？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                    "$R再跟您説一遍題目。$R;" +
                    "$P第九題$R;" +
                    "$R製造光明香水的植物是什麼？$R;");
                return;
            }

            if (CountItem(pc, 10043203) >= 1)
            {
                TakeItem(pc, 10043203, 1);
                Job2X_10_mask.SetValue(Job2X_10.提問第十題, true);
                //_4a23 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "對，$R材料就是藍色花瓣$R;" +
                    "$P最後一題$R;" +
                    "$R製造毒藥的材料是蒸餾水和什麼？$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                "$R再跟您説一遍題目。$R;" +
                "$P第九題$R;" +
                "$R水靈香水的製造材料是什麼植物？$R;");


        }
        void 回答第十題(ActorPC pc)
        {
            BitMask<Job2X_10> Job2X_10_mask = pc.CMask["Job2X_10"];

            if (Job2X_10_mask.Test(Job2X_10.題型1))
            {
                if (CountItem(pc, 10008600) >= 1)
                {
                    TakeItem(pc, 10008600, 1);
                    Job2X_10_mask.SetValue(Job2X_10.所有問題回答正確, true);
                    //_3a54 = true;
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "答對了，$R製造膠水的液體是雷魚的汗。$R;");
                    轉職(pc);
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                    "$R再跟您説一遍題目。$R;" +
                    "$P最後的問題$R;" +
                    "$R製造膠水的液體是什麼？$R;");
                return;
            }

            if (Job2X_10_mask.Test(Job2X_10.題型2))
            {
                if (CountItem(pc, 10000302) >= 1)
                {
                    TakeItem(pc, 10000302, 1);
                    Job2X_10_mask.SetValue(Job2X_10.所有問題回答正確, true);
                    //_3a54 = true;
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "對了，$R就是毒藥。$R;");
                    轉職(pc);
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                    "$R再跟您説一遍題目。$R;" +
                    "$P最後一題$R;" +
                    "$R製作鍍金液的液體是什麼？$R;");
                return;
            }

            if (CountItem(pc, 10045500) >= 1)
            {
                TakeItem(pc, 10045500, 1);
                Job2X_10_mask.SetValue(Job2X_10.所有問題回答正確, true);
                //_3a54 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "對，$R毒藥的材料是蒸餾水和蜘蛛酒$R;");
                轉職(pc);
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正確道具呢。$R;" +
                "$R再跟您説一遍題目。$R;" +
                "$P最後一題$R;" +
                "$R製造毒藥的材料是蒸餾水和什麼？$R;");
        }

        void 轉職(ActorPC pc)
        {
            Say(pc, 131, "…$R;" +
                "$P啊？$R;" +
                "都答對了？$R;" +
                "您太厲害了$R;" +
                "$R恭喜您，考試合格了！$R;" +
                "$P好，您現在開始就是鍊金術師了$R;" +
                "$R那麼就要舉行認定儀式了$R;" +
                "就在這裡吧！$R;" +
                pc.Name + "！ 往前！$R;");

            Say(pc, 131, "嗯哼！$R;" +
                pc.Name + "呀！$R;" +
                "$R您取得了鍊金術師的資格$R;" +
                "恭喜您。$R;");
            轉職選擇(pc);
        }

        void 轉職選擇(ActorPC pc)
        {
            switch (Select(pc, "真的要轉職嗎?", "", "我想成為鍊金術師", "聽取關於鍊金術師的注意事項", "還是算了吧"))
            {
                case 1:
                    Say(pc, 131, "那麼就給您烙印上這象徵鍊金術師的$R;" +
                        "『鍊金術師紋章』吧$R;");
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        ChangePlayerJob(pc, PC_JOB.ALCHEMIST);
                        pc.JEXP = 0;
                        //PARAM ME.JOB = 93
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 4000);
                        Say(pc, 131, "…$R;" +
                            "$P好，烙印好了$R;" +
                            "$R從今以後，$R您就成為代表我們的『鍊金術師』了。$R;");
                        PlaySound(pc, 4012, false, 100, 50);
                        Say(pc, 131, "轉職成功$R;");
                        return;
                    }
                    Say(pc, 131, "…$R;" +
                        "防禦太高的話，就無法烙印紋章了$R;" +
                        "請換上輕便的服裝後，再來吧。$R;");
                    break;
                case 2:
                    Say(pc, 131, "先跟您説清楚了，$R轉職的話職業等級會變回1級。$R;" +
                        "$P農夫的技能在轉職以後好像還可以學。$R;" +
                        "$R但是有要注意的地方。$R;" +
                        "$P技能點數是按照職業完全分開的。$R;" +
                        "$R農夫的技能點數$R只能在農夫的時候取得。$R;" +
                        "$P轉職之前的技能點數雖然還會留著，$R但是轉職以後就不會再增加了。$R;" +
                        "$P當然，轉職前的職業等級也不會提高。$R;" +
                        "$R如果還有想學的技能的話$R就在轉職之前學吧。$R;");
                    轉職選擇(pc);
                    break;
                case 3:
                    Say(pc, 131, "不論什麼時候都可以再來找我喔$R;");
                    break;
            }
        }
    }
}
