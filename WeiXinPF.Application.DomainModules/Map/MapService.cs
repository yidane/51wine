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
            .AfterMap((maker, dto) => dto.position = new Position()
            {
                lat = maker.Lat,
                lng = maker.Lng
            });

            Mapper.CreateMap<Model.wx_hotels_info, MapDTO>()
            .ForMember(dto => dto.id, opt => opt.MapFrom(hotel => hotel.id))
            .ForMember(dto => dto.name, opt => opt.MapFrom(hotel => hotel.hotelName))
            .ForMember(dto => dto.address, opt => opt.MapFrom(hotel => hotel.hotelAddress))
            .ForMember(dto => dto.remark, opt => opt.MapFrom(hotel => hotel.hotelIntroduct))
            .AfterMap((maker, dto) => dto.position = new Position()
            {
                lat = (double)maker.xplace,
                lng = (double)maker.yplace
            });

            Mapper.CreateMap<Model.wx_diancai_shopinfo, MapDTO>()
            .ForMember(dto => dto.id, opt => opt.MapFrom(shop => shop.id))
            .ForMember(dto => dto.name, opt => opt.MapFrom(shop => shop.hotelName))
            .ForMember(dto => dto.address, opt => opt.MapFrom(shop => shop.address))
            .ForMember(dto => dto.remark, opt => opt.MapFrom(shop => shop.hotelintroduction))
            .AfterMap((maker, dto) => dto.position = new Position()
            {
                lat = (double)maker.xplace,
                lng = (double)maker.yplace
            });
        }

        /// <summary>
        /// 获取景点实体
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
        public MapDTO GetShop(int id)
        {
            MapDTO result = null;

            var hotel = new BLL.wx_diancai_shopinfo().GetModel(id);

            result = Mapper.Map<Model.wx_diancai_shopinfo, MapDTO>(hotel);

            return result;
        }


        public MapDTO GetHotel(int id)
        {
            MapDTO result = null;

            var hotel = new BLL.wx_hotels_info().GetModel(id);

            result = Mapper.Map<Model.wx_hotels_info, MapDTO>(hotel);

            return result;
        }

        /// <summary>
        /// 获取所有周边景点
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="keywords"></param>
        /// <param name="recommend"></param>
        /// <returns></returns>
        public List<POIDto> GetScenics(int wid, string keywords, bool recommend = false)
        {
            BLL.wx_travel_marker bll = new BLL.wx_travel_marker();

            string strWhere = string.Format("wid={0}", wid);

            if (!string.IsNullOrEmpty(keywords))
            {
                strWhere += string.Format("  And Name Like '%{0}%'", keywords);
            }

            if (recommend)
            {
                strWhere += " And Recommend=1";
            }

            var markers = bll.GetModelList(strWhere);

            if (markers.Any())
            {
                return markers.Select(m => new POIDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Introduction = m.Remark,
                    Logo = "",
                    Lat = m.Lat,
                    Lng = m.Lng,
                    Url = m.Url,
                    PoiType = "scenic"
                }).ToList();
            }

            return null;
        }

        /// <summary>
        /// 获取周边美食
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="keywords"></param>
        /// <param name="recommend"></param>
        /// <returns></returns>
        public List<POIDto> GetCateringShops(int wid, string keywords, bool recommend = false)
        {
            BLL.wx_diancai_shopinfo bll = new BLL.wx_diancai_shopinfo();

            string strWhere = string.Format("wid={0}", wid);

            if (!string.IsNullOrEmpty(keywords))
            {
                strWhere += string.Format("  And hotelName Like '%{0}%'", keywords);
            }

            if (recommend)
            {
                strWhere += " And Recommend=1";
            }

            var shops = bll.GetModelList(strWhere);

            if (shops.Any())
            {
                return shops.Select(s => new POIDto
                {
                    Id = s.id,
                    Name = s.hotelName,
                    Introduction = s.hotelintroduction,
                    Logo = s.hotelLogo,
                    Lat = (double)(s.xplace.HasValue ? s.xplace.Value : 0),
                    Lng = (double)(s.yplace.HasValue ? s.yplace.Value : 0),
                    PoiType = "catering"
                }).ToList();
            }

            return null;
        }

        /// <summary>
        /// 获取周边酒店列表
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="keywords"></param>
        /// <param name="recommend"></param>
        /// <returns></returns>
        public List<POIDto> GetHotels(int wid, string keywords, bool recommend = false)
        {
            BLL.wx_hotels_info bll = new BLL.wx_hotels_info();

            string strWhere = string.Format("wid={0} ", wid);

            if (!string.IsNullOrEmpty(keywords))
            {
                strWhere += string.Format("  And hotelName Like '%{0}%'", keywords);
            }

            if (recommend)
            {
                strWhere += " And Recommend=1";
            }

            var hotels = bll.GetModelList(strWhere);

            if (hotels.Any())
            {
                return hotels.Select(h => new POIDto
                {
                    Id = h.id,
                    Name = h.hotelName,
                    Introduction = h.hotelIntroduct,
                    Logo = h.coverPic,
                    Lat = (double)(h.xplace.HasValue ? h.xplace.Value : 0),
                    Lng = (double)(h.yplace.HasValue ? h.yplace.Value : 0),
                    PoiType = "hotel"
                }).ToList();
            }

            return null;
        }

        /// <summary>
        /// 获取推荐的Poi
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public List<POIDto> GetRecommendPoi(int wid, string keywords)
        {
            List<POIDto> recommendPois = new List<POIDto>();

            //暂时先取第一个；
            var scenics = GetScenics(wid, keywords, true);
            if (scenics != null && scenics.Any())
            {
                recommendPois.AddRange(scenics);
            }

            var caterings = GetCateringShops(wid, keywords, true);
            if (caterings != null && caterings.Any())
            {
                recommendPois.AddRange(caterings);
            }

            var hotels = GetHotels(wid, keywords, true);
            if (hotels != null && hotels.Any())
            {
                recommendPois.AddRange(hotels);
            }

            return recommendPois;
        }
    }
}
