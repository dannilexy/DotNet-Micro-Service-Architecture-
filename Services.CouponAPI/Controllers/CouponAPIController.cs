using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.CouponAPI.Data;
using Services.CouponAPI.Models;
using Services.CouponAPI.Models.Dto;

namespace Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private IMapper _mapper;
        private readonly ServicesCouponApiContext _context;
        private ResponseDto _responseDto;

        public CouponAPIController(ServicesCouponApiContext _context, IMapper _mapper)
        {
            this._context = _context;
            _responseDto = new ResponseDto();
            this._mapper = _mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var coupons = await _context.Coupon.ToListAsync();
                var couponDto = _mapper.Map<List<CouponDto>>(coupons);
                _responseDto.Result = couponDto;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {

                _responseDto.IsSucess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var coupons = await _context.Coupon.FirstAsync(x=>x.Id == id);
                var couponDto = _mapper.Map<CouponDto>(coupons);
                _responseDto.Result = couponDto;
                return Ok(_responseDto);

            }
            catch (Exception ex)
            {
                _responseDto.IsSucess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);

            }
        }
        [HttpGet]
        [Route("Getbycode/{code}")]
        public async Task<IActionResult> Getbycode(string code)
        {
            try
            {
                var coupons = await _context.Coupon.FirstOrDefaultAsync(x => x.CouponCode.ToLower() == code.ToLower());
                if (coupons == null) { _responseDto.IsSucess = false; }
                var couponDto = _mapper.Map<CouponDto>(coupons);
                _responseDto.Result = couponDto;
                return Ok(_responseDto);

            }
            catch (Exception ex)
            {
                _responseDto.IsSucess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CouponDto couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);
                await _context.Coupon.AddAsync(coupon);
                await _context.SaveChangesAsync();
                _responseDto.Message = "Coupon Created Successfully";
            }
            catch (Exception ex)
            {
                _responseDto.IsSucess = false;
                _responseDto.Message = ex.Message;
            }
            return Ok(_responseDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CouponDto couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);
                 _context.Coupon.Update(coupon);
                await _context.SaveChangesAsync();
                _responseDto.Message = "Coupon Updated Successfully";
            }
            catch (Exception ex)
            {
                _responseDto.IsSucess = false;
                _responseDto.Message = ex.Message;
            }
            return Ok(_responseDto);
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var coupon = await _context.Coupon.FirstAsync(x => x.Id == id);
                 _context.Remove(coupon);
                await _context.SaveChangesAsync();
                return Ok(_responseDto);

            }
            catch (Exception ex)
            {
                _responseDto.IsSucess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);

            }
        }
    }
}
