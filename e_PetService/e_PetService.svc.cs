using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace e_PetService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "e_PetService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select e_PetService.svc or e_PetService.svc.cs at the Solution Explorer and start debugging.
    public class e_PetService : Ie_PetService
    {
        e_PetServiceDataContext db = new e_PetServiceDataContext();
        public bool checkIfRegistered(string email, string password)
        {

            var theBooking = (from b in db.RegUsers
                              where b.Email.Equals(email) && b.Password.Equals(password)
                              select b).FirstOrDefault();

            if (theBooking == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool Addtocart(int Uid, int Pid, int Pquantity, int active)
        {
            var newCart = new Cart
            {
                Quantity = Pquantity,
                Active = active,
                UserID = Uid,
                ProdId = Pid,

            };
            db.Carts.InsertOnSubmit(newCart);
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return false;
            }
        }

        public List<Product> getAllProduct()
        {
            List<Product> listofprod = new List<Product>();

            dynamic getprod = (from p in db.Products
                               where p.Active.Equals(1)
                               select p).DefaultIfEmpty();
            foreach (Product p in getprod)
            {
                listofprod.Add(p);
            }
            return listofprod;

        }

        public List<Cart> getAllCartProduct()
        {
            List<Cart> listofprod = new List<Cart>();

            dynamic getcartp = (from c in db.Carts
                                where c.Active.Equals(1)
                                select c).DefaultIfEmpty();

            foreach (Cart c in getcartp)
            {
                listofprod.Add(c);
            }
            return listofprod;
        }

        public List<Product> getitemtype(string type)
        {
            List<Product> listofprod = new List<Product>();

            dynamic getprod = (from p in db.Products
                               where p.Active.Equals(1) && p.Catergory.Equals(type)
                               select p).DefaultIfEmpty();
            foreach (Product p in getprod)
            {
                listofprod.Add(p);
            }
            return listofprod;
        }

        public Product getSingleProduct(int id)
        {
            var product = (from u in db.Products
                           where u.Id.Equals(id)
                           select u).FirstOrDefault();
            if (product != null)
            {
                return product;
            }
            else
            {
                return null;
            }
        }

        public bool Reguser(string name, string lname, string email, string phone, DateTime dob, string password, string Usertype)
        {
            var User = new RegUser
            {
                Name = name,
                Surname = lname,
                Email = email,
                Password = password,
                DoB = Convert.ToDateTime(dob),
                Phone = phone,
                UserType = Usertype,
            };
            db.RegUsers.InsertOnSubmit(User);
            try
            {
                db.SubmitChanges();
                return true;

            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return false;

            }
        }

        RegUser Ie_PetService.Login(string email, string password)
        {
            var log = (from p in db.RegUsers
                       where p.Email.Equals(email) &&
                       p.Password.Equals(password)
                       select p).FirstOrDefault();
            if (log != null)
            {
                return log;
            }
            else
            {
                return null;
            }
        }

        public void removeProduct(int Active, int pID)
        {
            var productInCart = (from p in db.Carts
                                 where p.ProdId.Equals(pID)
                                 select p).FirstOrDefault();

            productInCart.Active = Active;
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                e.GetBaseException();
            }
        }

        public void Updatetocart(int Uid, int Pid, int Pquantity)
        {
            dynamic userCart = (from c in db.Carts
                                where c.UserID.Equals(Uid) && c.ProdId.Equals(Pid)
                                select c).FirstOrDefault();

            if (userCart != null)
            {
                userCart.Quantity = Pquantity;

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    e.GetBaseException();
                }
            }
        }

        public List<Product> getProductByUserID(int UserID)
        {
            List<Product> products = new List<Product>();
            List<Cart> cartProd = new List<Cart>();

            cartProd = getCartProducts(UserID);
            if (cartProd != null)
            {
                foreach (Cart c in cartProd)
                {
                    var product = (from u in db.Products
                                   where u.Id.Equals(c.ProdId)
                                   select u).FirstOrDefault();
                    products.Add(product);

                }
                return products;
            }
            else
            {
                return null;
            }
        }

        public List<Cart> getCartProducts(int PId)
        {
            List<Cart> cartProducts = new List<Cart>();

            dynamic cart = (from c in db.Carts
                            where c.ProdId.Equals(PId)
                            select c);

            if (cart != null)
            {
                cartProducts = cart.ToList();
            }
            else
            {
                return null;
            }
            return cartProducts;
        }

        public bool checkOut(string name, string lname, string email, string address, string address1, string country, string province, int zip, string paymentType, string cardHolder, string cardNumber, DateTime expiration, int cvv)
        {
            var checkout = new CheckOut
            {
                Name = name,
                lastname = lname,
                Username = email,
                Address = address,
                Address1 = address1,
                Country = country,
                Province = province,
                Zip = zip,
                Payment = paymentType,
                NameOnCard = cardHolder,
                CreditCardNumber = cardNumber,
                Expiration = expiration,
                CVV = cvv
            };
            db.CheckOuts.InsertOnSubmit(checkout);
            try
            {
                db.SubmitChanges();
                return true;

            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return false;
            }
        }

        public RegUser getUser(int id)
        {
            var user = (from u in db.RegUsers
                        where u.Id.Equals(id)
                        select u).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public bool editProfile(RegUser u)
        {
            var user = (from x in db.RegUsers
                        where x.Id == u.Id
                        select x).FirstOrDefault();
            user.Name = u.Name;
            user.Surname = u.Surname;
            user.Email = u.Email;
            user.Phone = u.Phone;
            user.DoB = u.DoB;
            user.Password = u.Password;

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (IndexOutOfRangeException ex)
            {
                ex.GetBaseException();
                return false;
            }
        }

        public List<Product> gettype()
        {
            List<Product> listofprod = new List<Product>();

            dynamic getprod = (from p in db.Products
                               where p.Active.Equals(1)
                               select p).DefaultIfEmpty();
            foreach (Product p in getprod)
            {
                var getl = new Product
                {
                    Name = p.Name,
                    Catergory = p.Catergory,
                    Price = p.Price,
                    Image = p.Image,
                    Description = p.Description
                };
                listofprod.Add(getl);
            }
            return listofprod;
        }
    }
}
