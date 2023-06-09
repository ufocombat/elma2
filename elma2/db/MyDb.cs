﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace elma2
{
    internal class MyDb
    {
        public static MySqlConnection getSqlConnection()
        {
            string connectionString = "server=localhost; database=elma; uid=root;pwd=123456;";
            MySqlConnection con = new MySqlConnection(connectionString);

            try
            {
                con.Open();
            }
            catch (Exception error)
            {
                MessageBox.Show($"Ошибка подключения к БД: {error.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return con;
        }

        public static DataTable getSelectTable(String select)
        {
            MySqlConnection con = getSqlConnection();
            DataTable dataTable = new DataTable("table");

            try
            {
                MySqlDataAdapter returnVal = new MySqlDataAdapter(select, con);

                returnVal.Fill(dataTable);
                con.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show($"Ошибка выполнения запроса: {error.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }

        public static DataTable getCustomers()
        {
            return getSelectTable($"SELECT * FROM customers");
        }

        public static DataTable getPersons()
        {
            return getSelectTable($"SELECT * FROM persons");
        }


        public static DataTable getOrders()
        {
            return getSelectTable($"SELECT * FROM orders");
        }

        public static DataTable getOrder(Int32 id)
        {
            return getSelectTable($"SELECT * FROM orders where id={id}");
        }

        public static DataTable getOrderDetail(Int32 id)
        {
            return getSelectTable($"SELECT * FROM orderDetail d JOIN services s on d.serviceId=s.id where orderId={id} order by lineNo");
        }

        public static DataTable getServices()
        {
            return getSelectTable($"SELECT * FROM services");
        }

        public static DataTable getServiceStutuses()
        {
            return getSelectTable($"SELECT * FROM servicestatuses");
        }

        public static DataTable getOrderStutuses()
        {
            return getSelectTable($"SELECT * FROM orderstatuses");
        }


        public static Int32 getNextSosudNo()
        {
            DataTable result = getSelectTable($"SELECT max(sosudNo) FROM elma.orders");
            return Convert.ToInt32(result.Rows[0][0]) + 1;
        }

        public static Boolean testSosudNo(Int32 no)
        {
            DataTable result = getSelectTable($"SELECT count(sosudNo) FROM elma.orders where sosudNo={no}");
            return Convert.ToInt32(result.Rows[0][0])==0;
        }

        private static Int32 getOrderNextLine(Int32 orderId)
        {
            DataTable result = getSelectTable($"SELECT max(lineNo) FROM elma.orderdetail where orderId={orderId}");
            return Convert.ToInt32(result.Rows[0][0])+100;
        }

        public static void addServiceToOrder(Int32 orderId, Int32 serviceId)
        {
            MySqlConnection con = getSqlConnection();
            MySqlCommand com = con.CreateCommand();

            com.CommandText = "insert into orderDetail(orderId,lineNo, serviceId) VALUES(@orderId,@lineNo,@serviceId)";

            com.Parameters.AddWithValue("@orderId", orderId);
            com.Parameters.AddWithValue("@lineNo", getOrderNextLine(orderId));
            com.Parameters.AddWithValue("@serviceId", serviceId);

            try
            {
                com.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show($"Ошибка сохранения заказа: {error.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            con.Close();
        }

        //public static void callStoreProc()
        //{
        //    MySqlConnection con = getSqlConnection();
        //    MySqlCommand com = con.CreateCommand();

        //    com.CommandText = "currencies_cur_upd";
        //    com.CommandType = CommandType.StoredProcedure;

        //    try
        //    {
        //        com.ExecuteNonQuery();
        //    }
        //    catch (Exception error)
        //    {
        //        MessageBox.Show($"Ошибка сохранения заказа: {error.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    con.Close();
        //}

        //Работа с пользователями

        public static DataTable getUsers()
        {
            return getSelectTable($"SELECT * FROM users order by name");
        }

        //public static DataTable getUsers(String login, String password)
        //{
        //    return getSelectTable($"SELECT * FROM users where login='{login}' and password='{password}'");
        //}

        //public static User getUser(String login)
        //{
        //    var users = getSelectTable($"SELECT * FROM users where login='{login}'");

        //    User user = new User()
        //    {
        //        login = (String)users.Rows[0]["login"],
        //        name = (String)users.Rows[0]["name"],
        //        roleCode = (String)users.Rows[0]["roleCode"],
        //        customerId = Convert.ToInt32(users.Rows[0]["customerId"])
        //    };

        //    return user;
        //}

        //public static String getUserAddress(String login)
        //{
        //    DataTable tab = getSelectTable($"select c.address from users u join customers c on u.customerId=c.id where u.login='{login}'");
        //    return (String)tab.Rows[0][0];
        //}


        ////Работа со статусами

        //public static DataTable getStatuses()
        //{
        //    return getSelectTable("SELECT * FROM statuses order by level");
        //}


        ////Работа с услугами

        //public static DataTable getServices()
        //{
        //    return getSelectTable("SELECT * FROM services order by name");
        //}

        //public static DataTable getAllServices()
        //{
        //    return getSelectTable("SELECT id, name, price FROM services order by name");
        //}

        //public static DataTable getServicesFilter(String name, Decimal priceFrom, Decimal priceTo)
        //{
        //    if (String.IsNullOrEmpty(name))
        //    {
        //        return getSelectTable($"SELECT id, name, price FROM services where price>={priceFrom} and price<={priceTo} order by price");
        //    }
        //    else
        //    {
        //        return getSelectTable($"SELECT id, name, price FROM services where name like '%{name}%' and (price>={priceFrom} and price<={priceTo}) order by name");
        //    }
        //}

        //public static Service getServiceById(Int32 id)
        //{
        //    var s = getSelectTable($"SELECT * FROM services where id={id}");

        //    return new Service()
        //    {
        //        id = Convert.ToInt32(s.Rows[0]["id"]),
        //        name = (String)s.Rows[0]["name"],
        //        price = Convert.ToInt32(s.Rows[0]["price"])
        //    };
        //}

        ////Работа с ролями

        //public static DataTable getRoles()
        //{
        //    return getSelectTable("SELECT * FROM roles");
        //}

        //public static DataTable getRole(String code)
        //{
        //    return getSelectTable($"SELECT * FROM roles where code='{code}'");
        //}

        ////Работа с заказами

        //public static DataTable getOrders()
        //{
        //    return getSelectTable("SELECT * FROM orders");
        //}

        //public static DataTable getUsersOrders()
        //{
        //    return getSelectTable("SELECT * FROM otk.userorders");
        //}

        //public static DataTable getUserOrders(String login)
        //{
        //    return getSelectTable($"SELECT `Заказ Но`, `Статус`,`Услуга`, `Скидка (%)`,`Сумма Заказа`, `Итого` from userOrders where `Логин`='{login}' order by `Заказ Но`");
        //}

        //public static Order getOrderById(Int32 id)
        //{
        //    var o = getSelectTable($"SELECT * FROM orders where id='{id}");

        //    return new Order()
        //    {
        //        id = Convert.ToInt32(o.Rows[0]["id"]),
        //        userLogin = (String)o.Rows[0]["userLogin"],
        //        serviceId = Convert.ToInt32(o.Rows[0]["serviceId"]),
        //        discountPercent = Convert.ToDecimal(o.Rows[0]["discountPercent"])
        //    };
        //}

        //public static Order getOrderViewById(Int32 id)
        //{
        //    var o = getSelectTable($"SELECT * FROM orders o, services s where o.serviceId=s.id and o.id={id}");

        //    return new Order()
        //    {
        //        id = Convert.ToInt32(o.Rows[0]["id"]),
        //        userLogin = (String)o.Rows[0]["userLogin"],
        //        discountPercent = Convert.ToDecimal(o.Rows[0]["discountPercent"]),
        //        serviceId = Convert.ToInt32(o.Rows[0]["serviceId"]),
        //        amount = Convert.ToDecimal(o.Rows[0]["price"]),
        //        status = (String)o.Rows[0]["status"],
        //    };
        //}

        //public static void insertOrder(Order order)
        //{
        //    MySqlConnection con = getSqlConnection();
        //    MySqlCommand com = con.CreateCommand();

        //    com.CommandText = "insert into orders(serviceId,userLogin,discountPercent,status) VALUES(@serviceId,@userLogin,@discountPercent,@status)";

        //    com.Parameters.AddWithValue("@serviceId", order.serviceId);
        //    com.Parameters.AddWithValue("@userLogin", order.userLogin);
        //    com.Parameters.AddWithValue("@discountPercent", order.discountPercent);
        //    com.Parameters.AddWithValue("@status", order.status);

        //    try
        //    {
        //        com.ExecuteNonQuery();
        //    }
        //    catch (Exception error)
        //    {
        //        MessageBox.Show($"Ошибка сохранения заказа: {error.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    con.Close();
        //}

        //public static void deleteOrderById(Int32 id)
        //{
        //    MySqlConnection con = getSqlConnection();
        //    MySqlCommand com = con.CreateCommand();

        //    com.CommandText = $"delete from orders where id={id}";

        //    try
        //    {
        //        com.ExecuteNonQuery();
        //    }
        //    catch (Exception error)
        //    {
        //        MessageBox.Show($"Ошибка сохранения заказа: {error.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    con.Close();
        //}

        public static void updateOrder(Order order, DataGridViewRowCollection details)
        {
            MySqlConnection con = getSqlConnection();
            MySqlCommand com = con.CreateCommand();

            com.CommandText = $"update orders set  status=@status, description=@description, sosudNo=@sosudNo  where id={order.id}";

            com.Parameters.AddWithValue("@status", order.status);
            com.Parameters.AddWithValue("@description", order.description);
            com.Parameters.AddWithValue("@sosudNo", order.sosudNo);

            try
            {
                com.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show($"Ошибка сохранения заказа: {error.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach (DataGridViewRow row in details)
            {
                MySqlCommand det = con.CreateCommand();
                det.CommandText = $"update orderdetail set  serviceStatus=@serviceStatus, userLogin=@userLogin where orderId={row.Cells[0].Value} and lineNo={row.Cells["lineNo"].Value}";
                det.Parameters.AddWithValue("@serviceStatus", row.Cells["serviceStatus"].Value);
                det.Parameters.AddWithValue("@userLogin", row.Cells["userLogin"].Value);

                det.ExecuteNonQuery();
            }

            con.Close();
        }

        //public static void archOrderById(Int32 orderId)
        //{
        //    MySqlConnection con = getSqlConnection();
        //    MySqlCommand com = con.CreateCommand();

        //    MySqlTransaction myTrans = con.BeginTransaction();

        //    com.Transaction = myTrans;

        //    try
        //    {
        //        com.CommandText = $"INSERT INTO orders_arch(id,userLogin,serviceId,discountPercent,status) SELECT id,userLogin,serviceId,discountPercent, status FROM orders o WHERE o.id={orderId}";
        //        com.ExecuteNonQuery();

        //        com.CommandText = $"delete from orders where id={orderId}";
        //        com.ExecuteNonQuery();

        //        myTrans.Commit();
        //    }
        //    catch (Exception error)
        //    {
        //        myTrans.Rollback();
        //        MessageBox.Show($"Ошибка архивирования заказа: {error.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    con.Close();
        //}
    }
}
