using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Programowanko.Services
{
    public class FileReader
    {

        String Path
        {
            get;
            set;
        }

        DialogResult Status
        {
            get;
            set;
        }


        public FileReader()
        {
            FileOpenResult fileOpen = new FileOpenResult().FileDialogOpen();

            this.Path = fileOpen.GetPath();
            this.Status = fileOpen.GetStatus();

        }

        class ContentResult
        {
            string[] Content;

            public ContentResult(string[] Content)
            {
                this.Content = Content;
            }

            public string[] GetContent()
            {
                return Content;
            }

            public Boolean IsEmpty()
            {
                return Content.Equals(null);
            }
        }

        class FileOpenResult
        {
            String Path;
            DialogResult DialogResult;

            public String GetPath()
            {
                return Path;
            }

            public DialogResult GetStatus()
            {
                return DialogResult;
            }

            public FileOpenResult FileDialogOpen()
            {
                FileOpenResult openResult = new FileOpenResult();
                using (OpenFileDialog fileDialog = new OpenFileDialog())
                {
                    DialogResult result = fileDialog.ShowDialog();
                    fileDialog.InitialDirectory = @"c:\\";
                    fileDialog.Filter = "txt files (*.txt)|*.txt"; 
                    fileDialog.FilterIndex = 2;
                    fileDialog.RestoreDirectory = true;
                    fileDialog.Title = "Załaduj plik z wynikami pomiarów";
                    //zabezpieczenie, ze zostanie zaladowany plik z poprawnym rozszerzeniem
                    if (fileDialog.ShowDialog().Equals(DialogResult.OK) && System.IO.Path.GetExtension(fileDialog.FileName)==".txt")
                    {
                        Console.WriteLine(fileDialog.FileName);
                        openResult.Path = fileDialog.FileName;
                        openResult.DialogResult = DialogResult.OK;
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        openResult.Path = "";
                        openResult.DialogResult = DialogResult.None;
                    }
                    else
                    {
                        openResult.Path = "";
                        openResult.DialogResult = DialogResult.None;
                    }
                }
                return openResult;
            }
        }

        private ContentResult _GetContent()
        {
            char[] symbol = new char[] { ';' };
            ContentResult content = null;

            if (!Status.Equals(DialogResult.None))
            {
                try
                {
                    string[] Lines = File.ReadAllLines(Path);
                    
                    for (int i = 1; i < Lines.Length; i++)
                    {
                        Lines[i-1] = Lines[i].Trim(symbol);
                    }
     
                    content = new ContentResult(Lines);

                }
                catch (IOException exception)
                {
                    Console.WriteLine(exception.Message);
                    content = new ContentResult(new string[0]);
                }
            }
            else
            {
                Console.WriteLine("error");
            }

            return content;
        }

        public string[] GetContent()
        {
            ContentResult content = _GetContent();
            if(Status == DialogResult.None)
            {
                return new string[] {""};
            }
            return content.GetContent();
        }

        public void PrintContent()
        {
            if (!Status.Equals(DialogResult.None))
            {
                try
                {
                    using (StreamReader stream = new StreamReader(this.Path))
                    {
                        String Line = stream.ReadToEnd();
                        Console.WriteLine(Line);
                    }
                }
                catch (IOException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            else
            {
                Console.WriteLine("error");
            }
        }


    }
}