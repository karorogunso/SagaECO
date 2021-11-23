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
        }
    }
}


