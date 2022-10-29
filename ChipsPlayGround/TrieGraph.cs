using System.Collections.Generic;

namespace Coding
{
    public class TrieGraph
    {
        public class Node
        {
            private Dictionary<char, Node> _children = new Dictionary<char, Node>();

            public void Add(string s)
            {
                Add(s, 0);
            }

            public int GetCount(string s)
            {
                return GetNode(s, 0)?.Weight ?? 0;
            }

            private void Add(string s, int index)
            {
                if (index == s.Length) return;

                var c = s[index];

                if (!_children.ContainsKey(c))
                {
                    _children.Add(c, new Node());
                }
                else
                {
                    _children[c].Weight += 1;
                }

                _children[c].Add(s, index + 1);
            }

            private Node GetNode(string s, int index)
            {
                if (index == s.Length) return this;

                var c = s[index];

                if (_children.ContainsKey(c))
                {
                    return _children[c].GetNode(s, index + 1);
                }
                else
                {
                    return null;
                }
            }

            public int Weight { get; set; } = 1;
        }

        public static class Solution
        {
            public static List<int> Run()
            {
                return Run1();
            }

            public static List<int> Run0()
            {
                var result = new List<int>();
                var node = new Node();

                node.Add("ed");
                node.Add("eddie");
                node.Add("edward");
                result.Add(node.GetCount("ed"));

                node.Add("edwina");
                result.Add(node.GetCount("edw"));
                result.Add(node.GetCount("a"));

                return result;
            }

            public static List<int> Run1()
            {
                var result = new List<int>();
                var node = new Node();

                node.Add("hack");
                node.Add("hackerrank");
                result.Add(node.GetCount("hac"));
                result.Add(node.GetCount("hak"));

                return result;
            }
        }
    }
}
