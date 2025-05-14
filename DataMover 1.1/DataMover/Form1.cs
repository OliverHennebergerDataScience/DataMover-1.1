using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace DataMover
{
    public partial class Form1 : Form
    {

        public static string text;

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("Option1");
            comboBox1.Items.Add("Option2");
            comboBox1.Items.Add("Option3");

        }

        private void button1_Click(object sender, EventArgs e)
        {

            textBox1.Text = ChooseFolder();
            ReadDataFromTxt();
            ReadDataFromTxtTranfersPath();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = GetDateToday();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox6.Text = ChooseFolder();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox8.Text = ChooseFolder();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void button7_Click(object sender, EventArgs e)
        {
            textBox7.Text = ChooseFolder();
        }
        private void button12_Click(object sender, EventArgs e)
        {

            if (textBox3.Text == "")
            {
                textBox3.Text = ChooseFolder();
            }
            else if (textBox4.Text == "")
            {
                textBox4.Text = ChooseFolder();
            }
            else if (textBox5.Text == "")
            {
                textBox5.Text = ChooseFolder();
            }
            else
            {
                string errortext = "All TextBoxes are Filled.";
                popupError(errortext);
            }





        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            HideSpecificDateChoice();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ShowSpecificDateChoice();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "Option 3 Clicked!";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (label3.Visible == false)
            {
                ShowSubFolderTextBoxes();
            }
            else
            {

                HideSubFolderTextBoxes();
            }

        }


        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "Option 3 Changed!";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            bool finished = false;
            while (!finished)
            {

                string main_folderpath = textBox1.Text;
                

                if (main_folderpath == "")
                {
                  string errortext = "Please choose a working directory";
                    popupError(errortext);
                    break;
                }


                IList<String> folder_list1 = GetFolderList(main_folderpath);

                string previous_folder_path = GetPreviousFolderPath(folder_list1);

                string previous_folder_name = GetPreviousFolderName(previous_folder_path);

                string previous_folder_date = GetPreviousFolderDate(previous_folder_name);



                int i;
                bool success = int.TryParse(previous_folder_date, out i);


                if (success == true)
                {
                    textBox9.Text = i.ToString();
                    break;
                }
                else
                {
                    string errortext = "The output is not a number\nThe expected format for the date is:\nYearMonthDay with 2 numbers each\n The name of the previous directory has to start with this date";
                    popupError(errortext);
                    break;
                }

  
            }  //While Loop End
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox10.Text = ChooseFolder();
        }


        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void button9_Click(object sender, EventArgs e)
        {
            infoForm();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox12.Text = ChooseFolder();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            bool finished = false;
            while (!finished)
            {
                string targetfolder;
                string subfoldername1;

                int subfoldercount = 0;
                string newfolderpath = "";


                // Create Main Folder with date

                if (textBox1.Text == "")
                {
                    string errortext = "Please choose a working folder for the app";
                    popupError(errortext);
                    break;
                }
                else
                {
                    targetfolder = textBox1.Text;

                    if (textBox2.Text == "" || textBox6.Text == "")
                    {
                        string errortext = "Please make sure to add a date and a main folder name";
                        popupError(errortext);
                        break;
                    }
                    else
                    {
                        bool answer = CheckCorrectDate(textBox2.Text);
                        if (answer == true)
                        {
                            textBox2.Text = TransformDate(textBox2.Text);

                        }

                        string[] subdirectories = Directory.GetDirectories(targetfolder);
                        int matchcount = 0;
                        foreach (string directory in subdirectories)
                        {
                            if (directory.Substring(0, 6) == textBox2.Text)
                            {
                                matchcount++;
                            }

                        }


                        answer = CheckIfPath(textBox6.Text);
                        if (matchcount == 0 && answer == true)
                        {    // No Folder Beginning with the date found and a path inside textbox 3
                            subfoldername1 = CreateThisDayFolderName();
                            string mainfolderpath = textBox6.Text;
                            string mainfoldername = GetPreviousFolderName(mainfolderpath);

                            string folderpath = targetfolder + @"\" + subfoldername1 + " " + mainfoldername;
                            Directory.CreateDirectory(folderpath);
                            newfolderpath = folderpath;

                        }
                        else if (matchcount == 0 && answer == false)
                        {
                            subfoldername1 = CreateThisDayFolderName();
                            subfoldername1 = subfoldername1 + " " + textBox6.Text;
                            newfolderpath = CreateSubFolder(targetfolder, subfoldername1);
                        }
                        else
                        {
                            newfolderpath = textBox6.Text;
                        }

                    }
                }

                // Create Category 
                subfoldercount = GetSubFolderCount(newfolderpath);
                if (textBox8.Text != "")
                {
                    if (subfoldercount == 0)
                    {
                        targetfolder = newfolderpath;
                        subfoldername1 = textBox8.Text;


                        newfolderpath = CreateSubFolder(targetfolder, subfoldername1);

                    }
                    else
                    {
                        bool answer = CheckIfPath(textBox8.Text);
                        if (answer == true)
                        {
                            newfolderpath = textBox8.Text;
                        }



                    }
                }

                // Create Subfolders


                subfoldercount = GetSubFolderCount(newfolderpath);
                targetfolder = newfolderpath;
                if (checkBox1.Checked)
                {

                    if (textBox3.Text != "")
                    {
                        bool answer = CheckIfPath(textBox3.Text);
                        if (answer == false)
                        {
                            subfoldername1 = textBox3.Text;
                            newfolderpath = CreateSubFolder(targetfolder, subfoldername1);
                        }

                    }

                    if (textBox4.Text != "")
                    {
                        bool answer = CheckIfPath(textBox4.Text);
                        if (answer == false)
                        {
                            subfoldername1 = textBox4.Text;
                            newfolderpath = CreateSubFolder(targetfolder, subfoldername1);
                        }

                    }

                    if (textBox5.Text != "")
                    {
                        bool answer = CheckIfPath(textBox5.Text);
                        if (answer == false)
                        {
                            subfoldername1 = textBox5.Text;
                            newfolderpath = CreateSubFolder(targetfolder, subfoldername1);
                        }

                    }
                }

                break;
            } //While Loop End
        }

        private void button5_Click(object sender, EventArgs e)
        {

            string start_directory = textBox10.Text;
            string end_directory = textBox7.Text;

            bool finished = false;
            while (!finished)
            {
                bool answer = CheckIfPath(textBox10.Text);
                if (answer == false)
                {
                    string errortext = "Please make sure to add the start for the data transfer";
                    popupError(errortext);
                    break;
                }


                answer = CheckIfPath(textBox7.Text);
                if (answer == false)
                {
                    string errortext = "Please make sure to add a destination for the data transfer";
                    popupError(errortext);
                    break;
                }


                if (radioButton1.Checked)
                {
                    label13.Text = TransferAllFiles(start_directory, end_directory);
                }

                answer = CheckCorrectDate(textBox9.Text);
                if (radioButton2.Checked)
                {
                    if (textBox9.Text == "")
                    {
                        string errortext = "Please specify from which date the data should be transferred";
                        popupError(errortext);
                        break;

                    }
                    else if(answer == true)
                    {
                          textBox9.Text = TransformDate(textBox9.Text);
                     }
                    else
                    {
                        int i;
                        bool success = int.TryParse(textBox9.Text, out i);

                        if (success == true && textBox9.Text.Length == 6)
                        {

                        } else
                        {
                            string errortext = "Please use the right format for the date";
                            popupError(errortext);
                            break;
                        }
                    }



                    label13.Text = TransferFilesBasedOnDate();
                }

                label13.Visible = true;
                label8.Visible = true;

                CreateSaveFileTxt();

                break;
            } //While Loop End

        }

        private void button11_Click(object sender, EventArgs e)
        {

            string start_directory = textBox12.Text;
            string end_directory = textBox10.Text;

            bool finished = false;
            while (!finished)
            {
                bool answer = CheckIfPath(textBox10.Text);
                if (answer == false)
                {
                    string errortext = "Please choose a valid path for the directory where the data should be transferred from";
                    popupError(errortext);
                    break;
                }


                answer = CheckIfPath(textBox7.Text);
                if (answer == false)
                {
                    string errortext = "Please choose a valid path for the directory the data should be transferred to";
                    popupError(errortext);
                    break;
                }



                label14.Text = TransferAllFiles(start_directory, end_directory);


                label14.Visible = true;
                label12.Visible = true;

                CreateSaveFileTxt();

                break;
            } //While Loop End

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }







        //Methods


        public string ChooseFolder()
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

                string text = folderBrowserDialog1.SelectedPath;
                return text;
            }
            return text;
        }

        public void ChooseDataContainerFolder()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }



        public string GetDateToday()
        {
            DateTime thisday = DateTime.Today;
            string thisdaystring = thisday.ToString("d");
            return thisdaystring;
        }

        public string TransformDate(string thisdaystring)
        {
            (string s_year, string s_month, string s_day) = GetYearDayMonth(thisdaystring);
            string thisday_folderdate = CreateThisDayFolderDate(s_year, s_month, s_day);
            return thisday_folderdate;
        }

        public bool CheckCorrectDate(string text)
        {
            bool answer = text.Contains(".");
            return answer;
        }

        //Create the Name for the todays folder
        static string CreateThisDayFolderDate(string s_year, string s_month, string s_day)
        {
            string thisday_folderdate = s_year + s_month + s_day;
  
            return thisday_folderdate;
        }


        public string CreateThisDayFolderName()
        {
            IList<string> monthnames = new List<string> { "Januar", "Februar", "März", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "Novemebr", "Dezember" };

    
            string thisday_folderdate = textBox2.Text;

            string month = thisday_folderdate.Substring(2, 2);
            string thisday_foldername = thisday_folderdate + " " + monthnames[Convert.ToInt32(month) - 1];

            return thisday_foldername;
        }

        static (string s_year, string s_month, string s_day) GetYearDayMonth(string thisdaystring)
        {
            string s_year = thisdaystring.Substring(8);
            Console.WriteLine(s_year);
            string s_month = thisdaystring.Substring(3, 2);
            Console.WriteLine(s_month);
            string s_day = thisdaystring.Substring(0, 2);
            Console.WriteLine(s_day);
            return (s_year, s_month, s_day);
        }

        public void ShowSubFolderTextBoxes()
        {
            label3.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            button12.Visible = true;

        }

        public void HideSubFolderTextBoxes()
        {
            label3.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            button12.Visible = false;
        }


        public void ShowSpecificDateChoice()
        {
            label6.Visible = true;
            textBox9.Visible = true;
            button4.Visible = true;
        }
        public void HideSpecificDateChoice()
        {
            label6.Visible = false;
            textBox9.Visible = false;
            button4.Visible = false;
        }


        public void popupError(string errortext)
        {
            Form2.text = errortext; //Sent Error Message Here with text variable
            Form2 popup = new Form2();
            popup.Show();
        }

        public void infoForm()
        {

            Form3 info = new Form3();
            info.Show();
        }



        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        static List<string> GetFolderList(string main_folderpath)
        {
            IList<String> folder_list1 = new List<String> { };
            string[] subdirectories = Directory.GetDirectories(main_folderpath);
            foreach (string directory in subdirectories)
            {
                folder_list1.Add(directory);
            }
            return (List<string>)folder_list1;
        }


        public string GetPreviousFolderPath(IList<string> folder_list1)
        {
            IList<String> foldername_list = new List<String> { };
            IList<int> folderdate_list = new List<int> { };

            bool finished = false;
            while (!finished)
            {


                foreach (string folder in folder_list1)
                {
                    string foldername = GetPreviousFolderName(folder);
                    foldername_list.Add(foldername.Substring(0, 6));
                }

                foreach (string date in foldername_list)
                {
                    int i;
                    bool success = int.TryParse(date, out i);
                    folderdate_list.Add(i);
                }

                if (folderdate_list.Count() == 0)
                {
                    string errortext = "A dated folder previously created by DataMover doesn't exist";
                    popupError(errortext);
                    break;
                }


                int max = folderdate_list.Max();


                string datetoday = GetDateToday();

                datetoday = TransformDate(datetoday);



                if (max == Int32.Parse(datetoday))
                {
                    folderdate_list.Remove(max);
                }

          

                max = folderdate_list.Max();
                string max_string = max.ToString();
                string previous_folder_path = "";

                foreach (string folder in folder_list1)
                {
                    if (folder.Contains(max_string))
                    {
                        previous_folder_path = folder;
                    }
                }

                return previous_folder_path;
                
            } // End While Loop
            return "1";
        }


        static string GetPreviousFolderName(string previous_folder_path)
        {

            string previous_folder_name = "No Name";
            int index = previous_folder_path.LastIndexOf('\\');

            if (index != -1)
            {
                previous_folder_name = previous_folder_path.Substring(index + 1);
            }

            return previous_folder_name;

        }

        static string GetPreviousFolderDate(string previous_folder_name)
        {

            string previous_folder_date = "0";

            previous_folder_date = previous_folder_name.Substring(0, 6);

            return previous_folder_date;
        }



        static int GetSubFolderCount(string main_folderpath)
        {
            int count = 0;
            string[] subdirectories = Directory.GetDirectories(main_folderpath);

            foreach (string directory in subdirectories)
            {
                count++;
            }
            return count;
        }


        static bool CheckIfPath(string text)
        {
            bool answer = text.Contains("\\");
            return answer;
        }

        static string CreateSubFolder(string target_folder_path, string sub_folder_name)
        {
            string folderpath = target_folder_path + @"\" + sub_folder_name;
            Directory.CreateDirectory(folderpath);
            return folderpath;
        }


        // Transfer all files since the previous directory date into the newly created subfolder
        public string TransferFilesBasedOnDate()
        {
            string start_directory = textBox10.Text;
            string end_directory = textBox7.Text;
            string previous_folder_date = textBox9.Text; ;

            DirectoryInfo dir = new DirectoryInfo(start_directory);
            FileInfo[] files = dir.GetFiles();

            string first_file_to_transfer;
            bool first_file_found = false;
            int transfer_count = 0;

            foreach (FileInfo f in files)
            {
                if (f.Name.Contains(previous_folder_date.Substring(0, 2)) == true && f.Name.Contains(previous_folder_date.Substring(2, 2)) == true && f.Name.Contains(previous_folder_date.Substring(4, 2)) == true)
                {
                    first_file_to_transfer = f.Name;


                    first_file_found = true;
                }

                if (first_file_found == true)
                {
                    f.MoveTo($@"{end_directory}\{f.Name}");
                    transfer_count++;
                }
            }
            string transfer_count_string = Convert.ToString(transfer_count);

            return transfer_count_string;
        }


        // Transfer all files from one folder back to the phone
        static string TransferAllFiles(string start_directory, string end_directory)
        {

            DirectoryInfo dir = new DirectoryInfo(start_directory);
            FileInfo[] files = dir.GetFiles();

            int transfer_count = 0;

            foreach (FileInfo f in files)
            {
                f.MoveTo($@"{end_directory}\{f.Name}");
                transfer_count++;
            }
            string transfer_count_string = Convert.ToString(transfer_count);
            return transfer_count_string;
        }


        public void CreateSaveFileTxt()
        {
            string file_name = "Datamover_Save_File";
            string working_directory = textBox1.Text;
            string datetoday = textBox2.Text;
            string main_folderpath = textBox6.Text;

            string start_directory = textBox10.Text;
            string end_directory = textBox7.Text;
            string back_directory = textBox12.Text;

            string path = working_directory + @"\" + file_name;


            string transfer_count = label13.Text;
            string transfer_count_back = label14.Text;


            IList<String> sub_foldernames = new List<String> { textBox8.Text, textBox3.Text, textBox4.Text, textBox5.Text };

            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Name Save File: " + file_name);
                    sw.WriteLine("Save Date: " + datetoday);
                    sw.WriteLine("Working Directory: " + working_directory);
                    sw.WriteLine("Start Directory: " + start_directory);
                    sw.WriteLine("End Directory: " + end_directory);
                    sw.WriteLine("Back Directory: " + back_directory);
                    sw.WriteLine(" ");
                    sw.WriteLine("File Transfered: " + transfer_count);
                    sw.WriteLine("File Transfered Back: " + transfer_count_back);
                    sw.WriteLine(" ");
                    sw.WriteLine("Main Folder: "+ main_folderpath);
                    sw.WriteLine(" ");
                    sw.WriteLine("Subfolder Names:");
                    for (int i = 0; i < sub_foldernames.Count; i++)
                    {
                        if (sub_foldernames[i] != "")
                        {
                            sw.WriteLine(sub_foldernames[i]);
                        }
                    }


                }
            }
        }

        public string ExchangeMainFolderNameInPath(string path)
        {
            bool finished = false;
            while (!finished)
            {

                string save_file = "Datamover_Save_File";
                string file_path = textBox1.Text + @"\" + save_file;
                string previous_folder = "";


                int count = 0;

                if (File.Exists(file_path))
                {
                    var lines = File.ReadAllLines(file_path);
                    foreach (var line in lines)
                    {
                        count++;
                        var index = line.IndexOf(":");

                        if (count == 11)
                        {
                            previous_folder = line.Substring(index + 2);

                        }
                    }
                }


                string cut_path = path.Replace(textBox1.Text, "");
                cut_path = cut_path.Substring(1);

                string cut_path2;

                if (cut_path == previous_folder)
                {
                    cut_path2 = "";

                }
                else
                {
                    cut_path2 = cut_path.Replace(previous_folder, "");
                    cut_path2 = cut_path2.Substring(1);
                }

                bool answer = CheckCorrectDate(textBox2.Text);
                if (answer == true)
                {
                    textBox2.Text = TransformDate(textBox2.Text);

                }

                string new_dated_folder = CreateThisDayFolderName();
                new_dated_folder = new_dated_folder + " " + textBox6.Text;


                string new_path = textBox1.Text + @"\" + new_dated_folder + @"\" + cut_path2;
                if(cut_path2 == ""){ 
                 new_path = new_path.Substring(0, new_path.Length - 1);
                }


                return new_path;


            }
            return "1";

         }

        public void ReadDataFromTxtTranfersPath()
        {
            string save_file = "Datamover_Save_File";
            string path = textBox1.Text + @"\" + save_file;
            string new_path = "";

            int count = 0;

            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    count++;
                    var index = line.IndexOf(":");




                  if (count == 5)
                    {
                        if (line != "") { 
                        new_path = ExchangeMainFolderNameInPath(line.Substring(index + 2));
                        textBox7.Text = new_path;
                        }

                    }

                    if (count == 6)
                    {
                        line.Substring(index + 2);
                        if (line != "")
                        {
                            new_path = ExchangeMainFolderNameInPath(line.Substring(index + 2));
                            textBox12.Text = new_path;
                        }

                    }


                }
            }
        }

        public void ReadDataFromTxt()
        {
            string save_file = "Datamover_Save_File";
            string path = textBox1.Text + @"\" + save_file;

            if (File.Exists(path))
            {

                var lines = File.ReadAllLines(path);

                bool answer = false;
                string line_content = "";

                int count = 0;

                foreach (var line in lines)
               {
                    count++;
                    var index = line.IndexOf(":");

                    if (count == 2)
                    {
   

                       button2.PerformClick();
                       radioButton2.PerformClick();
                       button4.PerformClick();
                    }

                    if (count == 4)
                    {
                       textBox10.Text = line.Substring(index + 2);


                    }

                    if (count == 11)
                    {
                        line_content = line.Substring(index + 2);
                        answer = CheckIfPath(line_content);
                        if (answer == true) { line_content = GetPreviousFolderName(line_content);
                        }
                        else {
                            line_content = line.Substring(index + 2); }
                     
                        if (line_content.Length > 0)
                        {
                            int i = line_content.IndexOf(" ") + 1;
                            line_content = line_content.Substring(i);
                        }

                        if (line_content.Length > 0)
                        {
                            int i = line_content.IndexOf(" ") + 1;
                            line_content = line_content.Substring(i);
                        }

                        textBox6.Text = line_content;



                    }


                if (count == 14)
                {
                    line_content = line.Substring(index + 2);
                    answer = CheckIfPath(line_content);
                    if (answer == true)
                    {
                        line_content = GetPreviousFolderName(line_content);
                        textBox8.Text = line_content;
                    }
                    else { textBox8.Text = line; }

                }

                if (count == 15)
                {
                    line_content = line.Substring(index + 2);

                    if (line_content != "")
                        {

                            checkBox1.Checked = true;
                        }

                    answer = CheckIfPath(line_content);
                    if (answer == true)
                    {
                        line_content = GetPreviousFolderName(line_content);
                        textBox3.Text = line_content;
                    }
                    else { textBox3.Text = line; }

                }

                if (count == 16)
                {
                    line_content = line.Substring(index + 2);

                        if (line_content != "")
                        {
                            checkBox1.Checked = true;
                        }

                        answer = CheckIfPath(line_content);
                    if (answer == true)
                    {
                        line_content = GetPreviousFolderName(line_content);
                        textBox4.Text = line_content;
                    }
                    else { textBox4.Text = line; }

                }

                if (count == 17)
                {
                    line_content = line.Substring(index + 2);

                        if (line_content != "")
                        {
                            checkBox1.Checked = true;
                        }

                        answer = CheckIfPath(line_content);
                    if (answer == true)
                    {
                        line_content = GetPreviousFolderName(line_content);
                        textBox5.Text = line_content;
                    }
                    else { textBox5.Text = line; }

                

                 }



                }

            }


        }





    }
}


