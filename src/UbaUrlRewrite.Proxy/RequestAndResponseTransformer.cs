// <copyright file="RequestAndResponseTransformer.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// </copyright>

namespace UbaUrlRewrite.Proxy;

using Yarp.ReverseProxy.Forwarder;

public class RequestAndResponseTransformer : HttpTransformer
{
	private readonly Func<HttpContext, HttpRequestMessage, ValueTask> requestTransform;
	private readonly Func<HttpContext, HttpResponseMessage?, ValueTask> responseTransform;

	public RequestAndResponseTransformer(Func<HttpContext, HttpRequestMessage, ValueTask> requestTransform,
		Func<HttpContext, HttpResponseMessage?, ValueTask> responseTransform)
	{
		this.requestTransform = requestTransform;
		this.responseTransform = responseTransform;
	}

	public override async ValueTask TransformRequestAsync(
		HttpContext httpContext,
		HttpRequestMessage proxyRequest,
		string destinationPrefix,
		CancellationToken cancellationToken)
	{
		ValueTask valueTask = base.TransformRequestAsync(httpContext, proxyRequest, destinationPrefix, cancellationToken);
		await valueTask;
		valueTask = this.requestTransform(httpContext, proxyRequest);
		await valueTask;
	}

	public override async ValueTask<bool> TransformResponseAsync(HttpContext httpContext, HttpResponseMessage? proxyResponse, CancellationToken cancellationToken)
	{
		ValueTask<bool> valueTask = base.TransformResponseAsync(httpContext, proxyResponse, cancellationToken);
		bool result = await valueTask;
		await this.responseTransform(httpContext, proxyResponse);
		return result;
	}
}