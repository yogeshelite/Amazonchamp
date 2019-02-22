function FillApiResult(url, data, method, contentType) {
 
    return $.ajax({
        url: url,
        dataType: "json",
        async: false,
        contentType: contentType,
        method: method,
        data: JSON.stringify(data),
        header: JSON.stringify(_header),
        success: function (result) {
            return result;

        },
        error: function (err) { return err.MessageText }
    });
}