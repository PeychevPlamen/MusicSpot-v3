namespace MusicSpot_v3.Areas.Admin.Controller
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using static MusicSpot_v3.Infrastructure.Data.AdminConstants;
  

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public abstract class AdminController : Controller
    {

    }
}
