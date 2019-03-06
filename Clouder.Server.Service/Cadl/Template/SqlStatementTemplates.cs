using System;
namespace Clouder.Server.Service.Cadl
{
    public static class SqlStatementTemplates
    {
        public static string SelectScalar = @"
// - SELECT statement running on database '#database'
// - Will generate a scalar output placed into '#output'
// - (see #cadl-help-sql for more information on CADL SQL statements)
#output = sql(#database)
{
#sql
}";

        public static string SelectEntity = @"
// - SELECT statement running on database '#database'
// - Will generate a #entity output placed into '#output'
// - (see #cadl-help-sql for more information on CADL SQL statements)
sql(#database, #output) as #entity
{
#sql
}";

    public static string UpsertOrDeleteNoOutout = @"
// - SQL statement running on database '#database'
// - (see #cadl-help-sql for more information on CADL SQL statements)
#output = sql(#database)
        {
#sql
        }";

        public static string InsertWithOutout = @"
// - INSERT statement running on database '#database'
// - Will placed the ID of the inserted value into '#output'
// - (see #cadl-help-sql for more information on CADL SQL statements)
#output = sql(#database)
        {
#sql
        }";
    }
}
