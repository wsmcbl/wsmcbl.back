using System.Text.Json.Serialization;

namespace wsmcbl.src.model.config;

public class MediaEntity
{
    [JsonRequired] public int mediaId { get; set; }
    [JsonRequired] public int type { get; set; }
    public string schoolyearId { get; set; } = null!;
    public string value { get; set; } = null!;

    public void update(MediaEntity media)
    {
        type = media.type;
        schoolyearId = media.schoolyearId;
        value = media.value;
    }
}