using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace FileDescriptor
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataTable table;

        public MainWindow()
        {
            InitializeComponent();

            // Create a DataTable for file details
            table = new DataTable();
            table.Columns.Add("Path");
            table.Columns.Add("Name");
            table.Columns.Add("Size (in Bytes)");
            table.Columns.Add("Created");
            table.Columns.Add("Last edit");
            table.Columns.Add("Computer");
            table.Columns.Add("Medium");

            fileDetails.ItemsSource = table.DefaultView;
            
        }

        private void OpenExplorer(object sender, RoutedEventArgs e)
        {
            // Anytime the button has been pressed, a new FileDialog displays 
            OpenFileDialog oFd = new OpenFileDialog();
            
            int count = 1;
            int index = 0;
            
            File[] files = new File[count];
            File f = new File(oFd);
            files[index] = f;

            if (f.showDialog())
            {
                // For each file which has been open in the OpenFileDialog, a new row with the file properties generates
                foreach (File file in files)
                { 
                    table.Rows.Add(file.getPath(), file.getName(), file.getSize(), file.getCreationDate(), file.getEditDate(), file.getComputerName(), file.getMedium());
                }
            }
            count++;
            
        }

    }

    public class File
    {
        private OpenFileDialog oFd;
        public DialogResult result;

        public File(OpenFileDialog oFd)
        {
            this.oFd = oFd;
        }

        public bool showDialog()
        {
            // If the file is valid the method returns true or when not it returns false. When true returns, then the file properties are displayed in the table
            result = oFd.ShowDialog();
            if (result == DialogResult.OK)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public string getPath()
        {
            return oFd.FileName;
        }

        public string getName()
        {
            return Path.GetFileName(oFd.FileName);
        }

        public long getSize()
        {
            return new FileInfo(oFd.FileName).Length;
        }

        public DateTime getCreationDate()
        {
            return new FileInfo(oFd.FileName).CreationTime;
        }

        public DateTime getEditDate()
        {
            return new FileInfo(oFd.FileName).LastWriteTime;
        }

        public string getComputerName()
        {
            return System.Net.Dns.GetHostName();
        }

        public string getMedium()
        {
            string driveName = "C";
            DriveInfo drive = new DriveInfo(driveName);
            return drive.VolumeLabel;
        }
    }
}

