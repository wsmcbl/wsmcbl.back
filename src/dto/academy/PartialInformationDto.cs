using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class PartialInformationDto
{
    public int partialId { get; set; }
    public string label { get; set; }
    public bool isActive { get; set; }

    public PartialInformationDto(PartialEntity partial) 
    {
        partialId = partial.partialId;
        label = partial.label;
        isActive = partial.isClosed();
    }
}