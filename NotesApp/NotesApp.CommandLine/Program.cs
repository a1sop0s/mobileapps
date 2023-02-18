using NotesApp.CommandLine.Models;
using System;
using System.Collections.Generic;
using static System.Guid;

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
        
        private static void Main(string[] args)
        {
            var run = true;
            
            
            var sqlService = new SqlService();
            var conn = sqlService.CreateConnection();
            sqlService.CreateTable(conn);

            while (run)
            {
                Console.Write(  
                 @"
                display - Display notes
                add - Add note
                edit - Edit note
                delete - Delete note
                exit - Exit the app
                >  ");
                var answer = Console.ReadLine()?.ToLower() ?? "add";
                Console.Clear();

                List<Note> notes;
                switch (answer)
                {
                    default: // Display notes
                        notes = SqlService.ReadData(conn);
                        Console.WriteLine("Here's your notes:");
                        ListNotes(notes);
                        break;
                    case "add":
                        Console.WriteLine("Add note\n------------");
                        Console.Write("Title\n> ");
                        var title = Console.ReadLine();
                        Console.WriteLine("---");
                        
                        Console.Write("Content\n> ");
                        var body = Console.ReadLine();

                        var note = new Note(NewGuid().ToString(), title, body, DateTime.Now);
                        sqlService.InsertData(conn, note);
                        
                        Console.WriteLine($"Successfully created note {title}.");
                        break;
                    case "edit":
                        notes = SqlService.ReadData(conn);
                        ListNotes(notes);
                        
                        Console.Write("Please type the index of the note you'd like to change\n> ");
                        var toEdit = int.Parse(Console.ReadLine() ?? "0");

                        if (toEdit > notes.Count || toEdit < 0)
                        {
                            Console.WriteLine($"There is no note with the index of {toEdit}");
                            break;
                        } 

                        Console.Write("Which field would you like to change? (Heading / Content)\n> ");
                        var fieldChoice = Console.ReadLine()?.ToLower();
                        
                        switch (fieldChoice)
                        {
                            case "heading": case "title":
                                Console.Write("What would you like the new value of Heading to be?\n> ");
                                var headValue = Console.ReadLine();
                                
                                sqlService.UpdateData(conn, notes[toEdit], "Heading", headValue);
                                break;
                            case "content": case "body":
                                Console.Write("What would you like the new value of Content to be?\n> ");
                                var contValue = Console.ReadLine();

                                sqlService.UpdateData(conn, notes[toEdit], "Content", contValue);
                                break;
                        }
                        
                        Console.WriteLine($"Successfully edited the note with the index of {toEdit}");
                        break;
                    case "delete":
                        notes = SqlService.ReadData(conn);
                        ListNotes(notes);
                        
                        Console.Write("Which note would you like to delete?\n> ");
                        var toRemove = int.Parse(Console.ReadLine() ?? "0");
                        if (toRemove > notes.Count || toRemove < 0)
                        {
                            Console.WriteLine($"There is no note with the index of {toRemove}");
                            break;
                        }

                        sqlService.RemoveData(conn, notes[toRemove]);
                        Console.WriteLine($"Successfully deleted the note with the index of {toRemove}");
                        break;
                    case "exit":
                        Console.WriteLine("Goodbye.");
                        run = false;
                        break;
                }
            }            
        }
    }
}
