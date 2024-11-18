using wsmcbl.src.model.config;

namespace wsmcbl.src.dto.config;

public class MediaToCreateDto
{
    public MediaToCreateDto()
    {
    }
    public MediaToCreateDto(MediaEntity media)
    {
        
    }
    
    public MediaEntity toEntity()
    {
        throw new NotImplementedException();
    }
}