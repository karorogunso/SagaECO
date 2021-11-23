using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:咖啡館店員(11000965) X:105 Y:136
namespace SagaScript.M10025001
{
    public class S11000965 : Event
    {
        public S11000965()
        {
            this.EventID = 11000965;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000965, 131, "您好!$R;" +
                                   "$R我是「阿克罗波利斯」的「下城」的$R;" +
                                   "「酒馆」的店员!$R;" +
                                   "$P我是来这里宣传的!$R;" +
                                   "$R那开始宣传吧!!!$R;" +
                                   "$P欢迎光临!$R;" +
                                   "欢迎光临!!$R;" +
                                   "$R世界上对初心者最亲切的酒馆，$R;" +
                                   "在「阿克罗波利斯」的「下城」东边阶梯下面唷!$R;" +
                                   "$P在酒馆里除了出售饮料外，$R;" +
                                   "也有介绍工作的(任务服务台)!!$R;" +
                                   "$P虽然任务服务台，$R;" +
                                   "只提供介绍任务的服务，$R;" +
                                   "但「阿克罗波利斯」周边的，$R;" +
                                   "东、南、西、北四个平原里都有分店哦!$R;" +
                                   "希望多多光顾呀!!!$R;", "酒馆店员");
        }
    }
}
