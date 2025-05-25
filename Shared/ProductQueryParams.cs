namespace Shared
{
   public class ProductQueryParams
    {

        public const int DefaultPageSize = 5;
        public const int MaxPageSize = 10;

        public int? BrandId {  get; set; }
        public int? TypeId { get; set; }

        public ProductOptionSorting Sort { get; set; }

        public string? Search { get; set; }

        public int PageNumber { get; set; } = 1;
        public int pageSize=DefaultPageSize;

        public int PageSize {

            get {  return pageSize; }
            set { pageSize = value>MaxPageSize?MaxPageSize:value; }
        
        }

        
    }
}
