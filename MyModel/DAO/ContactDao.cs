using MyModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel.DAO
{
    public class ContactDao
    {
        OnlineShopDbContext db = null;
        public ContactDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<Contact> ListAll(bool onlyActive = false)
        {
            List<Contact> listContact = db.Contacts.ToList();
            if (onlyActive) { listContact = listContact.Where(x => x.Status == true).ToList(); }
            return listContact;
        }
        public long Insert(Contact item)
        {
            try
            {
                db.Contacts.Add(item);
                db.SaveChanges();
                return item.ID;
            }
            catch (Exception)
            {

                return -1;
            }
        }
        public bool Update(Contact item)
        {
            try
            {
                Contact contact = db.Contacts.Find(item.ID);
                contact.Content = item.Content;
                contact.Status = item.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                Contact contact = db.Contacts.Find(id);
                db.Contacts.Remove(contact);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
