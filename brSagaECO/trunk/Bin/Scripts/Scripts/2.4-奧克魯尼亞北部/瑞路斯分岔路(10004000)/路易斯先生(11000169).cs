using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10004000
{
    public class S11000169 : Event
    {
        public S11000169()
        {
            this.EventID = 11000169;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<RLSFCLFlags> mask = new BitMask<RLSFCLFlags>(pc.CMask["RLSFCL"]);
            if (!mask.Test(RLSFCLFlags.路易斯先生第一次對話))
            {
                Say(pc, 131, "我的『針織鴨舌帽』帥氣吧？$R;" +
                    "合成製作的!$R;" +
                    "$R你想不想挑戰看看?$R;");
                switch (Select(pc, "要挑戰嗎?", "", "好啊!挑戰!", "煩……"))
                {
                    case 1:
                        Say(pc, 131, "想要製作編織帽的話$R;" +
                            "就要找『針織布』$R;" +
                            "$P前面的『寶拉熊』就會掉的$R;" +
                            "$R得到了告訴我吧$R;");
                        mask.SetValue(RLSFCLFlags.路易斯先生第一次對話, true);
                        break;
                    case 2:
                        break;
                }
                return;
            }
            if (!mask.Test(RLSFCLFlags.針織鴨舌帽完成))
            {
                if (CountItem(pc, 50022900) >= 1)
                {
                    Say(pc, 131, "完成了!!挺合適阿?$R;" +
                        "$R製作其他道具看看嗎?$R;" +
                        "可以教你各種方法喔$R;");
                    mask.SetValue(RLSFCLFlags.針織鴨舌帽完成, true);
                    return;
                }
                if (CountItem(pc, 10000407) >= 1 && CountItem(pc, 10045900) >= 1)
                {
                    Say(pc, 131, "材料都收集好了!$R;" +
                        "那就去裁縫師阿姨家吧$R;" +
                        "$R在阿高普路斯下城裡$R;" +
                        "$P選擇『拜託裁縫』的話$R;" +
                        "阿姨會馬上幫忙製作的$R;" +
                        "$R完成的話也會給我看吧?$R;");
                    return;
                }
                if (CountItem(pc, 10045900) >= 1)
                {
                    Say(pc, 131, "弄到『編織布』的話$R;" +
                        "然後再找『神風香水』$R;" +
                        "$R雖然也可以合成製作$R;" +
                        "但是在商裡直接購買的話方便又快速$R;" +
                        "$P阿高普路斯的下城裡應該有賣的$R;" +
                        "$R要的話，跟我說吧$R;");

                    return;
                }
                Say(pc, 131, "想要製作編織帽的話$R;" +
                    "就要找『針織布』$R;" +
                    "$P前面的『寶拉熊』就會掉的$R;" +
                    "$R得到了告訴我吧$R;");
                return;
            }
            Say(pc, 131, "我帥氣吧？$R;" +
                "$R什麽都可以問我!$R;");
            switch (Select(pc, "想問什麽呢?", "", "關於武器", "關於帽子", "關於眼鏡", "關於衣服", "關於襪子", "關於鞋", "沒什麽可問的"))
            {
                case 1:
                    Say(pc, 131, "啊？這個？是『貴族棒子』$R;" +
                        "象徵魔力的『金光藍寶珠』$R;" +
                        "是不是很適合我啊…$R;" +
                        "$P製作這個杖的方法?$R;" +
                        "不能那麽輕易的教你$R;" +
                        "$R光看是免費的，你覺得用什麽做的?$R;");
                    break;
                case 2:
                    Say(pc, 131, "哦？這是？　是『針織鴨舌帽』喔$R;" +
                        "$R再往前走，那裡會到更寒泠的$R;" +
                        "我擔心會得感冒，所以…$R;" +
                        "$P雖然說北域的商人有賣$R;");
                    break;
                case 3:
                    Say(pc, 131, "啊?這個?是『圓框眼鏡』$R;" +
                        "$R雖然不好意思，但講講這個眼鏡的$R;" +
                        "好處吧!$R;" +
                        "$P這個是用在臉上的飾品$R;" +
                        "$R不會讓眼睛變好，也不會提高防禦力$R;" +
                        "什麽功能都沒有!$R;" +
                        "$P雖然是那樣，可不是很帥嗎?$R;" +
                        "$R來，看看我吧!$R;" +
                        "$P製作圓框眼鏡的方法?$R;" +
                        "$R這個眼鏡是用鈦金屬製的!$R;" +
                        "把這個那個這裡加工一下$R;" +
                        "還有的，是秘密!$R;");
                    break;
                case 4:
                    Say(pc, 131, "啊?這個?是『厚的外套』$R;" +
                        "$R再往前走，那裡會到更寒泠的$R;" +
                        "我擔心會得感冒，所以…$R;" +
                        "$P雖然說北域的商人有賣$R;");
                    break;
                case 5:
                    Say(pc, 131, "編織緊身衣…非常暖和!$R;" +
                        "北邊的商人在賣呢$R;");
                    break;
                case 6:
                    Say(pc, 131, "啊?這個?是『工作用靴子』$R;" +
                        "$R在軍艦島上買的$R;" +
                        "$P問一些更能炫耀的就好了!$R;");
                    break;
                case 7:
                    break;
            }
        }
    }
}
