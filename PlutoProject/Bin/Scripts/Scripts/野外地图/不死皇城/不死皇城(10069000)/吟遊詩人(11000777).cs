using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069000
{
    public class S11000777 : Event
    {
        public S11000777()
        {
            this.EventID = 11000777;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (_5a67 && !_5a70)
            {
                _5a69 = true;
                Say(pc, 131, "叫畢妮奧的人，很久以前$R;" +
                    "在埃米爾世界旅行$R;" +
                    "是個治療了很多人的傳奇神官$R;" +
                    "$R她為了治療生病了的王妃$R;" +
                    "進到這城裡，可王妃還是死了$R;" +
                    "$P她的死是否和這城的没落$R;" +
                    "有什麽關係呢？$R;" +
                    "$P我是詩人！$R;" +
                    "對我來説真實不重要的…$R;" +
                    "$R想仔細了解，就去行會宫殿問問吧$R;" +
                    "有人對她的死亡很了解唷。$R;");
                return;
            }
            */
            Say(pc, 131, "这里是不死系住的黑暗之城$R;" +
                "$R因为有亡灵在妨碍着$R;" +
                "所以没有地图可以看$R;" +
                "就算进去了，也只会迷路瞎转而已！$R;" +
                "$R快回去吧…$R;");
        }
    }
}