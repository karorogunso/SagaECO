using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:帕斯特街道(10017000) NPC基本資訊:飛空庭匠人(11000786) X:148 Y:212
namespace SagaScript.M10017000
{
    public class S11000786 : Event
    {
        public S11000786()
        {
            this.EventID = 11000786;
        }


        public override void OnEvent(ActorPC pc)
        {
            //Say(pc, 11000786, 131, "飞空庭上有贼会偷东西，不继续开放了$R;");
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];

            if (pc.Account.GMLevel >= 100)
            {
                if (Select(pc, "要怎么做呢？", "", "进入管理模式", "算了") == 1)
                {
                    管理用(pc);
                    return;
                }
            }
            //貓靈茜子相關
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.收到碎紙) &&
                !Neko_05_cmask.Test(Neko_05.去哈爾列爾利的飛空庭))
            {
                Say(pc, 0, 131, "真的非常感谢!$R;" +
                    "$R「客人」!「客人」!$R来!快点!回唐卡吧!!$R回去妈妈在的地方吧$R;", "行李里面的哈利路亚");
                return;
            }
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.告知需寻找工匠) &&
                !Neko_05_cmask.Test(Neko_05.收到碎紙))
            {
                Neko_05_cmask.SetValue(Neko_05.收到碎紙, true);
                Say(pc, 11000786, 111, "…瞌睡…瞌睡……扑通!!$R;");
                Say(pc, 11000786, 131, "是?是!!是!!$R;" +
                    "$R是他保管飞空庭的部件吗?$R;");
                Say(pc, 0, 131, "刚刚在睡觉吧?$R;" +
                    "$R我想问一些有关飞空庭引擎的问题…$R;", "行李内的哈利路亚");
                Say(pc, 11000786, 131, "什么话啊!$R没睡!没睡啊!!$R;" +
                    "$R事情太多才有点累…$R…是谁?…谁在行李里面啊?$R;");
                Say(pc, 0, 131, "我是莉塔的儿子$R石像「哈利路亚」!$R;" +
                    "$R我想问一些有关飞空庭引擎的问题…$R;", "行李里面的哈利路亚");
                Say(pc, 0, 131, "…哈利路亚和飞空庭工匠的对话$R继续…$R;");
                Say(pc, 11000786, 131, "…嗯!还是那样啊!$R;" +
                    "$R啊!知道了!$R是圆顶材料的问题啊!$R;" +
                    "$P把乳化剂的混合比例改变的话$R黏度会上升，就可以承受圆顶的内压$R;" +
                    "$R您把秘方记在纸上吧$R这样做就可以了$R;" +
                    "$P把可以参考的，都一起写给您吧$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, 131, "『莉塔的哈利路亚』收到$R『碎纸』!$R;");
                Say(pc, 0, 131, "真的非常感谢!$R;" +
                    "$R「客人」!「客人」!$R来!快点!回唐卡吧!!$R回去妈妈在的地方吧$R;", "行李里面的哈利路亚");
                return;
            }//*/

            if (fgarden.Test(FGarden.得到飛空庭鑰匙) && CountItem(pc, 10022700) == 0)
            {
                Say(pc, 131, "什么?$R弄丢了『飞空庭钥匙』?$R;" +
                    "$R真是不小心啊$R钥匙很重要的，要好好保管啊!$R;");
                if (CheckInventory(pc, 10022700, 1))
                {
                    GiveItem(pc, 10022700, 1);
                    Say(pc, 131, "来!给您『飞空庭钥匙』$R;" +
                        "$R这次可不要弄丢了!$R;");
                }
                else
                {
                    Say(pc, 131, "行李好像都满了喔?$R;" +
                        "$R把行李减少后再来吧!$R;");
                }
                return;
            }

            if (SInt["FGarden_Potion"] < 50000 && !fgarden.Test(FGarden.得到飛空庭鑰匙))
            {
                傑利科藥水不夠(pc);
                return;
            }

            if (fgarden.Test(FGarden.還飛空庭旋轉帆超重))
            {
                還飛空庭旋轉帆(pc);
                return;
            }

            if (SInt["FGarden_Potion"] > 49000 && !fgarden.Test(FGarden.得到飛空庭鑰匙))
            {
                傑利科藥水夠了(pc);
                return;
            }
            Say(pc, 131, "您好!$R您的飞空庭最近运行的正常吗?$R;");
        }

        void 管理用(ActorPC pc)
        {
            //EVT1100078669
            switch (Select(pc, "管理者模式", "", "增加杰利科药水", "减少杰利科药水", "什么都不做"))
            {
                case 1:
                    SInt["FGarden_Potion"] += int.Parse(InputBox(pc, "输入要增加的数量", InputType.ItemCode));
                    break;
                case 2:
                    int count = int.Parse(InputBox(pc, "输入要减少的数量", InputType.ItemCode));
                    if (SInt["FGarden_Potion"] > count)
                        SInt["FGarden_Potion"] -= count;
                    else
                        SInt["FGarden_Potion"] = 0;
                    break;
            }
            SaveServerSvar();
        }

        void 傑利科藥水夠了(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            if (!fgarden.Test(FGarden.第一次和飛空庭匠人說話))
            {
                fgarden.SetValue(FGarden.第一次和飛空庭匠人說話, true);
                fgarden.SetValue(FGarden.得知飛空庭材料, true);
                Say(pc, 131, "喔喔!都忙到没魂了！太忙了！$R;" +
                    "$R要帮制作『飞空庭』话，别跟我说…$R;" +
                    "$P我说真的$R;" +
                    "$R有飞空庭从唐卡去诺森途中坠落了$R;" +
                    "$P因为诺森岛那边的$R;" +
                    "天气经常不好，所以是禁飞区$R;" +
                    "$R不应该让客机来啊…$R;" +
                    "$P修理时，一定需要『杰利科药水』的$R;" +
                    "自己一个人收集困难$R所以找了大家帮忙!$R;" +
                    "$P虽然转眼间材料都收集到了$R;" +
                    "真的太感动了，$R;" +
                    "所以答应了为大家造『飞空庭』…$R;" +
                    "$R看!$R;" +
                    "$P就因为这样$R;" +
                    "所以正赶制耽误进度的『飞空庭』喔$R;" +
                    "$P『飞空庭』是飞上天空的庭园!$R;" +
                    "就像名字一样，是庭园在飞!$R;" +
                    "$R在玛依玛依岛上的猴面包树$R;" +
                    "到了300年，就会产生浮力$R;" +
                    "在上面装上机械时代的发掘引擎$R;" +
                    "$P然后在那引擎轴上，$R;" +
                    "安装能调整猴面包树的旋转帆！$R;" +
                    "飞空庭就完成了!$R;" +
                    "$P在后面的就是『飞空庭』!$R;" +
                    "想像一下它在天空中翱翔的情景吧!$R;" +
                    "$R不是很漂亮吗…$R;" +
                    "$P真是!$R;" +
                    "我是唐卡的沃顿，是飞空庭的制作师$R;" +
                    "$R在国内被称为『飞空庭师』$R;" +
                    "是唐卡大师最崇高的称号$R;");
                Say(pc, 131, "……$R;" +
                    "$P…………$R;" +
                    "$P………………$R;" +
                    "$P莫非…$R;" +
                    "您也想拥有飞空庭?$R;" +
                    "$P那倒是可以理解$R;" +
                    "喔喔…哎呀!$R;" +
                    "$R那我也帮您制作飞空庭吧!$R;" +
                    "$P给我拿来飞空庭的部件的话$R;" +
                    "我就给您组装!$R;" +
                    "$R需要的部件都收集好的话$R;" +
                    "飞空庭就完成了!$R;" +
                    "$P需要的部件是…$R;" +
                    "$R首先是『飞空庭底座』$R;" +
                    "然后是『飞空庭引擎』$R;" +
                    "$P还有『飞空庭旋转帆』6张!$R;" +
                    "$R把这些都收集到的话$R;" +
                    "就成为『飞空庭旋转帆套装』!$R;" +
                    "$P还有『飞空庭的绞车』!$R;" +
                    "$R『操舵轮』和『触媒』也收集到的话$R;" +
                    "飞空庭就完成了!$R;");
            }
            else
            {
                if (!fgarden.Test(FGarden.得知飛空庭材料))
                {
                    Say(pc, 131, "真的谢谢!$R幸亏有您5万瓶『杰利科药水』$R全部都收集到了!$R;" +
                       "$P真的非常感谢！$R;" +
                       "$R用这个，引擎都修好了$R所以飞到诺森…$R;" +
                       "$P哎呀!差点忘了!$R;" +
                       "$R说好要给您制作飞空庭的吧!!$R;" +
                       "$P我会制作最好的飞空庭的，相信我！$R;" +
                       "$R把飞空庭的部件拿来给我$R我会帮您组装的!$R;" +
                       "$R需要的部件都收集好的话$R飞空庭就会完成的!$R;");
                    Say(pc, 131, "需要的部件是…$R;" +
                        "$R首先是『飞空庭底座』$R;" +
                        "$P还有『飞空庭旋转帆』6张!$R;" +
                        "$R把这些都收集到的话$R就成为『飞空庭旋转帆套装』!$R;" +
                        "$P还有『飞空庭的绞车』!$R『操舵轮』和『触媒』也收集到的话$R飞空庭就完成了!$R;");
                    fgarden.SetValue(FGarden.得知飛空庭材料, true);
                }
                else
                {
                    製作飛空庭(pc);
                }
            }
        }

        void 製作飛空庭(ActorPC pc)
        {
            BitMask<FGardenParts> parts = pc.AMask["FGardenParts"];
            Say(pc, 131, "拿来了什么部件?$R;");
            switch (Select(pc, "怎么做呢?", "", "给他『飞空庭底座』", "给他『操舵轮』", "给他『飞空庭引擎』", "给他『触媒』", "给他『飞空庭旋转帆』", "给他『飞空庭旋转帆套装』", "给他『飞空庭的绞车』", "看看都收集了多少部件", "放弃"))
            {
                case 1:
                    if (parts.Test(FGardenParts.Foundation))
                    {
                        Say(pc, 131, "部件已经接收了?$R;");
                    }
                    else
                    {
                        if (CountItem(pc, 10027300) >= 1)
                        {
                            Say(pc, 131, "是『飞空庭底座』啊!$R;" +
                                "$R我来帮您保管好吗?$R;");
                            switch (Select(pc, "怎么做呢", "", "让他保管『飞空庭底座』", "不让他保管"))
                            {
                                case 1:
                                    TakeItem(pc, 10027300, 1);
                                    parts.SetValue(FGardenParts.Foundation, true);
                                    Say(pc, 131, "我会好好保管『飞空庭底座』的！$R;");
                                    SaveServerSvar();
                                    break;
                                case 2:
                                    Say(pc, 131, "有空再来吧!$R;");
                                    return;
                            }
                        }
                        else
                            Say(pc, 131, "您好?$R好像没有部件喔?$R;");
                    }
                    break;
                case 2:
                    if (parts.Test(FGardenParts.Steer))
                    {
                        Say(pc, 131, "部件已经接收了?$R;");
                    }
                    else
                    {
                        if (CountItem(pc, 10028900) >= 1)
                        {
                            Say(pc, 131, "是『操舵轮』啊!$R;" +
                                "$R让我来保管好吗?$R;");
                            switch (Select(pc, "怎么做呢?", "", "让他保管『操舵轮』", "不让他保管"))
                            {
                                case 1:
                                    TakeItem(pc, 10028900, 1);
                                    parts.SetValue(FGardenParts.Steer, true);
                                    Say(pc, 131, "我会好好保管『操舵轮』的!$R;");
                                    SaveServerSvar();
                                    break;
                                case 2:
                                    Say(pc, 131, "有空再来吧!$R;");
                                    return;
                            }
                        }
                        else
                            Say(pc, 131, "您好?$R好像没有部件喔?$R;");
                    }
                    break;
                case 3:
                    if (parts.Test(FGardenParts.Engine))
                    {
                        Say(pc, 131, "部件已经接收了?$R;");
                    }
                    else
                    {
                        if (CountItem(pc, 10027900) >= 1)
                        {
                            Say(pc, 131, "是『飞空庭引擎』$R;" +
                                "$R我来帮您保管好吗?$R;");
                            switch (Select(pc, "怎么做呢", "", "让他保管『飞空庭引擎』", "不让他保管"))
                            {
                                case 1:
                                    TakeItem(pc, 10027900, 1);
                                    parts.SetValue(FGardenParts.Engine, true);
                                    Say(pc, 131, "我会好好保管『飞空庭引擎』的！$R;");
                                    SaveServerSvar();
                                    break;
                                case 2:
                                    Say(pc, 131, "有空再来吧!$R;");
                                    return;
                            }
                        }
                        else
                            Say(pc, 131, "您好?$R好像没有部件喔?$R;");
                    }

                    break;
                case 4:
                    if (parts.Test(FGardenParts.Catalyst))
                    {
                        Say(pc, 131, "部件已经接收了?$R;");
                    }
                    else
                    {
                        if (CountItem(pc, 10027600) >= 1)
                        {
                            Say(pc, 131, "是『触媒』啊!$R;" +
                                "$R让我来保管好吗?$R;");
                            switch (Select(pc, "怎么做呢?", "", "让他保管『触媒』", "不让他保管"))
                            {
                                case 1:
                                    TakeItem(pc, 10027600, 1);
                                    parts.SetValue(FGardenParts.Catalyst, true);
                                    Say(pc, 131, "我会好好保管『触媒』的!$R;");
                                    SaveServerSvar();
                                    break;
                                case 2:
                                    Say(pc, 131, "有空再来吧!$R;");
                                    return;
                            }
                        }
                        else
                            Say(pc, 131, "您好?$R好像没有部件喔?$R;");
                    }
                    break;
                case 5:
                    if (parts.Test(FGardenParts.SailComplete))
                    {
                        Say(pc, 131, "已经收集了6个旋转帆了吗?$R;");
                    }
                    else
                    {
                        if (CountItem(pc, 10028000) >= 1)
                        {
                            Say(pc, 131, "是『飞空庭旋转帆』啊!$R;" +
                                "$R让我来保管好吗?$R;");
                            switch (Select(pc, "怎么做呢?", "", "让他保管『飞空庭旋转帆』", "不让他保管"))
                            {
                                case 1:
                                    TakeItem(pc, 10028000, 1);
                                    if (!parts.Test(FGardenParts.Sail1))
                                    {
                                        parts.SetValue(FGardenParts.Sail1, true);
                                        Say(pc, 131, "是1个『飞空庭旋转帆』啊!$R我来保管吧$R;");
                                    }
                                    else if (parts.Test(FGardenParts.Sail1) && !parts.Test(FGardenParts.Sail2))
                                    {
                                        parts.SetValue(FGardenParts.Sail2, true);
                                        Say(pc, 131, "是第2张『飞空庭旋转帆』啊!$R我来保管吧;");
                                    }
                                    else if (parts.Test(FGardenParts.Sail2) && !parts.Test(FGardenParts.Sail3))
                                    {
                                        parts.SetValue(FGardenParts.Sail3, true);
                                        Say(pc, 131, "是第3张『飞空庭旋转帆』啊!$R我来保管吧;");
                                    }
                                    else if (parts.Test(FGardenParts.Sail3) && !parts.Test(FGardenParts.Sail4))
                                    {
                                        parts.SetValue(FGardenParts.Sail4, true);
                                        Say(pc, 131, "是第4张『飞空庭旋转帆』啊!$R我来保管吧!$R;");
                                    }
                                    else if (parts.Test(FGardenParts.Sail4) && !parts.Test(FGardenParts.Sail5))
                                    {
                                        parts.SetValue(FGardenParts.Sail5, true);
                                        Say(pc, 131, "是第5张『飞空庭旋转帆』啊!$R我来保管吧$R;");
                                    }
                                    else if (parts.Test(FGardenParts.Sail5) && !parts.Test(FGardenParts.SailComplete))
                                    {
                                        parts.SetValue(FGardenParts.SailComplete, true);
                                        Say(pc, 131, "是第6张『飞空庭旋转帆』啊!$R我来保管吧$R;" +
                                            "$R恭喜您!$R『飞空庭旋转帆』全都收集好了！$R;");
                                    }
                                    SaveServerSvar();
                                    break;
                                case 2:
                                    Say(pc, 131, "有空再来吧!$R;");
                                    return;
                            }
                        }
                        else
                            Say(pc, 131, "您好?$R好像没有部件喔?$R;");
                    }
                    break;
                case 6:
                    if (parts.Test(FGardenParts.SailComplete))
                    {
                        Say(pc, 131, "部件已经接收了?$R;");
                    }
                    else
                    {
                        if (CountItem(pc, 10028100) >= 1)
                        {
                            Say(pc, 131, "是『飞空庭旋转帆套装』啊!$R;" +
                                "$R让我来保管好吗?$R;");
                            switch (Select(pc, "怎么做呢?", "", "让他保管『飞空庭旋转帆套装』", "不让他保管"))
                            {
                                case 1:
                                    TakeItem(pc, 10028100, 1);
                                    parts.SetValue(FGardenParts.SailComplete, true);
                                    Say(pc, 131, "我会好好保管『飞空庭旋转帆套装』的!$R;");
                                    SaveServerSvar();
                                    還飛空庭旋轉帆(pc);
                                    break;
                                case 2:
                                    Say(pc, 131, "有空再来吧!$R;");
                                    return;
                            }
                        }
                        else
                            Say(pc, 131, "您好?$R好像没有部件喔?$R;");
                    }
                    break;
                case 7:
                    if (parts.Test(FGardenParts.Wheel))
                    {
                        Say(pc, 131, "部件已经接收了?$R;");
                    }
                    else
                    {
                        if (CountItem(pc, 10028200) >= 1)
                        {
                            Say(pc, 131, "是『飞空庭的绞车』啊!$R;" +
                                "$R让我来保管好吗?$R;");
                            switch (Select(pc, "怎么做呢?", "", "让他保管『飞空庭的绞车』", "不让他保管"))
                            {
                                case 1:
                                    TakeItem(pc, 10028200, 1);
                                    parts.SetValue(FGardenParts.Wheel, true);
                                    Say(pc, 131, "我会好好保管『飞空庭的绞车』的!$R;");
                                    SaveServerSvar();
                                    break;
                                case 2:
                                    Say(pc, 131, "有空再来吧!$R;");
                                    return;
                            }
                        }
                        else
                            Say(pc, 131, "您好?$R好像没有部件喔?$R;");
                    }
                    break;
                case 9:
                    return;
            }
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            if (!fgarden.Test(FGarden.得到飛空庭鑰匙) &&
                parts.Test(FGardenParts.Foundation) &&
                parts.Test(FGardenParts.Engine) &&
                parts.Test(FGardenParts.SailComplete) &&
                parts.Test(FGardenParts.Steer) &&
                parts.Test(FGardenParts.Wheel) &&
                parts.Test(FGardenParts.Catalyst))
            {
                Say(pc, 131, "终于都收集好了!$R;" +
                    "$P恭喜您!$R您的 「飞空庭」完成了!$R;" +
                    "$P这个给您吧!$R;");
                Say(pc, 131, "拿到了『飞空庭钥匙』!$R;");
                Say(pc, 131, "使用这道具的话$R您可以在使用的地方，$R召唤您的飞空庭$R;" +
                    "$P飞空庭里有自动导航系统$R只要收到『飞空庭钥匙』的讯号$R就会飞到您上面的!$R;" +
                    "$P然后，点击飞空庭的话$R会放下来「飞空庭绳子」$R选择「往飞空庭移动」$R就可以进入飞空庭里!$R;" +
                    "$P您可以选择「整理绳子」$R来整理「飞空庭绳子」$R;" +
                    "$P进去飞空庭以后，有不懂的话$R点击一下「操舵轮」$R或房间的「操作盘」试试看吧$R;" +
                    "$P『飞空庭钥匙』的功能是$R召唤自己飞空庭时，使用的道具$R所以没有飞空庭的人，就算用了$R也无法召唤飞空庭的$R;" +
                    "$P最后要说的是$R现在对飞空庭的飞空规则管理很严格$R;" +
                    "$R为避免飞空庭被用作军事用途$R和保护运送队的权益$R商人行会那边也有压力$R还有一些难言之隐$R;" +
                    "$P在阿克罗尼亚世界里$R如果没有得到特别许可$R只可以在「飞空庭机场地区」里$R召唤飞空庭$R;" +
                    "$R啊!$R;");

                if (pc.FGarden == null)//如果當前帳號還沒創建過飛空庭
                    pc.FGarden = new SagaDB.FGarden.FGarden(pc);//創建新的飛空庭
                GiveItem(pc, 10022700, 1);
                fgarden.SetValue(FGarden.得到飛空庭鑰匙, true);
            }
            SendFGardenCreateMaterial(pc, parts);
        }

        void 還飛空庭旋轉帆(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            BitMask<FGardenParts> parts = pc.AMask["FGardenParts"];
            if (parts.Test(FGardenParts.Sail1))
            {
                Say(pc, 131, "嗯?$R我保管的『飞空庭旋转帆』$R要还给您吗?$R;");
                switch (Select(pc, "怎么做呢?", "", "拿回『飞空庭旋转帆』", "不需要"))
                {
                    case 1:
                        ushort count = 0;
                        if (parts.Test(FGardenParts.Sail5))
                            count = 5;
                        else if (parts.Test(FGardenParts.Sail4))
                            count = 4;
                        else if (parts.Test(FGardenParts.Sail3))
                            count = 3;
                        else if (parts.Test(FGardenParts.Sail2))
                            count = 2;
                        else if (parts.Test(FGardenParts.Sail1))
                            count = 1;
                        if (CheckInventory(pc, 10028000, count))
                        {
                            GiveItem(pc, 10028000, count);
                            SaveServerSvar();
                        }
                        else
                        {
                            fgarden.SetValue(FGarden.還飛空庭旋轉帆超重, true);
                            Say(pc, 131, "行李都满了$R减少行李后再来吧!$R;");
                        }
                        break;
                    case 2:
                        Say(pc, 131, "不需要?$R那好!那我来处理吧$R;");
                        fgarden.SetValue(FGarden.還飛空庭旋轉帆超重, false);
                        break;
                }
            }
        }

        void 傑利科藥水不夠(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            if (!fgarden.Test(FGarden.第一次和飛空庭匠人說話))
            {
                Say(pc, 131, "这个真是犯愁啊!$R;" +
                    "$R有飞空庭从唐卡去诺森途中坠落了$R;" +
                    "$P因为诺森岛那边的$R;" +
                    "天气经常不好，所以是禁飞区$R;" +
                    "$R不应该让客机来啊…$R;" +
                    "我是唐卡的沃顿，是飞空庭的制作师$R;" +
                    "$R在国内被称为『飞空庭师』$R;" +
                    "是唐卡大师最崇高的称号$R;" +
                    "$P『飞空庭』是飞上天空的庭园!$R;" +
                    "就像名字一样，是庭园在飞!$R;" +
                    "$R在玛依玛依岛上的猴面包树$R;" +
                    "到了300年，就会产生浮力$R;" +
                    "在上面装上机械时代的发掘引擎$R;" +
                    "$P然后在那引擎轴上，$R;" +
                    "安装能调整猴面包树的旋转帆！$R;" +
                    "飞空庭就完成了!$R;" +
                    "$P本来要修理飞空庭的$R;" +
                    "可是引擎的汽油全都漏了$R;" +
                    "$R不好意思$R;" +
                    "可以帮我收集一些道具吗?$R;" +
                    "$P制作液体氢气，所需要的原料$R;" +
                    "是『杰利科药水』5万瓶$R;" +
                    "$R只要充电一次$R;" +
                    "下次开始，只要引擎待机时补充氢气$R;" +
                    "就可以半永久性的运作!$R;" +
                    "$P对了!$R;" +
                    "嗯嗯…$R;" +
                    "$P对了!$R;" +
                    "收集到『杰利科药水』5万瓶的话$R;" +
                    "给您制作飞空庭当作报酬!$R;" +
                    "怎么样?$R;" +
                    "$P什么?$R;" +
                    "$R哈哈哈哈!$R;" +
                    "我制作的飞空庭非常完美的!!$R;");
                fgarden.SetValue(FGarden.第一次和飛空庭匠人說話, true);
            }
            else
            {
                switch (Select(pc, " 怎么做呢?", "", "收集杰利科药水", "现在有几瓶杰利科药水?", "什么都不做"))
                {
                    case 1:
                        int count = 0;
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10000104)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交给了{0}个杰利科药水", count));
                            SInt["FGarden_Potion"] += count;
                        }
                        break;
                    case 2:
                        Say(pc, 131, "现在是…$R;" +
                            SInt["FGarden_Potion"] + "个杰利科药水啊!$R;");
                        break;
                }
            }
        }
    }
}

