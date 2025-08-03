using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:(11001893) X:20 Y:153
namespace SagaScript.M10071000
{
    public class S11001893 : Event
    {
        public S11001893()
        {
            this.EventID = 11001893;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ゴクゴクゴク……。$R;" +
            "（泉の水を飲んでいる）$R;", "ムーンペガサス");

            Say(pc, 11000725, 131, "その子はムーンペガサス。$R;" +
            "$R癒しの力を得た$R;" +
            "珍しいペガサスですの。$R;" +
            "$Rたまに、この湖の水を$R;" +
            "飲みに来るのをみかけます。$R;" +
            "$Pこの湖は「アンディーンの湖」$R;" +
            "この世界の先住民族$R;" +
            "アンディーンの民の生まれ出ずる$R;" +
            "神聖な場所ですの。$R;" +
            "$R何か、不思議な力が$R;" +
            "あるのかもしれませんわね。$R;", "湖畔のティタ");

        }
    }
}