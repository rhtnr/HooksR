using AutoMapper;
using HooksR.DTO;
using HooksR.Entities;
using HooksR.Options.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace HooksR.Service
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
      {
        Converters = { new HttpHeaderConverter() },
        WriteIndented = true
      };

      CreateMap<User, UIPushUser>();
      CreateMap<HttpRequest, WebRequest>()
        .ForMember(dest => dest.Headers, o => o.MapFrom(src => HeadersMapper(src)))
        .ForMember(dest => dest.Source, o => o.MapFrom(src => src.HttpContext.Connection.RemoteIpAddress))
        .ForMember(dest => dest.TimeStamp, o => o.MapFrom(src => (DateTime)src.HttpContext.Items[Consts.RequestStartedOn]));
      CreateMap<HttpRequest, UIPushRequest>()
        .ForMember(dest => dest.Headers, o => o.MapFrom(src =>
        TryDeserializeHTTPHeadersJSON(
          JsonSerializer.Serialize<List<KeyValuePair<string, IEnumerable<string>>>>(HeadersMapper(src), jsonSerializerOptions))))
        
        .ForMember(dest => dest.Source, o => o.MapFrom(src => src.HttpContext.Connection.RemoteIpAddress.ToString()))
        .ForMember(dest => dest.TimeStamp, o => o.MapFrom(src => (DateTime)src.HttpContext.Items[Consts.RequestStartedOn]));
      CreateMap<WebRequest, UIPushRequest>()
      .ForMember(dest => dest.Source, o => o.MapFrom(src => src.Source.ToString()));
      CreateMap<HttpContext, UIPushEvent>()
        .ForMember(dest => dest.Request, o => o.MapFrom(src => src.Request));

    }

    private dynamic TryDeserializeHTTPHeadersJSON(String str)
    {
      try
      {
        return JsonSerializer.Deserialize<dynamic>(str);
      }
      catch
      {
        
      }
      return str;
    }

    private List<KeyValuePair<String, IEnumerable<String>>> HeadersMapper(HttpRequest request)
    {
      return HeadersMapper(request.Headers, request.ContentType, request.ContentLength);

    }

    private List<KeyValuePair<String, IEnumerable<String>>> HeadersMapper(HttpResponse response)
    {
      return HeadersMapper(response.Headers, response.ContentType, response.ContentLength);
    }

    public List<KeyValuePair<String, IEnumerable<String>>> HeadersMapper
      (IHeaderDictionary dictionary, 
      String contentTypeValue, 
      long? contentLength)
    {
      if (dictionary == null) 
        return null;
      List<KeyValuePair<String, IEnumerable<string>>> list = HeadersMapper(dictionary);
      var contentTypeList = ContentHeadersMapper(contentTypeValue, contentLength);
      if (contentTypeList != null)
      {
        list.AddRange(contentTypeList);
      }
      return list;
    }

    public List<KeyValuePair<String, IEnumerable<String>>> HeadersMapper(IHeaderDictionary dictionary)
    {
      if (dictionary == null)
        return null;

      List<KeyValuePair<String, IEnumerable<string>>> list = new List<KeyValuePair<string, IEnumerable<string>>>();

      foreach (var header in dictionary)
      {
        if(!header.Key.StartsWith(":"))
          list.Add(new KeyValuePair<string, IEnumerable<string>>(header.Key, header.Value));
      }
      return list;
    }

    List<KeyValuePair<String, IEnumerable<String>>> ContentHeadersMapper(String contentTypeValue, long? contentLength)
    {
      if (String.IsNullOrEmpty(contentTypeValue) && contentTypeValue == null) 
        return null;
      List<KeyValuePair<String, IEnumerable<string>>> list = new List<KeyValuePair<string, IEnumerable<string>>>();

      if (!String.IsNullOrEmpty(contentTypeValue))
      {
        String key = HeaderNames.ContentType;
        IEnumerable<string> enumerable = StringToEnumerable(contentTypeValue);
        KeyValuePair<String, IEnumerable<String>> kvp = new KeyValuePair<string, IEnumerable<string>>(key, enumerable);
        list.Add(kvp);
      }

      if (contentLength != null)
      {
        String key = HeaderNames.ContentLength;
        IEnumerable<string> enumerable = StringToEnumerable(contentLength.ToString());
        KeyValuePair<String, IEnumerable<String>> kvp = new KeyValuePair<string, IEnumerable<string>>(key, enumerable);
        list.Add(kvp);
      }
      return list;
    }

    IEnumerable<string> StringToEnumerable(String str)
    {
      yield return str;
    }
  }
}
