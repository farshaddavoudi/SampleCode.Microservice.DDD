using SampleDDDMicroserviceApp.TransportPeople.Application.Common.Contracts;
using SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace SampleDDDMicroserviceApp.TransportPeople.Application.Common.Localization.Extensions
{
    public static class ValidationContextExtension
    {
        public static ILocalStringProvider GetStringProvider(this ValidationContext validationContext)
        {
            // Request originated from AspNetCore Controllers
            var stringsProvider = validationContext.GetService(typeof(ILocalStringProvider));

            if (stringsProvider is not null)
                return (ILocalStringProvider)stringsProvider;

            // Below block should be uncomment in backend only projects. Blazor will use AspNetCore context and
            // will validate the dto in client-side, so no need to backend error message.

            #region Request originated from Bit Controllers

            //var context = DefaultDependencyManager.Current.Resolve<IHttpContextAccessor>().HttpContext;

            //validationContext.InitializeServiceProvider(type => context!.RequestServices.GetRequiredService(type));

            //return validationContext.GetRequiredService<IStringsProvider>();

            #endregion

            throw new BusinessRuleException("In Blazor applications, this error should not happen! See the above comments");
        }
    }
}