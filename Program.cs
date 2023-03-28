using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string file = "tickets.txt";
        string choice;

        do
        {
            Console.WriteLine("\n1) Read data from file.");
            Console.WriteLine("2) Create file from data.");
            Console.WriteLine("Enter any other key to exit.");

            choice = Console.ReadLine();

            if (choice == "1")
            {
                if (File.Exists(file))
                {
                    List<Ticket> tickets = ReadTicketsFromFile(file);
                    foreach (Ticket ticket in tickets)
                    {
                        Console.WriteLine(ticket);
                    }
                }
                else
                {
                    Console.WriteLine("File does not exist");
                }
            }
            else if (choice == "2")
            {
                List<Ticket> tickets = new List<Ticket>();

                do
                {
                    Console.WriteLine("Enter a new ticket (Y/N)?");
                    choice = Console.ReadLine().ToUpper();
                    if (choice != "Y") { break; }

                    Ticket ticket = CreateTicket();
                    tickets.Add(ticket);

                } while (choice == "Y");

                WriteTicketsToFile(file, tickets);
            }

        } while (choice == "1" || choice == "2" || choice == "N");
    }

    static Ticket CreateTicket()
    {
        Console.WriteLine("Enter the TicketID.");
        string ticketID = Console.ReadLine();

        Console.WriteLine("Enter the summary.");
        string summary = Console.ReadLine();

        Console.WriteLine("Enter the status.");
        string status = Console.ReadLine();

        Console.WriteLine("Enter the priority.");
        string priority = Console.ReadLine();

        Console.WriteLine("Enter the submitter.");
        string submitter = Console.ReadLine();

        Console.WriteLine("Enter who was assigned.");
        string assigned = Console.ReadLine();

        List<string> watching = new List<string>();
        string choice;

        do
        {
            Console.WriteLine("Enter who is watching.");
            string watcher = Console.ReadLine();
            watching.Add(watcher);

            Console.WriteLine("Is there another person watching (Y/N)?");
            choice = Console.ReadLine().ToUpper();

        } while (choice == "Y");

        return new Ticket(ticketID, summary, status, priority, submitter, assigned, watching);
    }

    static void WriteTicketsToFile(string fileName, List<Ticket> tickets)
    {
        StreamWriter sw = new StreamWriter(fileName);

        foreach (Ticket ticket in tickets)
        {
            sw.WriteLine(ticket.ToString());
        }

        sw.Close();
    }

    static List<Ticket> ReadTicketsFromFile(string fileName)
    {
        List<Ticket> tickets = new List<Ticket>();

        StreamReader sr = new StreamReader(fileName);

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            string[] parts = line.Split(',');

            string ticketID = parts[0];
            string summary = parts[1];
            string status = parts[2];
            string priority = parts[3];
            string submitter = parts[4];
            string assigned = parts[5];
            List<string> watching = new List<string>(parts[6].Split('|'));

            Ticket ticket = new Ticket(ticketID, summary, status, priority, submitter, assigned, watching);
            tickets.Add(ticket);
        }

        sr.Close();

        return tickets;
    }
}