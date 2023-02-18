using BusinessObject.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for MemberManagement.xaml
    /// </summary>
    public partial class MemberManagement : Window
    {
        private readonly IMemberRepository memberRepository;

        private List<Member> memberList = new List<Member>();

        public MemberManagement(IMemberRepository _memberRepository)
        {
            InitializeComponent();
            memberRepository = _memberRepository;
            this.LoadMemberList();
        }

        /// <summary>
        /// This method is used to load the member list
        /// </summary>
        private void LoadMemberList()
        {
            this.memberList = memberRepository.GetAllMembers();
            this.UpdateGridView();
        }

        /// <summary>
        /// This method is used to update the grid view
        /// </summary>
        private void UpdateGridView()
        {
            this.lvMembers.ItemsSource = null;
            this.lvMembers.ItemsSource = this.memberList;
        }

        /// <summary>
        /// This method is used to get member object
        /// </summary>
        private Member GetMemberObject()
        {
            Member member = null;
            try
            {
                member = new Member
                {
                    MemberId = String.IsNullOrEmpty(txtMemberId.Text)
                        ? 0
                        : int.Parse(txtMemberId.Text),
                    Email = txtEmail.Text,
                    CompanyName = txtCompany.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get member");
            }
            return member;
        }

        /// <summary>
        /// This method is used to validate the member object
        /// </summary>
        private bool ValidateMemberObject()
        {
            if (
                String.IsNullOrEmpty(txtEmail.Text)
                || String.IsNullOrEmpty(txtCompany.Text)
                || String.IsNullOrEmpty(txtCity.Text)
                || String.IsNullOrEmpty(txtCountry.Text)
                || String.IsNullOrEmpty(txtPassword.Text)
            )
            {
                MessageBox.Show("Please fill all information");
                return false;
            }
            return true;
        }

        /// <summary>
        /// This method is used to click the load button
        /// </summary>
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadMemberList();
        }

        /// <summary>
        /// This method is used to click insert member button
        /// </summary>
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateMemberObject())
                {
                    Member member = GetMemberObject();
                    member.MemberId = 0;
                    memberRepository.CreateMember(member);
                    this.memberList.Add(member);
                    this.UpdateGridView();
                    MessageBox.Show($"{member.Email} insert succesfully", "Insert Member");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert Member");
            }
        }

        /// <summary>
        /// This method is used to update the member
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateMemberObject())
                {
                    Member member = GetMemberObject();
                    memberRepository.UpdateMember(member);
                    this.memberList[this.memberList.FindIndex(x => x.MemberId == member.MemberId)] =
                        member;
                    this.UpdateGridView();
                    MessageBox.Show($"{member.Email} update succesfully", "Update Member");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Member");
            }
        }

        /// <summary>
        /// This method is used to delete the member
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var memberId = txtMemberId.Text;
                if (String.IsNullOrEmpty(memberId))
                {
                    MessageBox.Show("Please choose a member to delete");
                }
                else
                {
                    var Result = MessageBox.Show(
                        "Are you sure to delete?",
                        "Confirm",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question
                    );
                    if (Result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var member = GetMemberObject();
                            memberRepository.DeleteMember(member);
                            this.memberList.Remove(
                                this.memberList.FirstOrDefault(x => x.MemberId == member.MemberId)
                            );
                            this.UpdateGridView();
                            MessageBox.Show($"{member.Email} delete succesfully", "Delete Member");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Delete Member");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Member");
            }
        }

        /// <summary>
        /// This method is called when the window is loaded and activated
        /// <summary>
        public Task ActivateAsync(object parameter)
        {
            var a = parameter;
            return Task.CompletedTask;
        }

        /// <summary>
        /// This method is search the member by email
        /// </summary>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var email = txtSearch.Text;
            if (String.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter email, id, company name, ...");
            }
            else
            {
                try
                {
                    var member = memberRepository.SearchMembersByName(email.ToLower());
                    if (member != null)
                    {
                        this.memberList = member;
                        this.UpdateGridView();
                    }
                    else
                    {
                        MessageBox.Show("No member found");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search Member");
                }
            }
        }

        /// <summary>
        /// Back to the main menu
        /// </summary>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
