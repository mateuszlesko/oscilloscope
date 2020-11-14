using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanko.Services
{
    class FileAdder
    {
        string name { get; set; }
        string path { get; set; }
        string[] data { get; set; }
        int n;
        public FileAdder(string path, string name, string [] data)
        {
            this.path = path;
            this.name = name;
            this.data = data;
            n = 0;
        }

        static Boolean IsFileExist(string path)
        {
            if (File.Exists(path))
                return true;

            return false;
        }


        static Boolean IsDirExist(string path)
        {
            if (Directory.Exists(path))
                return true;

            return false;
        }


        public  Boolean FileCreate()
        {
            //Console.Write(path);
            try
            {
                //  if (IsDirExist(path))
                //  {
                Console.Write(path + @"\" + name + (n <= 0 ? "" : n.ToString()));
                    using (FileStream fileStream = File.Create(path + @"\" + name + (n <= 0 ? "" : n.ToString()) , 1024))
                    {
                        Byte[] dataByte;
                        foreach (string str in data)
                        {
                            dataByte = new UTF8Encoding(true).GetBytes(str);
                            fileStream.Write(dataByte, 0, str.Length);
                        }
                        
                    }
            }
            catch(System.IO.DirectoryNotFoundException directoryException)
            {
                Directory.CreateDirectory(path);
                FileCreate();
            }
            catch (Exception e)
            {
                Console.WriteLine("Blad ");
                return false;
            }
            n++;
            return true;
        }

    }
}
