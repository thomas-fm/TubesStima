using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NasiPadang
{

    public class DepthFirstSearch
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

                foreach(string nodeTetangga in graphInput.adj2[indexCurrentNode]) {
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
            bool ketemu;
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

                foreach(string nodeTetangga in graphInput.adj2[indexCurrentNode]) {
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
                    ketemu = true;
                    break;
                }
                urutanDFS.Clear();
            }
            if(!ketemu) {
                List<List<string>> gakKetemu = new List<List<string>>();
                return gakKetemu;
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
}