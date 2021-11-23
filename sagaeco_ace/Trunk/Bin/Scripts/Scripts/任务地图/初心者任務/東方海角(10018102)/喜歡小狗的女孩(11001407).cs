using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:喜歡小狗的女孩(11001407) X:201 Y:90
namespace SagaScript.M10018102
{
    public class S11001407 : Event
    {
        public S11001407()
        {
            this.EventID = 11001407;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            //尚未插入♪表情
            Say(pc, 11001407, 0, "太可爱了~♪$R;" +
                                 "$R您找我有什么事情吗?$R;", "喜欢小狗的女孩");

            switch (Select(pc, "在看什么?", "", "那「♪」是…?", "没什么"))
            {
                case 1:
                    Say(pc, 11001407, 0, "这是「表情」哦!$R;" +
                                         "$R打开「表情」视窗，$R;" +
                                         "就可以简单地使用。$R;" +
                                         "$P旁边较大的聊天用视窗$R;" +
                                         "看到了吗?$R;" +
                                         "$R想要打开「表情」视窗，$R;" +
                                         "就点击这个视窗右边的$R;" +
                                         "笑脸图示。$R;" +
                                         "$P在「表情」和「动作」视窗，$R;" +
                                         "双击要使用的图示即可。$R;" +
                                         "$P很简单吧~♪$R;" +
                                         "$R跟朋友说话时，$R;" +
                                         "使用各种表情$R;" +
                                         "会更有趣的哦!$R;", "喜欢小狗的女孩");
                    break;
                    
                case 2:
                    break;
            }
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11001407, 0, "太可爱了!!$R;" +
                                 "$R(…好像完全无视周围的情況。)$R;", "喜欢小狗的女孩");
        }  
    }
}
