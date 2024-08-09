using System.Diagnostics;

namespace wsmcbl.src.controller.service;

public class PDFBuilder
{
    private readonly LatexBuilder latexBuilder;

    public PDFBuilder(LatexBuilder latexBuilder)
    {
        this.latexBuilder = latexBuilder;
    }
    
    public PDFBuilder build()
    {
        var argument = $"-interaction=nonstopmode" +
                      $" -output-directory=\"{latexBuilder.getOutPath()}\"" +
                      $" \"{latexBuilder.getFilePath()}\"";

        createPdf(argument);
        cleanFiles();
        return this;
    }
    
    private const string texCompile = "/usr/bin/pdflatex";
    private static void createPdf(string argument)
    {
        using var process = new Process();
        
        process.StartInfo.FileName = texCompile;
        process.StartInfo.Arguments = argument;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;

        process.Start();
        process.WaitForExit();

        if (process.ExitCode == 0)
        {
            return;
        }
            
        var errorOutput = process.StandardError.ReadToEnd();
        throw new ArgumentException($"The latex compiler failed with exit code {process.ExitCode}." +
                                    $"\nWith argument: {argument}" +
                                    $"\nError output: {errorOutput}");
    }

    private void cleanFiles()
    {
        var auxiliaryFiles = Directory
            .GetFiles(latexBuilder.getOutPath(), $"{latexBuilder.getFileName()}.*")
            .Where(f => !f.EndsWith(".pdf"));
        
        foreach (var file in auxiliaryFiles)
        {
            File.Delete(file);
        }
    }

    private string pdfPath => Path.Combine(latexBuilder.getOutPath(), $"{latexBuilder.getFileName()}.pdf");
    public byte[] getPdf() => File.ReadAllBytes(pdfPath);
}
