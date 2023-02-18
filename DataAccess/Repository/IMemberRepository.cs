using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    /// <summary>
    /// This interface represents the contract to be implemented by MemberRepository.
    /// @Author: ANHDVHE151359
    /// </summary>
    public interface IMemberRepository
    {
        /// <summary>
        /// Gets a list of all members.
        /// </summary>
        /// <returns>A list of all members.</returns>
        List<Member> GetAllMembers();

        /// <summary>
        /// Gets a member by ID.
        /// </summary>
        /// <param name="id">The ID of the member.</param>
        /// <returns>The member with the given ID.</returns>
        Member GetMemberById(int id);

        /// <summary>
        /// Searches for members by name.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <returns>A list of members with the given name.</returns>
        List<Member> SearchMembersByName(string name);

        /// <summary>
        /// Gets a member by email and password.
        /// </summary>
        /// <param name="email">The email of the member.</param>
        /// <param name="password">The password of the member.</param>
        /// <returns>The member with the given email and password.</returns>
        Member GetMemberByEmailAndPassword(string email, string password);

        /// <summary>
        /// Creates a new member.
        /// </summary>
        /// <param name="member">The member to create.</param>
        /// <returns>The created member.</returns>
        Member CreateMember(Member member);

        /// <summary>
        /// Updates a member.
        /// </summary>
        /// <param name="member">The member to update.</param>
        /// <returns>The updated member.</returns>
        Member UpdateMember(Member member);

        /// <summary>
        /// Deletes a member.
        /// </summary>
        /// <param name="member">The member to delete.</param>
        void DeleteMember(Member member);

    }
}
