using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace elma2
{
    public partial class Form1 : Form
    {
        public Int32 id = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void GetOrder(Int32 id)
        {

            var order = MyDb.getOrder(id);

            idBox.Text = Convert.ToString(order.Rows[0]["id"]);
            dateBox.Text = Convert.ToString(order.Rows[0]["orderDate"]);
            statusBox.Text = (String)order.Rows[0]["status"];
            descriptionBox.Text = (String)order.Rows[0]["description"];

            dataGridView1.DataSource = MyDb.getOrderDetail(id);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetOrder(id);

            serviceBox.DataSource = MyDb.getServices();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MyDb.addServiceToOrder(id, (Int32)serviceBox.SelectedValue);
            dataGridView1.DataSource = MyDb.getOrderDetail(id);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            GetOrder(++id);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            GetOrder(--id);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sosudBox.Text = MyDb.getNextSosudNo().ToString();
            labelSosudStatus.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }  

        private void sosudBox_TextChanged(object sender, EventArgs e)
        {
            Int32 no = 0;
            try
            {
                no = Convert.ToInt32(sosudBox.Text);
            }
            catch
            {
                labelSosudStatus.Text = "Номер некоректный";
                labelSosudStatus.Visible = true;
                return;
            }

            if (MyDb.testSosudNo(no))
            {
                labelSosudStatus.Text = "Номер свободен";
            }
            else
            {
                labelSosudStatus.Text = "Номер занят";
            }

            labelSosudStatus.Visible = true;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text== "Юрлицо")
            {
                comboBox2.DataSource = MyDb.getCustomers();
                comboBox2.DisplayMember = "name";
            }
            else
            {
                comboBox2.DataSource = MyDb.getPersons();
                comboBox2.DisplayMember = "personName";
            }

            comboBox2.ValueMember = "id";
        }
    }
}
