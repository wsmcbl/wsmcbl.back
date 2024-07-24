namespace wsmcbl.src.model.secretary;

public class StudentFileEntity
{
    public string Fileid { get; set; } = null!;

    public string Studentid { get; set; } = null!;

    public bool Transfersheet { get; set; }

    public bool Birthdocument { get; set; }

    public bool Parentidentifier { get; set; }

    public bool Updatedgradereport { get; set; }

    public bool Conductdocument { get; set; }

    public bool Financialsolvency { get; set; }
}