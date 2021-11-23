﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum PSTFlags
    {
        觀光局職員第一次對話 = 0x1,
        //東部警備犬
        獲得翡翠 = 0x2,
        //農村之泉水
        農村之泉水第一次對話 = 0x4,
        獲得技能點 = 0x8,
        //愛妻便當...
        獲得便當 = 0x10,
        交出便當 = 0x20,
        //咖啡館老闆
        咖啡館老闆第一次任務 = 0x40,
        //马厩,牛牛任务
        給予健康營養飲料 = 0x80,
        尋找香草 = 0x100,
        給予香草 = 0x200,
        尋找甜草 = 0x400,
        給予甜草 = 0x800,
        獲得牛牛的對話 = 0x1000,
        獲得牛牛 = 0x2000,
        //7只小雞
        開始尋找小雞 = 0x4000,
        找到1號小雞 = 0x8000,
        找到2號小雞 = 0x10000,
        找到3號小雞 = 0x20000,
        找到4號小雞 = 0x40000,
        找到5號小雞 = 0x80000,
        找到6號小雞 = 0x100000,
        找到7號小雞 = 0x200000,
        尋找小雞結束 = 0x400000,
        //綠色防備軍本部
        綠盾軍兵士第一次對話 = 0x800000,
    }
}