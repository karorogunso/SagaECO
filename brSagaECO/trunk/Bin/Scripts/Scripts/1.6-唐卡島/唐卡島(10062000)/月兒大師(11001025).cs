using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001025 : Event
    {
        public S11001025()
        {
            this.EventID = 11001025;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];

            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.开始指导) &&
                !Neko_05_cmask.Test(Neko_05.告知需寻找工匠))
            {
                Neko_05_cmask.SetValue(Neko_05.告知需寻找工匠, true);
                Say(pc, 0, 131, "是他嗎？$R;");
                Say(pc, 11001025, 131, "嗯？什麼事情呢？$R;");
                Say(pc, 0, 131, "想問一下關於飛空庭引擎消息。$R;", "行李裡的哈爾列爾利");
                Say(pc, 11001025, 131, "哇呀…$R;" +
                    "$R哦…還以為什麼呢，$R原來是活動木偶石像呀$R;" +
                    "$R是在行李裡頭吧。$R;" +
                    "$P想知道關於飛空庭的引擎是嗎？$R;" +
                    "$R嗯……$R;" +
                    "$P我認識一個精通飛空庭引擎的工匠，$R;" +
                    "$R不過他現在在奧克魯尼亞大陸喔$R;" +
                    "那是因為…自己的飛空庭發生故障時$R答應冒險者幫他們做飛空庭，$R就接受了他們的幫助。$R;" +
                    "所以他說，$R現在接到了很多飛空庭訂單，$R一時是回不來的唷。$R;");
                if (fgarden.Test(FGarden.第一次和飛空庭匠人說話))
                {
                    Say(pc, 0, 131, "啊$R;");
                    Say(pc, 0, 131, "怎麼了？$R;" +
                        "$R啊，是不是認識的人呀？$R;" +
                        "$P那麼送過去吧。$R;", "行李裡的哈爾列爾利");
                    Say(pc, 0, 131, "哎呀，怎麼辦..被發現了$R;" +
                        "$R帕斯特街道…太遠了$R;");
                    return;
                }
                Say(pc, 0, 131, "原來如此，不過都已經來到這裡了…$R;", "行李裡的哈爾列爾利");
                return;
            }
            Say(pc, 11001025, 131, "這飛空庭漂亮吧？$R;");
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.告知需寻找工匠) &&
                !Neko_05_cmask.Test(Neko_05.收到碎紙))
            {
                Say(pc, 0, 131, "　做什麼呢，『客人』$R;" +
                    "$R快去看引擎吧。$R;", "行李裡的哈爾列爾利");
                return;
            }
        }
    }
}