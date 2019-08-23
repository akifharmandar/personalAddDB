using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonelAdding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PersonelAddingEntities db = new PersonelAddingEntities();

        void Clean()
        {
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = (TextBox)item;
                    txt.Clear();
                }
            }
        }

        void Clean2()
        {
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        void PersonAdd()
        {
            listView1.Items.Clear();
            var kisiler = db.people.ToList();

            foreach (person person in kisiler)
            {
                ListViewItem item = new ListViewItem();
                item.Text = person.FirstName;
                item.SubItems.Add(person.LastName);
                item.SubItems.Add(person.Mail);
                item.SubItems.Add(person.Phone);
                item.Tag = person.id;

                listView1.Items.Add(item);

            }
        }
          

        private void Form1_Load(object sender, EventArgs e)
        {
            PersonAdd();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            person person = new person();
            person.FirstName = textBox1.Text;
            person.LastName = textBox2.Text;
            person.Mail = textBox3.Text;
            person.Phone = textBox4.Text;

            db.people.Add(person);
            db.SaveChanges();
            PersonAdd();
            Clean();
        }

        person person_;
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count==0)
            {
                MessageBox.Show("Lütfen dolu bir sütunu tıklayınız");
                return;
            }
            int id = (int)listView1.SelectedItems[0].Tag;
            person_ = db.people.Find(id);

            textBox5.Text = person_.FirstName;
            textBox8.Text = person_.LastName;
            textBox7.Text = person_.Mail;
            textBox6.Text = person_.Phone;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            person_.FirstName = textBox5.Text;
            person_.LastName = textBox8.Text;
            person_.Mail = textBox7.Text;
            person_.Phone = textBox6.Text;

            db.SaveChanges();
            Clean2();
            PersonAdd();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count==0)
            {
                MessageBox.Show("lütfen bir sütuna tıklayınız");
                return;
            }
            int id = (int)listView1.SelectedItems[0].Tag;
            person person = db.people.Find(id);

           DialogResult dr=                
                MessageBox.Show("Are you sure to delete?","Information",
                    MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

                if (dr==DialogResult.Yes)
                {
                    db.people.Remove(person);
                    db.SaveChanges();
                    PersonAdd();
                }
                else
                {
                    MessageBox.Show("Your data is not being deleted!","information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            
            
        }

      
    }
}
