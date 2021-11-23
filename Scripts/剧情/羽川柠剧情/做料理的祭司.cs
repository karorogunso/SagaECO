
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public partial class S60000014: Event
    {
        void 羽川柠剧情1(ActorPC pc, BitMask<羽川柠> mark)
        {
            if(mark.Test(羽川柠.初次对话) && !mark.Test(羽川柠.和兔纸对话))
            {
                ChangeMessageBox(pc);
                Say(pc, 131, "找窝有什么事吖？", "做料理的祭司");
                if(Select(pc," ","","那个...那边有个人在发神经")== 1)
                {
                    Say(pc, 131, "哎？$R$R窝望过去看看..", "做料理的祭司");
                    Say(pc, 131, "啊...真的诶！！$R$R但是那个人从来没见过呢..$R会是谁呢？", "做料理的祭司");
                    Say(pc, 60000009,131, "嘿嘿嘿，$R那个人和番茄长得好像哦$R$R是不是你的私生子呢？", "做料理的元素使");
                    Say(pc, 60000013, 131, "吓哭！！！$R$R仔细一看..还真的耶，$R但她怎么可能是我的私生子呢！", "做料理的番茄");
                    Say(pc, 131, "那个...$R她一直在那里发神经$R会不会影响市容了！$R$R不管管她真的好吗？", "做料理的祭司");
                    Say(pc, 60000009, 131, "给她吃番茄制作的『土豆烧牛肉』！$R包治百病呀！！", "做料理的元素使");
                    Say(pc, 60000013, 131, "诶诶诶？？$R我的料理有这种神奇的功效吗！？$R$R我都不知道耶！！", "做料理的番茄");
                    Say(pc, 60000009, 131, "有的有的！", "做料理的元素使");
                    Say(pc, 131, "对吖对吖！$R$R番茄泥快把泥刚刚做好的『土豆烧牛肉』$R拿去给她次吖！", "做料理的祭司");
                    Say(pc, 60000013, 131, "真、真的吗！？$R好..好开心哦！$R但..我有点不好意思拿过去呢..", "做料理的番茄");
                    Say(pc, 131, "那就让 " + pc.Name + " 拿过去吧！", "做料理的祭司");
                    Say(pc, 0, "得到了番茄做好的『土豆烧牛肉』$R$R不对...这颜色...怎么看起来都不像是『土豆烧牛肉』啊...");
                    Say(pc, 0, "感觉上应该是得到了『黑暗料理』");
                    Say(pc, 0, "一股很不舒服的感觉涌了上来...");
                    Say(pc, 60000013, 131, "那...那就麻烦你了哦，$R把我做好的『土豆烧牛肉』$R拿去给那边的小妹妹吃吧。", "做料理的番茄");
                    GiveItem(pc, 110123200, 1);
                    mark.SetValue(羽川柠.和兔纸对话, true);
                    return;
                }
            }
        }
    }
}