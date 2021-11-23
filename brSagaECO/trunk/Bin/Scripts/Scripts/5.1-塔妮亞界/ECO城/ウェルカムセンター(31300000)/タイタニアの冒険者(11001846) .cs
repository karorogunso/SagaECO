using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31300000
{
    public class S11001846 : Event
    {
        public S11001846()
        {
            this.EventID = 11001846;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "首領連敗了給大富豪$R;" +
            "已經、大赤字了哦……。$R;", "塔尼亞冒險者");

            Say(pc, 11001845, 0, "你在說甚麼？$R;" +
            "你太弱了所以$R;" +
            "不這樣做不行。$R;", "埃米爾冒險者");

            //
            /*
            Say(pc, 0, "リーダーに大富豪で連敗しちゃって$R;" +
            "もう、大赤字ですよ……。$R;", "タイタニアの冒険者");

            Say(pc, 11001845, 0, "なに言ってんの？$R;" +
            "あんたが弱すぎるから$R;" +
            "いけないんじゃないっ。$R;", "エミルの冒険者");
            */
        }


    }
}


