using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30140003
{
    public class S11000597 : Event
    {
        public S11000597()
        {
            this.EventID = 11000597;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_07> mask = pc.CMask["Job2X_07"];
            BitMask<Neko_01> Neko_01_cmask = pc.CMask["Neko_01"];
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];

            if (Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.獲得桃子))
            {
                Say(pc, 131, "您决定和那凱堤一起生活，$R是您的決心出現了奇蹟吧！$R;" +
                    "$P這下好了！$R回到您的世界吧。$R;");
                Neko_01_cmask.SetValue(Neko_01.獲得桃子, false);
                return;
            }
            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.得到裁縫阿姨的三角巾) &&
                !Neko_01_cmask.Test(Neko_01.獲得桃子) &&
                CountItem(pc, 10017904) >= 1)
            {
                Say(pc, 131, "啊!!$R您怎麼會來的？$R;");
                Say(pc, 0, 131, "喵~~$R;", "");
                Say(pc, 131, "啊，您的身上好像附上了凱堤呢$R;" +
                    "$R是它的魂把您引到這裡來的$R;");
                Say(pc, 131, "……?$R是不是想把它的魂驅走?$R;" +
                    "$R我雖然可以把它驅走$R;" +
                    "$R但它好像相當喜歡您呢$R;" +
                    "$P它是個好孩子，$R和它一起生活看看如何？$R;");
                Say(pc, 131, "再説……「擊退靈魂」挺危險的。$R;" +
                    "$P將迷戀現世的靈魂强行驅走，$R失敗的話……$R那個魂魄會墜落到黑暗裡，$R永遠得不到解脱。$R;" +
                    "$P那麼，您還想把它驅走嗎？$R;");
                桃子任務(pc);
                return;
            }
            
            
            if (mask.Test(Job2X_07.獲得神官的烙印))//_3A88)
            {
                Say(pc, 131, "謝謝！我不會忘記您的$R;");
                return;
            }
            if (pc.Job != PC_JOB.VATES || !mask.Test(Job2X_07.轉職開始))//_3A84)
            {
                Say(pc, 131, "回答您也沒意思……$R妹妹的心成為了結晶，$R散佈世界各地。$R;");
                return;
            }
            mask.SetValue(Job2X_07.獲得神官的烙印, true);
            //_3A88 = true;
            Say(pc, 131, "發現了「心之破片」嗎？$R那是我妹妹微微的心$R化成結晶灑落在世間的。$R;" +
                "$P我叫提多$R是塔妮亞第三部族的宙天師$R那邊的……那個是塔妮亞的微微$R;" +
                "$R是我的妹妹啊$R;" +
                "$P您幫我找到了那個破片$R真的很感謝。$R;" +
                "我正在一片一片的$R搜集妹妹的心之破片，$R一定要找回我妹妹的心。$R;");
            Say(pc, 131, "我的妹妹真的是個傻瓜$R;" +
                "$R原來您是祭司$R那就應該知道$R;" +
                "$P即使祭司可以治療很深的傷口$R;" +
                "$R但是失去的生命，是無法挽回的$R;" +
                "$P我的妹妹為了救活埃米爾少年$R才犧牲自己的心$R;" +
                "$R因為她深信，$R那個是賦予在自己身上的$R「塔妮亞的試煉」$R;");
            Say(pc, 131, "……!?$R;");
            Say(pc, 11000598, 131, "……哥……$R;");
            Say(pc, 131, "!!!$R微微!?$R;");
            Say(pc, 11000598, 131, "……$R;");
            PlaySound(pc, 3087, false, 100, 50);
            ShowEffect(pc, 4131);
            Wait(pc, 4000);
            Say(pc, 131, "啊…不…您身上$R有「神官的烙印」啊。$R;" +
                "$R妹妹……微微她…$R好像承認您是神官呢。$R;" +
                "$P可能是您給妹妹的破片$R讓她暫時恢復意識的。$R;" +
                "$R謝謝您……$R;");
            Say(pc, 131, "那傳送裝置的目的地$R阿高普路斯的白聖堂$R;" +
                "$P讓祭司總管看看您身上的$R「神官的烙印」吧。$R;" +
                "$P為了妹妹，我要留在這裡$R;" +
                "$R這個可能…$R就是給我的「塔妮亞的試煉」$R;");
            
        }

        void 桃子任務(ActorPC pc)
        {
            BitMask<Neko_01> Neko_01_cmask = pc.CMask["Neko_01"];
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];

            switch (Select(pc, "怎麼做呢?", "", "驅趕", "一起生活"))
            {
                case 1:
                    Say(pc, 131, "真的没關係嗎?$R;");
                    switch (Select(pc, "怎麼做呢?", "", "驅趕", "再想想"))
                    {
                        case 1:
                            Neko_01_cmask.SetValue(Neko_01.與瑪歐斯對話, false);
                            Neko_01_cmask.SetValue(Neko_01.與雷米阿對話, false);
                            Neko_01_cmask.SetValue(Neko_01.與祭祀對話, false);
                            Neko_01_cmask.SetValue(Neko_01.光之精靈對話, false);
                            Neko_01_cmask.SetValue(Neko_01.再次與祭祀對話, false);
                            Neko_01_cmask.SetValue(Neko_01.得到裁縫阿姨的三角巾, false);
                            Neko_01_cmask.SetValue(Neko_01.使用不明的鬍鬚, false);
                            TakeItem(pc, 10017904, 1);
                            TakeItem(pc, 10035610, 1);
                            Say(pc, 131, "知道了，開始了$R;" +
                                "$R閉上眼睛……$R;");
                            Say(pc, 0, 131, "喵~~~$R;");
                            Say(pc, 131, "咦？$R;" +
                                "$R感覺……消失了?$R;");
                            Say(pc, 131, "它好像看出我要驅趕它，$R它跑掉了$R;" +
                                "$R這下問題解决了吧$R;");
                            Say(pc, 0, 131, "「裁縫阿姨的三角巾」遺失了$R;");
                            return;
                        case 2:
                            桃子任務(pc);
                            break;
                    }
                    break;
                case 2:
                    Neko_01_amask.SetValue(Neko_01.桃子任務完成, true);
                    Neko_01_cmask.SetValue(Neko_01.與瑪歐斯對話, false);
                    Neko_01_cmask.SetValue(Neko_01.與雷米阿對話, false);
                    Neko_01_cmask.SetValue(Neko_01.與祭祀對話, false);
                    Neko_01_cmask.SetValue(Neko_01.光之精靈對話, false);
                    Neko_01_cmask.SetValue(Neko_01.再次與祭祀對話, false);
                    Neko_01_cmask.SetValue(Neko_01.得到裁縫阿姨的三角巾, false);
                    Neko_01_cmask.SetValue(Neko_01.獲得桃子, true);
                    TakeItem(pc, 10017904, 1);
                    TakeItem(pc, 10035610, 1);
                    GiveItem(pc, 10017900, 1);
                    Say(pc, 0, 131, "喵~$R;");
                    Say(pc, 0, 131, "「裁縫阿姨的三角巾」遺失了$R;");
                    Say(pc, 0, 131, "得到「凱堤」$R;");
                    Say(pc, 131, "啊？$R;" +
                        "$R三角巾上竟然附著凱堤$R;");
                    Say(pc, 0, 131, "喵~$R;");
                    Say(pc, 131, "或許……這是奇蹟吧……$R;" +
                        "$R自己復活了？$R;");
                    return;
            } 
        }
    }
}