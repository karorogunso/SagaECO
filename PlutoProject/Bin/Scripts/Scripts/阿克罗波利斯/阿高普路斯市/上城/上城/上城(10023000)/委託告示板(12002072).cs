using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:委託告示板(12002072) X:186 Y:108
namespace SagaScript.M10023000
{
    public class S12002072 : Event
    {
        public S12002072()
        {
            this.EventID = 12002072;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Sinker> mask = pc.AMask["Sinker"];
            if (pc.Job == PC_JOB.WIZARD
                || pc.Job == PC_JOB.SORCERER
                || pc.Job == PC_JOB.SAGE
                || pc.Job == PC_JOB.SHAMAN
                || pc.Job == PC_JOB.ELEMENTER
                || pc.Job == PC_JOB.ENCHANTER
                || pc.Job == PC_JOB.VATES
                || pc.Job == PC_JOB.DRUID
                || pc.Job == PC_JOB.BARD
                || pc.Job == PC_JOB.WARLOCK
                || pc.Job == PC_JOB.GAMBLER
                || pc.Job == PC_JOB.NECROMANCER)
            {
                if (pc.Level > 34)
                {
                    
                    if (mask.Test(Sinker.看過告示牌))//_2b02 && _7a91)
                    {
                        Say(pc, 131, "「现在没有已登记的委託」$R;");
                        return;
                    }
                    
                    Say(pc, 131, "「给所有生产系的人的委托」$R;");
                    Say(pc, 131, "要不要先念一下这个委托书？$R;");
                    switch (Select(pc, "念吗?", "", "念", "不念"))
                    {
                        case 1:
                            Say(pc, 131, "这是委託生產系的您办的事情$R;" +
                                "看到这告示的话，请到法伊斯特$R;" +
                                "$R委托人:农夫行会出差职员$R;");
                            mask.SetValue(Sinker.看過告示牌, true);
                            //_7a91 = true;
                            break;
                    }
                    return;
                }
            }
            //EVT1200207204
            /*
            if (!_7a83 && pc.Level > 29)
            {
                if (_7a83)
                {
                    Say(pc, 131, "……$R;" +
                        "그외에새로운게시물은$R;" +
                        "없는것같다$R;");
                    return;
                }
                Say(pc, 131, "누군가붙여놓은메모가있다$R;" +
                    "읽을까?$R;");
                switch (Select(pc, "읽을까?", "", "읽는다", "읽지"))
                {
                    case 1:
                        Say(pc, 131, "서쪽도개교의초보자안내인이$R;" +
                            "뭔가를준다더군$R;" +
                            "$R뭘주는지는모르겠지만$R;" +
                            "공짜니까가봐야하지않겠나?$R;");
                        break;
                }
                return;
            }
            */
            Say(pc, 131, "「现在没有已登记的委托」$R;");

        }
    }
}
