using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M22197000
{
    public class S11002027 : Event
    {
        public S11002027()
        {
            this.EventID = 11002027;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "う～ん。$R;" +
            "このアルゴリズムの解析ができれば$R;" +
            "恐らくいくつかのシステムを$R;" +
            "制御下におけると思うんだけど……。$R;", "レジスタンス研究員Ａ");

            Say(pc, 11002028, 131, "これはなかなか興味深いですね$R;" +
            "$Pココがこうなっているという事は$R;" +
            "この式をソコに代入して……。$R;" +
            "$Pむぅ、ダメみたいですね。$R;", "レジスタンス研究員Ｂ");

            Say(pc, 131, "あ！ちょっとまって！$R;" +
            "その式をコウ変形して$R;" +
            "この法則を当てはめれば……。$R;", "レジスタンス研究員Ａ");

            Say(pc, 11002028, 131, "ほぅ、コレは驚きました。$R;" +
            "なかなかやるじゃないですか。$R;" +
            "$Pしかし、そうなると$R;" +
            "ココが新たな問題になりますね。$R;", "レジスタンス研究員Ｂ");

            Say(pc, 0, 131, "……。$R;" +
            "$P（邪魔しないほうがよさそうだ）$R;", " ");

        }
    }
}


        
   


