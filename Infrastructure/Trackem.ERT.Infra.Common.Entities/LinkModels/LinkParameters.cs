
using Microsoft.AspNetCore.Http;
namespace Trackem.ERT.Infra.Common.Entities.LinkModels;

/// <summary>
/// Requester Parameters
/// </summary>
/// <typeparam name="T"> T type</typeparam>
/// <param name="value">Value</param>
/// <param name="Context">HttpContext</param>
public record RequestParameters<T>(T value, HttpContext Context);