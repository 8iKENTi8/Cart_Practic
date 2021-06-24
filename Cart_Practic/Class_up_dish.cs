using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart_Practic
{
    class Class_up_dish
    {
       static public string id,id_cat,id_res,id_ingr,ingr,ingr1,com,com1,id1,id2;

      static public  User_Form form = new User_Form();
      

        static public string[] names = new string[10];
       static public string[] names1 = new string[10];

        static public string[] cost = new string[10];
        static public string[] cost1 = new string[10];

        static public string[] img = new string[10];
        static public string[] img1 = new string[10];
        static public string[] tm = new string[10];
        static public string[] tm1 = new string[10];

        static public bool roleId(string role)
        {
            if (role == "админ")
                return true;
            else
                return false;
        } 

    }
}
