using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Neko_03
    {
        //凱堤(堇子)
        堇子任務開始 = 0x1,//Xb21
        接受下城的優秀阿姨給予的任務 = 0x2,//7A29
        堇子任務完成 = 0x4,//Xb22
        與初心者學校老師對話 = 0x8,//_7A30
        與商人之家的瑪莎對話 = 0x10,//_7A32
        與飛空艇的瑪莎對話 = 0x20, //_7A34
        與飛空艇的桃子對話 = 0x40,//_7A35
        與鬼斬破多加對話 = 0x80,//_7A36
        找到理路 = 0x100,//_7A39
        使用了電晶體 = 0x200,//_7A40
        帶理路離開 = 0x400, //_7A41
        再次與破多加對話 = 0x800,//_7A42
        得到堇子 = 0x1000,//_7A44

        發現機器人 = 0x2000, //_7A45
    }
}
