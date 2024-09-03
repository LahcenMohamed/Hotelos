using Hotelos.Domain.Hotels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Hotelos.Application.Custom
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser, IdentityRole>
    {
        private readonly IRepository<Hotel> _repository;

        public CustomUserClaimsPrincipalFactory(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IRepository<Hotel> repository,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
            _repository = repository;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            //if (await UserManager.IsInRoleAsync(user, "Hotel"))
            //{
            //    var hotel = await _repository.FirstOrDefaultAsync(x => x.UserId == user.Id);

            //    if (hotel != null)
            //    {
            //        identity.AddClaim(new Claim("hotelId", hotel.Id.ToString()));
            //    }
            //}

            return identity;
        }

    }
}
