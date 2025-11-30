namespace UniStudentDB.Core.Models
{
    // A generic class to hold items and pagination metadata
    public class PagedResponse<T> : BaseResponse
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public PagedResponse(List<T> data, int count, int pageNumber, int pageSize)
        {
            // BaseResponse
            Success = true;
            Message = "Data fetched successfully";
            Errors = null;

            // Pagination Data
            Data = data;
            TotalCount = count;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
