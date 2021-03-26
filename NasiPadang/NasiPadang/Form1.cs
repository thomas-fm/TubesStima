using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Security;
using System.Text.RegularExpressions;

namespace NasiPadang
{
    public partial class Form1 : Form
    {
        private Graph undirectedGraph; // class graph
        bool isBFS = false; // algoritma apa yang dipakai
        bool isDFS = false;

        // variabel fitur explore
        string account; // comob box 1
        string friends_with; // combo box 2

        public Microsoft.Msagl.Drawing.Graph visualGraph;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Upload button
        private void button1_Click(object sender, EventArgs e)
        {
            // Proses upload file
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Graph
                undirectedGraph = new Graph();
                // Path File
                var filePath = string.Empty;
                // Isi file
                var content = string.Empty;
                var fileStream = ofd.OpenFile();
                filePath = ofd.FileName;

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    content = reader.ReadToEnd();
                }
                // Tampilkan nama file di sebelah button
                textBox1.AppendText(filePath);
                string[] lines = content.Split("\n");
                // Ubah teks menjadi graf
                undirectedGraph.readFromFile(lines);

                // assign content ke Graph
                visualGraph = new Microsoft.Msagl.Drawing.Graph("visualGraph");
                visualGraph.Directed = false;

                int i = 0;
                // Get list of edge
                List<(string, string)> edges = undirectedGraph.getListOfEdge();

                // tampilkan dalam gviewer1
                foreach ((string, string) tuples in edges)
                {
                    var edge = visualGraph.AddEdge(tuples.Item1, tuples.Item2);
                    edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
                    edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                }

                // Isi combo box 1
                comboBox1.BeginUpdate();
                comboBox1.Items.Clear();

                gViewer1.Graph = visualGraph;
                
                foreach (string node in undirectedGraph.getNode())
                {
                    comboBox1.Items.Add(node);
                }
                comboBox1.EndUpdate();
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            // Rekomendasi Teman
            // sepertinya juga tida terpakai
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            // Explore friends
            // sepertinya tida terpakai
        }

        // Box 1 = pilihan account di explore friends
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Account
            account = string.Empty;
            // Cari siapa saja yang bukan teman account

            try
            {
                account = comboBox1.Text;
                comboBox2.BeginUpdate();
                comboBox2.Items.Clear();
                List<string> friends = undirectedGraph.friendsOfAccount(account);
                foreach (string node in undirectedGraph.getNode())
                {
                    if (node != account)
                    {
                        comboBox2.Items.Add(node);
                    }

                }
                comboBox2.EndUpdate();

                // Update friend recommendation
                // bedanya algoritma DFS dan BFS disini di bagian apa masih kurang paham
                textBox_friendRecommend.Text = string.Empty;
                if (isBFS || isDFS)
                    getFriendRecommendation();
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }
        
        // Menampilkan rekomendasi teman
        private void getFriendRecommendation()
        {
            try
            {
                string newLine = Environment.NewLine; // untuk newline, karena tidak bisa \n
                List<List<string>> associatedMutual = undirectedGraph.sortedMutual(account); // dapatkan mutual friends
                string content = string.Empty;
                // tampilkan ke text box
                // clear content
                textBox_friendRecommend.Text = string.Empty;
                // tampilkan
                //printGraph(); for testing
                for (int i = 0; i < associatedMutual.Count; i++)
                {
                    content = content + "=> Akun : " + associatedMutual[i][0] + newLine;
                    content += "Mutual Friends : ";
                    for (int j = 1; j < associatedMutual[i].Count; j++)
                    {
                        if (j != 1)
                        {
                            content += ", ";
                        }
                        content += associatedMutual[i][j];
                    }
                    content += newLine;
                }
                
                //MessageBox.Show(content); for testing
                textBox_friendRecommend.Text = content;
            }
            catch(Exception)
            {
                MessageBox.Show("Error");
            }
        }
        // for testing
        private void printGraph()
        {
            try
            {
                string newLine = Environment.NewLine;
                string graf = string.Empty;
                List<string> node = undirectedGraph.getNode();
                List<List<string>> adj = undirectedGraph.getAdjNode();

                for (int i = 0; i < node.Count; i++)
                {
                    graf += node[i];
                    graf += newLine;

                    for (int j = 0; j < adj[i].Count; j++)
                    {
                        graf += adj[i][j];
                        graf += " ";

                    }
                    graf += newLine;
                }

                MessageBox.Show(graf);
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }
        // Event pada friends with bagian explore friends
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            friends_with = string.Empty;
            try
            {
                // node diisi text pada comboBox
                friends_with = comboBox2.Text;
                // ini sebenarnya langsung saja tapi mager ubah heheh
                if (isBFS && account != null && account.Length != 0)
                {
                    exploreFriends();
                }
                else if (isDFS && account != null && account.Length != 0)
                {
                    // do something
                    exploreFriends();
                }
                else
                {
                    MessageBox.Show("Pilih BFS atau DFS");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        // Reset layout untuk menampilkan graph baru
        private void resetGraphLayout()
        {
            // Reset edge
            IEnumerable<Microsoft.Msagl.Drawing.Edge> listOfEdges = visualGraph.Edges;
            foreach(Microsoft.Msagl.Drawing.Edge edge in listOfEdges)
            {
                edge.Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
            }
            // Reset node
            IEnumerable<Microsoft.Msagl.Drawing.Node> listOfNodes = visualGraph.Nodes;
            foreach(Microsoft.Msagl.Drawing.Node node in listOfNodes)
            {
                node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
                node.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Box;
            }
        }

        //
        public void exploreFriends()
        {
            gViewer1.SuspendLayout();
            // clean graph layout
            resetGraphLayout();
            // get list of edges dari graph
            IEnumerable<Microsoft.Msagl.Drawing.Edge> listOfEdges = visualGraph.Edges;
            // Modifikasi nodes account dan friends with
            var accNode = visualGraph.FindNode(account);
            accNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Orange;

            var friendNode = visualGraph.FindNode(friends_with);
            friendNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Orange;
            
            visualGraph.FindNode(account).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
            visualGraph.FindNode(friends_with).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;

            List<string> result = new List<string>();
            // lakukan DFS atau bfs
            if (isBFS)
            {
                // do bfs
                //
                //
                BreadthFirstSearch bfs = new BreadthFirstSearch();
                List<List<string>> hasilBFS = bfs.ShortestPathToNode(undirectedGraph, account, friends_with);

                for (int k = 0; k < hasilBFS.Count; k++)
                {
                    for (int l = 0; l < hasilBFS[k].Count; l++)
                    {
                        result.Add(hasilBFS[k][l]);
                    }
                }

            }
            else if (isDFS)
            {
                // do dfs
                DepthFirstSearch dfs = new DepthFirstSearch();
                List<List<string>> hasilDFS = dfs.ShortestPathToNode(undirectedGraph, account, friends_with);

                for (int k = 0; k < hasilDFS.Count; k++)
                {
                    for (int l = 0; l < hasilDFS[k].Count; l++)
                    {
                        result.Add(hasilDFS[k][l]);
                    }
                }
            }

            if (result.Count == 0)
            {
                string newLine = Environment.NewLine;
                MessageBox.Show("Tidak ada jalur koneksi yang tersedia.\nAnda harus memulai koneksi baru itu sendiri.");
                string content = "Tidak ada jalur koneksi yang tersedia" + newLine + "Anda harus memulai koneksi baru itu sendiri.";
                textBox_ExploreFriend.Text = string.Empty;
                textBox_ExploreFriend.Text = content;
            }
            else
            {
                for (int i = 0; i < result.Count - 1; i++)
                {
                    // ini untuk ubah warna jalur
                    foreach (Microsoft.Msagl.Drawing.Edge edge in listOfEdges)
                    {
                        if ((result[i] == edge.Target && result[i + 1] == edge.Source)
                            || (result[i] == edge.Source && result[i + 1] == edge.Target))
                        {
                            edge.Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                        }
                    }
                    // perlu ubah warna node juga kah?
                    if (result[i] != account && result[i] != friends_with)
                    {
                        var node = visualGraph.FindNode(result[i]);
                        node.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                        node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightBlue;
                        //visualGraph.FindNode(result[i]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                        //visualGraph.FindNode(result[i]).Attr.Color = Microsoft.Msagl.Drawing.Color.LightBlue;
                    }
                }

                // Menuliskan ke text box
                int nDegree = result.Count - 2;
                string content = string.Empty;
                if (nDegree == 1)
                {
                    content += "1st";
                }
                else if (nDegree == 2)
                {
                    content += "2nd";
                }
                else if (nDegree == 3)
                {
                    content += "3rd";
                }
                else
                {
                    content += nDegree.ToString();
                    content += "th";
                }

                content += " degree connection";
                string newLine = Environment.NewLine;
                content += newLine;

                for (int i =0; i < result.Count; i++)
                {
                    if (i != 0)
                    {
                        content += " => ";
                    }
                    content += result[i];
                }
                textBox_ExploreFriend.Text = string.Empty;
                textBox_ExploreFriend.Text = content;
            }

            gViewer1.Graph = visualGraph;
            gViewer1.ResumeLayout();
        }

        // Exit button
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Radio button for BFS/DFS
        private void BFS_CheckedChanged(object sender, EventArgs e)
        {
            isBFS = BFS.Checked;
            MessageBox.Show("Do BFS things");
        }

        private void DFS_CheckedChanged(object sender, EventArgs e)
        {
            isDFS = DFS.Checked;
            MessageBox.Show("Do DFS things");
        }
        
    }
}
