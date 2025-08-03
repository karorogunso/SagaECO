using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10067000
{
    public class S10000645 : Event
    {
        public S10000645()

        {
            this.EventID = 10000645
;
        }

        public override void OnEvent(ActorPC pc)
        {   
            //万圣节部分
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (wsj_mask.Test(wsj.开始))
            {
                Say(pc, 0, 0, "……。$R;" +
                "我が名はヴェルデガルド$R;" +
                "$Rノーザン王朝を統べる女王。$R;" +
                "$P……。$R;" +
                "$P……あなたの友達は、まだあの部屋に$R;" +
                "閉じ込められています。$R;" +
                "$Rさあ、あなたが選ぶ道は２つ。$R;" +
                "友を助けるか、見捨てるか。$R;" +
                "どちらを選んでも、かまいません。$R;", "");
                if (Select(pc, "好きなほうを選びなさい", "", "友を助ける", "友を見捨てる") == 1)
                {
                    Say(pc, 0, 0, "魔法ギルド総本山に向かいなさい。$R;" +
                    "そこで、きっと何か$R;" +
                    "手がかりをつかむことでしょう。$R;" +
                    "$R魔法ギルド総本山は蒼と金の色で$R;" +
                    "彩られた装飾豊かな建物です。$R;" +
                    "$P道に迷ったり、どうしていいか$R;" +
                    "わからないときは僧兵に聞きなさい。$R;" +
                    "$R彼らに案内させましょう……。$R;", "");
                    wsj_mask.SetValue(wsj.女王, true);
                    wsj_mask.SetValue(wsj.开始, false);
                    Warp(pc, 10065000, 52, 21);
                    
                    return;
                }
                Say(pc, 0, 0, "すべてを無にかえしましょう。$R;" +
                "あなたは、夢を見ていたのです。$R;", "");
                wsj_mask.SetValue(wsj.开始, false);
                Warp(pc, 10023000, 131, 139);
                return;
            }
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);
            //EVT11000538
            /*
            if (_Xa10 && !_Xa11)
            {
                if (!_0b18)
                {
                    Say(pc, 131, "…$R;" +
                        "我的名字叫维尔迪加尔德$R;" +
                        "$R是诺森女王$R;" +
                        "$P…$R;" +
                        "$P您的朋友$R;" +
                        "还被关在这间房子里啊$R;" +
                        "$P现在您只有两个选择$R;" +
                        "救朋友或是装作不知道$R;" +
                        "选择哪个，是您的自由$R;");
                    switch (Select(pc, "选择哪个", "", "救朋友", "装做不知道"))
                    {
                        case 1:
                            _0b18 = true;
                            Say(pc, 131, "您去魔法行会总部吧$R;" +
                                "在那里会找到头绪的$R;" +
                                "$R魔法行会总部是$R;" +
                                "蓝色和金黄色风格的华丽建筑，$R;" +
                                "不明白的时候$R;" +
                                "$R问武僧吧$R;" +
                                "$R已经跟他们说好了，会为您引路的$R;");
                            //#ノーザン宮殿ホールへ
                            Warp(pc, 10065000, 50, 21);
                            //WARP  629
                            break;
                        case 2:
                            Say(pc, 131, //#ハロウィン終了
                            "就当做没发生过好吗$R;" +
                            "只当做一场您做的梦吧$R;");
                            _Xa10 = false;
                            //#アクロポリスへ
                            Warp(pc, 10023000, 130, 139);
                            //WARP  201
                            break;
                    }
                    return;
                }
                if (!_0b19)
                {
                    Say(pc, 131, "不明白的时候$R;" +
                        "问武僧吧$R;" +
                        "$R已经跟他们说好了，会为您引路的$R;");
                    return;
                }
            }
            */
            if (!mask.Test(NDFlags.貝爾德咖魯德第一次对话))
            {
                mask.SetValue(NDFlags.貝爾德咖魯德第一次对话, true);
                //_2A45 = true;
                SkillPointBonus(pc, 1);
                Say(pc, 131, "…$R;" +
                    "大陆来的冒险者，路上辛苦了$R;" +
                    "$P我的名字叫维尔迪加尔德$R;" +
                    "$R是诺森女王$R;" +
                    "$P…很惊讶是吗？$R您看到是我的幻象全息图$R;" +
                    "$P在您的脚下能看到塔内$R流出的光芒吧？$R;" +
                    "$R这就是这王国的真正首都诺森市$R;" +
                    "$P很遗憾，您能进入的地方只能到这儿$R;" +
                    "$R从这里开始是$R我们诺森市民的神圣领地，$R别国人是看不见的$R;");
                Wait(pc, 2000);
                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 2000);
                Say(pc, 131, "技能点数上升了1$R;");
                Say(pc, 131, "唤起了您潜在的力量$R;" +
                    "$P感谢您通过冰雪到我们这来，$R这是给您的小小礼物哦。$R;" +
                    "$P您只要经过磨练后，$R就会发现潜在的力量，$R通往神圣领域的门就会开启的$R;" +
                    "$R现在回去吧$R;");
                return;
            }
            //#うさぎクエストへ
            if (pc.Level > 59 && !mask.Test(NDFlags.职业装任务))
            {
                Say(pc, 131, "…$R;" +
                    "$R我的名字叫维尔迪加尔德$R;" +
                    "是诺森女王$R;" +
                    "$P冒险者！$R;" +
                    "充满力量的冒险者呀！$R;" +
                    "$R想不想为我做事呢？$R;" +
                    "$P只要发誓为我做事，$R;" +
                    "我会给您王宫的宝物$R;");
                switch (Select(pc, "为女王做事吗？", "", "够了", "为了女王做事"))
                {
                    case 1:
                        Say(pc, 131, "…$R;" +
                            "$R那么您没有理由待在这里了$R;" +
                            "回去吧$R;");
                        break;
                    case 2:
                        mask.SetValue(NDFlags.职业装任务, true);
                        // _0c53 = true;
                        Say(pc, 131, "真是有勇气的冒险者呀$R;" +
                            "听到这句话太高兴了$R;" +
                            "$R先把这个收下吧$R;");
                        GiveItem(pc, 10022600, 1);
                        Say(pc, 131, "拿到了『女王的钥匙』$R;");
                        Say(pc, 131, "这是『宝物仓库』的钥匙$R;" +
                            "拿这个钥匙打开您喜欢的箱子吧$R;" +
                            "$R这钥匙是对您承诺的报酬$R;" +
                            "$P每次您为我做事$R;" +
                            "我会给您一把钥匙哦$R;" +
                            "$P委托勤卫兵保管了$R;" +
                            "$R详细的去问宫廷大堂的女卫兵吧$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "…$R;");
        }
    }
}