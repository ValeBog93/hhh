using System;
using System.IO;
using System.Linq;

namespace TaskVale
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\faggi\\OneDrive\\Desktop\\TaskValentina\\TaskEs.csv";
            int scelta = 0;
            while (scelta!=5)
            {
                Console.WriteLine("Sceliere l'opzione desiderata:\n");
                Console.WriteLine("1. Stampare tasks");
                Console.WriteLine("2. Aggiungere nuovo task");
                Console.WriteLine("3. Eliminare tasks");
                Console.WriteLine("4. Filtrare i Task per importanza");
                Console.WriteLine("5. Uscire dal programma");
                scelta = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                switch (scelta)
                {
                    case 1:
                        TaskManagementcs.LetturaTask();
                        break;
                    case 2:
                        DateTime DataOggi = DateTime.Now;
                        Console.WriteLine("Inserire Descrizione da aggiungere:");
                        string descr = Console.ReadLine();
                        Console.WriteLine("Inserire Data da aggiungere:");

                        DateTime dat = Convert.ToDateTime(Console.ReadLine());
                        while(dat < DataOggi)
                        {
                            Console.WriteLine("Rinserire Data da aggiungere valida:");
                            dat = Convert.ToDateTime(Console.ReadLine());
                        }
                        Console.WriteLine("Inserire importanza da aggiungere:");
                        string imp = Console.ReadLine();
                        Task TaskDaAggiungere = new Task
                        {
                            Descrizione = descr,
                            Data = dat,
                            Importanza =imp,
                        };

                        TaskManagementcs.AddTask(TaskDaAggiungere);
                        break;
                    case 3:
                        Console.WriteLine("Inserire la descrizione da eliminare:");
                        string descriEliminare = Console.ReadLine();
                        TaskManagementcs.DeleteTask(descriEliminare);
                        break;
                    case 4:
                        string sceltaImporanza = "";
                        Console.WriteLine("Inserire l'importanza da visualizzare:");
                        sceltaImporanza = Console.ReadLine().ToLower();

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
                        Task[] FiltrTask = tasks;

                        if (sceltaImporanza == "alto")
                        {
                            foreach (Task task in FiltrTask)
                            {
                                if (task.Importanza == "alto")
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.WriteLine(task.Descrizione + " - " + task.Data + " - " + task.Importanza);
                                }
                                    
                            }
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                        else if (sceltaImporanza == "medio")
                        {
                            foreach (Task task in FiltrTask)
                            {
                                if (task.Importanza == "medio")
                                {
                                    Console.BackgroundColor = ConsoleColor.Yellow;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.WriteLine(task.Descrizione + " - " + task.Data + " - " + task.Importanza);
                                }
                                 

                            }
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                        else
                        {
                            foreach (Task task in FiltrTask)
                            {
                                if (task.Importanza == "basso")
                                {
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.WriteLine(task.Descrizione + " - " + task.Data + " - " + task.Importanza);
                                }
                                  

                            }
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }

                    case 5:
                        Console.WriteLine("Chiusura programma eseguita.");
                        break;
                }

                Console.WriteLine();

            }


        }
    }
}
