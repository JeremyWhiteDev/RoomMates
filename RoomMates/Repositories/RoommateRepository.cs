using Dapper;
using Microsoft.Data.SqlClient;
using RoomMates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomMates.Repositories;

public class RoommateRepository : BaseRepository
{
    public RoommateRepository(string connectionString) : base(connectionString) { }

    public Roommate GetById(int id)
    {
        using (SqlConnection conn = Connection)
        {
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT rm.id AS RoommateId, rm.FirstName, rm.LastName, rm.RentPortion, rm.MoveInDate, r.id AS RoomId, r.Name AS RoomName, r.MaxOccupancy AS RoomMaxOccupancy FROM Roommate rm
                JOIN  Room r
                ON r.id = rm.RoomId
				WHERE rm.id = @id
    ";
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                Roommate roommate = null;

                if (reader.Read())
                {
                    roommate = new Roommate
                    {
                        Id = id,
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                        MovedInDate = reader.GetDateTime(reader.GetOrdinal("MoveInDate")),
                        Room = new Room {
                            Id = reader.GetInt32(reader.GetOrdinal("RoommateId")),
                            Name = reader.GetString(reader.GetOrdinal("RoomName")),
                            MaxOccupancy = reader.GetInt32(reader.GetOrdinal("RoomMaxOccupancy"))}

                    };
                }
                reader.Close();
                return roommate;
            }
        }
    }

    public List<Roommate> GetAll()
    {
        using (SqlConnection conn = Connection)
        {

            var sql = @"SELECT rm.id, rm.FirstName, rm.LastName, rm.RentPortion, rm.MoveInDate, r.id AS RoomId, r.Name AS RoomName, r.MaxOccupancy AS RoomMaxOccupancy FROM Roommate rm
                JOIN  Room r
                ON r.id = rm.RoomId";

            var roommates = conn.Query<Roommate>(sql).ToList();
            return roommates;
        }
    }

}
