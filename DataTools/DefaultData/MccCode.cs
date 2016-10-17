namespace DataTools.DefaultData
{
    public class MccCode
    {
        public override string ToString()
        {
            return $"{nameof(Value)}: {Value}, {nameof(Description)}: {Description}, {nameof(ManProc)}: {ManProc}";
        }

        public int Id { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public float ManProc { get; set; }

    }
}