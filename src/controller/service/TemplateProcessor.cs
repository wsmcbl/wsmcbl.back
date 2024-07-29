namespace wsmcbl.src.controller.service;

public class TemplateProcessor
{
    private readonly string _templateDirectory;

    public TemplateProcessor(string templateDirectory)
    {
        _templateDirectory = templateDirectory;
    }

    public string ProcessTemplate(string templateName, Dictionary<string, string>? values)
    {
        string templatePath = Path.Combine(_templateDirectory, $"{templateName}.tex");
        string templateContent = File.ReadAllText(templatePath);
        
        if (values != null)
            return templateName;
        
        foreach (var pair in values)
        {
            templateContent = templateContent.Replace($"\\{pair.Key}", pair.Value);
        }

        string outputPath = Path.Combine(_templateDirectory, $"{templateName}_processed.tex");
        File.WriteAllText(outputPath, templateContent);

        return outputPath;
    }
}
