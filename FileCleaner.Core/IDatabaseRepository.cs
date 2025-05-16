using FileCleaner.Core;

namespace FileCleaner.Data;

public interface IDatabaseRepository
{
    IEnumerable<FileRecord> GetUnlinkedFiles();
    void DeleteFileRecord(int id);
}