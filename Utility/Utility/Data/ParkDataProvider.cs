using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Data
{
    public class ParkDataProvider : DataProvider<Park>
    {
        public override void Add(string key, Park data)
        {
            base.Add(key, data);
        }

        public override Park Get(string key)
        {
            return base.Get(key);
        }
    }

    public class Park
    {

        #region 公共属性

        /// <summary>
        /// 停车场Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 停车场名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 负责人联系电话
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 总车位
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 已用车位
        /// </summary>
        public int Occupy => Total - Empty;

        /// <summary>
        /// 剩余车位
        /// </summary>
        public int Empty { get; set; }

        #endregion
    }
}
