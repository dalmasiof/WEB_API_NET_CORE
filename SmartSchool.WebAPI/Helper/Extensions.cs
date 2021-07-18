using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SmartSchool.WebAPI.Helper
{
    public static class Extensions
    {
        public static void addPagination(this HttpResponse response, int currentPage,
        int totalPages,
        int pageSize,
        int totalCount)
        {
            var paginationHeader = new PaginationHeader(currentPage,
            totalPages,
            pageSize,
            totalCount);

            response.Headers.Add("Pagination",JsonConvert.SerializeObject(paginationHeader));
            response.Headers.Add("Acces-Control-Response-Header","Pagination");

        }
    }
}