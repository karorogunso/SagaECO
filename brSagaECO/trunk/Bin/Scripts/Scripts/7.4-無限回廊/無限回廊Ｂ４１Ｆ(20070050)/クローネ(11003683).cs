using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M20070050
{
    public class S11003683 : Event
    {
        public S11003683()
        {
            this.EventID = 11003683;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*開發中 進度
            基本資料O
            原文搬運O
            翻譯校對X
            細節修正X
            */
            Say(pc, 0, "聞いた？$R;" +
                "ここから先には$R;" +
                "見たこともない様な$R;"+
                "モンスターが出てくるって。$R;", "クローネ");
            Say(pc, 11003684, 0, "そうみたいですね。$R;" +
                "中々興味をそそります。$R;", "フォル");
            Say(pc, 11003682, 0, "修練の場？って$R;"+
                "言っておきながら$R;" +
                "中はかなり危険な仕組みに$R;" +
                "なっているらしいぞ。$R;", "バーン");
            Say(pc, 11003681, 0, "上級者用ってことか！？$R;"+
                "$Rいいじゃん、大いに結構。$R;" +
                "壁は高いほうが$R;" +
                "登りがいがあるってもんだ$R;", "アインス");
            Say(pc, 0, "確かにね、そんだけ$R;" +
                "危険だって言うなら$R;" +
                "突破した後の報酬も$R;" +
                "さぞ、良い物なんでしょね。$R;", "クローネ");
            Say(pc, 11003684, 0, "どんなお宝が手に入るか$R;" +
                "楽しみですね。$R;", "フォル");
            Say(pc, 11003681, 0, "善は急げだ！$R;" +
                "$Rいいかオレに付いて来い！$R;", "アインス");

        }
    }


}

