using bakery.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace bakery.Database
{
    public static class DatabaseControl
    {
        public static List<User> GetUsersForView()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.User.Include(p => p.RoleEntity).ToList();
            }
        }
        public static List<User> GetUsersForViewByFullName(string name)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.User.Include(p => p.RoleEntity).Where(p => EF.Functions.Like(p.FullName.ToLower(), $"%{name.ToLower()}%")).ToList();
            }
        }
        public static void AddUser(User user)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.User.Add(user);
                ctx.SaveChanges();
            }
        }
        public static void UpdateUser(User user)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                User _user = ctx.User.FirstOrDefault(p => p.Id == user.Id);

                if (_user == null)
                {
                    return;
                }

                _user.FullName = user.FullName;
                _user.Login = user.Login;
                _user.Password = user.Password;
                _user.RoleId = user.RoleId;

                ctx.SaveChanges();
            }
        }
        public static void RemoveUser(User user)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                if (user == null)
                {
                    return;
                }
                ctx.User.Remove(user);
                ctx.SaveChanges();
            }
        }

        public static List<Role> GetRolesForView()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Role.Include(p => p.UserEntities).ToList();
            }
        }
        public static void AddRole(Role role)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Role.Add(role);
                ctx.SaveChanges();
            }
        }
        public static void UpdateRole(Role role)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Role _role = ctx.Role.FirstOrDefault(p => p.Id == role.Id);

                if (_role == null)
                {
                    return;
                }

                _role.Title = role.Title;

                ctx.SaveChanges();
            }
        }
        public static void RemoveRole(Role role)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                if (role == null)
                {
                    return;
                }

                if (role.UserEntities.Count > 0)
                {
                    MessageBox.Show("Невозможно удалить роль, поскольку она используется одним или несколькими пользователями!");
                    return;
                }

                ctx.Role.Remove(role);
                ctx.SaveChanges();
                
            }
        }

    }
}
