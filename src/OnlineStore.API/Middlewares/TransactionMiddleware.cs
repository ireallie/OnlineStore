using OnlineStore.Data;

namespace OnlineStore.Api.Middlewares
{
    public class TransactionMiddleware
    {
        private readonly RequestDelegate _next;

        public TransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ApplicationDbContext databaseContext)
        {
            await using var transaction = await databaseContext.Database.BeginTransactionAsync();

            try
            {
                await _next(context);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
            }
        }
    }
}
