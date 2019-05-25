using DP.V2.Core.Common.Base;
using DP.V2.Core.Common.Ultilities;
using DP.V2.Core.Data.DataModel;
using DP.V2.Core.Data.Interface;
using DP.V2.Core.WebApi.Attributes;
using DP.V2.Core.WebApi.Configuration;
using DP.V2.Core.WebApi.Dependencies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DP.V2.Core.WebApi.Filters
{
    public class ApiAuthentication : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = GetResultError(context.Exception.Message, HttpStatusCode.InternalServerError);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items.Add("ControllerName", ((ControllerActionDescriptor)context.ActionDescriptor).ControllerName);
            context.HttpContext.Items.Add("ActionName", ((ControllerActionDescriptor)context.ActionDescriptor).ActionName);

            bool bypassAuth = AppSetting.GetObject<bool>("EnableAuthenticate");

            if (!bypassAuth || AllowAnomynous(context))
            {
                base.OnActionExecuting(context);
                return;
            }

            string provider = context.HttpContext.Request.Headers["Provider"];

            if (string.IsNullOrEmpty(provider)) //authen by token
            {
                ValidateToken(context);
            }
            else if (provider.Equals("1")) // authen by username and password
            {
                ValidateIdentity(context);
            }
            else
            {
                context.Result = GetResultError("Provider không hợp lệ! Bạn không có quyền truy cập.", HttpStatusCode.Unauthorized);
            }

            base.OnActionExecuting(context);
        }

        /// <summary>
        /// Định dạng dối tượng response
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private ObjectResult GetResultError(string message, HttpStatusCode code)
        {
            var response = new BaseResponse
            {
                ErrorCode = -1,
                Errors = message
            };

            var result = new BaseServiceResult()
            {
                Result = response,
                Success = false,
                ReturnCode = code
            };

            return new ObjectResult(result);
        }

        /// <summary>
        /// Định dạng dối tượng response
        /// Token hết hạn
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private ObjectResult GetResultOutOfTokenError(string message, HttpStatusCode code)
        {
            var response = new BaseResponse
            {
                ErrorCode = -2,
                Errors = message
            };

            var result = new BaseServiceResult()
            {
                Result = response,
                Success = false,
                ReturnCode = code
            };

            return new ObjectResult(result);
        }

        /// <summary>
        /// Kiểm tra action truy cập có check token không
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private bool AllowAnomynous(ActionExecutingContext actionContext)
        {
            return (actionContext.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes(typeof(AllowAnonymous), false).Count() > 0;
        }

        /// <summary>
        /// Lấy thông tin token từ request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetTokenFromRequest(ActionExecutingContext context)
        {
            if (context.Controller.GetType().IsSubclassOf(typeof(Controller)))
            {
                return context.HttpContext.Request.Cookies["_tk"];
            }
            else
            {
                return context.HttpContext.Request.Headers["Authorization"];
            }
        }

        /// <summary>
        /// Xác thực quyền truy cap hệ thống bằng token
        /// </summary>
        /// <param name="context"></param>
        private void ValidateToken(ActionExecutingContext context)
        {
            string token = GetTokenFromRequest(context);

            if (string.IsNullOrEmpty(token))
            {
                context.Result = GetResultError("Bạn không có quyền truy cập", HttpStatusCode.Unauthorized);
            }
            else
            {
                try
                {
                    IRepository<SysUser> repoUsers = DependencyProvider.Resolve<IRepository<SysUser>>();

                    SysUser user = repoUsers.FindOne(x => x.Token.Equals(token) && x.TokenExp.Value > DateTime.Now);
                    SysUser userLimitToken = repoUsers.FindOne(x => x.Token.Equals(token) && x.TokenExp.Value < DateTime.Now);
                    if (userLimitToken !=null)
                    {
                        context.Result = GetResultOutOfTokenError("Token đã hết hạn.", HttpStatusCode.Unauthorized);
                        return;
                    }

                    if (user == null)
                    {
                        context.Result = GetResultError("Token không hợp lệ.", HttpStatusCode.Unauthorized);
                        return;
                    }

                    if(AppSetting.GetObject<bool>("EnableAuthorize"))
                    {
                        var result = CheckPermission(context, user);

                        if (!result)
                            return;
                    }

                    // System.Web.HttpContext.Current.Items["UserContext"] = user;
                    context.HttpContext.Items.Add("UserContext", user);
                }
                catch (Exception ex)
                {
                    context.Result = GetResultError(ex.Message, HttpStatusCode.InternalServerError);
                }
            }
        }

        /// <summary>
        /// Xác thực quyền truy cap hệ thống bằng username và password
        /// </summary>
        /// <param name="context"></param>
        private void ValidateIdentity(ActionExecutingContext context)
        {
            string username = context.HttpContext.Request.Headers["Username"];
            string password = context.HttpContext.Request.Headers["Password"];

            try
            {
                IRepository<SysUser> repoUsers = DependencyProvider.Resolve<IRepository<SysUser>>();

                SysUser user = repoUsers.FindOne(x => x.Username.Equals(username) && x.Password.Equals(password));

                if (user == null)
                {
                    context.Result = GetResultError("Xác thực không hợp lệ.", HttpStatusCode.BadRequest);
                    return;
                }

                if (AppSetting.GetObject<bool>("EnableAuthorize"))
                {
                    var result = CheckPermission(context, user);

                    if (!result)
                        return;
                }

                System.Web.HttpContext.Current.Items["UserContext"] = user;
            }
            catch (Exception ex)
            {
                context.Result = GetResultError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Kiểm tra phân quyền của user đang truy cập
        /// </summary>
        /// <param name="context"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool CheckPermission(ActionExecutingContext context, SysUser user)
        {
            if (user.RoleId == null)
            {
                context.Result = GetResultError("User chưa được phân quyền.", HttpStatusCode.Unauthorized);
                return false;
            }

            string fnCd = IsRequestAPI(context) ?
                        Convert.ToString(((dynamic)context.ActionArguments["action"]).FnCd) :
                        context.HttpContext.Request.Query["fnCode"].ToString();

            if (string.IsNullOrEmpty(fnCd))
            {
                context.Result = GetResultError("Không tìm thấy thông tin FnCd.", HttpStatusCode.BadRequest);
                return false;
            }

            IRepository<SysPermission> repoPermis = DependencyProvider.Resolve<IRepository<SysPermission>>();

            var permisRecord = repoPermis.FindOne(x => x.RoleId.Equals(user.RoleId.Value) && x.FnCd.Equals(fnCd));

            if (!IsRequestAPI(context))
            {
                if(permisRecord == null || !permisRecord.View)
                {
                    context.Result = GetResultError("Bạn không có quyền truy cập trang này !", HttpStatusCode.Unauthorized);
                    return false;
                }
                    
                return true;
            }

            var attrs = context.ActionArguments["action"].GetType().GetCustomAttributes(typeof(PermisOption), false);

            IList<string> permisTypes;

            if (attrs.Count() == 0)
            {
                permisTypes = new List<string>();

                string actionName = context.ActionArguments["action"].GetType().Name;

                if (actionName.StartsWith("Get"))
                    permisTypes.Add("View");
                else if (actionName.StartsWith("Update") || actionName.StartsWith("Edit"))
                    permisTypes.Add("Update");
                else if (actionName.StartsWith("Insert") || actionName.StartsWith("Create"))
                    permisTypes.Add("Insert");
                else if (actionName.StartsWith("Remove") || actionName.StartsWith("Delete"))
                    permisTypes.Add("Remove");
                else if (actionName.StartsWith("Import"))
                    permisTypes.Add("Import");
                else if (actionName.StartsWith("Export"))
                    permisTypes.Add("Export");
                else
                {
                    context.Result = GetResultError("Action này chưa được phân quyền", HttpStatusCode.Unauthorized);
                    return false;
                }
            }
            else
            {
                permisTypes = ((PermisOption)attrs[0]).PermisTypes;
            }

            if (permisTypes.Count == 0)
                throw new Exception("Exist apply attribute PermisOption but not value");

            if (permisTypes.Any(x => x.Equals("*")))
                return true;

            if (permisRecord == null)
            {
                context.Result = GetResultError("Không tìm thấy thông tin phân quyền với FnCd này.", HttpStatusCode.Unauthorized);
                return false;
            }

            foreach (var type in permisTypes)
            {
                var permisValue = ObjectHelper.GetValue(type, permisRecord);

                if (permisValue == null || (bool)permisValue == false)
                {
                    context.Result = GetResultError("Bạn không có quyền truy cập action này !", HttpStatusCode.Unauthorized);
                    return false;
                }
            }

            return true;
        }

        private bool IsRequestAPI(ActionExecutingContext context)
        {
            if (context.Controller.GetType().IsSubclassOf(typeof(Controller)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
