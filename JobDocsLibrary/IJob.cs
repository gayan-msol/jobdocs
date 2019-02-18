namespace JobDocsLibrary
{
    public interface IJob
    {
        string Customer { get; set; }
        string JobName { get; set; }
        string JobNo { get; set; }
    }

    public class ProductionReport : IJob
    {
        public string Customer { get; set ; }
        public string JobName { get; set; }
        public string JobNo { get ; set; }

    }
}