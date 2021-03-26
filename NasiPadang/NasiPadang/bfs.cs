using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NasiPadang
{
    //Class Breadth First Search (BFS)
    //Berisi method-method untuk melakukan BFS
    public class BreadthFirstSearch
    {
        //Method yang menerima node berupa string dan juga graph.
        //Akan dicari apakah node ada di dalam graph.
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
                    return e; //mengembalikan string node apabila ditemukan
                foreach (string friend in graphInput.adj2[0])
                {
                    if (!Dikunjungi.Contains(friend))
                    {
                        AntrianNode.Enqueue(friend);
                        Dikunjungi.Add(friend);
                    }
                }
            }
            return "Tidak ditemukan"; //mengembalikan pesan tidak ditemukan
        }

        //Method yang menerima graph dan juga sebuah node bertipe string.
        //Mengembalikan index node pada graph
        public int SearchNodeIndex(Graph graphInput, string nodeToSearch) {
            int i = 0;
            while (graphInput.adj2[i][0] != nodeToSearch) {
                i++;
            }
            return i;
        }

        //Algoritma utama untuk melakukan penelurusan graph dengan BFS
        public List<List<string>> BFS(Graph graphInput, string startingNode) // ini kalau Graph udah bener
        {

            //Inisialisasi variabel untuk menyimpan node dan menandai node yang telah dikunjungi
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
            
            //Algoritma BFS akan terus dijalankan sampai seluruh node selesai dikunjungi (AntrianNode.Count() == 0)
            while (AntrianNode.Count > 0)
            {
                //Mengeluarkan node yang saat ini sedang ditelurusi cabang-cabangnya
                string e = AntrianNode.Dequeue();
                indexCurrentNode = SearchNodeIndex(graphInput, e);
                
                //Melakukan traversal terhadap seluruh cabang dari node yang sedang ditelusuri
                foreach(string nodeTetangga in graphInput.adj2[indexCurrentNode])
                {
                    //Apabila node cabang belum pernah dikunjungi sebelumnya, maka ia akan ditandai
                    if (!Dikunjungi.Contains(nodeTetangga)) {
                        AntrianNode.Enqueue(nodeTetangga);
                        TempAntrian.Add(nodeTetangga);
                        Dikunjungi.Add(nodeTetangga);
                        nodeTerakhirLevel = nodeTetangga;
                    }
                }

                //Menyimpan node yang telah dikunjungi ke dalam variabel yang sesuai dengan type return method BFS
                if (TempAntrian.Count > 0 && daftarNodeTerakhirLevel.Peek() == e ) {
                    daftarNodeTerakhirLevel.Dequeue();
                    daftarNodeTerakhirLevel.Enqueue(nodeTerakhirLevel);
                    AntrianNodeLevel.Add(new List<string>(TempAntrian));
                    TempAntrian.Clear();
                }
            }
            return AntrianNodeLevel;
        }

        //Method untuk menemukan jarak dari suatu node asal ke node tujuan dengan metode BFS
        public List<List<string>> ShortestPathToNode(Graph graphInput, string startingNode, string endNode) // ini kalau Graph udah bener
        {
            //Inisialiasi variabel
            bool ketemu = false;

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
            //Apabila node asal sama dengan node tujuan
            if (startingNode == endNode) {
                return AntrianNodeLevel;
            }

            //Melakukan travers seperti algoritma pada method BFS
            while (AntrianNode.Count > 0)
            {
                string e = AntrianNode.Dequeue();
                indexCurrentNode = SearchNodeIndex(graphInput, e);
                foreach(string nodeTetangga in graphInput.adj2[indexCurrentNode])
                {
                    if (!Dikunjungi.Contains(nodeTetangga)) {
                        AntrianNode.Enqueue(nodeTetangga);
                        TempAntrian.Add(nodeTetangga);
                        Dikunjungi.Add(nodeTetangga);
                        nodeTerakhirLevel = nodeTetangga;
                        //Terdapat tambahan pengecekan, apabila node yang ditemukan sama dengan node tujuan
                        if (nodeTetangga == endNode) {
                            break;
                        }
                    }
                }
                    

                //Memasukan node yang telah ditemukan
                if ((TempAntrian.Count > 0 && daftarNodeTerakhirLevel.Peek() == e) || nodeTerakhirLevel == endNode ) { 
                    daftarNodeTerakhirLevel.Dequeue();
                    daftarNodeTerakhirLevel.Enqueue(nodeTerakhirLevel);
                    AntrianNodeLevel.Add(new List<string>(TempAntrian));
                    TempAntrian.Clear();
                }

                //Menandai apabila node tujuan dapat dicapai oleh node asal
                if (nodeTerakhirLevel == endNode) {
                    ketemu = true;
                    break;
                }
            }
            //Apabila node tujuan tidak bisa dicapai oleh node asal, akan mengembalikan jalur kosong
            if(!ketemu) {
                List<List<string>> gakKetemu = new List<List<string>>();
                return gakKetemu;
            }

            //Algoritma untuk menyusun jalur yang diperlukan untuk mencapai node tujuan
            Stack<string> Pathnya = new Stack<string>();
            bool found;
            Pathnya.Push(nodeTerakhirLevel);
            i = AntrianNodeLevel.Count()-2;

            //Algoritma "Backtrack" terhadap hasil penelusuran sebelumnya. Guna mencari rute untuk sampai ke node tujuan
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
            AntrianNodeLevel.Clear();
            TempAntrian.Clear();

            //Memasukkan hasil jalur ke dalam container yang sesuai dengan type return method
            while(Pathnya.Count() > 0) {
                TempAntrian.Add(Pathnya.Pop());
                AntrianNodeLevel.Add(new List<string>(TempAntrian));
                TempAntrian.Clear();
            }
            return AntrianNodeLevel;
        }

        //Method untuk melakukan print hasil BFS ke console; digunakan hanya untuk keperluan testing
        public void PrintAllPath(List<List<string>> AllPath)
        {

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


        //Method untuk melakukan print hasil BFS ke console; digunakan hanya untuk keperluan testing
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