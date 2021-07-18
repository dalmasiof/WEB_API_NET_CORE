namespace SmartSchool.WebAPI.Helper
{
    public class PageParams
    {
        public const int MaxPageSize = 50;
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;

        public string Nome { get; set; } = string.Empty;
        public int? Matricula { get; set; } = null;
        public int? Ativo { get; set; }

        public int PageSize
        {
            get { return pageSize; }

            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }

        }

    }
}