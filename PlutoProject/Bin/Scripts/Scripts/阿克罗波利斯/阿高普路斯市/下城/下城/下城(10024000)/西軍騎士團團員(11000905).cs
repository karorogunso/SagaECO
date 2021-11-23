using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:下城(10024000) NPC基本信息:西軍騎士團團員(11000905) X:133 Y:50
namespace SagaScript.M10024000
{
    public class S11000905 : Event
    {
        public S11000905()
        {
            this.EventID = 11000905;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<INSD> mask = pc.CMask["INSD"];

            if (pc.Account.GMLevel >= 100)
            {
                switch (Select(pc, "GM用选择列表", "", "进入广场", "普通玩家对话", "什么都不做"))
                { 
                    case 1:
                        Warp(pc, 20080012, 22, 28);
                        return;

                    case 3:
                        return;
                }
            }

            if (pc.Fame < 10)
            {
                Say(pc, 131, "…不好意思$R;" +
                    "可能对你是困难的事情$R;");
                return;
            }
            if (mask.Test(INSD.西軍團員要求協助))//_0C65)
            {
                Say(pc, 131, "现在都准备好了吗?$R;" +
                    "那我来带路到遗迹吧$R;");
                Warp(pc, 20080012, 22, 28);
                return;
            }
            if (mask.Test(INSD.西軍團員詢問遺跡))//_0C66)
            {
                Say(pc, 131, "混成骑士团很期待你的协助呢!$R;");
                switch (Select(pc, "怎么做呢?", "", "想去遗迹!", "什么都不是"))
                {
                    case 1:
                        mask.SetValue(INSD.西軍團員要求協助, true);
                        //_0C65 = true;
                        Say(pc, 131, "真的很感谢你能帮忙!$R;" +
                            "准备好的再跟我说话吧!$R;");
                        break;
                    case 2:
                        break;
                }
                return;
            }
            Say(pc, 131, "喂!你啊!$R;" +
                "看你从格斗场过来$R;" +
                "对自己的实力应该很有自信吧！$R;" +
                "$R想协助我们到遗迹调查吗?$R;");
            switch (Select(pc, "怎么办？", "", "不协助!", "遗迹?"))
            {
                case 1:
                    break;
                case 2:
                    mask.SetValue(INSD.西軍團員詢問遺跡, true);
                    //_0C66 = true;
                    Say(pc, 131, "是一个超大的遗跡。到现在为止$R;" +
                        "发现的遗跡中，没有一个能相比的$R;" +
                        "$R各个国家都在留意$R;" +
                        "现在这个国家的家伙们也派了调查队$R;" +
                        "$P混成骑士团这次也合作调查呢$R;" +
                        "平时互相虎视眈眈$R;" +
                        "偶尔也应该这样吧?$R;" +
                        "$R啊…不好意思$R;" +
                        "$P回到正题$R;" +
                        "怎么样?想协助调查吗?$R;");
                    switch (Select(pc, "怎么做呢?", "", "协助", "放弃"))
                    {
                        case 1:
                            mask.SetValue(INSD.西軍團員要求協助, true);
                            //_0C65 = true;
                            Say(pc, 131, "真的很感谢你能帮忙!$R;" +
                                "准备好的再跟我说话吧!$R;");
                            break;
                        case 2:
                            break;
                    }
                    break;
            }
        }
    }
}
