using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:東方海角(10018001) NPC基本信息:瑪莎(11000936) X:33 Y:49
namespace SagaScript.M10018001
{
    public class S11000936 : Event
    {
        public S11000936()
        {
            this.EventID = 11000936;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000936, 131, "您好，我叫瑪莎。$R;" +
                                   "$R受埃米爾的委託，$R;" +
                                   "負責傳授初心者一些知識。$R;" +
                                   "$P現在介紹「飛空庭」吧!$R;" +
                                   "$P準備好了，$R;" +
                                   "就點擊我旁邊的「飛空庭繩子」喔。$R;" +
                                   "$R現在讓我介紹「飛空庭」吧!$R;", "瑪莎");
        }
    }
}
