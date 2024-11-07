// <copyright file="HttpForwarderExtensions.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Proxy;

using Yarp.ReverseProxy.Forwarder;

public static class HttpForwarderExtensions
{
	public static ValueTask<ForwarderError> SendAsync(this IHttpForwarder forwarder, HttpContext context, string destinationPrefix,
		HttpMessageInvoker httpClient, ForwarderRequestConfig requestConfig,
		Func<HttpContext, HttpRequestMessage, ValueTask> requestTransform,
		Func<HttpContext, HttpResponseMessage?, ValueTask> responseTransform) =>
		forwarder.SendAsync(context, destinationPrefix, httpClient, requestConfig,
			new RequestAndResponseTransformer(requestTransform, responseTransform));
}