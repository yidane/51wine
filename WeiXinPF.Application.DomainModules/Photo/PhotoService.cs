using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using WeiXinPF.Application.DomainModules.Common;
using WeiXinPF.Application.DomainModules.Photo.DTOS;
using WeiXinPF.Common;
using WeiXinPF.Infrastructure.DomainDataAccess.Photo;

namespace WeiXinPF.Application.DomainModules.Photo
{
    public class PhotoService
    {

        public PhotoService()
        {
            Mapper.CreateMap<photoActionInfo, photoActionDTO>().ForMember(
               dest => dest.beginDate,
               opt => opt.ResolveUsing(src => src.beginDate.ToString("yyyy-MM-dd HH:mm:ss"))
                )
                .ForMember(dest => dest.endDate,
               opt => opt.ResolveUsing(src => src.endDate.ToString("yyyy-MM-dd HH:mm:ss"))
                ).ForMember(dest => dest.createTime,
               opt => opt.ResolveUsing(src => src.createTime.ToString("yyyy-MM-dd HH:mm:ss"))
                );
            Mapper.CreateMap<photoActionDTO, photoActionInfo>().ForMember(
               dest => dest.beginDate,
               opt => opt.ResolveUsing(src => MyCommFun.Obj2DateTime(src.beginDate))
                )
                .ForMember(dest => dest.endDate,
               opt => opt.ResolveUsing(src => MyCommFun.Obj2DateTime(src.endDate))
                );
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="searchPhrase"></param>
        /// <param name="wid"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public PagingDto<photoActionDTO> GetList(int pageIndex,int pageSize, 
            string searchPhrase, int wid)
        {
            PagingDto<photoActionDTO> result = null;

            var list = photoActionInfo.GetList(p => p.wid == wid&&
            (p.actContent.Contains(searchPhrase)||p.brief.Contains(searchPhrase)
            ||p.actName.Contains(searchPhrase)
            ))
                .OrderByDescending(p => p.createTime);
            if (list.Any())
            {
                result=new PagingDto<photoActionDTO>()
                {
                    pageIndex = pageIndex,
                    pageSize = pageSize,
                    totalCount = list.Count()
                };
                var li = list.Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).ToList();
                var da = Mapper.Map<List<photoActionInfo>
                    , List<photoActionDTO>>(li);
                result.list = da;
            }


            return result;
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        public void Add(photoActionDTO dto)
        {
            if (dto != null)
            {
                var info = Mapper.Map<photoActionDTO, photoActionInfo>(dto);
                info.Add();
                dto.id = info.id;
            }
        }

        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public photoActionDTO GetModel(int id)
        {
            photoActionDTO result = null;
            var info = photoActionInfo.GetModel(id);
            if (info != null)
            {
                result = Mapper.Map<photoActionDTO>(info);
            }
            return result;
        }

        public void Modify(photoActionDTO dto)
        {
            if (dto != null)
            {
                var info = Mapper.Map<photoActionDTO, photoActionInfo>(dto);
                info.Modify();
            }
        }

        public void Delete(int id)
        {
            photoActionInfo.Delete(id);
        }

        public bool Exists(int id)
        {
            return photoActionInfo.Exists(id);
        }
    }
}
