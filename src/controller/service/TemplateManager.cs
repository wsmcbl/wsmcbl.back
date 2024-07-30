using System.Diagnostics;

namespace wsmcbl.src.controller.service;

public class TemplateManager
{
    private readonly string _templatesPath;
    private readonly string _tempPath;
    private readonly string _texCompile = "/usr/bin/latexmk";

    public TemplateManager(string templatesPath, string tempPath)
    {
        _templatesPath = templatesPath;
        _tempPath = tempPath;
    }

    public string UpdateTemplate(string templateName, Dictionary<string, string> data)
    {
        var templatePath = Path.Combine(_templatesPath, $"{templateName}.tex");
        var template = File.ReadAllText(templatePath);

        foreach (var (key, value) in data)
            template = template.Replace($"\\{key}", value);
        
        return template;
    }
    
    public string CompileToPdf(string template, string outputName)
    {
        var tempFilePath = Path.Combine(_tempPath, $"{outputName}.tex");
        File.WriteAllText(tempFilePath, template);

        var pdfPath = Path.Combine(_tempPath, $"{outputName}.pdf");
        var command = $"-interaction=nonstopmode -pdf -output-directory=\"{_tempPath}\" \"{tempFilePath}\"";

        using (var process = new Process())
        {
            process.StartInfo.FileName = _texCompile;
            process.StartInfo.Arguments = command;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                var errorOutput = process.StandardError.ReadToEnd();
                throw new ArgumentException($"latexmk failed with exit code {process.ExitCode}. Error output:\n{errorOutput}");
            }
        }

        var auxiliaryFiles = Directory.GetFiles(_tempPath, $"{outputName}.*")
            .Where(f => !f.EndsWith(".pdf"));
        foreach (var file in auxiliaryFiles)
        {
            File.Delete(file);
        }

        return pdfPath;
    }
}