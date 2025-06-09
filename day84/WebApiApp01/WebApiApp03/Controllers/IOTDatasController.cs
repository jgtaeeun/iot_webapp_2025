using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiApp03.Models;

namespace WebApiApp03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IOTDatasController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public IOTDatasController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/IOTDatas
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<IOTDatas>>> GetIOT_DATA()
        //{
        //    return await _context.IOT_DATA.ToListAsync();
        //}


        [HttpGet]
        public async Task<ActionResult<IEnumerable<IOTDatas>>> GetIOT_DATA(string serviceKey, string startDate, string endDate , string resultType)
        {
            if (serviceKey == null)
            {
                return BadRequest();
            }
            else
            {
                //서버에서 키를 검색해서 valid 서비스키일 때
            }
            Debug.WriteLine($"startDate: {startDate}, endDate: {endDate}");
            startDate = $"{startDate} 00:00:00";
            endDate = $"{endDate} 23:59:59";
            var result = await _context.IOT_DATA.FromSql($"SELECT * FROM IOT_DATA WHERE sensing_dt >= {startDate } AND sensing_dt <= {endDate}").ToListAsync();
            Debug.WriteLine($"총개수 : {result.Count}");
            //resultType이 xml과 json에 따라 리턴하는 데이터 형을 변경
            if (resultType == "xml")
            {
                //xml로 형변환
            }
            else if (resultType == "json")
            {
                //json으로 형변환
            }


            return result;

        }


        // GET: api/IOTDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IOTDatas>> GetIOTDatas(int id)
        {
            var iOTDatas = await _context.IOT_DATA.FindAsync(id);

            if (iOTDatas == null)
            {
                return NotFound();
            }

            return iOTDatas;
        }

        // PUT: api/IOTDatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutIOTDatas(int id, IOTDatas iOTDatas)
        //{
        //    if (id != iOTDatas.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(iOTDatas).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!IOTDatasExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/IOTDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<IOTDatas>> PostIOTDatas(IOTDatas iOTDatas)
        //{
        //    _context.IOT_DATA.Add(iOTDatas);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetIOTDatas", new { id = iOTDatas.Id }, iOTDatas);
        //}

        // DELETE: api/IOTDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIOTDatas(int id)
        {
            var iOTDatas = await _context.IOT_DATA.FindAsync(id);
            if (iOTDatas == null)
            {
                return NotFound();
            }

            _context.IOT_DATA.Remove(iOTDatas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IOTDatasExists(int id)
        {
            return _context.IOT_DATA.Any(e => e.Id == id);
        }
    }
}
