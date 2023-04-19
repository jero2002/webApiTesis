namespace webApiTesis.Comun
{
    public class Constantes
    {
        public static class DefaultMessages
        {
            public const string DefaultErrorMessage = "¡Houston, tenemos un problema!. " +
                                                      "Si el problema persiste contacte al equipo de IT.";
            public const string DefaultSuccesMessage = "Los cambios se guardaron exitosamente.";
        }

        public static class DefaultSecurityValues
        {
            public const string DefaultUserName = "usuario1";
        }

        public static class DefinicionRoles
        {
            public const string Reportes = "Admin";
            public const string Conectar = "Competidor";
        }
    }
}
