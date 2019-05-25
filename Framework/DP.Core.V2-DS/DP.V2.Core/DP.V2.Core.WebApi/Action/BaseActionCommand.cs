using DP.V2.Core.Common.Base;
using DP.V2.Core.WebApi.Dependencies;
using FluentValidation.Results;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace DP.V2.Core.WebApi.Action
{
    public abstract class BaseActionCommand<TBusiness>
    {
        /// <summary>
        /// Function Code
        /// </summary>
        public string FnCd { get; set; }

        /// <summary>
        /// Function Code
        /// </summary>
        protected ValidationResult validate { get; set; }

        /// <summary>
        /// Business Logic Type
        /// </summary>
        private readonly Type _businessLogicType;

        /// <summary>
        /// Instance of Business Logic
        /// </summary>
        private readonly TBusiness _businessLogic;

        /// <summary>
        /// The current Action Name
        /// </summary>
        private readonly string _nameAction;

        /// <summary>
        /// The current method Name
        /// </summary>
        private readonly string _methodName;

        protected BaseActionCommand()
        {
            _businessLogicType = typeof(TBusiness);
            _businessLogic = DependencyProvider.Resolve<TBusiness>();
            _nameAction = GetType().Name;

            if (!string.IsNullOrEmpty(_nameAction))
            {
                _methodName = _nameAction.Substring(0, _nameAction.Length - 6);
            }
        }

        public abstract bool Validate();
        public abstract Task<BaseServiceResult> ExecuteAsync();

        public async Task<BaseServiceResult> Execute<TResponse>(params object[] methodArgs) 
            where TResponse : BaseResponse, new()
        {
            var serviceResult = new BaseServiceResult()
            {
                ReturnCode = HttpStatusCode.OK,
                Success = true,
            };

            if(!Validate())
            {
                if(validate != null)
                {
                    serviceResult.Result = new BaseResponse()
                    {
                        ErrorCode = -1,
                        Errors = validate.Errors[0].ErrorMessage
                    };
                }

                serviceResult.Success = false;
                serviceResult.ReturnCode = HttpStatusCode.BadRequest;
                return serviceResult;
            }

            try
            {
                serviceResult.Result = await OnExecute<TResponse>(methodArgs);

                if(!string.IsNullOrEmpty(((BaseResponse)serviceResult.Result).Errors))
                {
                    serviceResult.Success = false;
                }
            }
            catch(Exception ex)
            {
                serviceResult.Result = HandleExeption<TResponse>(ex);
                serviceResult.ReturnCode = HttpStatusCode.InternalServerError;
                serviceResult.Success = false;
            }

            return serviceResult;
        }

        private async Task<TResponse> OnExecute<TResponse>(params object[] methodArgs) where TResponse : BaseResponse, new()
        {
            TResponse response = default(TResponse);

            try
            {
                if (_businessLogicType != null)
                {
                    MethodInfo methodInfo = _businessLogicType.GetMethod(_methodName);

                    if (methodInfo != null)
                    {
                        dynamic awaitable = methodInfo.Invoke(_businessLogic, methodArgs);

                        await awaitable;

                        response = (TResponse)awaitable.GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception ex)
            {
                //return HandleExeption<TResponse>(ex);
                throw ex;
            }

            return response;
        }

        private TResponse HandleExeption<TResponse>(Exception ex) where TResponse : BaseResponse, new()
        {
            Type type = ex.GetType();
            string innerError = "";

            if(ex.InnerException != null)
            {
                innerError = ex.InnerException.Message;
            }

            return new TResponse()
            {
                Errors = ex.Message + ". \r\n" + innerError
            };
        }
    }
}
