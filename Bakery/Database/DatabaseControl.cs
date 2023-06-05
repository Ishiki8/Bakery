using bakery.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace bakery.Database
{
    public static class DatabaseControl
    {
        public static List<User> GetUsersForView(string name = "")
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

        public static List<Order> GetOrdersForView(string name = "")
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Order.Include(p => p.CustomerEntity).Include(p => p.OrderedProductEntities)
                                .Where(p => EF.Functions.Like(p.CustomerEntity.Name.ToLower(), $"%{name.ToLower()}%")).ToList();
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

        public static List<Customer> GetCustomersForView(string name = "")
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Customer.Include(p => p.OrderEntities).Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{name.ToLower()}%")).ToList();
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

        public static List<Product> GetProductsForView(string title = "")
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Product.Include(p => p.OrderedProductEntities).Where(p => EF.Functions.Like(p.Title.ToLower(), $"%{title.ToLower()}%")).ToList();
            }
        }

        public static void AddProduct(Product product)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Product.Add(product);
                ctx.SaveChanges();
            }
        }

        public static void UpdateProduct(Product product)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Product _product = ctx.Product.FirstOrDefault(p => p.Id == product.Id);

                if (_product == null)
                {
                    return;
                }

                _product.Title = product.Title;
                _product.Weight = product.Weight;
                _product.Price = product.Price;

                ctx.SaveChanges();
            }
        }

        public static void RemoveProduct(Product product)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                if (product == null)
                {
                    return;
                }

                if (product.OrderedProductEntities.Count > 0)
                {
                    MessageBox.Show("Невозможно удалить продукцию, поскольку она используется в одном или нескольких заказах!");
                    return;
                }

                ctx.Product.Remove(product);
                ctx.SaveChanges();
            }
        }

        public static List<Ordered_Product> GetOrderedProductsForView()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Ordered_Product.Include(p => p.OrderEntity).Include(p => p.ProductEntity).ToList();
            }
        }

        public static void AddProductToOrder(Ordered_Product ordered_product)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Ordered_Product.Add(ordered_product);
                ctx.SaveChanges();
            }
        }

        public static void RemoveProductFromOrder(Ordered_Product ordered_product)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                if (ordered_product == null)
                {
                    return;
                }

                ctx.Ordered_Product.Remove(ordered_product);
                ctx.SaveChanges();
            }
        }

        public static List<Supply> GetSuppliesForView(string name = "")
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Supply.Include(p => p.ProviderEntity).Include(p => p.SuppliedRawEntities)
                                 .Where(p => EF.Functions.Like(p.ProviderEntity.Name.ToLower(), $"%{name.ToLower()}%")).ToList();
            }
        }

        public static void AddSupply(Supply supply)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Supply.Add(supply);
                ctx.SaveChanges();
            }
        }

        public static void UpdateSupply(Supply supply)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Supply _supply = ctx.Supply.FirstOrDefault(p => p.Id == supply.Id);

                if (_supply == null)
                {
                    return;
                }

                _supply.Date = supply.Date;
                _supply.Status = supply.Status;
                _supply.ProviderId = supply.ProviderId;

                ctx.SaveChanges();
            }
        }

        public static void RemoveSupply(Supply supply)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                if (supply == null)
                {
                    return;
                }

                ctx.Supply.Remove(supply);
                ctx.SaveChanges();
            }
        }

        public static List<Provider> GetProvidersForView(string name = "")
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Provider.Include(p => p.SupplyEntities).Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{name.ToLower()}%")).ToList();
            }
        }

        public static void AddProvider(Provider provider)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Provider.Add(provider);
                ctx.SaveChanges();
            }
        }

        public static void UpdateProvider(Provider provider)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Provider _provider = ctx.Provider.FirstOrDefault(p => p.Id == provider.Id);

                if (_provider == null)
                {
                    return;
                }

                _provider.Name = provider.Name;
                _provider.Address = provider.Address;
                _provider.ITN = provider.ITN;
                _provider.PhoneNumber = provider.PhoneNumber;

                ctx.SaveChanges();
            }
        }

        public static void RemoveProvider(Provider provider)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                if (provider == null)
                {
                    return;
                }

                if (provider.SupplyEntities.Count > 0)
                {
                    MessageBox.Show("Невозможно удалить поставщика, поскольку содержатся одна или несколько записей о его поставках!");
                    return;
                }

                ctx.Provider.Remove(provider);
                ctx.SaveChanges();
            }
        }

        public static List<Raw> GetRawForView(string title = "")
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Raw.Include(p => p.SuppliedRawEntities).Where(p => EF.Functions.Like(p.Title.ToLower(), $"%{title.ToLower()}%")).ToList();
            }
        }

        public static void AddRaw(Raw raw)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Raw.Add(raw);
                ctx.SaveChanges();
            }
        }

        public static void UpdateRaw(Raw raw)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Raw _raw = ctx.Raw.FirstOrDefault(p => p.Id == raw.Id);

                if (_raw == null)
                {
                    return;
                }

                _raw.Title = raw.Title;
                _raw.Weight = raw.Weight;
                _raw.Price = raw.Price;
                _raw.InStock = raw.InStock;

                ctx.SaveChanges();
            }
        }

        public static void RemoveRaw(Raw raw)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                if (raw == null)
                {
                    return;
                }

                if (raw.SuppliedRawEntities.Count > 0)
                {
                    MessageBox.Show("Невозможно удалить сырье, поскольку оно используется в одной или нескольких поставках!");
                    return;
                }

                ctx.Raw.Remove(raw);
                ctx.SaveChanges();
            }
        }

        public static List<Supplied_Raw> GetSuppliedRawForView()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Supplied_Raw.Include(p => p.SupplyEntity).Include(p => p.RawEntity).ToList();
            }
        }

        public static void AddRawToSupply(Supplied_Raw supplied_raw)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Supplied_Raw.Add(supplied_raw);
                ctx.SaveChanges();
            }
        }

        public static void RemoveRawFromSupply(Supplied_Raw supplied_raw)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                if (supplied_raw == null)
                {
                    return;
                }

                ctx.Supplied_Raw.Remove(supplied_raw);
                ctx.SaveChanges();
            }
        }
    }
}
