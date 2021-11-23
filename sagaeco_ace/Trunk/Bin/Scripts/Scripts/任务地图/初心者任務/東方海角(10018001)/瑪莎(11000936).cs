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
            Say(pc, 11000936, 131, "您好，我叫玛莎。$R;" +
                                   "$R受埃米尔的委托，$R;" +
                                   "负责传授初心者一些知识。$R;" +
                                   "$P现在介紹「飞空庭」吧!$R;" +
                                   "$P准备好了，$R;" +
                                   "就点击我旁边的「飞空庭绳子」把。$R;" +
                                   "$R现在让我介绍「飞空庭」吧!$R;", "玛莎");
        }
    }
}
