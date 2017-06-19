# UWP_Calculator
UWP calculator<br />
This app work with MySQL. Run following query in your MySQL workbench:<br />
CREATE DATABASE calculatordb;<br />
CREATE TABLE calculatordb.Users(<br />
	UserName varchar(255),<br />
  Password varchar(255)<br />
);<br />
CREATE TABLE calculatordb.History(<br />
	UserName varchar(255),<br />
  Operation varchar(255)<br />
)<br />
Then change connStr variable in connector.cs to match your local MySQL<br /> server's username and password.<br />
