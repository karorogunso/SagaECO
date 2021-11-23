using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Item;
//所在地圖:裁縫阿姨的家(30020000) NPC基本信息:裁縫阿姨(11000149) X:3 Y:1
namespace SagaScript.M30020000
{
    public class S11000149 : Event
    {
        public S11000149()
        {
            this.EventID = 11000149;
        }

        public override void OnEvent(ActorPC pc)
        {         
            BitMask<Neko_01> Neko_01_cmask = pc.CMask["Neko_01"];
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];
            BitMask<Neko_02> Neko_02_amask = pc.AMask["Neko_02"];
            BitMask<Neko_02> Neko_02_cmask = pc.CMask["Neko_02"];
            BitMask<Neko_04> Neko_04_amask = pc.AMask["Neko_04"];
            BitMask<Neko_04> Neko_04_cmask = pc.CMask["Neko_04"];
            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];
            BitMask<Neko_09> Neko_09_mask = new BitMask<Neko_09>(pc.CMask["Neko_09"]);
           if (Neko_09_mask.Test(Neko_09.新绿任务开始))
            {
                新绿任务(pc);
                return;
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                if ((pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900 ||
                    pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902 ||
                    pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017907) &&
                Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                Neko_06_cmask.Test(Neko_06.與杏子對話) &&
                !Neko_06_cmask.Test(Neko_06.詢問裁縫阿姨))
                {

                    Say(pc, 131, "あら？おやまあ！$R;" +
                    "$Rかわいい子ネコちゃんが$R迷いこんで来たねえ。$R;" +
                    "$Rどうしたんだい？$Rご主人様はまだ外にいるのかい？$R;" +
                    "$P小さくてかわいい女の子だねぇ。$R;" +
                    "$R子ネコちゃん、お名前は…$R;", "裁縫おばさん");

                    Say(pc, 0, 131, "ボクは男の子だよ！！$R;", "ネコマタ（杏）");

                    Say(pc, 0, 131, "ちょっとちょっと！杏！$R;", " ");

                    Say(pc, 131, "！！！$R;" +
                    "$R……？！$R;" +
                    "$P……あなた、まさか、$R;" +
                    pc.Name + "ね？$R;" +
                    "$R…なんてこと？その格好…。$R;", "裁縫おばさん");

                    Say(pc, 0, 111, "裁縫おばさんに$Rこれまでのいきさつを話した。$R;", " ");

                    Say(pc, 131, "…なるほどね。$R;" +
                    "$R飛び出してきたネコ魂と$Rあなたの精神が融合を起こして$Rネコマタの姿になったと。$R;" +
                    "$Rで、今あなたの身体の抜け殻は$Rミランダの家にあるのね？$R;" +
                    "$Pうーん、$R;" +
                    "$R少し気になることがあるんだけど…$R;" +
                    "$R子ネコちゃん。$Rお名前はなんていうの？$R;", "裁縫おばさん");

                    Say(pc, 0, 131, "杏だよ。$R;", "ネコマタ（杏）");

                    Say(pc, 131, "杏ちゃん。$R今、お姉さんたちとお話できる？$R;" +
                    "$R気配を感じられなくなっていない？$R;", "裁縫おばさん");

                    Say(pc, 0, 131, "……うん、$R;" +
                    "$Rお返事してくれないよ。$R;", "ネコマタ（杏）");

                    Say(pc, 131, pc.Name + "$R;" +
                    "$Rあなたもネコちゃん達に$R話しかけてみて。$R;", "裁縫おばさん");

                    Say(pc, 0, 131, "え？…でも、$R;" +
                    "$Rネコの言葉わからないし…。$R;", " ");

                    Say(pc, 131, "何言ってるのよ、$R;" +
                    "$R気がついてないの？$Rあなた杏ちゃんと話せているでしょう？$R;" +
                    "$R今のあなたはネコ魂と融合して$Rネコ語を理解できるようになってるの。$R;", "裁縫おばさん");

                    Say(pc, 0, 131, "……！？$Rそういえば！$R;" +
                    "$R気が動転していて気付かなかった…。$R;", " ");
                    string neko = "桃";
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                        neko = "桃";
                    else if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                        neko = "緑";
                    else if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017907)
                        neko = "茜";
                    Say(pc, 0, 131, neko + "に話しかけてみるか…。$R;" +
                    "$Rううっ、$Rあらたまって話しかけるとなると$R緊張するな…。$R;" +
                    "$P…" + neko + "…？$R;", " ");
                    if (neko == "桃")
                        Say(pc, 0, 131, "がんばれ～……$R;" +
                        "$R……。$R;", "ネコマタ（桃）");
                    else if (neko == "緑")
                        Say(pc, 0, 131, "…だいじょうぶ…$R;" +
                        "$R……。$R;", "ネコマタ（緑）");
                    else if (neko == "茜")
                        Say(pc, 0, 131, "またあたしを呼んだ～…いいけどさ…$R;" +
                        "$R……。$R;", "ネコマタ（緑）");
                    
                    Say(pc, 0, 131, "え？$R;" +
                    "$Rなんか変だ…$R…心ここにあらずって感じだ。$R;", " ");

                    Say(pc, 131, "…やっぱり。$R;" +
                    "$R反応がおかしいわね、$Rネコちゃんの心が抜けてしまっている。$R;" +
                    "$R取り憑き先のあなたの身体から$Rあなたの魂が離れてしまったからだわ。$R;", "裁縫おばさん");

                    Say(pc, 0, 131, "ええっ！？$R;", " ");

                    Say(pc, 0, 131, "どうして！？$R…お姉ちゃんたち、どうなったの？$R;", "ネコマタ（杏）");

                    Say(pc, 131, "うーん、難しいんだけど…$R;" +
                    "$Rお姉ちゃんたちは$Rこの人の生命力の力を借りて$R精神体として生きていたのよ。$R;" +
                    "$Rこの人の心と身体がばらばらになって$R力を借りることができなくなったのね。$R;" +
                    "$P今はまだネコマタの形を保っているけど$Rいずれ保てなくなるわ。$R;" +
                    "$Rそうなると…$R;", "裁縫おばさん");

                    Say(pc, 0, 131, "そうなると…？$R;", " ");

                    Say(pc, 131, "…ネコちゃんたちは$R２度と元の姿に戻れなくなるわね。$R;", "裁縫おばさん");

                    Say(pc, 0, 131, "そんな…！！$R;", " ");

                    Say(pc, 0, 131, "ど、どうしよう…！$R;", "ネコマタ（杏）");

                    Say(pc, 131, "落ち着きなさい、$R;" +
                    "$Rネコちゃんたちの心は$Rまだこの世界のどこかに漂っているはず。$R;" +
                    "$Pまずはなによりも、あなたたちが$R元の身体に戻る方法を見つけることね。$R;", "裁縫おばさん");

                    Say(pc, 0, 131, "どうすればいいの？$R;", "ネコマタ（杏）");

                    Say(pc, 131, "…残念だけど、私にはわからないわ。$R;" +
                    "$R精神体のことに関しては$Rやっぱりタイタニア族が一番詳しいわね。$R;" +
                    "$Pギルド元宮のタイタニア代表に$R聞いてみるのがいいかしら？$R;", "裁縫おばさん");
                    PlaySound(pc, 2040, false, 100, 50);

                    Say(pc, 0, 131, "『あんず色の三角巾』を入手した！$R;", " ");
                    GiveItem(pc, 10017909, 1);
                    Say(pc, 131, "これを渡しておくわね。$R;" +
                    "$Rこれがあれば$Rあなたの魂が元の身体に戻るときに$Rその子はネコマタになれるはずよ。$R;", "裁縫おばさん");
                    Neko_06_cmask.SetValue(Neko_06.詢問裁縫阿姨, true);
                    return;
                }

            if (pc.Level >= 30 && pc.Fame >= 20 &&
                pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET) &&
                Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                !Neko_06_amask.Test(Neko_06.获得杏子) &&
                !Neko_06_cmask.Test(Neko_06.与喜歡花的米嵐多对话))
            {
                if (Neko_06_amask.Test(Neko_06.杏子任务开始) &&
                    pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 131, "トンカシティのミランダの家から$R;" +
                    "ネコ魂の入った壷を運んできておくれ。$R;" +
                    "$R頼んだよ。$R;", "裁縫おばさん");
                }
                else if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 131, "いらっしゃい。$R;" +
                     "$Rあらまあ、またあんたかい。$R;" +
                     "$Pちょうどよかったよ。$R;" +
                     "$Rネコマタ好きのあんたを見込んで$R;" +
                     "頼みごとがあるんだけど$R;" +
                     "$R聞いてくれるかい？$R;", "裁縫おばさん");
                    switch (Select(pc, "聞いてくれるかい？", "", "ネ、ネコマタ好きって……", "聞いてみる"))
                    {
                        case 1:
                            Say(pc, 131, "あら、そうなのかい？$R;");
                            Say(pc, 0, 131, "にゃぁ……$R;", "ネコマタ");
                            break;
                        case 2:
                            Say(pc, 131, "助かるわ……実はね$R;" +
                            "$Rトンカに住んでいる$R;" +
                            "私の友達のミランダの家に$R;" +
                            "ネコ魂が迷い込んだみたいなの。$R;" +
                            "$Rそのネコ魂が$R;" +
                            "それはそれは元気なネコ魂で$R;" +
                            "$Rやんちゃに困り果てたミランダは$R;" +
                            "ネコ魂を壷の中に閉じ込めたそうなの。$R;" +
                            "$Rその壷を私の所まで$R;" +
                            "運んできて欲しいのよ。$R;");
                            Say(pc, 0, 131, "にゃあ！にゃああ♪$R;", "ネコマタ");
                            Say(pc, 131, "そうよ子ネコちゃん。$R;" +
                            "$Rこの子たちの姉妹かも知れないしね。$R;");
                            switch (Select(pc, "？", "", "引き受ける", "やめておく"))
                            {
                                case 1:
                                    Say(pc, 131, "そうかい！助かるよ。$R;" +
                                     "$Rミランダの家はトンカシティ南東にある$R;" +
                                     "花壇のたくさんある家だよ。$R;" +
                                     "$Rミランダを家の前に待たせておくから$R;" +
                                     "行ってきてくれるかい?$R;");
                                    Say(pc, 0, 131, "にゃん♪にゃああん♪$R;", "ネコマタ");
                                    Neko_06_amask.SetValue(Neko_06.杏子任务开始, true);
                                    break;
                                case 2:
                                    Say(pc, 131, "あら、そうなのかい？$R;");
                                    Say(pc, 0, 131, "にゃぁ……$R;", "ネコマタ");
                                    break;
                            }
                            break;
                    }
                    return;
                }
            }

            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.被詢問犯人的事) &&
                !Neko_04_cmask.Test(Neko_04.被告知犯人是小孩))
            {
                Neko_04_cmask.SetValue(Neko_04.被告知犯人是小孩, true); 
                Say(pc, 0, 131, "桃子!$R這位奶奶是誰啊?$R;", "凱堤(綠子)");
                Say(pc, 0, 131, "什麼?$R綠子!什麼奶奶啊!!$R;", "凱堤（桃）");
                Say(pc, 131, "奶奶?$R叫我奶奶?$R;" +
                    "$R嗯嗯…叫奶奶!!$R;");
                Say(pc, 0, 131, "咪 咪 喵…$R;", "凱堤（桃）");
                Say(pc, 131, "那個就那樣了$R…你又被凱堤纏著了?$R;" +
                    "$R是人太好還是太傻啊$R;");
                Say(pc, 0, 131, "還是…那樣啊(嘆息)$R;");
                Say(pc, 0, 131, "咪咪!咪咪喵!喵!$R;" +
                    "$R咪咪!咪咪喵!喵!$R;");
                Say(pc, 131, "嗯嗯!這樣啊!$R;" +
                    "$R…雖然不知道是什麼意思$R但好像是在說「犯人是小孩!」$R;" +
                    "$P…到底什麼意思啊?$R;");
                Say(pc, 0, 131, "犯人是小孩…??$R;");
                return;
            }
            //*/
            if (Neko_02_cmask.Test(Neko_02.藍任務失敗))
            {
                Neko_02_cmask.SetValue(Neko_02.藍任務失敗, false);
                Say(pc, 131, "…快把「原始」停止，小貓的靈魂…$R真可憐啊$R;" +
                    "$R不要那麼傷心了，不是你的錯啊$R;" +
                    "$R你只是做了一件$R不管是誰一定要做的事情而已$R;" +
                    "$R那凱堤也總有一天會理解的$R;");
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.獲知原始的事情) &&
                !Neko_02_cmask.Test(Neko_02.得到藍) &&
                !Neko_02_cmask.Test(Neko_02.得到三角巾) &&
                CountItem(pc, 10043700) >= 1 &&
                CountItem(pc, 10021300) >= 1 &&
                CountItem(pc, 10019800) >= 1)
            {
                switch (Select(pc, "要不要委託製作『裁縫阿姨的三角巾』", "", "委託", "放棄"))
                {
                    case 1:
                        Neko_02_cmask.SetValue(Neko_02.得到三角巾, true);
                        TakeItem(pc, 10043700, 1);
                        TakeItem(pc, 10021300, 1);
                        TakeItem(pc, 10019800, 1);
                        GiveItem(pc, 10017904, 1);
                        Say(pc, 131, "得到『裁縫阿姨的三角巾』$R;");
                        Say(pc, 131, "可以的話，快阻止「原始」的復活吧$R;");
                        break;
                    case 2:
                        break;
                }
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.獲知原始的事情) &&
                !Neko_02_cmask.Test(Neko_02.得到藍))
            {
                Say(pc, 131, "可以的話，快阻止「原始」的復活吧$R;");
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.聽取建議) &&
                !Neko_02_cmask.Test(Neko_02.獲知原始的事情))
            {

                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900 ||
                        pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        藍(pc);
                        return;
                    }
                }
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.與裁縫阿姨第一次對話) &&
                !Neko_02_cmask.Test(Neko_02.得知維修方法))
            {
                Say(pc, 131, "修理故障的活動木偶的話$R凱堤也會離開活動木偶回來的$R;" +
                    "$R如果是唐卡人說不定會告訴您$R修理活動木偶得方法$R;" +
                    "$R工匠會打扮成道具鑑定師的模樣$R;" +
                    "不知道會不會在阿高普路斯呢~$R;");
                判斷(pc);
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.藍任務開始) && 
                !Neko_02_cmask.Test(Neko_02.與裁縫阿姨第一次對話))
            {

                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900 ||
                        pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        Neko_02_cmask.SetValue(Neko_02.與裁縫阿姨第一次對話, true);
                        Say(pc, 131, "哎喲！凱堤好吵啊！$R;" +
                            "$R怎麼了?$R;");
                        Say(pc, 0, 131, "喵!!咪咪!!喵…$R;", "凱堤(桃子)");
                        Say(pc, 131, "真是真是!$R;" +
                            "是嗎?這樣啊?$R不論怎樣我都會給您做啊$R;");
                        Say(pc, 131, "…??$R;");
                        Say(pc, 131, "你的凱堤好像發現了朋友$R;" +
                            "$P可能把出故障的活動木偶$R當作了主人$P憑依的狀態下怎樣都無法脫離啊$R;");
                        Say(pc, 0, 131, "喵~~~$R;", "凱堤(桃子)");
                        Say(pc, 131, "修理故障的活動木偶的話$R凱堤也會離開活動木偶回來的$R;" +
                            "$R如果是唐卡人說不定會告訴您$R修理活動木偶得方法$R;" +
                            "$R唐卡是活動木偶和飛空庭的$R最大生産國$R;" +
                            "$R可是真的奇怪啊…$R為什麼“凱堤”$R會在沒有生命的活動木偶上…$R;" +
                            "$R難道…?不是…不會是的$R;");
                        Say(pc, 131, "…?$R;");
                        return;
                    }
                }
            }
            


            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.再次與祭祀對話) &&
                !Neko_01_cmask.Test(Neko_01.得到裁縫阿姨的三角巾))
            {
                Say(pc, 131, "哎呀!$R好可愛的寵物啊!$R;" +
                    "$P坐在肩膀上…$R好…好…心情好像很好啊$R;");
                Say(pc, 0, 131, "咪~嗷$R;", "");
                Say(pc, 131, "…??$R;");
                if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                    Neko_01_cmask.Test(Neko_01.再次與祭祀對話) &&
                    !Neko_01_cmask.Test(Neko_01.得到裁縫阿姨的三角巾) &&
                    CountItem(pc, 10043700) >= 1 &&
                    CountItem(pc, 10021300) >= 1 &&
                    CountItem(pc, 10019800) >= 1)
                {
                    Say(pc, 131, "哎呀!$R;" +
                        "那『棉緞帶』還有『布』和『線』！$R;" +
                        "$P只要集齊這三樣$R就可以給那傢伙製作漂亮的禮物了$R;" +
                        "$R怎麼樣?要製作嗎?$R;");
                    switch (Select(pc, "要製作嗎？", "", "要", "不要"))
                    {
                        case 1:
                            Neko_01_cmask.SetValue(Neko_01.得到裁縫阿姨的三角巾, true);
                            TakeItem(pc, 10043700, 1);
                            TakeItem(pc, 10021300, 1);
                            TakeItem(pc, 10019800, 1);
                            GiveItem(pc, 10017904, 1);
                            Say(pc, 131, "得到『裁縫阿姨的三角巾』$R;");
                            Say(pc, 0, 131, "咪-咪-喵$R;");
                            Say(pc, 131, "好可愛啊…看來心情很好啊$R太好了$R;" +
                                "$P哎呀！你的小貓不見了？$R真是，怎麼搞得?$R;" +
                                "$R這麼可愛…$R;" +
                                "$R…對了!$R我奶奶說小貓喜歡「溫暖的光」$R;" +
                                "$P給那小貓溫暖的光的話$R會不會能看到牠的樣子呢？$R怎麼樣?$R;");
                            Say(pc, 0, 131, "喵~$R;");
                            break;
                        case 2:
                            Say(pc, 131, "真是…沒辦法啊…$R下次再來吧$R;");
                            break;
                    }
                    return;
                }
                Say(pc, 131, "真的可惜啊...$R只要有材料，可以給牠製作漂亮的禮物呢…$R;");
                return;
            }
            /*
            if (!_0b12)
            {
                判斷(pc);
                return;
            }
            */
            判斷(pc);
        }

        void 判斷(ActorPC pc)
        {
            BitMask<Puppet_02> Puppet_02_mask = pc.CMask["Puppet_02"];
            if (Puppet_02_mask.Test(Puppet_02.要求製作泰迪))
            {
                木偶泰迪(pc);
                return;
            }
            if (CountItem(pc, 10020208) >= 1)
            {
                Say(pc, 131, "呃?你拿著的那個東西$R;" +
                    "好像是『縫製玩偶的布』啊$R;" +
                    "$R…原來是這樣啊$R;" +
                    "$P這是能動的玩偶$R;" +
                    "『活動木偶泰迪』的材料!$R;" +
                    "$R只要你願意我可以給你製作喔$R;");
                switch (Select(pc, "要不要請他幫忙呢?", "", "要", "不要"))
                {
                    case 1:
                        Say(pc, 131, "不要著急啦，還差幾樣材料$R;" +
                            "$R一個是『棉花』還有一個$R;" +
                            "是『奧拉克妮線』$R;" +
                            "最後是3枝『針』$R;" +
                            "$P但是那針不是一般的針$R;" +
                            "$P是需要得裁縫之神守護的$R;" +
                            "特殊針3支$R;" +
                            "$P用什麼辦法可以弄到那種針嗎?$R;" +
                            "$R那個我不太清楚$R;" +
                            "我從來不外出的$R;" +
                            "$P無論如何找一找試試看吧$R;" +
                            "我會在這裡等您的$R;");
                        Puppet_02_mask.SetValue(Puppet_02.要求製作泰迪, true);
                        break;
                    case 2:
                        普通販賣(pc);
                        break;
                }
                return;
            }
            普通販賣(pc);
        }

        void 普通販賣(ActorPC pc)
        {
            switch (Select(pc, "想做什麼呢?", "", "買男性服裝", "買女性服裝", "賣東西", "委託裁縫", "委託烹調", "什麼都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 19);
                    Say(pc, 131, "再來玩吧$R;");
                    break;
                case 2:
                    OpenShopBuy(pc, 20);
                    Say(pc, 131, "再來玩吧$R;");
                    break;
                case 3:
                    OpenShopSell(pc, 20);
                    Say(pc, 131, "再來玩吧$R;");
                    break;
                case 4:
                    Synthese(pc, 2054, 5);
                    break;
                case 5:
                    Synthese(pc, 2040, 5);
                    break;
                case 6:
                    break;
            }
        }

        void 藍(ActorPC pc)
        {
            BitMask<Neko_02> Neko_02_cmask = pc.CMask["Neko_02"];

            Neko_02_cmask.SetValue(Neko_02.獲知原始的事情, true);
            Say(pc, 131, "還是那樣了!?$R;" +
                "$R真是大事啊…!$R;");
            Say(pc, 0, 131, "…!?$R;", " ");
            Say(pc, 131, "平復一下心情後好好聽啊$R;" +
                "$R那活動木偶塔依$R不是你認識的活動木偶$R;" +
                "$P那塔依是「原始」$R;");
            Say(pc, 0, 131, "…原始…?$R;", "凱堤(桃子)");
            Say(pc, 131, "是啊~$R;" +
                "$P活動木偶塔依$R是根據機械文明時代的設計圖$R在身上貼上洋鐵後製作而成的$R自動戰鬥兵器的複製品$R;" +
                "$P以現在的技術$R無法發揮它原來的性能$R所以只能當作活動木偶來用$R;");
            Say(pc, 131, "也可以說是在塔依中$R真的難得擁有「原來的性能」的$R;" +
                "$P把那個叫「原始」$R擁有人工智能的「原始」$R聽說自己一個也可以瞬間$R把一個村落毁掉$R;" +
                "$P萬一「原始」出現的話$R技術人員會把那個活動木偶停止後$R立即銷毁$R;" +
                "$P萬一以前的「破壞命令」$R還殘餘在電子頭腦裡的話$R就大事不妙了$R;");
            Say(pc, 0, 131, "…$R;", " ");
            Say(pc, 131, "要停止「原始」啓動啊…$R;" +
                "$R現在開始告訴你停止塔依的方法$R;" +
                "$R塔依的背下面有個修理箱$R;" +
                "$R把它打開，裡面有黃色插頭$R把那個拔掉就可以了$R;" +
                "$P那樣的話「原始」會$R馬上停止活動的$R;" +
                "$P「原始」自己恢復的話$R到時候就沒辦法了$R;" +
                "$R可以的話盡快阻止「原始」復活啊$R;");

            if (CountItem(pc, 10017902) >= 1)
            {
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                    {
                        Say(pc, 0, 131, "稍等!!$R;", "凱堤(桃子)");
                        Say(pc, 131, "?…$R是粉紅色的凱堤…$R這真是沒辦法…$R;");
                        Say(pc, 0, 131, "不過…$R那樣的話…藍…也會消失啊!!$R;" +
                            "$P那可不行!!$R;", "凱堤(桃子)");
                        Say(pc, 131, "…$R;");
                        Say(pc, 0, 131, "…救救藍$R;", "凱堤(綠子)");
                        Say(pc, 0, 131, "綠子!?$R;", "凱堤(桃子)");
                        Say(pc, 0, 131, "…求求您$R朋友們都在戰爭中死掉了$R;" +
                            "$R…現在就剩下我們兩個$R;" +
                            "$R…就剩下我們兩個$R;", "凱堤(綠子)");
                        Say(pc, 0, 131, "綠子…$R;", "凱堤(桃子)");
                        Say(pc, 131, "…$R;" +
                            "知道了$R雖然不能肯定…$R;" +
                            "$P可以拿跟以前一樣的材料過來嗎?$R;" +
                            "『棉緞帶』、『布』和『線』$R;" +
                            "$P給您製作跟那凱堤一樣的$R三角頭巾吧$R;" +
                            "$P只好相信凱堤會憑依在$R我製作的三角頭巾上$R;" +
                            "$R現在好了嗎?$R;");
                        Say(pc, 0, 131, "喵$R;");
                        return;
                    }
                }
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 131, "稍等!!$R;", "凱堤(桃子)");
                    Say(pc, 131, "?…$R是粉紅色的凱堤…$R這真是沒辦法…$R;");
                    Say(pc, 0, 131, "不過…$R那樣的話…藍…也會消失啊!!$R;" +
                        "$P那可不行!!$R;", "凱堤(桃子)");
                    Say(pc, 131, "…$R;");
                    Say(pc, 131, "…$R;" +
                        "知道了$R雖然不能肯定…$R;" +
                        "$P可以拿跟以前一樣的材料過來嗎?$R;" +
                        "『棉緞帶』、『布』和『線』$R;" +
                        "$P給您製作跟那凱堤一樣的$R三角頭巾吧$R;" +
                        "$P只好相信凱堤會憑依在$R我製作的三角頭巾上$R;" +
                        "$R現在好了嗎?$R;");
                    Say(pc, 0, 131, "喵$R;");
                    return;
                }
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "…救一下藍$R;", "凱堤(綠子)");
                    Say(pc, 131, "?…$R是草綠色的凱堤…$R這真是沒辦法…$R;");
                    Say(pc, 0, 131, "…求求您$R朋友們都在戰爭中死掉了$R;" +
                        "$R…現在就剩下我們兩個$R;" +
                        "$R…就剩下我們兩個$R;", "凱堤(綠子)");
                    Say(pc, 131, "…$R;");
                    Say(pc, 131, "…$R;" +
                        "知道了$R雖然不能肯定…$R;" +
                        "$P可以拿跟以前一樣的材料過來嗎?$R;" +
                        "『棉緞帶』、『布』和『線』$R;" +
                        "$P給您製作跟那凱堤一樣的$R三角頭巾吧$R;" +
                        "$P只好相信凱堤會憑依在$R我製作的三角頭巾上$R;" +
                        "$R現在好了嗎?$R;");
                    Say(pc, 0, 131, "喵$R;");
                    return;
                }
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        Say(pc, 0, 131, "稍等!!$R;", "凱堤(桃子)");
                        Say(pc, 131, "?…$R是粉紅色的凱堤…$R這真是沒辦法…$R;");
                        Say(pc, 0, 131, "不過…$R那樣的話…藍…也會消失啊!!$R;" +
                            "$P那可不行!!$R;", "凱堤(桃子)");
                        Say(pc, 131, "…$R;");
                        Say(pc, 0, 131, "…救救藍$R;", "凱堤(綠子)");
                        Say(pc, 0, 131, "綠子!?$R;", "凱堤(桃子)");
                        Say(pc, 0, 131, "…求求您$R朋友們都在戰爭中死掉了$R;" +
                            "$R…現在就剩下我們兩個$R;" +
                            "$R…就剩下我們兩個$R;", "凱堤(綠子)");
                        Say(pc, 0, 131, "綠子…$R;", "凱堤(桃子)");
                        Say(pc, 131, "…$R;" +
                            "知道了$R雖然不能肯定…$R;" +
                            "$P可以拿跟以前一樣的材料過來嗎?$R;" +
                            "『棉緞帶』、『布』和『線』$R;" +
                            "$P給您製作跟那凱堤一樣的$R三角頭巾吧$R;" +
                            "$P只好相信凱堤會憑依在$R我製作的三角頭巾上$R;" +
                            "$R現在好了嗎?$R;");
                        Say(pc, 0, 131, "喵$R;");
                        return;
                    }
                }
            }
            if (CountItem(pc, 10017900) >= 1 && CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "稍等!!$R;", "凱堤(桃子)");
                Say(pc, 131, "?…$R是粉紅色的凱堤…$R這真是沒辦法…$R;");
                Say(pc, 0, 131, "不過…$R那樣的話…藍…也會消失啊!!$R;" +
                    "$P那可不行!!$R;", "凱堤(桃子)");
                Say(pc, 131, "…$R;");
                Say(pc, 0, 131, "…救救藍$R;", "凱堤(綠子)");
                Say(pc, 0, 131, "綠子!?$R;", "凱堤(桃子)");
                Say(pc, 0, 131, "…求求您$R朋友們都在戰爭中死掉了$R;" +
                    "$R…現在就剩下我們兩個$R;" +
                    "$R…就剩下我們兩個$R;", "凱堤(綠子)");
                Say(pc, 0, 131, "綠子…$R;", "凱堤(桃子)");
                Say(pc, 131, "…$R;" +
                    "知道了$R雖然不能肯定…$R;" +
                    "$P可以拿跟以前一樣的材料過來嗎?$R;" +
                    "『棉緞帶』、『布』和『線』$R;" +
                    "$P給您製作跟那凱堤一樣的$R三角頭巾吧$R;" +
                    "$P只好相信凱堤會憑依在$R我製作的三角頭巾上$R;" +
                    "$R現在好了嗎?$R;");
                Say(pc, 0, 131, "喵$R;");
                return;
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 131, "稍等!!$R;", "凱堤(桃子)");
                Say(pc, 131, "?…$R是粉紅色的凱堤…$R這真是沒辦法…$R;");
                Say(pc, 0, 131, "不過…$R那樣的話…藍…也會消失啊!!$R;" +
                    "$P那可不行!!$R;", "凱堤(桃子)");
                Say(pc, 131, "…$R;");
                Say(pc, 131, "…$R;" +
                    "知道了$R雖然不能肯定…$R;" +
                    "$P可以拿跟以前一樣的材料過來嗎?$R;" +
                    "『棉緞帶』、『布』和『線』$R;" +
                    "$P給您製作跟那凱堤一樣的$R三角頭巾吧$R;" +
                    "$P只好相信凱堤會憑依在$R我製作的三角頭巾上$R;" +
                    "$R現在好了嗎?$R;");
                Say(pc, 0, 131, "喵$R;");
                return;
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "…救一下藍$R;", "凱堤(綠子)");
                Say(pc, 131, "?…$R是草綠色的凱堤…$R這真是沒辦法…$R;");
                Say(pc, 0, 131, "…求求您$R朋友們都在戰爭中死掉了$R;" +
                    "$R…現在就剩下我們兩個$R;" +
                    "$R…就剩下我們兩個$R;", "凱堤(綠子)");
                Say(pc, 131, "…$R;");
                Say(pc, 131, "…$R;" +
                    "知道了$R雖然不能肯定…$R;" +
                    "$P可以拿跟以前一樣的材料過來嗎?$R;" +
                    "『棉緞帶』、『布』和『線』$R;" +
                    "$P給您製作跟那凱堤一樣的$R三角頭巾吧$R;" +
                    "$P只好相信凱堤會憑依在$R我製作的三角頭巾上$R;" +
                    "$R現在好了嗎?$R;");
                Say(pc, 0, 131, "喵$R;");
                return;
            }
        }

        void 木偶泰迪(ActorPC pc)
        {
            switch (Select(pc, "什麼事情啊?", "", "買男性服裝", "買女性服裝", "賣東西", "委託裁縫", "委託烹調", "製作活動木偶泰迪", "委託烹調"))
            {
                case 1:
                    OpenShopBuy(pc, 19);
                    Say(pc, 131, "再來玩吧$R;");
                    break;
                case 2:
                    OpenShopBuy(pc, 20);
                    Say(pc, 131, "再來玩吧$R;");
                    break;
                case 3:
                    OpenShopSell(pc, 20);
                    Say(pc, 131, "再來玩吧$R;");
                    break;
                case 4:
                    Synthese(pc, 2054, 5);
                    break;
                case 5:
                    Synthese(pc, 2040, 5);
                    break;
                case 6:
                    if (CountItem(pc, 10020208) >= 1 &&
                        CountItem(pc, 10019701) >= 1 &&
                        CountItem(pc, 10019702) >= 1 &&
                        CountItem(pc, 10019703) >= 1 &&
                        CountItem(pc, 10024002) >= 1 &&
                        CountItem(pc, 10019600) >= 1)
                    {
                        Say(pc, 131, "材料都弄齊了!$R;" +
                            "現在開始就是我做的事了$R;" +
                            "交給我吧!$R;");
                        Say(pc, 131, "給了他『縫製玩偶的布』$R;" +
                            "給了他『奧拉克妮線』$R;" +
                            "給了他『棉花』$R;" +
                            "給了他『早晨的針』$R;" +
                            "給了他『白天的針』$R;" +
                            "給了他『夜晚的針』$R;");

                        Fade(pc, FadeType.Out, FadeEffect.Black);
                        Wait(pc, 1000);
                        Wait(pc, 1000);
                        Fade(pc, FadeType.In, FadeEffect.Black);
                        Say(pc, 131, "好了!$R;" +
                            "我嘔心瀝血的傑作$R;" +
                            "$R很可愛吧?$R;" +
                            "好好珍惜使用吧！$R;");
                        PlaySound(pc, 4006, false, 100, 50);
                        Say(pc, 131, "得到了『活動木偶泰迪』$R;");
                        TakeItem(pc, 10020208, 1);
                        TakeItem(pc, 10019701, 1);
                        TakeItem(pc, 10019702, 1);
                        TakeItem(pc, 10019703, 1);
                        TakeItem(pc, 10024002, 1);
                        TakeItem(pc, 10019600, 1);
                        GiveItem(pc, 10022000, 1);
                        return;
                    }
                    Say(pc, 131, "活動木偶泰迪的材料是$R;" +
                        "『縫製玩偶的布』$R;" +
                        "『棉花』$R;" +
                        "『奧拉克妮線』$R;" +
                        "『針』$R;" +
                        "$P針如果不是得裁縫之神守護的$R;" +
                        "特殊針，是不行的阿$R;" +
                        "$R裁縫之神化成蝴蝶的樣子$R;" +
                        "注視著這個世界呢$R;" +
                        "$P無論怎麼樣，你找找看吧$R;" +
                        "我會等著的!$R;");
                    木偶泰迪(pc);
                    break;
                case 7:
                    break;
            }
        }

       void 新绿任务(ActorPC pc)
       {
		BitMask<Neko_09> Neko_09_mask = new BitMask<Neko_09>(pc.CMask["Neko_09"]);
		//int selection;
		Say(pc, 0, 131, "にゃん～！！$R;" + 
		"にゃおお～～ん！$R;", "ネコマタ");
		
		Say(pc, 131, "あら、まぁ！$R;" + 
		"どうしたんだい！？$R;", "裁縫おばさん");
		Wait(pc, 1980);
		
		Say(pc, 0, 131, "おばさんに、天まで続く塔での$R;" + 
		"出来事を説明した。$R;", " ");
		
		Say(pc, 131, "つまり……。$R;" + 
		"$Rネコちゃんを見つけたけれど$R;" + 
		"すぐに光の塊となって$R;" + 
		"弾けて消えてしまった……と？$R;", "裁縫おばさん");
		
		Say(pc, 0, 131, "にゃん！$R;", "ネコマタ");
		
		Say(pc, 131, "その後、ネコちゃんの$R;" + 
		"気配はどこにも$R;" + 
		"なくなってしまったのね？$R;", "裁縫おばさん");
		
		Say(pc, 0, 131, "にゃん！$R;", "ネコマタ");
		
		Say(pc, 131, "う～ん、言いにくいんだけど……。$R;", "裁縫おばさん");
		
		Say(pc, 0, 131, "にゃ？$R;", "ネコマタ");
		
		Say(pc, 131, "そのネコちゃんは……。$R;" + 
		"「昇天」したんじゃないんのかねぇ？$R;", "裁縫おばさん");
		
		Say(pc, 0, 131, "？？？$R;", "ネコマタ");
		
		Say(pc, 131, "……もともとネコちゃんは$R;" + 
		"精神体だから、こういう表現が$R;" + 
		"あっているのかわからないけど。$R;", "裁縫おばさん");
		
		Say(pc, 0, 131, "にゃ、にゃぁ？$R;", "ネコマタ");
		
		Say(pc, 131, "死んでしまったんじゃ……。$R;", "裁縫おばさん");
		
		Say(pc, 0, 131, "にゃーーー！？$R;" + 
		"$Pにゃんにゃにゃ、にゃにゃん！$R;" + 
		"にゃにゃーん！！$R;", "ネコマタ");
		
		Say(pc, 131, "無理言わないでおくれよ。$R;" + 
		"$R……いくらおばさんでも$R;" + 
		"消滅してしまったネコマタ魂を$R;" + 
		"元に戻すことなんて……。$R;", "裁縫おばさん");
		
		Say(pc, 0, 131, "にゃ……。ぐずっ、ぐずっ。$R;", "ネコマタ");
		
		Say(pc, 131, "……そうねぇ。$R;" + 
		"$R黒の聖堂の司祭に$R;" + 
		"相談してみるのはどうかしら？$R;" + 
		"$P司祭は死魔を統べる能力を持つ$R;" + 
		"ウォーロックたちのマスター。$R;" + 
		"$Rおばさんよりも$R;" + 
		"死後の世界に詳しいと思うんだよ。$R;", "裁縫おばさん");
		
		Say(pc, 0, 131, "にゃ？ぐずっ、ぐずっ。$R;", "ネコマタ");
		
		Say(pc, 131, "これを持って行きなさい。$R;" + 
		"もし、ネコマタの魂が戻ったら$R;" + 
		"きっと役に立つと思うわ。$R;", "裁縫おばさん");
		PlaySound(pc, 2040, false, 100, 50);
		GiveItem(pc, 10017917, 1);
		Say(pc, 0, 131, "『緑の三角巾』を入手した。$R;", " ");
		Neko_09_mask.SetValue(Neko_09.綠色三角巾入手, true);
		Neko_09_mask.SetValue(Neko_09.新绿任务开始, false);
       }

       void 萬聖節(ActorPC pc)
        {

            //EVT1100014953
            switch (Select(pc, "怎麼做好呢？", "", "就那樣打招呼", "不給糖就搗蛋！"))
            {
                case 1:
                    判斷(pc);
                    break;
                case 2:
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
                    {
                        if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025800 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024350 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024351 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024352 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024353 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024354 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024355 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024356 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024357 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024358 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022500 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022600 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022700 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022800)
                        {
                            Say(pc, 131, "呵呵呵$R;" +
                                "$R小妖精長的好可愛阿$R;" +
                                "來！給你餅乾，不許淘氣啊$R;");
                            if (CheckInventory(pc, 10009300, 1))
                            {
                                //_0b12 = true;
                                GiveItem(pc, 10009300, 1);
                                return;
                            }
                            return;
                        }
                    }
                    Say(pc, 131, "嗯…打扮後再來吧$R;" +
                        "到時候我會給你餅乾的$R;");
                    break;
            }
        }
    }
}
