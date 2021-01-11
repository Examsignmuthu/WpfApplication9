using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<stu> AuthorList = new ObservableCollection<stu>();
        SqlConnection con = new SqlConnection(Properties.Settings.Default.ConnectionString);
        SqlCommand cmd = new SqlCommand();


        List<MediaFile> medialist = new List<MediaFile>();
        List<data> mynewlist = new List<data>();
        int count = 1;
        int count1 = 0;

        public MainWindow()
        {
            InitializeComponent();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select name,filename from Test ";
            SqlDataReader reader1;

            reader1 = cmd.ExecuteReader();
            while (reader1.Read())
            {

                mynewlist.Add(new data { name = reader1.GetString(0), filename = (byte[])reader1["FileName"] });

            }
         
            con.Close();







            


        }
        public class stu
        {
            public string num { get; set; }
            public string name { get; set; }
            public byte[] filename { get; set; }
        }

        private void sub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //    ListViewItem item = ListView1.Items[ListView1.SelectedIndex];

            //    TextBox1.Text = item.ToString();
            //    //textBoxName.Text = item.SubItems[0].Text;
            //ListViewItem item = list.Items[list.SelectedValue];

        }

        private void Markverify_Click(object sender, RoutedEventArgs e)
        {

        }

        private void voice_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer Sound1 = new MediaPlayer();

            foreach (var item in medialist)
            {


                var existfile = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.wav").FirstOrDefault();
                if (existfile != null)

                {
                    File.Delete(existfile);
                }

                System.IO.File.WriteAllBytes(item.Name + ".wav", item.FileName);
                var file = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.wav").FirstOrDefault();
                //System.Media.SoundPlayer mplay = new System.Media.SoundPlayer(file);
                //mplay.Play();
                Sound1.Open(new Uri(file));
                Sound1.Play();
                //Sound1.Stop();

            }


        }

        private void NextQues_Click(object sender, RoutedEventArgs e)
        {

            //var v = 5;
            //var count = 0;
            //for (var i = 0; i < 5; i++)
            //{
            //    var c = mynewlist[count].name;
            //    MessageBox.Show(c.ToString());
            //    count = count + 1;
            //}
            if (list.Items.Count < 10)
            {
                if (txtsearch.Text != "")
                {
                    //foreach (var items in mynewlist)
                    //{
                    //    if (items.name.ToString() == txtsearch.Text.ToString())
                    //    {

                    //        medialist.Add(new MediaFile { Name = items.name, FileName = items.filename });


                    //    }
                    //}
                    LabCountText.Content = count.ToString();

                    var exist1 = mynewlist.Where(temp => temp.name.ToLower() == txtsearch.Text.ToLower()).FirstOrDefault();
                    var exist = mynewlist.Where(temp => temp.name.ToLower() == txtsearch.Text.ToLower()).Count();
                    if (exist!= 0)
                    {
                        AuthorList.Add(new stu() { num = count.ToString() + ".", name = txtsearch.Text, filename = exist1.filename });
                    }
                    else
                    {
                        AuthorList.Add(new stu() { num = count.ToString() + ".", name = txtsearch.Text, filename = AuthorList[2].filename });
                    }
                  
                    //foreach (var items in mynewlist)
                    //{
                    //    if (items.name.ToString() == txtsearch.Text.ToString())
                    //    {
                    //        AuthorList.Add(new stu() { num = count.ToString() + ".", name = txtsearch.Text, filename = items.filename });
                    //    }

                    //     if(items.name.ToString() != txtsearch.Text.ToString())
                    //    {
                    //        AuthorList.Add(new stu() { num = count.ToString() + ".", name = txtsearch.Text, filename = items.filename });
                    //        //break;   
                    //    }
                        
                    //}
                   



                    list.ItemsSource = AuthorList;
                    count++;
                    txtsearch.Clear();


                }
                else
                {
                    MessageBox.Show("Enter the Text");
                }
            }
                

                if (list.Items.Count == 10)
                {

                    SubmitTest.Visibility = Visibility.Visible;
                    NextQues.Visibility = Visibility.Hidden;
                    voice.Visibility = Visibility.Visible;
                    //voiceDup.Visibility = Visibility.Hidden;
                    txtsearch.Visibility = System.Windows.Visibility.Visible;
                }
            //else
            //{
            //    MessageBox.Show("invalid");
            //}


        }

        private void SubmitTest_Click(object sender, RoutedEventArgs e)
        {
            listen.Visibility = Visibility.Hidden;
            mark.Visibility = System.Windows.Visibility.Visible;


        }
        public class MediaFile
        {
            public string Name { get; set; }
            public byte[] FileName { get; set; }
        }
        public class data
        {
            public string name { get; set; }
            public byte[] filename { get; set; }
        }


    }
}
