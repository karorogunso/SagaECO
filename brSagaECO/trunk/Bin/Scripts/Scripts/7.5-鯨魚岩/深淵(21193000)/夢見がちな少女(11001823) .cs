using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001823 : Event
    {
        public S11001823()
        {
            this.EventID = 11001823;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "往這個噴泉投入錢幣$R;" +
            "願望成真了。$R;" +
            "$P我也跟著做了一次看看、$R;" +
            "可是在決定許下的願前$R;" +
            "已投下了錢幣呢。$R;" +
            "$R唔、有甚麼可以做？$R;" +
            "$P…但是這樣的我、$R;" +
            "願望也許會成真！$R;" +
            "這被叫作期待感的感覺真好呢～$R;" +
            "$P就喜愛腦袋的期待$R;" +
            "不斷膨脹這樣的說。$R;" +
            "$R雖然實際上願望並沒有成真呢。$R;", "夢見がちな少女");

            //
            /*
            Say(pc, 0, "この噴水に小銭を投げ込むと$R;" +
            "願いが叶うんだって。$R;" +
            "$P私もやってみたんだけど、$R;" +
            "願い事を決める前に$R;" +
            "小銭を投げ込んじゃったんだよね。$R;" +
            "$Rうーん、何にしよ？$R;" +
            "$P…でもこういうのって、$R;" +
            "願いが叶うかも！$R;" +
            "っていう期待感がいいんだよね～$R;" +
            "$Pそうやって頭の中で期待を$R;" +
            "膨らませてるのが好きなの。$R;" +
            "$R実際に叶うに越したことは無いけどね。$R;", "夢見がちな少女");
            */
        }
    }
}


