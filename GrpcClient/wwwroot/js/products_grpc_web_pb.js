/**
 * @fileoverview gRPC-Web generated client stub for products
 * @enhanceable
 * @public
 */

// GENERATED CODE -- DO NOT EDIT!


/* eslint-disable */
// @ts-nocheck



const grpc = {};
grpc.web = require('grpc-web');

const proto = {};
proto.products = require('./products_pb.js');

/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?Object} options
 * @constructor
 * @struct
 * @final
 */
proto.products.ProductsClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options['format'] = 'text';

  /**
   * @private @const {!grpc.web.GrpcWebClientBase} The client
   */
  this.client_ = new grpc.web.GrpcWebClientBase(options);

  /**
   * @private @const {string} The hostname
   */
  this.hostname_ = hostname;

};


/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?Object} options
 * @constructor
 * @struct
 * @final
 */
proto.products.ProductsPromiseClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options['format'] = 'text';

  /**
   * @private @const {!grpc.web.GrpcWebClientBase} The client
   */
  this.client_ = new grpc.web.GrpcWebClientBase(options);

  /**
   * @private @const {string} The hostname
   */
  this.hostname_ = hostname;

};


/**
 * @const
 * @type {!grpc.web.MethodDescriptor<
 *   !proto.products.GetAllRequest,
 *   !proto.products.SearchResult>}
 */
const methodDescriptor_Products_GetAll = new grpc.web.MethodDescriptor(
  '/products.Products/GetAll',
  grpc.web.MethodType.UNARY,
  proto.products.GetAllRequest,
  proto.products.SearchResult,
  /**
   * @param {!proto.products.GetAllRequest} request
   * @return {!Uint8Array}
   */
  function(request) {
    return request.serializeBinary();
  },
  proto.products.SearchResult.deserializeBinary
);


/**
 * @const
 * @type {!grpc.web.AbstractClientBase.MethodInfo<
 *   !proto.products.GetAllRequest,
 *   !proto.products.SearchResult>}
 */
const methodInfo_Products_GetAll = new grpc.web.AbstractClientBase.MethodInfo(
  proto.products.SearchResult,
  /**
   * @param {!proto.products.GetAllRequest} request
   * @return {!Uint8Array}
   */
  function(request) {
    return request.serializeBinary();
  },
  proto.products.SearchResult.deserializeBinary
);


/**
 * @param {!proto.products.GetAllRequest} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.Error, ?proto.products.SearchResult)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.products.SearchResult>|undefined}
 *     The XHR Node Readable Stream
 */
proto.products.ProductsClient.prototype.getAll =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/products.Products/GetAll',
      request,
      metadata || {},
      methodDescriptor_Products_GetAll,
      callback);
};


/**
 * @param {!proto.products.GetAllRequest} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.products.SearchResult>}
 *     A native promise that resolves to the response
 */
proto.products.ProductsPromiseClient.prototype.getAll =
    function(request, metadata) {
  return this.client_.unaryCall(this.hostname_ +
      '/products.Products/GetAll',
      request,
      metadata || {},
      methodDescriptor_Products_GetAll);
};


module.exports = proto.products;

