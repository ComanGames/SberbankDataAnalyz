using System;

namespace DataTools.LocalData
{
    public class ProgressCount :IDisposable
    {
        private readonly double _countOfOperations;
        private double _currentLine;
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
                LogWriteLine(" Reading In Progress ", 2);

                LogWriteLine("", 2);
                LogWriteLine($"{0}%", 2);
            }
        }

        public void Update()
        {
            if (IsReadingProgress)
            {
                int newProcent = (int)(((++_currentLine) / _countOfOperations) * 100);
                if (newProcent != _oldProcent)
                {
                    _oldProcent = newProcent;
                    LogReWriteLine($"{newProcent}%", 2);
                }
            }

        }

        public void Dispose()
        {
            if (IsReadingProgress)
            {
                int newProcent = (int)(((++_currentLine) / _countOfOperations) * 100);
                if (newProcent != _oldProcent)
                {
                    _oldProcent = newProcent;
                    LogReWriteLine($"{newProcent}%", 2);
                }
            }

        }
    }
}