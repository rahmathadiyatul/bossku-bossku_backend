using Microsoft.AspNetCore.Mvc;
using backend_bossku._1_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_bossku._2_Service.Service.Interface
{
    public interface IInvoiceService
    {
        public Task<bool> Create(Invoice invoice);
        public Task<List<Invoice>> Get(int id);
        public Task<List<DetailInvoice>> GetDetail(int id);
        public Task<List<DetailInvoice>> MyClass(int id);
        /*public Task<Invoice> Update([FromBody] Invoice invoice);
        public Task<bool> Delete([FromBody] int id);*/
    }
}
