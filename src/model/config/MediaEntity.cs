namespace wsmcbl.src.model.config;

public class MediaEntity
{
    public int mediaId { get; set; }
    public int type { get; set; }
    public string schoolyearId { get; set; }
    public string value { get; set; }

    public MediaEntity()
    {
    }

    public void update(MediaEntity media)
    {
        type = media.type;
        schoolyearId = media.schoolyearId;
        value = media.value;
    }
}