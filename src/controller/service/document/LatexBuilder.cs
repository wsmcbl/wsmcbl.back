using System.Diagnostics;
using System.Security.Cryptography;

namespace wsmcbl.src.controller.service.document;

public abstract class LatexBuilder
{
    private readonly string templatesPath;

    protected LatexBuilder(string templatesPath, string outPath)
    {
        setOutPath(outPath);
        this.templatesPath = templatesPath;
        
        createOutPath();
    }

    private void setOutPath(string basePath)
    {
        var asciiValue = RandomNumberGenerator.GetInt32(97, 123);
        var numberValue = RandomNumberGenerator.GetInt32(10, 999);
        
        outPath = $"{basePath}/{(char)asciiValue}{numberValue}";
    }
    
    private void createOutPath()
    {
        using var process = new Process();
        process.StartInfo.FileName = "/bin/bash";
        process.StartInfo.Arguments = $"-c \"mkdir -p {outPath} | true\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;

        process.Start();
        process.WaitForExit();
        
        Console.WriteLine(process.ExitCode == 0
            ? $"Directory {outPath} created successfully."
            : $"Error creating directory {outPath}.\nExit code: {process.ExitCode}");
    }
    
    private string? outPath;
    public string getOutPath() => outPath!;
    
    
    private string? filePath;
    public string? getFilePath() => filePath;
    
    
    private string? fileName;
    public string? getFileName() => fileName;

    protected string getImagesPath() => $"{templatesPath}/image";

    public void build()
    {
        var content = File.ReadAllText(Path.Combine(templatesPath, $"{getTemplateName()}.tex"));

        content = updateContent(content);

        fileName = $"{getTemplateName()}_output";
        filePath = Path.Combine(outPath!, $"{fileName}.tex");
        
        File.WriteAllText(filePath, content);
    }
    
    protected abstract string getTemplateName();
    
    protected abstract string updateContent(string content);
}