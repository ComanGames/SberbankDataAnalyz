using CsvHelper;
using DataTools;

namespace ConvertCsvDb
{
    public class TypeReader
    {
        public static MccCode GetMccCode(CsvReader csv)
        {
            var codeIdex = csv.GetField<int>(0);
            var codeDescription = csv.GetField<string>(1);
            MccCode mccCode = new MccCode() {Value = codeIdex, Description = codeDescription, ManProc = -1};
            return mccCode;
        }
    }
}