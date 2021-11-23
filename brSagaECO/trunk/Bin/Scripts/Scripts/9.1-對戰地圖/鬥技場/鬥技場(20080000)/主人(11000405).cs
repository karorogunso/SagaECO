using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaMap;

using SagaLib;
//所在地圖:鬥技場(20080000) NPC基本信息:主人(11000405) X:25 Y:8
namespace SagaScript.M20080000
{
    public class S11000405 : Event
    {
        public S11000405()
        {
            this.EventID = 11000405;
        }

        public override void OnEvent(ActorPC pc)
        {
            byte x, y;

            if (pc.Mode == PlayerMode.COLISEUM_MODE)
            {
                取消對戰模式(pc);
                return;
            }

            Say(pc, 11000405, 131, "歡迎來到鬥技場唷!!$R;" +
                                   "$R這裡是有著多種對戰方式，$R;" +
                                   "供冒險者之間切磋的戰鬥空間喔。$R;" +
                                   "$P但必須得先向我提出申請，$R;" +
                                   "不然是無法參戰的呀。$R;" +
                                   "$P決鬥方式有，$R;" +
                                   "把參賽者分成$R;" +
                                   "東、南、西、北騎士團的「騎士團練習」。$R;" +
                                   "$R還有把參賽者全體變身成皮露露，$R;" +
                                   "分成兩組競爭的「夢之狂想戰」。$R;" +
                                   "$P怎麼樣呢?$R;" +
                                   "$R想要參加嗎?$R;", "主人");

            switch (Select(pc, "'想要參加哪個競技活動?", "", "參加「鬥技場」", "參加「騎士團練習」", "參加「夢之狂想戰」", "從鬥技場出去", "什麼也不做"))
            {
                case 1:
                    鬥技場模式切換(pc);
                    break;

                case 2:
                    骑士团演习(pc);
                    break;

                case 3:
                   // GOTO EVT11000405202;
                    break;

                case 4:
                    x = (byte)Global.Random.Next(134, 135);
                    y = (byte)Global.Random.Next(47, 50);

                    Warp(pc, 10024000, x, y);
                    break;

                case 5:
                    break;
            }
        }

        void 骑士团演习(ActorPC pc)
        {
            Say(pc, 11000405, 131, "想要參加騎士團練習是嗎？$R;" +
                "讓我詳細告訴您規則好嗎？$R;");
            int sel2;
            do
            {
                sel2 = Select(pc, "聽騎士團練習規則嗎？", "", "聽騎士團練習規則。", "騎士團練習報名方法", "還是放棄吧");
                switch (sel2)
                {
                    case 1:
                        Say(pc, 11000405, 131, "騎士團練習是分為東南西北組$R;" +
                            "在規定時間內爭取得分的競賽唷$R;" +
                            "$P得分的方法有好幾種$R;" +
                            "「打倒對方玩家」2分$R;" +
                            "「破壞石榴石」1分$R;" +
                            "「破壞藍玉」3分$R;" +
                            "「破壞祖母綠」5分$R;" +
                            "「破壞電氣石」50分$R;" +
                            "只要完成上述的行為$R;" +
                            "就可以為自己所屬團隊取得分數唷$R;" +
                            "$P還想聽更詳細的嗎？$R;");
                        int sel;
                        do
                        {
                            sel = Select(pc, "繼續聽嗎？", "", "什麼叫石榴石？", "為什麼只有電氣石可以得到50分呢？", "我被打倒會怎麼樣？", "有沒有參加獎品？", "好了");
                            switch (sel)
                            {
                                case 1:
                                    Say(pc, 11000405, 131, "「石榴石」是戰爭中產生$R;" +
                                        "稀有的魔法物質喔$R;" +
                                        "$R除此以外，還有「藍玉」、$R;" +
                                        "「祖母綠」、「電氣石」等$R;" +
                                        "也是在戰爭中產生的唷$R;" +
                                        "$P破壞這些寶石$R;" +
                                        "就可以為自己所屬團隊取得分數呀$R;" +
                                        "$P且如果是持有知識技能的人$R;" +
                                        "破壞這些魔法物質的話$R;" +
                                        "還可以得到道具唷$R;" +
                                        "這些道具在騎士團練習完畢後$R;" +
                                        "是不會消失的唷$R;" +
                                        "所以生產系們，道具一定要$R;" +
                                        "保存到最後唷$R;");
                                    break;
                                case 2:
                                    Say(pc, 11000405, 131, "「電氣石」是最高分的魔法物質呀，$R;" +
                                        "可以知道所有參賽者的位置喔$R;" +
                                        "$P而且破壞「電氣石」的人$R;" +
                                        "參賽的時候，得分會增加兩倍唷。$R;" +
                                        "$R但是有幾個條件$R;" +
                                        "$P1）憑依中的人破壞電氣石，$R;" +
                                        "主人可以得到雙倍的分數$R;" +
                                        "$P2）如果得分2倍的人登出的話，$R;" +
                                        "其效果將轉移到周圍的參賽者呀。$R;" +
                                        "$P3）而若得分2倍的人死亡的話，$R;" +
                                        "其效果將轉移到打敗他的人身上唷。$R;");
                                    break;
                                case 3:
                                    Say(pc, 11000405, 131, "進行騎士團練習時，即使被打死了$R;" +
                                        "也可以得到同伴的幫助復活後$R;" +
                                        "返回儲存點唷$R;" +
                                        "$P但是如果選擇返回儲存點，$R;" +
                                        "當您回到報名的大樓中庭$R;" +
                                        "$P要返回前線，必須先在那裡等3分鐘$R;" +
                                        "，然後再跟報名工作人員說話唷$R;" +
                                        "$P放心，騎士團練習沒有死亡的懲罰$R;" +
                                        "$P並不會像格鬥場一樣，$R;" +
                                        "出現武器和防具$R降低耐久度的情況唷。$R;");
                                    break;
                                case 4:
                                    Say(pc, 11000405, 131, "騎士團練習結束後，$R;" +
                                        "還會給參賽全員分配基本經驗值唷。$R;" +
                                        "$P分配標準根據以下條件來定$R;" +
                                        "1）騎士團練習參加人數$R;" +
                                        "2）自己組的順序$R;" +
                                        "3）組裡的人數$R;" +
                                        "4）自己的得分$R;" +
                                        "5）有無拿取得各種獎賞$R;" +
                                        "$P想得到獎賞有幾種條件喔，$R;" +
                                        "個人死亡的次數，$R;" +
                                        "持有瞬間最大攻擊破壞$R;" +
                                        "個人所持的道具數目$R;" +
                                        "個人累積的破壞數量$R;");
                                    break;
                                case 5:
                                    Say(pc, 11000405, 131, "那麼，要不要參加試試看呢？$R;");
                                    break;
                            }
                        }
                        while (sel != 5);
                        break;
                        /*
                    case 2:
                        //KWAR_ENTRY 0
                        if (true)
                        {
                            Say(pc, 11000405, 131, "對不起，現在不是報名時間喔，$R;" +
                                "請等可以報名的時候，再來吧$R;");
                            return;
                        }
                        if (true)
                        {
                            Say(pc, 11000405, 131, "非常抱歉，下回的騎士團練習$R;" +
                                "有等級的限制唷$R;" +
                                "$R您的實力太强了，所以不能參加呀。$R;");
                            return;
                        } 
                        
                        Say(pc, 11000405, 131, "那麼在這個大樓中庭報名吧。$R;");
                        
                        switch (Global.Random.Next(1, 4))
                        {
                           
                            case 1:
                                Warp(pc, 20080000, 1, 1);
                              //warp  752
                                break;
                            case 2:
                                Warp(pc, 20080000, 1, 1);
                                //WARP 753
                                break;
                            case 3:
                                Warp(pc, 20080000, 1, 1);
                                //WARP 754
                                break;
                            case 4:
                                Warp(pc, 20080000, 1, 1);
                                //WARP 755
                                break;
                                
                        }
                        
                        break;
                        */
                        
                }
            } while (sel2 == 1);
        }

        void 鬥技場模式切換(ActorPC pc)
        {
            if (pc.PossesionedActors.Count != 0)
            {
                Say(pc, 11000405, 131, "抱歉，$R;" +
                                       "請您解除憑依好嗎?$R;" +
                                       "$R請申請參賽以後再憑依可以嗎?$R;", "主人");          
            }
            else
            {
                Say(pc, 11000405, 131, "知道了。$R;" +
                                       "現在給您15秒的時間做準備。$R;" +
                                       "$P好好準備一下吧!!$R;", "主人");

                pc.Mode = PlayerMode.COLISEUM_MODE;

                //SagaMap.Tasks.PC.PVPTime task = new SagaMap.Tasks.PC.PVPTime();
                //pc.Tasks.Add("PVPTime", task);
                //task.Activate();
            }
        }

        void 取消對戰模式(ActorPC pc)
        {
            Say(pc, 11000405, 131, "怎麼了嗎?$R;" +
                                   "$R不想要參加了呀?$R;", "主人");

            switch (Select(pc, "放棄參加嗎?", "", "不放棄", "放棄參加"))
            {
                case 1:
                    break;

                case 2:
                    if (pc.PossesionedActors.Count != 0)
                    {
                        Say(pc, 11000405, 131, "抱歉，$R;" +
                                               "請您解除憑依好嗎?$R;", "主人");
                    }
                    else
                    {
                        pc.Mode = PlayerMode.NORMAL;

                        Say(pc, 11000405, 131, "期待您下次的參加唷!!$R;", "主人");
                    }
                    break;
            }
        }


    }
}
