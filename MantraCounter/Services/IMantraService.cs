using MantraCounter.Models;

namespace MantraCounter.Services
{
  
    public interface IMantraService
    {
        Task InitializeAsync();
        Task<(string Mantra, int Goal)> GetMantraSettingsAsync();
        Task SaveMantraSettingsAsync(string mantra, int goal);
        Task<MantraLog> GetLogForDateAsync(DateTime date);
        Task SaveLogAsync(MantraLog log);
        Task ResetAllDataAsync();

    }
}