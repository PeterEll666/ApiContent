using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiContent.Models;
using ApiContent.Models.DTOs;
using AutoMapper;

namespace ApiContent
{
    public static class MapperConfig
    {
        public static void ConfigureMapper()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<TextDTO, Text>();
                cfg.CreateMap<Text, TextDTO>();
                                         cfg.CreateMap<TextLang, TextDTO>()
                                             .ForMember(dest => dest.Name,
                                                 opt => opt.MapFrom(src => src.Text.Name))
                                             .ForMember(dest => dest.TextType,
                                                 opt => opt.MapFrom(src => src.Text.TextType));
                cfg.CreateMap<ContentDTO, Content>();
                cfg.CreateMap<Content, ContentDTO>();
                cfg.CreateMap<PageDTO, Page>();
                cfg.CreateMap<Page, PageDTO>();
            });

        }
    }
}