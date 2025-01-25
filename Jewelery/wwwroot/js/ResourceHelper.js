
var ResourceHelperGetter = {
    getStringFromResourse: function (key) {
        return $.ajax({
            url: '/ResourceHelper/GetString',
            type: 'GET',
            data: { key: key },
            async:false
        }).responseText;
    }
}

var ResourceHelper = {
    getString: function (key) {
        return decodeURIComponent(JSON.parse(ResourceHelperGetter.getStringFromResourse(key)));

    }
}