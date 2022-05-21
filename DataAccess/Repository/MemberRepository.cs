using BusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public IEnumerable<Member> GetMembersList() => MemberDAO.Instance.GetMembersList();

        public Member GetMemberLogin(String email, String password) => MemberDAO.Instance.GetMemberLogin(email, password);

        public IEnumerable<Member> GetMemberByEmail(string email) => MemberDAO.Instance.GetMemberByEmail(email);

        public IEnumerable<Member> GetMemberById(int ID) => MemberDAO.Instance.GetMemberById(ID);

        public void Create(Member member) => MemberDAO.Instance.Create(member);

        public Member Update(Member member) => MemberDAO.Instance.Update(member);

        public void Delete(Member member) => MemberDAO.Instance.Delete(member);

        public bool IsAdminLogin(string email, string password) => MemberDAO.Instance.IsAdminLogin(email, password);
    }
}
