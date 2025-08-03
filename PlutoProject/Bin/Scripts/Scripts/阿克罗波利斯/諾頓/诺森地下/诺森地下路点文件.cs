using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    //原始地圖:20212000	诺森地下·世界树
    //目標地圖:20211000	诺森地下·冰洞
    //目標坐標:
    public class P10002169 : RandomPortal
    {
        public P10002169()
        {
            Init(10002169, 20211000, 96,42, 96,42);
        }
    }

    //原始地圖:20211000	诺森地下·冰洞
    //目標地圖:20212000	诺森地下·世界树
    //目標坐標:
    public class P10002168 : RandomPortal
    {
        public P10002168()
        {
            this.EventID = 10002168;
            //Init(10002168, 20212000, 102, 125, 102,125);
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 0, 0, "！！！！！！！！！！！$P;" +
                          "这不是这个世界的感觉$R;" +
                          "这种恐怖的知识和存在...$P;" +
                          "感受到了非常强大而恐怖的力量$R;");
            Say(pc, 0, 0, "如果再往前走,恐怕无法保证安全$R;");
            switch (Select(pc, "要怎么办呢?", "", "勇往直前!!", "赶快逃走吧!!"))
            {
                case 1:
                    Warp(pc, 20212000, 102, 125);
                    break;
                case 2:
                    break;
            }
            //Init(10002188, 10065000, 24, 43, 24, 43);
        }
    }



    //原始地圖:20211000	诺森地下·冰洞
    //目標地圖:20210000	诺森地下·市民街道
    //目標坐標:
    public class P10002164 : RandomPortal
    {
        public P10002164()
        {
            Init(10002164, 20210000, 22, 1, 22, 1);
        }
    }


    //原始地圖:20210000	诺森地下·市民街道
    //目標地圖:20211000	诺森地下·冰洞
    //目標坐標:
    public class P10002167 : RandomPortal
    {
        public P10002167()
        {
            Init(10002167, 20211000, 22, 248, 22, 248);
        }
    }

    //原始地圖:20211000	诺森地下·冰洞
    //目標地圖:20210000	诺森地下·市民街道
    //目標坐標:
    public class P10002165 : RandomPortal
    {
        public P10002165()
        {
            Init(10002165, 20210000, 232, 3, 232, 3);
        }
    }


    //原始地圖:20210000	诺森地下·市民街道
    //目標地圖:20211000	诺森地下·冰洞
    //目標坐標:
    public class P10002166 : RandomPortal
    {
        public P10002166()
        {
            Init(10002166, 20211000, 232, 251, 232, 251);
        }
    }



    //原始地圖:20210000	诺森地下·市民街道
    //目標地圖:20015001	诺森宫殿秘密通道
    //目標坐標:
    public class P10002187 : RandomPortal
    {
        public P10002187()
        {
            Init(10002187, 20015001, 10, 58, 10, 58);
        }
    }


    //原始地圖:20015001	诺森宫殿秘密通道
    //目標地圖:20210000	诺森地下·市民街道
    //目標坐標:
    public class P10002186 : RandomPortal
    {
        public P10002186()
        {
            Init(10002186, 20210000, 149, 250, 149, 250);
        }
    }


    //原始地圖:20015001	诺森宫殿秘密通道
    //目標地圖:10065000	诺森
    //目標坐標:
    public class P10002188 : Event
    {
        public P10002188()
        {
            this.EventID = 10002188;
        }
        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 0, 0, "看不清楚外面是什么$R;" +
                          "好像有人看守?$R;");
            switch (Select(pc, "要怎么办呢?", "", "偷偷溜出去", "往反方向探索看看"))
            {
                case 1:
                    Warp(pc, 10065000, 24, 43);
                    break;
                case 2:
                    break;
            }
            //Init(10002188, 10065000, 24, 43, 24, 43);
        }
    }

}