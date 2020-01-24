using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using search_in_excel;

namespace search_gui
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();

            listView1.Columns.Clear();

            

            // Add a column with width 20 and left alignment.
            listView1.Columns.Add("Directory", 120, HorizontalAlignment.Center);
            listView1.Columns.Add("Worksheet", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Signal", 120, HorizontalAlignment.Left);
            listView1.View = View.Details;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFolderBrowse = new FolderBrowserDialog();

            openFolderBrowse.ShowNewFolderButton = false;

            openFolderBrowse.Description = "Ordner auswählen";



            DialogResult res = openFolderBrowse.ShowDialog();
            res.ToString();

            if(res == DialogResult.OK)
            {
                string path = openFolderBrowse.SelectedPath;

                this.selectedPathLabel.Text = path;

                listView1.Items.Clear();
                //ListViewItem lvi = new ListViewItem("test");
                //lvi.SubItems.Add("test");
                //lvi.SubItems.Add("test2");
                //listView1.Items.Add(lvi);
            }

         

        }

        private void selectedPathLabel_Click(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {

            this.startButton.Enabled = false;
            DirectorySearch searchObject = new DirectorySearch();

            searchObject.setLocation(this.selectedPathLabel.Text + @"\");

            searchObject.searchForString(this.inputBox.Text);

            List<List<string>> retObj = new List<List<string>>();

            retObj = searchObject.getListObjects();

            foreach ( List<string> pairList in retObj)
            {
                ListViewItem lvi = new ListViewItem("FirstColumn");
                foreach (string pairValue in pairList)
                {
                    lvi.SubItems.Add(pairValue);
                }

                this.listView1.Items.Add(lvi);
            }


            this.startButton.Enabled = true;

            // Object mit inhalt zurückgeben und in listView einfügen
        }
    }
}
