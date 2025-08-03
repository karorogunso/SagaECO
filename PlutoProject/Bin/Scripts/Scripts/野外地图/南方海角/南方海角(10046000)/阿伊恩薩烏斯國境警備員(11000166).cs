using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10046000
{
    public class S11000166 : Event
    {
        public S11000166()
        {
            this.EventID = 11000166;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "欢迎来到艾恩萨乌斯联邦!$R;" +
                "$R要进艾恩萨乌斯联邦的话$R;" +
                "需要加入属于阿克罗尼亚$R;" +
                "的混成骑士团的『南军』$R;" +
                "或$R需要$R;" +
                "『艾恩萨乌斯入国许可证』$R;");
        }
        void 騎士團(ActorPC pc)
        {
            Say(pc, 131, "隶属于阿克罗尼亚混成骑士团$R;" +
                "『南军』吗?$R;" +
                "$R请在里面办理一下入境手续$R;");
        }
        void 虎眼任務(ActorPC pc)
        {
            Say(pc, 131, "哎呀…!$R;" +
            "在担心是不是在途中踫到$R;" +
            "「破坏ＲＸ１」或$R;" +
            "「绯红巴乌」了呢$R;" +
            "$P原来是喜欢跟随人$R;" +
            "受到喜爱的小魔物$R;" +
            "$R但是现在成了袭击$R;" +
            "人的可怕的存在$R;" +
            "$P这变化可能是天上给人们的$R;" +
            "惩罚也不一定$R;" +
            "$R但是我相信$R;" +
            "总有一天又会$R;" +
            "重新可以生活在一起的$R;" +
            "$P我能讲得故事就到此为止了$R;");

            Say(pc, 131, "那现在往法伊斯特去看看吧$R;" +
                "在国境附近桥前面的人$R;" +
                "会跟你讲的$R;");
        }
    }
}
