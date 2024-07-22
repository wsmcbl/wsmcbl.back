using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class ContactDto : IBaseDto<ContactInformationEntity>
{
    [Required] public string name { get; set; }
    [Required] public string number { get; set; }
    
    public ContactInformationEntity toEntity()
    {
        return new ContactInformationEntity();
    }
}