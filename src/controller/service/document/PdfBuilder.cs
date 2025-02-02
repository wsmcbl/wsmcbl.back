using System.Diagnostics;

namespace wsmcbl.src.controller.service.document;

public class PdfBuilder
{
    private readonly LatexBuilder latexBuilder;

    public PdfBuilder(LatexBuilder latexBuilder)
    {
        this.latexBuilder = latexBuilder;
    }
    
    public PdfBuilder build()
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
        throw new ArgumentException($"The latex compiler failed with exit code: ({process.ExitCode}). " +
                                    $"With argument: ({argument}). " +
                                    $"Error output: ({errorOutput}).");
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
