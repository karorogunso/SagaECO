using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10024000
{
    public class S11001975 : Event
    {
        public S11001975()
        {
            this.EventID = 11001975;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Iris_1> Iris_1_mask = new BitMask<Iris_1>(pc.CMask["Iris_1"]);
            //int selection;
            if (Iris_1_mask.Test(Iris_1.第一次对话后))
            {
                Say(pc, 131, "よぅ、お疲れ。$R;" +
                "研究所に行くのか？$R;", "壊し屋バキア");
                if (Select(pc, "研究所に行く？", "", "行く", "行かない") == 1)
                {
                    Warp(pc, 30166000, 9, 16);
                    return;
                }
                Say(pc, 131, "行かないのか。$R;" +
                "たまには顔を出してやれよ、$R;" +
                "ジーニャはあれで寂しがりだからな$R;", "壊し屋バキア");

            }
        }
    }
}