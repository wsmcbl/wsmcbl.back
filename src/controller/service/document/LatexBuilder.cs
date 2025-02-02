using System.Diagnostics;

namespace wsmcbl.src.controller.service.document;

public abstract class LatexBuilder
{
    private readonly string templatesPath;

    protected LatexBuilder(string templatesPath, string outPath)
    {
        this.outPath = outPath;
        this.templatesPath = templatesPath;
        
        createOutPath(outPath);
    }
    
    private static void createOutPath(string path)
    {
        using var process = new Process();
        process.StartInfo.FileName = "/bin/bash";
        process.StartInfo.Arguments = $"-c \"mkdir {path} | true\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;

        process.Start();
        process.WaitForExit();

        Console.WriteLine(process.ExitCode == 0
            ? $"Directory {path} created successfully."
            : $"Error creating directory {path}.\nExit code: {process.ExitCode}");
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