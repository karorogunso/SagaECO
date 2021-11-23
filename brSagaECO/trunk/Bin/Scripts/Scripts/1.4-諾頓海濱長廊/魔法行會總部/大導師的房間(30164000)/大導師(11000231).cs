using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30164000
{
    public class S11000231 : Event
    {
        public S11000231()
        {
            this.EventID = 11000231;
        }


        public override void OnEvent(ActorPC pc)
        {
            BitMask<wsj> wsj_mask = new BitMask<wsj>(pc.CMask["wsj"]);
            //int selection;
            if (wsj_mask.Test(wsj.扫把))
            {
                Say(pc, 11000232, 131, "これこれ。$R;" +
                "$R大導師様は尊ぶべき存在であるからして$R;" +
                "直接、口を聞いてはならぬ。$R;", "大導師の手下");

                Say(pc, 11000233, 131, "大導師様に用があるときは$R;" +
                "わしたちに話しかけるのじゃ。$R;", "大導師の手下");

                Say(pc, 131, "……。$R;", "大導師");

                Say(pc, 11000232, 131, "……えっ？$R;" +
                "直接話されると言うのですか？$R;" +
                "$Rわかりました。$R;" +
                "$Pうむむむむ……。$R;" +
                "$R大導師様がお話になられる。$R;" +
                "心して聞くように……。$R;", "大導師の手下");

                Say(pc, 131, "……。$R;" +
                "$Pヘンピコのことで$R;" +
                "ここにきたのじゃろう？$R;" +
                "$R巻き込んでしまったようですな。$R;" +
                "$Pふう……。$R;" +
                "迷惑をかけてすまなかったね。$R;" +
                "$Pヘンピコは、本当は$R;" +
                "すごい力を秘めた子なのじゃが$R;" +
                "あの通りの性格でなぁ。$R;" +
                "$R修行もさぼってばかり。$R;" +
                "ちゃんと、育つか心配で心配で。$R;" +
                "$Pまあ、今回はよいとしようか。$R;" +
                "$Rおぬしのような友達が出来たのならば$R;" +
                "あの子も、きっと$R;" +
                "よい方向にかわるじゃろう。$R;" +
                "$Pさあ、あの子のもとに$R;" +
                "行ってあげておくれ。$R;" +
                "$R結界は解除したから$R;" +
                "あの子の力でもあそこから$R;" +
                "出ることが出来るはずだ……。$R;", "大導師");
                Warp(pc, 50000000, 3, 3);
                return;
            }
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);
            BitMask<Job2X_05> Job2X_05_mask = pc.CMask["Job2X_05"];
            BitMask<FallenTitantia> FallenTitantia_mask = pc.CMask["FallenTitantia"];

            if (Job2X_05_mask.Test(Job2X_05.防禦過高))//_4A12)
            {
                Say(pc, 131, "那麼把魔導士的標誌$R;" +
                    "『魔導士的紋章』給您吧$R;");
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Job2X_05_mask.SetValue(Job2X_05.轉職完成, true);

                    ChangePlayerJob(pc, PC_JOB.SORCERER);
                    pc.JEXP = 0;

                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 4000);
                    Say(pc, 131, "…$R;" +
                        "$P呵呵$R;" +
                        "紋章烙印得非常漂亮$R;" +
                        "那麼現在您是魔導士了$R;");
                    PlaySound(pc, 4012, false, 100, 50);
                    Say(pc, 131, "轉職為『魔導士』$R;");
                    Job2X_05_mask.SetValue(Job2X_05.轉職開始, false);
                    Job2X_05_mask.SetValue(Job2X_05.防禦過高, false);
                    Job2X_05_mask.SetValue(Job2X_05.拒絕轉職, false);
                    Job2X_05_mask.SetValue(Job2X_05.要求5樣物品, false);
                    Say(pc, 131, "現在您身上的力量$R;" +
                        "只釋放了很小部份$R;" +
                        "$R您今後可以以自己的力量成長了$R;");
                    return;
                }
                Say(pc, 131, "防禦能力太強$R紋章就不能烙印上去$R;" +
                    "先把裝備脫下來吧$R;");
                return;
            }
            if (Job2X_05_mask.Test(Job2X_05.拒絕轉職))//_4A11)
            {
                switch (Select(pc, "轉職嗎?", "", "轉職", "不轉職"))
                {
                    case 1:
                        Say(pc, 131, "那麼把魔導士的標誌$R;" +
                            "『魔導士的紋章』給您吧$R;");
                        if (pc.Inventory.Equipments.Count == 0)
                        {
                            Job2X_05_mask.SetValue(Job2X_05.轉職完成, true);

                            ChangePlayerJob(pc, PC_JOB.SORCERER);

                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "…$R;" +
                                "$P呵呵$R;" +
                                "紋章烙印得非常漂亮$R;" +
                                "那麼現在您是魔導士了$R;");
                            PlaySound(pc, 4012, false, 100, 50);
                            Say(pc, 131, "轉職為『魔導士』$R;");
                            Job2X_05_mask.SetValue(Job2X_05.轉職開始, false);
                            Job2X_05_mask.SetValue(Job2X_05.防禦過高, false);
                            Job2X_05_mask.SetValue(Job2X_05.拒絕轉職, false);
                            Job2X_05_mask.SetValue(Job2X_05.要求5樣物品, false);
                            Say(pc, 131, "現在您身上的力量$R;" +
                                "只釋放了很小部份$R;" +
                                "$R您今後可以以自己的力量成長了$R;");
                            return;
                        }
                        Job2X_05_mask.SetValue(Job2X_05.防禦過高, true);
                        Say(pc, 131, "防禦能力太強$R紋章就不能烙印上去$R;" +
                            "先把裝備脫下來吧$R;");
                        break;
                    case 2:
                        Say(pc, 131, "好好考慮吧，下定決心再來吧$R;");
                        break;
                }
                return;
            }

            Say(pc, 11000232, 131, "您看！$R;" +
                "$R大導師是高貴的人$R;" +
                "不能直接跟他對話呀$R;");
            Say(pc, 11000233, 131, "有什麼要問大導師$R;" +
                "請跟我們說吧$R;");

            if (pc.Inventory.Equipments.Count == 0)
            {
                Say(pc, 11000232, 131, "看！$R;" +
                    "$R大導師面前怎麼能這樣！$R;" +
                    "快穿上衣服吧$R;");
                Say(pc, 11000233, 131, "有什麼要問大導師$R;" +
                    "請跟我們說吧$R;");
                return;
            }
            if (Job2X_05_mask.Test(Job2X_05.轉職開始))//_3A89)
            {
                if (Job2X_05_mask.Test(Job2X_05.要求5樣物品))//_3A90)
                {
                    if (CountItem(pc, 10013350) >= 1 && 
                        CountItem(pc, 10025300) >= 1 && 
                        CountItem(pc, 10024900) >= 1 && 
                        CountItem(pc, 10009150) >= 1 && 
                        CountItem(pc, 10014300) >= 1)
                    {
                        Say(pc, 131, "嗯…$R;" +
                            "看樣子，找齊了$R;" +
                            "$P讓我看看吧$R;");
                        TakeItem(pc, 10013350, 1);
                        TakeItem(pc, 10025300, 1);
                        TakeItem(pc, 10024900, 1);
                        TakeItem(pc, 10009150, 1);
                        TakeItem(pc, 10014300, 1);
                        Say(pc, 131, "給他一塊『金光藍寶珠破片』$R;" +
                            "給他一粒『圓莉多的寶石』$R;" +
                            "給他一張『紋章紙』$R;" +
                            "給他一粒『雪花』$R;" +
                            "給他一粒『水晶』$R;");
                        Say(pc, 131, "沒錯…$R;" +
                            "$R那最後再問您$R;" +
                            "真的想當魔導士嗎?$R;");

                        switch (Select(pc, "轉職嗎?", "", "轉職", "不轉職"))
                        {
                            case 1:
                                Say(pc, 131, "那麼把魔導士的標誌$R;" +
                                    "『魔導士的紋章』給您吧$R;");
                                if (pc.Inventory.Equipments.Count == 0)
                                {
                                    Job2X_05_mask.SetValue(Job2X_05.轉職完成, true);

                                    ChangePlayerJob(pc, PC_JOB.SORCERER);

                                    PlaySound(pc, 3087, false, 100, 50);
                                    ShowEffect(pc, 4131);
                                    Wait(pc, 4000);
                                    Say(pc, 131, "…$R;" +
                                        "$P呵呵$R;" +
                                        "紋章烙印得非常漂亮$R;" +
                                        "那麼現在您是魔導士了$R;");
                                    PlaySound(pc, 4012, false, 100, 50);
                                    Say(pc, 131, "轉職為『魔導士』$R;");
                                    Job2X_05_mask.SetValue(Job2X_05.轉職開始, false);
                                    Job2X_05_mask.SetValue(Job2X_05.防禦過高, false);
                                    Job2X_05_mask.SetValue(Job2X_05.拒絕轉職, false);
                                    Job2X_05_mask.SetValue(Job2X_05.要求5樣物品, false);
                                    Say(pc, 131, "現在您身上的力量$R;" +
                                        "只釋放了很小部份$R;" +
                                        "$R您今後可以以自己的力量成長了$R;");
                                    return;
                                }
                                Job2X_05_mask.SetValue(Job2X_05.防禦過高, true);
                                Say(pc, 131, "防禦能力太強$R紋章就不能烙印上去$R;" +
                                    "先把裝備脫下來吧$R;");
                                break;
                            case 2:
                                Job2X_05_mask.SetValue(Job2X_05.拒絕轉職, true);
                                Say(pc, 131, "好好考慮吧，下定決心再來吧$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 131, "嗯…$R;" +
                        "好像還沒有找完$R;" +
                        "$R找完後再來吧$R;");
                    return;
                }
                Job2X_05_mask.SetValue(Job2X_05.要求5樣物品, true);
                //_3A90 = true;
                Say(pc, 131, "…$R;");
                Say(pc, 131, "呀？$R;" +
                    "想直接跟他說嗎？$R;" +
                    "$R知道了$R;" +
                    "$P嗯…$R;" +
                    "$R大導師要親自講話$R;" +
                    "請留意…$R;");
                Say(pc, 131, "想成為魔導士？$R;" +
                    "嗯…$R;" +
                    "$R知道了$R;" +
                    "$P現在開始，要成為了不起的人$R;" +
                    "一定要接受試煉的內容呢$R;" +
                    "『金光藍寶珠破片』$R;" +
                    "『圓莉多的寶石』$R;" +
                    "『紋章紙』$R;" +
                    "『雪花』$R;" +
                    "『水晶』$R;" +
                    "把這五件道具找來吧$R;" +
                    "$P諾頓周圍的$R;" +
                    "魔物那裡能得到阿$R;" +
                    "$P顯示您的實力吧$R;");
                return;
            }

            if (!mask.Test(NDFlags.大导师第一次对话))
            {
                mask.SetValue(NDFlags.大导师第一次对话, true);
                //_2A42 = true;
                SkillPointBonus(pc, 1);
                Say(pc, 11000231, 131, "…$R;");
                Say(pc, 11000232, 131, "什麼？$R;" +
                    "想直接跟他說嗎？$R;" +
                    "$R知道了$R;" +
                    "$P嗯…$R;" +
                    "$R大導師要親自講話$R;" +
                    "請留心聽唷$R;");
                Wait(pc, 2000);
                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 2000);
                Say(pc, 131, "技能點數上升1$R;");
                Say(pc, 11000231, 131, "…$R;" +
                    "不要慌$R;" +
                    "只是喚醒了您沉睡的力量…$R;" +
                    "$P一般的修行$R;" +
                    "不能得到所有力量$R;" +
                    "$R要得到所有的力量$R;" +
                    "需要一種『契機』的$R;" +
                    "$P在總部通過修行$R;" +
                    "可以賦予契機喔$R;" +
                    "$R您現在可以$R;" +
                    "靠自己的力量成長了$R;" +
                    "$P暫時繼續勤奮刻苦吧$R;");
                //EVENTEND
                return;
            }

            /*
            if (!_0b24)
            {
                //EVT1100023103
                _0b24 = true;
                Say(pc, 131, "…$R;");
                Say(pc, 11000232, 131, "呀？$R;" +
                    "想直接跟他說嗎？$R;" +
                    "$R知道了$R;" +
                    "$P嗯…$R;" +
                    "$R大導師要親自講話$R;" +
                    "請留心聽唷$R;");
                Say(pc, 11000231, 131, "…$R;" +
                    "$P為了賢皮克$R;" +
                    "來到這裡的呀$R;" +
                    "$R連您也被捲進來了$R;" +
                    "哎…$R;" +
                    "麻煩您了，很抱歉$R;" +
                    "賢皮克是一個力氣非凡的小子$R;" +
                    "但沒有人能管得了他$R;" +
                    "$R不做修行$R;" +
                    "擔心能不能好好成長呀$R;" +
                    "$P這次好像沒有什麼問題，$R;" +
                    "$R現在有了像您這樣的朋友$R;" +
                    "這孩子應該會變好吧$R;" +
                    "$P那麼，現在回到$R;" +
                    "那個孩子的身邊吧$R;" +
                    "$R已經解除結界，$R;" +
                    "他可以自己出來了…$R;");
                //EVENTMAP_IN 0 1 3 3 0
                //SWITCH START
                //ME.WORK0 = -1 EVT1100023103a
                //SWITCH END
                //EVENTEND
                //EVT1100023103a
                Say(pc, 131, "怎麼回事$R;" +
                    "好像我的MP值消耗完了！$R;" +
                    "$R年齡大了，稍等一下吧$R;" +
                    "要恢復需要時間…$R;" +
                    "$R先散散步再來吧$R;");
                //EVENTEND
                return;
            }
            */

            
            if (pc.Job >= (PC_JOB)41 &&
                pc.Job < (PC_JOB)80 &&
                FallenTitantia_mask.Test(FallenTitantia.任务开始) &&
                !FallenTitantia_mask.Test(FallenTitantia.大导师告知可以回来了))
            {
                if (FallenTitantia_mask.Test(FallenTitantia.给花))
                {
                    FallenTitantia_mask.SetValue(FallenTitantia.大导师告知可以回来了, true);
                    //_4a65 = true;
                    Say(pc, 11000231, 131, "…$R;");
                    Say(pc, 11000232, 131, "呀？$R;" +
                        "想直接跟他說嗎？$R;" +
                        "$R知道了$R;" +
                        "$P嗯…$R;" +
                        "$R大導師要親自講話$R;" +
                        "請留心聽唷$R;");
                    Say(pc, 11000231, 131, "看著這個水鏡呢$R;" +
                        "$R該跟說聲謝謝了$R;" +
                        "$P打算讓他在總部，休息一會兒$R;" +
                        "$R有機會的話，去探望他吧$R;");
                    return;
                }
                FallenTitantia_mask.SetValue(FallenTitantia.告知需要花, true);
                //_4a64 = true;
                Say(pc, 11000231, 131, "…$R;");
                Say(pc, 11000232, 131, "呀？$R;" +
                    "想直接跟他說嗎？$R;" +
                    "$R知道了$R;" +
                    "$P嗯…$R;" +
                    "$R大導師要親自講話$R;" +
                    "請留意…$R;");
                Say(pc, 11000231, 131, "進入了秘密房間$R;" +
                    "沒關係，沒關係$R;" +
                    "不是怪您$R;" +
                    "$R他是從塔妮亞世界$R;" +
                    "流放出來的$R;" +
                    "$P哪有比從故鄉被趕出去$R;" +
                    "更傷心的事情啊$R;" +
                    "$R想送給他東西，$R;" +
                    "安慰他呀…$R;" +
                    "$P他住過的地方，$R;" +
                    "到處都開著漂亮的花，$R;" +
                    "像天國一樣美麗唷$R;" +
                    "$R看到花會不會好一點$R;" +
                    "但是在諾頓花是稀有的$R;" +
                    "所以很難看到花…$R;" +
                    "$P如果有機會去奧克魯尼亞大陸$R;" +
                    "一定給他送上花呀$R;");
                return;
            }
            //*/
        }
    }
}