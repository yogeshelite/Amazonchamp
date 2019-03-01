function getUserId()
{
    var url = window.location.href;
    var vars = url.split('/');
    console.log(vars);
    var StrVal = vars[3] + '/' + vars[4];
   // console.log(StrVal);
   // var url1 = '/' + vars[5] + '/' + vars[6];
   // top.location = url1;
    return StrVal;
   
   
}
function getUserName() {
    var url = window.location.href;
    var vars = url.split('/');
    var StrVal = vars[3] / vars[4]
    return vars[3];

}
function getAsin() {
    var url = window.location.href;
    var vars = url.split('/');
    console.log(vars);
    return vars[7];

}
// About Form Use Only
function GetUserAbout(userIdPram) {
    var userId = "1E424621-6402-4ECC-870E-83D76BCF6739";
    // var userId = GetQueryStringParameter();
    console.log(userIdPram + '/Template1Controller/GetProductASIN');

    $.ajax({
        url: "/" + userIdPram + "/GetUserAbout",
        //url: "../../../UserAbout/GetUserAbout",
        type: "POST",
        data: {
            "UserId": userId

        },
        success: function (result) {
            console.log('testing');
            console.log(result);
            result = JSON.parse(result);
            var logoImg = '<img src="http://localhost:50552/u/gurvinder/' + '../../Doc/' + result.AttachmentLogoName + '" width="50" />';
            $("#DivTitle").append(result.AboutTitle);
            $("#divAboutSummary").append(result.AboutSummary);
            $("#Divlogo").append(logoImg);
            // return AsinListNew;
        }
    });
}
// User About Contact
function GetUserAboutContact(userIdPram) {
    var userId = "1E424621-6402-4ECC-870E-83D76BCF6739";
    $.ajax({
        url: "../../../UserAbout/GetUserAbout",
        type: "POST",
        data: {
            "UserId": userIdPram


        },
        success: function (result) {
            result = JSON.parse(result);
            var Facebook = result.Facebook;
            var Linkedin = result.Linkedin;
            var Twitter = result.Twitter;
            var SocialMediaLink = '<ul>' +
                '<li> <a href="' + Facebook + '" class="icon icon-cube agile_facebook"></a></li>' +
                '<li><a href="' + Linkedin + '" class="icon icon-cube agile_rss"></a></li>' +
                '<li><a href="' + Twitter + '" class="icon icon-cube agile_t"></a></li>' +
                '</ul >';
            $("#DivSocialMedia").append(SocialMediaLink);
            //DivPhone,DivAddress,DivPhone
            $("#DivAddress").append(result.Address);
            $("#DivPhone").append(result.PhoneNo);
            var logoImg = '<img src="http://localhost:50552/TemplateThemes/Templete1/' + '../../Doc/' + result.AttachmentLogoName + '" width="50" />';
            $("#Divlogo").append(logoImg);



            //  $("#DivEmailId").append(result.AboutSummary);
            // return AsinListNew;
        }
    });
}
// Send Mail
function SendMail() {
    GetProductAsinIndex
    var userIdPram = getUserId();
    var userId = "1E424621-6402-4ECC-870E-83D76BCF6739";

     var name = document.getElementById('txtName').value;
    var email = document.getElementById('txtEmail').value;
    var message = document.getElementById('txtMessage').value;
    var Phone = document.getElementById('txtPhone').value;
    var Subject = document.getElementById('txtSubject').value;
    console.log(Subject, email, message, name, Phone);
    //console.log("/" + userIdPram + "/SendMail");
    $.ajax({
        url: "/" + userIdPram + "/SendMail",
        //url: "../../../../UserAbout/SendMailContact",
        type: "POST",
        data: {
            "UserId": userId,
            "receiverEmailId": email,
            "name": name,
            "message": message,
            "phone": Phone,
            "subject": Subject
            
        },
        success: function (result) {
        }
    });
}
// index
  function GetProductAsinIndex(userIdPram) {

            var userId = "1E424621-6402-4ECC-870E-83D76BCF6739";
            // var userId = GetQueryStringParameter();
            //console.log(userIdPram + '/Template1Controller/GetProductASIN');
            $.ajax({
                url:"/"+userIdPram + "/GetProductASIN",
                type: "POST",
                data: {
                    "UserId": userId
                },
                success: function (result) {
                    //console.log(result);
                    var AsinList = "";
                    var AsinListFeature = "";
                    var AsinListNonFeature = "";
                    result = JSON.parse(result);
                    for (i = 0; i < result.length; i++) {
                        if (result[i].isFeatured)
                            AsinListFeature += result[i].ASIN + ",";
                        else
                            AsinListNonFeature += result[i].ASIN + ",";
                    }
                    var AsinListFeature = AsinListFeature.substr(0, AsinListFeature.length - 1);
                    var AsinListNonFeature = AsinListNonFeature.substr(0, AsinListNonFeature.length - 1);
                    GetAmazonProductIndex(AsinListFeature, true, userIdPram);
                    GetAmazonProductIndex(AsinListNonFeature, false, userIdPram);
                    GetUserAboutIndex(userIdPram);
                    // return AsinListNew;
                }
            });
        }
  function GetAmazonProductIndex(asin, isFeatured, userIdPram) {
      var userId = "1E424621-6402-4ECC-870E-83D76BCF6739";

      //console.log(userIdPram + '/Template1Controller/JsonGetItemFromAmazon');
            $.ajax({
                //url: "../../../Home/JsonGetItemFromAmazon",
                url: "/" + userIdPram + "/JsonGetItemFromAmazon",
                type: "POST",
                data: {
                    ASIN: asin,
                     "UserId": userId
                },
                success: function (result) {
                    //console.log(result);
                    if (result == "Not Found")
                        return false;
                    var object = JSON.parse(result);  //Dataset
                    var title = "";//object.ItemAttributes[0].Title;
                    var item = object.Item;
                    if (typeof object.Item === 'undefined')
                        return false;
                    var smallImage = object.SmallImage;
                    var DivFeaturedItems = "";
                    for (i = 0; i < object.Item.length; i++) {
                        // Item Bind RunTime
                        //=========Start

                        var ItemPrice = object.ListPrice[i].FormattedPrice;
                        DivFeaturedItems += ' <div class="col-md-4 top_brand_left">	<div class="col-md-4 top_brand_left"> ' +
                            '< div class="hover14 column" > ' +
                            '<div class="agile_top_brand_left_grid"> ' +
                            ' <div class="agile_top_brand_left_grid_pos"> ' +
                            '    <img src="http://localhost:50552/TemplateThemes/Templete1/images/offer.png" alt=" " class="img-responsive" /> ' +
                            ' </div> ' +
                            '<div class="agile_top_brand_left_grid1"> ' +
                            '   <figure> ' +
                            '      <div class="snipcart-item block" > ' +
                            '         <div class="snipcart-thumb"> ' +
                            '            <a href="products.html"><img title=" " alt=" " src="' + object.SmallImage[i].URL + '" /></a> ' +
                            '           <p>Tata-salt</p> ' +
                            '          <div class="stars"> ' +
                            '             <i class="fa fa-star blue-star" aria-hidden="true"></i> ' +
                            '            <i class="fa fa-star blue-star" aria-hidden="true"></i> ' +
                            '           <i class="fa fa-star blue-star" aria-hidden="true"></i> ' +
                            '          <i class="fa fa-star blue-star" aria-hidden="true"></i> ' +
                            '         <i class="fa fa-star gray-star" aria-hidden="true"></i> ' +
                            '    </div> ' +
                            '   <h4>$20.99 <span>$35.00</span></h4> ' +
                            '</div> ' +
                            '<div class="snipcart-details top_brand_home_details"> ' +
                            '   <form action="#" method="post"> ' +
                            '      <fieldset> ' +
                            '         <input type="hidden" name="cmd" value="_cart" /> ' +
                            '        <input type="hidden" name="add" value="1" /> ' +
                            '       <input type="hidden" name="business" value=" " /> ' +
                            '      <input type="hidden" name="item_name" value="Fortune Sunflower Oil" /> ' +
                            '     <input type="hidden" name="amount" value="20.99" /> ' +
                            '    <input type="hidden" name="discount_amount" value="1.00" /> ' +
                            '   <input type="hidden" name="currency_code" value="USD" /> ' +
                            '  <input type="hidden" name="return" value=" " /> ' +
                            ' <input type="hidden" name="cancel_return" value=" " /> ' +
                            '<input type="submit" name="submit" value="Add to cart" class="button" /> ' +
                            '</fieldset> ' +
                            '</form> ' +
                            ' </div> ' +
                            '</div> ' +
                            ' </figure> ' +
                            '</div> ' +
                            ' </div> ' +
                            '</div > ' +
                            '</div > ' +
                            ' <div class="hover14 column"> ' +
                            '    <div class="agile_top_brand_left_grid"> ' +
                            '       <div class="agile_top_brand_left_grid_pos"> ' +
                            '          <img src="http://localhost:50552/TemplateThemes/Templete1/images/offer.png" alt=" " class="img-responsive" /> ' +
                            '     </div> ' +
                            '    <div class="agile_top_brand_left_grid1"> ' +
                            '       <figure> ' +
                            '          <div class="snipcart-item block" > ' +
                            '             <div class="snipcart-thumb"> ' +
                            '                <a href="products.html"><img title=" " alt=" " src="' + object.SmallImage[i].URL + '" /></a> ' +
                            '               <p>' + object.ItemAttributes[i].Label + '</p> ' +
                            '              <div class="stars"> ' +
                            '                 <i class="fa fa-star blue-star" aria-hidden="true"></i> ' +
                            '                <i class="fa fa-star blue-star" aria-hidden="true"></i> ' +
                            '               <i class="fa fa-star blue-star" aria-hidden="true"></i> ' +
                            '              <i class="fa fa-star blue-star" aria-hidden="true"></i> ' +
                            '             <i class="fa fa-star gray-star" aria-hidden="true"></i> ' +
                            '        </div> ' +
                            '       <h4>' + ItemPrice + ' <span>$35.00</span></h4> ' +
                            '  </div> ' +
                            ' <div class="snipcart-details top_brand_home_details"> ' +
                            '    <form action="#" method="post"> ' +
                            '       <fieldset> ' +
                            '          <input type="hidden" name="cmd" value="_cart" /> ' +
                            '         <input type="hidden" name="add" value="1" /> ' +
                            '        <input type="hidden" name="business" value=" " /> ' +
                            '       <input type="hidden" name="item_name" value="Fortune Sunflower Oil" /> ' +
                            '      <input type="hidden" name="amount" value="20.99" /> ' +
                            '     <input type="hidden" name="discount_amount" value="1.00" /> ' +
                            '    <input type="hidden" name="currency_code" value="USD" /> ' +
                            '   <input type="hidden" name="return" value=" " /> ' +
                            '  <input type="hidden" name="cancel_return" value=" " /> ' +
                            ' <input type="submit" name="submit" value="Add to cart" class="button" /> ' +
                            ' </fieldset> ' +
                            ' </form> ' +
                            ' </div> ' +
                            '</div> ' +
                            '</figure>  ' +
                            ' </div> ' +
                            ' </div> ' +
                            ' </div> ' +
                            ' </div>';

                        //===========End

                    }
                    if (isFeatured)
                        $("#FeacturedItem").append(DivFeaturedItems);
                    else
                        $("#NonFeacturedItem").append(DivFeaturedItems);
                    // $("#table").append(tblComp);


                    // $("#table").append(content);

                    //$("#result").text(result);        //Just in case need to study result
                }
            });
        }
function GetUserAboutIndex(userIdPram) {
    var userId = "1E424621-6402-4ECC-870E-83D76BCF6739";
    console.log(userIdPram + '/Template1Controller/GetUserAbout');
    // var userId = GetQueryStringParameter();
    $.ajax({
        url: "/" + userIdPram +"/GetUserAbout",
        type: "POST",
        data: {
            "UserId": userId
        },
        success: function (result) {
            console.log(result);
            result = JSON.parse(result);
            var logoImg = '<img src="http://localhost:50552/u/gurvinder/' + '../../Doc/' + result.AttachmentLogoName + '" width="50" />';
           
            $("#Divlogo").append(logoImg);
            // return AsinListNew;
        }
    });
}
//All Product Form 
function GetProductAsinAllProducts(userIdPram) {
    var userId = "1E424621-6402-4ECC-870E-83D76BCF6739";
    
    // var userId = GetQueryStringParameter();
    $.ajax({
        //url: "../../../../UserProduct/GetProductASIN",
        url: "/" + userIdPram + "/GetProductASIN",

        type: "POST",
        data: {
            "UserId": userIdPram


        },
        success: function (result) {
            var AsinList = "";
            var AsinListFeature = "";
            var AsinListNonFeature = "";
            result = JSON.parse(result);
            for (i = 0; i < result.length; i++) {
                AsinListFeature += result[i].ASIN + ",";
            }
            AsinListFeature = AsinListFeature.substr(0, AsinListFeature.length - 1);
            GetAmazonProductAllProducts(AsinListFeature, userIdPram, getUserName());
            GetUserAboutAllProducts(userIdPram);
            // return AsinListNew;
        }
    });

}
function GetAmazonProductAllProducts(asin, userid, username) {
    console.log(username);
    $.ajax({
        url: "../../../../Home/JsonGetItemFromAmazon",
        type: "POST",
        data: {
            ASIN: asin


        },
        success: function (result) {
            if (result == "Not Found")
                return false;
            var object = JSON.parse(result);  //Dataset
            var item = object.Item;
            if (typeof object.Item === 'undefined')
                return false;
            var smallImage = object.SmallImage;
            var DivFeaturedItems = "";
            for (i = 0; i < object.Item.length; i++) {
                // Item Bind RunTime
                //=========Start
                var ItemPrice = object.ListPrice[i].FormattedPrice;
                DivFeaturedItems += ' <div class="col-md-4 top_brand_left"> ' +
                    '<div class="hover14 column" > ' +
                    '<div class="agile_top_brand_left_grid"> ' +
                    '<div class="agile_top_brand_left_grid_pos"> ' +
                    '<img src="http://localhost:50552/TemplateThemes/Templete1/images/offer.png" alt=" " class="img-responsive"> ' +
                    '</div> ' +
                    '<div class="agile_top_brand_left_grid1"> ' +
                    '<figure> ' +
                    '<div class="snipcart-item block"> ' +
                    '<div class="snipcart-thumb"> ' +
                    '<a href="http://localhost:50552/singleproduct/1userName/' + username+'/' + userid + '/' + object.Item[i].ASIN + '"><img title=" " alt=" " src="' + object.SmallImage[i].URL + '"></a> ' +
                    '<p>' + object.ItemAttributes[i].Label + '</p> ' +
                    '<h4>' + object.ListPrice[i].FormattedPrice + ' <span>$55.00</span></h4> ' +
                    '</div> ' +
                    '<div class="snipcart-details top_brand_home_details"> ' +
                    '<form action="#" method="post"> ' +
                    '<fieldset> ' +
                    '<input type="hidden" name="cmd" value="_cart"> ' +
                    '<input type="hidden" name="add" value="1"> ' +
                    '<input type="hidden" name="business" value=" "> ' +
                    '<input type="hidden" name="item_name" value="Fortune Sunflower Oil"> ' +
                    '<input type="hidden" name="amount" value="35.99"> ' +
                    '<input type="hidden" name="discount_amount" value="1.00"> ' +
                    '<input type="hidden" name="currency_code" value="USD"> ' +
                    '<input type="hidden" name="return" value=" "> ' +
                    '<input type="hidden" name="cancel_return" value=" "> ' +
                    '<input type="submit" name="submit" value="Add to cart" class="button"> ' +
                    '</fieldset> ' +
                    '</form> ' +
                    '</div> ' +
                    '</div> ' +
                    '</figure> ' +
                    '</div> ' +
                    '</div> ' +
                    '</div> ' +
                    '</div> ';

                //===========End

            }
            $("#DivAllProducts").append(DivFeaturedItems);
        }
    });
}
function GetUserAboutAllProducts(userIdPram) {
    //var userId = "1E424621-6402-4ECC-870E-83D76BCF6739";
    // var userId = GetQueryStringParameter();
    $.ajax({
        url: "../../../UserAbout/GetUserAbout",
        type: "POST",
        data: {
            "UserId": userIdPram


        },
        success: function (result) {
            result = JSON.parse(result);
            var logoImg = '<img src="http://localhost:50552/TemplateThemes/Templete1/' + '../../Doc/' + result.AttachmentLogoName + '" width="50" />';

            $("#Divlogo").append(logoImg);
            // return AsinListNew;
        }
    });
}
//Single Product
function GetAmazonProductSingleProduct(asin, userid) {
    $.ajax({
        url: "../../../../Home/JsonGetItemFromAmazon",
        type: "POST",
        data: {
            ASIN: asin


        },
        success: function (result) {
            if (result == "Not Found")
                return false;
            var object = JSON.parse(result);  //Dataset
           // console.log(result);
            var item = object.Item;
            if (typeof object.Item === 'undefined')
                return false;
            var smallImage = object.SmallImage;
            var DivSingleItems = "";
            for (i = 0; i < object.Item.length; i++) {
                // Item Bind RunTime
                //=========Start
              //  console.log("UserId=" + userid + "ASIN No=" + object.Item[i].ASIN);
                var ItemPrice = object.ListPrice[i].FormattedPrice;

                //===========End

            }

            DivSingleItems = '<div class="col-md-4 agileinfo_single_left">' +
                '<img id="example" src="' + object.LargeImage[0].URL + '" alt=" " class="img-responsive" >' +
                '</div >' +
                '<div class="col-md-8 agileinfo_single_right" > ' +
                '<h2 > ' + object.ItemAttributes[0].Label + '</h2 > ' +

                '</div>';


            $("#DivSingleProduct").append(DivSingleItems);
        }
    });
    GetUserAboutSingleProduct(userid)
}

function GetUserAboutSingleProduct(userIdPram) {
    //var userId = "1E424621-6402-4ECC-870E-83D76BCF6739";
    // var userId = GetQueryStringParameter();
    $.ajax({
        url: "../../../../UserAbout/GetUserAbout",
        type: "POST",
        data: {
            "UserId": userIdPram


        },
        success: function (result) {
            result = JSON.parse(result);
            var logoImg = '<img src="http://localhost:50552/TemplateThemes/Templete1/' + '../../Doc/' + result.AttachmentLogoName + '" width="50" />';

            $("#Divlogo").append(logoImg);
            // return AsinListNew;
        }
    });
}




