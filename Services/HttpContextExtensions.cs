namespace GestionVoitureFrontOffice.Services
{
    public static class HttpContextExtensions
    {
        public static string GetUserEmail(this HttpContext context)
        {
            return context.Items["UserEmail"]?.ToString();
        }

        public static int? GetIdUser(this HttpContext context)
        {
            if (int.TryParse(context.Items["idUser"]?.ToString(), out int idUser))
            {
                return idUser;
            }
            return null;
        }

        public static string GetUserRole(this HttpContext context)
        {
            return context.Items["UserRole"]?.ToString();
        }
    }
}
