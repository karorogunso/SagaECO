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
                Say(pc, 131, "您决定和那猫灵一起生活，$R是您的决心出现了奇迹吧！$R;" +
                    "$P好了！$R回到您的世界吧。$R;");
                Neko_01_cmask.SetValue(Neko_01.獲得桃子, false);
                return;
            }
            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.得到裁縫阿姨的三角巾) &&
                !Neko_01_cmask.Test(Neko_01.獲得桃子) &&
                CountItem(pc, 10017904) >= 1)
            {
                Say(pc, 131, "啊!!$R您怎么会来的？$R;");
                Say(pc, 0, 131, "喵~~$R;", "");
                Say(pc, 131, "啊，您的身上好像附上了猫灵呢$R;" +
                    "$R是它的魂把您引到这里来的$R;");
                Say(pc, 131, "……?$R是不是想把它的魂驱走?$R;" +
                    "$R我虽然可以把它驱走$R;" +
                    "$R但它好像相当喜欢您呢$R;" +
                    "$P它是个好孩子，$R和它一起生活看看如何？$R;");
                Say(pc, 131, "再者……「击退灵魂」挺危险的。$R;" +
                    "$P将迷恋现世的灵魂强行驱走，$R失败的话……$R那个魂魄会坠落到黑暗里，$R永远得不到解脱。$R;" +
                    "$P那么，您还想把它驱走吗？$R;");
                桃子任務(pc);
                return;
            }
            
            
            if (mask.Test(Job2X_07.獲得神官的烙印))//_3A88)
            {
                Say(pc, 131, "谢谢！我不会忘记您的$R;");
                return;
            }
            if (pc.Job != PC_JOB.VATES || !mask.Test(Job2X_07.轉職開始))//_3A84)
            {
                Say(pc, 131, "告诉你也没关系……$R妹妹的心化为了结晶，$R散布于世界各地。$R;");
                return;
            }
            mask.SetValue(Job2X_07.獲得神官的烙印, true);
            //_3A88 = true;
            Say(pc, 131, "发现了「心之破片」吗？$R那是我妹妹蒂塔的心$R化成结晶洒落在世间的。$R;" +
                "$P我叫泰塔斯$R是泰达尼亚第三部族的大天使$R那边的……那个是泰达尼亚的蒂塔$R;" +
                "$R是我的妹妹$R;" +
                "$P您帮我找到了那个破片$R真的很感谢。$R;" +
                "我正在一片一片的$R搜集妹妹的心之破片，$R一定要找回我妹妹的心。$R;");
            Say(pc, 131, "我的妹妹真的是个傻瓜$R;" +
                "$R原来您是祭司$R那就应该知道$R;" +
                "$P即使祭司可以治疗很深的伤口$R;" +
                "$R但是失去的生命，是无法挽回的$R;" +
                "$P我的妹妹为了救活埃米尔少年$R才牺牲自己的心$R;" +
                "$R因为她深信，$R那个是赋予在自己身上的$R「泰达尼亚的试炼」$R;");
            Say(pc, 131, "……!?$R;");
            Say(pc, 11000598, 131, "……哥……哥$R;");
            Say(pc, 131, "!!!$R蒂塔!?$R;");
            Say(pc, 11000598, 131, "……$R;");
            PlaySound(pc, 3087, false, 100, 50);
            ShowEffect(pc, 4131);
            Wait(pc, 4000);
            Say(pc, 131, "啊…不…您身上$R有「神官的烙印」啊。$R;" +
                "$R妹妹……蒂塔她…$R好像承认您是神官呢。$R;" +
                "$P可能是您给妹妹的破片$R让她暂时恢复意识的。$R;" +
                "$R谢谢您……$R;");
            Say(pc, 131, "那传送装置的目的地$R阿克罗波利斯的白之圣堂$R;" +
                "$P让祭司总管看看您身上的$R「神官的烙印」吧。$R;" +
                "$P为了妹妹，我要留在这里$R;" +
                "$R这个可能…$R就是给我的「泰达尼亚的试炼」$R;");
            
        }

        void 桃子任務(ActorPC pc)
        {
            BitMask<Neko_01> Neko_01_cmask = pc.CMask["Neko_01"];
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];

            switch (Select(pc, "怎么做呢?", "", "驱赶", "一起生活"))
            {
                case 1:
                    Say(pc, 131, "真的没关系吗?$R;");
                    switch (Select(pc, "怎么做呢?", "", "驱赶", "再想想"))
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
                            Say(pc, 131, "知道了，开始了$R;" +
                                "$R闭上眼睛……$R;");
                            Say(pc, 0, 131, "喵~~~$R;");
                            Say(pc, 131, "咦？$R;" +
                                "$R感觉……消失了?$R;");
                            Say(pc, 131, "它好像看出我要驱赶它，$R它跑掉了$R;" +
                                "$R这下问题解决了吧$R;");
                            Say(pc, 0, 131, "「裁缝阿姨的三角巾」遗失了$R;");
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
                    Say(pc, 0, 131, "「裁缝阿姨的三角巾」遗失了$R;");
                    Say(pc, 0, 131, "得到「猫灵」$R;");
                    Say(pc, 131, "啊？$R;" +
                        "$R三角巾上竟然附著猫灵$R;");
                    Say(pc, 0, 131, "喵~$R;");
                    Say(pc, 131, "或许……这是奇迹吧……$R;" +
                        "$R自己复活了？$R;");
                    return;
            } 
        }
    }
}