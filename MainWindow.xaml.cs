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

namespace Assignment2
{
    ///Made By Uzair Ahmad (FA17-BSE-142) .. EzioPC
    ///Used Two Window. Login and Main.. I switch windows instead of using visibility.
    ///Had two Backend Coding Files.
    ///user 1 username = "uzair" and pass = "123"
    ///user 2 username = "uzair2" and pass = "1234"
    ///Update can only be performed when edit is pressed
    ///Picture can be chosen from anywhere in the computer
    ///EzioOut
    public partial class MainWindow : Window
    {

        //Dummy List through which i initialized two students and add some contact information in it

        public static List<Student> studentList = new List<Student>();

        public MainWindow()
        {
            InitializeComponent();


            //Two students initialization one has 3 contacts and other has 2


            studentList.Add(new Student() {
                username = "uzair",
                pass = "123",
                addressList = new List<Address>()
                {
                    new Address("Uzair", "Ahmad", "+92-33319998072", "Comsats", "Student", "Uzair@gmail.com", "Islamabad", "image/DSC_0244copy.jpg"),
                    new Address("Messi", "Lionel", "+001-3319998072", "BArcelona", "Footballer", "John0439@gmail.com", "Spain", "image/messi.jpg"),
                     new Address("Zulkifle", "Maroof", "+92-33317299881", "PindiBoy", "Student", "zulfi@gmail.com", "Rawalpindi", "image/zulfi.jpg"),
                }
                
            });

            studentList.Add(new Student()
            {
                username = "uzair2",
                pass = "1234",
                addressList = new List<Address>()
                {
                    new Address("Ezio", "Firenze", "+92-33319998072", "CUI", "Footballer", "Ezio@gmail.com", "France", "image/messi.jpg"),
                    new Address("John", "Snow", "033319998072", "Microsoft", "Engineer", "John0439@gmail.com", "United States", "image/DSC_0244copy.jpg"),
                }

            });
        }

        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {


            foreach (var item in studentList)
            {
                if (txt1.Text.Equals("") || (txt2.Password.Equals("")))
                {
                    //in case if username and password fields are empty

                    MessageBox.Show("Enter Username and Password");
                    return;
                }

                else if (txt1.Text.Equals(item.username) && (txt2.Password.Equals(item.pass)))
                {
                    //if login successful it will switch the window to the main window

                    Window1.std = item;  //the Student object passing to the next window for further use
                    Window1.showList = item.addressList;   //The Contact information is passing through to the listbox in the next window

                    Window1 second = new Window1();
                    second.Owner = this;
                    this.Hide();
                    second.ShowDialog();                 
                    return;
                }

            }
        }


        public class Student  //Student class
        {
            public string username { get; set; }
            public string pass { get; set; }

            //List of address books having contacts information

            public List<Address> addressList = new List<Address>(); 


            //Indexer to filter the listBox by only the First name (Used the FindAll method (E.g Find all contacts starting from U))

            public List<Address> this[string nameSearch,bool check, int i]
            {
                get
                {
                    return this.addressList.FindAll(x => x.FirstName.ToLower().Contains(nameSearch));
                }
            }
          
        }

        public class Address  //ADDRESS bOOK cLASS
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string phone { get; set; }
            public string company { get; set; }
            public string jobtitle { get; set; }
            public string email { get; set; }
            public string address { get; set; }

            public string imagePath { get; set; }

            public Address(string fn, string ln, string phn, string com, string job, string em, string add, string img)
            {
                this.FirstName = fn;
                this.LastName = ln;
                this.phone = phn;
                this.company = com;
                this.jobtitle = job;
                this.email = em;
                this.address = add;
                this.imagePath = img;
            }

            public override string ToString()
            {
                return FirstName + " " + " " + LastName;
            }
        }
    }
}
