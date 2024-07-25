namespace wsmcbl.src.dto;

public interface IBaseDto<out Entity>
{
    public Entity toEntity();
}