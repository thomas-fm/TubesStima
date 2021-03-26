using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DepthFirst
{
    class Program
    {
        public class Graph
        {
            public List<List<string>> adj { get; set; }
            public List<string> nodes { get; set ; }
            public int nNodes { get; set; }
            public int nPairs { get; set; }
            public Graph(List<List<string>> adj)
            {
                this.adj = adj;
                this.nodes = new List<String>();
                int i = 0;
                int j = 0;
                foreach(List<string> pair in adj) {
                    foreach(string node in pair)
                        if (!this.nodes.Contains(node)) {
                            this.nodes.Add(node);
                            i++;
                        }
                    j++;
                }
                this.nNodes = i;
                this.nPairs = j;
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
                foreach (string line in this.nodes)
                {
                    if (line == node) return true;
                }

                return false;
            }
        }

        public class DepthFirstSearch
        {
            public string Search(Graph graphInput, string nameToSearchFor)
            {
                Queue<string> AntrianNode = new Queue<string>();
                HashSet<string> Dikunjungi = new HashSet<string>();
                AntrianNode.Enqueue(graphInput.adj[0][0]);
                Dikunjungi.Add(graphInput.adj[0][0]);

                while (AntrianNode.Count > 0)
                {
                    string e = AntrianNode.Dequeue();
                    if (e == nameToSearchFor)
                        return e;
                    foreach (string friend in graphInput.adj[0])
                    {
                        if (!Dikunjungi.Contains(friend))
                        {
                            AntrianNode.Enqueue(friend);
                            Dikunjungi.Add(friend);
                        }
                    }
                }
                return "Tidak ditemukan";
            }

            public int SearchNodeIndex(Graph graphInput, string nodeToSearch) {
                int i = 0;
                while (graphInput.adj[i][0] != nodeToSearch) {
                    i++;
                }
                return i;
            }

            public List<List<string>> DFS(Graph graphInput, string startingNode) 
            {
                Stack<string> nodeBlmKelar = new Stack<string>();
                Stack<string> AntrianNode = new Stack<string>();
                List<string> urutanDFS = new List<string>();
                List<List<string>> nodeByLevel = new List<List<string>>();

                HashSet<string> Dikunjungi = new HashSet<string>();

                int indexCurrentNode = SearchNodeIndex(graphInput, startingNode);
                bool adaYangBelum;
                int level = 0;

                urutanDFS.Add(startingNode);
                nodeBlmKelar.Push(startingNode);
                AntrianNode.Push(startingNode);
                Dikunjungi.Add(startingNode);

                //Nambahin ke list akhir
                nodeByLevel.Add(new List<string>(urutanDFS));
                urutanDFS.Clear();

                while(AntrianNode.Count() > 0) {
                    adaYangBelum = false;
                    string e = AntrianNode.Pop();
                    indexCurrentNode = SearchNodeIndex(graphInput, e);

                    foreach(string nodeTetangga in graphInput.adj[indexCurrentNode]) {
                        if(!Dikunjungi.Contains(nodeTetangga)) {
                            //Debug
                            level++;
                            urutanDFS.Add(nodeTetangga);
                            nodeBlmKelar.Push(nodeTetangga);
                            AntrianNode.Push(nodeTetangga);
                            Dikunjungi.Add(nodeTetangga);
                            adaYangBelum = true;

                            //Masukin ke hasil akhir
                            if (nodeByLevel.Count()<=level) {
                                nodeByLevel.Add(new List<string>(urutanDFS));
                            } else {
                                nodeByLevel[level].Add(nodeTetangga);
                            }
                            urutanDFS.Clear();
                            break;
                        }
                    }

                    if(!adaYangBelum) {
                        level--;
                        nodeBlmKelar.Pop();
                        if (nodeBlmKelar.Count() > 0) {
                            AntrianNode.Push(nodeBlmKelar.Peek());
                        }
                    }
                }
                

                return nodeByLevel;
            }

            public List<List<string>> ShortestPathToNode(Graph graphInput, string startingNode, string endNode) // ini kalau Graph udah bener
            {
                Stack<string> nodeBlmKelar = new Stack<string>();
                Stack<string> AntrianNode = new Stack<string>();
                List<string> urutanDFS = new List<string>();
                List<List<string>> nodeByLevel = new List<List<string>>();

                HashSet<string> Dikunjungi = new HashSet<string>();

                int indexCurrentNode = SearchNodeIndex(graphInput, startingNode);
                string nodeTerakhirLevel = startingNode;
                bool adaYangBelum;
                int level = 0;

                urutanDFS.Add(startingNode);
                nodeBlmKelar.Push(startingNode);
                AntrianNode.Push(startingNode);
                Dikunjungi.Add(startingNode);

                //Nambahin ke list akhir
                nodeByLevel.Add(new List<string>(urutanDFS));
                urutanDFS.Clear();

                while(AntrianNode.Count() > 0) {
                    adaYangBelum = false;
                    string e = AntrianNode.Pop();
                    indexCurrentNode = SearchNodeIndex(graphInput, e);

                    foreach(string nodeTetangga in graphInput.adj[indexCurrentNode]) {
                        if(!Dikunjungi.Contains(nodeTetangga)) {
                            //Debug
                            level++;
                            urutanDFS.Add(nodeTetangga);
                            nodeBlmKelar.Push(nodeTetangga);
                            AntrianNode.Push(nodeTetangga);
                            Dikunjungi.Add(nodeTetangga);
                            nodeTerakhirLevel = nodeTetangga;
                            adaYangBelum = true;

                            //Masukin ke hasil akhir
                            if (nodeByLevel.Count()<=level) {
                                nodeByLevel.Add(new List<string>(urutanDFS));
                            } else {
                                nodeByLevel[level].Add(nodeTetangga);
                            }
                            break;
                        }
                    }

                    if(!adaYangBelum) {
                        level--;
                        nodeBlmKelar.Pop();
                        if (nodeBlmKelar.Count() > 0) {
                            AntrianNode.Push(nodeBlmKelar.Peek());
                        }
                    }
                    if (nodeTerakhirLevel == endNode) {
                        break;
                    }
                    urutanDFS.Clear();
                }

                Stack<string> Pathnya = new Stack<string>();
                bool found;
                nodeTerakhirLevel = endNode;
                Pathnya.Push(nodeTerakhirLevel);
                int i = nodeByLevel.Count()-2;

                while (i > 0) {
                    foreach(string node1 in nodeByLevel[i]){
                        found = false;
                        indexCurrentNode = SearchNodeIndex(graphInput, node1);
                        foreach(string node2 in graphInput.adj[indexCurrentNode]) {
                            if (node2 == nodeTerakhirLevel) {
                                Pathnya.Push(node1);
                                nodeTerakhirLevel = node1;
                                found = true;
                                break;
                            }
                        }
                        if (found) {
                            break;
                        }
                    }
                    i--;
                }
                Pathnya.Push(startingNode);
                nodeByLevel.Clear();
                urutanDFS.Clear();
                while(Pathnya.Count() > 0) {                    
                    urutanDFS.Add(Pathnya.Pop());
                    if(!cekAdaBug(nodeByLevel, urutanDFS))
                    {
                        nodeByLevel.Add(new List<string>(urutanDFS));
                    }
                    
                    urutanDFS.Clear();
                }


                return nodeByLevel;   
            }

            public bool cekAdaBug(List<List<string>> container, List<string> bug) {
                int i = 0;
                while (i < container.Count()) {
                    if(container[i][0] == bug[0]) {
                        return true;
                    }
                    i++;
                }
                return false;
            }

            public void PrintAllPath(List<List<string>> AllPath)
            {

                int currentLevel = 0;
                bool stop = false;

                foreach(List<string> level in AllPath) {
                    foreach(string nodePerLevel in level) {
                        if(!stop) {
                            Console.WriteLine("Level: {0}", currentLevel);
                            stop = true;
                        }
                        Console.WriteLine(nodePerLevel);
                    }
                    if (!stop) {
                        break;
                    }else {
                        stop = false;
                    }
                    currentLevel++;
                } 
            }

            public void PrintShortestPath(List<List<string>> ShortestPath) {
                int i = ShortestPath.Count()-1;
                Console.Write("Jarak terpendek dari node {0} ke node {1} adalah: ", ShortestPath[0][0], ShortestPath[i][0]);
                i = 1;
                while (i <= ShortestPath.Count()) {
                    if (i == ShortestPath.Count()) {
                        Console.WriteLine(ShortestPath[i-1][0]);
                    }
                    Console.Write("{0}-",ShortestPath[i-1][0]);
                    i++;
                }
            }

        }

        static void Main(string[] args)
        {
            // var satu = new List<string> { "1", "2","3"};
            // var dua = new List<string> { "2","1","4"};
            // var tiga = new List<string> { "3", "1","5","6"};
            // var empat = new List<string> { "4", "2","7"};
            // var lima = new List<string> { "5","3","6","7","8"};
            // var enam = new List<string> { "6", "3","5"};
            // var tujuh = new List<string> { "7", "4","5"};
            // var delapan = new List<string> { "8", "5","9","10"};
            // var sembilan = new List<string> { "9","8","10"};
            // var sepuluh = new List<string> { "10", "8","9"};
            // var test = new List<List<string>> {satu,dua, tiga,empat,lima,enam,tujuh,delapan,sembilan,sepuluh};
        

            // Graph testGraph = new Graph(test);

            var satu = new List<string> { "A", "B","C","D"};
            var dua = new List<string> { "B","A","C","E","F"};
            var tiga = new List<string> { "C", "A","B","F","G"};
            var empat = new List<string> { "D", "A","F","G"};
            var lima = new List<string> { "E","B","F","H"};
            var enam = new List<string> { "F","B", "C","D","E","H"};
            var tujuh = new List<string> { "G", "C","D"};
            var delapan = new List<string> { "H", "E","F"};
            var test2 = new List<List<string>> {satu,dua, tiga,empat,lima,enam,tujuh,delapan};

            Graph ujiShortPathGraph = new Graph(test2);

            DepthFirstSearch d = new DepthFirstSearch();
            // List<List<string>> hasilPath = b.BFS(testGraph, "1");
            // // b.PrintAllPath(hasilPath);

            // Console.WriteLine("Uji Path:");
            // hasilPath = b.ShortestPathToNode(testGraph,"1","8");
            // b.PrintShortestPath(hasilPath);
            // Console.WriteLine(b.Search(testGraph, "c"));

            List<List<string>> cek = d.DFS(ujiShortPathGraph,"F");
            d.PrintAllPath(cek);

            cek = d.ShortestPathToNode(ujiShortPathGraph,"F", "H");
            d.PrintShortestPath(cek);
            

        }
    }
}