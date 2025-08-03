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

            Say(pc, 131, "我是管理炼金术师们的$R;" +
                "炼金术师总管$R;");
            if (pc.Job == PC_JOB.FARMASIST && Job2X_10_mask.Test(Job2X_10.轉職開始))//_3a49)
            {
                switch (Select(pc, "做什么呢？", "", "精制道具", "合成药物", "收我为徒吧", "什么也不做。"))
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

                            Say(pc, 131, "那就开始考试了$R;" +
                                "$P从现在开始给您10个题。$R;" +
                                "只要把答案的道具拿来给我就行了$R;" +
                                "$R要全部正确才能通过考试。$R;");
                            int a = Global.Random.Next(1, 3);
                            switch (a)
                            {
                                case 1:
                                    Job2X_10_mask.SetValue(Job2X_10.題型1, true);
                                    Job2X_10_mask.SetValue(Job2X_10.提問第一題, true);

                                    Say(pc, 131, "第一题$R;" +
                                        "$R做治愈药水的药草是什么？$R;" +
                                        "要把两种都答出来。$R;");
                                    break;
                                case 2:
                                    Job2X_10_mask.SetValue(Job2X_10.題型2, true);
                                    Job2X_10_mask.SetValue(Job2X_10.提問第一題, true);

                                    Say(pc, 131, "第一题$R;" +
                                        "$R做治愈药水的药草是什么？$R;" +
                                        "要把两种都答出来。$R;");
                                    break;
                                case 3:
                                    Job2X_10_mask.SetValue(Job2X_10.題型3, true);
                                    Job2X_10_mask.SetValue(Job2X_10.提問第一題, true);

                                    Say(pc, 131, "第一題$R;" +
                                        "$R做治愈药水的药草是什么？$R;" +
                                        "要把两种都答出来。$R;");
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
                        Say(pc, 131, "不管什么时候都可以再来找我$R;");
                        break;
                }
                return;
            }

            if (pc.Job == PC_JOB.FARMASIST && pc.JobLevel1 >= 30)
            {
                switch (Select(pc, "做什么呢？", "", "精制道具", "合成药物", "收我为徒吧", "什么也不做"))
                {
                    case 1:
                        Synthese(pc, 2009, 3);
                        break;
                    case 2:
                        Synthese(pc, 2022, 5);
                        break;
                    case 3:
                        Say(pc, 131, "我不收徒弟的。$R;" +
                            "$R…若想和我一样，$R;" +
                            "做炼金术师怎么样啊？$R;" +
                            "$P要成为炼金术师$R;" +
                            "必须通过行会举办的$R;" +
                            "药物组合技术考试呢$R;" +
                            "$P合格率是0.8%，$R;" +
                            "考试虽然有点难，$R;" +
                            "$R但是是很好的职业呢$R;");
                        switch (Select(pc, "要考试吗？", "", "考试", "我最讨厌考试呢"))
                        {
                            case 1:
                                Job2X_10_mask.SetValue(Job2X_10.轉職開始, true);

                                Say(pc, 131, "好，考官就由我来担任。$R;" +
                                    "$R考题有点多，$R;" +
                                    "但没有时间限制。$R;" +
                                    "所以慢慢尽全力去完成它吧。$R;" +
                                    "$P那么，准备好了$R;" +
                                    "就再来和我说话吧。$R;");
                                break;
                            case 2:
                                Say(pc, 131, "不管什么时候都可以再来找我$R;");
                                break;
                        }
                        break;
                    case 4:
                        Say(pc, 131, "不管什么时候都可以再来找我$R;");
                        break;
                }
                return;
            }

            if (!Job2X_10_mask.Test(Job2X_10.第一場對話))//_3A16)
            {
                Job2X_10_mask.SetValue(Job2X_10.第一場對話, true);

                Say(pc, 131, "我们炼金术师有变幻物质的能力。$R;" +
                    "$R可以做提高物质纯度的精制道具，$R;" +
                    "提取原材料药效成分的合成药物。$R;" +
                    "$P只要把材料拿来的话$R不管什么时候都会帮助您的。$R;");
                return;
            }
            switch (Select(pc, "做什么呢？", "", "精制道具", "合成药物", "收我为徒吧", "什么也不做"))
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
                    Say(pc, 131, "不管什么时候都可以再来找我$R;");
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
                Say(pc, 131, "对$R;" +
                    "$R治愈药水的材料$R是茉莉草和原生草。$R;" +
                    "第二题$R;" +
                    "$R做魔法药水的草药是什么？$R;" +
                    "两种都要说出来呢$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正确的道具呢。$R;" +
                "$R再跟您说一遍。$R;" +
                "$R第一题$R;" +
                "$R做治愈药水的药草是什么？$R;" +
                "要把两种都答出来。$R;");
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
                Say(pc, 131, "对了！$R;" +
                    "$R魔法药水的材料$R是卷叶草或者雨滴草。$R;" +
                    "$P第三题$R;" +
                    "$R耐力药水的材料是什么？$R;" +
                    "两种都要说出来。$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正确的道具。$R;" +
                "$R再跟您说一遍题目。$R;" +
                "$P第二题$R;" +
                "$R做魔法药水的草药是什么？$R;" +
                "两种都要说出来呢$R;");
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
                Say(pc, 131, "对了$R;" +
                    "$R耐力药水的材料$R是锯叶草和神秘草。$R;" +
                    "$P第四题$R;" +
                    "$R多功能药水的材料是什么？$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正确的道具。$R;" +
                "$R再跟您说一遍题目。$R;" +
                "$P第三题$R;" +
                "$R耐力药水的材料是什么？$R;" +
                "两种都要说出来。$R;");
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
                Say(pc, 131, "对了，$R多功能药水的材料$R是车轴草。$R;" +
                    "$P第五题$R;" +
                    "$R复活药水的材料是什么？$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正确的道具。$R;" +
                "$R再跟您説一遍題目。$R;" +
                "$P第四题$R;" +
                "$R多功能药水的材料是什么？$R;");
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
                    Say(pc, 131, "对了$R;" +
                        "复活药水的材料$R是铜锣草$R;" +
                        "$P第六题$R;" +
                        "$R研磨剂的材料是什么？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正确的道具。$R;" +
                    "$R再跟您説一遍題目。$R;" +
                    "$P第五题$R;" +
                    "$R复活药水的材料是什么？$R;");
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
                    Say(pc, 131, "对了$R;" +
                        "$R复活药水的材料$R是铜锣草$R;" +
                        "$P第六题$R;" +
                        "$R制造柔顺剂的液体是什么？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                    "$R再跟您说一遍题目。$R;" +
                    "$P第五题$R;" +
                    "$R复活药水的材料是什么？$R;");
                return;
            }

            if (CountItem(pc, 10004908) >= 1)
            {
                TakeItem(pc, 10004904, 1);
                Job2X_10_mask.SetValue(Job2X_10.提問第六題, true);
                //_4a19 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "对了$R;" +
                    "$R复活药水的材料$R是铜锣草$R;" +
                    "$P第六题$R;" +
                    "$R制造除色剂$R必要的粉末是什么？$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                "$R再跟您说一遍题目。$R;" +
                "$P第五题$R;" +
                "$R复活药水的材料是什么？$R;");

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
                    Say(pc, 131, "对了$R;" +
                        "$R研磨剂的材料$R是石块。$R;" +
                        "$P第七题$R;" +
                        "$R石化药的材料是蒸馏水和什么？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                    "$R再跟您说一遍题目。$R;" +
                    "$P第六题$R;" +
                    "$R研磨剂的材料是什么？$R;");
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
                    Say(pc, 131, "对了，$R是乙醚$R;" +
                        "$P第七题$R;" +
                        "$R制造安眠药的材料是蒸馏水和什么？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                    "$R再跟您说一遍题目。$R;" +
                    "$P第六题$R;" +
                    "$R制造柔顺剂的液体是什么？$R;");
                return;
            }

            if (CountItem(pc, 10001112) >= 1)
            {
                TakeItem(pc, 10001112, 1);
                Job2X_10_mask.SetValue(Job2X_10.提問第七題, true);
                //_4a20 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "对了，$R那个材料就是无色粉末$R;" +
                    "$P第七题$R;" +
                    "制造沉默药的材料是蒸馏水和什么？$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                "$R再跟您说一遍题目。$R;" +
                "$P第六题$R;" +
                "$R制造除色剂$R必要的粉末是什么？$R;");
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
                    Say(pc, 131, "对了，$R答案是蒸馏水和$R;" +
                        "蛇鸡的眼珠$R;" +
                        "$P第八题$R;" +
                        "$R生发素里要使用的液体是什么？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                    "$R再跟您说一遍题目。$R;" +
                    "$P第七题$R;" +
                    "$R石化药的材料是蒸馏水和什么？$R;");
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
                    Say(pc, 131, "对，$R材料是蒸馏水和植物的刺。$R;" +
                        "$P第八题$R;" +
                        "$R制造头发颜色的液体是什么？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                    "$R再跟您说一遍题目。$R;" +
                    "$P第七题$R;" +
                    "$R制造安眠药的材料是蒸馏水和什么？$R;");
                return;
            }

            if (CountItem(pc, 10009150) >= 1)
            {
                TakeItem(pc, 10009150, 1);
                Job2X_10_mask.SetValue(Job2X_10.提問第八題, true);
                //_4a21 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "$R材料就是$R蒸馏水和雪花糖。$R;" +
                    "$P第八题$R;" +
                    "$R杰利科药水的材料是？$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                "$R再跟您说一遍题目。$R;" +
                "$P第七题$R;" +
                "$R制造沉默药的材料是蒸馏水和什么？$R;");
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
                    Say(pc, 131, "对了$R;" +
                        "是用对头发有益的树皮精华素做的。$R;" +
                        "$P第九题$R;" +
                        "$R制造火焰香水时$R必要的植物是什么？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                    "$R再跟您说一遍题目。$R;" +
                    "$P第八题$R;" +
                    "$R生发素里要使用的液体是什么？$R;");
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
                    Say(pc, 131, "对，$R就是搬运爬爬虫油。$R;" +
                        "$P第九题$R;" +
                        "$R制造光明香水的植物是什么？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                    "$R再跟您说一遍题目。$R;" +
                    "$P第八题$R;" +
                    "$R制造头发颜色的液体是什么？$R;");
                return;
            }

            if (CountItem(pc, 10032800) >= 1)
            {
                TakeItem(pc, 10032800, 1);
                Job2X_10_mask.SetValue(Job2X_10.提問第九題, true);
                //_4a22 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "对，$R材料就是杰利科$R;" +
                    "$P第九题$R;" +
                    "$R水灵香水的制造材料是什么植物？$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                "$R再跟您说一遍题目。$R;" +
                "$P第八题$R;" +
                "$R杰利科药水的材料是？$R;");

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
                    Say(pc, 131, "对了$R;" +
                        "$R制造火焰香水时$R必要的植物是红色花瓣。$R;" +
                        "$P最后的问题$R;" +
                        "$R制造胶水的液体是什么？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                    "$R再跟您说一遍题目。$R;" +
                    "$P第九题$R;" +
                    "$R制造火焰香水时$R必要的植物是什么？$R;");
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
                    Say(pc, 131, "对，$R就是白色花瓣。$R;" +
                        "$P最后一题$R;" +
                        "$R制作金溶液的液体是什么？$R;");
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                    "$R再跟您说一遍题目。$R;" +
                    "$P第九题$R;" +
                    "$R制造光明香水的植物是什么？$R;");
                return;
            }

            if (CountItem(pc, 10043203) >= 1)
            {
                TakeItem(pc, 10043203, 1);
                Job2X_10_mask.SetValue(Job2X_10.提問第十題, true);
                //_4a23 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "对，$R材料就是蓝色花瓣$R;" +
                    "$P最后一题$R;" +
                    "$R制造毒药的材料是蒸馏水和什么？$R;");
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                "$R再跟您说一遍题目。$R;" +
                "$P第九题$R;" +
                "$R水灵香水的制造材料是什么植物？$R;");


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
                    Say(pc, 131, "答对了，$R制造胶水的液体是雷鱼的汗。$R;");
                    轉職(pc);
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                    "$R再跟您说一遍题目。$R;" +
                    "$P最后的问题$R;" +
                    "$R制造胶水的液体是什么？$R;");
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
                    Say(pc, 131, "对了，$R就是毒药。$R;");
                    轉職(pc);
                    return;
                }
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                    "$R再跟您说一遍题目。$R;" +
                    "$P最后一题$R;" +
                    "$R制作金溶液的液体是什么？$R;");
                return;
            }

            if (CountItem(pc, 10045500) >= 1)
            {
                TakeItem(pc, 10045500, 1);
                Job2X_10_mask.SetValue(Job2X_10.所有問題回答正確, true);
                //_3a54 = true;
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "对，$R毒药的材料是蒸馏水和蜘蛛酒$R;");
                轉職(pc);
                return;
            }
            PlaySound(pc, 2041, false, 100, 50);
            Say(pc, 131, "真是可惜啊，$R您好像没有正确道具呢。$R;" +
                "$R再跟您说一遍题目。$R;" +
                "$P最后一题$R;" +
                "$R制造毒药的材料是蒸馏水和什么？$R;");
        }

        void 轉職(ActorPC pc)
        {
            Say(pc, 131, "…$R;" +
                "$P啊？$R;" +
                "都答对了？$R;" +
                "您太厉害了$R;" +
                "$R恭喜您，考试合格了！$R;" +
                "$P好，您现在开始就是炼金术师了$R;" +
                "$R那么就要举行认定仪式了$R;" +
                "就在这里吧！$R;" +
                pc.Name + "！ 往前！$R;");

            Say(pc, 131, "嗯哼！$R;" +
                pc.Name + "呀！$R;" +
                "$R您取得了炼金术师的资格$R;" +
                "恭喜您。$R;");
            轉職選擇(pc);
        }

        void 轉職選擇(ActorPC pc)
        {
            switch (Select(pc, "真的要转职吗?", "", "我想成为炼金术师", "听取关于炼金术师的注意事项", "还是算了吧"))
            {
                case 1:
                    Say(pc, 131, "那么就给您纹上这象征炼金术师的$R;" +"『炼金术师纹章』吧$R;");
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
                            "$R从今以后，$R您就成为代表我们的『炼金术师』了。$R;");
                        PlaySound(pc, 4012, false, 100, 50);
                        Say(pc, 131, "转职成功$R;");
                        return;
                    }
                    Say(pc, 131, "…$R;" +
                        "防御太高的话，就无法烙印纹章了$R;" +
                        "请换上轻便的服装后，再来吧。$R;");
                    break;
                case 2:
                    Say(pc, 131, "先跟您说清楚了，$R转职的话职业等级会变回1级。$R;" +
                        "$P農夫的技能在轉職以後好像還可以學。$R;" +
                        "$R但是有要注意的地方。$R;" +
                        "$P技能点数是按照职业完全分开的。$R;" +
                        "$R农夫的技能点数$R只能在农夫的时候取得。$R;" +
                        "$P转职之前的技能点数虽然还会留着，$R但是转职之后就不会再增加了。$R;" +
                        "$P当然，转职前的职业等级也不会提高了。$R;" +
                        "$R如果还有想学的技能$R就在转职之前学吧。$R;");
                    轉職選擇(pc);
                    break;
                case 3:
                    Say(pc, 131, "不论什么时候都可以再来找我$R;");
                    break;
            }
        }
    }
}
