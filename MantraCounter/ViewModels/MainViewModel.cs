using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MantraCounter.Models;
using MantraCounter.Services;

namespace MantraCounter.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IMantraService _mantraService;
        private MantraLog _todayLog;

        [ObservableProperty]
        private string mantraText;

        [ObservableProperty]
        private int dailyGoal;

        [ObservableProperty]
        private int currentCount;

        [ObservableProperty]
        private double progress;

        [ObservableProperty]
        private bool isGoalMet;

        public MainViewModel(IMantraService mantraService)
        {
            _mantraService = mantraService;
        }


        [RelayCommand]
        private async Task LoadDataAsync()
        {
            // Load mantra settings
            var settings = await _mantraService.GetMantraSettingsAsync();
            MantraText = settings.Mantra;
            DailyGoal = settings.Goal;

            // Load today's log entry
            _todayLog = await _mantraService.GetLogForDateAsync(DateTime.Today);
            if (_todayLog == null)
            {
                // If no log exists for today, create a new one.
                _todayLog = new MantraLog { Date = DateTime.Today, Count = 0 };
            }

            CurrentCount = _todayLog.Count;
            UpdateProgress();
        }

        [RelayCommand]
        private async Task IncrementCountAsync()
        {
            if (IsGoalMet) return; // Don't increment if the goal is already met

            CurrentCount++;
            _todayLog.Count = CurrentCount;
            await _mantraService.SaveLogAsync(_todayLog);
            UpdateProgress();
        }

        // This command is triggered when the user finishes editing the mantra or goal fields.
        [RelayCommand]
        private async Task SaveSettingsAsync()
        {
            if (DailyGoal <= 0) DailyGoal = 1; // Ensure goal is at least 1
            await _mantraService.SaveMantraSettingsAsync(MantraText, DailyGoal);
            UpdateProgress(); // Re-calculate progress in case the goal changed
        }

        private void UpdateProgress()
        {
            if (DailyGoal > 0)
            {
                // Progress is a value between 0.0 and 1.0
                Progress = (double)CurrentCount / DailyGoal;
            }
            else
            {
                Progress = 0;
            }

            IsGoalMet = CurrentCount >= DailyGoal;
        }

        [RelayCommand]
        private async Task ResetTodaysCountAsync()
        {
            if (_todayLog == null) return; // Nothing to reset

            CurrentCount = 0;
            _todayLog.Count = 0;
            await _mantraService.SaveLogAsync(_todayLog);
            UpdateProgress(); // Update the UI (progress bar, etc.)
        }

        [RelayCommand]
        private async Task ResetAllDataAsync()
        {
            // It's critical to ask for confirmation before a destructive action.
            // We access the main page to show an alert.
            if (Application.Current?.MainPage == null) return;

            bool confirmed = await Application.Current.MainPage.DisplayAlert(
                "Confirm Reset",
                "Are you sure you want to reset everything? Your mantra, goal, and all history will be permanently deleted.",
                "Yes, Reset Everything",
                "Cancel");

            if (!confirmed)
            {
                return; // User cancelled the operation.
            }

            // User confirmed, proceed with the reset.
            await _mantraService.ResetAllDataAsync();

            // After wiping all data, we must reload the ViewModel to reflect
            // the new default state. The easiest way is to call our existing load command.
            if (LoadDataCommand.CanExecute(null))
            {
                await LoadDataCommand.ExecuteAsync(null);
            }
        }
    }
}