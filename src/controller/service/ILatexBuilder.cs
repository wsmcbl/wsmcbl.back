namespace wsmcbl.src.controller.service;

public interface ILatexBuilder<T> where T : class
{
    public string getOutPath();
    public string getFilePath();
    public string getFileName();

    public void build(string templateName, Dictionary<string, string> data);
    public void build(T entity);
}