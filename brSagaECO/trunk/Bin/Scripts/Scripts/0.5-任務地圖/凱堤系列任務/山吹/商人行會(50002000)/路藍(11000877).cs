using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50002000
{
    public class S11000877 : Event
    {
        public S11000877()
        {
            this.EventID = 11000877;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_04> Neko_04_amask = pc.AMask["Neko_04"];
            BitMask<Neko_04> Neko_04_cmask = pc.CMask["Neko_04"];

            if (!Neko_04_cmask.Test(Neko_04.交出商人的傳達品))
            {
                Say(pc, 11000877, 131, "在和平的阿高普路斯市上城$R居然發生這種事情呀$R;");
                return;
            }
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.被告知襲擊) && 
                Neko_04_cmask.Test(Neko_04.與嚮導對話) &&
                !Neko_04_cmask.Test(Neko_04.被詢問犯人的事))
            {
                if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.交出商人的傳達品) &&
                !Neko_04_cmask.Test(Neko_04.收到商人的傳達品))
                {
                    return;
                }
                if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.收到商人的傳達品) &&
                !Neko_04_cmask.Test(Neko_04.被告知襲擊))
                {
                    return;
                }
                Neko_04_cmask.SetValue(Neko_04.被詢問犯人的事, true);
                Say(pc, 11000877, 131, "再問您一次$R您真的沒見到犯人嗎？$R;" +
                    "$R…$R知道了$R;" +
                    "$R恕我剛才無禮，現在放您走吧，您現在自由了$R;" +
                    "$P混城騎士團成員！$R;" +
                    "$R犯人可能$R還在阿高普路斯市上城的某個地方$R建議暫時封鎖上城的出入吧$R;");
                Say(pc, 11000879, 131, "什麼？$R;" +
                    "那麼那是誰的責任呢？$R;");
                Say(pc, 11000877, 131, "責任？$R;" +
                    "$R維持阿高普路斯的$R治安是我們的責任不是嗎？！$R;");
                Say(pc, 0, 131, "喵！喵！！喵喵喵！$R;" +
                    "$R喵！喵！！喵喵喵！$R;");
                Say(pc, 0, 131, "…貓又哭了…$R;" +
                    "$R！！…是不是知道…凱堤就是犯人$R;");
                return;
            }
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.被詢問犯人的事) &&
                !Neko_04_cmask.Test(Neko_04.被告知去找機器人))
            {
                Say(pc, 11000877, 131, "在和平的阿高普路斯市上城$R居然發生這種事情呀$R;");
                return;
            }
            if (!Neko_04_amask.Test(Neko_04.任務開始))
            {
                Say(pc, 11000877, 131, "在和平的阿高普路斯市上城$R居然發生這種事情呀$R;");
                return;
            }
            if (Neko_04_amask.Test(Neko_04.任務結束))
            {
                Say(pc, 11000877, 131, "在和平的阿高普路斯市上城$R居然發生這種事情呀$R;");
                return;
            }
            Neko_04_cmask.SetValue(Neko_04.被告知襲擊, true);
            Say(pc, 11000877, 131, "回過神了$R;" +
                "$R不知被誰襲擊，在這裡昏過去了$R;" +
                "$R商人總管也一起暈過去了$R;" +
                "$P總之…幸好不是很嚴重阿$R;" +
                "$R雖然不知道為什麼被襲擊$R不知道有沒有丟東西呀？$R;");
            Say(pc, 0, 131, "…!!$R;" +
                "陶瓷盒呢？$R;");
            Say(pc, 11000877, 131, "哎呀！看樣子被偷了$R;" +
                "$R陶瓷盒？只有那個嗎？$R裡面有什麼？$R;" +
                "$P不知道？…$R啊…是受人之託的吧$R;");
            Say(pc, 11000877, 131, "在這和平的阿高普路斯市上城…$R;" +
                "$R真不敢相信在$R阿高普路斯市中心的行會宮殿$R居然發生強盜事件呀$R;" +
                "$P丟了重要的東西，真可惜$R;" +
                "$R在調查現場之前，請先待在這裡吧$R;");
        }
    }
}