using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab2Server
{
    class WorkFile
    {
        List<string> attendance = new List<string>() {};
       // List<string> attendanceOld = new List<string>() { };

        public void write (string text)
        {
            StreamWriter file2 = new StreamWriter(@"f:\file.txt");
            attendance.Add(text);
            for (int i = 0; i < attendance.Count; i++)
            {
                file2.WriteLine(attendance[i] + " \n ");
            }           
            file2.Close();
        }


        public string read()
        {
            int counter = 0;
            string text2;
            string text = "";

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(@"f:\file.txt");
            while ((text2 = file.ReadLine()) != null)
            {
                //System.Console.WriteLine("text - " + text2);
                text += text2 + "\n";
                counter++;
            }

            file.Close();
            
            return text;
        }
        public string find(string find)
        {
            string text = "";
            text = attendance.Find(x => x.Contains(find));
            return text;
        }
        public string del(int id)
        {
            string text = "";

            attendance.RemoveAt(id);
            for (int i = 0; i < attendance.Count; i++)
            {
               text  += attendance[i] + " \n ";
            }
            return text;
        }

        public string edit(int id, string txt)
        {
            string text = "";

            attendance[id] = txt;
            for (int i = 0; i < attendance.Count; i++)
            {
                text += attendance[i] + " \n ";
            }
            return text;
        }
    }
}