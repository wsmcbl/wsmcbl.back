namespace wsmcbl.src.controller.service;

public abstract class LatexBuilder
{
    private readonly string templatesPath;

    protected LatexBuilder(string templatesPath, string outPath)
    {
        this.outPath = outPath;
        this.templatesPath = templatesPath;
    }
    
    private readonly string outPath;
    public string getOutPath() => outPath;
    
    
    private string? filePath;
    public string? getFilePath() => filePath;
    
    
    private string? fileName;
    public string? getFileName() => fileName;

    public string getImagesPath() => $"{templatesPath}/image";
    

    public void build()
    {
        var content = File.ReadAllText(Path.Combine(templatesPath, $"{getTemplateName()}.tex"));

        content = updateContent(content);

        fileName = $"{getTemplateName()}_output";
        filePath = Path.Combine(outPath, $"{fileName}.tex");
        
        File.WriteAllText(filePath, content);
    }

    protected abstract string getTemplateName();
    protected abstract string updateContent(string content);
}