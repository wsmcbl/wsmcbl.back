using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.input;

public class DetailDto
{
    [Required] public int tariffId { get; set; }
    public float amount { get; set; }
    public bool applyArrear { get; set; }
}