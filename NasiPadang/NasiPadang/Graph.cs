using System;
using System.Collections.Generic;

public class Graph
{
    private List<List<string>> adj;
    private List<string> nodes;
    private int nNodes;
    //List<string> node;
    //Dictionary<string, Queue<string>>;
    public Graph()
    {
        adj = new List<List<string>>();
        nodes = new List<string>();
        nNodes = 0;
    }
    public void readFromFile(string[] text)
    {
        adj = new List<List<string>>();
        nodes = new List<string>();
        nNodes = 0;

        int i = 0;
        foreach (string line in text)
        {
            if (i == 0)
            {
                this.nNodes = Int16.Parse(line);
            }
            else
            {
                string[] node = line.Split();
                addAdj(node[0], node[1]);
            }
            i++;
        }
    }

    public void addNode(string node)
    {
        if (!isExist(node))
        {
            nodes.Add(node);
        }
        return;
    }

    public void addAdj(string node, string adjNode)
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
        

        return false;
    }
}
