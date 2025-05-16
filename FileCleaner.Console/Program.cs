using FileCleaner.Console;
using FileCleaner.Core;
using FileCleaner.Data;
using FileCleaner.Email;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data.Common;

Console.WriteLine("Выбор БД:\n1.MS SQL\n2.PostgreSQL\n3.Своя строка");
var choice = Console.ReadLine();

Console.WriteLine("Строка подключения (если выбрали 3):");
var connStr = choice switch
{
    "1" => "Server=.;Database=FilesDB;Trusted_Connection=True;",
    "2" => "Host=localhost;Database=filesdb;Username=user;Password=password;",
    "3" => Console.ReadLine(),
    _ => throw new Exception("Неверный выбор")
};

using DbConnection connection = connStr.Contains("Host=")
    ? new NpgsqlConnection(connStr)
    : new SqlConnection(connStr);

connection.Open();

var repo = new DatabaseRepository(connection);
var logger = new FileLogger();
var cleaner = new FileCleanerService(repo, logger);
var email = new EmailService("recipient@example.com");

var files = cleaner.GetUnlinkedFiles();
cleaner.DeleteFiles(files);
email.SendLog(logger.GetLogFilePath());

Console.WriteLine("Завершено!");