using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10059100
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
                Say(pc, 11001273, 131, "欢迎来到玛依玛依岛哦$R;" +
                    "您远道而来，真是辛苦了$R;" +
                    "$R我是复活战士$R;" +
                    "您冒险的时候受伤倒下的话$R;" +
                    "我会把您送到玛依玛依岛的$R;" +
                    "$P如果想去别的地方呢$R;" +
                    "就委托别的复活战士吧$R;" +
                    "$R复活战士有很多的喔$R;" +
                    "打扮像我一样就是了，很好认出来的$R;");
                maimaidao_mask.SetValue(maimaidao.店员第一次对话, true);
                //_0A97 = true;
            }
            else if (pc.Job > (PC_JOB)0 && !maimaidao_mask.Test(maimaidao.初心者对话))
            {
                Say(pc, 11001273, 131, "转职了吗？$R;" +
                    "现在是真正的冒险者了$R;" +
                    "$R不能像以前一样动不动就倒下呀$R;" +
                    "没有判断力的的冒险者得不到信赖喔$R;");
                maimaidao_mask.SetValue(maimaidao.初心者对话, true);
                //_1A48 = true;
            }
            Say(pc, 11001273, 131, "现在的储存点是$R;" +
                GetMapName(pc.SaveMap) + "!$R;");
            switch (Select(pc, "储存点设在这里吗", "", "不变更", "储存在这里"))
            {
                case 1:
                    break;
                case 2:
                    SetHomePoint(pc, 10059000, 98, 54);
                    Say(pc, 0, 131, "储存点设定为$R;" +
                        "『玛依玛依岛』了喔$R;");
                    break;
            }
        }
    }
}