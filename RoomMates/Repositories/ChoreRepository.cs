using Microsoft.Data.SqlClient;
using RoomMates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace RoomMates.Repositories;

public class ChoreRepository : BaseRepository
{
    public ChoreRepository(string connectionString) : base(connectionString) { }


    public List<Chore> GetAll()
    {
        using (SqlConnection conn = Connection)
        {
            conn.Open();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, Name FROM Chore;";
                SqlDataReader reader= cmd.ExecuteReader();
                List<Chore> chores= new List<Chore>();
                while (reader.Read())
                {
                    int idColumnPosition = reader.GetOrdinal("Id");
                    int idValue = reader.GetInt32(idColumnPosition);

                    int nameColumnPosition = reader.GetOrdinal("Name");
                    string nameValue = reader.GetString(nameColumnPosition);

                    Chore chore = new Chore
                    {
                        Id = idValue,
                        Name = nameValue
                    };
                    chores.Add(chore);

                }
                reader.Close();
                return chores;
            }
        }
    }


    //Add Dapper NUGET PKG and add using Dapper statement above
    public List<Chore> GetAll2()
    {
        using (SqlConnection conn = Connection)
        {    
            // Create a query that retrieves all books with an author name of "John Smith"    
            var sql = "SELECT * FROM Chore";

            // Use the Query method to execute the query and return a list of objects    
            var chores = conn.Query<Chore>(sql).ToList(); 
            return chores;
        }

    }
    public Chore GetById(int id)
    {
        using (SqlConnection conn = Connection)
        {
            conn.Open();
            using (SqlCommand cmd= conn.CreateCommand())
            {
                cmd.CommandText = "SELECT id, Name FROM Chore WHERE Id = @id ";
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader= cmd.ExecuteReader();
                Chore chore = null;

                if (reader.Read())
                {
                    chore = new Chore 
                    { 
                        Id = id,
                        Name = reader.GetString(reader.GetOrdinal("Name"))
                    };
                }
                reader.Close();
                return chore;
            }
        }
    }

    public void Insert(Chore chore)
    {
        using (SqlConnection conn = Connection)
        {
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO Chore (Name) OUTPUT Inserted.Id
           VALUES (@name)";
                cmd.Parameters.AddWithValue("@Name", chore.Name);
                int id = (int)cmd.ExecuteScalar();
                chore.Id = id;
           }
        }
    }
    public List<Chore> GetUnassignedChores()
    {
        using (SqlConnection conn = Connection)
        {
            // Create a query that retrieves all books with an author name of "John Smith"    
            var sql = @"SELECT c.Id, c.name FROM chore c 
LEFT JOIN RoommateChore rc
ON c.Id = rc.ChoreId
LEFT JOIN Roommate r
ON r.Id = rc.RoommateId
WHERE rc.RoommateId IS NULL";

            // Use the Query method to execute the query and return a list of objects    
            var chores = conn.Query<Chore>(sql).ToList();
            return chores;
        }

    }

    public void AssignChore(int roommateId, int choreId)
    {
        using (SqlConnection conn = Connection)
        {
            var sql = @"SELECT rm.id AS RoommateId, rm.FirstName, rm.LastName, rm.RentPortion, rm.MoveInDate, r.id AS RoomId, r.Name AS RoomName, r.MaxOccupancy AS RoomMaxOccupancy FROM Roommate rm
                JOIN  Room r
                ON r.id = rm.RoomId";

       var identity = conn.QuerySingle<int>("INSERT INTO RoommateChore (RoommateId, ChoreId) output inserted.Id VALUES (@RoomMateId, @ChoreId);"
        , new  { RoomMateId = roommateId, ChoreId = choreId });
        }
    }

}
