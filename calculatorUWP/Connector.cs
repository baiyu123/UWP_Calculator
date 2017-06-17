using System;
using System.Diagnostics;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections.Generic;

namespace CalculatorUWP
{
    internal class Connector
    {
        MySqlConnection conn;
        public Connector()
        {
            string connStr = "server=localhost;user=root;database=calculatordb;port=3306;password=baiyusql;SslMode=None;";
            conn = new MySqlConnection(connStr);
        }

        //login user with password
        public string LoginUser(string username, string password) {
            string hashPass = GetPassword(username);
            if (hashPass == "") return "User does not exist.";
            if (hashPass != Hash(password)) return "Invalid password.";
            return "Success";
        }

        //get user hashed password from database
        private string GetPassword(string username) {
            string result ="";
            try
            {
                Debug.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT Password FROM users WHERE UserName = '" + username + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    result = rdr.GetString(0);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine("we have exception");
            }
            conn.Close();
            Debug.WriteLine("Done.");
            return result;
        }

        //check if user exist by fetching its password
        private bool checkUserExist(string username) {
            if (GetPassword(username) != "") {
                return true;
            }
            return false;
        }

        //register new user to database
        public string RegisterNewUser(string username, string password) {
            if (checkUserExist(username)) {
                return "Username already Registered.";
            }
            string hashPassword = Hash(password);
            try
            {
                conn.Open();
                string sql = "INSERT INTO Users VALUES ('" + username + "','" + hashPassword + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine("Adding new user fail");
            }
            conn.Close();
            return "Successfully registered.";
        }

        //insert operation to history table
        public void InsertAnOperation(string username, string operation) {
             try
            {
                conn.Open();
                string sql = "INSERT INTO History VALUES ('" + username + "','" + operation + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine("Adding new operation failed.");
            }
            conn.Close();
        }

        //get history from history table
        public List<string> fetchHistory(string username) {
            string result = "";
            List<string> results = new List<string>();
            try
            {
                Debug.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT Operation FROM history WHERE UserName = '" + username + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    result = rdr.GetString(0);
                    results.Add(result);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine("we have exception");
            }
            conn.Close();
            Debug.WriteLine("Done.");
            return results;
        }

        //hash password
        public string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}