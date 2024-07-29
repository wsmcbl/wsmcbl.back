using System.Diagnostics;

namespace wsmcbl.src.controller.service;

public class PdfGenerator
{
    private readonly string _texLivePath;

    public PdfGenerator(string texLivePath)
    {
        _texLivePath = texLivePath;
    }

    public void GeneratePdf(string texFilePath)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = _texLivePath,
                Arguments = $"-output-directory {Path.GetDirectoryName(texFilePath)} {texFilePath}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        process.Start();
        process.WaitForExit();
    }
}
