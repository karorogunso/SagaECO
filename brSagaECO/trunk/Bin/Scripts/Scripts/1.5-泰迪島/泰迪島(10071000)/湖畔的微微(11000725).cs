using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:湖畔的微微(11000725) X:71 Y:141
namespace SagaScript.M10071000
{
    public class S11000725 : Event
    {
        public S11000725()
        {
            this.EventID = 11000725;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            if (!Tinyis_Land_01_mask.Test(Tinyis_Land_01.已經與湖畔的微微進行第一次對話))
            {
                初次與湖畔的微微進行對話(pc);
                return;
            }

            Say(pc, 11000725, 131, "想回到「阿高普路斯市」，$R;" +
                                   "就問一下海岸可愛的「泰迪」吧!$R;" +
                                   "$R我跟他說好，$R;" +
                                   "他說我還早著呢。$R;", "湖畔的微微");
        }

        void 初次與湖畔的微微進行對話(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.已經與湖畔的微微進行第一次對話, true);

            Say(pc, 11000725, 131, "這座湖叫『韻丁尼湖』。$R;" +
                                   "$R韻丁尼湖的發源地，$R;" +
                                   "是個很神聖的地方喔!$R;" +
                                   "$P韻丁尼族，$R;" +
                                   "是生活在這個世界裡的原住民，$R;" +
                                   "是跟我們塔妮亞或道米尼$R;" +
                                   "最相近的種族唷!$R;" +
                                   "$P不過他們沒有感情，$R;" +
                                   "但卻擁有永恆的生命。$R;" +
                                   "$R但如果有了人間的感情，$R;" +
                                   "就會得到愛情和快樂，$R;" +
                                   "也會有死亡的悲傷。$R;", "湖畔的微微");

            Say(pc, 11000725, 131, "真對不起，$R;" +
                                   "沒有早點跟您打招呼。$R;" +
                                   "$P我叫「微微」。$R;" +
                                   "$R是塔妮亞第三部族的大天使。$R;" +
                                   "$P因為某種事情，$R;" +
                                   "來到了這座神秘的島喔。$R;" +
                                   "$R真的肉體放在了原本的世界，$R;" +
                                   "沒帶過來這裡。$R;", "湖畔的微微");

            Say(pc, 11000725, 131, "哦?$R;" +
                                   "$R一定在哪裡見過?$R;" +
                                   "對了! 好像在您的夢裡。$R;" +
                                   "$P那時太抱歉了，$R;" +
                                   "妨礙了您的休息。$R;" +
                                   "$R您知道不知道，$R;" +
                                   "所有生命的夢，$R;" +
                                   "都是連在一起的。$R;", "湖畔的微微");

            Say(pc, 11000725, 131, "想回到「阿高普路斯市」，$R;" +
                                   "就問一下海岸可愛的「泰迪」吧!$R;" +
                                   "$R我跟他說好，$R;" +
                                   "他說我還早著呢。$R;", "湖畔的微微");

            Say(pc, 11000725, 131, "……!!$R;" +
                                    "哦?!$R;" +
                                    "見到「埃米爾」了嗎?$R;" +
                                    "$R「埃米爾」…$R;" +
                                    "「埃米爾」過得怎麼樣?$R;" +
                                    "$P是嗎? 那就好了。$R;" +
                                    "$P我跟「埃米爾」是一同冒險的同伴。$R;" +
                                    "$P我老是給埃米爾帶來麻煩啊!$R;" +
                                    "$R「埃米爾」在「冰結的坑道」$R;" +
                                    "為了救我，挨了死亡的一刀。$R;" +
                                    "$R丟掉了性命…$R;" +
                                    "$P我付出了我的全部以及生命，$R;" +
                                    "救了「埃米爾」。$R;" +
                                    "$R但我不後悔，$R;" +
                                    "他對我來說，是非常重要的人。$R;" +
                                    "$P不過我真擔心哥哥，$R;" +
                                    "哥哥不太喜歡「埃米爾」。$R;" +
                                    "$R好像有點過分了。$R;", "湖畔的微微");
        }
    }
}