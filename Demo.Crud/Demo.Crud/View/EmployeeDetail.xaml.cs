using Demo.Crud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Crud.View
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class EmployeeDetail : ContentPage
   {
      EmployeeModel _employee;

      public EmployeeDetail()
      {
         InitializeComponent();
         nameEntry.Focus();
      }

      public EmployeeDetail(EmployeeModel employee)
      {
         InitializeComponent();
         Title = "Edit Employee";
         _employee = employee;
         nameEntry.Text = employee.Name;
         addressEntry.Text = employee.Address;
         nameEntry.Focus();
      }

      private async void Button_Clicked(object sender, EventArgs e)
      {
         if (string.IsNullOrEmpty(nameEntry.Text) || string.IsNullOrEmpty(addressEntry.Text))
            await DisplayAlert("Invalid", "Blank or Whitespace value is Invalid!", "OK");
         else if (_employee != null)
            EditEmployee();
         else
            AddNewEmployee();
      }

      async void EditEmployee()
      {
         _employee.Name = nameEntry.Text;
         _employee.Address = addressEntry.Text;
         await App.MyDatabase.UpdateEmployee(_employee);
         await Navigation.PopAsync();
      }

      async void AddNewEmployee()
      {
         await App.MyDatabase.CreateEmployee(new Model.EmployeeModel()
         {
            Name = nameEntry.Text,
            Address = addressEntry.Text
         });
         await Navigation.PopAsync();
      }
   }
}