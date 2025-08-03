using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30069002
{
    public class S20010011 : Event
    {
        public S20010011()
        {
            this.EventID =20010011;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 113, "����~$R;" +
                "$R��ӭ����~$R;");
            switch (Select(pc, "������ʲô����", "", "��Ե�","��..���Ǹ���", "ʲô������"))
            { 
                case 1:
                    OpenShopBuy(pc, 2009);
                    break;
                case 2:
                    OpenShopBuy(pc, 2010);
                    break;
                case 3:
                    break;
            }
            //Say(pc, 113, "����~$R;" +
            //    "$R����ȥ��$R;");
            //switch (Select(pc, "�������Ǹ��𣿡�", "", "��", "����"))
            //{
            //    case 1:
            //        OpenShopBuy(pc, 2010);
            //        //Say(pc, 135, "Ŀǰ��Ϊ����ϵ��δʵװ�����Է���˹����Ʊ������ʱ����~$R;");
            //        break;
            //    case 2:
            //        break;
            //}
        }
    }
}