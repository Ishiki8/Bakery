﻿using bakery.Objects;
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
        public static List<Order> GetOrdersForView()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Order.Include(p => p.CustomerEntity).ToList();
            }
        }
        public static List<Customer> GetCustomersForViewByName(string name)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Customer.Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{name.ToLower()}%")).ToList();
            }
        }
        public static void AddOrder(Order order)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Order.Add(order);
                ctx.SaveChanges();
            }
        }
        public static void UpdateOrder(Order order)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Order _order = ctx.Order.FirstOrDefault(p => p.Id == order.Id);

                if (_order == null)
                {
                    return;
                }

                _order.Date = order.Date;
                _order.Status = order.Status;
                _order.CustomerId = order.CustomerId;

                ctx.SaveChanges();
            }
        }
        public static void RemoveOrder(Order order)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                if (order == null)
                {
                    return;
                }
                ctx.Order.Remove(order);
                ctx.SaveChanges();
            }
        }
        public static List<Customer> GetCustomersForView()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Customer.Include(p => p.OrderEntities).ToList();
            }
        }
        public static void AddCustomer(Customer customer)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Customer.Add(customer);
                ctx.SaveChanges();
            }
        }
        public static void UpdateCustomer(Customer customer)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Customer _customer = ctx.Customer.FirstOrDefault(p => p.Id == customer.Id);

                if (_customer == null)
                {
                    return;
                }

                _customer.Name = customer.Name;
                _customer.Address = customer.Address;
                _customer.ITN = customer.ITN;
                _customer.PhoneNumber = customer.PhoneNumber;

                ctx.SaveChanges();
            }
        }
        public static void RemoveCustomer(Customer customer)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                if (customer == null)
                {
                    return;
                }

                if (customer.OrderEntities.Count > 0)
                {
                    MessageBox.Show("Невозможно удалить заказчика, поскольку содержатся одна или несколько записей о его заказах!");
                    return;
                }

                ctx.Customer.Remove(customer);
                ctx.SaveChanges();
            }
        }

    }
}
