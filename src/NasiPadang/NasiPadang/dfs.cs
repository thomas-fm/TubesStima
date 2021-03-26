using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NasiPadang
{
    //Class yang menyimpan method-method untuk melakukan algoritma Depth First Search (DFS)
    public class DepthFirstSearch
    {
        //Method yang akan mengembalikan node apabila suatu node ditemukan pada graph
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

        //Method yang mengembalikan letak index node pada graph
        public int SearchNodeIndex(Graph graphInput, string nodeToSearch) {
            int i = 0;
            while (graphInput.adj2[i][0] != nodeToSearch) {
                i++;
            }
            return i;
        }

        //Method utama untuk menelusuri graph dengan algoritma DFS
        public List<List<string>> DFS(Graph graphInput, string startingNode) 
        {
            //Inisialisasi variabel
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

            //DFS akan dijalankan hingga AntrianNode.Count() == 0 dengan kata lain, ketika semua node sudah dikunjungi dan ditelusuri
            while(AntrianNode.Count() > 0) {
                adaYangBelum = false;
                //Menyimpan node yang saat ini ditelusuri ke dalam variabel
                string e = AntrianNode.Pop();
                indexCurrentNode = SearchNodeIndex(graphInput, e);

                //Melakukan traversal untuk menemukan node cabang yang belum ditelusuri
                foreach(string nodeTetangga in graphInput.adj2[indexCurrentNode]) {
                    //Apabila node cabang belum ditelusuri, masukan ke dalam stack dan lanjutkan menelusuri node tersebut
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

                //Apabila belum semua node ditelusuri, mengganti node yang saat ini akan ditelusuri
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

        //Algoritma untuk menemukan rute dari suatu node ke node lainnya dengan DFS
        public List<List<string>> ShortestPathToNode(Graph graphInput, string startingNode, string endNode) // ini kalau Graph udah bener
        {
            //Inisialisasi variabel
            bool ketemu = false;
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

            //Algoritma DFS dijalankan
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
                //Apabila node tujuan sudah ditemukan dalam pencarian, keluar dari loop
                if (nodeTerakhirLevel == endNode) {
                    ketemu = true;
                    break;
                }
                urutanDFS.Clear();
            }
            //Apabila tidak ditemukan rute untuk menuju node tujuan, kembalikan list kosong
            if(!ketemu) {
                List<List<string>> gakKetemu = new List<List<string>>();
                return gakKetemu;
            }

            //Algoritma untuk "backtrack" dan menyusun jalur dari node asal ke node tujuan sesuai hasil DFS yang telah didapatkan
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


            //Menyalin hasil jalur untuk disimpan sesuai tipe return method
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

        //Method antara yang digunakan untuk mengecek apakah terdapat bug pada hasil penelusuran DFS
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

        //Method untuk mengeluarkan hasil penelusuran DFS ke layar; digunakan untuk testing
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

        //Method untuk mengeluarkan hasil penelusuran DFS ke layar; digunakan untuk testing
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