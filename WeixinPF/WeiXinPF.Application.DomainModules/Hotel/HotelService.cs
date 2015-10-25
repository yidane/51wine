using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using WeiXinPF.Application.DomainModules.Hotel.DTOS;
using WeiXinPF.Infrastructure.DomainDataAccess.Hotel;
using WeiXinPF.Model;

namespace WeiXinPF.Application.DomainModules.Hotel
{
   public  class HotelService
    {
        public HotelService()
        {
            Mapper.CreateMap<TuidanInfo, TuidanDto>() ;
            Mapper.CreateMap<TuidanDto, TuidanInfo>();
        }

        /// <summary>
        /// 添加退单实体
        /// </summary>
        /// <param name="dto"></param>
        public void AddTuidan(TuidanDto dto)
       {
            if (dto != null)
            {
                var info = Mapper.Map<TuidanDto, TuidanInfo>(dto);
                info.Add();
                dto.id = info.id;
            }
        }
        /// <summary>
        /// 修改退单模型
        /// </summary>
        /// <param name="dto"></param>
        public void ModifyTuidan(TuidanDto dto)
        {
            if (dto != null)
            {
                var info = Mapper.Map<TuidanDto, TuidanInfo>(dto);
                info.Modify();
                
            }
        }
        /// <summary>
        /// 获取退单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TuidanDto GetModel(int id)
        {
            TuidanDto result = null;
            var info = TuidanInfo.GetModel(id);
            if (info != null)
            {
                result = Mapper.Map<TuidanDto>(info);
              
            }
            return result;
        }

        /// <summary>
        /// 获取退单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TuidanDto GetModel(int dingdanid,int hotelid)
        {
            TuidanDto result = null;
            var info =
                TuidanInfo.GetList(t => t.dingdanid == dingdanid && t.hotelid == hotelid)
                    .FirstOrDefault();
            if (info != null)
            {
                result = Mapper.Map<TuidanDto>(info);
                
            }
            return result;
        }

    }
}
