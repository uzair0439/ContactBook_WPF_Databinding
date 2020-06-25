using Microsoft.Win32;
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
using System.Windows.Shapes;


///Made By Uzair Ahmad (FA17-BSE-142) .. EzioPC
///Used Two Window. Login and Main.. I switch windows instead of using visibility.
///Had two Backend Coding Files.
///user 1 username = "uzair" and pass = "123"
///user 2 username = "uzair2" and pass = "1234"
///Update can only be performed when edit is pressed
///Picture can be chosen from anywhere in the computer
///EzioOut


namespace Assignment2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        //List to show in listbox. The contact information is coming from mainwindow.StudentList

        public static List<MainWindow.Address> showList = new List<MainWindow.Address>();

        //List used to search (Filter) the object in the listbox 

        public static MainWindow.Student std = new MainWindow.Student();

        string filename = "";  //used to get path of image uploaded through show open dialoh
        public Window1()
        {

            InitializeComponent();
            this.listContacts.ItemsSource = showList;
        }


        //Add Button Method (Adding a new Contact to the Listbox

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {

            if (fn_txt.Text.Equals("") || ln_txt.Text.Equals("") || phone_txt.Text.Equals("") || company_txt.Text.Equals("")
                || job_txt.Text.Equals("") || email_txt.Text.Equals("") || address_txt.Text.Equals(""))

            {
                MessageBox.Show("Please Fill all the Fields");
                return;
            }

            else
            {
                if (search_box.Text.Equals(""))
                {
                    showList.Add(new MainWindow.Address(fn_txt.Text, ln_txt.Text, phone_txt.Text, company_txt.Text,
                   job_txt.Text, email_txt.Text, address_txt.Text, filename));

                    this.listContacts.Items.Refresh();

                    choose_btn.Content = "Choose";

                    update_btn.IsEnabled = false;

                    filename = "";
                }

                else
                {
                    MessageBox.Show("Remove search Filter First");
                }

            }
        }



        // Clear method to wipe out TextFields

        private void clear_btn_Click(object sender, RoutedEventArgs e)
        {
            fn_txt.Clear();
            ln_txt.Clear();
            phone_txt.Clear();
            company_txt.Clear();
            job_txt.Clear();
            email_txt.Clear();
            address_txt.Clear();
            update_btn.IsEnabled = false;
            this.listContacts.UnselectAll();

        }

        //ChooseButton method to pick an image path from anywhere in the Computer 

        private void choose_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = true;
            file.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.png) | *.jpg; *.jpeg; *.gif; *.png;";
            Nullable<bool> okDialog = file.ShowDialog();

            if (okDialog == true)
            {
                filename = file.FileName;
                choose_btn.Content = "Done";
            }
        }

        //Logout button method to go to  login page again

        private void logout_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow first = new MainWindow();
            first.Owner = this;
            this.Hide();
            first.ShowDialog();
            return;
        }

        //Delete button method to delete a selected item from the listbox

        private void del_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to Delete the Contact", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                showList.RemoveAt(this.listContacts.SelectedIndex);
                this.listContacts.Items.Refresh();

                update_btn.IsEnabled = false;
            }

        }

        //Update button method for updating any field in the selected contact
        //Update button is enabled only when Edit is triggered

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {

            if (listContacts.SelectedIndex < 0)
            {
                MessageBox.Show("Select a Contact to Be updated");
            }

            else
            {
                showList.RemoveAt(this.listContacts.SelectedIndex);

                showList.Insert(this.listContacts.SelectedIndex, new MainWindow.Address(fn_txt.Text, ln_txt.Text, phone_txt.Text, company_txt.Text,
                    job_txt.Text, email_txt.Text, address_txt.Text, filename));

                this.listContacts.Items.Refresh();

                MessageBox.Show("Contact Updated", "Update");

                update_btn.IsEnabled = false;

                choose_btn.Content = "Choose";

                filename = "";
            }


        }



        //Edit Button to Enable the Update button for the selected contact.

        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            update_btn.IsEnabled = true;
        }


        //Search box method.. When text is changed in the search box then it is triggered(text_Changed_event)
        //Search all the Contacts in the list by its first name using an indexer created in the student class and the FindAll method in it
        //using predicate

        private void search_box_changed_text(object sender, TextChangedEventArgs e)
        {

            if (search_box.Text.Equals(""))  //if there is nothing in the search box then show the whole list;
            {
                listContacts.ItemsSource = showList;
                listContacts.Items.Refresh();
            }

            else  //if there is something in the search box then show only those contacts of text written in the Box
            {
                List<MainWindow.Address> newList = std[search_box.Text.ToLower(), true, 1];
                listContacts.ItemsSource = newList;
                listContacts.Items.Refresh();
            }

        }

        private void ListContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

