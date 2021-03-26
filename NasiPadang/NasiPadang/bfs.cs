using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NasiPadang
{

    public class BreadthFirstSearch
    {
        public string Search(Graph graphInput, string nameToSearchFor)
        {
            Queue<string> AntrianNode = new Queue<string>();
            HashSet<string> Dikunjungi = new HashSet<string>();
            AntrianNode.Enqueue(graphInput.adj2[0][0]);
            Dikunjungi.Add(graphInput.adj2[0][0]);

            while (AntrianNode.Count > 0)
            {
                string e = AntrianNode.Dequeue();
                if (e == nameToSearchFor)
                    return e;
                foreach (string friend in graphInput.adj2[0])
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
            while (graphInput.adj2[i][0] != nodeToSearch) {
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

            AntrianNode.Enqueue(graphInput.adj2[indexCurrentNode][0]);
            Dikunjungi.Add(graphInput.adj2[indexCurrentNode][0]);
            List<List<string>> AntrianNodeLevel = new List<List<string>>();
            List<string> TempAntrian = new List<string>();
            TempAntrian.Add(graphInput.adj2[indexCurrentNode][0]);
            AntrianNodeLevel.Add(new List<string>(TempAntrian));
            nodeTerakhirLevel = graphInput.adj2[indexCurrentNode][0];
            daftarNodeTerakhirLevel.Enqueue(nodeTerakhirLevel);
            TempAntrian.Clear();

            int i = 0;
            while (AntrianNode.Count > 0)
            {
                string e = AntrianNode.Dequeue();
                indexCurrentNode = SearchNodeIndex(graphInput, e);
                // BFSOrder.Enqueue(e);

                foreach(string nodeTetangga in graphInput.adj2[indexCurrentNode])
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
                //if (i < graphInput.nPairs) {
                //    i++;
                //}
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

            AntrianNode.Enqueue(graphInput.adj2[indexCurrentNode][0]);
            Dikunjungi.Add(graphInput.adj2[indexCurrentNode][0]);
            List<List<string>> AntrianNodeLevel = new List<List<string>>();
            List<string> TempAntrian = new List<string>();
            TempAntrian.Add(graphInput.adj2[indexCurrentNode][0]);
            AntrianNodeLevel.Add(new List<string>(TempAntrian));
            nodeTerakhirLevel = graphInput.adj2[indexCurrentNode][0];
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

                foreach(string nodeTetangga in graphInput.adj2[indexCurrentNode])
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
                //if (i < graphInput.nPairs) {
                //    i++;
                //}

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
                    foreach(string node2 in graphInput.adj2[indexCurrentNode]) {
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
}