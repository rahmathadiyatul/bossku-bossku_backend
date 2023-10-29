using Microsoft.AspNetCore.Mvc;
using backend_bossku._1_Core.Entities;
using backend_bossku._2_Service.Service.Interface;
using backend_bossku._3_Data.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace backend_bossku._2_Service.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly SqlConnection _db;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
            _db = new SqlConnection("data source=.; database=SoupDatabase; integrated security=SSPI");
        }

        public async Task<bool> Create(Invoice invoice)
        {
            string command = invoiceRepository.GetPrice();
            int totalPrice = 0;
            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                
                for (int i = 0; i < invoice.Course.Count; i++)
                {
                    await _db.OpenAsync();
                    cmd.Parameters.AddWithValue("@Courses", invoice.Course[i]);
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        totalPrice += Convert.ToInt32(reader["Price"]);
                    }
                    cmd.Parameters.Clear();
                    await _db.CloseAsync();
                }
            }

            string invNum = "";
            command = invoiceRepository.GetLastInvNum();
            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                await _db.OpenAsync();
                cmd.Parameters.AddWithValue("@InvNum", invoice.InvNum);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    invNum = reader["invNum"].ToString();
                }
                await _db.CloseAsync();

            }

            int invoiceNum = 0;
            if (invNum == "")
            {
                invNum = "SOU00001";
            }
            else
            {
                invoiceNum = Convert.ToInt32(invNum.Substring(3)) + 1;
                invNum = "SOU" + invoiceNum.ToString().PadLeft(5, '0');
            }

            command = invoiceRepository.CreateInvoice();
            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                cmd.Parameters.AddWithValue("@InvNum", invNum);
                cmd.Parameters.AddWithValue("@UserId", invoice.UserId);
                cmd.Parameters.AddWithValue("@TotalCourse", invoice.Course.Count);
                cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                await _db.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                await _db.CloseAsync();
            }

            command = invoiceRepository.CreateInvoiceCourseRelation();
            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                await _db.OpenAsync();
                for (int i = 0; i < invoice.Course.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@InvNum", invNum);
                    cmd.Parameters.AddWithValue("@Courses", invoice.Course[i]);
                    cmd.Parameters.AddWithValue("@Schedule", invoice.Schedule[i]);
                    await cmd.ExecuteNonQueryAsync();
                    cmd.Parameters.Clear();
                }
                await _db.CloseAsync();
            }
            return true;
        }

        public async Task<List<Invoice>> Get(int id)
        {
            var command = invoiceRepository.GetInvoice();
            var result = new List<Invoice>();

            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                await _db.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    result.Add(new Invoice
                    {
                        IdInvoice = Convert.ToInt32(reader["idInvoice"]),
                        InvNum = reader["InvNum"].ToString(),
                        Date = (DateTime)reader["Date"],
                        UserId = Convert.ToInt32(reader["UserId"]),
                        TotalCourse = Convert.ToInt32(reader["TotalCourse"]),
                        TotalPrice = Convert.ToDecimal(reader["TotalPrice"])
                    });
                }

                await _db.CloseAsync();
                return result;
            }
        }

        public async Task<List<DetailInvoice>> GetDetail(int id)
        {
            var command = invoiceRepository.GetDetailInvoice();
            var result = new List<DetailInvoice>();

            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                await _db.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    result.Add(new DetailInvoice
                    {
                        IdInvoice = Convert.ToInt32(reader["invoiceId"]),
                        InvNum = reader["InvNum"].ToString(),
                        Date = (DateTime)reader["Date"],
                        Schedule = (DateTime)reader["Schedule"],
                        Name = reader["Name"].ToString(),
                        Category = reader["Category"].ToString(),
                        Price = Convert.ToInt32(reader["Price"]),
                        TotalPrice = Convert.ToInt32(reader["TotalPrice"]),
                        Img = reader["Img"].ToString()
                    });
                }

                await _db.CloseAsync();
                return result;
            }
        }

        public async Task<List<DetailInvoice>> MyClass(int id)
        {
            var command = invoiceRepository.GetMyClass();
            var result = new List<DetailInvoice>();

            using (SqlCommand cmd = new SqlCommand(command, _db))
            {

                cmd.Parameters.AddWithValue("@Id", id);
                await _db.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    result.Add(new DetailInvoice
                    {
                        IdInvoice = Convert.ToInt32(reader["invoiceId"]),
                        InvNum = reader["InvNum"].ToString(),
                        Date = (DateTime)reader["Date"],
                        Schedule = (DateTime)reader["Schedule"],
                        Name = reader["Name"].ToString(),
                        Category = reader["Category"].ToString(),
                        Price = Convert.ToInt32(reader["Price"]),
                        TotalPrice = Convert.ToInt32(reader["TotalPrice"]),
                        Img = reader["Img"].ToString()
                    });
                }
                await _db.CloseAsync();
                return result;
            }
        }

/*        public async Task<Invoice> Update([FromBody] Invoice invoice)
        {
            var result = await invoiceRepository.UpdateInvoice(invoice);
            return result;
        }

        public async Task<bool> Delete([FromBody] int id)
        {
            var result = await invoiceRepository.DeleteInvoice(id);
            return result;
        }*/
    }
}
