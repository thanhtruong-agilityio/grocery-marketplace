namespace GroceryMarketPlace.Domain.Dtos
{
    public class BasePaginationResponseDto<T>
    {
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public IEnumerable<T> Data { get; set; }

        public BasePaginationResponseDto(IEnumerable<T> data, int totalRecords, int? pageNumber, int? pageSize)
        {
            pageNumber ??= 1;
            pageSize ??= totalRecords;
            int roundedTotalPages = totalRecords != 0 ? Convert.ToInt32(Math.Ceiling((double)totalRecords / pageSize.Value)) : 0;

            this.Data = data;
            this.TotalRecords = totalRecords;
            this.TotalPages = roundedTotalPages;
            this.HasNext = pageNumber >= 1 && pageNumber < roundedTotalPages;
            this.HasPrevious = pageNumber - 1 >= 1 && pageNumber <= roundedTotalPages;
        }
    }
}
