using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using System.Threading;
using System.IO;

public class CreateDatabase : MonoBehaviour
{
  private static string connString;

  private void Start()
  {
    //dbPath = "URI=file:" + Application.persistentDataPath + "/rallyManagerData.db";

#if UNITY_EDITOR
  var dbPath = string.Format(@"Assets/StreamingAssets/{0}", Global.dbName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, Global.dbName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + Global.dbName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + Global.dbName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + Global.dbName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
    Global.dbPath = dbPath;
    Debug.Log("Final PATH: " + dbPath); // /storage/emulated/0/Android/data/com.DefaultCompany.RallyManager/files/rallyManagerData.db

    connString = @"Data Source=" + dbPath + ";Pooling=true;FailIfMissing=false;Version=3";
  }

  public static void CreateSchemas()
  {
    using (var conn = new SqliteConnection(connString))
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