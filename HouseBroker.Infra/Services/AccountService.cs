using HouseBroker.Domain.Models;
using HouseBroker.Domain.Models.Identity;
using HouseBroker.Infra.Dtos;
using HouseBroker.Infra.Helpers;
using HouseBroker.Infra.Interface;
using HouseBroker.Infra.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Services
{
    public interface IAccountService
    {
        Task<string> Login(UserAuthenticationDtos user);


        Task<RegisterRoleDtos> RegisterRole(RegisterRoleDtos user);
          
        Task<UserAuthenticationDtos> RegisterHomeSeeker(UserAuthenticationDtos user);

        Task<UserAuthenticationDtos> RegisterHomeBroker(UserAuthenticationDtos user);
    }

    public class AccountService : IAccountService
    {
        private readonly IDbContextFactory<DbManagerContext> _DbContextFactory;
        private readonly IJwtTokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IDbContextFactory<DbManagerContext> DbContextFactory, IUnitOfWork unitOfWork, IJwtTokenService tokenService)
        {
            _DbContextFactory = DbContextFactory;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<string> Login(UserAuthenticationDtos user)
        {


            using (var dbContext = await _DbContextFactory.CreateDbContextAsync())
            {
                var pw = Security.Encrypt(user.Password);
                try
                {
                    var dbUser = await dbContext.Users.Where(u => !u.IsDeleted && (u.UserName == user.Username || u.Email == user.Email) && u.PasswordHash == pw).FirstOrDefaultAsync();
                    if (dbUser != null)
                    {
                        var roledata = await dbContext.UserRoles.Where(x => x.UserId == dbUser.Id).FirstOrDefaultAsync();
                        var role = await dbContext.Roles.Where(x => x.Id == roledata.RoleId).FirstOrDefaultAsync();

                        var token = await _tokenService.GenerateToken(dbUser.UserName, role.Description, dbUser.Id, roledata.RoleId);
                        return token;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return null;


        }


        public async Task<RegisterRoleDtos> RegisterRole(RegisterRoleDtos user)
        {


            using (var dbContext = await _DbContextFactory.CreateDbContextAsync())
            {
                try
                {
                    ApplicationRole vm = new ApplicationRole();
                    vm.Description = user.Description;
                    vm.RolePriority = user.RolePriority;
                    vm.Id = new Guid();

                    var data = await dbContext.Roles.Where(x => x.Description == vm.Description).FirstOrDefaultAsync();



                    if (data == null)
                    {
                        await dbContext.Roles.AddAsync(vm);

                        await dbContext.SaveChangesAsync();

                        return user;

                    }



                    throw new Exception("Roles Already Exist");

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return null;


        }


        public async Task<UserAuthenticationDtos> RegisterHomeBroker(UserAuthenticationDtos user)
        {
            using (var dbContext = await _DbContextFactory.CreateDbContextAsync())
            {
                var pw = Security.Encrypt(user.Password ?? "manager@123");
                try
                {
                    await _unitOfWork.BeginTransactionAsync();
                    ApplicationUser userdata = new ApplicationUser();
                    userdata.UserName = user.Username;
                    userdata.Id = new Guid();
                    userdata.IsActive = true;
                    userdata.PasswordHash = pw;
                    userdata.Email = user.Email;
                    var ruser = await dbContext.Users.AddAsync(userdata);
                    await dbContext.SaveChangesAsync();


                    var roledata = await dbContext.Roles.Where(x => x.Description == "Broker").FirstOrDefaultAsync();

                    if(roledata == null)
                    {
                        throw new Exception("Role not found");
                    }

                    ApplicationUserRole userRole = new ApplicationUserRole();
                    userRole.UserId = userdata.Id;
                    userRole.RoleId = roledata.Id;


                    await dbContext.UserRoles.AddAsync(userRole);

                    await dbContext.SaveChangesAsync();

                    await _unitOfWork.CommitTransactionAsync();
                    return user;

                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw new Exception(ex.Message);
                }
            }

            return null;
        }

        public async Task<UserAuthenticationDtos> RegisterHomeSeeker(UserAuthenticationDtos user)
        {
            using (var dbContext = await _DbContextFactory.CreateDbContextAsync())
            {
                var pw = Security.Encrypt(user.Password ?? "manager@123");
                try
                {
                    await _unitOfWork.BeginTransactionAsync();
                    ApplicationUser userdata = new ApplicationUser();
                    userdata.UserName = user.Username;
                    userdata.Id = new Guid();
                    userdata.IsActive = true;
                    userdata.PasswordHash = pw;
                    userdata.Email = user.Email;
                    var ruser = await dbContext.Users.AddAsync(userdata);
                    await dbContext.SaveChangesAsync();


                    var roledata = await dbContext.Roles.Where(x => x.Description == "Seaker").FirstOrDefaultAsync();

                    if (roledata == null)
                    {
                        throw new Exception("Role not found");
                    }

                    ApplicationUserRole userRole = new ApplicationUserRole();
                    userRole.UserId = userdata.Id;
                    userRole.RoleId = roledata.Id;


                    await dbContext.UserRoles.AddAsync(userRole);

                    await dbContext.SaveChangesAsync();

                    await _unitOfWork.CommitTransactionAsync();
                    return user;

                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw new Exception(ex.Message);
                }
            }

            return null;
        }
    }
}
