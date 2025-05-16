using FileCleaner.Core;
using System.Data.Common;

namespace FileCleaner.Data;

public class DatabaseRepository : IDatabaseRepository
{
    private readonly DbConnection _connection;

    public DatabaseRepository(DbConnection connection) =>
        _connection = connection;

    public IEnumerable<FileRecord> GetUnlinkedFiles()
    {
        var files = new List<FileRecord>();

        using var cmd = _connection.CreateCommand();
        cmd.CommandText = @"
            SELECT Id, Path FROM File f
            WHERE NOT EXISTS (
                SELECT 1 FROM LinkedTable lt WHERE lt.FileId = f.Id
            )";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            files.Add(new FileRecord { Id = reader.GetInt32(0), Path = reader.GetString(1) });

        return files;
    }

    public void DeleteFileRecord(int id)
    {
        using var cmd = _connection.CreateCommand();
        cmd.CommandText = "DELETE FROM File WHERE Id = @id";
        var param = cmd.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
        cmd.Parameters.Add(param);
        cmd.ExecuteNonQuery();
    }
}