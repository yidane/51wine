using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeiXinPF.Application.DomainModules.Map.DTOS;

namespace WeiXinPF.Application.DomainModules.Map
{
    public class MapService
    {
        #region propertes
        //todo remove test data
        private List<MapDTO> _list;
        #endregion

        public MapService()
        {
            Mapper.CreateMap<Model.wx_travel_marker, MapDTO>()
            .ForMember(dto => dto.id, opt => opt.MapFrom(marker => marker.Id))
            .ForMember(dto => dto.name, opt => opt.MapFrom(marker => marker.Name))
            .ForMember(dto => dto.address, opt => opt.MapFrom(marker => marker.Description))
            .ForMember(dto => dto.remark, opt => opt.MapFrom(marker => marker.Remark))
            .AfterMap((maker, dto) => dto.position = new Position() {
                lat=maker.Lat,
                lng=maker.Lng
            });
            //.ForMember(dto => dto.position.lat, opt => opt.MapFrom(marker => marker.Lat))
            //.ForMember(dto => dto.position.lng, opt => opt.MapFrom(marker => marker.Lng));
            //_list = new List<MapDTO>()
            //{
            //    new MapDTO()
            //    {
            //        id = 1,name = "神仙湾",address = "从xxx出发路过第一个路口后右转",
            //        position = new Position() {lat = 48.65837828202619,lng =  87.04097628593445}
            //    },
            //    new MapDTO()
            //    {
            //        id = 2,name = "卧龙湾",address = "从xxx出发路过第二个路口后右转",
            //        position = new Position() {lat = 48.621374597155466,lng =  87.05433905124664}
            //    },
            //    new MapDTO()
            //    {
            //        id = 3,name = "月亮湾",address = "从xxx出发路过第三个路口后右转",
            //        position = new Position() {lat = 48.63458191214553,lng =  87.05019772052765}
            //    },
            //    new MapDTO()
            //    {
            //        id = 4,name = "钓鱼台",address = "从xxx出发路过第四个路口后右转",
            //        position = new Position() {lat = 48.713413423425365,lng =  86.9758951663971}
            //    },
            //};
        }

        /// <summary>
        /// 获取地图实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MapDTO GetMapInfo(int id)
        {
            MapDTO result = null;
            
            var marker = new BLL.wx_travel_marker().GetModel(id);

            result = Mapper.Map<Model.wx_travel_marker, MapDTO>(marker);

            return result;
        }
    }
}
