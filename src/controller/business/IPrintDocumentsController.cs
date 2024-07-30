namespace wsmcbl.src.controller.business;

public interface IPrintDocumentsController
{
    public Task<byte[]> getEnrollDocument(string studentId);
}