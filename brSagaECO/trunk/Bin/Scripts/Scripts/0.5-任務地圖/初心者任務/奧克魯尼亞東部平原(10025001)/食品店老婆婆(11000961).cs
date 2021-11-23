using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:食品店老婆婆(11000961) X:38 Y:120
namespace SagaScript.M10025001
{
    public class S11000961 : Event
    {
        public S11000961()
        {
            this.EventID = 11000961;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.物品過重教學完成))
            {
                物品過重教學(pc);
                return;
            }

            Say(pc, 11000961, 131, "剛才給您的，是『笨重傑利科』，$R;" +
                                   "統稱沉重的石頭。$R;" +
                                   "$P拿著太多沉重或龐大的東西，$R;" +
                                   "就會動不了的呀!$R;" +
                                   "$R那個時候，$R;" +
                                   "把非必要的道具扔掉才行。$R;" +
                                   "$P把圖示往視窗外拖動，就可以扔掉，$R;" +
                                   "那樣的話，就可以重新走動了。$R;" +
                                   "$P冒險時要經常留意自己的行李啊!$R;" +
                                   "不然要是被敵人包圍，$R;" +
                                   "就會被魔物打死的…$R;" +
                                   "$R這樣的事，也是有可能發生的!$R;", "食品店老婆婆");
        }

        void 物品過重教學(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.物品過重教學開始))
            {
                Beginner_02_mask.SetValue(Beginner_02.物品過重教學開始, true);

                Say(pc, 11000961, 131, "這裡是食品店。$R;" +
                                       "$R烹調的時候，$R;" +
                                       "需要的基本材料在這裡買就有了。$R;" +
                                       "$P我…是吧!$R;" +
                                       "拿這個看看吧?$R;", "食品店老婆婆");

                PlaySound(pc, 2041, false, 100, 50);
                GiveItem(pc, 10032810, 1);
                Say(pc, 0, 0, "得到『笨重傑利科』!$R;", " ");
            }

            if (Beginner_02_mask.Test(Beginner_02.物品過重教學開始) && 
                CountItem(pc, 10032810) > 0)
            {
                Beginner_02_mask.SetValue(Beginner_02.物品過重教學完成, true);

                Say(pc, 11000961, 131, "走不動了吧?$R;" +
                                       "$R您的『重量』過重，所以才會那樣。$R;" +
                                       "$R道具視窗裡，$R;" +
                                       "會顯示「重量」和「體積」$R;" +
                                       "看一下吧?$R;" +
                                       "$P這裡顯示為紅色的話，就不能動了。$R;" +
                                       "$P那個時候要扔掉道具，$R;" +
                                       "把重量和體積減低才能移動呀!$R;" +
                                       "$P剛剛給您的『笨重傑利科』，$R;" +
                                       "一般叫沉重的石頭。$R;" +
                                       "$R它又重又沒用，所以趕緊扔掉吧!$R;" +
                                       "$P把圖示向窗外拖，就可以扔掉了，$R;" +
                                       "那樣的話，就可以重新走路了。$R;" +
                                       "$P冒險時要經常留意自己的行李啊!$R;" +
                                       "不然要是被敵人包圍，$R;" +
                                       "就會被魔物打死的…$R;" +
                                       "$R這樣的事，也有可能發生的!$R;", "食品店老婆婆");
            }
        }
    }
}
