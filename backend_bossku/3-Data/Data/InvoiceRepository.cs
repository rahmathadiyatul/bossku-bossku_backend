using backend_bossku._1_Core.Entities;
using backend_bossku._2_Service.Service.Interface;
using backend_bossku._3_Data.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_bossku._3_Data.Data
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public string CreateInvoice()
        {
            var result = "INSERT INTO dbo.invoice " +
                "(inVNum, date, userId, totalCourse, totalPrice) values " +
                "(@InvNum, GETDATE(), @UserId, @TotalCourse, @TotalPrice)";
            return result;
        }

        public string GetInvoice()
        {
            var result = "SELECT * FROM [dbo].[invoice]  " +
                "WHERE [dbo].[invoice].[userId] = @Id";
            return result;
        }

        public string CreateInvoiceCourseRelation()
        {
            var result = "INSERT INTO dbo.course_invoice_relation " +
                "(invoiceId, courseId, schedule) values " +
                "((SELECT TOP (1) [idInvoice] " +
                    "FROM[SoupDatabase].[dbo].[invoice]" +
                    "WHERE[invNum] = @InvNum)" +
                ", @Courses" +
                ", @Schedule)";
            return result;
        }
        //(SELECT TOP(1)[idCourse]" +
                   // "FROM[SoupDatabase].[dbo].[course]" +
                    //"WHERE[name] = @Courses)

        public string GetDetailInvoice()
        {
            var result = "SELECT " +
                "cir.[invoiceId], i.[invNum],c.[name],c.[category],c.[price]" +
                ",i.[date],cir.[schedule],i.[totalPrice],c.[img] " +
                "FROM[SoupDatabase].[dbo].[course_invoice_relation] cir " +
                "JOIN[SoupDatabase].[dbo].[course] c " +
                "ON cir.[courseId] = c.[idCourse] " +
                "JOIN[SoupDatabase].[dbo].[invoice] i " +
                "ON cir.[invoiceId] = i.idInvoice " +
                "WHERE cir.[invoiceId] = @Id";
            return result;
        }

        public string GetMyClass()
        {
            var result = "SELECT " +
                "cir.[invoiceId], i.[invNum],c.[name],c.[category],c.[price]" +
                ",i.[date],cir.[schedule],c.[description],c.[img] " +
                "FROM[SoupDatabase].[dbo].[course_invoice_relation] cir " +
                "JOIN[SoupDatabase].[dbo].[course] c " +
                "ON cir.[courseId] = c.[idCourse] " +
                "JOIN[SoupDatabase].[dbo].[invoice] i " +
                "ON cir.[invoiceId] = i.idInvoice " +
                "WHERE i.[userId] = @Id " +
                "AND cir.[schedule] > GETDATE()";
            return result;
        }

        public string GetPrice()
        {
            var result = "SELECT [price]" +
                "FROM[SoupDatabase].[dbo].[course] " +
                "WHERE[dbo].[course].[idCourse] = @Courses";
            return result;
        }
        public string GetLastInvNum()
        {
            var result = "SELECT TOP (1) [invNum]" +
                "FROM[SoupDatabase].[dbo].[invoice]" +
                "ORDER BY[idInvoice] DESC";
            return result;
        }

        /*        public async Task<Invoice> UpdateInvoice(Invoice invoice)
                {
                    await _dBService.InvoiceCourseRelation("UPDATE dbo.course_invoice_relation" +
                        " SET courseId=@CourseId WHERE invoiceId=@InvoiceId;", invoice);
                    await _dBService.ModifyDataInvoice("UPDATE dbo.invoice" +
                        " SET invNum=@InvNum, date=@Date, schedule=@Schedule, userId=@UserId " +
                        "WHERE idInvoice=@IdInvoice;", invoice);
                    return invoice;
                }
                public async Task<bool> DeleteInvoice(int id)
                {
                    throw new NotImplementedException();
                }*/
    }
}
