using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RealEstate.Messaging
{
    public class MessageLogger
    {
        private readonly string _connectionString;

        private const string InsertSqlCmd = @"INSERT INTO Messages
                                            (MessageType, Message, DateCreated)
                                            VALUES (@MessageType, @Message, @DateCreated)";

        public MessageLogger(string connectionString)
        {
            _connectionString = connectionString;
        }

        static async Task<int> ExecuteQuery(SqlConnection conn, SqlCommand cmd)
        {
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            return 1;
        }

        public async Task<int> LogAsync(IMessage message)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(InsertSqlCmd, conn);
                    cmd.Parameters.AddWithValue("@MessageType", message.MessageType.ToString());
                    cmd.Parameters.AddWithValue("@Message", message.Message);
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.UtcNow);

                    await ExecuteQuery(conn, cmd);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return 1;
        }
    }
}