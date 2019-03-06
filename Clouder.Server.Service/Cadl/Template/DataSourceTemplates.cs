using System;
namespace Clouder.Server.Service.Cadl
{
    public static class DataSourceTemplates
    {
        public static string SqlDatabase = @"
// - Creates a database called '#database'
// - CADL script will refer to this function as '#comp-name'
// - (see #cadl-help-sources for more information on CADL data sources)
component(SQL, #database) #comp-name
{
#sql
}";

        public static string Queue = @"
// - Creates a queue called '#queue'
// - CADL script will refer to this function as '#comp-name'
// - (see #cadl-help-sources for more information on CADL data sources)
component(Queue, #queue) paymentQueue";
    }
}
