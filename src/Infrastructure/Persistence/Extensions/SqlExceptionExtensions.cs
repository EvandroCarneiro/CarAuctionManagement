namespace BCA.CarAuctionManagement.Infrastructure.Persistence.Extensions;

using Microsoft.Data.SqlClient;

internal static class SqlExceptionExtensions
{
    public static bool IsUniqueConstraintException(this SqlException sqlException)
        => sqlException.Errors.Cast<SqlError>().Any(error => error.Number is 2627);
}
