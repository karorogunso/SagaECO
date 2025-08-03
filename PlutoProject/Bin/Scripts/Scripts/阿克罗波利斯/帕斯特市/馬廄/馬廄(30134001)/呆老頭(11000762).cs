using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30134001
{
    public class S11000762 : Event
    {
        public S11000762()
        {
            this.EventID = 11000762;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];
            if (mask.Test(PSTFlags.獲得牛牛))//_5a80)
            {
                Say(pc, 131, "噢噢！您来了！$R;" +
                    "小花的孩子怎样？健康吗？$R;");
                return;
            }
            if (mask.Test(PSTFlags.獲得牛牛的對話))//_5a79)
            {
                if (CheckInventory(pc, 10013002, 1))
                {
                    mask.SetValue(PSTFlags.獲得牛牛, true);
                    mask.SetValue(PSTFlags.獲得牛牛的對話, false);
                    //_5a80 = true;
                    //_5a79 = false;
                    GiveItem(pc, 10052100, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "收到『哞哞』！$R;", "");
                    Say(pc, 131, "好好珍惜吧！$R;" +
                        "偶尔要过来喔！$R;" +
                        "$R知道吗？$R;");
                    Say(pc, 11000763, 364, "哞呜！$R;");
                    return;
                }
                Say(pc, 131, "行李太多了$R;");
                return;
            }
            if (mask.Test(PSTFlags.給予甜草))//_5a78)
            {
                int c = Global.Random.Next(1, 5);
                if (c <= 2)
                {
                    Say(pc, 131, "还没有生啊？$R;" +
                        "…我现在也累了$R;");
                    return;
                }
                if (c <= 4)
                {
                    Say(pc, 131, "最近看小花的样子$R;" +
                        "好像快生了！$R;");
                    return;
                }
                Say(pc, 131, "既然这样，最好您在的时候$R;" +
                    "生出来就好了$R;");
                Say(pc, 11000763, 331, "哞！哞~！$R;");
                Say(pc, 131, "哦！$R;" +
                    "还好吗？小花！！$R;");
                Say(pc, 11000763, 331, "哞~！哞~！！$R;");
                Say(pc, 131, "加油啊！$R;" +
                    "加油！小花！！$R;" +
                    "小花！！$R;");
                Say(pc, 11000763, 331, "哞呜呜~~!!$R;");
                PlaySound(pc, 4001, false, 100, 50); //FADE OUT WHITE
                Wait(pc, 6000);
                //FADE IN
                Say(pc, 11000763, 331, "哞！哞！$R;");
                Say(pc, 131, "噢噢噢！看那边！生出来了！$R;" +
                    "$P…$R;" +
                    "$P起来!得站起来!$R;" +
                    "$P…起来了!$R;" +
                    "什么？用两只脚站着？$R;");
                PlaySound(pc, 4009, false, 100, 50);
                Say(pc, 131, "哞哞是刚出生的时候就开始$R;" +
                    "两只脚走路的动物啊…？$R;");
                Say(pc, 11000763, 131, "哞呜！$R;");
                mask.SetValue(PSTFlags.獲得牛牛的對話, true);
                mask.SetValue(PSTFlags.給予甜草, false);
                //_5a79 = true;
                //_5a78 = false;
                Say(pc, 131, "顺利生下真是太幸运了~$R;" +
                    "我也累了，您已经很努力了$R;" +
                    "$P光说谢谢好像不够，$R;" +
                    "不然您能不能负责这孩子？$R;" +
                    "$R老实说…一个小花也够我受的了$R;" +
                    "如果是给您养，也不错啊$R;" +
                    "不不…如果是您的话$R可以安心的交给您$R;" +
                    "$R是不是？小花~？$R;");
                Say(pc, 11000763, 131, "哞呜~！$R;");
                return;
            }
            if (mask.Test(PSTFlags.尋找甜草))//_5a77)
            {
                if (CountItem(pc, 10004906) >= 1)
                {
                    mask.SetValue(PSTFlags.給予甜草, true);
                    mask.SetValue(PSTFlags.尋找甜草, false);
                    //_5a78 = true;
                    //_5a77 = false;
                    TakeItem(pc, 10004906, 1);
                    Say(pc, 131, "但现在无法离开小花！$R;");
                    Say(pc, 131, "这个真是谢谢了！$R;" +
                        "应该是我亲自去的采的$R;" +
                        "但现在无法离开小花！$R;");
                    return;
                }
                Say(pc, 131, "「卷叶草」还没有好吗？$R;" +
                    "小花也在等着呢！$R;");
                Say(pc, 11000763, 131, "哞哞！$R;");
                return;
            }
            if (mask.Test(PSTFlags.給予香草))//_5a76)
            {
                int b = Global.Random.Next(1, 10);
                if (b == 1)
                {
                    mask.SetValue(PSTFlags.尋找甜草, true);
                    mask.SetValue(PSTFlags.給予香草, false);
                    //_5a77 = true;
                    //_5a76 = false;
                    Say(pc, 131, "您是不是很无聊啊?$R;" +
                        "我在这里是工作…$R;" +
                        "$P无聊的话去采采$R;" +
                        "给小花的饲料怎样？$R;" +
                        "对了！这次「卷叶草」不错啊!$R;" +
                        "「卷叶草」真的很不错的啊!$R;" +
                        "$R呐!快去快回!$R;");
                    return;
                }
                if (b <= 5)
                {
                    Say(pc, 131, "小花最近变得好安静！$R;" +
                        "好像快到时间了$R;");
                    return;
                }
                Say(pc, 131, "我再说一遍!$R小花绝…对…不会给别人的$R;");
                return;
            }
            if (mask.Test(PSTFlags.尋找香草))//_5a75)
            {
                if (CountItem(pc, 10004902) >= 1)
                {
                    mask.SetValue(PSTFlags.給予香草, true);
                    mask.SetValue(PSTFlags.尋找香草, false);
                    //_5a76 = true;
                    //_5a75 = false;
                    TakeItem(pc, 10004902, 1);
                    Say(pc, 131, "给他神秘草$R;", "");
                    Say(pc, 131, "真感谢您$R;" +
                        "应该是我去采的$R;" +
                        "但是，我好担心小花$R没办法离开它身边$R;" +
                        "$R太感谢了$R;");
                    return;
                }
                Say(pc, 131, "「神秘草」还没有找到吗?$R;" +
                    "小花在等着呢~$R;");
                Say(pc, 11000763, 131, "哞呜$R;");
                return;
            }
            if (mask.Test(PSTFlags.給予健康營養飲料))//_5a74)
            {
                int a = Global.Random.Next(1, 10);
                if (a == 1)
                {
                    mask.SetValue(PSTFlags.尋找香草, true);
                    mask.SetValue(PSTFlags.給予健康營養飲料, false);
                    //_5a75 = true;
                    //_5a74 = false;
                    Say(pc, 131, "没关系！不要慌！$R;" +
                        "$R没有那么快产子…$R;" +
                        "$P无聊的话去弄弄$R;" +
                        "给小花的食物如何？$R;" +
                        "$R是呀！营养丰富的$R;" +
                        "「神秘草」是不错啊!$R;" +
                        "$R快点去吧！$R;");
                    return;
                }
                if (a <= 6)
                {
                    Say(pc, 131, "嗯？$R;" +
                        "那么喜欢小花吗？$R;" +
                        "$R但不要那么贪心啊$R;" +
                        "我才不会把小花给任何人的！$R;");
                    return;
                }
                Say(pc, 131, "真认真！$R;" +
                    "慢慢等等看看…$R;");
                return;
            }
            if (pc.Fame > 10)
            {
                Say(pc, 131, "怎样？$R;" +
                    "我小时候就开始养的哞哞！$R;" +
                    "叫做小花！$R;" +
                    "$R漂亮吗?$R;" +
                    "$P再过一阵子就生产了！$R;" +
                    "要忙于准备啰！$R;" +
                    "$R得充分吸收营养呢~$R;" +
                    "有没有什么好东西？$R;");
                if (CountItem(pc, 10000305) >= 1)
                {
                    Say(pc, 131, "什么事？$R;");
                    switch (Select(pc, "怎么办?", "", "用这个吧！", "看起来可口的乳牛啊"))
                    {
                        case 1:
                            mask.SetValue(PSTFlags.給予健康營養飲料, true);
                            //_5a74 = true;
                            Say(pc, 131, "给他健康营养饮料$R;", "");
                            Say(pc, 131, "这是什么？$R;" +
                                "您也担心小花啊？$R;" +
                                "$R无论如何，都很感谢您!$R;" +
                                "一个人受累了，谢谢!$R;" +
                                "$P但是!这个给动物也可以吗？$R;" +
                                "…可以吧…$R;" +
                                "$P这个或许也是什么缘分$R;" +
                                "以后经常过来吧$R;" +
                                "$R小花应该也会很高兴的！$R;" +
                                "是吧？小花？$R;");
                            Say(pc, 11000763, 364, "哞哞！！$R;");
                            TakeItem(pc, 10000305, 1);
                            break;
                        case 2:
                            Say(pc, 131, "什么？$R;" +
                                "说什么呢！$R;" +
                                "$R那个啊…$R应该是蛮好吃的，但是…$R;");
                            Say(pc, 11000763, 331, "哞哞！！$R;");
                            Say(pc, 131, "什么！？$R;" +
                                "快点出去吧！！$R;");
                            break;
                    }
                    return;
                }
                return;
            }
            Say(pc, 131, "怎样？$R;" +
                "我小时候就开始养的哞哞！$R;" +
                "叫做小花！$R;" +
                "$R漂亮吗?$R;" +
                "$P现在已经怀了孩子$R;" +
                "希望快点出生…$R;");
        }
    }
}