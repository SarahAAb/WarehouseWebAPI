using Microsoft.AspNetCore.Identity;
using WarehouseSystemWebAPI.Models;

namespace WarehouseSystemWebAPI.Services
{
    public class AccountService:IAccountService
    {
         UserManager<ApplicationUsers> userManager;
         SignInManager<ApplicationUsers> signInManager;
         RoleManager<IdentityRole> roleManager;

        public AccountService(UserManager<ApplicationUsers> _userManager,SignInManager<ApplicationUsers> _signInManager,RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }
        public async Task<IdentityResult> CreateAccount(SignUp signUp)
        {
            ApplicationUsers users = new ApplicationUsers()
            {
                FullName= signUp.FullName,
                UserName=signUp.Email,
                Active=true,
                Warehouse_Id=signUp.Warehouse_Id

            };
            var result=await userManager.CreateAsync(users,signUp.Password);
            return result;
        }
        public async Task<SignInResult> SignIn(SignIn signIn)
        {
            var result = await signInManager.PasswordSignInAsync(signIn.Email, signIn.Password, signIn.remmebrme, false);
            return result;
        }
        public async Task LogOut()
        {
            await signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> InsertRole(Role role)
        {
            IdentityRole roleIdentity = new IdentityRole()
            {
                Name = role.Name,
            };
       var   result= await roleManager.CreateAsync(roleIdentity);
            return result;

        }
        public List<Role> RoleList()
        {
            List<IdentityRole> roles= roleManager.Roles.ToList();
            List<Role> roles1=new List<Role>();
            foreach (var role in roles)
            {
                roles1.Add(new Role()
                {
                   
                    Name = role.Name


                });
            }
            return roles1;
        }
        public List<UsersDTO> UsersList()
        {
            List<ApplicationUsers> users= userManager.Users.ToList();
            List<UsersDTO> usersDTOs= new List<UsersDTO>();
            foreach(var item in users)
            {
                usersDTOs.Add(new UsersDTO()
                {
                    FullName = item.FullName,
                    Email = item.UserName,
                    Active = item.Active,
                    UserId = item.Id,
                    Warehouse_Id = item.Warehouse_Id
                });
            }
            return usersDTOs;
        }
        public async Task<List<UserRoles>> UserRole(string userId) {
            List<IdentityRole> roles = roleManager.Roles.ToList();
            List<UserRoles> userRoles= new List<UserRoles>();
            foreach (var role in roles)
            {
                userRoles.Add(new UserRoles()
                {
                    UserId = userId,
                    RoleId= role.Id,
                    RoleName= role.Name,
                    IsSelected=false
                });
                    
                
            }
            foreach(var item in userRoles)
            {
                var user = await userManager.FindByIdAsync(userId);
                var uroles = await userManager.GetRolesAsync(user);
                foreach(var urole in uroles)
                {
                    if (item.RoleName == urole)
                    {
                        item.IsSelected= true;
                    }
                }
               
            }
            return userRoles;
        }
        public async Task UpdateRoles(List<UserRoles> userRoles)
        {
            
            foreach (var item in userRoles) 
            {
                var user = await userManager.FindByIdAsync(item.UserId);
                if (item.IsSelected)
                {
                    if (await userManager.IsInRoleAsync(user, item.RoleName) == false)
                    {
                        await userManager.AddToRoleAsync(user, item.RoleName);
                    }

                }
                else
                {
                    if (await userManager.IsInRoleAsync(user, item.RoleName))
                    {
                        await userManager.RemoveFromRoleAsync(user, item.RoleName);
                    }
                }
            }
        }
        public async Task Activate(string userId)
        {
            var user= await userManager.FindByIdAsync(userId);
            user.Active = !(user.Active);
            await userManager.UpdateAsync(user);
            
         }
       public async Task<ApplicationUsers> GetUserInfo(string name)
        {
            var result = await userManager.FindByNameAsync(name);
            return result;
        }
        public List<string> GetRoles(ApplicationUsers user)
        {
            var result =  userManager.GetRolesAsync(user).Result.ToList();
            return result;
        }

    }
}
