using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreadthFirst
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

        public class BreadthFirstSearch
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

            public List<List<string>> BFS(Graph graphInput, string startingNode) // ini kalau Graph udah bener
            {
                // Queue<string> BFSOrder = new Queue<string>();

                Queue<string> AntrianNode = new Queue<string>();
                HashSet<string> Dikunjungi = new HashSet<string>();


                int indexCurrentNode = SearchNodeIndex(graphInput, startingNode);
                string nodeTerakhirLevel;
                Queue<string> daftarNodeTerakhirLevel = new Queue<string>();

                AntrianNode.Enqueue(graphInput.adj[indexCurrentNode][0]);
                Dikunjungi.Add(graphInput.adj[indexCurrentNode][0]);
                List<List<string>> AntrianNodeLevel = new List<List<string>>();
                List<string> TempAntrian = new List<string>();
                TempAntrian.Add(graphInput.adj[indexCurrentNode][0]);
                AntrianNodeLevel.Add(new List<string>(TempAntrian));
                nodeTerakhirLevel = graphInput.adj[indexCurrentNode][0];
                daftarNodeTerakhirLevel.Enqueue(nodeTerakhirLevel);
                TempAntrian.Clear();

                int i = 0;
                while (AntrianNode.Count > 0)
                {
                    string e = AntrianNode.Dequeue();
                    indexCurrentNode = SearchNodeIndex(graphInput, e);
                    // BFSOrder.Enqueue(e);

                    foreach(string nodeTetangga in graphInput.adj[indexCurrentNode])
                    {
                        if (!Dikunjungi.Contains(nodeTetangga)) {
                            AntrianNode.Enqueue(nodeTetangga);
                            TempAntrian.Add(nodeTetangga);
                            Dikunjungi.Add(nodeTetangga);
                            nodeTerakhirLevel = nodeTetangga;
                        }
                    }


                    if (TempAntrian.Count > 0 && daftarNodeTerakhirLevel.Peek() == e ) { //Tadi ngebug, makanya dikasih if ini
                        daftarNodeTerakhirLevel.Dequeue();
                        daftarNodeTerakhirLevel.Enqueue(nodeTerakhirLevel);
                        AntrianNodeLevel.Add(new List<string>(TempAntrian));
                        TempAntrian.Clear();
                    }
                    if (i < graphInput.nPairs) {
                        i++;
                    }
                }

                // while (BFSOrder.Count > 0)
                // {
                //     string e = BFSOrder.Dequeue();
                //     Console.WriteLine(e);
                // }

                return AntrianNodeLevel;
            }

            public List<List<string>> ShortestPathToNode(Graph graphInput, string startingNode, string endNode) // ini kalau Graph udah bener
            {
                
                // Queue<string> BFSOrder = new Queue<string>();

                Queue<string> AntrianNode = new Queue<string>();
                HashSet<string> Dikunjungi = new HashSet<string>();


                int indexCurrentNode = SearchNodeIndex(graphInput, startingNode);
                string nodeTerakhirLevel;
                Queue<string> daftarNodeTerakhirLevel = new Queue<string>();

                AntrianNode.Enqueue(graphInput.adj[indexCurrentNode][0]);
                Dikunjungi.Add(graphInput.adj[indexCurrentNode][0]);
                List<List<string>> AntrianNodeLevel = new List<List<string>>();
                List<string> TempAntrian = new List<string>();
                TempAntrian.Add(graphInput.adj[indexCurrentNode][0]);
                AntrianNodeLevel.Add(new List<string>(TempAntrian));
                nodeTerakhirLevel = graphInput.adj[indexCurrentNode][0];
                daftarNodeTerakhirLevel.Enqueue(nodeTerakhirLevel);
                TempAntrian.Clear();

                int i = 0;
                if (startingNode == endNode) {
                    return AntrianNodeLevel;
                    // List<string> temp = new List<string>(e);
                    // List<List<string>> sama = new List<List<string>>(temp);
                    // return sama;
                }
                while (AntrianNode.Count > 0)
                {
                    string e = AntrianNode.Dequeue();
                    indexCurrentNode = SearchNodeIndex(graphInput, e);
                    // BFSOrder.Enqueue(e);

                    foreach(string nodeTetangga in graphInput.adj[indexCurrentNode])
                    {
                        if (!Dikunjungi.Contains(nodeTetangga)) {
                            AntrianNode.Enqueue(nodeTetangga);
                            TempAntrian.Add(nodeTetangga);
                            Dikunjungi.Add(nodeTetangga);
                            nodeTerakhirLevel = nodeTetangga;
                            if (nodeTetangga == endNode) {
                                break;
                            }
                        }
                    }
                    


                    if ((TempAntrian.Count > 0 && daftarNodeTerakhirLevel.Peek() == e) || nodeTerakhirLevel == endNode ) { //Tadi ngebug, makanya dikasih if ini
                        daftarNodeTerakhirLevel.Dequeue();
                        daftarNodeTerakhirLevel.Enqueue(nodeTerakhirLevel);
                        AntrianNodeLevel.Add(new List<string>(TempAntrian));
                        TempAntrian.Clear();
                    }
                    if (i < graphInput.nPairs) {
                        i++;
                    }

                    if (nodeTerakhirLevel == endNode) {
                        break;
                    }
                }

                Stack<string> Pathnya = new Stack<string>();
                bool found;
                Pathnya.Push(nodeTerakhirLevel);
                i = AntrianNodeLevel.Count()-2;

                // Console.WriteLine("Nilai i: {0}", i);
                while (i > 0) {
                    foreach(string node1 in AntrianNodeLevel[i]){
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
                // Console.WriteLine("Ini nilai si node {0}", Pathnya.Pop());
                AntrianNodeLevel.Clear();
                TempAntrian.Clear();
                while(Pathnya.Count() > 0) {
                    // Console.WriteLine("Masuk Pathnya");
                    TempAntrian.Add(Pathnya.Pop());
                    AntrianNodeLevel.Add(new List<string>(TempAntrian));
                    TempAntrian.Clear();
                }

                // while (BFSOrder.Count > 0)
                // {
                //     string e = BFSOrder.Dequeue();
                //     Console.WriteLine(e);
                // }

                return AntrianNodeLevel;
            }

            public void PrintAllPath(List<List<string>> AllPath)
            {
                // Console.WriteLine("Yang ini versi satunya");

                int currentLevel = 0;
                bool stop = false;

                // Console.WriteLine(AllPath.Count());
                foreach(List<string> level in AllPath) {
                    // Console.WriteLine("Level: {0}", currentLevel);
                    foreach(string nodePerLevel in level) {
                        if(!stop) {
                            Console.WriteLine("Level: {0}", currentLevel);
                            stop = true;
                        }
                        Console.WriteLine(nodePerLevel);
                    }
                    if (!stop) {
                        //UPDATE: Kayanya udah ga perlu
                        break; // ini masih ngebug. kalau ga dibreak dia bakal looping terus karena AntrianNodeLevelnya masih ngebug
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

            BreadthFirstSearch b = new BreadthFirstSearch();
            // List<List<string>> hasilPath = b.BFS(testGraph, "1");
            // // b.PrintAllPath(hasilPath);

            // Console.WriteLine("Uji Path:");
            // hasilPath = b.ShortestPathToNode(testGraph,"1","8");
            // b.PrintShortestPath(hasilPath);
            // Console.WriteLine(b.Search(testGraph, "c"));

            List<List<string>> hasilPath = b.ShortestPathToNode(ujiShortPathGraph,"A","H");
            b.PrintShortestPath(hasilPath);

        }
    }
}