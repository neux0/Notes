using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NotesAPI.Models;
using System.Security.Cryptography;
namespace NotesAPI.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        SqliteConnection con = new SqliteConnection("Data Source = Notes.db");
        [HttpGet]
        public IActionResult Get(Guid id)
        {
            con.Open();
            SqliteCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Notes WHERE ID = @id";
            cmd.Parameters.AddWithValue("id", id.ToString());
            SqliteDataReader reader = cmd.ExecuteReader();
            if (!reader.Read()) { return NotFound(); }
            List<Note> notes = new List<Note>();
            notes.Add(new Note()
            {
                ID = Guid.Parse(reader.GetString(0)),
                Title = reader.GetString(1),
                Content = reader.GetString(2),
                Visible = reader.GetBoolean(3),
                Date = DateTime.Parse(reader.GetString(4))
            });
            return Ok(notes);
        }
        [HttpGet]
        [Route("GetAllNotes")]
        public IActionResult GetAllNotes()
        {
            con.Open();
            SqliteCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Notes";
            SqliteDataReader reader = cmd.ExecuteReader();
            List<Note> notes = new List<Note>();
            while (reader.Read())
            {
                notes.Add(new Note() 
                {  ID = Guid.Parse(reader.GetString(0)),
                   Title = reader.GetString(1),
                   Content = reader.GetString(2),
                   Visible = reader.GetBoolean(3),
                   Date = DateTime.Parse(reader.GetString(4)) 
                });
            }
            return Ok(notes);
        }
        [HttpPost]
        public IActionResult Post(Note note)
        {
            con.Open();
            SqliteCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO Notes (ID,Title,Content,Visible,Date) VALUES (@id,@title,@content,@visible,@date)";
            cmd.Parameters.AddWithValue("id",Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("title", note.Title);
            cmd.Parameters.AddWithValue("content", note.Content);
            cmd.Parameters.AddWithValue("visible", note.Visible);
            cmd.Parameters.AddWithValue("date", DateTime.Now.ToString());
            cmd.ExecuteNonQuery();
            con.Close();
            return Ok();
        }
        [HttpPut]
        public IActionResult Put(Guid guid, NotePutted note)
        {
            con.Open();
            SqliteCommand cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE Notes SET Title = @title, Content = @content, Visible = @visible, Date = @date WHERE ID = @id";
            cmd.Parameters.AddWithValue("id", guid.ToString());
            cmd.Parameters.AddWithValue("title", note.Title);
            cmd.Parameters.AddWithValue("content", note.Content);
            cmd.Parameters.AddWithValue("visible", note.Visible);
            cmd.Parameters.AddWithValue("date", DateTime.Now.ToString());
            cmd.ExecuteNonQuery();
            con.Close();
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            con.Open();
            SqliteCommand cmd = con.CreateCommand();
            cmd.CommandText = "DELETE FROM Notes WHERE ID = @id";
            cmd.Parameters.AddWithValue("id",guid.ToString());
            cmd.ExecuteNonQuery();
            con.Close();
            return Ok();
        }
    }
}
