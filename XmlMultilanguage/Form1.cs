using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlMultilanguage.MultiLanguage;

namespace XmlMultilanguage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SetTextValues()
        {
            if (dataGridView1.CurrentRow.Cells[0].Value == null) return;
            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtKey.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtValue.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            Lang.LoadKeys();
            var keys = (Lang.Keys as CustomDynamicObject).DynamicProperties;
            int i = 1;
            foreach (var item in keys)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = i.ToString();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = item.Key;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = item.Value.ToString();
                i++;
            }
            SetTextValues();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            SetTextValues();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result = Lang.UpdateKey(txtKey.Text, txtValue.Text);
            if (!result)
                MessageBox.Show("Key is not exists.");
            else
                LoadData();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            bool result = Lang.InsertKey(txtKey.Text, txtValue.Text);
            if (!result)
                MessageBox.Show("Duplicated key.");
            else
                LoadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.CurrentCulture = new System.Globalization.CultureInfo(comboBox1.Text);
            LoadData();
        }
    }
}
