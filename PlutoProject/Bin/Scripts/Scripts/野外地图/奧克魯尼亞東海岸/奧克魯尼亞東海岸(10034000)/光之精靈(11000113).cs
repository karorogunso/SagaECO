using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東海岸(10034000) NPC基本信息:光之精靈(11000113) X:48 Y:95
namespace SagaScript.M10034000
{
    public class S11000113 : Event
    {
        public S11000113()
        {
            this.EventID = 11000113;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DHAFlags> mask = new BitMask<DHAFlags>(pc.CMask["DHA"]);
            BitMask<Neko_01> Neko_01_cmask = pc.CMask["Neko_01"];
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];
            BitMask<Crystal> Crystal_mask = pc.CMask["Crystal"];

            int MJ = 0;

            if (Crystal_mask.Test(Crystal.開始收集) &&
                !Crystal_mask.Test(Crystal.光之精靈) &&
                CountItem(pc, 10014300) >= 1)
            {
                if (!Crystal_mask.Test(Crystal.暗之精靈) && 
                    !Crystal_mask.Test(Crystal.炎之精靈) &&
                    !Crystal_mask.Test(Crystal.水之精靈) &&
                    !Crystal_mask.Test(Crystal.土之精靈) &&
                    !Crystal_mask.Test(Crystal.風之精靈) &&
                    CountItem(pc, 10014300) >= 1)
                {
                    Crystal_mask.SetValue(Crystal.風之精靈, true);
                    Say(pc, 131, "您好$R;" +
                        "$R我叫米玛斯！是光明精灵$R;" +
                        "$R什么事呢？$R;" +
                        "$P…$R;" +
                        "$R想在『水晶』上注入力量？$R;" +
                        "$R对我来说是很简单的事情呀$R;" +
                        "$P那么开始了$R;");
                    PlaySound(pc, 3120, false, 100, 50);
                    ShowEffect(pc, 4037);
                    Say(pc, 131, "『水晶』里注入力量了$R;" +
                        "…?$R;" +
                        "不能再增加力量了$R;");
                    return;
                }
                Say(pc, 131, "您好$R;" +
                    "$R我叫米玛斯！是光明精灵$R;" +
                    "$R什么事呢？$R;" +
                    "$P…$R;" +
                    "$R想在『水晶』上注入力量？$R;" +
                    "哎呀！$R;" +
                    "好像已经有别的精灵力量了$R;" +
                    "$P现在不能注入我的力量了$R;");
                return;
            }

            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.光之精靈對話) &&
                !Neko_01_cmask.Test(Neko_01.再次與祭祀對話))
            {
                Say(pc, 131, "不能帮到忙，很抱歉呀$R;");
                return;
            }

            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.與祭祀對話) &&
                !Neko_01_cmask.Test(Neko_01.光之精靈對話))
            {
                Say(pc, 131, "您好，我叫米玛斯！是光明精灵$R;" +
                    "$P哎呀！您身上附着猫灵呀？$R;");
                if (CountItem(pc, 10011210) >= 1)
                {
                    Say(pc, 131, "…？$R想把猫灵赶走吗？$R;" +
                        "$P在埃米尔和泰达尼亚世界里，$R召唤结束了的生命$R或过分的追逐灵魂是被禁止的。$R;");
                    Say(pc, 131, "只要我能办到的，我会尽力的$R;" +
                        "$P但是，我会收取相应的报酬的喔$R;" +
                        "$R给我一个您的$R『光之召唤石』可以吧？$R;");
                    switch (Select(pc, "怎么办呢?", "", "帮我赶走吗", "放弃"))
                    {
                        case 1:
                            Neko_01_cmask.SetValue(Neko_01.光之精靈對話, true);
                            TakeItem(pc, 10011210, 1);
                            Say(pc, 131, "知道了$R;" +
                                "$R把猫灵赶走，净化您的身体吧$R;" +
                                "$P闭上眼睛，平静心情。$R;" +
                                "$P…$R;");
                            Say(pc, 0, 131, "喵~~嗷$R;", " ");
                            Say(pc, 131, "…?$R;" +
                                "$P哎呀，不能赶走他…$R不知为什么呀？$R;" +
                                "$P这是怎么回事呢？$R;");
                            Say(pc, 0, 131, "喵$R;", " ");
                            Say(pc, 131, "唉！怎么会失败呢！！$R我的力量为什么赶不走呢！$R;" +
                                "$R不敢相信…！！$R;" +
                                "$P…对不起…$R失败了$R;" +
                                "$R猫灵好像很喜欢您$R;" +
                                "$P以我的力量是不能硬把它赶走的$R;");
                            break;
                        case 2:
                            Say(pc, 131, "知道了。不会勉强您的$R;");
                            break;
                    }
                    return;
                }
                Say(pc, 131, "我知道您需要什么？但是您…$R;" +
                    "$P没带光之召唤石啦$R;");
                return;
            }

            if (!mask.Test(DHAFlags.光之精靈第一次對話))
            {
                mask.SetValue(DHAFlags.光之精靈第一次對話, true);
                Say(pc, 131, "您好$R;" +
                    "$R我叫米玛斯！是光明精灵$R;" +
                    "$R什么事呢？$R;");
            }
            else
            {
                Say(pc, 131, "您好$R;" +
                    "什么事呢？$R;");
            }
            if (CountItem(pc, 10011210) >= 1 && CountItem(pc, 10026400) >= 1)
            {
                Say(pc, 131, "『光之召唤石』呀$R;" +
                    "$R用召唤石在$R;" +
                    "『木箭』上注入光之力是吗？$R;");
                switch (Select(pc, "怎么办呢?", "", "注入光之力", "放弃"))
                {
                    case 1:
                        PlaySound(pc, 3088, false, 100, 50);
                        Wait(pc, 2000);
                        Say(pc, 131, "『木箭』变成了『光明之箭』$R;");
                        TakeItem(pc, 10011210, 1);
                        while (CountItem(pc, 10026400) >= 1)
                        {
                            MJ++;
                        }
                        TakeItem(pc, 10026400, (ushort)MJ);
                        GiveItem(pc, 10026510, (ushort)MJ);
                        Say(pc, 131, "谢谢您给我『光之召唤石』$R;" +
                            "请再来玩呀！$R;");
                        break;
                    case 2:
                        Say(pc, 131, "啊！我误会您了$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "…?$R;" +
                "没有光之召唤石什么都$R"+"做不了啦$R;");
        }
    }
}