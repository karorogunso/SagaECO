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
                Say(pc, 131, "來，選個您喜歡的吧。$R;");
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
                Say(pc, 131, "沒有適合您的呢。$R;");
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
                                "我的兒子還健康嗎？$R;" +
                                "$R呵呵，等他回來以後$R;" +
                                "要給他做點好吃的唷。$R;" +
                                "$P看來還要答謝您呢。$R;" +
                                "$R這是我們家代代相傳的防具，$R;" +
                                "便宜點賣給您吧$R;");
                            Say(pc, 131, "來，選個您喜歡的吧。$R;");
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
                            Say(pc, 131, "沒有適合您的呢。$R;");
                        }
                        if (mask.Test(AYEFlags.尋找阿魯斯開始))//_4a66)
                        {
                            Say(pc, 131, "$P我可愛的兒子，去了阿高普路斯$R;" +
                                "還沒有回來呢。$R;" +
                                "$R您能不能幫我叫他$R;" +
                                "快點回來呢？$R;");
                            return;
                        }
                        mask.SetValue(AYEFlags.尋找阿魯斯開始, true);
                        //_4a66 = true;
                        Say(pc, 131, "您好像是挺有實力的戰士呢。$R;" +
                            "等級高，又有錢$R;" +
                            "$R嗯，好，就拜託您吧。$R;" +
                            "$P我可愛的兒子，去了阿高普路斯$R;" +
                            "還沒有回來呢。$R;" +
                            "$R您能不能幫我叫他$R;" +
                            "快點回來呢？$R;");
                        return;
                    }
                    Say(pc, 131, "最討厭窮人了阿$R;");
                    return;
                }
                Say(pc, 131, "和等級低的人，沒什麼好說的。$R;");
                return;
            }
            Say(pc, 131, "真正的上流的人$R;" +
                "指的就是我這樣的人吧$R;");
        }
    }
}