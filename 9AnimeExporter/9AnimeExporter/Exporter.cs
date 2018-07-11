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

namespace _9AnimeExporter
{
    public partial class Exporter : Form
    {
        public Exporter()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        string selectedfile = "";
        string text = "";
        string strStart = "alt=";
        string strEnd=">";
        string data = "";
        bool stop = false;
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public void reset()
        {
            // selectedfile = "";
             text = "";
             strStart = "alt=";
             strEnd = ">";
             data = "";
             stop = false;
        }


        public void getRawData()
        {
           
            using (StreamReader sr = File.OpenText(selectedfile))
            {
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    text += s;
                }
            }
        }

        public void getBetweenMult()
        {

            int Start, End;
           
            if (text.Contains(strStart) && text.Contains(strEnd))
            {
                
                Start = text.IndexOf(strStart, 0) + strStart.Length;
                Start += 1;
                End = text.IndexOf(strEnd, Start);
                End -= 1;
                data= text.Substring(Start, End - Start)+ "\r\n"; ;
               string texttmp = text.Substring(End);
                text = texttmp;

                ////////////////////////
                // for '
                string Replace="'";
                string Find = "&#39;";
                bool stopreplacement = false;
                do
                {
                    if (data.Contains(Find))
                    {
                        int Place = data.IndexOf(Find);
                        string result = data.Remove(Place, Find.Length).Insert(Place, Replace);
                        data = result;
                    }
                    else
                    {
                        stopreplacement = true;
                    }
                } while (stopreplacement == false);

                ////////////////////////// 
                ////////////////////////
                // for &
                 Replace = "&";
                 Find = "&amp;";
                 stopreplacement = false;
                do
                {
                    if (data.Contains(Find))
                    {
                        int Place = data.IndexOf(Find);
                        string result = data.Remove(Place, Find.Length).Insert(Place, Replace);
                        data = result;
                    }
                    else
                    {
                        stopreplacement = true;
                    }
                } while (stopreplacement == false);

                ////////////////////////// 


                string file_name = path+"\\exported.txt";
                System.IO.StreamWriter objWriter;
                objWriter = new System.IO.StreamWriter(file_name,true);
                objWriter.Write(data);
                objWriter.Close();

                //  return text.Substring(Start, End - Start);
            }
            else
            {
                stop = true;
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            reset();
            
            if (textBox1.Text!="")
            {
               
                getRawData();
                do
                {
                    try { getBetweenMult(); }
                    catch { stop = true; }
                } while (stop == false);
                label1.Text = "Done";
            }
            else
            {
                label1.Text = "Select A File First";
            }
          


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
           
            fdlg.InitialDirectory = path;
            fdlg.Filter = "HTML|*.HTML|All files (*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                string file = fdlg.FileName;
                textBox1.Text = file;

                selectedfile = file;
                label1.Text = "Ready To Export";
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
