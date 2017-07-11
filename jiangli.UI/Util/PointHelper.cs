using System;
using System.Collections.Generic;
using System.Text;

namespace jiangli.Util
{

    public static class PointHelper
    {
        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToListString(this List<Point> list)
        {
            StringBuilder result = new StringBuilder("[");
            foreach (var item in list)
            {
                result.Append(item.ToString());
                result.Append(",");
            }
            result.Remove(result.Length - 1, 1);
            result.Append("]");
            return result.ToString();
        }
    }
    [Serializable]
    public class Point
    {
        /// <summary>
        /// 赔付金额
        /// </summary>
        public double x { get; set; }
        /// <summary>
        /// 满意度
        /// </summary>
        public double y { get; set; }
        public override string ToString()
        {
            return "[" + x + "," + y + "]";
        }
    }
}