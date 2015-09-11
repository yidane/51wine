using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeiXinPF.Application.DomainModules.Photo.DTOS;
using WeiXinPF.Infrastructure.DomainDataAccess.Photo;

namespace WeiXinPF.Application.DomainModules.Photo
{
    public class PhotoService
    {
        
        public PhotoService()
        {
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
                result = list.Select(p => new photoActionDTO()
                {
                   id = p.id,
                   wid = p.wid,
                   actContent = p.actContent,
                   beginDate = p.beginDate.ToString("yyyy-MM-dd HH:mm:ss"),
                   brief = p.brief,
                   endDate = p.endDate.ToString("yyyy-MM-dd HH:mm:ss"),
                   isAllowSharing = p.isAllowSharing
                }).ToList();
            }


            return result;
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        public void Add(photoActionDTO dto)
        {
            if (dto!=null)
            {
                var info=new photoActionInfo();
                info.Add();

            }
        }
    }
}
