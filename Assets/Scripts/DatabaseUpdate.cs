using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class DatabaseUpdate : MonoBehaviour
{
  private static string conString = @"Data Source=C:\Users\Faruk\AppData\LocalLow\DefaultCompany\Rally Manager\rallyManagerData.db;Pooling=true;FailIfMissing=false;Version=3";

  public static void UpdateManagerTeamPilot(int teamID, int id)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "UPDATE 'tblPilot' " +
                          "SET teamID = @param1 " +
                          "WHERE id = @param2";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", teamID));
        cmd.Parameters.Add(new SqliteParameter("@param2", id));

        var result = cmd.ExecuteReader();

        Debug.Log("update schema: " + result);
      }

      conn.Close();
    }
  }

  public static void UpdateManagerTeamCar(int teamID, int id)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "UPDATE 'tblCar' " +
                          "SET teamID = @param1 " +
                          "WHERE id = @param2";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", teamID));
        cmd.Parameters.Add(new SqliteParameter("@param2", id));

        var result = cmd.ExecuteReader();

        Debug.Log("update schema: " + result);
      }

      conn.Close();
    }
  }

  public static void UpdateManagerTeamBudget(string budget)
  {
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "UPDATE 'tblTeam' " +
                          "SET budget = @param1 " +
                          "WHERE id = @param2";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", budget));
        cmd.Parameters.Add(new SqliteParameter("@param2", 1));

        var result = cmd.ExecuteReader();

        Debug.Log("update schema: " + result);
      }

      conn.Close();
    }
  }
}
