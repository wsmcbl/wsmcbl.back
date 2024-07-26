namespace wsmcbl.src.controller.business;

public interface IPrintDocumentsController
{
    public Task getEnrollDocument(string studentId, MemoryStream stream);
}