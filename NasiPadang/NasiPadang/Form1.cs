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

namespace NasiPadang
{
    public partial class Form1 : Form
    {
        //private OpenFileDialog openFileDialog1;
        private List<List<string>> graph;
        private string selectedAccount;
        private string selectedFriend;
        private List<string> listOfNode;
        private Graph undirectedGraph;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gViewer1_Load(object sender, EventArgs e)
        {
           
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //MessageBox.Show("Test");
                //var filePath = string.Empty;
                //undirectedGraph = new Graph();
                var content = string.Empty;
                var fileStream = ofd.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    content = reader.ReadToEnd();
                }
                string[] lines = content.Split("\n");
                // MessageBox.Show(content);
                //undirectedGraph.readFromFile(lines);
                // assign content ke Graph
                Microsoft.Msagl.Drawing.Graph visualGraph = new Microsoft.Msagl.Drawing.Graph("visualGraph");
                int i = 0;
                foreach (string line in lines)
                {
                    if (i != 0)
                    {
                        string[] node = line.Split(" ");
                        visualGraph.AddEdge(node[0], node[1]);
                    }
                    i++;
                }
                gViewer1.Graph = visualGraph;
                //SuspendLayout();
            
                //ResumeLayout();
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            // Rekomendasi Teman
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            // Explore friends
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Account
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
