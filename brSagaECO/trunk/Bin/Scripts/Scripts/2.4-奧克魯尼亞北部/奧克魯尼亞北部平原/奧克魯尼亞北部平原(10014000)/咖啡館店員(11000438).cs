using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Quests;
//所在地圖:奧克魯尼亞北部平原(10014000) NPC基本信息:咖啡館店員(11000438) X:105 Y:136
namespace SagaScript.M10014000
{
    public class S11000438 : Event
    {
        public S11000438()
        {
            this.EventID = 11000438;

            this.notEnoughQuestPoint = "哎呀，剩餘的任務點數是0啊$R;" +
                "下次再來吧$R;";
            this.leastQuestPoint = 1;
            this.questFailed = "失敗了?$R;" +
                "$R真是遺憾阿$R;" +
                "下次一定要成功喔$R;";
            this.alreadyHasQuest = "任務順利嗎?$R;";
            this.gotNormalQuest = "那拜託了$R;" +
                "$R等任務結束後，再來找我吧;";
            this.gotTransportQuest = "是阿，道具太重了吧$R;" +
                "所以不能一次傳送的話$R;" +
                "分幾次給就可以！;";
            this.questCompleted = "真是辛苦了$R;" +
                "$R任務成功了$R來！收報酬吧！;";
            this.transport = "哦哦…全部收來了嗎?;";
            this.questCanceled = "嗯…如果是你，我相信你能做到的$R;" +
                "很期待呢……;";
            this.questTooEasy = "唔…但是對你來說$R;" +
                "說不定是太簡單的事情$R;" +
                "$R那樣也沒關係嘛?$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Emil_Letter> Emil_Letter_mask = new BitMask<Emil_Letter>(pc.CMask["Emil_Letter"]);

            if (!Emil_Letter_mask.Test(Emil_Letter.埃米爾介紹書任務完成) &&
                CountItem(pc, 10043081) > 0)
            {
                埃米爾介紹書(pc);
                return;
            }

            switch (Select(pc, "歡迎光臨咖啡館!", "", "任務服務台", "什麼也不做"))
            {
                case 1:
                    HandleQuest(pc, 6);
                    break;

                case 2:
                    break;
            }
        }

        void 埃米爾介紹書(ActorPC pc)
        {
            BitMask<Emil_Letter> Emil_Letter_mask = new BitMask<Emil_Letter>(pc.CMask["Emil_Letter"]);

            int selection;

            Emil_Letter_mask.SetValue(Emil_Letter.埃米爾介紹書任務開始, true);

            TakeItem(pc, 10043081, 1);

            Say(pc, 11000438, 131, "哎呀!!$R;" +
                                   "$R這不是『埃米爾介紹書』嗎?$R;" +
                                   "$P看您拿著這封信，您是初心者吧?$R;" +
                                   "$R這裡是「阿高普路斯市下城」的$R;" +
                                   "「咖啡館」的分店喔。$R;" +
                                   "$P剛開始冒險時，會有點辛苦的。$R;" +
                                   "$R這是我做的果醬唷，$R;" +
                                   "不嫌棄的話，在冒險的時候用吧。$R;", "咖啡館店員");

            PlaySound(pc, 2042, false, 100, 50);
            GiveItem(pc, 10033900, 1);
            Say(pc, 0, 0, "得到『果醬』!$R;", " ");

            Say(pc, 11000438, 131, "那麼，要挑戰任務嗎?$R;", "咖啡館店員");

            selection = Select(pc, "想要挑戰任務嗎?", "", "有什麼樣的任務?", "挑戰任務", "放棄");

            while (selection != 3)
            {
                switch (selection)
                {
                    case 1:
                        任務種類詳細解說(pc);
                        break;

                    case 2:
                        擊退皮露露(pc);
                        break;

                    case 3:
                        break;
                }

                selection = Select(pc, "想要挑戰任務嗎?", "", "有什麼樣的任務?", "挑戰任務", "放棄");
            }
        }

        void 任務種類詳細解說(ActorPC pc)
        {
            int selection;

            Say(pc, 11000438, 131, "任務的要求幾乎都很簡單喔!$R;" +
                                   "$R「咖啡館」除了賣糧食外，$R;" +
                                   "也會介紹一些工作給冒險者唷!$R;" +
                                   "$P久而久之，口碑越來越好了，$R;" +
                                   "所以在「阿高普路斯市」周圍，$R;" +
                                   "開了許多分店。$R;" +
                                   "$R最近魔物比較多，有點害怕呀…$R;" +
                                   "$P工作內容有「擊退魔物」、$R;" +
                                   "「收集/搬運道具」等。$R;" +
                                   "$R當然，我們會根據任務內容，$R;" +
                                   "給予不同的報酬唷!$R;" +
                                   "$P工作內容不同，$R;" +
                                   "任務執行方式也不一樣。$R;" +
                                   "$R想聽詳細的說明嗎?$R;", "咖啡館店員");

            selection = Select(pc, "想聽什麼說明呢?", "", "任務的注意事項", "關於「擊退任務」", "關於「收集任務」", "關於「搬運任務」", "什麼也不聽");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000438, 131, "成功完成任務的話，$R;" +
                                               "可以得到相對應的經驗值和報酬。$R;" +
                                               "$R但托付的任務並不是很多，$R;" +
                                               "有時候會供過於求，不太平衡啊!$R;" +
                                               "$P所以工作是有次數限制的。$R;" +
                                               "$R真是非常抱歉，因為還有別的冒險者，$R;" +
                                               "所以沒辦法只好這樣，請您諒解呀!$R;" +
                                               "$P除此之外，$R;" +
                                               "為了避免有人承接任務卻沒有回報，$R;" +
                                               "所以任務都定了時限呀!$R;" +
                                               "$R規定時間內沒有完成任務，$R;" +
                                               "就會當作任務失敗唷!$R;" +
                                               "這個任務，就會給別的冒險者了。$R;" +
                                               "$P剩餘的任務點數和任務所剩時間，$R;" +
                                               "可以在「任務視窗」確認喔。$R;" +
                                               "$R盡量不要失敗，$R;" +
                                               "請您努力吧!$R;", "咖啡館店員");
                        break;

                    case 2:
                        Say(pc, 11000438, 131, "「擊退任務」就是要在指定的區域，$R;" +
                                               "抓到指定數量的魔物。$R;" +
                                               "$P例如：$R;" +
                                               "擊退「奧克魯尼亞東方平原」的$R;" +
                                               "5隻「皮露露」。$R;" +
                                               "$R接受這樣的任務時，$R;" +
                                               "只要抓住指定區域的5隻「皮露露」，$R;" +
                                               "任務就算成功了唷!$R;" +
                                               "$P其他地方的「皮露露」，$R;" +
                                               "並不會列入計算的。請多留意呀!$R;" +
                                               "$P委託內容和完成進度等，$R;" +
                                               "可以在「任務視窗」隨時確認唷!$R;" +
                                               "$R執行任務時，只要打開這個視窗，$R;" +
                                               "就可以隨時確認，很方便吧?$R;" +
                                               "$P任務成功後，要記得回報，$R;" +
                                               "這樣才可以拿到報酬喔。$R;" +
                                               "$R關於「報酬」，$R;" +
                                               "可以在任何附近的「任務服務台」拿到，$R;" +
                                               "所以只要到附近的「服務台」就可以了。$R;", "咖啡館店員");
                        break;

                    case 3:
                        Say(pc, 11000438, 131, "「收集任務」就是收集指定道具的任務唷!$R;" +
                                               "$P如果接到收集3個『杰利科』的任務。$R;" +
                                               "$R只要想盡辦法收集3個『杰利科』，$R;" +
                                               "就算任務完成了。$R;" +
                                               "$P收集完以後，$R;" +
                                               "把道具拿到「任務服務台」就可以了。$R;" +
                                               "$R接受「收集任務」時，$R;" +
                                               "選擇「任務服務台」$R;" +
                                               "就會顯示交易視窗喔!$R;" +
                                               "$P把收集的道具，$R;" +
                                               "從道具視窗移到交易視窗的左邊，$R;" +
                                               "$R點擊『確認』再點擊『交易』，$R;" +
                                               "道具就交易到「服務台」了。$R;" +
                                               "$P交易指定數量後，$R;" +
                                               "任務就算成功了。$R;" +
                                               "$R如果道具太重，$R;" +
                                               "一次交易不了，可以分批送出喔。$R;" +
                                               "$P我會清點交易的道具的。$R;" +
                                               "$R我不會算錯啦，盡管放心!!$R;", "咖啡館店員");
                        break;

                    case 4:
                        Say(pc, 11000438, 131, "「搬運任務」是從委託人那裡取得道具，$R;" +
                                               "然後轉交給收件人的任務唷!$R;" +
                                               "$P例如：$R;" +
                                               "在「奧克魯尼亞東方平原」的$R;" +
                                               "「咖啡館店員」那裡$R;" +
                                               "取得4個『杰利科』。$R;" +
                                               "$R然後拿給「奧克魯尼亞東方平原」的$R;" +
                                               "「鑑定師」。$R;" +
                                               "$P接到這樣的任務的話，$R;" +
                                               "只要從我這裡取得4個『杰利科』，$R;" +
                                               "送到「奧克魯尼亞東方平原」的$R;" +
                                               "「鑑定師」那裡，就算成功了。$R;" +
                                               "$P要給予運送道具，$R;" +
                                               "只要跟相關的人交談就可以了。$R;" +
                                               "$R任務成功以後，$R;" +
                                               "就跟「擊退任務」一樣，$R;" +
                                               "到「任務服務台」，拿取報酬就可以了。$R;", "咖啡館店員");
                        break;
                }

                selection = Select(pc, "想聽什麼說明呢?", "", "任務的注意事項", "關於「擊退任務」", "關於「收集任務」", "關於「搬運任務」", "什麼也不聽");
            }
        }

        void 擊退皮露露(ActorPC pc)
        {
            BitMask<Emil_Letter> Emil_Letter_mask = new BitMask<Emil_Letter>(pc.CMask["Emil_Letter"]);

            Say(pc, 11000438, 131, "最近「阿高普路斯市」周圍，$R;" +
                                   "出現了很多「皮露露」，$R;" +
                                   "能不能擊退呢?$R;" +
                                   "$R「皮露露」是像布丁的天藍色魔物。$R;" +
                                   "$P在任務清單選擇任務後，$R;" +
                                   "點擊『確認』，就可以接受任務了。$R;" +
                                   "$R那麼，您想挑戰嗎?$R;", "咖啡館店員");

            switch (Select(pc, "想怎麼做呢?", "", "挑戰任務", "再聽一次說明", "放棄"))
            {
                case 1:
                    Emil_Letter_mask.SetValue(Emil_Letter.埃米爾介紹書任務完成, true);

                    HandleQuest(pc, 1);
                    break;

                case 2:
                    擊退皮露露(pc);
                    break;

                case 3:
                    break;
            }         
        }
    }
}