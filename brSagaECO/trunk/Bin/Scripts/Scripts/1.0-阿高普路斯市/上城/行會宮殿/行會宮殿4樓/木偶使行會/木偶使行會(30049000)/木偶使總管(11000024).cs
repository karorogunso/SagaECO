using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:木偶使行會(30049000) NPC基本信息:木偶使總管(11000024) X:3 Y:3
namespace SagaScript.M30049000
{
    public class S11000024 : Event
    {
        public S11000024()
        {
            this.EventID = 11000024;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Puppetry> Puppetry_amask = pc.AMask["Puppetry"];
            BitMask<Puppetry> Puppetry_cmask = pc.CMask["Puppetry"];

            int sel1 = 0, sel2 = 0;

            Say(pc, 131, "歡迎光臨木偶術師行會$R;");
            if (Puppetry_cmask.Test(Puppetry.第一次对话))
            {
                do
                {
                    sel2 = Select(pc, "做什麼呢??", "", "買東西", "製作石像", "問石像的使用方法", "問關於石像出售的問題", "製造魔物模型", "關於魔物模型", "什麼也不做");
                    switch (sel2)
                    {
                        case 1:
                            OpenShopBuy(pc, 66);
                            break;
                        case 2:
                            Synthese(pc, 2038, 1);
                            break;
                        case 3:
                            石像介绍(pc);
                            break;
                        case 4:
                            Say(pc, 131, "您知道通信出售是什麼嗎？$R;" +
                                "$R就是您看完目錄以後$R在原地預定的話，$R;" +
                                "$R在2-3天内商品就會到達的$R;" +
                                "非常便利的東西。$R;" +
                                "$P石像通信出售是更加便利的$R;" +
                                "$R如果看目錄以後選購的話$R就可以直接得到商品了。$R;" +
                                "是不是很便利呢？$R;");
                            do
                            {
                                sel1 = Select(pc, "怎麼辦呢?", "", "使用石像目錄。", "看目錄", "在目錄上登記。", "回去");
                                switch (sel1)
                                {
                                    case 1:
                                        GiveItem(pc, 10029600, 1);
                                        Say(pc, 131, "在目錄上登記的話需要500金幣。$R;" +
                                            "$R如果有錢的話可以試一下。$R;" +
                                            "得到石像目錄。$R;");
                                        break;
                                    case 2:
                                        Say(pc, 131, "看目錄的方法就更簡單了。$R;" +
                                            "$R雙撃『石像目錄』就可以了！$R;" +
                                            "$P但是有一點要注意。$R;" +
                                            "在這一張目錄上標示的$R只是這張目錄的內容。$R;" +
                                            "$R並不會顯示別的目錄內容的。$R;");
                                        break;
                                    case 3:
                                        Say(pc, 131, "想登錄商品到目錄上的話，$R就在店面設定時$R;" +
                                            "在登錄與否的選項，點撃就可以了$R;" +
                                            "$R這樣店就會自動登錄到目錄上。$R;" +
                                            "$P但是每登錄一次$R就要花『500金幣』。$R;" +
                                            "$R想要增加銷售額，$R是不是應該試一試呢。$R;");
                                        break;
                                    case 4:
                                        break;
                                }
                            } while (sel1 != 4);
                            break;
                        case 5:
                            Synthese(pc, 2024, 1);
                            break;
                        case 6:
                            Say(pc, 131, "魔物模型是可以裝飾在$R;" +
                                "飛空庭上的魔物娃娃。$R;" +
                                "$P如果有魔物樣子的繪畫$R;" +
                                "只要有畫板（畫作完成）就可以得到$R;" +
                                "和它一樣的娃娃$R;" +
                                "$P您知道怎麼繪畫魔物嗎？$R;");
                            switch (Select(pc, "知道嗎?", "", "知道", "不知道"))
                            {
                                case 1:
                                    Say(pc, 131, "那就做一個漂亮的比基亞吧。$R;");
                                    break;
                                case 2:
                                    Say(pc, 131, "要繪畫就需要『畫板』和$R;" +
                                        "「魔物繪圖」兩種必要道具。$R;" +
                                        "$R要做『畫板』的$R;" +
                                        "就要拜託給賢者幫忙了。$R;" +
                                        "$P知道畫板的製作方法嗎？$R;");
                                    switch (Select(pc, "知道嗎", "", "知道", "不知道"))
                                    {
                                        case 1:
                                            Say(pc, 131, "那就做一個漂亮的比基亞吧。$R;");
                                            break;
                                        case 2:
                                            Say(pc, 131, "製作方法如下。$R;" +
                                                "$R需要的道具有『刷子』，$R技能要『精製道具』3級，$R;" +
                                                "$R材料有『木漿捲』一個、$R;" +
                                                "$R『杰利科』四個和『木材』一個$R;" +
                                                "$P繪圖不是一定會成功的。$R;" +
                                                "$P所以最好是多準備一個$R;" +
                                                "畫板比較好。$R;");
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 7:
                            break;
                    }
                } while (sel2 == 3 || sel2 == 4);
                return;
            }
            if (Puppetry_amask.Test(Puppetry.已經獲得木偶) || pc.Marionette != null)
            {
                Say(pc, 131, "您好像看過神秘的『活動木偶』了。$R;" +
                    "$P如果看過了，$R那就應該給您介紹『石像』吧！$R;");
                第一次对话(pc);
                return;
            }
            Say(pc, 131, "我是木偶術師總管$R;" +
                "$P這個世界上有很多神秘的東西，$R;" +
                "$R活動木偶可算是當中最神秘的喔！$R;");
        }

        void 第一次对话(ActorPC pc)
        {
            BitMask<Puppetry> Puppetry_cmask = pc.CMask["Puppetry"];

            Say(pc, 131, "石像是活動木偶的變異型態，$R全身散發著神秘感$R;" +
                "$R它們是擁有獨立思想的活動木偶$R;" +
                "$P只要給活動木偶能夠思考的道具的話$R它們就會成為石像了$R;" +
                "$P石像通常在我們休息的時候活動的$R;" +
                "$P它們會幫我們看店或者擊退魔物。$R;" +
                "$R風雨不改，為我們辛勤工作的$P就是石像囉$R;");
            if (!Puppetry_cmask.Test(Puppetry.第一次对话))
            {
                Say(pc, 131, "來，給您個好東西。$R;" +
                    "名字是『石像金寶利』$R;" +
                    "$P它是個能夠讓活動木偶$R有思考力的道具。$R;" +
                    "$R和活動木偶合成的話$R就會變成石像了。$R;" +
                    "$P擁有思考能力的石像$R是不能復原到活動木偶狀態的。$R;" +
                    "$R所以在合成的時候，要好好想清楚喔！$R;");
                if (CheckInventory(pc, 10002100, 1))
                {
                    Puppetry_cmask.SetValue(Puppetry.第一次对话, true);
                    GiveItem(pc, 10002100, 1);
                    Say(pc, 131, "得到石像金寶利$R;");
                    石像介绍(pc);
                    return;
                }
                Say(pc, 131, "…$R;" +
                    "$R咦？不能給您喔$R;" +
                    "把行李整理好再來吧。$R;");
                return;
            }
            石像介绍(pc);
        }

        void 石像介绍(ActorPC pc)
        {

            switch (Select(pc, "問什麼呢?", "", "石像是什麼？", "讓人看店的方法", "搜集道具的方法", "回去"))
            {
                case 1:
                    第一次对话(pc);
                    break;
                case 2:
                    Say(pc, 131, "讓石像看店的，就叫『石像商店』。$R;" +
                        "$P把石像商店就當作是自己的店就好了。$R;" +
                        "$R裡面出售的什麼道具，價格高低$R都是由您自己定喔！$R;" +
                        "$P設定石像商店的方法很簡單。$R;" +
                        "$P用道具控制石像，就可以讓他$R;" +
                        "做您想命令它做的事情了。$R;" +
                        "$P挑選『看店』的話$R就可以設定石像商店了。$R;" +
                        "$P設定想要出售的道具、價格、數量等$R;" +
                        "$R三個項目後，點撃OK鍵。$R;" +
                        "$P設定就完成了。$R;" +
                        "$P結束冒險後想休息時$R選擇『交給石像後退出』就可以了。$R;" +
                        "這樣的話$R石像就會自動給您出售道具了。$R;" +
                        "$P怎樣?簡單吧？$R;");
                    石像介绍(pc);
                    break;
                case 3:
                    Say(pc, 131, "在您休息的時候$R可以讓石像搜集道具。$R;" +
                        "$R可以搜集的東西是一下八種。$R;" +
                        "『食物、礦物、植物、魔法物、$R;" +
                        "寶物箱、出土品、各種物件、$R奇怪的東西等』。$R;" +
                        "$P雖然可能會搜集到『杰利科』$R那樣一般的東西，$R;" +
                        "但是偶爾也會搜集到稀有的道具的。$R;" +
                        "$R大致上都是已定的，但是實際上$R;" +
                        "您能夠搜集到什麼道具$R就要看您的『運氣』了。$R;" +
                        "$P各種石像都有自己擅長搜集的道具類型$R;" +
                        "$R像瑪歐斯那樣喜歡吃的石像$R;" +
                        "一定會在搜集食物上有一套吧？$R;" +
                        "$P像讓它看店的時候$R使用道具的話，可以設定各種性能$R;" +
                        "$P設定完畢後點撃$R『交給石像後退出』就可以了。$R;" +
                        "$R呵呵...聽起來很不錯啊，呵呵。$R;" +
                        "$P石像擁有的道具$R是放在『石像倉庫』裡的。$R;" +
                        "$R在石像倉庫裡的東西，如果不移到$R道具欄的話就不能使用。$R;" +
                        "$P石像倉庫是在石像設定目錄裡$R選擇的話就可以看了。$R;" +
                        "$R道具是不會消失或者丢失的，$R所以放心吧。$R;");
                    石像介绍(pc);
                    break;
                case 4:
                    break;
            }
        }
    }
}
