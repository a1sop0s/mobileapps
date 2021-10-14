using NotesApp.CommandLine.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.CommandLine
{
    public class SqlService
    {
        private static string _tableName = "NotesTable";
        public SqlService()
        {
            
        }

        public SQLiteConnection CreateConnection()
        {
            var sqliteConn = new SQLiteConnection("Data Source=database.dbL;Version=3;New=True;Compress=True;");
            sqliteConn.Open();
            return sqliteConn;
        }

        public void CreateTable(SQLiteConnection conn)
        {
            string createSql = $"CREATE TABLE IF NOT EXISTS {_tableName} (id TEXT, heading TEXT, content TEXT, datetime TEXT )";
            var sqliteCmd = conn.CreateCommand();
            sqliteCmd.CommandText = createSql;
            sqliteCmd.ExecuteNonQuery();
        }

        public void InsertData(SQLiteConnection conn, Note note)
        {
            var sqliteCmd = conn.CreateCommand();
            sqliteCmd.CommandText = $"INSERT INTO {_tableName} (id, heading, content, datetime) VALUES ('{note.Id}', '{note.Heading}', '{note.Content}', '{note.DateTime}')";
            sqliteCmd.ExecuteNonQuery();
        }

        public void RemoveData(SQLiteConnection conn, Note note)
        {
            var sqliteCmd = conn.CreateCommand();
            sqliteCmd.CommandText = $"DELETE FROM {_tableName} WHERE id=\"{note.Id}\"";
            sqliteCmd.ExecuteNonQuery();
        }

        public void UpdateData(SQLiteConnection conn, Note note, string column, string value)
        {
            var sqliteCmd = conn.CreateCommand();
            sqliteCmd.CommandText = $"UPDATE {_tableName} SET {column} = \"{value}\"";
            sqliteCmd.ExecuteNonQuery();
        }

        public static List<Note> ReadData(SQLiteConnection conn)
        {
            var notesList = new List<Note>();
            var sqliteCmd = conn.CreateCommand();
            sqliteCmd.CommandText = $"SELECT * FROM {_tableName}";
            var sqliteDatareader = sqliteCmd.ExecuteReader();
            while (sqliteDatareader.Read())
            {
                var note = new Note(sqliteDatareader.GetString(0), 
                    sqliteDatareader.GetString(1), 
                    sqliteDatareader.GetString(2),
                    DateTime.Parse(sqliteDatareader.GetString(3)));                
                notesList.Add((Note)note);
            }
            return notesList;
        }

        //Modify
    }
}
