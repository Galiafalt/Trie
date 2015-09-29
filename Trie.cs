using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.IO;

namespace TextAutocomplite
{
    public class WordListItem
    {
        public String word;
        public int freq;

        public WordListItem(String str)
        {
            var parts = str.Split(' ');
            word = parts[0];
            if (parts.Length > 1) freq = int.Parse(parts[1]);
        }
    }

    public class Node
    {
        public char NodeKey { get; set; }
        public Dictionary<int, Node> Childs { get; set; }
        public Dictionary<String, int> Words { get; set; }

        public Node()
        {
            Words = new Dictionary<String, int>();
        }
    }
    
    public class Trie
    {
        private Node root;

        public Trie()
        {
            root = new Node() { NodeKey = ' '};
        }

        public void Add(String word)
        {
            Node current = root;
            Node tmp = null;

            var parts = word.Split(' ');
            foreach (var ch in parts[0])
            {
                if (current.Childs == null) { current.Childs = new Dictionary<int, Node>(); }
                if (!current.Childs.Keys.Contains(ch))
                {
                    tmp = new Node();
                    tmp.NodeKey = ch;
                    tmp.Words.Add(parts[0], int.Parse(parts[1]));
                    current.Childs.Add(ch, tmp);
                }
                else
                {
                    int Sequence = int.Parse(parts[1]);
                    if (current.Words.Count < 10)
                    {
                        current.Words.Add(parts[0], Sequence);
                    }
                    else
                    {
                        int MinValue = current.Words.Values.Min();
                        if (MinValue < Sequence)
                        {
                            current.Words.Remove(current.Words.Where(r => r.Value == MinValue).OrderByDescending(r => r.Key).Select(r => r.Key).First());
                            current.Words.Add(parts[0], Sequence);
                        }
                    }
                }
                current = current.Childs[ch];
            }
        }

        public String[] GetSeg(String str)
        {
            Node current = root;
            String[] nullres = new String[0];
            foreach (var ch in str)
            {
                if (current.Childs == null)
                { return nullres; }
                else
                {
                    if (current.Childs.Keys.Contains(ch))
                        current = current.Childs[ch];
                }
            }
            return current.Words.OrderByDescending(r => r.Value).ThenBy(r => r.Key).Select(r => r.Key).ToArray();
        }
    }
}
