using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000326 : Event
    {
        public S11000326()
        {
            this.EventID = 11000326;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (!mask.Test(AYEFlags.阿伊恩薩烏斯的母親第一次對話))//_2a51)
            {
                mask.SetValue(AYEFlags.阿伊恩薩烏斯的母親第一次對話, true);
                //_2a51 = true;
                Say(pc, 131, "快来吧$R;" +
                    "我是艾恩萨乌斯的母亲。$R;" +
                    "$R我可以看见灵魂的闪烁喔。$R;" +
                    "这样的人就是可以信任的。$R;" +
                    "而且越是这样就越有名气呢。$R;" +
                    "经常会得到任务呀$R;");
            }
            switch (Select(pc, "什么事呢？", "", "看看我灵魂的闪烁吧", "算了"))//, "谈人生（期间限定）", "算了"))
            {
                case 1:
                    if (!mask.Test(AYEFlags.阿伊恩薩烏斯的母親的技能點))//_2a64)
                    {
                        mask.SetValue(AYEFlags.阿伊恩薩烏斯的母親的技能點, true);
                        //_2a64 = true;
                        SkillPointBonus(pc, 1);
                        Wait(pc, 2000);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);

                        Wait(pc, 2000);
                        Say(pc, 131, "技能点数上升一点$R;");
                        Say(pc, 131, "现在的那个强光是什么？$R;" +
                            "$P…$R;" +
                            "$R您的灵魂$R;" +
                            "拥有惊人的力量啊。$R;" +
                            "好像是您的力量和我的力量$R;" +
                            "冲突溢出了。$R;");
                        return;
                    }
                    if (pc.Fame == 0)
                    {
                        Say(pc, 131, "现在一点都不亮呢。$R;" +
                            "还是无名的初级冒险者吧。$R;");
                        return;
                    }
                    if (pc.Fame <= 99)
                    {
                        Say(pc, 131, "您里面的光虽小$R;" +
                            "但就像泉水里$R;" +
                            "闪烁着的一缕缕光辉呢$R;" +
                            "$R虽然不多，$R;" +
                            "还是有感谢您的人。$R;");
                        return;
                    }
                    if (pc.Fame <= 399)
                    {
                        Say(pc, 131, "有一团温暖而柔和的光辉$R;" +
                            "围绕着您。$R;" +
                            "数百个人要感谢您$R;" +
                            "他们对您有很大期望呢$R;");
                        return;
                    }
                    if (pc.Fame <= 999)
                    {
                        Say(pc, 131, "强大的光芒。$R;" +
                            "到现在您已经帮助了很多人呢。$R;" +
                            "您是人们的希望之光。$R;" +
                            "以后还有很多困苦的人$R需要您的帮忙啊$R;");
                        return;
                    }
                    if (pc.Fame >= 1000)
                    {
                        Say(pc, 131, "您是谁啊？$R;" +
                            "您灵魂的闪光好美丽啊。$R;" +
                            "要感谢您，尊敬您的人数，$R;" +
                            "真是惊人啊！$R;");
                        return;
                    }
                    break;
                    /*
                case 2:
                    談人生(pc);
                    break;
                    */
            }
        }

        void 談人生(ActorPC pc)
        {
            switch (Select(pc, "跟我谈谈吧", "", "想重新开始人生", "想赚钱", "想交朋友", "凭依运不好", "不谈了"))
            {
                case 1:
                    int a = pc.Level * 1000;
                    Say(pc, 131, "回到刚出生的时候吧。$R;" +
                        "想将奖励点数$R;" +
                        "由1开始重新分配吗？$R;" +
                        "$P好，帮您办吧$R;" +
                        "$R可是需要交" + a + "金币的钱呢$R;");
                    switch (Select(pc, "需要钱呢~?", "", "那就算了吧", "还是决定重生"))
                    {
                        case 2:
                            if (a < pc.Gold)
                            {
                                Say(pc, 131, "钱不够呢$R;");
                                return;
                            }
                            pc.Gold -= a;
                            //STATUSRESET
                            Say(pc, 131, "闪闪！$R;");
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "状态初级化$R;");
                            break;
                    }
                    break;
                case 2:
                    Say(pc, 131, "谁都想赚钱呢。$R;" +
                        "$P魔物掉下来的道具$R;" +
                        "虽然比较便宜，$R但经过合成和精制的话$R;" +
                        "也可以卖高价的阿$R;" +
                        "$R好好利用赚大钱$R;" +
                        "不是很聪明吗?$R;");
                    break;
                case 3:
                    Say(pc, 131, "到队伍招募广场$R;" +
                        "以队伍为单位的任务$R;" +
                        "会有很多人参加的。$R;" +
                        "$R然后在来的人中$R;" +
                        "给喜欢的人写信就好了。$R;" +
                        "$P一开始跟他说话$R;" +
                        "需要很大的勇气呢$R;" +
                        "但也要试一下啊！$R;");
                    break;
                case 4:
                    Say(pc, 131, "不能回到最初的凭依状态吗?$R;" +
                        "那个真的是凭运气的$R;" +
                        "$P但要是我帮您祈祷的话$R;" +
                        "运势可能会上升$R;" +
                        "$R每次祈祷要收5个金币$R;");
                    switch (Select(pc, "要5个金币呢?", "", "算了", "请为我的凭依运祈祷"))
                    {
                        case 2:
                            if (pc.Gold < 5)
                            {
                                Say(pc, 131, "钱不够呢$R;");
                                return;
                            }
                            pc.Gold -= 5;
                            Say(pc, 131, "闪！$R;");
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            int b = Global.Random.Next(1, 4);
                            switch (b)
                            {
                                case 1:
                                    Say(pc, 131, "…$R;" +
                                        "$P现在用右手试一下凭依$R;" +
                                        "您会遇到好的人喔$R;");
                                    break;
                                case 2:
                                    Say(pc, 131, "…$R;" +
                                        "$P现在用左手试一下凭依$R;" +
                                        "您会遇到好的人喔$R;");
                                    break;
                                case 3:
                                    Say(pc, 131, "…$R;" +
                                        "现在用胸前饰物试一下凭依$R;" +
                                        "您一定会遇到好的人的$R;");
                                    break;
                                case 4:
                                    Say(pc, 131, "…$R;" +
                                        "现在用甲衣试一下凭依$R;" +
                                        "您会遇到好的人呢$R;");
                                    break;

                            }
                            break;
                    }
                    break;
            }
        }
    }
}