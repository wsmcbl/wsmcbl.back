using System.Diagnostics;

namespace wsmcbl.back.config;

public class TerminalHelper
{
    public static string ExecuteCommand(string command)
    {
        // Crear un proceso para ejecutar el comando en la terminal
        Process process = new Process();
        process.StartInfo.FileName = "/bin/bash"; // Especifica que el shell de bash se utilizará para ejecutar el comando
        process.StartInfo.Arguments = $"-c \"{command}\""; // Especifica el comando a ejecutar
        process.StartInfo.RedirectStandardOutput = true; // Redirige la salida estándar para que podamos leerla
        process.StartInfo.UseShellExecute = false; // No use el shell del sistema operativo para iniciar el proceso
        process.StartInfo.CreateNoWindow = true; // No cree una ventana para el proceso
        process.Start(); // Inicia el proceso

        // Lee la salida estándar del proceso y la almacena en una variable
        string output = process.StandardOutput.ReadToEnd();

        // Espera a que el proceso termine
        process.WaitForExit();

        // Devuelve la salida del comando
        return output;
    }
}
