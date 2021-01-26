using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Utility.Data
{
    /// <summary>
    /// 数据提供者
    /// </summary>
    public class DataProvider<TData>
    {
        #region 单例

        /// <summary>
        /// 线程同步锁
        /// </summary>
        private static readonly object _objLock = new object();

        /// <summary>
        /// 单例
        /// </summary>
        private static DataProvider<TData> _instance;

        /// <summary>
        /// 单例
        /// </summary>
        public static DataProvider<TData> Instance
        {
            get
            {
                if (_instance == null)
                    lock (_objLock)
                        if (_instance == null)
                            _instance = new DataProvider<TData>();

                return _instance;
            }
        }

        #endregion

        #region 私有字段

        /// <summary>
        /// 数据中心
        /// </summary>
        private readonly ConcurrentDictionary<string, TData> _data = new ConcurrentDictionary<string, TData>();

        #endregion

        #region 公共方法

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="key">唯一标识</param>
        /// <param name="data">数据</param>
        public virtual void Add(string key, TData data)
            => _data[key] = data;

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key">唯一标识</param>
        /// <returns>如果含有对应key的数据，则返回该数据，否则返回空</returns>
        public virtual TData Get(string key)
        {
            _data.TryGetValue(key, out var data);
            return data;
        }

        #endregion
    }
}
