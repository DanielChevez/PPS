using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_Record_Data
{
    public class DB_Logic
    {
        public List<int> ActionsList = new List<int>();

        public List<LoginCredentials> UserList()
        {
            return new List<LoginCredentials>
            {
                new LoginCredentials{Email = "cali@gmail.com",Username = "Carlos",Password = "1234", Roles = new string[] {"Admin" } },
                new LoginCredentials{Email = "Daniel@gmail.com",Username = "Daniel",Password = "1234", Roles = new string[] {"Asistente" } },
                new LoginCredentials{Email = "Manuel@gmail.com",Username = "Manuel",Password = "1234", Roles = new string[] {"Doctor/a" } },
                new LoginCredentials{Email = "Fulano@gmail.com",Username = "Fulano",Password = "1234", Roles = new string[] {"Estudiante" } },
                new LoginCredentials{Email = "Liss@gmail.com",Username = "Liss",Password = "1234", Roles = new string[] {"Paciente" } }
            };
        }


        public LoginCredentials ValidateUser(string _Email, string _Password)
        {
            return UserList().Where(us => us.Email == _Email && us.Password == _Password).FirstOrDefault();
        }



        public List<int> getActionsOfUser()
        {

            ActionsList.Add(1);
            ActionsList.Add(2);
            ActionsList.Add(3);
            ActionsList.Add(4);
            ActionsList.Add(5);
            ActionsList.Add(6);
            ActionsList.Add(7);
            ActionsList.Add(8);
            ActionsList.Add(9);

            return ActionsList;
        }
    }
}
