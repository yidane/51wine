using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using WeiXinPF.Application.DomainModules.Photo.DTOS;
using WeiXinPF.Infrastructure.DomainDataAccess.Photo;

namespace WeiXinPF.Application.DomainModules.Photo
{
    public class PhotoService
    {
        
        public PhotoService()
        {
            Mapper.CreateMap< photoActionInfo,photoActionDTO >().ForMember(
               dest=>dest.beginDate,
               opt=>opt.ResolveUsing(src=>src.beginDate.ToString("yyyy-MM-dd HH:mm:ss"))
                )
                .ForMember(dest => dest.endDate,
               opt => opt.ResolveUsing(src => src.endDate.ToString("yyyy-MM-dd HH:mm:ss"))
                );
            Mapper.CreateMap<photoActionDTO, photoActionInfo>().ForMember(
               dest => dest.beginDate,
               opt => opt.ResolveUsing(src => DateTime.Parse(src.beginDate))
                )
                .ForMember(dest => dest.endDate,
               opt => opt.ResolveUsing(src => DateTime.Parse(src.endDate))
                );
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="wid"></param>
        /// <returns></returns>
        public List<photoActionDTO> GetList(int wid)
        {
            List<photoActionDTO> result = null;

            var list = photoActionInfo.GetList(wid);
            if (list!=null&&list.Any())
            {
                var li = list.ToList();
                var da = Mapper.Map<List<photoActionInfo>
                    , List<photoActionDTO>>(li);
                result = da.ToList();
            }


            return result;
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        public void Add( photoActionDTO  dto)
        {
            if (dto!=null)
            {
                var info=Mapper.Map<photoActionDTO,photoActionInfo>(dto);
                info.Add();
                dto.id = info.id;
            }
        }

        public photoActionDTO GetModel(int id)
        {
            photoActionDTO result = null;
            var info = photoActionInfo.GetModel(id);
            if (info!=null)
            {
                result = Mapper.Map<photoActionDTO>(info);
            }
            return result;
        }

        public void Modify(photoActionDTO dto)
        {
            if (dto!=null)
            {
                var info = Mapper.Map<photoActionDTO,photoActionInfo>(dto);
                info.Modify();
            }
        }
    }
}
