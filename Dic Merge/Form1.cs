using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dic_Merge
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            string original = "";
            string alt = "";
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                 original = OFD.FileName;
                //Path.GetExtension(original)
            }

            if(original != "")
            {
                OFD = new OpenFileDialog();
               
                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    alt = OFD.FileName;
                    //Path.GetExtension(original)
                }
            }

            if(original != "" && alt != "")
            {
                MergeDic(original, alt);
            }

        }

        private void MergeDic(string original, string alt)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            MergeDic(dic, original);
            MergeDic(dic, alt);

            string resultText = "";
            using (StreamWriter newTask = new StreamWriter(original, false))
            {
                resultText = original + " 에 저장  "+ System.Environment.NewLine + "-------------------------------" + System.Environment.NewLine;
                resultText += "데이터 : " + dic.Count.ToString() + System.Environment.NewLine; 

                foreach (var obj in dic)
                {

                    newTask.WriteLine("/s");
                    newTask.WriteLine(obj.Key);
                    newTask.WriteLine(obj.Value);
                    newTask.WriteLine("");

                    resultText += obj.Key + System.Environment.NewLine + obj.Value + System.Environment.NewLine;
                    resultText += System.Environment.NewLine;
                }            
                newTask.Close();

                richTextBox1.Text = resultText;
            }

            
        }

        private void MergeDic(Dictionary<string, string> dic, string file)
        {
            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if(line == "/s")
                    {
                        string key = reader.ReadLine();
                        string result = reader.ReadLine();

                        if(result != "/s")
                        {
                            dic[key] = result;
                        }                    
                    }
                }
            }
        }
    }
}
