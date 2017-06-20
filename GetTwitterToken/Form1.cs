using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tweetinvi;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;
using Tweetinvi.Parameters;


namespace GetTwitterToken {

    public partial class Form1 : Form {

        public string consumer_key = "3nVuSoBZnx6U4vzUxf5w";
        public string consumer_secret = "Bcs59EFbbsdF6Sl9Ng71smgStWEGwXXKSjYvPVt7qys";
        public string auth_key = "";
        public string auth_secret = "";

        public TwitterCredentials appCredentials;
        public IAuthenticationContext authenticationContext;
        public ITwitterCredentials userCredentials;
        public IAuthenticatedUser authenticatedUser;

        Dictionary<string, string> list_consumer_key = new Dictionary<string, string>();
        Dictionary<string, string> list_consumer_secret = new Dictionary<string, string>();

        public Form1() {

            list_consumer_key.Add("Twitter for Android", "3nVuSoBZnx6U4vzUxf5w");
            list_consumer_secret.Add("Twitter for Android", "Bcs59EFbbsdF6Sl9Ng71smgStWEGwXXKSjYvPVt7qys");
            list_consumer_key.Add("Twitter for iPhone", "IQKbtAYlXLripLGPWd0HUA");
            list_consumer_secret.Add("Twitter for iPhone", "GgDYlkSvaPxGxC4X8liwpUoqKwwr3lCADbz8A7ADU");
            list_consumer_key.Add("Twitter for iPad", "CjulERsDeqhhjSme66ECg");
            list_consumer_secret.Add("Twitter for iPad", "IQWdVyqFxghAtURHGeGiWAsmCAGmdW3WmbEx6Hck");
            list_consumer_key.Add("Twitter for Mac", "3rJOl1ODzm9yZy63FACdg");
            list_consumer_secret.Add("Twitter for Mac", "5jPoQ5kQvMJFDYRNE8bQ4rHuds4xJqhvgNJM4awaE8");
            list_consumer_key.Add("Twitter for Windows Phone", "yN3DUNVO0Me63IAQdhTfCA");
            list_consumer_secret.Add("Twitter for Windows Phone", "c768oTKdzAjIYCmpSNIdZbGaG0t6rOhSFQP0S5uC79g");
            list_consumer_key.Add("Twitter for Google TV", "iAtYJ4HpUVfIUoNnif1DA");
            list_consumer_secret.Add("Twitter for Google TV", "172fOpzuZoYzNYaU3mMYvE8m8MEyLbztOdbrUolU");
            list_consumer_key.Add("twicca", "7S2l5rQTuFCj4YJpF7xuTQ");
            list_consumer_secret.Add("twicca", "L9VHCXMKBPb2eWjvRvQTOEmOyGlH4W50getaQJPya4");
            list_consumer_key.Add("Echofon", "yqoymTNrS9ZDGsBnlFhIuw");
            list_consumer_secret.Add("Echofon", "OMai1whT3sT3XMskI7DZ7xiju5i5rAYJnxSEHaKYvEs");
            list_consumer_key.Add("Instagram", "7YBPrscvh0RIThrWYVeGg");
            list_consumer_secret.Add("Instagram", "sMO1vDyJ9A0xfOE6RyWNjhTUS1sNqsa7Ae14gOZnw");

            InitializeComponent();

            textBox1.Text = consumer_key;
            textBox2.Text = consumer_secret;

            comboBox1.DisplayMember = "Key";
            comboBox1.ValueMember = "Value";
            comboBox1.DataSource = new BindingSource(list_consumer_key, null);
            
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            consumer_key = textBox1.Text;
            consumer_secret = textBox2.Text;

            Clipboard.SetData("0",DataFormats.Text);

            appCredentials = new TwitterCredentials(consumer_key, consumer_secret);
            authenticationContext = AuthFlow.InitAuthentication(appCredentials);

            Process.Start(authenticationContext.AuthorizationURL);
            while (true) {
                if (Clipboard.GetDataObject().GetData(DataFormats.Text) == null) {
                    Delay(1000);
                    continue;
                }
                var clipboard = Clipboard.GetDataObject().GetData(DataFormats.Text).ToString();

                if (clipboard.Length==7) {
                    userCredentials = AuthFlow.CreateCredentialsFromVerifierCode(clipboard, authenticationContext);
                    Auth.SetCredentials(userCredentials);
                    try {
                        authenticatedUser = User.GetAuthenticatedUser();
                        if (Convert.ToInt64(authenticatedUser.IdStr) > 0) {
                            textBox3.Text = userCredentials.AccessToken;
                            textBox4.Text = userCredentials.AccessTokenSecret;
                        }
                    } catch (Exception) {
                        break;
                    }
                    break;
                }
                Delay(500);
            }
            try {
               

            } catch (Exception) {
                //an error occured
            }

        }
        public static DateTime Delay(int MS) {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);
            while (AfterWards >= ThisMoment) {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                textBox1.Text = list_consumer_key[comboBox1.Text];
                textBox2.Text = list_consumer_secret[comboBox1.Text];
                
            } catch (Exception) {
                textBox1.Text = list_consumer_key["Twitter for Android"];
                textBox2.Text = list_consumer_secret["Twitter for Android"];
            }
            consumer_key = textBox1.Text;
            consumer_secret = textBox2.Text;
        }
    }

}
