using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Extensions
{
    public class PermissaoNecessario : IAuthorizationRequirement
    {
        public string Permissao { get; set; }

        public PermissaoNecessario(string permissao)
        {
            Permissao = permissao;
        }
    }

    public class PermissaoNecessariaHandler : AuthorizationHandler<PermissaoNecessario>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissaoNecessario requirement)
        {
            if (context.User.HasClaim(c => c.Type == "Permissao" && c.Value.Contains(requirement.Permissao)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
