namespace wsmcbl.src.dto.input;

public interface IBaseDto<out Entity>
{
    public Entity toEntity();
}