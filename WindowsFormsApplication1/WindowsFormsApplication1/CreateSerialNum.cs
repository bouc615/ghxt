using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class CreateSerialNum
    {
        /// <summary>
        /// 生成充值流水号格式：8位日期加8位顺序号，如2010030200000056。
        /// </summary>
        public static string GetSerialNumber()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }
    }
}
