using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomMates.Models;

public class ChoreCount
{
    public int Count { get; set; }
    public Roommate Roommate { get; set; }
}
