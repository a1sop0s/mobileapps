using NotesApp.CommandLine.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Reflection;

namespace NotesApp.CommandLine
{
    internal static class Program
    {
        private static void ListNotes(List<Note> n)
        {
            if (n.Count > 0)
            {
                for (var i = 0; i < n.Count; i++)
                {
                    Console.WriteLine($"Index: {i}");
                    Console.WriteLine($"Title: {n[i].Heading}");
                    Console.WriteLine($"Content: {n[i].Content}");
                    Console.WriteLine($"Date: {n[i].DateTime}");
                    for (var j = 0; j < ("Content: ".Length + n[i].Content.Length + 5); j++) Console.Write("-");
                    Console.WriteLine();
                }
            } else Console.WriteLine("No notes to display!");
        }
        
        static void Main(string[] args)
        {
            var run = true;
            
            
            var sqlService = new SqlService();
            var conn = sqlService.CreateConnection();
            sqlService.CreateTable(conn);

            while (run)
            {
                Console.Write(  
                 @"
                1.Display notes
                2.Add note
                3.Edit note
                4.Delete note
                5.Exit
                >  ");
                var answer = int.Parse(Console.ReadLine() ?? "1");
                Console.Clear();

                List<Note> notes;
                switch (answer)
                {
                    case 1: // Display
                        notes = SqlService.ReadData(conn);
                        Console.WriteLine("Here's your notes:");
                        ListNotes(notes);
                        break;
                    case 2: // Add
                        Console.WriteLine("Add note\n------------");
                        Console.Write("Title\n> ");
                        var title = Console.ReadLine();
                        Console.WriteLine("---");
                        Console.Write("Content\n> ");
                        var body = Console.ReadLine();

                        var note = new Note(Guid.NewGuid().ToString(), title, body, DateTime.Now);
                        sqlService.InsertData(conn, note);
                        
                        Console.WriteLine($"Successfully created note {title}.");
                        break;
                    case 3: // Edit
                        notes = SqlService.ReadData(conn);
                        ListNotes(notes);
                        
                        Console.Write("Please type the index of the note you'd like to change\n> ");
                        var toEdit = int.Parse(Console.ReadLine() ?? "0");
                        
                        Console.Write("Which field would you like to change? (Heading / Content)\n> ");
                        var fieldChoice = Console.ReadLine()?.ToLower();
                        
                        switch (fieldChoice)
                        {
                            case "heading": case "title":
                                Console.Write("What would you like the new value of Heading to be?\n> ");
                                var headValue = Console.ReadLine();
                                
                                sqlService.UpdateData(conn, notes[toEdit], "Heading", headValue);// notes[toEdit].Heading = Console.ReadLine();
                                break;
                            case "content": case "body":
                                Console.Write("What would you like the new value of Content to be?\n> ");
                                var contValue = Console.ReadLine();
                                
                                sqlService.UpdateData(conn, notes[toEdit], "Content", contValue);
                                break;
                        }
                        
                        Console.WriteLine($"Successfully edited the note with the index of {toEdit}");
                        break;
                    case 4: // Delete
                        notes = SqlService.ReadData(conn);
                        ListNotes(notes);
                        
                        Console.Write("Which note would you like to delete?\n> ");
                        var toRemove = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine();
                        
                        sqlService.RemoveData(conn, notes[toRemove]);
                        Console.WriteLine($"Successfully deleted the note with the index of {toRemove}");
                        break;
                    case 5: // Exit
                        Console.WriteLine("Goodbye.");
                        run = false;
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
            }            
        }
    }
}
