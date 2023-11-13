using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Tutor.Models.DTO
{
    public class SearchDTO
    {
        [AllowNull]
        public int? page { get; set; }
        [AllowNull] public int? limit { get; set; } = 10;
        [AllowNull] public string? search { get; set; } = null;

        [AllowNull] public int? role { get; set; } = null;

    }
}
