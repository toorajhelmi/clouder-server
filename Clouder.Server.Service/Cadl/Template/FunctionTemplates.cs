using System;
namespace Clouder.Server.Service.Cadl
{
    public static class FunctionTemplates
    {

        public static string ReqInFunction = @"
// - Function '#func-name' triggered by a HTTP Request
// - CADL script will refer to this function as '#comp-name
// - The post body is placed into a variable called '#input'
// - Function will be deployed at #size capacity 
// - (see #cadl-help-func for more information on CADL functions)
component(Function, #func-name) #comp-name size:#size
input(request, #input)
{
#body
}";
        public static string ReqInQueueOutFunction = @"
// - Function '#func-name' triggered by a HTTP Request
// - CADL script will refer to this function as '#comp-name
// - The post body is placed into a variable called '#input'
// - Function will place data in a Queue called '#output'
// - Function will be deployed at #size capacity 
// - (see #cadl-help-func for more information on CADL functions)
component(Function, #func-name) #comp-name size:#size
input(request, #input), output(#output)
{
#body
}";

        public static string TimerInFunction = @"
// - Function '#func-name' triggered priodically every #period seconds
// - CADL script will refer to this function as '#comp-name
// - Function will be deployed at #size capacity 
// - (see #cadl-help-func for more information on CADL functions)
component(Function, #comp-name) #comp-name size:#size
input(timer, #period)
{
#body
}";

    public static string TimerInQueueOutFunction = @"
// - Function '#func-name' triggered priodically every #period seconds
// - CADL script will refer to this function as '#comp-name
// - Function will place data in a Queue called '#output'
// - Function will be deployed at #size capacity 
// - (see #cadl-help-func for more information on CADL functions)
component(Function, #func-name) #comp-name size:#size
input(timer, #period, output(#output)
{
#body
}";

        public static string QueueInFunction = @"
// - Function '#func-name' triggered when data is added to queue called '#queue'
// - CADL script will refer to this function as '#comp-name
// - Queue data is placed in a variable called '#input'
// - Function will be deployed at #size capacity 
// - (see #cadl-help-func for more information on CADL functions)
component(Function, #func-name) #comp-name size:#size
input(queue, #queue , #input)
{
#body
}";

        public static string QueueInQueueOutFunction = @"
// - Function '#func-name' triggered when data is added to queue called '#queue'
// - CADL script will refer to this function as '#comp-name
// - Queue data is placed in a variable called '#input'
// - Function will place data in a Queue called '#output'
// - Function will be deployed at #size capacity 
// - (see #cadl-help-func for more information on CADL functions)
component(Function, #func-name) #comp-name size:#size
input(queue, #queue , #input), output(#output)
{
#body
}";
    }
}