
    //    debugger;
    getCookie("EMVSoft");


    function getCookie(cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');


        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
    
                var Data = c.substring(name.length, c.length).split('&');
                for (var j = 0; j < Data.length; j++) {
                    var a = Data[j].split('=');
                    if (a[0] == "Group") {
                        if (a[1] != "Admin") {
                            // Code for Disable Right mouse click,cut copy and paste
                            $(document).ready(function () {
                                //Disable cut copy paste
                                $('body').bind('cut copy paste', function (e) {
                                    e.preventDefault();

                                });

                                document.onselectstart = new Function("return false")
                                if (window.sidebar) {
                                    document.onmousedown = false
                                }

                                //Disable mouse right click
                                $("body").on("contextmenu", function (e) {
                                    return false;
                                });



                            });
                        }
                    }
                }
            }
        }
        return "";
    };

