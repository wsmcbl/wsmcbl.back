using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class PartialInformationDto
{
    public string partial { get; set; }
    public bool close { get; set; }

    public PartialInformationDto(string partial, bool close)
    {
        this.partial = partial;
        this.close = close;
    }

    public PartialInformationDto(PartialEntity partial) : this(partial.label, partial.isClosed())
    {
    }
}