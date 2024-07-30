using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.service;

public class ReportCardLatexBuilder : ILatexBuilder<StudentEntity>
{
    private readonly string outputPath;
    private readonly string templatesPath;
    private string? filePath;
    private string? fileName;

    public ReportCardLatexBuilder(string templatesPath, string outputPath)
    {
        this.outputPath = outputPath;
        this.templatesPath = templatesPath;
    }

    public string getOutPath() => outputPath;
    public string getFilePath() => filePath!;
    public string getFileName() => fileName!;
    
    public void build(string templateName, Dictionary<string, string> data)
    {
        var content = File.ReadAllText(Path.Combine(templatesPath, $"{templateName}.tex"));

        foreach (var (key, value) in data)
        {
            content = content.Replace($"\\{key}", value);
        }

        fileName = $"{templateName}_output";
        filePath = Path.Combine(outputPath, $"{fileName}.tex");
        
        File.WriteAllText(filePath, content);
    }

    public void build(StudentEntity entity)
    {
        var templateName = "report-card";
        
        var content = File.ReadAllText(Path.Combine(templatesPath, $"{templateName}.tex"));
        
        content = content.Replace($"\\name", entity.fullName());

        fileName = $"{templateName}_output";
        filePath = Path.Combine(outputPath, $"{fileName}.tex");
        
        File.WriteAllText(filePath, content);
    }
}