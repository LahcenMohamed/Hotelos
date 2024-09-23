using Hotelos.Application.Contracts.Subscriptions;
using Hotelos.Domain.Employees;
using Hotelos.Domain.Hotels;
using Hotelos.Domain.Subscription.Entities.SubscriptionHotels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

public class CustomSignInManager : SignInManager<IdentityUser>, ITransientDependency
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly IRepository<SubscriptionHotel> _subscriptionHotelRepository;
    private readonly IRepository<Hotel> _hotelRepository;
    private readonly IRepository<Employee> _employeeRepository;

    public CustomSignInManager(
        UserManager<IdentityUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<IdentityUser> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<IdentityUser>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<IdentityUser> confirmation,
        ISubscriptionService subscriptionService,
        IRepository<SubscriptionHotel> subscriptionHotelRepository,
        IRepository<Hotel> hotelRepository,
        IRepository<Employee> employeeRepository)
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
        _subscriptionService = subscriptionService;
        _subscriptionHotelRepository = subscriptionHotelRepository;
        _hotelRepository = hotelRepository;
        _employeeRepository = employeeRepository;
    }

    public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
    {
        var user = await UserManager.FindByNameAsync(userName);
        if (user == null)
        {
            return SignInResult.Failed;
        }
        if (!await UserManager.IsInRoleAsync(user, "admin"))
        {
            int hotelId = 0;
            if (await UserManager.IsInRoleAsync(user, "HotelAdmin"))
            {
                var hotel = await _hotelRepository.FirstOrDefaultAsync(x => x.UserId == user.Id);
                hotelId = hotel.Id;
            }
            else if (await UserManager.IsInRoleAsync(user, "Employee"))
            {
                var emp = await _employeeRepository.FirstOrDefaultAsync(x => x.UserId == user.Id);
                var hotel = await _hotelRepository.FirstOrDefaultAsync(x => x.Id == emp.HotelId);
                hotelId = hotel.Id;
            }

            var DateNow = DateOnly.FromDateTime(DateTime.Now);
            var isSubscribed = await _subscriptionHotelRepository.AnyAsync(x => x.StartDate <= DateNow &&
                                                                                x.EndDate > DateNow &&
                                                                                x.HotelId == hotelId);
            if (!isSubscribed)
            {
                return SignInResult.NotAllowed;
            }
        }

        return await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
    }
}
