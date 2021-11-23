using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10059000
{
    public class S11001273 : Event
    {
        public S11001273()
        {
            this.EventID = 11001273;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<maimaidao> maimaidao_mask = pc.CMask["maimaidao"];
            if (!maimaidao_mask.Test(maimaidao.店员第一次对话))
            {
                Say(pc, 11001273, 131, "歡迎來到瑪衣瑪衣島唷$R;" +
                    "您遠道而來，真是辛苦了$R;" +
                    "$R我是復活戰士$R;" +
                    "您冒險的時候受傷倒下的話$R;" +
                    "把您送到瑪衣瑪衣島的$R;" +
                    "$P如果想去別的地方呢$R;" +
                    "就委託別的復活戰士吧$R;" +
                    "$R復活戰士有很多的喔$R;" +
                    "打扮像我一樣就是了，很好認出來唷$R;");
                maimaidao_mask.SetValue(maimaidao.店员第一次对话, true);
                //_0A97 = true;
            }
            else if (pc.Job > (PC_JOB)0 && !maimaidao_mask.Test(maimaidao.初心者对话))
            {
                Say(pc, 11001273, 131, "轉職了嗎？$R;" +
                    "現在是真正的冒險者了$R;" +
                    "$R不能像以前一樣動不動就倒下呀$R;" +
                    "沒有判斷力的的冒險者得不到信賴喔$R;");
                maimaidao_mask.SetValue(maimaidao.初心者对话, true);
                //_1A48 = true;
            }
            Say(pc, 11001273, 131, "現在的儲存點是$R;" +
                GetMapName(pc.SaveMap) + "!$R;");
            switch (Select(pc, "儲存點設在這裡嗎", "", "不變更", "儲存在這裡"))
            {
                case 1:
                    break;
                case 2:
                    SetHomePoint(pc, 10059000, 98, 54);
                    Say(pc, 0, 131, "儲存點設定為$R;" +
                        "『瑪衣瑪衣島』了喔$R;");
                    break;
            }
        }
    }
}