using GenericApp.Infra.CC.Localization.Resources;
using Microsoft.Data.SqlClient;
using System.Net;

namespace GenericApp.Infra.Data.Extensions
{
    public static class SqlExceptionExtensions
    {
        public static string GetSqlErrorMessage(this SqlException ex)
        {
            return ex.Number switch
            {

                53 => SharedResource.DatabaseServerUnreacheble,
                547 => SharedResource.CannotDeleteReferencedRecord,
                2601 => SharedResource.CannotInsertDuplicatedRow,
                _ => SharedResource.CannotConnectToDatabase,
            };
        }

        public static int GetSqlErrorCode(this SqlException ex)
        {
            return ex.Number switch
            {
                _ => (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
