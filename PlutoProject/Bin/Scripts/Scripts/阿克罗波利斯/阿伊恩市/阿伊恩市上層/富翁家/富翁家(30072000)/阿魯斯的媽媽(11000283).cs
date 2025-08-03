using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaLib;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30072000
{
    public class S11000283 : Event
    {
        public S11000283()
        {
            this.EventID = 11000283;

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = new BitMask<AYEFlags>(pc.CMask["AYE"]);
            if (mask.Test(AYEFlags.尋找阿魯斯結束))//_4a68)
            {
                Say(pc, 131, "来，选个您喜欢的吧。$R;");
                if (pc.Job == PC_JOB.SWORDMAN
                    || pc.Job == PC_JOB.BLADEMASTER
                    || pc.Job == PC_JOB.BOUNTYHUNTER)
                {
                    OpenShopBuy(pc, 116);
                    return;
                }
                if (pc.Job == PC_JOB.FENCER
                    || pc.Job == PC_JOB.KNIGHT
                    || pc.Job == PC_JOB.DARKSTALKER)
                {
                    OpenShopBuy(pc, 117);
                    return;
                }
                if (pc.Job == PC_JOB.SCOUT
                    || pc.Job == PC_JOB.ASSASSIN
                    || pc.Job == PC_JOB.COMMAND)
                {
                    OpenShopBuy(pc, 118);
                    return;
                }
                if (pc.Job == PC_JOB.ARCHER
                    || pc.Job == PC_JOB.STRIKER
                    || pc.Job == PC_JOB.GUNNER)
                {
                    OpenShopBuy(pc, 119);
                    return;
                }
                Say(pc, 131, "没有适合您的呢。$R;");
                return;
            }
            if (pc.Job == PC_JOB.SWORDMAN
                || pc.Job == PC_JOB.BLADEMASTER
                || pc.Job == PC_JOB.BOUNTYHUNTER
                || pc.Job == PC_JOB.FENCER
                || pc.Job == PC_JOB.KNIGHT
                || pc.Job == PC_JOB.DARKSTALKER
                || pc.Job == PC_JOB.SCOUT
                || pc.Job == PC_JOB.ASSASSIN
                || pc.Job == PC_JOB.COMMAND
                || pc.Job == PC_JOB.ARCHER
                || pc.Job == PC_JOB.STRIKER
                || pc.Job == PC_JOB.GUNNER)
            {
                if (pc.Level > 39)
                {
                    if (pc.Gold > 100000)
                    {
                        if (mask.Test(AYEFlags.找到阿魯斯))//_4a67)
                        {
                            mask.SetValue(AYEFlags.尋找阿魯斯結束, true);
                            //_4a68 = true;
                            Say(pc, 131, "真的辛苦您了呢。$R;" +
                                "我的儿子还健康吗？$R;" +
                                "$R呵呵，等他回来以后$R;" +
                                "要给他做点好吃的。$R;" +
                                "$P看来还要答谢您呢。$R;" +
                                "$R这是我们家代代相传的防具，$R;" +
                                "便宜点卖给您吧$R;");
                            Say(pc, 131, "来，选个您喜欢的吧。$R;");
                            if (pc.Job == PC_JOB.SWORDMAN
                                || pc.Job == PC_JOB.BLADEMASTER
                                || pc.Job == PC_JOB.BOUNTYHUNTER)
                            {
                                OpenShopBuy(pc, 116);
                                return;
                            }
                            if (pc.Job == PC_JOB.FENCER
                                || pc.Job == PC_JOB.KNIGHT
                                || pc.Job == PC_JOB.DARKSTALKER)
                            {
                                OpenShopBuy(pc, 117);
                                return;
                            }
                            if (pc.Job == PC_JOB.SCOUT
                                || pc.Job == PC_JOB.ASSASSIN
                                || pc.Job == PC_JOB.COMMAND)
                            {
                                OpenShopBuy(pc, 118);
                                return;
                            }
                            if (pc.Job == PC_JOB.ARCHER
                                || pc.Job == PC_JOB.STRIKER
                                || pc.Job == PC_JOB.GUNNER)
                            {
                                OpenShopBuy(pc, 119);
                                return;
                            }
                            Say(pc, 131, "没有适合您的呢。$R;");
                        }
                        if (mask.Test(AYEFlags.尋找阿魯斯開始))//_4a66)
                        {
                            Say(pc, 131, "$P我可爱的儿子，去了阿克罗波利斯$R;" +
                                "还没有回来呢。$R;" +
                                "$R您能不能帮我叫他$R;" +
                                "快点回来呢？$R;");
                            return;
                        }
                        mask.SetValue(AYEFlags.尋找阿魯斯開始, true);
                        //_4a66 = true;
                        Say(pc, 131, "您好像是挺有实力的战士呢。$R;" +
                            "等级高，又有钱$R;" +
                            "$R嗯，好，就拜托您吧。$R;" +
                            "$P我可爱的儿子，去了阿克罗波利斯$R;" +
                            "还没有回来呢。$R;" +
                            "$R您能不能帮我叫他$R;" +
                            "快点回来呢？$R;");
                        return;
                    }
                    Say(pc, 131, "最讨厌穷人了阿$R;");
                    return;
                }
                Say(pc, 131, "和等级低的人，没什么好说的。$R;");
                return;
            }
            Say(pc, 131, "真正的上流的人$R;" +
                "指的就是我这样的人吧$R;");
        }
    }
}