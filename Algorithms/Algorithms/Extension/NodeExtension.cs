using Algorithms.DataStruct.Chain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Extension
{
    public static class NodeExtension
    {
        /// <summary>
        /// 删除链表的最后一个节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        public static void DeleteLastNode<T>(this Node<T> node)
        {
            while (node.Next?.Next != null)
                node = node.Next;
            node.Next = null;
        }

        /// <summary>
        /// 删除链表的第n个节点（如果存在的话）
        /// </summary>
        /// <param name="n"></param>
        /// <param name="node">删除后指定元素后的链表</param>
        public static Node<T> Delete<T>(this Node<T> node, int n)
        {
            //var prev = node;
            //while (n-- > 0 && prev.Next != null)
            //    prev = prev.Next;
            //prev.Next = prev.Next.Next;
            //return prev;

            return default;
        }
    }
}
