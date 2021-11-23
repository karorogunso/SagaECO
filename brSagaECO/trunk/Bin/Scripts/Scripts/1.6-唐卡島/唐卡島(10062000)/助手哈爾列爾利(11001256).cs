using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001256 : Event
    {
        public S11001256()
        {
            this.EventID = 11001256;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (pc.Level > 29 && pc.Fame > 9 && _Xb31)
            {
                if (_Xb41 && _Xb42 && _Xb43 && _2b38 && _2b39 && _2b40)
                {
                    Say(pc, 131, "謝謝您，$R;" +
                        "把道具全都拿來了。$R;" +
                        "$R幫了我很大的忙唷。$R;");
                    return;
                }
                if (_Xb41 && !_2b38)
                {
                    _2b38 = true;
                    Say(pc, 131, "謝謝您給我拿來了『飛空庭甲板』$R;" +
                        "還拿來了悔過書呢，$R;" +
                        "$R為了報答您，給您一個部件吧。$R;" +
                        "其實對我來說，對方肯反省，$R就已經足夠了$R;");
                    return;
                }
                if (_Xb42 && !_2b39)
                {
                    _2b39 = true;
                    Say(pc, 131, "謝謝您給我拿來了『渦輪引擎』$R;" +
                        "還拿來了悔過書呢，$R;" +
                        "$R為了報答您，給您一個部件吧。$R;" +
                        "其實對我來說，對方肯反省，$R就已經足夠了$R;");
                    return;
                }
                if (_Xb43 && !_2b40)
                {
                    _2b40 = true;
                    Say(pc, 131, "謝謝您給我拿來了『汽笛』$R;" +
                        "還拿來了悔過書呢，$R;" +
                        "$R為了報答您，給您一個部件吧。$R;" +
                        "其實對我來說，對方肯反省，$R就已經足夠了$R;");
                    return;
                }
                Say(pc, 131, "正在這裡製作飛行帆的部件。$R;" +
                    "$R很不好意思，飛行帆部件被偷了。$R;" +
                    "$R如果有人能找回來就好了。$R;");
                switch (Select(pc, "怎麼辦呢？", "", "幫忙", "不幫忙"))
                {
                    case 1:
                        Say(pc, 131, "您真是一個好人$R;" +
                            "$R被偷的部件有$R;" +
                            "『汽笛』$R;" +
                            "『飛空庭甲板』$R;" +
                            "『渦輪引擎』3個。$R;");
                        int eday = DateTime.Now.DayOfYear;
                        int a = ((eday + 6) / 7) % 3;
                        switch (a)
                        {
                            case 0:
                                if (_Xb41)
                                {
                                    Say(pc, 131, "這個星期只學會了$R;" +
                                        "有關『飛空庭甲板』$R;" +
                                        "的東西嗎？$R;");
                                }
                                else
                                {
                                    _2b35 = true;
                                    Say(pc, 131, "話說回來……$R;" +
                                        "$R『飛空庭甲板』旁邊$R;" +
                                        "掉了很多奇怪的棉花$R;" +
                                        "$P還看到了戴著紅帽子$R跟藍帽子的縫製玩偶。$R;" +
                                        "$R他…不是犯人嗎？$R;");
                                }
                                break;
                            case 1:
                                if (_Xb42)
                                {
                                    Say(pc, 131, "這個星期只學會了$R;" +
                                        "有關『渦輪引擎』$R;" +
                                        "的東西嗎？$R;");
                                }
                                else
                                {
                                    _2b36 = true;
                                    Say(pc, 131, "話說回來……$R;" +
                                        "$R『渦輪引擎』旁邊印著很多泥腳印$R;" +
                                        "那個人是不是從泥濘的地方來的呢？$R;" +
                                        "$P還有，$R看到了穿著褐色衣服健康的叔叔…$R;" +
                                        "$R他是不是犯人呢？$R;");
                                }
                                break;
                            case 2:
                                if (_Xb43)
                                {
                                    Say(pc, 131, "這個星期學會了關於『汽笛』的資料$R;");
                                }
                                else
                                {
                                    _2b37 = true;
                                    Say(pc, 131, "話說回來……$R;" +
                                        "$R『汽笛』旁邊$R有一頂骸骨圖案的小帽子。$R;" +
                                        "$P還有看到了帶著護膝的熊玩偶。$R;" +
                                        "$R會不會他是犯人呢？$R;");
                                }
                                break;
                        }
                        Say(pc, 131, "關於別的道具，還在調查中$R;" +
                            "$R唐卡的管理工作速度本來就慢。$R;" +
                            "$R調查需要1個星期，耐心等待吧。$R;");
                        break;
                    case 2:
                        break;
                }
                return;
            }
            */
            Say(pc, 131, "正在這裡製作飛行帆的部件。$R;");

        }
    }
}