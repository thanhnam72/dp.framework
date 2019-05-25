using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace System.Web
{
    public static class HttpContext
    {
        private static IServiceCollection _serviceCollection;

        private static IHttpContextAccessor m_httpContextAccessor;

        public static void Configure(IServiceCollection service)
        {
            _serviceCollection = service;
            m_httpContextAccessor = service.BuildServiceProvider()
                                        .GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
        }

        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                return m_httpContextAccessor.HttpContext;
            }
        }

        public static IServiceCollection ServiceCollection
        {
            get
            {
                return _serviceCollection;
            }
        }
    }
}
