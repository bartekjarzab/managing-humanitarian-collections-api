namespace managing_humanitarian_collections_api.Models.QueryValidators
{
    public class ManagingHumanitarianQuery
    {
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        //       public SortDirection SortDirection { get; set; }
    }
}
