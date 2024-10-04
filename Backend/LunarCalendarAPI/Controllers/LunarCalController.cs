using Lunar;
using LunarCalendarAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace LunarCalendarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LunarCalController : Controller
    {
        private readonly ILogger<LunarCalController> _logger;
        private readonly DateTime _currentDate;
        private readonly Solar _solar;

        public LunarCalController(ILogger<LunarCalController> logger)
        {
            _logger = logger;
            this._currentDate = DateTime.Now;
            this._solar = new Solar(this._currentDate.Year, this._currentDate.Month, this._currentDate.Day);
        }

        [HttpGet, Route("LunarDate")]
        public IActionResult GetDate()
        {
            return Ok(new { Value = this._solar.Lunar.ToString() });
        }

        [HttpGet, Route("Foto_ZhaiTen")]
        public IActionResult GetFoto_ZhaiTenList(int year)
        {
            FotoService service = new FotoService(this._solar);
            var list = service.ZhaiTenListByYear(year);

            return Ok(new { Year = year, Dates = list });
        }

        [HttpGet, Route("Foto_ZhaiTeniCal")]
        public async Task<IActionResult> GetFoto_ZhaiTeniCal(int year)
        {
            FotoService service = new FotoService(this._solar);
            var list = service.ZhaiTenListByYear(year);
            var datestring = service.GetCalString("Foto_ZhaiTen", list);

            var stream = await service.GetFileMemoryStream(datestring);

            return File(stream, "application/octet-stream", $"zhaiten-{year}.ical");
        }

        [HttpGet, Route("Foto_ZhaiShuo")]
        public IActionResult GetFoto_ZhaiShuoList(int year)
        {
            FotoService service = new FotoService(this._solar);
            var list = service.ZhaiShuoListByYear(year);

            return Ok(new { Year = year, Dates = list });
        }

        [HttpGet, Route("Foto_ZhaiShuoiCal")]
        public async Task<IActionResult> GetFoto_ZhaiShuoiCal(int year)
        {
            FotoService service = new FotoService(this._solar);
            var list = service.ZhaiShuoListByYear(year);
            var datestring = service.GetCalString("Foto_ZhaiShuo", list);

            var stream = await service.GetFileMemoryStream(datestring);

            return File(stream, "application/octet-stream", $"zhaishuo-{year}.ical");
        }

        [HttpGet, Route("Foto_ZhaiGuanYin")]
        public IActionResult GetFoto_ZhaiGuanYinList(int year)
        {
            FotoService service = new FotoService(this._solar);
            var list = service.ZhaiGuanYinListByYear(year);

            return Ok(new { Year = year, Dates = list });
        }

        [HttpGet, Route("Foto_ZhaiGuanYiniCal")]
        public async Task<IActionResult> GetFoto_ZhaiGuanYiniCal(int year)
        {
            FotoService service = new FotoService(this._solar);
            var list = service.ZhaiGuanYinListByYear(year);
            var datestring = service.GetCalString("Foto_ZhaiGuanYin", list);

            var stream = await service.GetFileMemoryStream(datestring);

            return File(stream, "application/octet-stream", $"zhaiGuanYin-{year}.ical");
        }

        //[HttpGet, Route("Foto_ZhaiShuoiCal2")]
        //public async Task<IActionResult> GetFoto_ZhaiShuoiCal2(int year)
        //{
        //    FotoService service = new FotoService(this._solar);
        //    var list = service.ZhaiShuoListByYear(year);
        //    var datestring = service.GetCalString("Foto_ZhaiShuo", list);

        //    var stream = await service.GetFileMemoryStream(datestring);

        //    return File(stream, "application/octet-stream", $"zhaishuo-{year}.ical");
        //}

        //private string ConvertToUTF8(string value)
        //{
        //    Byte[] bytes = Encoding.Unicode.GetBytes(value);
        //    var bUTF8 = Encoding.UTF8.GetString(bytes);

        //    return bUTF8 ?? "-";
        //}
    }
}
