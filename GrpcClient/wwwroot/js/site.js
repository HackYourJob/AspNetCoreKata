const { GetAllRequest, SearchResult } = require('./products_pb.js');
const { ProductsClient } = require('./products_grpc_web_pb.js');

var client = new ProductsClient('https://localhost:5101');

var request = new GetAllRequest();
request.setQuality(GetAllRequest.Quality.DOUBLE);

client.getAll(request, { 'Authorization': 'Bearer Toto' }, (err, response) => {
    if (err) {
        console.error(err);
        return;
    }

    response.getProductsList().forEach(p => {
        console.log(p.getId() + ' - ' + p.getName());
    });
});
