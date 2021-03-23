using System;
using System.Collections.Generic;

namespace NasiPadang
{
    static class Graph
    {
        private List<List<string>> adj;
        private List<string> nodes;
        private int nNodes;
        //List<string> node;
        //Dictionary<string, Queue<string>>;

        void readFromFile(string[] text)
        {
            int i = 0;
            foreach (string line in text)
            {
                if (i == 0)
                {
                    this.nNode = Int16.Parse(line);
                }
                else
                {
                    string[] node = line.Split();
                    addAdj(node[0], node[1]);
                }
                i++;
            }
        }

        void addNode(string node)
        {
            if (!isExist(node))
            {
                nodes.Add(node);
            }
            return;
        }

        void addAdj(string node, string adjNode)
        {
            addNode(node); // in case node belum ada

            for (int i = 0; i < nodes.Capacity; i++)
            {
                if (nodes[i] == node)
                {
                    adj[i].Add(adjNode);
                    adj[i].Sort();
                    return;
                }
            }
        }
        bool isExist(string node)
        {
            foreach (line in this->nodes)
            {
                if (line == node) return true;
            }

            return false;
        }
    }
}
