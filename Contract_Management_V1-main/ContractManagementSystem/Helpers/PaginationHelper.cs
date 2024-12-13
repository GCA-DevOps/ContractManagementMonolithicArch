using System.Text;

namespace ContractManagementSystem.Helpers
{
    public static class PaginationHelper
    {
        public static string GeneratePaginationLinks(int currentPage, int totalPages, string baseUrl)
        {
            var sb = new StringBuilder();
            var prevPage = currentPage > 1 ? currentPage - 1 : 1;
            var nextPage = currentPage < totalPages ? currentPage + 1 : totalPages;

            sb.Append($"<a href='{baseUrl}?page=1'>First</a> ");
            sb.Append($"<a href='{baseUrl}?page={prevPage}'>Previous</a> ");

            for (int i = 1; i <= totalPages; i++)
            {
                if (i == currentPage)
                {
                    sb.Append($"<span>{i}</span> ");
                }
                else
                {
                    sb.Append($"<a href='{baseUrl}?page={i}'>{i}</a> ");
                }
            }

            sb.Append($"<a href='{baseUrl}?page={nextPage}'>Next</a> ");
            sb.Append($"<a href='{baseUrl}?page={totalPages}'>Last</a>");

            return sb.ToString();
        }
    }
}
