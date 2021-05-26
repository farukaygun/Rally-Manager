using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;

public class Database : MonoBehaviour
{
  private static string conString;

  // private void Start()
  // {
  //   dbPath = "URI=file:" + Application.persistentDataPath + "/rallyManagerData.db";
  // }

  // public static void InsertTotblManager(string name, string surname, int age)
  // {
  //   conString = @"Data Source=C:\Users\Faruk\AppData\LocalLow\DefaultCompany\Rally Manager\rallyManagerData.db;Pooling=true;FailIfMissing=false;Version=3";
  //   using (var conn = new SqliteConnection(conString))
  //   {
  //     conn.Open();
  //     Debug.Log(name + " " + surname + " " + age);
  //     using (var cmd = conn.CreateCommand())
  //     {
  //       cmd.CommandType = CommandType.Text;
  //       cmd.CommandText = "INSERT INTO 'tblManager' ('name', 'surname', 'age') VALUES (" 
  //                         + name + ", " 
  //                         + surname + ", " 
  //                         + age + ")";
  //       var result = cmd.ExecuteReader();

  //       Debug.Log("insert schema: " + result);
  //     }

  //     conn.Close();
  //   }
  // }


  public static void InsertTotblManager(string name, string surname, int age)
  {
    conString = @"Data Source=C:\Users\Faruk\AppData\LocalLow\DefaultCompany\Rally Manager\rallyManagerData.db;Pooling=true;FailIfMissing=false;Version=3";
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();
      Debug.Log(name + " " + surname + " " + age);
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblManager' ('name', 'surname', 'age') VALUES " +
                                                    "(@param1, @param2, @param3)";
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", name));
        cmd.Parameters.Add(new SqliteParameter("@param2", surname));
        cmd.Parameters.Add(new SqliteParameter("@param3", age));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }

  public static void InsertTotblManagerSkills(int avaliableSkillPoints, int personManagement, int motivation, int adaptation, int decisiveness)
  {
    conString = @"Data Source=C:\Users\Faruk\AppData\LocalLow\DefaultCompany\Rally Manager\rallyManagerData.db;Pooling=true;FailIfMissing=false;Version=3";
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblManagerSkills' (avaliableSkillPoints, personManagement, motivation, adaptation, decisiveness) VALUES " +
                                                          "(@param1, @param2, @param3, @param4, @param5)";

        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", avaliableSkillPoints));
        cmd.Parameters.Add(new SqliteParameter("@param2", personManagement));
        cmd.Parameters.Add(new SqliteParameter("@param3", motivation));
        cmd.Parameters.Add(new SqliteParameter("@param4", adaptation));
        cmd.Parameters.Add(new SqliteParameter("@param5", decisiveness));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }

  public static void InsertTotblTeam(string name, string avatar, string logo, float budget)
  {
    conString = @"Data Source=C:\Users\Faruk\AppData\LocalLow\DefaultCompany\Rally Manager\rallyManagerData.db;Pooling=true;FailIfMissing=false;Version=3";
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblTeam' (name, avatar, logo, budget) VALUES " +
                                                          "(@param1, @param2, @param3, @param4)";

        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", name));
        cmd.Parameters.Add(new SqliteParameter("@param2", null));
        cmd.Parameters.Add(new SqliteParameter("@param3", null));
        cmd.Parameters.Add(new SqliteParameter("@param4", budget));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }

  public static void InsertTotblGameSettings(string diffuculty)
  {
    conString = @"Data Source=C:\Users\Faruk\AppData\LocalLow\DefaultCompany\Rally Manager\rallyManagerData.db;Pooling=true;FailIfMissing=false;Version=3";
    using (var conn = new SqliteConnection(conString))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "INSERT INTO 'tblGameSettings' (gameDiffuculty) VALUES " +
                                                          "(@param1)";

        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqliteParameter("@param1", diffuculty));

        var result = cmd.ExecuteReader();

        Debug.Log("insert schema: " + result);
      }

      conn.Close();
    }
  }
}
