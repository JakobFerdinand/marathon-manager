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

    public static partial class Extensions
    {
        public static ExportSportsclub WithRang(this ExportSportsclub @this, int rang)
            => new ExportSportsclub(
                rang,
                @this.Name,
                @this.Count);
    }
}
