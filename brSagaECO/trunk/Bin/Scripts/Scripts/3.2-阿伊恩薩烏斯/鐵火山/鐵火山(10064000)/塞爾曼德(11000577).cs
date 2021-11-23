using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10064000
{
    public class S11000577 : Event
    {
        public S11000577()
        {
            this.EventID = 11000577;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NBDLFlags> mask = new BitMask<NBDLFlags>(pc.CMask["NBDL"]);
            BitMask<Job2X_09> Job2X_09_mask = pc.CMask["Job2X_09"];
            
            if (pc.Job == PC_JOB.TATARABE//ME.JOB = 81
            && Job2X_09_mask.Test(Job2X_09.轉職開始) &&
            !Job2X_09_mask.Test(Job2X_09.獲得塞爾曼德的心臟))// _3a42 && !_3a43)
            {
                if (CountItem(pc, 10013850) >= 1)
                {
                    if (pc.Marionette != null)//ISMARIO
                    {
                        Say(pc, 131, "要解除活動木偶$R;");
                        return;
                    }
                    Say(pc, 131, "做好心理準備了嗎？$R;" +
                        "$R等一下，那個孩子$R;" +
                        "想和您說話呢。$R;" +
                        "聽嗎？$R;");
                    switch (Select(pc, "要聽嗎？", "", "聽", "現在不聽"))
                    {
                        case 1:
                            Job2X_09_mask.SetValue(Job2X_09.獲得塞爾曼德的心臟, true);
                           // _3a43 = true;
                            TakeItem(pc, 10013850, 1);
                            GiveItem(pc, 10014200, 1);
                            //NPCPICTCHANGE 11000260 12003000
                            ShowEffect(pc, 12003000, 8015);
                            Wait(pc, 3000);
                            Say(pc, 131, "您好$R;" +
                                "好像是第一次跟您說話$R;" +
                                "$R我知道您來這裡的目的$R;" +
                                "$P…需要我的心臟吧?$R;" +
                                "沒關係，不用太傷心。$R;" +
                                "$R就算我的心臟給您了，$R;" +
                                "我也不會死的。$R;" +
                                "$P把心臟給您$R;" +
                                "就是跟您成為一體$R;" +
                                "是最親密的關係阿$R;" +
                                "$R所以沒什麼不好的$R;" +
                                "$P但要是跟您成為一體$R;" +
                                "就不可以跟您鬥嘴$R;" +
                                "又不可以跟您聊天$R;" +
                                "$R只有那小小的遺憾$R;" +
                                "$P…$R;" +
                                "$P可以幫我多辦一件事嗎?$R;" +
                                "幫我取一個名字，好嗎?$R;" +
                                "$R我快要消失了$R;" +
                                "所以，請幫我起個名字$R;" +
                                "證明我曾經存在吧$R;");
                            string count = string.Format(InputBox(pc, "給我取名吧。", InputType.PetRename));
                            //"INPUTSTRING STR1 ""給我取名吧。"""
                            //PARAM STR1 + …
                            Say(pc, 131, count + "…$R;" +
                                "$P很好的名字啊，謝謝$R;" +
                                "$R真的很開心$R;" +
                                "$R那現在，我該走了$R;" +
                                "再見！$R;");
                            //NPCPICTCHANGE 12003000 12003000
                            Fade(pc, FadeType.Out, FadeEffect.Black);
                            //FADE OUT BLACK
                            Wait(pc, 3000);
                            Fade(pc, FadeType.In, FadeEffect.Black);
                            //FADE IN
                            Say(pc, 131, "得到『塞爾曼德的心臟』$R;");
                            break;
                        case 2:
                            break;
                    }
                    return;
                }
                Say(pc, 131, "…$R想把火的化身紋在身上？$R;" +
                    "$P火的化身就是塞爾曼德啊$R;" +
                    "如果吃了塞爾曼德的心臟$R;" +
                    "您的內心就會附有塞爾曼德$R;" +
                    "武器製造所的火$R也傷不到您強健的身體唷。$R;" +
                    "將塞爾曼德帶走吧$R;");
                return;
            }
             
            if (mask.Test(NBDLFlags.未获得特效藥) )//_2A54)
            {
                if (CountItem(pc, 10011308) >= 1 && CountItem(pc, 10007650) >= 1 && CountItem(pc, 10001111) >= 1)
                {
                    Say(pc, 131, "都搜集到了啊？$R;" +
                        "全都找回來了?$R;" +
                        "稍等一下$R;" +
                        "$P…$R;" +
                        "$P…$R;" +
                        "$P…$R;" +
                        "$P來$R;" +
                        "$R答應您的『火焰特效藥』$R;" +
                        "$R拿著吧$R;");
                    if (CheckInventory(pc, 10000601, 1))
                    {
                        TakeItem(pc, 10011308, 1);
                        TakeItem(pc, 10007650, 1);
                        TakeItem(pc, 10001111, 1);
                        GiveItem(pc, 10000601, 1);
                        mask.SetValue(NBDLFlags.接受特效藥任务, false);
                        mask.SetValue(NBDLFlags.未获得特效藥, false);
                        //_2A52 = false;
                        //_2A54 = false;
                        Say(pc, 131, "給他一個『火山岩』$R;" +
                            "給他一個『鳥嘴』$R;" +
                            "給他一個『怒火小幽浮的灰』$R;" +
                            "得到一個『火焰特效藥』$R;");
                        return;
                    }
                    Say(pc, 131, "行李太多了，整理一下後再來吧$R;");
                    return;
                }
                Say(pc, 131, "材料有$R;" +
                    "『火山岩』$R;" +
                    "『鳥嘴』$R;" +
                    "『怒火小幽浮的灰』$R;" +
                    "就是這樣$R;" +
                    "$P那等您啊！$R;");
                return;
            }
            if (mask.Test(NBDLFlags.接受特效藥任务))//_2A52)
            {
                mask.SetValue(NBDLFlags.未获得特效藥, true);
                //_2A54 = true;
                Say(pc, 131, "有人拜託您拿特效藥嗎?$R;" +
                    "$R不好意思啊，特效藥沒了$R;" +
                    "$P啊！放心$R;" +
                    "有材料的話就可以給您做$R;" +
                    "那我現在告訴您材料$R;" +
                    "您拿來了，我就做給您$R;" +
                    "材料可以在$R;" +
                    "這附近的魔物會掉落的。$R;" +
                    "$R那現在告訴您了$R;");
                if (CountItem(pc, 10011308) >= 1 && CountItem(pc, 10007650) >= 1 && CountItem(pc, 10001111) >= 1)
                {
                    Say(pc, 131, "都搜集到了啊？$R;" +
                        "全都找回來了?$R;" +
                        "稍等一下$R;" +
                        "$P…$R;" +
                        "$P…$R;" +
                        "$P…$R;" +
                        "$P來$R;" +
                        "$R答應您的『火焰特效藥』$R;" +
                        "$R拿著吧$R;");
                    if (CheckInventory(pc, 10000601, 1))
                    {
                        TakeItem(pc, 10011308, 1);
                        TakeItem(pc, 10007650, 1);
                        TakeItem(pc, 10001111, 1);
                        GiveItem(pc, 10000601, 1);
                        mask.SetValue(NBDLFlags.接受特效藥任务, false);
                        mask.SetValue(NBDLFlags.未获得特效藥, false);
                        //_2A52 = false;
                        //_2A54 = false;
                        Say(pc, 131, "給他一個『火山岩』$R;" +
                            "給他一個『鳥嘴』$R;" +
                            "給他一個『怒火小幽浮的灰』$R;" +
                            "得到一個『火焰特效藥』$R;");
                        return;
                    }
                    Say(pc, 131, "行李太多了，整理一下後再來吧$R;");
                    return;
                }
                Say(pc, 131, "材料有$R;" +
                    "『火山岩』$R;" +
                    "『鳥嘴』$R;" +
                    "『怒火小幽浮的灰』$R;" +
                    "就是這樣$R;" +
                    "$P那等您啊！$R;");
                return;
            }
            
            Say(pc, 131, "…$R;");
        }
    }
}