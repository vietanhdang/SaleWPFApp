using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        /// <summary>
        /// Gets a list of all members.
        /// </summary>
        /// <returns>A list of all members.</returns>
        /// @author: ANHDVHE151359
        public List<Member> GetAllMembers()
        {
            using (var context = new Sale_ManagementContext())
            {
                return context.Members.ToList();
            }
        }

        /// <summary>
        /// Creates a new member.
        /// </summary>
        /// <param name="member">The member to create.</param>
        /// <returns>The created member.</returns>
        /// @author: ANHDVHE151359
        public Member CreateMember(Member member)
        {
            try
            {
                using (var context = new Sale_ManagementContext())
                {
                    context.Members.Add(member);
                    context.SaveChanges();
                    return member;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates a member.
        /// </summary>
        /// <param name="member">The member to update.</param>
        /// <returns>The updated member.</returns>
        /// @author: ANHDVHE151359
        public Member UpdateMember(Member member)
        {
            try
            {
                using (var context = new Sale_ManagementContext())
                {
                    context.Members.Update(member);
                    context.SaveChanges();
                    return member;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets a member by email and password.
        /// </summary>
        /// <param name="email">The email of the member.</param>
        /// <param name="password">The password of the member.</param>
        /// <returns>The member with the given email and password.</returns>
        /// @author: ANHDVHE151359
        public Member GetMemberByEmailAndPassword(string email, string password)
        {
            using (var context = new Sale_ManagementContext())
            {
                return context.Members.SingleOrDefault(
                    member => member.Email == email && member.Password == password
                );
            }
        }

        /// <summary>
        /// Gets a member by ID.
        /// </summary>
        /// <param name="id">The ID of the member.</param>
        /// <returns>The member with the given ID.</returns>
        /// @author: ANHDVHE151359
        public Member GetMemberById(int id)
        {
            using (var context = new Sale_ManagementContext())
            {
                return context.Members.SingleOrDefault(member => member.MemberId == id);
            }
        }

        /// <summary>
        /// Deletes a member.
        /// </summary>
        /// <param name="member">The member to delete.</param>
        /// <returns>The deleted member.</returns>
        public void DeleteMember(Member member)
        {
            try
            {
                using (var context = new Sale_ManagementContext())
                {
                    context.Members.Remove(member);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Searches for members by name.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <returns>A list of members with the given name.</returns>
        public List<Member> SearchMembersByName(string name)
        {
            using (var context = new Sale_ManagementContext())
            {
                return context.Members.Where(member => member.Email.ToLower().Contains(name) 
                || member.CompanyName.ToLower().Contains(name) 
                || member.City.ToLower().Contains(name)
                || member.Country.ToLower().Contains(name)
                || member.MemberId.ToString().Contains(name)).ToList();
            }
        }
    }
}
