using DraggableElementRCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddDraggableElementServices(this IServiceCollection services)
    {
        services.AddScoped<IDraggableElementJsInterop, DraggableElementJsInterop>();
        return services;
    }
}
