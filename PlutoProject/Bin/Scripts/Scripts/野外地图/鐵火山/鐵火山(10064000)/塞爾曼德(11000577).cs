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
                        Say(pc, 131, "要解除活动木偶$R;");
                        return;
                    }
                    Say(pc, 131, "做好心理准备了吗？$R;" +
                        "$R等一下，那个孩子$R;" +
                        "想和您说话呢。$R;" +
                        "听吗？$R;");
                    switch (Select(pc, "要听吗？", "", "听", "现在不听"))
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
                                "好像是第一次跟您说话$R;" +
                                "$R我知道您来这里的目的$R;" +
                                "$P…需要我的心脏吧?$R;" +
                                "没关系，不用太伤心。$R;" +
                                "$R就算我的心脏给您了，$R;" +
                                "我也不会死的。$R;" +
                                "$P把心脏给您$R;" +
                                "就是跟您成为一体$R;" +
                                "是最亲密的关系啊$R;" +
                                "$R所以没什么不好的$R;" +
                                "$P但要是跟您成为一体$R;" +
                                "就不可以跟您斗嘴$R;" +
                                "又不可以跟您聊天$R;" +
                                "$R只有那小小的遗憾$R;" +
                                "$P…$R;" +
                                "$P可以帮我多办一件事吗?$R;" +
                                "帮我取一个名字，好吗?$R;" +
                                "$R我快要消失了$R;" +
                                "所以，请帮我起个名字$R;" +
                                "证明我曾经存在吧$R;");
                            string count = string.Format(InputBox(pc, "给我取名吧。", InputType.PetRename));
                            //"INPUTSTRING STR1 ""给我取名吧。"""
                            //PARAM STR1 + …
                            Say(pc, 131, count + "…$R;" +
                                "$P很好的名字啊，谢谢$R;" +
                                "$R真的很开心$R;" +
                                "$R那现在，我该走了$R;" +
                                "再见！$R;");
                            //NPCPICTCHANGE 12003000 12003000
                            Fade(pc, FadeType.Out, FadeEffect.Black);
                            //FADE OUT BLACK
                            Wait(pc, 3000);
                            Fade(pc, FadeType.In, FadeEffect.Black);
                            //FADE IN
                            Say(pc, 131, "得到『烈焰蜥蜴的心脏』$R;");
                            break;
                        case 2:
                            break;
                    }
                    return;
                }
                Say(pc, 131, "…$R想把火的化身纹在身上？$R;" +
                    "$P火的化身就是烈焰蜥蜴啊$R;" +
                    "如果吃了烈焰蜥蜴的心脏$R;" +
                    "您的内心就会附有烈焰蜥蜴$R;" +
                    "铁匠铺的火$R也伤不到您强健的身体哦。$R;" +
                    "将烈焰蜥蜴带走吧$R;");
                return;
            }
             
            if (mask.Test(NBDLFlags.未获得特效藥) )//_2A54)
            {
                if (CountItem(pc, 10011308) >= 1 && CountItem(pc, 10007650) >= 1 && CountItem(pc, 10001111) >= 1)
                {
                    Say(pc, 131, "都搜集到了啊？$R;" +
                        "全都找回来了?$R;" +
                        "稍等一下$R;" +
                        "$P…$R;" +
                        "$P…$R;" +
                        "$P…$R;" +
                        "$P来$R;" +
                        "$R答应您的『火焰特效药』$R;" +
                        "$R拿着吧$R;");
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
                        Say(pc, 131, "给他一个『火山岩』$R;" +
                            "给他一个『鸟喙』$R;" +
                            "给他一个『黯淡的灰』$R;" +
                            "得到一个『炎之特效药』$R;");
                        return;
                    }
                    Say(pc, 131, "行李太多了，整理一下后再来吧$R;");
                    return;
                }
                Say(pc, 131, "材料有$R;" +
                    "『火山岩』$R;" +
                    "『鸟喙』$R;" +
                    "『黯淡的灰』$R;" +
                    "就是这样$R;" +
                    "$P那等您啊！$R;");
                return;
            }
            if (mask.Test(NBDLFlags.接受特效藥任务))//_2A52)
            {
                mask.SetValue(NBDLFlags.未获得特效藥, true);
                //_2A54 = true;
                Say(pc, 131, "有人拜托您拿特效药吗?$R;" +
                    "$R不好意思啊，特效药没了$R;" +
                    "$P啊！放心$R;" +
                    "有材料的话就可以给您做$R;" +
                    "那我现在告诉您材料$R;" +
                    "您拿来了，我就做给您$R;" +
                    "材料可以在$R;" +
                    "这附近的魔物会掉落的。$R;" +
                    "$R那现在告诉您了$R;");
                if (CountItem(pc, 10011308) >= 1 && CountItem(pc, 10007650) >= 1 && CountItem(pc, 10001111) >= 1)
                {
                    Say(pc, 131, "都搜集到了啊？$R;" +
                        "全都找回来了?$R;" +
                        "稍等一下$R;" +
                        "$P…$R;" +
                        "$P…$R;" +
                        "$P…$R;" +
                        "$P来$R;" +
                        "$R答应您的『炎之特效药』$R;" +
                        "$R拿着吧$R;");
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
                        Say(pc, 131, "给他一个『火山岩』$R;" +
                            "给他一个『鸟喙』$R;" +
                            "给他一个『黯淡的灰』$R;" +
                            "得到一个『炎之特效药』$R;");
                        return;
                    }
                    Say(pc, 131, "行李太多了，整理一下后再来吧$R;");
                    return;
                }
                Say(pc, 131, "材料有$R;" +
                    "『火山岩』$R;" +
                    "『鸟喙』$R;" +
                    "『黯淡的灰』$R;" +
                    "就是这样$R;" +
                    "$P那您的好消息！$R;");
                return;
            }
            
            Say(pc, 131, "…$R;");
        }
    }
}