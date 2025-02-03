using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.management;

public class PartialDto
{
    public int partialId { get; set; }
    public string label { get; set; }
    public bool isActive { get; set; }
    public string? period { get; set; }

    public PartialDto(PartialEntity partial) 
    {
        partialId = partial.partialId;
        label = partial.label;
        isActive = partial.recordIsActive();
    }
}