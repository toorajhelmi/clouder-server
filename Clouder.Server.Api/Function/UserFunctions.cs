using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clouder.Server.Dto;
using Clouder.Server.Entity;
using Clouder.Server.Helper.Azure;
using Clouder.Server.Helper.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Clouder.Server.Api.Function
{
    public static class UserFunctions
    {
        private const int MaxItemCount = 100;
        private const string colId = "User";
        private static FeedOptions feedOptions = new FeedOptions { MaxItemCount = MaxItemCount };

        static UserFunctions()
        {
            Startup.Spin();
        }     

        [FunctionName("User_Create")]
        public static async Task<IActionResult> CreateUser(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            ILogger log)
        {
            return await HttpF.Create(colId, new IndexingPolicy(new HashIndex(DataType.Number))
            {
                IndexingMode = IndexingMode.Consistent,
            });
        }

        [FunctionName("User_Get")]
        public static async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            ILogger log)
        {
            try
            {
                return new OkObjectResult(new List<FactoryDto>
                {
                    new FactoryDto { Id = "1", Name = "Factory 1",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."},
                    new FactoryDto { Id = "2", Name = "Factory 2", Description = "E-Commerce"},
                    new FactoryDto { Id = "3", Name = "Factory 3", Description = "Ticketing"},
                    new FactoryDto { Id = "4", Name = "Factory 4", Description = "Trading"},
                    new FactoryDto { Id = "5", Name = "Factory 5", Description = "Media"},
                    new FactoryDto { Id = "6", Name = "Factory 6", Description = "Multi-tennant"},
                });

                var userName = req.Parse("userName").ToLower();
                var password = req.Parse("password");
                var user = await NakedF.Get<Entity.User, UserDto>(colId,
                    query => query.Where(u =>
                                         u.Username == userName &&
                                         u.Password == password));

                LoginResponse loginResponse = new LoginResponse();

                if (user == null)
                {
                    loginResponse.WrongLogin = true;
                }
                else if (!user.Activated)
                {
                    loginResponse.NotActivated = true;
                }
                else if (user.Suspended)
                {
                    loginResponse.Suspended = true;
                }
                else
                {
                    loginResponse.Succeeded = true;
                    loginResponse.User = user;
                }

                return new OkObjectResult(loginResponse);
            }
            catch (System.Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [FunctionName("User_Add")]
        public static async Task<IActionResult> Add(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req,
            ILogger log)
        {
            var user = await req.Parse<Entity.User>();
            user.ActivationCode = System.Guid.NewGuid().ToString();
            user.Username = user.Username.ToLower();
            user.Email = user.Email.ToLower();
            user = await NakedF.Add<Entity.User>(user, colId);

            //SendEmail(Localization.Instance.ActivateAcount,
                      //new EmailAddress { Email = user.Email, Name = user.FullName },
                      //"content",
                      //user);

            return new OkObjectResult(user);
        }

        [FunctionName("User_Activate")]
        public static async Task<IActionResult> Activate(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            ILogger log)
        {
            var activationCode = req.Parse("c");
            var userId = req.Parse("uid");
            var user = await NakedF.Get<Entity.User>(userId, colId);

            if (user != null && user.ActivationCode == activationCode)
            {
                user.Activated = true;
                //user.Rewards = new System.Collections.Generic.List<Reward>
                //{
                //    new Reward
                //    {
                //        Credit = earlyUserReward.Prize,
                //        Description = string.Format(earlyUserReward.Description, earlyUserReward.Prize),
                //        Code = (int)earlyUserReward.LotteryType
                //    }
                //};

                await NakedF.Update<Entity.User>(user, colId);

                return new ContentResult
                {
                    Content = "content",
                    ContentType = "text/html;charset=UTF-8"
                };
            }
            else
            {
                return new ContentResult
                {
                    Content = "",
                    ContentType = "text/html;charset=UTF-8",
                };
            }
        }

        [FunctionName("User_Update")]
        public static async Task<IActionResult> Update(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req,
            ILogger log)
        {
            return await HttpF.Update<Entity.User>(req, colId);
        }

        [FunctionName("User_IsUsernameTaken")]
        public static async Task<IActionResult> IsUsernameTaken(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            ILogger log)
        {
            var userName = req.Parse("userName");
            var email = req.Parse("email");
            var users = await NakedF.Get<Entity.User, UserDto>(
                colId, feedOptions, query =>
                query.Where(u => u.Username == userName.ToLower()));
            var accountExistsRepsonse = new AccountExistsResponse();
            if (users.Any())
            {
                accountExistsRepsonse.UsernameTaken = true;
            }

            users = await NakedF.Get<Entity.User, UserDto>(
                colId, feedOptions, query =>
                query.Where(u => u.Email == email.ToLower()));
            if (users.Any())
            {
                accountExistsRepsonse.EmailTaken = true;
            }

            return new OkObjectResult(accountExistsRepsonse);
        }

        //private static async void SendEmail(string subject, EmailAddress to, string template, Entity.User user)
        //{
        //    var content = template
        //        .Replace("[BUYER]", user.Username)
        //        .Replace("[SELLER]", user.Username)
        //        .Replace("[USER-NAME]", user.Username)
        //        .Replace("[PASSWORD]", user.Password)
        //        .Replace("[ACTIVATION-LINK]", $"<a href='{string.Format(Configuration.ActivationLink, user.Id, user.ActivationCode)}'>{Localization.Instance.ActivateAcount}<a/>");

        //    await Email.Send(Configuration.GeneralSupportEmail,
        //                     subject,
        //                     new EmailAddress { Email = user.Email, Name = user.FullName },
        //                     content);
        //}
    }
}