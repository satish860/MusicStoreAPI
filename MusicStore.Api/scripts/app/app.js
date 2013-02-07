/// <reference path="../backbone.js" />
/// <reference path="../main.js" />
/// <reference path="../require-jquery.js" />

var categoryCollection = new CategoryCollection();

categoryCollection.bind("reset", function () {
    categoryCollection.forEach(function (ctgry) {
        var cat = new CategoryView({
            model: ctgry
        });
        $("#category").append(cat.render().el);
    });
});

categoryCollection.fetch();


var productCollection = new ProductCollection();

productCollection.bind("reset", function () {
    productCollection.forEach(function (prod) {
        prod = JSON.parse(JSON.stringify(prod));
        _(prod.products).each(function (data) {
            alert(JSON.stringify(data));

            var prd = new ProductView({
                model: data
            });
            $("#product").append(prd.render().el);

        });
        
        
    });
});

productCollection.fetch();