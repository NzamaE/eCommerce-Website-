using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace e_PetService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Ie_PetService" in both code and config file together.
    [ServiceContract]
    public interface Ie_PetService
    {
        [OperationContract]
        List<Product> gettype();

        [OperationContract]
        RegUser Login(string email, string password);

        [OperationContract]
        RegUser getUser(int id);

        [OperationContract]
        bool editProfile(RegUser u);

        [OperationContract]
        Product getSingleProduct(int id);

        [OperationContract]
        bool Reguser(String name, string lname, string email, string phone, DateTime dob, string password, string Usertype);

        [OperationContract]
        bool checkIfRegistered(string email, string password);

        [OperationContract]
        List<Product> getAllProduct();

        [OperationContract]
        List<Product> getitemtype(string type);

        [OperationContract]
        bool Addtocart(int Uid, int Pid, int Pquantity, int active);

        [OperationContract]
        void Updatetocart(int Uid, int Pid, int Pquantity);

        [OperationContract]
        List<Cart> getCartProducts(int PId);

        [OperationContract]
        void removeProduct(int Active, int pID);

        [OperationContract]
        List<Product> getProductByUserID(int UserID);

        [OperationContract]
        List<Cart> getAllCartProduct();

        [OperationContract]
        bool checkOut(String name, string lname, string email, string address, string address1, string country, string province, int zip, string paymentType, string cardHolder, string cardNumber, DateTime expiration, int cvv);
    }
}
