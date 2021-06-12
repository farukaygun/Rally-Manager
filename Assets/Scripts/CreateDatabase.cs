using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using System.Threading;

public class CreateDatabase : MonoBehaviour
{
  private static string dbPath;

  private void Start()
  {
    dbPath = "URI=file:" + Application.persistentDataPath + "/rallyManagerData.db";
  }

  public static void CreateSchemas()
  {
    Debug.Log(Application.persistentDataPath + "/rallyManagerData.db");
    using (var conn = new SqliteConnection(dbPath))
    {
      conn.Open();

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS 'tblManager' ( " +
                          " 'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          " 'name' VARCHAR(20) NOT NULL, " +
                          " 'surname' VARCHAR(20) NOT NULL, " +
                          " 'age' INT NOT NULL " +
                          " );";
        var result = cmd.ExecuteNonQuery();
      }

      
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS 'tblManagerSkills' ( " +
                          " 'id' INT PRIMARY KEY, " +
                          " 'avaliableSkillPoints' INT DEFAULT 0, " +
                          " 'personManagement' INT, " +
                          " 'motivation' INT, " +
                          " 'adaptation' INT, " +
                          " 'decisiveness' INT " +
                          " );";
        var result = cmd.ExecuteNonQuery();
      }

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS 'tblTeam' ( " +
                          " 'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          " 'name' VARCHAR(20), " +
                          " 'avatar' TEXT, " +
                          " 'logo' TEXT, " +
                          " 'budget' INT " +
                          " );";
        var result = cmd.ExecuteNonQuery();
      }

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS 'tblGameSettings' ( " +
                          " 'gameDiffuculty' VARCHAR(5), " +
                          " 'fixtureId' INT " +
                          " );";
        var result = cmd.ExecuteNonQuery();
      }

      
      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS 'tblPilot' ( " +
                          " 'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          " 'name' VARCHAR(30), " +
                          " 'age' INT, " +
                          " 'abilityPoint' INT, " +
                          " 'potantial' INT, " +
                          " 'salary' INT DEFAULT NULL, " +
                          " 'teamID' REFERENCES tblTeam(id) " +
                          " );";
        var result = cmd.ExecuteNonQuery();
      }

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS 'tblCar' ( " +
                          " 'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          " 'name' VARCHAR(30), " +
                          " 'mass' INT, " +
                          " 'suspansionDistance' INT, " +
                          " 'horsePower' INT, " +
                          " 'price' INT DEFAULT NULL, " +
                          " 'teamID' REFERENCES tblTeam(id) " +
                          " );";
        var result = cmd.ExecuteNonQuery();
      }

      using (var cmd = conn.CreateCommand())
      {
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS 'tblFixture' ( " +
                          " 'id' INTEGER PRIMARY KEY AUTOINCREMENT, " +
                          " 'name' VARCHAR(30), " +
                          " 'date' VARCHAR(20), " +
                          " 'status' BOOLEAN " +
                          " );";
        var result = cmd.ExecuteNonQuery();
      }

      conn.Close();
    
    }
  }
}