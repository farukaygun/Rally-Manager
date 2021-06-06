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
                          " 'gameDiffuculty' VARCHAR(5) " +
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

      conn.Close();
    
    }
  }


// INSERT INTO tblPilot (name, age, abilityPoint, potantial, salary, teamID) VALUES ('Ali', 24, 70, 80, 1000, 1);
// INSERT INTO tblPilot (name, age, abilityPoint, potantial, salary, teamID) VALUES ('Seca', 24, 70, 80, 2000, 2);

// SELECT * FROM tblPilot WHERE teamID = 1;

// SELECT * FROM tblPilot as p INNER JOIN tblTeam as t ON p.teamID = t.id WHERE t.name="FA FC"


  // public string conn;
  // public IDbConnection dbConn;

  // public Database()
  // {
  //     conn = "URI=file:" + Application.persistentDataPath + "/data.db";
  //     Debug.Log("coon: " + conn);

  //     dbConn = new SqliteConnection(conn);
  //     dbConn.Open();
  // }

  // ~Database()
  // {
  //     dbConn.Close();
  // }
}


// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// using Mono.Data.Sqlite;
// using System.Data;

// public class Database : MonoBehaviour
// {
//     private static Database instance = null;

//     // 1 nesne oluşmasını sağlamak için lock
//     private static readonly object padlock = new object();

//     private Database() {}

//     public static Database Instance
//     {
//         get
//         {
//             lock (padlock)
//             {
//                 if (instance == null)
//                 {
//                     instance = new Database();
//                 }
//             }
//             return instance;
//         }
//     }