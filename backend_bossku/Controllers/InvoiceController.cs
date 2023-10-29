using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend_bossku._1_Core.Entities;
using backend_bossku._2_Service.Service;
using backend_bossku._2_Service.Service.Interface;

namespace backend_bossku.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Invoice invoice)
        {
            var result = await invoiceService.Create(invoice);
            return Ok(result);
        }
        [HttpGet("detail_invoice/{idInvoice}:int")]
        public async Task<List<DetailInvoice>> GetDetail(int idInvoice)
        {
            List<DetailInvoice> result = await invoiceService.GetDetail(idInvoice);
            return result;
        }
        [HttpGet("invoice/{idUser}:int")]
        public async Task<List<Invoice>> Get(int idUser)
        {
            List<Invoice> result = await invoiceService.Get(idUser);
            return result;
        }
        [HttpGet("myclass/{idUser}:int")]
        public async Task<List<DetailInvoice>> MyClass(int idUser)
        {
            List<DetailInvoice> result = await invoiceService.MyClass(idUser);
            return result;
        }
/*        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Invoice invoice)
        {
            var result = await invoiceService.Update(invoice);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var result = await invoiceService.Delete(id);
            return Ok(result);
        }*/
    }
}
