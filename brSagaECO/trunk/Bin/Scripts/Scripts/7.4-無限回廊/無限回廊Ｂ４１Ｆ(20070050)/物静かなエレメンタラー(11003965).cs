using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M20070050
{
    public class S11003965 : Event
    {
        public S11003965()
        {
            this.EventID = 11003965;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*開發中 進度
            基本資料O
            原文搬運O
            翻譯校對X
            細節修正X
            */
            Say(pc, 0, "無限回廊中層に$R;" +
            "光り輝くブルルが居たのよ。$R;" +
            "$Pそのプルルが宝箱を落としたから$R;" +
            "知り合いのヒンジャーに開錠を$R;" +
            "お願いしたんだけど$R;" +
            "あけられなくてね。$R;" +
            "$Pでもきつき、$R;"+
            "そこにいる彼女が$R;"+
            "あけてくれたの。$R;"+
            "$R……力ずくで。" +
            "あれが噂に聞く「脳筋」と$R;" +
            "呼ばれる人に違いないわ。$R;" +
            "$Pし、失礼だなぁ！$R;" +
            "$Pあ、あたし脳筋じゃないからね？$R;" +
            "信じちゃダメだよ？$R;", "物静かなエレメンタラー");

        }
    }


}

