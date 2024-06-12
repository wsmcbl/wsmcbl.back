using System.ComponentModel.DataAnnotations;

namespace wsmcbl.back.dto.input;

public class DetailDto
{
    [Required]
    public int tariffId { get; set; }

    public bool applyArrear { get; set; }
}