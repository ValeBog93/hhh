using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TaskVale
{
    class TaskManagementcs
    {
        public static string path { get; } = "C:\\Users\\faggi\\OneDrive\\Desktop\\TaskValentina\\TaskEs.csv";


        // Leggere tutto il contenuto del nostro file TaskEs.csv
        public static void LetturaTask()
        {
            int totalLines = File.ReadLines(path).Count(); 
            Task[] tasks = new Task[totalLines - 1];
            string line;

            using (StreamReader reader = File.OpenText(path))
            {
                string header = reader.ReadLine();
                //Console.WriteLine(header);
                while (!reader.EndOfStream)
                {
                    for (int i = 0; i < totalLines - 1; i++)
                    {
                        line = reader.ReadLine();
                        string[] divisore = line.Split(";");
                        Task compito = new Task
                        {
                            Descrizione = divisore[0],
                            Data = Convert.ToDateTime(divisore[1]),
                            Importanza = divisore[2],
                      
                        };

                        tasks[i] = compito;
                    }
                }
            }

            Task[] compiti = tasks;
            Console.WriteLine("Descizione - Data - Importanza:\n");
            foreach (Task elemento in compiti)
            {
                if (elemento.Importanza== "alto")
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(elemento.Descrizione + " - " + elemento.Data + " - " + elemento.Importanza);
                }
                else if(elemento.Importanza == "basso")
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(elemento.Descrizione + " - " + elemento.Data + " - " + elemento.Importanza);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(elemento.Descrizione + " - " + elemento.Data + " - " + elemento.Importanza);
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }


         }

        // Aggiunta task:  
        public static bool AddTask(Task task)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(path))
                {
                    writer.WriteLine();
                    writer.Write(task.Descrizione.ToLower() + ";" + task.Data + ";" + task.Importanza.ToLower() + ";");
                }
                Console.WriteLine("i tasks sono stati aggiunti correttamente");
                return true;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static void DeleteTask(string descriEliminare)
        {
            int totalLines = File.ReadLines(path).Count();
            Task[] tasks = new Task[totalLines - 1];
            string line;

            using (StreamReader reader = File.OpenText(path))
            {
                string header = reader.ReadLine();
                //Console.WriteLine(header);
                while (!reader.EndOfStream)
                {
                    for (int i = 0; i < totalLines - 1; i++)
                    {
                        line = reader.ReadLine();
                        string[] divisore = line.Split(";");
                        Task compito = new Task
                        {
                            Descrizione = divisore[0],
                            Data = Convert.ToDateTime(divisore[1]),
                            Importanza = divisore[2],

                        };

                        tasks[i] = compito;
                    }
                }
            }

            Task[] compiti = tasks;
            ArrayList newTaskList = new ArrayList();

            foreach (Task tsk in compiti)
            {
                if (tsk.Descrizione != descriEliminare)
                {
                    newTaskList.Add(tsk);
                }
            }

            using (StreamWriter writer = File.CreateText(path))
            {
                writer.Write("FirstName,LastName,Role,Department");
                foreach (Task e in newTaskList)
                {
                    writer.WriteLine();
                    writer.Write(e.Descrizione + ";" + e.Data + ";" + e.Importanza + ";" );

                }
            }
        }
    }
   
}
