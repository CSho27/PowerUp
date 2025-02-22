using System;
using System.Net;

namespace PowerUp.Fetchers
{
  public class HttpStatusException : Exception
  {
    public HttpStatusCode StatusCode { get; set; }

    public HttpStatusException(HttpStatusCode statusCode)
      : base(statusCode.ToString()) 
    {
      StatusCode = statusCode;
    }
  }
}
