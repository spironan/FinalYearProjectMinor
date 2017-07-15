using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using System.Collections;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class Database
{
    /*
     * Every Database File Should Contain
     * connectionString : A variable to store where the database file is
     * dbConnection : The Actual Linking with The database using the connectionString
     * dbCmd : The Command Given to the database for it to execute
     * reader : A variable to store the results of whatever command that have been executed
     * connected : To check if the file is already connected to something
     */

    public DatabaseInfo databaseInfo;
    public string connectionString;
    public IDbConnection dbConnection;
    public IDbCommand dbCmd;
    public IDataReader reader;
    public bool connected;

    public Database(Database database)
    {
        connectionString = database.connectionString;
        dbConnection = database.dbConnection;
        dbCmd = database.dbCmd;
        reader = database.reader;
        connected = database.connected;
    }

    public Database(DatabaseInfo databaseInfo)
    {
        this.databaseInfo = databaseInfo;
        connectionString = "";
        connected = false;
        reader = null;
        dbCmd = null;
        dbConnection = null;
    }

    public Database()
    {
        connectionString = "";
        connected = false;
        reader = null;
        dbCmd = null;
        dbConnection = null;
    }

    private void HardReset()
    {
        connectionString = "";
        connected = false;
        SoftReset();
        dbConnection = null;
    }

    public void SoftReset()
    {
        if (reader != null && !reader.IsClosed)
            reader.Close();
        if (reader != null)
            reader = null;
        ExecuteReset();
    }

    public void ExecuteReset()
    {
        if (dbCmd != null)
        {
            dbCmd.Dispose();
            dbCmd = null;
        }
        if (dbConnection.State != ConnectionState.Closed)
            dbConnection.Close();
    }

    public bool SetConnection(string connectionString)
    {
        if (connected)
            Debug.Log("Already Connected to " + this.connectionString + " Please Drop it Before Trying Again");

        else if (connectionString == "")
            Debug.Log("You Cant Input an empty string and expect a database to be created out of nowhere!");

        else
        {
            this.connectionString = connectionString;
            dbConnection = (IDbConnection)new SqliteConnection(connectionString);
            if (dbConnection != null)
            {
                Debug.Log("Connected to database!");
                connected = true;
                return true;
            }

            Debug.Log("Cant Connect to The Sqlite, are you sure you've Created A SqLite or is the file Path correct :" + connectionString);
            return false;
        }

        return false;
    }

    public bool IsConnected()
    {
        return connected;
    }

    public void DropConnection()
    {
        if (!connected)
            Debug.Log("Cant Drop When you are not connected to anything in the first place");
        else
        {
            HardReset();
            Debug.Log("Succesfully Dropped Current Connection");
        }
    }

    /*
     * This Function Effective tells The Database directly What to do with whatever you want
     * This Function Does Not Check For Any Wrong Commands, Use at Own Risk
     * Dont Use This Function if you are Getting Values from it
     */

    public void ExecuteCommand(string command)
    {
        if (!IsConnected())
        {
            Debug.Log("Cant Execute any command if you have not connected to any database yet");
            return;
        }
        // Open The Database File
        dbConnection.Open();
        // Create A Command 
        dbCmd = dbConnection.CreateCommand();
        // Insert The Command to run
        string sqlQuery = command;
        // string sqlQuery = String.Format("INSERT INTO HighScores(Name,Score) VALUES(\"{0}\",\"{1}\")", name, newScore);
        // Execute The Command in The Database
        dbCmd.CommandText = sqlQuery;
        dbCmd.ExecuteScalar();
        ExecuteReset();
    }

    public bool CheckIfExist(string tableName, string variable, string reference_no)
    {
        dbConnection.Open();
        dbCmd = dbConnection.CreateCommand();
        string sqlQuery = String.Format("SELECT * FROM \"{0}\" where \"{1}\" = \"{2}\"", tableName, variable, reference_no);
        dbCmd.CommandText = sqlQuery;
        int exist = (int)dbCmd.ExecuteScalar();
        ExecuteReset();
        return (exist > 0);
    }

    public void AddToDataBase(string tableName, string reference_no)
    {
        if (dbConnection.State != ConnectionState.Open)
            dbConnection.Open();
        dbCmd = dbConnection.CreateCommand();
        string sqlQuery = String.Format("INSERT INTO {0} VALUES({1})", tableName, reference_no);
        Debug.Log(sqlQuery);
        dbCmd.CommandText = sqlQuery;
        dbCmd.ExecuteScalar();
        ExecuteReset();
    }

    public void SelectTable(string tableName)
    {
        if (dbConnection.State != ConnectionState.Open)
            dbConnection.Open();
        dbCmd = dbConnection.CreateCommand();
        string sqlQuery = String.Format("SELECT * FROM \"{0}\"", tableName);
        dbCmd.CommandText = sqlQuery;
        reader = dbCmd.ExecuteReader();
    }

    public void FindColumnInTable(string tableName, string variable, string reference_no)
    {
        dbConnection.Open();
        dbCmd = dbConnection.CreateCommand();
        string sqlQuery = String.Format("SELECT * FROM \"{0}\" where \"{1}\" = \"{2}\"", tableName, variable, reference_no);
        dbCmd.CommandText = sqlQuery;
        reader = dbCmd.ExecuteReader();
    }

    public void AddOneToDataBase(string tableName, string variableName, string reference_no)
    {
        if (dbConnection.State != ConnectionState.Open)
            dbConnection.Open();
        dbCmd = dbConnection.CreateCommand();
        string sqlQuery = String.Format("INSERT INTO \"{0}\"(\"{1}\") VALUES(\"{2}\");", tableName, variableName, reference_no);
        dbCmd.CommandText = sqlQuery;
        dbCmd.ExecuteScalar();
        ExecuteReset();
    }

    public void UpdateLine(string tableName, string variableName, string condition = "")
    {
        if (dbConnection.State != ConnectionState.Open)
            dbConnection.Open();
        dbCmd = dbConnection.CreateCommand();
        string sqlQuery;
        if(condition != "")
            sqlQuery = String.Format("UPDATE {0} SET {1} WHERE {2}", tableName, variableName, condition);
        else
            sqlQuery = String.Format("UPDATE {0} SET {1} ", tableName, variableName);
        Debug.Log(sqlQuery);
        dbCmd.CommandText = sqlQuery;
        dbCmd.ExecuteScalar();
        ExecuteReset();
    }

    public void ReadFromTwoTables(string outputVariable, string leftTable, string rightTable, string leftOutput, string rightOutput, string condition)
    {
        if (dbConnection.State != ConnectionState.Open)
            dbConnection.Open();
        dbCmd = dbConnection.CreateCommand();
        string sqlQuery = String.Format("SELECT {0} FROM {1} LEFT JOIN {2} ON {3} = {4} WHERE {5}", outputVariable, leftTable, rightTable, leftOutput, rightOutput, condition);
        Debug.Log(sqlQuery);
        dbCmd.CommandText = sqlQuery;
        reader = dbCmd.ExecuteReader();
    }

    public void GetLatestData(string table, string condition)
    {
        if (dbConnection.State != ConnectionState.Open)
            dbConnection.Open();
        dbCmd = dbConnection.CreateCommand();
        string sqlQuery = String.Format("SELECT * FROM {0} WHERE {1} = (SELECT MAX({1}) FROM {0})", table, condition);
        Debug.Log(sqlQuery);
        dbCmd.CommandText = sqlQuery;
        reader = dbCmd.ExecuteReader();
    }

    public void DeleteFromTable(string table, string condition)
    {
        if (dbConnection.State != ConnectionState.Open)
            dbConnection.Open();
        dbCmd = dbConnection.CreateCommand();
        string sqlQuery = String.Format("DELETE FROM {0} WHERE {1}", table, condition);
        Debug.Log(sqlQuery);
        dbCmd.CommandText = sqlQuery;
        dbCmd.ExecuteScalar();
    }

}
