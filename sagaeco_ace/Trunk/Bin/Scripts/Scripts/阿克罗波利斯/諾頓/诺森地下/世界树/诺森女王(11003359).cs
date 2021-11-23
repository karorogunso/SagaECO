using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:下城(10024000) NPC基本信息:銀(11001172) X:132 Y:97
namespace SagaScript.M80061062
{
    public class S11003370 : Event
    {
        public S11003370()
        {
            this.EventID = 11003359;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<NDFlags> mask = new BitMask<NDFlags>(pc.CMask["ND"]);
            if (!mask.Test(NDFlags.貝爾德咖魯德第一次对话))
            {
                if(!mask.Test(NDFlags.和维尔迪加尔德真身第一次对话))
                {
                    Say(pc, 0, 0, "(欢迎)$R;" +
                            "(远道而来的冒险者)$R;" +
                            "(没记错的话，在地上我们应该见过面了)$R;" +
                            "(虽然这么说很失礼，但你必须赶快离开这里)$R;" +
                            "(请相信我，在这里呆着对你只会有危险)$R;", "维尔迪加尔德的回响");
                        mask.SetValue(NDFlags.和维尔迪加尔德真身第一次对话, true);
                }
                else
                {
                    Say(pc, 0, 0, "(为了你的安全,请尽快离开)$R;", "维尔迪加尔德的回响");
                }
                
            }
            else
            {
                if (!mask.Test(NDFlags.和维尔迪加尔德真身第一次对话))
                {
                    Say(pc, 0, 0, "(初次见面)$R;" +
                            "(远道而来的冒险者)$R;" +
                            "(虽然这么说很失礼，但你必须赶快离开这里)$R;" +
                            "(请相信我，在这里呆着对你只会有危险)$R;", "维尔迪加尔德的回响");
                    mask.SetValue(NDFlags.和维尔迪加尔德真身第一次对话, true);
                }
                else
                {
                    Say(pc, 0, 0, "(为了你的安全,请尽快离开)$R;", "维尔迪加尔德的回响");
                }
                
            }
                     
            switch (Select(pc, "？？？", "", "你是谁", "这里怎么了", "能送我回去吗"))
            {
                case 1:
                    Say(pc, 0, 0, "(我是诺森女王，维尔迪加尔德)$R;" +
                                           "(是这冰封的诺森王国长久)$R;" +
                                           "(以来唯一一个女王)$R;" , "维尔迪加尔德的回响");
                            if (!mask.Test(NDFlags.貝爾德咖魯德第一次对话))
                            {
                                Say(pc, 0, 0, "(我知道你在地上肯定怀疑过我的身份)$R;" +
                                              "(不知道这样的解释和眼前的情景)$R;" +
                                              "(你能理解吗?)$R;", "维尔迪加尔德的回响");
                            }
                                          
                    break;
                case 2:
                    Say(pc, 0, 0, "……$P;" +
                                           "(这一切全是我的错)$R;" +
                                           "(是我亲手毁掉了我的王国)$R;" +
                                           "(当然，也有值得欣慰的事物存在着)$R;" +
                                           "(我的子民，我的后人，仍然顽强地)$R;" +
                                           "(生活在这里，生生不息)$R;", "维尔迪加尔德的回响");
                    Say(pc, 0, 0, "(至于我)$R;" +
                                           "(我到底是不是维尔迪加尔德，其实)$R;" +
                                           "(我也不清楚。严格说，我只是)$R;" +
                                           "(维尔迪加尔德的灵魂碎片之一)$R;" +
                                           "(如果我不是这种魂魄四散的模样的话)$R;" +
                                           "(可能对你的态度会完全不同呢)$P;" +
                                           "(另外，我能感受到这世上，好像)$R;" +
                                           "(还有其他人和我现在的状态类似?)$R;", "维尔迪加尔德的回响");
                    Say(pc, 0, 0, "(这样的天罚)$R;" +
                                           "(说到底其实也是咎由自取)$R;" +
                                           "(毕竟我犯下了那么多罪孽)$R;" +
                                           "(比起来这天罚已是)$R;"+
                                           "(难以置信地宽大了呢)$R;", "维尔迪加尔德的回响");
                    break;
                case 3:
                    int a=0;
	                a=Global.Random.Next(1,19);
                    switch(a)
                    {
                        case 1:
                            GiveItem(pc, 10014300, 1);
                            break;
                        case 2:
                            GiveItem(pc, 10025300, 1);
                            break;
                        case 3:
                            GiveItem(pc, 10013350, 1);
                            break;
                        case 4:
                            GiveItem(pc, 10009150, 1);
                            break;
                        case 5:
                            GiveItem(pc, 10024900, 1);
                            break;
                        case 6:
                            GiveItem(pc, 10050851, 1);
                            break;
                        case 7:
                            GiveItem(pc, 10050852, 1);
                            break;
                        case 8:
                            GiveItem(pc, 50003352, 1);
                            break;
                        case 9:
                            GiveItem(pc, 60001853, 1);
                            break;
                        case 10:
                            GiveItem(pc, 10024900, 1);
                            break;
                        case 11:
                            GiveItem(pc, 10000103,50);
                            break;
                        case 12:
                            GiveItem(pc, 10000102, 50);
                            break;
                        case 13:
                            GiveItem(pc, 10000108, 50);
                            break;
                        case 14:
                            GiveItem(pc, 10022600, 1);
                            break;
                        case 15:
                            GiveItem(pc, 10024500, 1);
                            break;
                        case 16:
                            GiveItem(pc, 10019300, 1);
                            break;
                        case 17:
                            GiveItem(pc, 10035602, 1);
                            break;
                        case 18:
                            GiveItem(pc, 50031300, 1);
                            break;
                        case 19:
                            GiveItem(pc, 10002204, 1);
                            break;
                    }
                    Warp(pc, 10023000, 132, 151);
                    Say(pc, 0, 0, "(远道而来，没能好生招待，请原谅)$R;" +
                                  "(在这里的一切，请你务必保守秘密)$R;" +
                                  "(我的肉体，在这里还有其他的)$R;" +
                                  "(更多的未完成的使命)$R;" +
                                  "(纵然是巧合，我也很高兴有人能来看我)$R;" +
                                  "(请收下吧……)$P;" +
                                  "(……不必担心，我们总有一日)$R;" +
                                  "(还会再见面的……)$R;", "维尔迪加尔德的回响");
                    
                    break;
                case 4:
                    break;
            }
        }
    }
}