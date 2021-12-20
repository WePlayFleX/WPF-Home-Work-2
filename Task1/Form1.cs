using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Task1
{
    public partial class Form1 : Form
    {
        private const string path = "Log";

        public Form1()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (CheckFields())
            {
                listBox1.Items.Add(new User(textBox4.Text, textBox1.Text, textBox2.Text, textBox3.Text));
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            else
            {
                MessageBox.Show("Заполните все нужные поля");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                var user = (User)listBox1.SelectedItem;

                textBox4.Text = user.Name;
                textBox1.Text = user.LastName;
                textBox2.Text = user.Email;
                textBox3.Text = user.Telephone.ToString();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            using (var writer = new StreamWriter(File.Open(path, FileMode.Create)))
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    var user = (User)listBox1.Items[i];

                    writer.WriteLine(user.Name + " " + user.LastName + " " + user.Email + " " + user.Telephone);
                }
            }

            MessageBox.Show("Successfully exported");
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            var users = new List<User>();

            using (var reader = new StreamReader(path))
            {
                string[] splited_str;

                while (reader.Peek() != -1)
                {
                    splited_str = reader.ReadLine().Split(' ');
                    users.Add(new User(splited_str[0], splited_str[1], splited_str[2], splited_str[3]));
                }
            }


            listBox1.Items.AddRange(users.ToArray());
            MessageBox.Show("Successfully imported");
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                if (CheckFields())
                {
                    int index = listBox1.SelectedIndex;
                    var user = new User(textBox4.Text, textBox1.Text, textBox2.Text, textBox3.Text);

                    listBox1.Items.RemoveAt(index);
                    listBox1.Items.Insert(index, user);
                }
                else
                {
                    MessageBox.Show("Заполните все нужные поля");
                }
            }
        }

        private bool CheckFields()
        {
            return !string.IsNullOrWhiteSpace(textBox4.Text) &&
                   !string.IsNullOrWhiteSpace(textBox1.Text) &&
                   !string.IsNullOrWhiteSpace(textBox3.Text);
        }
    }

    public class User
    {
        public User(string name, string lastName, string email, string telephone)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Telephone = telephone;
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }

        public override string ToString()
        {
            return $"{Name} {LastName} E-mail: {Email}, Telephone: {Telephone}";
        }
    }
}
