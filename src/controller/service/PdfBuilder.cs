using System.Diagnostics;

namespace wsmcbl.src.controller.service;

public class PdfBuilder<T> where T : class
{
    private readonly ILatexBuilder<T> latexBuilder;

    public PdfBuilder(ILatexBuilder<T> latexBuilder)
    {
        this.latexBuilder = latexBuilder;
    }
    
    public PdfBuilder<T> build()
    {
        var command = $"-interaction=nonstopmode -pdf" +
                      $" -output-directory=\"{latexBuilder.getOutPath()}\"" +
                      $" \"{latexBuilder.getFilePath()}\"";

        createPdf(command);
        cleanFiles();
        return this;
    }
    
    private const string texCompile = "/usr/bin/latexmk";
    private static void createPdf(string command)
    {
        using var process = new Process();
        
        process.StartInfo.FileName = texCompile;
        process.StartInfo.Arguments = command;
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
        throw new ArgumentException($"latexmk failed with exit code {process.ExitCode}." +
                                    $" Error output:\n{errorOutput}");
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
