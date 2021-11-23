using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M20070050
{
    public class S11003682 : Event
    {
        public S11003682()
        {
            this.EventID = 11003682;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*開發中 進度
            基本資料O
            原文搬運O
            翻譯校對X
            細節修正X
            */
            Say(pc, 0, "……。$R;" +
                "で、フォーメーションに$R;" +
                "ついてなんだけど$R;" +
                "どういう感じで行く？$R;", "バーン");
            Say(pc, 11003684, 0, "では、私がアタッカ一兼、壁役を。$R;", "フォル");
            Say(pc, 11003683, 0, "回復は任せてね。$R;" +
                "$R初めての場所だから気をついて$R;" +
                "行きましょう。$R;", "クローネ");
            Say(pc, 0, "ぁぁ、危なくなつたら。$R;" +
                "憑依してくれて構わないから。$R;", "バーン");
            Say(pc, 11003681, 0, "あの、オレは？$R;", "アインス");
            Say(pc, 11003681, 0, "憑依でもしてれば？$R;", "バーン&フォル&クローネ");
            Say(pc, 11003681, 0, "！？$R;" +
                "$R酷い、一応$R;"+
                "アァイター系なのに……。$R;", "アインス");
        }
    }


}

