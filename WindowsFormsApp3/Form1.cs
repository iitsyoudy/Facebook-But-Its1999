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
using Newtonsoft.Json;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        Panel Panel;
        TextBox txt1, txt2;
        Button rgr;
        Button tiz;
        Label name;
        Label fs;
        PictureBox pictureBox;
        public class User
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        List<User> users;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            users = LoadUsers(); // Load existing users from file or create a new list if file doesn't exist
            fs = new Label();
            fs.Location = new Point(495,300);
            fs.ForeColor = Color.SteelBlue;
            fs.Size = new Size(200, 20);
            fs.Font = new Font("Arial", 16);
            fs.Text = "FASBOOK";
            Controls.Add(fs);
            this.BackColor = Color.Blue;
            Panel = new Panel();
            pictureBox = new PictureBox();
            string imagePath = Path.Combine(Application.StartupPath, "facebook.png");
            if (File.Exists(imagePath))
                 pictureBox.Image = Image.FromFile(imagePath);
             else MessageBox.Show("aaaaaaah" + imagePath ); 
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Location = new System.Drawing.Point(300, 100);
            pictureBox.Size = new System.Drawing.Size(500, 150);
            pictureBox.BackColor = Color.Blue;
            Controls.Add(pictureBox);
            Panel.Location = new System.Drawing.Point(1100, 50);
            Panel.Size = new System.Drawing.Size(400, 400);
            Panel.BackColor = Color.SteelBlue;
            Panel.BorderStyle = BorderStyle.None;
            name = new Label();
            name.Location = new Point(100, 130);
            name.ForeColor = Color.Black;
            name.Text = "Enter username";
            txt1 = new TextBox();
            txt2 = new TextBox();
            rgr = new Button();
            tiz = new Button();
            txt1.Location = new Point(100, 150);
            txt2.Location = new Point(100, 200);
            txt2.PasswordChar = '*';
            Panel.Controls.Add(txt1);
            Panel.Controls.Add(txt2);
            rgr.Location = new Point(200, 300);
            tiz.Location = new Point(100, 300);
            rgr.BackColor = Color.Gray;
            rgr.Text = "Sign up";
            tiz.Text = "Sign in";
            tiz.BackColor= Color.Gray;
            rgr.Click += nmps;
            tiz.Click += tizz;
            Panel.Controls.Add(rgr);
            Panel.Controls.Add(name);
            Panel.Controls.Add(tiz);
            Controls.Add(Panel);

            // Subscribe to the FormClosing event
            this.FormClosing += Form1_FormClosing;
        }
        private void tizz (object sender,EventArgs e)
        {
            Form2 tecondForm = new Form2(users);

            // Show the SecondForm
            tecondForm.ShowDialog();
        }

        private void nmps(object sender, EventArgs e)
        {
            string username = txt1.Text;
            string password = txt2.Text;
            if (users.Exists(userr => userr.UserName == username))
            {
                MessageBox.Show("Username already exists. Please choose a different one.");
                return; 
            }

            User user = new User
            {
                UserName = username,
                Password = password
            };
            users.Add(user);
            MessageBox.Show("Registration successful!");
            OpenSecondForm();
            SaveUsersToFile(); 
        }

        private void SaveUsersToFile()
        {
            try
            {
                
                string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText("users.json", jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving users: {ex.Message}");
            }
        }

        private List<User> LoadUsers()
        {
            try
            {
                
                if (File.Exists("users.json"))
                {
                    //File.Delete("users.json");
                    string json = File.ReadAllText("users.json");
                    return JsonConvert.DeserializeObject<List<User>>(json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}");
            }

            return new List<User>();
        }

        private void OpenSecondForm()
        {
           
            Form2 secondForm = new Form2(users);

            
            secondForm.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            SaveUsersToFile();
        }
    }

    public class Form2 : Form
    {
        TextBox usernameTextBox, passwordTextBox;
        Button loginButton;
        List<Form1.User> users;

        public Form2(List<Form1.User> userList)
        {
            users = userList;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            MinimizeBox = false;
            MaximizeBox = false;
            // this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.SteelBlue;
            usernameTextBox = new TextBox
            {
                Location = new Point(100, 50),
                Size = new Size(150, 20)
            };

            passwordTextBox = new TextBox
            {
                Location = new Point(100, 80),
                Size = new Size(150, 20),
                PasswordChar = '*'
            };
            Controls.Add(usernameTextBox);
            Controls.Add(passwordTextBox);

            loginButton = new Button
            {
                Text = "Login",
                Location = new Point(100, 110)
            };
            loginButton.Click += LoginButton_Click;
            Controls.Add(loginButton);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string enteredUsername = usernameTextBox.Text;
            string enteredPassword = passwordTextBox.Text;

            
            if (users.Exists(user => user.UserName == enteredUsername && user.Password == enteredPassword))
            {
                MessageBox.Show($"Login successful! Welcome, {enteredUsername}!");
                OpentecondForm();
            }
            else
            {
                MessageBox.Show("Login failed. Please check your username and password.");
            }
           
        }
        private void OpentecondForm()
        {
            
            Form3 tecondForm = new Form3();

            
            tecondForm.ShowDialog();
        }
    }
    public class Form3 : Form
    {
        TextBox post;
        Label postt;
        Button pos;
        public Form3()
        {
            this.BackColor = Color.SteelBlue;
            this.WindowState = FormWindowState.Maximized;
            post = new TextBox();   
            postt= new Label();
            pos = new Button();
            pos.ForeColor = Color.White;
            pos.Text = "POST";
            postt.Font = new Font("Arial", 16);
            postt.Size = new Size(500, 100);
            post.Location = new Point(200, 100);
            Controls.Add(post);
            postt.Location = new Point(200, 200);
            pos.Location = new Point(200, 400);
            pos.Click += converts;
            Controls.Add(pos);

        }
        private void converts(object sender, EventArgs e)
        {
            postt.Text = post.Text;
            Controls.Add(postt);
        }
    }
}
