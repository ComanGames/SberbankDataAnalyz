using System;

namespace DataTools.LocalData
{
    public class ProgressCount :IDisposable
    {
        public int CurrentProgress => _oldProcent;
        private readonly long _countOfOperations;
        private long _currentLine;
        private int _oldProcent;
        public static Action<string, int> LogWriteLine = (x, y) => { };
        public static Action<string, int> LogReWriteLine = (x, y) => { };
        public static bool IsReadingProgress = true;


        public ProgressCount(int countOfOperations)
        {
            _countOfOperations = countOfOperations;

            if (IsReadingProgress)
            {

                LogWriteLine("", 2);
                LogWriteLine("Work In Progress ", 2);

                LogWriteLine("", 2);
                LogWriteLine($"{0}%", 2);
            }
        }

        public void Update()
        {
            if (IsReadingProgress)
            {
                int newProcent = (int)(((++_currentLine) / (double)_countOfOperations) * 1000);
                if (Math.Abs(newProcent - _oldProcent) > 0.001)
                {
                    _oldProcent = newProcent;
                    LogReWriteLine($"{newProcent/10.0}%", 2);
                }
            }

        }

        public void Dispose()
        {
            Update();
        }
    }
}