using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wiki_server.Models;

/**
    Old context to connect with MySQL without ORM  
*/
namespace wiki_server.Services 
{
    public class DatabaseContext {
        public string ConnectionString { get; set; }

        public DatabaseContext(string connectionString) {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() {
            return new MySqlConnection(ConnectionString);
        }

        public List<WikiItem> FindAllPages() {
            List<WikiItem> list = new List<WikiItem>();

            using (MySqlConnection conn = GetConnection()) {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM pages", conn);

                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        list.Add(new WikiItem() {
                            pageid = Convert.ToInt32(reader["pageid"]),
                            title = reader["title"].ToString(),
                            snippet = reader["snippet"].ToString(),
                            timestamp = reader["timestamp"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        public int UpdateWikiItem(WikiItem it) {
            using (MySqlConnection conn = GetConnection()) {
                conn.Open();
                string command = $"UPDATE pages " +
                    $"SET title={it.title}, snippet={it.snippet} " +
                    $"WHERE pageid={it.pageid}";

                MySqlCommand cmd = new MySqlCommand(command, conn);
                int row_count = cmd.ExecuteNonQuery();
                return row_count;
            }
            throw new NotImplementedException();
        }

        public int InsertWikiItem(WikiItem item)
        {
            /*title - уникальное, не длиннее 128 символов
            snippet
            pageid и timestamp должны заполняться автоматически*/
            using (MySqlConnection conn = GetConnection()) {
                conn.Open();
                string command = $"INSERT INTO pages(title, snippet, timestamp) " +
                    $"VALUES('{item.title}', '{item.snippet}', '{item.timestamp}')";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                int row_count = cmd.ExecuteNonQuery();
                return row_count;
            }
            throw new NotImplementedException();
        }

        public int DeleleWikiItemById(int pageid)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string command = $"DELETE FROM pages WHERE pageid={pageid}";
                MySqlCommand cmd = new MySqlCommand(command, conn);
                int row_count = cmd.ExecuteNonQuery();
                return row_count;
            }
            throw new NotImplementedException();
        }

        public List<WikiItem> FindPageByContainText(string text)
        {
            List<WikiItem> list = new List<WikiItem>();

            using (MySqlConnection conn = GetConnection()) {
                conn.Open();
                string command = $"SELECT * FROM pages WHERE title LIKE '%{text}%'";
                MySqlCommand cmd = new MySqlCommand(command, conn);

                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        list.Add(new WikiItem() {
                            pageid = Convert.ToInt32(reader["pageid"]),
                            title = reader["title"].ToString(),
                            snippet = reader["snippet"].ToString(),
                            timestamp = reader["timestamp"].ToString(),
                        });
                    }
                }
            }
            return list;
        }
    }
}
