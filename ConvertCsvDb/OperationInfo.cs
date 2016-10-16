using System;
using System.Diagnostics;
using System.Reflection;

namespace ConvertCsvDb
{
    public class OperationInfo:IDisposable
    {
        public static Action<string,int> LogAction;
        private string _opertionText;
        private int _tabsCount;
        private Stopwatch _stopwatch;

        public OperationInfo(string opertionText,int tabsCount)
        {
            _opertionText = opertionText;
            _tabsCount = tabsCount;
           _stopwatch = new Stopwatch();

            LogAction?.Invoke($"Start {_opertionText}",_tabsCount);
            _stopwatch.Start();

        }

        public void Dispose()
        {
           _stopwatch.Stop();
            if (LogAction != null)
            {
               LogAction.Invoke($"Done {_opertionText}",_tabsCount); 
                LogAction.Invoke($"It taked {_stopwatch.ElapsedMilliseconds/1000.0} seconds",_tabsCount);
                LogAction.Invoke("",_tabsCount);
            }
        }
    }
}