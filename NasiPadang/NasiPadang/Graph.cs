using System;
using System.Collections.Generic;

public class Graph
{
    private List<List<string>> adj;// berisi sisi dari graf
    private List<string> nodes; // berisi simpul graf
    private int nNodes;
    private List<(string, string)> listOfEdge;
    // Atribut untuk rekomendasi teman
    private string selectedAccount;
    private List<string> mutualFriends;

    public Graph()
    {
        adj = new List<List<string>>();
        nodes = new List<string>();
        nNodes = 0;
    }
    public void readFromFile(string[] text)
    {
        // Inisiasi
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
                addAdj(node[0], node[1]); // tambah sisi
            }
            i++;
        }

        // assign ke tuple edge
        i = 0;
        listOfEdge = new List<(string, string)>();
        foreach (string line in text)
        {
            if (i != 0)
            {
                string[] node = line.Split();
                var t1 = (node[0], node[1]);
                listOfEdge.Add(t1);
            }
            i++;
        }
    }

    // Menambahkan simpul ke dalam graf
    public bool addNode(string node)
    {
        // Apabila node tidak ada maka append nodes
        // nodes = list simpul
        if (!isExist(node))
        {
            nodes.Add(node);
            return false;
        }
        // Apabila ada maka node ada dan tidak perlu di append
        return true;
    }
    public List<(string, string)> getListOfEdge()
    {
        return listOfEdge;
    }
    public void addAdj(string node, string adjNode)
    {
        bool isNodeExist = addNode(node); // true apabila node sudah ada
        bool isadjNodeExist = addNode(adjNode); // true apabila adjNode sudah ada

        if (!isNodeExist)
        {
            // Apabila sebelumnya belum ada maka assign string list ke representasi graf
            var newNode = new List<string>();
            adj.Add(newNode);
        }

        if (!isadjNodeExist)
        {
            // Apabila sebelumnya belum ada adjNode maka assign string list ke representasi graf
            var newNode = new List<string>();
            adj.Add(newNode);
        }

        // Tambahkan sisi ke tiap graf dari nodes
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] == node)
            {
                adj[i].Add(adjNode);
                adj[i].Sort();
                continue;
            }
            if (nodes[i] == adjNode)
            {
                adj[i].Add(node);
                adj[i].Sort();
                continue;
            }
        }
    }
    bool isExist(string node)
    {
        foreach (string line in this.nodes)
        {
            if (line == node) return true;
        }

        return false;
    }
    public List<string> getNode()
    {
        return nodes;
    }

    public List<List<string>> getAdjNode()
    {
        return adj;
    }

    // Mengembalikan list teman-teman dari node a
    public List<string> friendsOfAccount(string acc)
    {
        List<string> friends = new List<string>();
        
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] == acc)
            {
                foreach(string friend in adj[i])
                {
                    if (friend != acc)
                    friends.Add(friend);
                }
            }
        }

        return friends;
    }

    // Menghitung mutual friend, jalur diperoleh dari BFS atau DFS
    public List<List<string>> sortedMutual(string acc)
    {
        selectedAccount = string.Empty;
        selectedAccount = acc;

        List<string> marked = new List<string>(); // mutual friends yang didapat
        List<List<string>> mutuals = new List<List<string>>(); // kumpulan tetangga acc yang berteman dengan mutual
        //List<int> countMutuals = new List<int>();

        // Cari tetangga acc
        for(int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] == acc)
            {
                // search simpul tetangga dari acc
                for (int j = 0; j < adj[i].Count; j++)
                {
                    // cari tetangga dan temannya dan hitung count
                    for (int k = 0; k < nodes.Count; k++)
                    {
                        if (nodes[k] == adj[i][j])
                        {
                            // traversal si tetangga dan simpuknya
                            for (int l = 0; l < adj[k].Count; l++)
                            {
                                // cek apakah mutual sudah ditambahkan dan node bukan tetangga dari acc dengan mengecek simpul tetangga nodes[i]
                                if (marked.Contains(adj[k][l]) && adj[k][l] != acc && notAdj(nodes[i], adj[k][l]))
                                {
                                    int idx = marked.IndexOf(adj[k][l]); // cari mutual friends ada di indeks ke berapa
                                    //countMutuals[idx]++;
                                    mutuals[idx].Add(nodes[k]);
                                }
                                // apabila mutual belum ditambahkan
                                else if (adj[k][l] != acc && notAdj(nodes[i], adj[k][l]))
                                {
                                    marked.Add(adj[k][l]); // tambahkan ke list mutual
                                    List<string> newMutual = new List<string>(); // deklarasi list baru
                                    newMutual.Add(nodes[k]); // tambahkan simpul tetangga acc
                                    mutuals.Add(newMutual); // tambahkan list
                                    
                                    //countMutuals.Add(1);
                                }
                            }
                        }
                        //break;
                    }
                }
                //break;
            }
        }

        for (int i = 0; i < marked.Count; i++)
        {
            mutuals[i].Insert(0, marked[i]);
        }

        // Sort
        int n = marked.Count;
            
        for (int i = 0; i < n - 1; i++)
        {
            int min_idx = i;
            for (int j = i + 1; j < n; j++) 
            { 
                if (mutuals[j].Count > mutuals[min_idx].Count)
                    min_idx = j;
                List<string> temp = mutuals[min_idx];
                mutuals[min_idx] = mutuals[i];
                mutuals[i] = temp;
            }
        }
        return mutuals;
    }

    // Cek apakah node1 bertetangga dengan node2 based on list node1
    public bool notAdj(string node1, string node2)
    {
        int i = 0;
        foreach(string node in nodes)
        {
            if (node == node1)
            {
                foreach(string friend in adj[i])
                {
                    if (friend == node2)
                    {
                        return false;
                    }
                }
                return true;
            }
            i++;
        }
        return false;
    }
    public List<string> getMutual()
    {
        return mutualFriends;
    }
}
