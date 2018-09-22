namespace UI.ExportResults.Models
{
    public sealed class ExportSportsclub
    {
        public ExportSportsclub(int rang, string name, int count)
        {
            Rang = rang;
            Name = name;
            Count = count;
        }

        public int Rang { get; }
        public string Name { get; }
        public int Count { get; }
    }
}
