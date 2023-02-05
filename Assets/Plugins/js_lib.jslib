mergeInto(LibraryManager.library, {
    setCookie: function (cname, cvalue, exdays) {
       var d = new Date();
       d.setTime(d.getTime() + (exdays*24*60*60*1000));
       var expires = "expires="+ d.toUTCString();
       document.cookie = Pointer_stringify(cname) + "=" + Pointer_stringify(cvalue) + expires + ";path=/";
       console.log('set cookie='+document.cookie);
    },
 
    getCookie: function (cname) {
       var ret="";
       var name = Pointer_stringify(cname) + "=";
       var decodedCookie = decodeURIComponent(document.cookie);
       console.log('get cookie='+decodedCookie);
       var ca = decodedCookie.split(';');
       for(var i = 0; i <ca.length; i++) {
           var c = ca[i];
           while (c.charAt(0) == ' ') {
               c = c.substring(1);
           }
           if (c.indexOf(name) == 0) {
               ret=c.substring(name.length, c.length);
               break;
           }
       }
       var buffer = _malloc(lengthBytesUTF8(ret) + 1);
       writeStringToMemory(ret, buffer);
       return buffer;
    },

    getToken: function(strKey) {
        var n = sessionStorage.getItem(UTF8ToString(strKey));
        //window.alert("Get Token : " + n);
        if( n == null )
            return ""

        var len = lengthBytesUTF8(n) + 1;
        var buffer = _malloc(len);

        stringToUTF8(n, buffer, len);

        return buffer;
    },

    setToken: function(strKey, strToken) {
        sessionStorage.setItem(UTF8ToString(strKey), UTF8ToString(strToken));
        //window.alert("Set Token : " + strToken);
    },

    getTokenTest: function(strKey) {
        console.log("TestGet");
        window.alert("Get Token : " + strKey);
        return strKey;
    },

    setTokenTest: function(strKey, strToken) {
        console.log("TestSet");
        window.alert("Set Token : " + strToken);
    },

    showAlert: function(strMsg) {
        window.alert(UTF8ToString(strMsg));
    }
});