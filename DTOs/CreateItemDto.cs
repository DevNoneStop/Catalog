using System.ComponentModel.DataAnnotations;

namespace Catalog.DTOs
{
    public record CreateItemDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(1, 200)]
        public decimal Price { get; init; }
    }
}