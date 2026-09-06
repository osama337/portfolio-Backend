namespace portfolio_web.Dto.Project
{
    public class CreateUpdateProjectDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? GithubUrl { get; set; }

        public string? LiveDemoUrl { get; set; }

        public IFormFile? Image { get; set; }

        public bool IsFeatured { get; set; }


    }
}
