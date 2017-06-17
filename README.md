# UWP_Calculator
UWP calculator
This app work with MySQL. Run following query in your MySQL workbench:
CREATE DATABASE calculatordb;
CREATE TABLE calculatordb.Users(
	UserName varchar(255),
  Password varchar(255)
);
CREATE TABLE calculatordb.History(
	UserName varchar(255),
  Operation varchar(255)
)
Then change connStr variable in connector.cs to match your local MySQL server's username and password.
