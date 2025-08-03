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

            Say(pc, 11000725, 131, "想回到「阿克罗波利斯」，$R;" +
                                   "就问一下海岸可爱的「泰迪」吧!$R;" +
                                   "$R我跟他说想回去，$R;" +
                                   "他却说我还早着呢。$R;", "湖畔的蒂塔");
        }

        void 初次與湖畔的微微進行對話(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.已經與湖畔的微微進行第一次對話, true);

            Say(pc, 11000725, 131, "这座湖叫『韵丁尼湖』。$R;" +
                                   "$R韵丁尼湖的发源地，$R;" +
                                   "是个很神圣的地方喔!$R;" +
                                   "$P韵丁尼族，$R;" +
                                   "是生活在这个世界里的原住民，$R;" +
                                   "是跟我们泰达尼亚或多米尼翁$R;" +
                                   "最相近的种族哦!$R;" +
                                   "$P不过他们没有感情，$R;" +
                                   "但却拥有永恒的生命。$R;" +
                                   "$R但如果有了人间的感情，$R;" +
                                   "就会得到爱情和快乐，$R;" +
                                   "也会有死亡的悲伤。$R;", "湖畔的蒂塔");

            Say(pc, 11000725, 131, "真对不起，$R;" +
                                   "没有早点跟您打招呼。$R;" +
                                   "$P我叫「蒂塔」。$R;" +
                                   "$R是泰达尼亚第三部族的大天使。$R;" +
                                   "$P因为某种事情，$R;" +
                                   "来到了这座神秘的岛喔。$R;" +
                                   "$R真的肉体放在了原本的世界，$R;" +
                                   "没带过来这里。$R;", "湖畔的蒂塔");

            Say(pc, 11000725, 131, "哦?$R;" +
                                   "$R一定在哪里见过?$R;" +
                                   "对了! 好像在您的梦里。$R;" +
                                   "$P那时太抱歉了，$R;" +
                                   "妨碍了您的休息。$R;" +
                                   "$R您知道不知道，$R;" +
                                   "所有生命的梦，$R;" +
                                   "都是连在一起的。$R;", "湖畔的蒂塔");

            Say(pc, 11000725, 131, "想回到「阿克罗波利斯」，$R;" +
                                   "就问一下海岸可爱的「泰迪」吧!$R;" +
                                   "$R我跟他说想回去，$R;" +
                                   "他却说我还早着呢。$R;", "湖畔的蒂塔");

            Say(pc, 11000725, 131, "……!!$R;" +
                                    "哦?!$R;" +
                                    "见到「埃米尔」了吗?$R;" +
                                    "$R「埃米尔」…$R;" +
                                    "「埃米尔」过得怎么样?$R;" +
                                    "$P是吗? 那就好了。$R;" +
                                    "$P我跟「埃米尔」是一同冒险的同伴。$R;" +
                                    "$P我老是给埃米尔带来麻烦啊!$R;" +
                                    "$R「埃米尔」在「冰结的坑道」$R;" +
                                    "为了救我，挨了致命的一击。$R;" +
                                    "$R丢掉了性命…$R;" +
                                    "$P我付出了我的全部力量，$R;" +
                                    "救了「埃米尔」。$R;" +
                                    "$R但我不后悔，$R;" +
                                    "他对我来说，是非常重要的人。$R;" +
                                    "$P不过我真担心哥哥，$R;" +
                                    "哥哥不太喜欢「埃米尔」。$R;" +
                                    "$R好像有点过分了。$R;", "湖畔的蒂塔");
        }
    }
}