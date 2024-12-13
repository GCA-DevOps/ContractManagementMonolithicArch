using ContractManagementSystem.Data;
using Microsoft.AspNetCore.Identity;

namespace ContractManagementSystem.Models
{
    public class GetApproversService
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public GetApproversService(ApplicationDbContext db, UserManager<AppUser> userManager)
        {
            this._context = db;
            this._userManager = userManager;
        }



        public (string, string, string) GetApprovers()
        {
            var roleid1 = "cdf427e8-610e-44f6-b4ba-cf800bde8f6b";//admin
            var roleid2 = "cdf427e8-610e-44f6-b4ba-cf800bde8f6b";//riskofficer
            var roleid3 = "cdf427e8-610e-44f6-b4ba-cf800bde8f6b";//contractmanager

            var approverids = _context.UserRoles.Where(x => x.RoleId == roleid1).Select(u => u.UserId).Take(50).ToList(); // return 50 user ids

            var random = new Random();


            var approverUserIds = approverids.OrderBy(x => random.Next()).ToList();//randomize userids from the list

            var approver1UserId = approverUserIds.ElementAtOrDefault(0) ?? "";//approver1

            //get next approver
            var approverids2 = _context.UserRoles.Where(x => x.RoleId == roleid2).Select(u => u.UserId).ToList();




            approverids2 = approverids2.OrderBy(c => random.Next()).ToList();//randomize


            var approver2UserId = approverUserIds.ElementAtOrDefault(1) ?? "";//aprrover2


            //get approver3

            var approverids3 = _context.UserRoles.Where(x => x.RoleId == roleid3).Select(u => u.UserId).ToList();




            approverids3 = approverids3.OrderBy(c => random.Next()).ToList();//randomize




            var approver3UserId = approverUserIds.ElementAtOrDefault(2) ?? "";

            return (approver1UserId, approver2UserId, approver3UserId);
        }

    }

}
