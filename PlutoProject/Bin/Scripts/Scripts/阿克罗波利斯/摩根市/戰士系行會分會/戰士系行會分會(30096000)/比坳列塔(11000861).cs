using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30096000
{
    public class S11000861 : Event
    {
        public S11000861()
        {
            this.EventID = 11000861;

        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (_6a69)
            {
                Say(pc, 131, "沒想到搶到我前面去了…$R;" +
                    "實力不錯呢！$R;");
                return;
            }
            if (CountItem(pc, 100200021) > 0)
            {
                Say(pc, 131, "哦！這不是證據文件嗎？$R;" +
                    "$R實力不錯嘛！$R;" +
                    "現在趕緊通知芝奇吧$R;");
                return;
            }
            if (_6a73)
            {
                Say(pc, 131, "逃到光之塔？$R;" +
                    "$R逃到叫人頭疼的地方了…$R;" +
                    "那裡有很多危險的傢伙阿$R;" +
                    "$P以現在的成員人數$R;" +
                    "$R根本不夠，沒辦法了$R;" +
                    "只能叫戰鬥部隊了$R;" +
                    "$P不過沒有時間呀…$R;" +
                    "$P嗯…怎麼辦呢…$R;");
                return;
            }
            if (_6a72)
            {
                Say(pc, 131, "現在還在這裡呢？$R;" +
                    "調查員在迪澳曼特煉制所$R;" +
                    "$R趕快去看看吧！$R;");
                return;
            }
            if (_6a71)
            {
                Say(pc, 131, "什麼事情？$R;" +
                    "現在很忙，下次再來吧$R;");
                switch (Select(pc, "怎麼辦呢？", "", "知道了", "受芝奇委託而來的。"))
                {
                    case 1:
                        break;
                    case 2:
                        _6a72 = true;
                        Say(pc, 131, "芝奇之托？$R;" +
                            "那麼是為了那件事情嗎？$R;" +
                            "$R芝奇現在在哪兒呢？$R;" +
                            "$P正在取材？$R;" +
                            "重要的事情委託給別人，$R;" +
                            "他自己在幹什麼呢？$R;" +
                            "$R那傢伙派來的一定信得過$R;" +
                            "但是…$R;" +
                            "$P至於我委託潛入調查這件事情…$R;" +
                            "調查員現在還沒有來呀$R;" +
                            "$R昨天接到消息說，$R;" +
                            "取得了迪澳曼特和恩德議員的$R;" +
                            "決定性證據…$R;" +
                            "$P抱歉，$R;" +
                            "您能不能做一下調查呢？$R;" +
                            "$R調查員是偽裝成$R;" +
                            "迪澳曼特煉制所工作人員的$R;" +
                            "$R銀髮道米尼呀。$R;");
                        break;
                }
                return;
            }
            */
            Say(pc, 131, "埃米尔最近怎么不来了$R;" +
                "$P听说在光之塔上$R;" +
                "发现了稀奇的东西阿$R;" +
                "是不是为了调查此事$R;" +
                "去了唐卡呢？$R;" +
                "$P哎…$R;" +
                "原以为是好机会呀$R;" +
                "$R虽然遗憾，但没办法呀…$R;");
        }
    }
}