using BaMinimalTemplate.Data;
using BaMinimalTemplate.Extensions;
using BaMinimalTemplate.Models;
using Microsoft.EntityFrameworkCore;

namespace BaMinimalTemplate.Repositories;

public class UserTypeRepository : GenericRepository<UserType>, IUserTypeRepository
{
    public UserTypeRepository(ApplicationDbContext context) : base(context) { }



}