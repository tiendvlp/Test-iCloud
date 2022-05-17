﻿using BusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        Member GetMemberLogin(String email, String password);
        IEnumerable<Member> GetMembersList();
        Member GetMemberById(int ID);
        Member GetMemberByEmail(String email);
        void Create(Member member);
        Member Update(Member member);
        void Delete(Member member);
    }
}
