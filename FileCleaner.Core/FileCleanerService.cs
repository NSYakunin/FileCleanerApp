using FileCleaner.Data;

namespace FileCleaner.Core;

public class FileCleanerService
{
    private readonly IDatabaseRepository _dbRepository;
    private readonly ILogger _logger;

    public FileCleanerService(IDatabaseRepository dbRepository, ILogger logger)
    {
        _dbRepository = dbRepository;
        _logger = logger;
    }

    public IEnumerable<FileRecord> GetUnlinkedFiles() =>
        _dbRepository.GetUnlinkedFiles();

    public void DeleteFiles(IEnumerable<FileRecord> files)
    {
        foreach (var file in files)
        {
            try
            {
                if (File.Exists(file.Path))
                    File.Delete(file.Path);

                _dbRepository.DeleteFileRecord(file.Id);
                _logger.Log($"Удален: {file.Path}");
            }
            catch (Exception ex)
            {
                _logger.Log($"Ошибка удаления {file.Path}: {ex.Message}");
            }
        }
    }
}