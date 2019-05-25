using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DP.V2.Core.WebApi.Dependencies
{
    public class DependencyProvider
    {
        public static IServiceCollection Service;
        private static Dictionary<Type, HashSet<Type>> _internalTypeMapping;
        private static readonly string[] _namespacePLG = new string[] {
            "DP.V2.PLG.Auth",
            "DP.V2.PLG.Identity",
            "DP.V2.PLG.Route",
            "DP.V2.PLG.Role",
            "DP.V2.PLG.Log"
        };

        static DependencyProvider()
        {
            Service = HttpContext.ServiceCollection;
            var platform = Environment.OSVersion.Platform.ToString();
            var runtimeAssemblyNames = DependencyContext.Default.GetRuntimeAssemblyNames(platform);
            var list = runtimeAssemblyNames
                        .Select(Assembly.Load)
                        .SelectMany(a => a.ExportedTypes)
                        .Where(x => x.IsClass && x.Namespace.StartsWith("DP.V2", StringComparison.InvariantCultureIgnoreCase));

            // load dll plugin - begin
            foreach(string np in _namespacePLG)
            {
                if (list.Any(x => x.Namespace.Contains(np)))
                {
                    var controllersAssembly = Assembly.Load(new AssemblyName(np));
                    Service.AddMvc().AddApplicationPart(controllersAssembly).AddControllersAsServices();
                }
            }
            // load dll plugin - end

            foreach (var type in list)
            {
                var allInterfacesOnType = type.GetInterfaces().Select(i => i.IsGenericType ? i.GetGenericTypeDefinition() : i).ToList();

                var interfacesOnType = allInterfacesOnType.Where(i => i.Name.Contains(type.Name)).ToList();

                if (_internalTypeMapping == null)
                {
                    _internalTypeMapping = new Dictionary<Type, HashSet<Type>>();
                }

                interfacesOnType.ForEach(interfaceOnType =>
                {
                    if (!_internalTypeMapping.ContainsKey(interfaceOnType))
                    {
                        _internalTypeMapping[interfaceOnType] = new HashSet<Type>();
                    }

                    _internalTypeMapping[interfaceOnType].Add(type);
                });
            }

            foreach (var typeMapping in _internalTypeMapping)
            {
                if (typeMapping.Value.Count == 1)
                {
                    var type = typeMapping.Value.First();
                    Service.AddScoped(typeMapping.Key, type);
                }
            }
        }

        public static void Configure() { }

        public static T Resolve<T>()
        {
            T ret = default(T);

            ret = (T)Service.BuildServiceProvider().GetService(typeof(T));

            if (ret == null)
                throw new InvalidOperationException(string.Format("Type {0} not registered in service", typeof(T).Name));

            return ret;
        }
    }
}
