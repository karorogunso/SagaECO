
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S60000017 : Event
    {
        public S60000017()
        {
            this.EventID = 60000017;
        }
        #region 各种检测
        void 重置每日(ActorPC pc)
        {
            if (pc.AStr["熊熊儿童节每日记录"] != DateTime.Now.ToString("d"))
            {
                pc.AInt["熊熊儿童节100任务点兑换次数"] = 0;
                pc.AInt["熊熊儿童节强化石兑换次数"] = 0;
                pc.AStr["熊熊儿童节每日记录"] = DateTime.Now.ToString("d");
            }
        }
        bool 异常检查(ActorPC pc)
        {
            if (pc.AInt["熊熊儿童节EFFECT兑换次数"] >= 1 && CountItem(pc, 61083200) == 1)
            {
                pc.AInt["熊熊儿童节EFFECT兑换次数"] = 0;
                pc.AInt["熊熊儿童节EFFECT兑换次数2"] = 1;
                return true;

            }
            return true;
        }
        #endregion
        #region 熊熊儿童节
        void 熊熊儿童节(ActorPC pc)
        {
            if (异常检查(pc) == false) //如果检查返回false，则
                return;//结束脚本
            重置每日(pc);
            uint itemid = 952000002;

            ushort n1 = (ushort)(pc.AInt["熊熊儿童节100任务点兑换次数"] * 300 + 300);
            string s1 = n1.ToString() + "个";
            if (pc.AInt["熊熊儿童节100任务点兑换次数"] >= 3) s1 = "无法兑换";
            string op1 = "·100点任务点卷[今日限制:" + pc.AInt["熊熊儿童节100任务点兑换次数"] + "/3]需要:" + s1;

            ushort n2 = (ushort)(pc.AInt["熊熊儿童节强化石兑换次数"] * 50 + 50);
            string s2 = n2.ToString() + "个";
            if (pc.AInt["熊熊儿童节强化石兑换次数"] >= 5) s2 = "无法兑换";
            string op2 = "·强化石头[今日限制:" + pc.AInt["熊熊儿童节强化石兑换次数"] + "/5]需要:" + s2;

            ushort n3 = 5000;
            string s3 = n3.ToString() + "个";
            if (pc.AInt["熊熊儿童节SSS搭档兑换次数"] >= 1) s3 = "无法兑换";
            string op3 = "·SSS搭档[总限制:" + pc.AInt["熊熊儿童节SSS搭档兑换次数"] + "/1]需要" + s3;

            ushort n4 = (ushort)(800 + pc.AInt["熊熊儿童节S搭档兑换次数"] * 150);
            string s4 = n4.ToString() + "个";
            if (pc.AInt["熊熊儿童节S搭档兑换次数"] >= 3) s4 = "无法兑换";
            string op4 = "·S搭档[总限制:" + pc.AInt["熊熊儿童节S搭档兑换次数"] + "/3]需要:" + s4;

            ushort n5 = 5500;
            string s5 = n5.ToString() + "个";
            if (pc.AInt["熊熊儿童节5000CP兑换次数"] >= 1) s5 = "无法兑换";
            string op5 = "·5000CP[总限制:" + pc.AInt["熊熊儿童节5000CP兑换次数"] + "/1]需要:" + s5;

            ushort n6 = 1000;
            string s6 = n6.ToString() + "个";
            if (pc.AInt["熊熊儿童节EFFECT兑换次数2"] >= 1) s6 = "无法兑换";
            string op6 = "·EFFECT装备[总限制:" + pc.AInt["熊熊儿童节EFFECT兑换次数2"] + "/1]需要:" + s6;

            ushort n7 = 9999;
            string s7 = n7.ToString() + "个";
            if (pc.AInt["熊熊儿童节SSS突破石兑换次数"] >= 1) s7 = "无法兑换";
            string op7 = "·SSS突破石[总限制:" + pc.AInt["熊熊儿童节SSS突破石兑换次数"] + "/1]需要:" + s7;

            ushort n8 = 5000;
            string s8 = n8.ToString() + "个";
            if (pc.AInt["熊熊儿童节SS突破石兑换次数"] >= 2) s8 = "无法兑换";
            string op8 = "·SS突破石[总限制:" + pc.AInt["熊熊儿童节SS突破石兑换次数"] + "/2]需要:" + s8;

            ushort n9 = 1000;
            string s9 = n9.ToString() + "个";
            if (pc.AInt["熊熊儿童节S突破石兑换次数"] >= 5) s9 = "无法兑换";
            string op9 = "·S突破石[总限制:" + pc.AInt["熊熊儿童节S突破石兑换次数"] + "/5]需要:" + s9;


            string op10 = "·泰迪礼物盒[无兑换限制]需要:100个";


            Say(pc, 113, "春天到啦~$R咖啡馆正在举办收集熊熊糖罐的活动哦！$R只要你带来$CR「熊熊糖罐」$CD，$R就能兑换相应的礼品哦。$R$R在岛外击杀魔物$R就有机率掉落$CR「熊熊糖罐」$CD$R收集到一定数量就来找我兑换吧~", "咖啡馆看板娘·ECO猪");
            switch (Select(pc, "想要用「熊熊糖罐」兑换什么呢？", "", op1, op2, op3, op4, op5, op6, op7, op8, op9, op10, "离开"))
            {
                case 1:
                    if (pc.AInt["熊熊儿童节100任务点兑换次数"] >= 3) return;
                    if (CountItem(pc, itemid) < n1)
                    {
                        Say(pc, 113, "嗯？你的$CR「熊熊糖罐」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["熊熊儿童节100任务点兑换次数"]++;
                    pc.AInt["熊熊儿童节100任务点兑换次数"]++;
                    TakeItem(pc, itemid, n1);
                    GiveItem(pc, 910000103, 2);
                    break;
                case 2:
                    int set = Select(pc, "要兑换哪个呢？", "", "项链强化石", "武器强化石", "衣服强化石", "离开");
                    if (set == 4) return;
                    if (pc.AInt["熊熊儿童节强化石兑换次数"] >= 5) return;
                    if (CountItem(pc, itemid) < n2)
                    {
                        Say(pc, 113, "嗯？你的$CR「熊熊糖罐」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["熊熊儿童节强化石兑换次数"]++;
                    pc.AInt["熊熊儿童节强化石兑换次数"]++;
                    TakeItem(pc, itemid, n2);
                    GiveItem(pc, (uint)(960000000 + set - 1), 1);
                    break;
                case 3:
                    if (pc.AInt["熊熊儿童节SSS搭档兑换次数"] >= 1) return;
                    if (CountItem(pc, itemid) < n3)
                    {
                        Say(pc, 113, "嗯？你的$CR「熊熊糖罐」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["熊熊儿童节SSS搭档兑换次数"]++;
                    pc.AInt["熊熊儿童节SSS搭档兑换次数"]++;
                    TakeItem(pc, itemid, n3);
                    GiveItem(pc, 110170100, 1);
                    break;
                case 4:
                    if (pc.AInt["熊熊儿童节S搭档兑换次数"] >= 3) return;
                    if (CountItem(pc, itemid) < n4)
                    {
                        Say(pc, 113, "嗯？你的$CR「熊熊糖罐」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["熊熊儿童节S搭档兑换次数"]++;
                    pc.AInt["熊熊儿童节S搭档兑换次数"]++;
                    TakeItem(pc, itemid, n4);
                    GiveItem(pc, 110137600, 1);
                    break;
                case 5:
                    if (pc.AInt["熊熊儿童节5000CP兑换次数"] >= 1) return;
                    if (CountItem(pc, itemid) < n5)
                    {
                        Say(pc, 113, "嗯？你的$CR「熊熊糖罐」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["熊熊儿童节5000CP兑换次数"]++;
                    pc.AInt["熊熊儿童节5000CP兑换次数"]++;
                    TakeItem(pc, itemid, n5);
                    GiveItem(pc, 910000040, 1);
                    break;
                case 6:
                    if (pc.AInt["熊熊儿童节EFFECT兑换次数2"] >= 1) return;
                    if (CountItem(pc, itemid) < n6)
                    {
                        Say(pc, 113, "嗯？你的$CR「熊熊糖罐」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["熊熊儿童节EFFECT兑换次数2"]++;
                    pc.AInt["熊熊儿童节EFFECT兑换次数2"]++;
                    TakeItem(pc, itemid, n6);
                    GiveItem(pc, 61084300, 1);
                    break;
                case 7:
                    if (pc.AInt["熊熊儿童节SSS突破石兑换次数"] >= 1) return;
                    if (CountItem(pc, itemid) < n7)
                    {
                        Say(pc, 113, "嗯？你的$CR「熊熊糖罐」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["熊熊儿童节SSS突破石兑换次数"]++;
                    pc.AInt["熊熊儿童节SSS突破石兑换次数"]++;
                    TakeItem(pc, itemid, n7);
                    GiveItem(pc, 950000029, 1);
                    break;
                case 8:
                    if (pc.AInt["熊熊儿童节SS突破石兑换次数"] >= 2) return;
                    if (CountItem(pc, itemid) < n8)
                    {
                        Say(pc, 113, "嗯？你的$CR「熊熊糖罐」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["熊熊儿童节SS突破石兑换次数"]++;
                    pc.AInt["熊熊儿童节SS突破石兑换次数"]++;
                    TakeItem(pc, itemid, n8);
                    GiveItem(pc, 950000028, 1);
                    break;
                case 9:
                    if (pc.AInt["熊熊儿童节S突破石兑换次数"] >= 5) return;
                    if (CountItem(pc, itemid) < n9)
                    {
                        Say(pc, 113, "嗯？你的$CR「熊熊糖罐」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["熊熊儿童节S突破石兑换次数"]++;
                    pc.AInt["熊熊儿童节S突破石兑换次数"]++;
                    TakeItem(pc, itemid, n9);
                    GiveItem(pc, 950000027, 1);
                    break;
                case 10:
                    string input = InputBox(pc, "请输入想要兑换的盒子数量哦！", InputType.Bank);
                    if (input == "")
                        return;
                    ushort num = 0;
                    try
                    {
                        num = ushort.Parse(input);
                    }
                    catch
                    {
                        Say(pc, 0, "那个……你输入的数量是不是太多了呢？", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    if (num == 0) return;
                    if (num > 1000) return;
                    if (CountItem(pc, itemid) >= num * 100)
                    {
                        TakeItem(pc, itemid, (ushort)(num * 100));
                        GiveItem(pc, 952000003, num);
                        SInt["泰迪礼物盒兑换次数"] += num;
                        pc.AInt["泰迪礼物盒兑换次数"] += num;
                    }
                    else
                    {
                        Say(pc, 113, "嗯？你的$CR「熊熊糖罐」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    break;
                case 11:
                    break;
            }
        }
        #endregion
        #region 春天的气息4月12-5月14
        void 春天的气息(ActorPC pc)
        {
            if (异常检查(pc) == false) //如果检查返回false，则
                return;//结束脚本
            uint itemid = 952000000;

            ushort n1 = (ushort)(pc.AInt["春季活动100任务点兑换次数"] * 300 + 300);
            string s1 = n1.ToString() + "个";
            if (pc.AInt["春季活动100任务点兑换次数"] >= 3) s1 = "无法兑换";
            string op1 = "·100点任务点卷[总限制:" + pc.AInt["春季活动100任务点兑换次数"] + "/3]需要:" + s1;

            ushort n2 = (ushort)(pc.AInt["春季活动强化石兑换次数"] * 50 + 50);
            string s2 = n2.ToString() + "个";
            if (pc.AInt["春季活动强化石兑换次数"] >= 5) s2 = "无法兑换";
            string op2 = "·强化石头[总限制:" + pc.AInt["春季活动强化石兑换次数"] + "/5]需要:" + s2;

            ushort n3 = 5000;
            string s3 = n3.ToString() + "个";
            if (pc.AInt["春季活动SSS搭档兑换次数"] >= 1) s3 = "无法兑换";
            string op3 = "·SSS搭档[总限制:" + pc.AInt["春季活动SSS搭档兑换次数"] + "/1]需要" + s3;

            ushort n4 = (ushort)(800 + pc.AInt["春季活动S搭档兑换次数"] * 150);
            string s4 = n4.ToString() + "个";
            if (pc.AInt["春季活动S搭档兑换次数"] >= 3) s4 = "无法兑换";
            string op4 = "·S搭档[总限制:" + pc.AInt["春季活动S搭档兑换次数"] + "/3]需要:" + s4;

            ushort n5 = 5500;
            string s5 = n5.ToString() + "个";
            if (pc.AInt["春季活动5000CP兑换次数"] >= 1) s5 = "无法兑换";
            string op5 = "·5000CP[总限制:" + pc.AInt["春季活动5000CP兑换次数"] + "/1]需要:" + s5;

            ushort n6 = 1000;
            string s6 = n6.ToString() + "个";
            if (pc.AInt["春季活动EFFECT兑换次数2"] >= 1) s6 = "无法兑换";
            string op6 = "·EFFECT装备[总限制:" + pc.AInt["春季活动EFFECT兑换次数2"] + "/1]需要:" + s6;

            ushort n7 = 9999;
            string s7 = n7.ToString() + "个";
            if (pc.AInt["春季活动SSS突破石兑换次数"] >= 1) s7 = "无法兑换";
            string op7 = "·SSS突破石[总限制:" + pc.AInt["春季活动SSS突破石兑换次数"] + "/1]需要:" + s7;

            ushort n8 = 5000;
            string s8 = n8.ToString() + "个";
            if (pc.AInt["春季活动SS突破石兑换次数"] >= 2) s8 = "无法兑换";
            string op8 = "·SS突破石[总限制:" + pc.AInt["春季活动SS突破石兑换次数"] + "/2]需要:" + s8;

            ushort n9 = 1000;
            string s9 = n9.ToString() + "个";
            if (pc.AInt["春季活动S突破石兑换次数"] >= 5) s9 = "无法兑换";
            string op9 = "·S突破石[总限制:" + pc.AInt["春季活动S突破石兑换次数"] + "/5]需要:" + s9;


            string op10 = "·永远的魂火赠礼[无兑换限制]需要:100个";


            Say(pc, 113, "这个活动已经结束了，$R已经没有办法重置每日了哦。", "咖啡馆看板娘·ECO猪");
            switch (Select(pc, "想要用「樱花花瓣」兑换什么呢？", "", op1, op2, op3, op4, op5, op6, op7, op8, op9, op10, "离开"))
            {
                case 1:
                    if (pc.AInt["春季活动100任务点兑换次数"] >= 3) return;
                    if (CountItem(pc, itemid) < n1)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花花瓣」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["春季活动100任务点兑换次数"]++;
                    pc.AInt["春季活动100任务点兑换次数"]++;
                    TakeItem(pc, itemid, n1);
                    GiveItem(pc, 910000103, 2);
                    break;
                case 2:
                    int set = Select(pc, "要兑换哪个呢？", "", "项链强化石", "武器强化石", "衣服强化石", "离开");
                    if (set == 4) return;
                    if (pc.AInt["春季活动强化石兑换次数"] >= 5) return;
                    if (CountItem(pc, itemid) < n2)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花花瓣」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["春季活动强化石兑换次数"]++;
                    pc.AInt["春季活动强化石兑换次数"]++;
                    TakeItem(pc, itemid, n2);
                    GiveItem(pc, (uint)(960000000 + set - 1), 1);
                    break;
                case 3:
                    if (pc.AInt["春季活动SSS搭档兑换次数"] >= 1) return;
                    if (CountItem(pc, itemid) < n3)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花花瓣」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["春季活动SSS搭档兑换次数"]++;
                    pc.AInt["春季活动SSS搭档兑换次数"]++;
                    TakeItem(pc, itemid, n3);
                    GiveItem(pc, 110176700, 1);
                    break;
                case 4:
                    if (pc.AInt["春季活动S搭档兑换次数"] >= 3) return;
                    if (CountItem(pc, itemid) < n4)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花花瓣」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["春季活动S搭档兑换次数"]++;
                    pc.AInt["春季活动S搭档兑换次数"]++;
                    TakeItem(pc, itemid, n4);
                    GiveItem(pc, 110176800, 1);
                    break;
                case 5:
                    if (pc.AInt["春季活动5000CP兑换次数"] >= 1) return;
                    if (CountItem(pc, itemid) < n5)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花花瓣」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["春季活动5000CP兑换次数"]++;
                    pc.AInt["春季活动5000CP兑换次数"]++;
                    TakeItem(pc, itemid, n5);
                    GiveItem(pc, 910000040, 1);
                    break;
                case 6:
                    if (pc.AInt["春季活动EFFECT兑换次数2"] >= 1) return;
                    if (CountItem(pc, itemid) < n6)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花花瓣」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["春季活动EFFECT兑换次数2"]++;
                    pc.AInt["春季活动EFFECT兑换次数2"]++;
                    TakeItem(pc, itemid, n6);
                    GiveItem(pc, 61087600, 1);
                    break;
                case 7:
                    if (pc.AInt["春季活动SSS突破石兑换次数"] >= 1) return;
                    if (CountItem(pc, itemid) < n7)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花花瓣」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["春季活动SSS突破石兑换次数"]++;
                    pc.AInt["春季活动SSS突破石兑换次数"]++;
                    TakeItem(pc, itemid, n7);
                    GiveItem(pc, 950000029, 1);
                    break;
                case 8:
                    if (pc.AInt["春季活动SS突破石兑换次数"] >= 2) return;
                    if (CountItem(pc, itemid) < n8)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花花瓣」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["春季活动SS突破石兑换次数"]++;
                    pc.AInt["春季活动SS突破石兑换次数"]++;
                    TakeItem(pc, itemid, n8);
                    GiveItem(pc, 950000028, 1);
                    break;
                case 9:
                    if (pc.AInt["春季活动S突破石兑换次数"] >= 5) return;
                    if (CountItem(pc, itemid) < n9)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花花瓣」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["春季活动S突破石兑换次数"]++;
                    pc.AInt["春季活动S突破石兑换次数"]++;
                    TakeItem(pc, itemid, n9);
                    GiveItem(pc, 950000027, 1);
                    break;
                case 10:
                    string input = InputBox(pc, "请输入想要兑换的盒子数量哦！", InputType.Bank);
                    if (input == "")
                        return;
                    ushort num = 0;
                    try
                    {
                        num = ushort.Parse(input);
                    }
                    catch
                    {
                        Say(pc, 0, "那个……你输入的数量是不是太多了呢？", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    if (num == 0) return;
                    if (num > 1000) return;
                    if (CountItem(pc, itemid) >= num * 100)
                    {
                        TakeItem(pc, itemid, (ushort)(num * 100));
                        GiveItem(pc, 952000001, num);
                        SInt["永远的魂火赠礼兑换次数"] += num;
                        pc.AInt["永远的魂火赠礼兑换次数"] += num;
                    }
                    else
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花花瓣」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    break;
                case 11:
                    break;
            }
        }
        #endregion
        #region 白色情人节
        void 白色情人节(ActorPC pc)
        {
            Say(pc, 131, "这个活动已经结束了，$R已经没有办法重置每日了哦。", "咖啡馆看板娘·ECO猪");
            uint itemid = 950000034;

            ushort n1 = (ushort)(pc.AInt["314活动100任务点兑换次数"] * 300 + 300);
            string s1 = n1.ToString() + "个";
            if (pc.AInt["314活动100任务点兑换次数"] >= 3) s1 = "无法兑换";
            string op1 = "·100点任务点卷[总限制:" + pc.AInt["314活动100任务点兑换次数"] + "/3]需要:" + s1;

            ushort n2 = (ushort)(pc.AInt["314活动强化石兑换次数"] * 50 + 50);
            string s2 = n2.ToString() + "个";
            if (pc.AInt["314活动强化石兑换次数"] >= 5) s2 = "无法兑换";
            string op2 = "·强化石头[总限制:" + pc.AInt["314活动强化石兑换次数"] + "/5]需要:" + s2;

            ushort n3 = 5000;
            string s3 = n3.ToString() + "个";
            if (pc.AInt["314活动SSS搭档兑换次数"] >= 1) s3 = "无法兑换";
            string op3 = "·SSS搭档[总限制:" + pc.AInt["314活动SSS搭档兑换次数"] + "/1]需要" + s3;

            ushort n4 = (ushort)(800 + pc.AInt["314活动S搭档兑换次数"] * 150);
            string s4 = n4.ToString() + "个";
            if (pc.AInt["314活动S搭档兑换次数"] >= 3) s4 = "无法兑换";
            string op4 = "·S搭档[总限制:" + pc.AInt["314活动S搭档兑换次数"] + "/3]需要:" + s4;

            ushort n5 = 5500;
            string s5 = n5.ToString() + "个";
            if (pc.AInt["314活动5000CP兑换次数"] >= 1) s5 = "无法兑换";
            string op5 = "·5000CP[总限制:" + pc.AInt["314活动5000CP兑换次数"] + "/1]需要:" + s5;

            ushort n6 = 1000;
            string s6 = n6.ToString() + "个";
            if (pc.AInt["314活动EFFECT兑换次数2"] >= 1) s6 = "无法兑换";
            string op6 = "·EFFECT装备[总限制:" + pc.AInt["314活动EFFECT兑换次数2"] + "/1]需要:" + s6;

            ushort n7 = 9999;
            string s7 = n7.ToString() + "个";
            if (pc.AInt["314活动SSS突破石兑换次数"] >= 1) s7 = "无法兑换";
            string op7 = "·SSS突破石[总限制:" + pc.AInt["314活动SSS突破石兑换次数"] + "/1]需要:" + s7;

            ushort n8 = 5000;
            string s8 = n8.ToString() + "个";
            if (pc.AInt["314活动SS突破石兑换次数"] >= 2) s8 = "无法兑换";
            string op8 = "·SS突破石[总限制:" + pc.AInt["314活动SS突破石兑换次数"] + "/2]需要:" + s8;

            ushort n9 = 1000;
            string s9 = n9.ToString() + "个";
            if (pc.AInt["314活动S突破石兑换次数"] >= 5) s9 = "无法兑换";
            string op9 = "·S突破石[总限制:" + pc.AInt["314活动S突破石兑换次数"] + "/5]需要:" + s9;


            string op10 = "·白色情人节甜蜜盒子[无兑换限制]需要:100个";

            switch (Select(pc, "想要用「巧克力」兑换什么呢？", "", op1, op2, op3, op4, op5, op6, op7, op8, op9, op10, "离开"))
            {
                case 1:
                    if (pc.AInt["314活动100任务点兑换次数"] >= 3) return;
                    if (CountItem(pc, itemid) < n1)
                    {
                        Say(pc, 113, "嗯？你的$CR「巧克力」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["314活动100任务点兑换次数"]++;
                    pc.AInt["314活动100任务点兑换次数"]++;
                    TakeItem(pc, itemid, n1);
                    GiveItem(pc, 910000103, 2);
                    break;
                case 2:
                    int set = Select(pc, "要兑换哪个呢？", "", "项链强化石", "武器强化石", "衣服强化石", "离开");
                    if (set == 4) return;
                    if (pc.AInt["314活动强化石兑换次数"] >= 5) return;
                    if (CountItem(pc, itemid) < n2)
                    {
                        Say(pc, 113, "嗯？你的$CR「巧克力」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["314活动强化石兑换次数"]++;
                    pc.AInt["314活动强化石兑换次数"]++;
                    TakeItem(pc, itemid, n2);
                    GiveItem(pc, (uint)(960000000 + set - 1), 1);
                    break;
                case 3:
                    if (pc.AInt["314活动SSS搭档兑换次数"] >= 1) return;
                    if (CountItem(pc, itemid) < n3)
                    {
                        Say(pc, 113, "嗯？你的$CR「巧克力」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["314活动SSS搭档兑换次数"]++;
                    pc.AInt["314活动SSS搭档兑换次数"]++;
                    TakeItem(pc, itemid, n3);
                    GiveItem(pc, 110127950, 1);
                    break;
                case 4:
                    if (pc.AInt["314活动S搭档兑换次数"] >= 3) return;
                    if (CountItem(pc, itemid) < n4)
                    {
                        Say(pc, 113, "嗯？你的$CR「巧克力」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["314活动S搭档兑换次数"]++;
                    pc.AInt["314活动S搭档兑换次数"]++;
                    TakeItem(pc, itemid, n4);
                    GiveItem(pc, 110137500, 1);
                    break;
                case 5:
                    if (pc.AInt["314活动5000CP兑换次数"] >= 1) return;
                    if (CountItem(pc, itemid) < n5)
                    {
                        Say(pc, 113, "嗯？你的$CR「巧克力」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["314活动5000CP兑换次数"]++;
                    pc.AInt["314活动5000CP兑换次数"]++;
                    TakeItem(pc, itemid, n5);
                    GiveItem(pc, 910000040, 1);
                    break;
                case 6:
                    if (pc.AInt["314活动EFFECT兑换次数2"] >= 1) return;
                    if (CountItem(pc, itemid) < n6)
                    {
                        Say(pc, 113, "嗯？你的$CR「巧克力」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["314活动EFFECT兑换次数2"]++;
                    pc.AInt["314活动EFFECT兑换次数2"]++;
                    TakeItem(pc, itemid, n6);
                    GiveItem(pc, 61083100, 1);
                    break;
                case 7:
                    if (pc.AInt["314活动SSS突破石兑换次数"] >= 1) return;
                    if (CountItem(pc, itemid) < n7)
                    {
                        Say(pc, 113, "嗯？你的$CR「巧克力」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["314活动SSS突破石兑换次数"]++;
                    pc.AInt["314活动SSS突破石兑换次数"]++;
                    TakeItem(pc, itemid, n7);
                    GiveItem(pc, 950000029, 1);
                    break;
                case 8:
                    if (pc.AInt["314活动SS突破石兑换次数"] >= 2) return;
                    if (CountItem(pc, itemid) < n8)
                    {
                        Say(pc, 113, "嗯？你的$CR「巧克力」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["314活动SS突破石兑换次数"]++;
                    pc.AInt["314活动SS突破石兑换次数"]++;
                    TakeItem(pc, itemid, n8);
                    GiveItem(pc, 950000028, 1);
                    break;
                case 9:
                    if (pc.AInt["314活动S突破石兑换次数"] >= 5) return;
                    if (CountItem(pc, itemid) < n9)
                    {
                        Say(pc, 113, "嗯？你的$CR「巧克力」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["314活动S突破石兑换次数"]++;
                    pc.AInt["314活动S突破石兑换次数"]++;
                    TakeItem(pc, itemid, n9);
                    GiveItem(pc, 950000027, 1);
                    break;
                case 10:
                    string input = InputBox(pc, "请输入想要兑换的盒子数量哦！", InputType.Bank);
                    if (input == "")
                        return;
                    ushort num = 0;
                    try
                    {
                        num = ushort.Parse(input);
                    }
                    catch
                    {
                        Say(pc, 0, "那个……你输入的数量是不是太多了呢？", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    if (num == 0) return;
                    if (num > 1000) return;
                    if (CountItem(pc, itemid) >= num * 100)
                    {
                        TakeItem(pc, itemid, (ushort)(num * 100));
                        GiveItem(pc, 910000122, num);
                        SInt["白色情人节甜蜜盒子兑换次数"] += num;
                        pc.AInt["白色情人节甜蜜盒子兑换次数"] += num;
                    }
                    else
                    {
                        Say(pc, 113, "嗯？你的$CR「巧克力」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    break;
                case 11:
                    break;
            }
        }
        #endregion
        #region 端午节活动
        void 端午节活动(ActorPC pc)
        {
            if (异常检查(pc) == false) //如果检查返回false，则
                return;//结束脚本
            uint itemid = 110051810;

            ushort n1 = (ushort)(300);
            string s1 = n1.ToString() + "个";
            if (pc.AInt["端午活动100任务点兑换次数"] >= 10) s1 = "无法兑换";
            string op1 = "·100点任务点卷[总限制:" + pc.AInt["端午活动100任务点兑换次数"] + "/10]需要:" + s1;

            ushort n2 = (ushort)((pc.AInt["端午活动强化石兑换次数"] + 1) * 50);
            string s2 = n2.ToString() + "个";
            if (pc.AInt["端午活动强化石兑换次数"] >= 50) s2 = "无法兑换";
            string op2 = "·强化石头[总限制:" + pc.AInt["端午活动强化石兑换次数"] + "/50]需要:" + s2;

            ushort n5 = 1500;
            string s5 = n5.ToString() + "个";
            if (pc.AInt["端午活动5000CP兑换次数"] >= 1) s5 = "无法兑换";
            string op5 = "·5000CP[总限制:" + pc.AInt["端午活动5000CP兑换次数"] + "/1]需要:" + s5;

            ushort n6 = 300;
            string s6 = n6.ToString() + "个";
            if (pc.AInt["端午活动东部地牢兑换次数"] >= 5) s6 = "无法兑换";
            string op6 = "·东部地牢套装盒子[总限制:" + pc.AInt["端午活动东部地牢兑换次数"] + "/5]需要:" + s6;

            ushort n7 = 300;
            string s7 = n7.ToString() + "个";
            if (pc.AInt["端午活动流秘宝盒兑换次数"] >= 3) s7 = "无法兑换";
            string op7 = "· 寒流秘宝盒[总限制:" + pc.AInt["端午活动流秘宝盒兑换次数"] + "/3]需要:" + s7;

            ushort n8 = 800;
            string s8 = n8.ToString() + "个";
            if (pc.AInt["端午活动神秘贝壳兑换次数"] >= 1) s8 = "无法兑换";
            string op8 = "· 坐骑—神秘贝壳[总限制:" + pc.AInt["端午活动神秘贝壳兑换次数"] + "/1]需要:" + s8;

            ushort n9 = 450;
            string s9 = n9.ToString() + "个";
            if (pc.AInt["端午活动CP1000兑换次数"] >= 3) s9 = "无法兑换";
            string op9 = "·CP1000[总限制:" + pc.AInt["端午活动CP1000兑换次数"] + "/3]需要:" + s9;

            ushort n10 = 20;
            string s10 = n10.ToString() + "个";
            if (pc.AInt["端午活动特制KUJI币兑换次数"] >= 50) s10 = "无法兑换";
            string op10 = "·特制KUJI代币[总限制:" + pc.AInt["端午活动特制KUJI币兑换次数"] + "/50]需要:" + s10;

            ushort n11 = 850;
            string s11 = n11.ToString() + "个";
            if (pc.AInt["端午活动S突破石兑换次数"] >= 1) s11 = "无法兑换";
            string op11 = "·S级搭档突破石[总限制:" + pc.AInt["端午活动S突破石兑换次数"] + "/1]需要:" + s11;

            ushort n12 = 4500;
            string s12 = n12.ToString() + "个";
            if (pc.AInt["端午活动SS突破石兑换次数"] >= 1) s12 = "无法兑换";
            string op12 = "·SS级搭档突破石[总限制:" + pc.AInt["端午活动SS突破石兑换次数"] + "/1]需要:" + s12;



            switch (Select(pc, "想要用「樱花粽子」兑换什么呢？", "", op1, op2, op5, op6, op7, op8, op9, op10, op11, op12, "离开"))
            {
                case 1:
                    if (pc.AInt["端午活动100任务点兑换次数"] >= 10) return;
                    if (CountItem(pc, itemid) < n1)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花粽子」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["端午活动100任务点兑换次数"]++;
                    pc.AInt["端午活动100任务点兑换次数"]++;
                    TakeItem(pc, itemid, n1);
                    GiveItem(pc, 910000103, 2);
                    break;
                case 2:
                    int set = Select(pc, "要兑换哪个呢？", "", "项链强化石", "武器强化石", "衣服强化石", "离开");
                    if (set == 4) return;
                    if (pc.AInt["端午活动强化石兑换次数"] >= 5) return;
                    if (CountItem(pc, itemid) < n2)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花粽子」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["端午活动强化石兑换次数"]++;
                    pc.AInt["端午活动强化石兑换次数"]++;
                    TakeItem(pc, itemid, n2);
                    GiveItem(pc, (uint)(960000000 + set - 1), 1);
                    break;
                case 3:
                    if (pc.AInt["端午活动5000CP兑换次数"] >= 1) return;
                    if (CountItem(pc, itemid) < n5)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花粽子」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["端午活动5000CP兑换次数"]++;
                    pc.AInt["端午活动5000CP兑换次数"]++;
                    TakeItem(pc, itemid, n5);
                    GiveItem(pc, 910000040, 1);
                    break;
                case 4:
                    if (pc.AInt["端午活动东部地牢兑换次数"] >= 5) return;
                    if (CountItem(pc, itemid) < n6)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花粽子」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["端午活动东部地牢兑换次数"]++;
                    pc.AInt["端午活动东部地牢兑换次数"]++;
                    TakeItem(pc, itemid, n6);
                    GiveItem(pc, 953000000, 1);
                    break;
                case 5:
                    if (pc.AInt["端午活动流秘宝盒兑换次数"] >= 3) return;
                    if (CountItem(pc, itemid) < n7)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花粽子」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["端午活动流秘宝盒兑换次数"]++;
                    pc.AInt["端午活动流秘宝盒兑换次数"]++;
                    TakeItem(pc, itemid, n7);
                    GiveItem(pc, 953000021, 1);
                    break;
                case 6:
                    if (pc.AInt["端午活动神秘贝壳兑换次数"] >= 1) return;
                    if (CountItem(pc, itemid) < n8)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花粽子」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["端午活动神秘贝壳兑换次数"]++;
                    pc.AInt["端午活动神秘贝壳兑换次数"]++;
                    TakeItem(pc, itemid, n8);
                    GiveItem(pc, 10156300, 1);
                    break;
                case 7:
                    if (pc.AInt["端午活动CP1000兑换次数"] >= 3) return;
                    if (CountItem(pc, itemid) < n9)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花粽子」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["端午活动CP1000兑换次数"]++;
                    pc.AInt["端午活动CP1000兑换次数"]++;
                    TakeItem(pc, itemid, n9);
                    GiveItem(pc, 910000116, 1);
                    break;
                case 8:
                    if (pc.AInt["端午活动特制KUJI币兑换次数"] >= 50) return;
                    if (CountItem(pc, itemid) < n10)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花粽子」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["端午活动特制KUJI币兑换次数"]++;
                    pc.AInt["端午活动特制KUJI币兑换次数"]++;
                    TakeItem(pc, itemid, n10);
                    GiveItem(pc, 950000025, 1);
                    break;
                case 9:
                    if (pc.AInt["端午活动S突破石兑换次数"] >= 1) return;
                    if (CountItem(pc, itemid) < n11)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花粽子」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["端午活动S突破石兑换次数"]++;
                    pc.AInt["端午活动S突破石兑换次数"]++;
                    TakeItem(pc, itemid, n11);
                    GiveItem(pc, 950000027, 1);
                    break;
                case 10:
                    if (pc.AInt["端午活动SS突破石兑换次数"] >= 1) return;
                    if (CountItem(pc, itemid) < n12)
                    {
                        Say(pc, 113, "嗯？你的$CR「樱花粽子」$CD不够哦", "咖啡馆看板娘·ECO猪");
                        return;
                    }
                    SInt["端午活动SS突破石兑换次数"]++;
                    pc.AInt["端午活动SS突破石兑换次数"]++;
                    TakeItem(pc, itemid, n12);
                    GiveItem(pc, 950000028, 1);
                    break;
                case 11:
                    break;
            }
        }
        #endregion
        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "『熊熊儿童节』活动正在进行！请选择", "", "『熊熊儿童节』活动", "『春天的气息』活动（已结束，可补领）", "『白色情人节』活动（已结束，可补领）", "『端午节临时活动』【端午节】", "离开"))
            {
                case 1:
                    熊熊儿童节(pc);
                    break;
                case 2:
                    春天的气息(pc);
                    break;
                case 3:
                    白色情人节(pc);
                    break;
                case 4:
                    端午节活动(pc);
                    break;
            }
            return;
        }
        void 抽奖(ActorPC pc)
        {
            uint itemid = 952000000;
            if (CountItem(pc, itemid) >= 50)
            {
                Say(pc, 131, "那么，请您把手伸进箱子里，抽一张签吧。");
                Select(pc, "往哪边摸签呢？", "", "向东", "向南", "向西", "向北");
                TakeItem(pc, itemid, 50);//拿走卷
                int random = Global.Random.Next(0, 100000);//升成随机数
                if (random <= 1)//判断随机数
                {
                    pc.Gold += 1000000;//给钱
                    GiveItem(pc, 910000070, 1);//给道具
                    Announce(pc.Name + "抽到了头赏！！！获得了100万G！！！");//公告
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("抽到了 头赏！");//发送系统消息
                    Say(pc, 60000017, 131, "我看看...$R哇！！你抽中了头赏。$R莫非你就是传说中的欧皇？", "咖啡馆看板娘·ECO猪");
                    if (Select(pc, "还要继续抽奖吗？", "", "继续抽！", "我选择煌。") == 1)
                        抽奖(pc);
                }
                if (random > 1 && random <= 10)//二等奖
                {
                    pc.Gold += 500000;
                    GiveItem(pc, 910000069, 1);
                    Announce(pc.Name + "抽到了二等奖！！！获得了50万G！！！");//公告
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("抽到了 二等奖！");
                    Say(pc, 60000017, 131, "我看看...$R你抽到的是二等奖。", "咖啡馆看板娘·ECO猪");
                    if (Select(pc, "还要继续抽奖吗？", "", "继续抽！", "我选择煌。") == 1)
                        抽奖(pc);

                }
                if (random > 10 && random <= 50)//三等奖
                {
                    pc.Gold += 300000;
                    GiveItem(pc, 910000068, 1);
                    Announce(pc.Name + "抽到了二等奖！！！获得了30万G！！！");//公告
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("抽到了 三等奖！");
                    Say(pc, 60000017, 131, "我看看...$R你抽到的是三等奖。", "咖啡馆看板娘·ECO猪");
                    if (Select(pc, "还要继续抽奖吗？", "", "继续抽！", "我选择煌。") == 1)
                        抽奖(pc);
                }
                if (random > 50 && random <= 200)//四等奖
                {
                    pc.Gold += 100000;
                    GiveItem(pc, 910000067, 1);
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("抽到了 四等奖！");
                    Say(pc, 60000017, 131, "我看看...$R你抽到的是四等奖。", "咖啡馆看板娘·ECO猪");
                    if (Select(pc, "还要继续抽奖吗？", "", "继续抽！", "我选择煌。") == 1)
                        抽奖(pc);
                }
                if (random > 200 && random <= 1000)//五等奖
                {
                    pc.Gold += 50000;
                    GiveItem(pc, 910000066, 1);
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("抽到了 五等奖！");
                    Say(pc, 60000017, 131, "我看看...$R你抽到的是五等奖。", "咖啡馆看板娘·ECO猪");
                    if (Select(pc, "还要继续抽奖吗？", "", "继续抽！", "我选择煌。") == 1)
                        抽奖(pc);
                }
                if (random > 1000 && random <= 2000)//六等奖
                {
                    pc.Gold += 20000;
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("抽到了 六等奖！");
                    Say(pc, 60000017, 131, "我看看...$R你抽到的是六等奖。", "咖啡馆看板娘·ECO猪");
                    if (Select(pc, "还要继续抽奖吗？", "", "继续抽！", "我选择煌。") == 1)
                        抽奖(pc);
                }
                if (random > 2000 && random <= 10000)//七等奖
                {
                    pc.Gold += 10000;
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("抽到了 七等奖！");
                    Say(pc, 60000017, 131, "我看看...$R你抽到的是七等奖。", "咖啡馆看板娘·ECO猪");
                    if (Select(pc, "还要继续抽奖吗？", "", "继续抽！", "我选择煌。") == 1)
                        抽奖(pc);
                }
                if (random > 10000 && random <= 50000)//八等奖
                {
                    pc.Gold += 5000;
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("抽到了 八等奖！");
                    Say(pc, 60000017, 131, "我看看...$R你抽到的是八等奖。", "咖啡馆看板娘·ECO猪");
                    if (Select(pc, "还要继续抽奖吗？", "", "继续抽！", "我选择煌。") == 1)
                        抽奖(pc);
                }
                if (random > 50000 && random <= 100000)//九等奖
                {
                    pc.Gold += 1000;
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("抽到了 九等奖！");
                    Say(pc, 60000017, 131, "我看看...$R你抽到的是九等奖。", "咖啡馆看板娘·ECO猪");
                    if (Select(pc, "还要继续抽奖吗？", "", "继续抽！", "我选择煌。") == 1)
                        抽奖(pc);
                }
            }
            else
            {
                Say(pc, 131, "樱花花瓣似乎不够！", "咖啡馆看板娘·ECO猪");
                return;
            }
        }
    }
}