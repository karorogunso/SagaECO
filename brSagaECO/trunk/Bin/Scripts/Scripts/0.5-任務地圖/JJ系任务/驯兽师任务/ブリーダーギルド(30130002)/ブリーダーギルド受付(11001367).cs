using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30130002
{
    public class S11001367 : Event
    {
        public S11001367()
        {
            this.EventID = 11001367;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<jjxs> jjxs_mask = new BitMask<jjxs>(pc.CMask["jjxs"]);

            if (jjxs_mask.Test(jjxs.面试))
            {
                Say(pc, 131, "最後にあなたがブリーダーになるに$R;" +
                "相応しいか面接いたします。$R;" +
                "右奥の部屋で少々お待ちください。$R;", "ブリーダーギルド受付");
                return;
            }
            else if (jjxs_mask.Test(jjxs.失败))
            {
                Say(pc, 131, "ようこそ、ブリーダーギルドへ$R;" +
                "どのようなご用件でしょうか？$R;", "ブリーダーギルド受付");
                switch (Select(pc, "何か質問等はございますか？", "", "いえ、特にありません", "ブリーダーの説明を", "もう一度、面接を！"))
                {
                    case 2:
                        Say(pc, 131, "では、説明に入らせていただきます。$R;", "ブリーダーギルド受付");

                        Say(pc, 131, "先ずブリーダーと言う職業に$R;" +
                        "ついてですが、この職業はペット育成に$R;" +
                        "特化した職業となります。$R;" +
                        "$P基本能力はバックパッカー職の上位版$R;" +
                        "と言った感じでしょうか。$R;" +
                        "また、ブリーダー専用の騎乗ペット$R;" +
                        "もございます。$R;" +
                        "$Rそちらについてはブリーダーに$R;" +
                        "転職されてからの$R;" +
                        "ご説明とさせていただきます。$R;", "ブリーダーギルド受付");

                        Say(pc, 131, "次に、ジョブジョイントについて$R;" +
                        "ご説明させていただきます。$R;" +
                        "$Rこちらは既に各職に転職されてしまった$R;" +
                        "方でも条件さえ満たせば$R;" +
                        "どなたでも転職可能と言う$R;" +
                        "画期的な転職システムなのです。$R;" +
                        "$P何故、それが可能かと言いますと$R;" +
                        "ブリーダーギルドは民間のギルド$R;" +
                        "だからなのです。$R;" +
                        "$P因みにスキルは対応したレベルに$R;" +
                        "到達すれば自動的に習得されるように$R;" +
                        "なっております。$R;", "ブリーダーギルド受付");
                        break;
                    case 3:
                        Say(pc, 131, "面接に落ちてしまわれたのですか……$R;" +
                        "$Rそれでは、右奥の部屋へ$R;" +
                        "お進みください。$R;", "ブリーダーギルド受付");
                        jjxs_mask.SetValue(jjxs.失败, false);
                        jjxs_mask.SetValue(jjxs.正确, false);
                        jjxs_mask.SetValue(jjxs.面试, true);

                        break;
                }
                return;
            }
            else if((pc.Job == pc.JobBasic && pc.JobLevel1 > 29) ||
                (pc.Job == pc.Job2X && pc.JobLevel2X > 29) ||
                (pc.Job == pc.Job2T && pc.JobLevel2T > 29))
            {
                Say(pc, 131, "ようこそ、ブリーダーギルドへ$R;" +
                "転職希望の方でしょうか？$R;", "ブリーダーギルド受付");
                if (Select(pc, "転職希望の方でしょうか？", "", "はい、そうです！", "いいえ、違います") == 1)
                {
                    说明(pc);
                    return;
                }
            }
            Say(pc, 131, "有事么？$R;", "ブリーダーギルド受付");
        }

        void 说明(ActorPC pc)
        {
            BitMask<jjxs> jjxs_mask = new BitMask<jjxs>(pc.CMask["jjxs"]);

            Say(pc, 131, "では、説明に入らせていただきます。$R;", "ブリーダーギルド受付");

            Say(pc, 131, "先ずブリーダーと言う職業に$R;" +
            "ついてですが、この職業はペット育成に$R;" +
            "特化した職業となります。$R;" +
            "$P基本能力はバックパッカー職の上位版$R;" +
            "と言った感じでしょうか。$R;" +
            "また、ブリーダー専用の騎乗ペット$R;" +
            "もございます。$R;" +
            "$Rそちらについてはブリーダーに$R;" +
            "転職されてからの$R;" +
            "ご説明とさせていただきます。$R;", "ブリーダーギルド受付");

            Say(pc, 131, "次に、ジョブジョイントについて$R;" +
            "ご説明させていただきます。$R;" +
            "$Rこちらは既に各職に転職されてしまった$R;" +
            "方でも条件さえ満たせば$R;" +
            "どなたでも転職可能と言う$R;" +
            "画期的な転職システムなのです。$R;" +
            "$P何故、それが可能かと言いますと$R;" +
            "ブリーダーギルドは民間のギルド$R;" +
            "だからなのです。$R;" +
            "$P因みにスキルは対応したレベルに$R;" +
            "到達すれば自動的に習得されるように$R;" +
            "なっております。$R;", "ブリーダーギルド受付");

            Say(pc, 131, "以上で説明を終わりますが$R;" +
            "何か質問等はございますか？$R;", "ブリーダーギルド受付");
            if (Select(pc, "何か質問等はございますか？", "", "いえ、特にありません", "もう一度、説明を") == 1)
            {
                Say(pc, 131, "最後にあなたがブリーダーになるに$R;" +
                "相応しいか面接いたします。$R;" +
                "右奥の部屋で少々お待ちください。$R;", "ブリーダーギルド受付");
                jjxs_mask.SetValue(jjxs.开始, true);
                jjxs_mask.SetValue(jjxs.面试, true);
                return;
            }
            说明(pc);
            return;
        }
    }
}