namespace Entities.RequestFeatures
{
    public class MetaData
    {
        public int CurentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => CurentPage > 1;
        public bool HasPAge => CurentPage < TotalCount;
    }
}
