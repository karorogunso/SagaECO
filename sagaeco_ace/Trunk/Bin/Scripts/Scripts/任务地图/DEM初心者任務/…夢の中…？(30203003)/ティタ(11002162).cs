using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30203003
{
    public class S11002162 : Event
    {
        public S11002162()
        {
            this.EventID = 11002162;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, pc.Name + "…？$R;" +
            "第一次见面吗？;" +
            "您好！$R;", "蒂塔");

            Say(pc, 132, "您好，我叫蒂塔。$R;" +
            "我是泰达尼亚第3氏族的大天使。$R;" +
            "$R很荣幸见到你。$R;" +
            "$P我作为你梦里的人...我必须告诉你知$R;" +
            "现在已经醒了！$R;" +
            "$P不要太担心...$R;" +
            "$P因为还有世界...$R;" +
            "是你的朋友......$R;", "蒂塔");
            Wait(pc, 990);

            pc.CInt["Beginner_Map"] = CreateMapInstance(50080000, 10023100, 250, 132);

            Warp(pc, (uint)pc.CInt["Beginner_Map"], 26, 25);
        }
    }
}