using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace PeriodicTimerSample
{
    public class MainViewModel
    {
        private PeriodicTimer _periodicTimer;

        private CancellationTokenSource _token = new();

        private Task? _timerTask;

        private readonly ObservableCollection<string> _names = new();
        public MainViewModel(TimeSpan timespan)
        {
            _periodicTimer = new(timespan);
            
        }
        public ObservableCollection<string> Names => _names;
        public void Start()
        {
            _token = new();
            _timerTask = DoWork();
        }
        public async Task DoWork()
        {
            try
            {
                while (await _periodicTimer.WaitForNextTickAsync(_token.Token))
                {
                    _names.Add($"Clone Number : {Random.Shared.Next(1, 1000)}");
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
        }
        public async Task StopTimer()
        {
            if (_timerTask is null)
            {
                return;
            }

            _token.Cancel();
            await _timerTask;
            _token.Dispose();
        }
    }
}
