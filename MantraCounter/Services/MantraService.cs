using MantraCounter.Models;
using MantraCounter.Services;
using SQLite;

namespace MantraCounter.Services
{
    public class MantraService : IMantraService
    {
        private SQLiteAsyncConnection _database;
        private bool _isInitialized = false;

        
        private static string DbPath =>
            Path.Combine(FileSystem.AppDataDirectory, "MantraTracker.db3");

        public MantraService()
        {
           
        }

       
        public async Task InitializeAsync()
        {
            if (_isInitialized)
                return;

            _database = new SQLiteAsyncConnection(DbPath);
            await _database.CreateTableAsync<MantraLog>();
            _isInitialized = true;
        }

        
        public Task<(string Mantra, int Goal)> GetMantraSettingsAsync()
        {
            string mantra = Preferences.Get("MantraText", "I am calm and focused.");
            int goal = Preferences.Get("DailyGoal", 108);
            return Task.FromResult((mantra, goal));
        }

        public Task SaveMantraSettingsAsync(string mantra, int goal)
        {
            Preferences.Set("MantraText", mantra);
            Preferences.Set("DailyGoal", goal);
            return Task.CompletedTask;
        }

        public async Task<MantraLog> GetLogForDateAsync(DateTime date)
        {
            await InitializeAsync(); 
            return await _database.Table<MantraLog>().Where(log => log.Date == date.Date).FirstOrDefaultAsync();
        }

       
        public async Task SaveLogAsync(MantraLog log)
        {
            await InitializeAsync(); 
            await _database.InsertOrReplaceAsync(log);
        }

        public async Task ResetAllDataAsync()
        {
            Preferences.Clear();

            await InitializeAsync(); 

          
            await _database.DropTableAsync<MantraLog>();
            await _database.CreateTableAsync<MantraLog>();
        }
    }
}