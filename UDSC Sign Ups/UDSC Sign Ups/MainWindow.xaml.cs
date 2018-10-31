using System;
using System.Collections.Generic;
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

namespace UDSC_Sign_Ups
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string date;
        string organizer;
        string filePath;

        public MainWindow()
        {
            InitializeComponent();
            Name.Visibility = Visibility.Collapsed;
            Email.Visibility = Visibility.Collapsed;
            Gamertag.Visibility = Visibility.Collapsed;
            Melee.Visibility = Visibility.Collapsed;
            Sm4sh.Visibility = Visibility.Collapsed;
            None.Visibility = Visibility.Collapsed;
            FinishButton.Visibility = Visibility.Collapsed;
            DataSubmit.Visibility = Visibility.Collapsed;

            MeleeTextBox.Visibility = Visibility.Collapsed;
            Sm4shTextBox.Visibility = Visibility.Collapsed;
            MeleeList.Visibility = Visibility.Collapsed;
            Sm4shList.Visibility = Visibility.Collapsed;
            contentControl.Content = new SignUpControl();
        }

        private void SubmitButton(object sender, RoutedEventArgs e)
        {
            date = Date.Text;
            organizer = Organizer.Text;
            bool success = true;

            if (date == "" || organizer == "")
                success = false;

            if (success)
            {
                List<string> data = new List<string> { date, organizer };
                DataWriter writer = new DataWriter();
                writer.Create(data);
                string path = Directory.GetCurrentDirectory();
                filePath = path + @"\" + date + ", " + organizer + ".csv";
                contentControl.Content = new SignUpControlData();

                LogOnSubmit.Visibility = Visibility.Collapsed;
                Date.Visibility = Visibility.Collapsed;
                Organizer.Visibility = Visibility.Collapsed;

                Name.Visibility = Visibility.Visible;
                Email.Visibility = Visibility.Visible;
                Gamertag.Visibility = Visibility.Visible;
                Melee.Visibility = Visibility.Visible;
                Sm4sh.Visibility = Visibility.Visible;
                None.Visibility = Visibility.Visible;
                FinishButton.Visibility = Visibility.Visible;
                DataSubmit.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Missing value(s)", "Incomplete Form");
            }
        }

        private void DataSubmitButton(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            string email = Email.Text;
            string gamerTag = Gamertag.Text;
            string melee = "n";
            string sm4sh = "n";
            bool success = true;

            if (name == "" || email == "" || gamerTag == "")
                success = false;

            if (Melee.IsChecked == true)
                melee = "y";
            if (Sm4sh.IsChecked == true)
                sm4sh = "y";

            if (Melee.IsChecked == false && Sm4sh.IsChecked == false && None.IsChecked == false)
                success = false;

            if (success)
            {
                List<string> data = new List<string> { name, email, gamerTag, melee, sm4sh };
                DataWriter writer = new DataWriter();
                writer.Write(filePath, data);

                Name.Clear();
                Email.Clear();
                Gamertag.Clear();
                Melee.IsChecked = false;
                Sm4sh.IsChecked = false;
                None.IsChecked = false;
                MessageBox.Show("Submission Complete", "Submitted");
            }
            else
            {
                MessageBox.Show("Missing value(s)", "Incomplete Form");
            }
        }

        private void FinishButtonClick(object sender, RoutedEventArgs e)
        {
            string[] lines = File.ReadAllLines(filePath);

            List<string[]> values = new List<string[]>();

            foreach (string line in lines)
            {
                values.Add(line.Split(','));
            }

            List<string> MeleeL = new List<string>();
            List<string> Sm4shL = new List<string>();

            foreach (string[] value in values)
            {
                if (value[3].Contains('y'))
                {
                    MeleeL.Add(value[2]);
                } else if (value[4].Contains('y')) 
                {
                    Sm4shL.Add(value[2]);
                }
            }
            
            Name.Visibility = Visibility.Collapsed;
            Email.Visibility = Visibility.Collapsed;
            Gamertag.Visibility = Visibility.Collapsed;
            Melee.Visibility = Visibility.Collapsed;
            Sm4sh.Visibility = Visibility.Collapsed;
            None.Visibility = Visibility.Collapsed;
            FinishButton.Visibility = Visibility.Collapsed;
            DataSubmit.Visibility = Visibility.Collapsed;

            contentControl.Content = new Finish();

            MeleeTextBox.Visibility = Visibility.Visible;
            Sm4shTextBox.Visibility = Visibility.Visible;
            string melee = string.Join(Environment.NewLine, MeleeL.ToArray());
            string sm4sh = string.Join(Environment.NewLine, Sm4shL);
            MeleeList.Text = melee;
            Sm4shList.Text = sm4sh;
            MeleeList.Visibility = Visibility.Visible;
            Sm4shList.Visibility = Visibility.Visible;
        }
    }
}
