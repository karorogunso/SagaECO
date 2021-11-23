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
                Say(pc, 131, "快來吧$R;" +
                    "我是阿伊恩薩烏斯的母親。$R;" +
                    "$R我可以看見靈魂的閃爍喔。$R;" +
                    "這樣的人就是可以信任的。$R;" +
                    "而且越是這樣就越有名氣呢。$R;" +
                    "經常會得到任務呀$R;");
            }
            switch (Select(pc, "什麼事呢？", "", "看看我靈魂的閃爍吧", "算了"))//, "談人生（期間限定）", "算了"))
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
                        Say(pc, 131, "技能點數上升一點$R;");
                        Say(pc, 131, "現在的那個強光是什麼？$R;" +
                            "$P…$R;" +
                            "$R您的靈魂$R;" +
                            "擁有驚人的力量啊。$R;" +
                            "好像是您的力量和我的力量$R;" +
                            "衝突溢出了。$R;");
                        return;
                    }
                    if (pc.Fame == 0)
                    {
                        Say(pc, 131, "現在一點都不亮呢。$R;" +
                            "還是無名的初級冒險者吧。$R;");
                        return;
                    }
                    if (pc.Fame <= 99)
                    {
                        Say(pc, 131, "您裡面的光雖小$R;" +
                            "但就像泉水裡$R;" +
                            "閃爍著的一縷縷光輝呢$R;" +
                            "$R雖然不多，$R;" +
                            "還是有感謝您的人唷。$R;");
                        return;
                    }
                    if (pc.Fame <= 399)
                    {
                        Say(pc, 131, "有一團溫暖而柔和的光輝$R;" +
                            "圍繞著您。$R;" +
                            "數百個人要感謝您$R;" +
                            "他們對您有很大期望呢$R;");
                        return;
                    }
                    if (pc.Fame <= 999)
                    {
                        Say(pc, 131, "強大的光芒。$R;" +
                            "到現在您已經幫助了很多人呢。$R;" +
                            "您是人們的希望之光唷。$R;" +
                            "以後還有很多困苦的人$R需要您的幫忙啊$R;");
                        return;
                    }
                    if (pc.Fame >= 1000)
                    {
                        Say(pc, 131, "您是誰啊？$R;" +
                            "您靈魂的閃光好美麗啊。$R;" +
                            "要感謝您，尊敬您的人數，$R;" +
                            "真是驚人啊！$R;");
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
            switch (Select(pc, "跟我談談吧", "", "想重新開始人生", "想賺錢", "想交朋友", "憑依運不好", "不談了"))
            {
                case 1:
                    int a = pc.Level * 1000;
                    Say(pc, 131, "回到剛出生的時候吧。$R;" +
                        "想將獎勵點數$R;" +
                        "由1開始重新分配嗎？$R;" +
                        "$P好，幫您辦吧$R;" +
                        "$R可是需要交" + a + "金幣的錢呢$R;");
                    switch (Select(pc, "需要錢呢~?", "", "那就算了吧", "還是決定重生"))
                    {
                        case 2:
                            if (a < pc.Gold)
                            {
                                Say(pc, 131, "錢不夠呢$R;");
                                return;
                            }
                            pc.Gold -= a;
                            //STATUSRESET
                            Say(pc, 131, "閃閃！$R;");
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "狀態初級化$R;");
                            break;
                    }
                    break;
                case 2:
                    Say(pc, 131, "誰都想賺錢呢。$R;" +
                        "$P魔物掉下來的道具$R;" +
                        "雖然比較便宜，$R但經過合成和精製的話$R;" +
                        "也可以賣高價的阿$R;" +
                        "$R好好利用賺大錢$R;" +
                        "不是很聰明嗎?$R;");
                    break;
                case 3:
                    Say(pc, 131, "到隊伍招募廣場$R;" +
                        "以隊伍為單位的任務$R;" +
                        "會有很多人參加的。$R;" +
                        "$R然後在來的人中$R;" +
                        "給喜歡的人寫信就好了。$R;" +
                        "$P一開始跟他說話$R;" +
                        "需要很大的勇氣呢$R;" +
                        "但也要試一下啊！$R;");
                    break;
                case 4:
                    Say(pc, 131, "不能回到最初的憑依狀態嗎?$R;" +
                        "那個真的是憑運氣的$R;" +
                        "$P但要是我幫您祈禱的話$R;" +
                        "運勢可能會上升唷$R;" +
                        "$R每次祈禱要收5個金幣$R;");
                    switch (Select(pc, "要5個金幣呢?", "", "算了", "請為我的憑依運祈禱"))
                    {
                        case 2:
                            if (pc.Gold < 5)
                            {
                                Say(pc, 131, "錢不夠呢$R;");
                                return;
                            }
                            pc.Gold -= 5;
                            Say(pc, 131, "閃！$R;");
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            int b = Global.Random.Next(1, 4);
                            switch (b)
                            {
                                case 1:
                                    Say(pc, 131, "…$R;" +
                                        "$P現在用右手試一下憑依$R;" +
                                        "您會遇到好的人喔$R;");
                                    break;
                                case 2:
                                    Say(pc, 131, "…$R;" +
                                        "$P現在用左手試一下憑依$R;" +
                                        "您會遇到好的人喔$R;");
                                    break;
                                case 3:
                                    Say(pc, 131, "…$R;" +
                                        "現在用胸前飾物試一下憑依$R;" +
                                        "您一定會遇到好的人唷$R;");
                                    break;
                                case 4:
                                    Say(pc, 131, "…$R;" +
                                        "現在用甲衣試一下憑依$R;" +
                                        "您會遇到好的人喔$R;");
                                    break;

                            }
                            break;
                    }
                    break;
            }
        }
    }
}