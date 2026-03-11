using wsmcbl.src.controller.service;
using wsmcbl.src.exception;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class LoginController : BaseController
{
    private JwtGenerator jwtGenerator { get; }
    private UserAuthenticator userAuthenticator { get; }
    private EmailNotifierService emailNotifierService { get; }

    public LoginController(DaoFactory daoFactory, JwtGenerator jwtGenerator, UserAuthenticator userAuthenticator, EmailNotifierService emailNotifierService) : base(daoFactory) 
    {
        this.jwtGenerator = jwtGenerator;
        this.userAuthenticator = userAuthenticator;
        this.emailNotifierService = emailNotifierService;
    }

    public async Task<string> getTokenByCredentials(UserEntity user)
    {
        var result = await userAuthenticator.authenticateUser(user);
        if (!result.isActive)
        {
            throw new ConflictException("This user is disabled.");
        }

        _ = Task.Run(async () =>
        {
            try
            {
                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo nicaraguaZone = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
                DateTime managuaTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, nicaraguaZone);

                string format = "dddd, dd 'de' MMMM 'de' yyyy HH:mm:ss";
                string managuaDesc = managuaTime.ToString(format, new System.Globalization.CultureInfo("es-NI"));
                string utcDesc = utcNow.ToString(format, new System.Globalization.CultureInfo("es-NI"));

                string fullName = $"{result.name} {result.secondName} {result.surname} {result.secondSurname}"
                    .Replace("  ", " ").Trim();
                string roleName = result.role?.name ?? "No especificado";

                var logsEmails = new List<string> { "logs@cbl-edu.com" };
                string subject = $"NOTIFICACIÓN DE SEGURIDAD: Acceso detectado - {result.email}";

                string body = $@"
        <div style='font-family: sans-serif; border: 1px solid #e0e0e0; padding: 20px; border-radius: 8px;'>
            <h2 style='color: #4e73df;'>Reporte de Acceso</h2>
            <p>Se ha generado un nuevo token de autenticación en el sistema <b>WSM-CBL</b>.</p>
            
            <table style='width: 100%; border-collapse: collapse; font-size: 14px;'>
                <tr style='background-color: #f8f9fa;'>
                    <td style='padding: 10px; border: 1px solid #ddd;'><b>Usuario:</b></td>
                    <td style='padding: 10px; border: 1px solid #ddd;'>{fullName}</td>
                </tr>
                <tr>
                    <td style='padding: 10px; border: 1px solid #ddd;'><b>Email:</b></td>
                    <td style='padding: 10px; border: 1px solid #ddd;'>{result.email}</td>
                </tr>
                <tr style='background-color: #f8f9fa;'>
                    <td style='padding: 10px; border: 1px solid #ddd;'><b>Rol del Usuario:</b></td>
                    <td style='padding: 10px; border: 1px solid #ddd;'>{roleName} (ID: {result.roleId})</td>
                </tr>
                <tr>
                    <td style='padding: 10px; border: 1px solid #ddd;'><b>Hora Managua:</b></td>
                    <td style='padding: 10px; border: 1px solid #ddd; color: #4e73df;'><b>{managuaDesc}</b></td>
                </tr>
                <tr style='background-color: #f8f9fa;'>
                    <td style='padding: 10px; border: 1px solid #ddd;'><b>Hora Universal (UTC):</b></td>
                    <td style='padding: 10px; border: 1px solid #ddd;'>{utcDesc} Z</td>
                </tr>
            </table>

            <p style='margin-top: 25px; font-size: 0.85em; color: #777; border-top: 1px solid #eee; padding-top: 10px;'>
                <b>ID de Rastreo:</b> {result.userId}
            </p>
        </div>";

                await emailNotifierService.sendEmail(logsEmails, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fallo envío de correo de auditoría: {ex.Message}");
            }
        });

        await result.getIdFromRole(daoFactory);
        return jwtGenerator.generateToken(result);
    }
}