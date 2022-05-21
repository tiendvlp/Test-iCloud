using BusinessObject.BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public Member GetMemberLogin(String email, String password)
        {
            Member loggedInMember = null;
            try
            {
                var fStoreDb = new FStoreDBContext();
                loggedInMember = fStoreDb.Members.SingleOrDefault(mem => mem.Email == email && mem.Password == password);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            return loggedInMember;
        }

        public bool IsAdminLogin(String email, String password)
        {
            var path = Directory.GetCurrentDirectory();
            IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(path)
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();
            String adminEmail = config["admin:email"];
            String adminPassword = config["admin:password"];
            if (email == adminEmail && password == adminPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Member> GetMembersList()
        {
            List<Member> members;
            try
            {
                var fStoreDb = new FStoreDBContext();
                members = fStoreDb.Members.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return members;
        }

        public IEnumerable<Member> GetMemberById(int ID)
        {
            IEnumerable<Member> mem = null;
            try
            {
                var fStoreDb = new FStoreDBContext();
                mem = fStoreDb.Members.Where(mem => mem.MemberId == ID).ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }

        public IEnumerable<Member> GetMemberByEmail(String email)
        {
            IEnumerable<Member> member = null;
            try
            {
                var fStoreDb = new FStoreDBContext();
                member = fStoreDb.Members.Where(mem => mem.Email == email).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        public void Create(Member member)
        {
            if (member == null 
                || member.Email.Length == 0 
                || member.CompanyName.Length == 0
                || member.City.Length == 0 
                || member.Country.Length == 0 
                || member.Password.Length == 0)
            {
                throw new Exception("All fields are required. Please input all, thank you!");
            }
            else
            {
                try
                {
                    var mem = GetMemberByEmail(member.Email);
                    if (mem.Count() == 0)
                    {
                        var fStoreDb = new FStoreDBContext();
                        fStoreDb.Members.Add(member);
                        fStoreDb.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("This member already existed!");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Member Update(Member member)
        {
            Member updatedMember = member;
            try
            {
                if (member == null
                    || member.Email.Length == 0
                    || member.CompanyName.Length == 0
                    || member.City.Length == 0
                    || member.Country.Length == 0
                    || member.Password.Length == 0)
                {
                    throw new Exception("All fields are required. Please input all, thank you!");
                }
                else
                {
                    try
                    {
                        if (GetMemberById(member.MemberId) != null)
                        {
                            var fStoreDb = new FStoreDBContext();
                            fStoreDb.Entry<Member>(member).State = EntityState.Modified;
                            fStoreDb.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("This member haven't existed yet!");
                        }
                    } 
                    catch(Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return updatedMember;
        }

        public void Delete(Member member)
        {
            try
            {
                if (GetMemberById(member.MemberId) != null)
                {
                    var fStoreDb = new FStoreDBContext();
                    fStoreDb.Members.Remove(member);
                    fStoreDb.SaveChanges();
                }
                else
                {
                    throw new Exception("This member haven't existed yet!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
