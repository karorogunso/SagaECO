using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M20070050
{
    public class S11003967 : Event
    {
        public S11003967()
        {
            this.EventID = 11003967;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*開發中 進度
            基本資料O
            原文搬運O
            翻譯校對X
            細節修正X
            */
            Say(pc, 0, "染色出来るものを求めて$R;" +
            "こんな場所まで来ちゃいました！$R;" +
            "$Pあのあの！$R;" +
            "$R「黒色」か「茶色」の$R;" +
          　"「ウエンぺシールド」$R;" +
            "って持ってないですか～？$R;" +
            "持ってから、染色しちゃいますよ？$R;", "好奇心旺盛な染色少女");

        }
    }


}
