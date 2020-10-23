using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.UnionFind
{
    /// <summary>
    /// 最基础的并查集数据结构
    /// </summary>
    public class QuickFind
    {
        private readonly int[] _ids;
        private int _count;

        public QuickFind(int count)
        {
            _count = count;
            _ids = Enumerable.Range(0, count).ToArray();
        }

        public void Union(int p, int q)
        {
            var pid = _ids[p];
            var qid = _ids[q];

            if (pid == qid) return;

            for (int i = 0; i < _ids.Length; i++)
                if (_ids[i] == pid) _ids[i] = qid;
            _count--;
        }

        public bool Connected(int p, int q)
            => _ids[p] == _ids[q];
    }
}
